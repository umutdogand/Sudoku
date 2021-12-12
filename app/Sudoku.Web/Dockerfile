#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["app/Sudoku.Web/Sudoku.Web.csproj", "app/Sudoku.Web/"]
COPY ["shared/Sudoku.Helper/Sudoku.Generator.csproj", "shared/Sudoku.Helper/"]
RUN dotnet restore "app/Sudoku.Web/Sudoku.Web.csproj"
COPY . .
WORKDIR "/src/app/Sudoku.Web"
RUN dotnet build "Sudoku.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sudoku.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sudoku.Web.dll"]