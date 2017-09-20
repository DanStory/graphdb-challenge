FROM microsoft/dotnet:2.0-runtime
RUN mkdir /app
WORKDIR /app
ENTRYPOINT ["dotnet", "GraphDb.ExampleData.dll"]
COPY /publish .