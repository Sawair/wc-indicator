# WC Indicator
**NOTICE** This software is currently in *Work in Progress* stage.

Projects aim is to create online indicator where you could observe if the Toilet is free or not.

## Getting Started

To install this in your office you will need:
  - Orange Pi
  - Magnetic Meter  #todo: @sawair
  - internet connection to central server which will be deployed
  - some host for aspnet core

## Server

Our platform of choice is Windows Server 2016 with SQL Server 2017, yours may be different.

 - Install dotnet sdk 2.1 from official site https://www.microsoft.com/net/download/dotnet-core/2.1
 - set environment variables or configure appsettings.json to match yours SQL Server connection string
 - run `cd wcindicator.api/;dotnet run`
  
  For IIS Publishing use below commands
``` 
cd wcindicator.api/
dotnet restore ./wcindicator.api.csproj
dotnet msbuild ./wcindicator.api.csproj /t:Rebuild
dotnet publish ./wcindicator.api.csproj /p:PublishProfile=Prod
```


## Agents/Sensors


**wcindicator.agent** is the project you have to deploy to the orange pi device and run `python run.py` file.
