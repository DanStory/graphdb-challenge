Function Export-HostDockerMachine {
    $hostMachine = $env:COMPUTER_NAME
    if((Get-Command docker-machine)){
        $hostMachine = $(docker-machine ip $env:DOCKER_MACHINE_NAME)
        if($LASTEXITCODE -ne 0){
            $hostMachine = [System.Net.Dns]::GetHostName()
        }
    }

    if(![String]::IsNullOrWhiteSpace($hostMachine)){
        [System.Environment]::SetEnvironmentVariable('HOSTMACHINE', $hostMachine, 'Process');
        return $hostMachine
    }
}

Export-ModuleMember Export-HostDockerMachine