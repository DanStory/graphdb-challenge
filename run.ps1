$ErrorActionPreference='Stop'

# https://stackoverflow.com/questions/46042261/in-osx-how-to-open-another-powershell-console-from-a-powershell-script
Function Start-InNewWindowMacOS {
    param(
       [Parameter(Mandatory)] [ScriptBlock] $ScriptBlock,
       [Switch] $NoProfile,
       [Switch] $NoExit
    )
  
    # Construct the shebang line 
    $shebangLine = '#!/usr/bin/env powershell'
    # Add options, if specified:
    # As an aside: Fundamentally, this wouldn't work on Linux, where
    # the shebang line only supports *1* argument, which is `powershell` in this case.
    if ($NoExit) { $shebangLine += ' -NoExit' }
    if ($NoProfile) { $shebangLine += ' -NoProfile' }
  
    # Create a temporary script file
    $tmpScript = New-TemporaryFile
  
    # Add the shebang line, the self-deletion code, and the script-block code.
    # Note: 
    #      * The self-deletion code assumes that the script was read *as a whole*
    #        on execution, which assumes that it is reasonably small.
    #        Ideally, the self-deletion code would use 
    #        'Remove-Item -LiteralPath $PSCommandPath`, but, 
    #        as of PowerShell Core v6.0.0-beta.6, this doesn't work due to a bug 
    #        - see https://github.com/PowerShell/PowerShell/issues/4217
    #      * UTF8 encoding is desired, but -Encoding utf8, regrettably, creates
    #        a file with BOM. For now, use ASCII.
    #        Once v6 is released, BOM-less UTF8 will be the *default*, in which
    #        case you'll be able to use `> $tmpScript` instead.
    $shebangLine, "Remove-Item -LiteralPath '$tmpScript'", $ScriptBlock.ToString() | 
      Set-Content -Encoding Ascii -LiteralPath $tmpScript
  
    # Make the script file executable.
    chmod +x $tmpScript
  
    # Invoke it in a new terminal window via `open -a Terminal`
    # Note that `open` is a macOS-specific utility.
    open -a Terminal -- $tmpScript
}

docker-compose rm -sf
docker rm -f graphdb.exampledata

docker rm graphdb.exampledata -f

$cli = "docker run --interactive --tty --rm --env=APIURL=http://$([System.Net.Dns]::GetHostName()):8080 --name graphdb.exampledata danstory/graphdb.exampledata"
if($PSVersionTable.Platform -eq 'Windows')
{
    Start-Process "cmd.exe" "/C $cli"
}else{
    $setenv = (([System.Environment]::GetEnvironmentVariables().GetEnumerator() `
        | Where-Object { $_.Key.StartsWith('DOCKER_') } `
        | ForEach-Object { "[System.Environment]::SetEnvironmentVariable('$($_.Key)','$($_.Value)','Process')" }) `
        ) -join "`r"
    Start-InNewWindowMacOS -ScriptBlock ([ScriptBlock]::Create("$setenv`r$cli"))
}

docker-compose up
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }