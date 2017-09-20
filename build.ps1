$ErrorActionPreference='Stop'

Import-Module ./functions.psm1 -Force
"Host Machine: $(Export-HostDockerMachine)"

# fix bash scripts' line endings (remove CR)
Get-ChildItem -Filter "docker-compose.build.sh" -Recurse | ForEach-Object {
    (Get-Content -Path $_.FullName -Raw).Replace("`r","") | Set-Content -Path $_.FullName -NoNewline
}

docker-compose -f ./docker-compose.build.yml up --remove-orphans
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }

docker-compose -f ./docker-compose.yml build
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }

docker build -t danstory/graphdb.exampledata ./src/GraphDb.ExampleData
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }