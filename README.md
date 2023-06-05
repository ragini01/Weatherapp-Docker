# Weatherapp-Docker(ASP.Net Core)

This application is created using ASP.NET Core framework, an API Gateway KONG and Konga on docker. This application basically gives the current weather condition for a city by consuming OpenWeatherMapApi.

## Project Structure

1. src/WeatherForecastApp - contains the main application and source code of the project which includes model, view and controller section.
2. Dockerfile - This file contains step by step instructions to deploy the application image inside a docker container. It also contains an entry for KONG Gateway congfiguration.
3. docker-compose.yml - This file contains configuration details for KONG Gateway, KONGA and POSTGRES
4. README.md - Contains information about application building, findings and instructions for building the application, running the endpoints/apis & automated tests.

## How it works



## Installation

To run this .Net web API application, follow these steps:
1. Install the .NET SDK 6/7, if you have not it. You can refer Microsoft official website (https://dotnet.microsoft.com/download) to get the installer and follow the docs for installation processes.
2. Install your preferrable code editors either Visual Studio Code or Visual Studio(any version, may be the latest one's like 2017 onwards.)
3. Clone the source code from GitHub using below command in your command prompt or using git Bash terminal.
   `git clone https://github.com/ragini01/website-monitoring-script.git`
5. Once you have the clone copy of the source code, open the code with your code editor and navigate to the root directory of the project.
6. Open a new terminal in the root directory and try building the project by running the following command:
   `dotnet build WeatherForecastApp.csproj`
7. Since the application uses docker, make sure you have docker installed in your local system to run the application image. Refer this link to get docker running on your system for Windows, 
[Install Docker for Windows](https://docs.docker.com/docker-for-windows/install/)
8. If you have docker running locally or you're done with the setup, run the following commands:
   i. `docker build -t weatherforecastapp .`
  ii. `docker container run -d weatherforecastapp`
  iii. `docker container ls`
  Above commend will give you the container details and its current status.
  iv. `docker container inspect — format ‘{{.NetworkSettings.Networks.nat.IPAddress }}’ CONTAINERNAME
  You will receive the container name by running the 2nd command, replace the name with CONTAINERNAME. This will return an IP address, which you then need to use in the browser to see application running.
9. Once you enter the city to get the forecast for, the application will return the details like:
     Forecast for the selected city
     City:
     Temperature:
     Humidity:
     Pressure:
     WindSpeed:
     WeatherCondition:
10. All the above steps were to test the application into docker containers. Now, Will do the setup for KONG in Docker. You need to 1st setup Kong into your machine. You can do the setups by referring to their official website(https://docs.konghq.com/install/docker/#:~:text=%20The%20steps%20involved%20in%20starting%20Kong%20in,Prepare%20your%20declarative%20configuration%20file%0AThe%20syntax...%20More)
11. Once the KONG setup is ready, verify its running using below command which will return 200 OK response:
    `curl -i http://localhost:8001/`
12. Now, We have to expose our api through KONG Gateway, so I have added service and Route objects which will let my api expose through KONG Gateway.
13. And, then you will finally find the api running on port 8000,
    `localhost:8000/WeatherForecastApp/?`    
## Usage

## Overall assumptions/findings to develop this application

## Notes
I am facing issues in the unit test files and currently trying to fix them, so please run and access the application by excluding WeatherForecastApp/Tests folder.
