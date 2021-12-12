# Sudoku

## Getting started
* Install the [.NET Core 6.0 SDK.](https://dotnet.microsoft.com/download/dotnet/6.0)
* Download [Docker Desktop](https://www.docker.com/products/docker-desktop/) for Mac or Windows. [Docker Compose](https://docs.docker.com/compose/) will be automatically installed. On Linux, make sure you have the latest version of [Compose](https://docs.docker.com/compose/install/).

## Build and Run 
* Dowloand and extract files in Sudoku-master.zip folder. 
* For build the projects, In the directory where Sudoku.sln is located, the following command is executed by opening command promt or powershell.

```bash
dotnet build --configuration Release
```
After that, in the root of the project, then start running with docker. After run bellow command, web project will up:
```bash
docker-compose -f "ci/docker-compose.yml" -f "ci/docker-compose.override.yml" -p dockercomposesudokuweb --ansi never up -d
``` 

## Usage
 
1- When client application run user can play the game.

2- The web project works on [localhost:8081/](http://localhost:8081) 
 
