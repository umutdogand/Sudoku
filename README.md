# Sudoku

## Getting started
* Install the [.NET Core 6.0 SDK.](https://dotnet.microsoft.com/download/dotnet/6.0)
* Download [Docker Desktop](https://www.docker.com/products/docker-desktop/) for Mac or Windows. [Docker Compose](https://docs.docker.com/compose/) will be automatically installed. On Linux, make sure you have the latest version of [Compose](https://docs.docker.com/compose/install/).

## Build and Run 
* Dowloand and extract files in Sudoku-master.zip folder. 
* For run the projects, In the directory where Sudoku.sln is located, the following command is executed by opening command promt or powershell. After run bellow command, web project will up.(Docker must run before that command)
```bash
docker-compose -f "ci/docker-compose.yml" -f "ci/docker-compose.override.yml" -p dockercomposesudokuweb --ansi never up -d
``` 

## Usage
 
1- When client application run user can play the game.

2- The web project works on [localhost:8081/](http://localhost:8081) 

3- For the upload file for solve the sudoku you can use example_sudoku.txt
 
