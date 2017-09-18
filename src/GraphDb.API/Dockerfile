FROM microsoft/aspnetcore:2.0
RUN mkdir /app
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "GraphDb.API.dll"]
COPY /publish .