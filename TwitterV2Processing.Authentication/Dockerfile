#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8088
ENV ASPNETCORE_URLS=http://+:8088

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TwitterV2Processing.Authentication/TwitterV2Processing.Authentication.csproj", "TwitterV2Processing.Authentication/"]
RUN dotnet restore "./TwitterV2Processing.Authentication/TwitterV2Processing.Authentication.csproj"
COPY . .
WORKDIR "/src/TwitterV2Processing.Authentication"
RUN dotnet build "./TwitterV2Processing.Authentication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TwitterV2Processing.Authentication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwitterV2Processing.Authentication.dll"]