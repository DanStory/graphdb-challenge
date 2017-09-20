$ErrorActionPreference='Stop'

docker-compose rm -sf

docker rm graphdb.exampledata -f
Start-Process "cmd.exe" "/C docker run --interactive --tty --rm --env=APIURL=http://$($env:COMPUTERNAME):8080 --name graphdb.exampledata danstory/graphdb.exampledata"

docker-compose up
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }