#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarAPI/CarAPI.csproj", "CarAPI/"]
RUN dotnet restore "CarAPI/CarAPI.csproj"
COPY . .
WORKDIR "/src/CarAPI"
RUN dotnet build "CarAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarAPI.dll"]