FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY [^wwwroot]* .

RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM node:20.12.2-alpine3.19 AS node_build
WORKDIR /node_build
COPY wwwroot .
RUN npm i
RUN npm run build


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node_build /node_build/build wwwroot/build

ENTRYPOINT ["dotnet", "WebApp.dll"]