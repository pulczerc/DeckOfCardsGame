#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DeckOfCardsGame.API/DeckOfCardsGame.API.csproj", "DeckOfCardsGame.API/"]
COPY ["DeckOfCardsGame.Common/DeckOfCardsGame.Common.csproj", "DeckOfCardsGame.Common/"]
COPY ["DeckOfCardsGame.DAL/DeckOfCardsGame.DAL.csproj", "DeckOfCardsGame.DAL/"]
RUN dotnet restore "./DeckOfCardsGame.API/./DeckOfCardsGame.API.csproj"
COPY . .
WORKDIR "/src/DeckOfCardsGame.API"
RUN dotnet build "./DeckOfCardsGame.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DeckOfCardsGame.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeckOfCardsGame.API.dll"]