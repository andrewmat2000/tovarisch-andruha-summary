FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Identity/Identity.Domain/Identity.Domain.csproj", "Identity/Identity.Domain/"]
COPY ["Identity/Identity.Application/Identity.Application.csproj", "Identity/Identity.Application/"]
COPY ["Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity/Identity.Infrastructure/"]
COPY ["Identity/Identity.Web/Identity.Web.csproj", "Identity/Identity.Web/"]

RUN dotnet restore "Identity/Identity.Domain/Identity.Domain.csproj"
COPY . .
WORKDIR "/src/Identity/Identity.Web"
RUN dotnet build "Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY Identity/Identity.Web/default-admin-avatar.jpg .

RUN apt update
RUN apt install curl -y

HEALTHCHECK --interval=7s --timeout=50s --retries=7 CMD curl http://localhost/.well-known/openid-configuration || exit 1

ENTRYPOINT ["dotnet", "Identity.Web.dll", "--urls", "http://0.0.0.0"]