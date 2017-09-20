#!bin/bash
set -e

dotnet restore ./GraphDb.ExampleData.csproj

rm -rf ./publish
dotnet publish ./GraphDb.ExampleData.csproj -c release -o ./publish