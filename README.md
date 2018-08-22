# WC Indicator
**NOTICE** This software is currently in *Work in Progress* stage.

Projects aim is to create online indicator where you could observe if the Toilet is free or not.

## Getting Started

To install this in your office you will need:
  - Orange Pi
  - Magnetic Meter HO-03N
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


## Agents


**wcindicator.agent** is the project you have to deploy to the orange pi device.
Agent need's to have [orangepi_PC_gpio_pyH3](https://github.com/duxingkei33/orangepi_PC_gpio_pyH3) installed on device, project is added as submodule in **wcindicator.agent/gpio**. To first run use below commands
```
python setup.py install 
sudo python run.py
```
Every next time run `sudo python run.py`
Also we recomend adding cron entry using command `sudo crontab -e`, and in editor add line:
```
@reboot /usr/bin/python /{project-dir}/wcindicator.agent/run.py
```


## Sensors

Connection scheme you can find in [Scheme.png](https://github.com/Sawair/wc-indicator/blob/master/Scheme.png).
Outputs X1 and X2 are for Magnetic Meter.
