#!bin/bash
set -e

dotnet restore ./GraphDb.API.Tests/GraphDb.API.Tests.csproj
dotnet restore ./GraphDb.API/GraphDb.API.csproj

dotnet test ./GraphDb.API.Tests/GraphDb.API.Tests.csproj

rm -rf ./GraphDb.API/publish
dotnet publish ./GraphDb.API/GraphDb.API.csproj -c release -o ./publish