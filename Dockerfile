#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IntuitAssignment/IntuitAssignment.csproj", "IntuitAssignment/"]
COPY ["IntuitAssignment/ExternalResources/players.csv", "IntuitAssignment/ExternalResources/"]
COPY ["IntuitAssignment.Scrapers/IntuitAssignment.Scrapers.csproj", "IntuitAssignment.Scrapers/"]
COPY ["IntuitAssignment.API.Models/IntuitAssignment.API.Models.csproj", "IntuitAssignment.API.Models/"]
COPY ["IntuitAssignment.Engine/IntuitAssignment.Engine.csproj", "IntuitAssignment.Engine/"]
COPY ["IntuitAssignment.DAL/IntuitAssignment.DAL.csproj", "IntuitAssignment.DAL/"]
COPY ["IntuitAssignment.DAL.Models/IntuitAssignment.DAL.Models.csproj", "IntuitAssignment.DAL.Models/"]
COPY ["IntuitAssignment.Utils/IntuitAssignment.Utils.csproj", "IntuitAssignment.Utils/"]
RUN dotnet restore "IntuitAssignment/IntuitAssignment.csproj"
COPY . .
WORKDIR "/src/IntuitAssignment"
RUN dotnet build "IntuitAssignment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntuitAssignment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntuitAssignment.dll"]