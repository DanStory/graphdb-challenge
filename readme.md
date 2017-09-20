# GraphDb
   Example data graph application using Neo4J, ASP.NET Core, Swagger, SignalR, and D3.js.

## Applications
   * `GraphDb.API` - ASP.NET Core Web API providing RESTful endpoint with Swagger and SignalR Hub.
   * `GraphDb.Client` - JS Web App hosted in Node.JS, using D3.js and SignalR Client.
   * `GraphDb.ExampleData` .NET Core Console App, generating base data and streams random updates to simulate events.

## Build
   `docker` and `docker-compose` are required dependencies to compile and run applications (also Powershell Core for MacOS).
   * `./build.ps1` to compile .NET Core applications and build docker images.
   * `./run.ps1` executes `docker-compose` and runs an interactive shell to `GraphDb.ExampleData` docker container.