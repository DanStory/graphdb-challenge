$ErrorActionPreference='Stop'

docker-compose -f ./docker-compose.build.yml up
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }

docker-compose -f ./docker-compose.yml build
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }

docker build -t danstory/graphdb.exampledata ./src/GraphDb.ExampleData
if($LASTEXITCODE -ne 0){ exit $LASTEXITCODE }