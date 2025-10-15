FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY Fiap.Web.Api.Desperdicio.sln .
COPY Fiap.Web.Api.Desperdicio.csproj .


RUN dotnet restore "Fiap.Web.Api.Desperdicio.sln"


COPY . .


RUN dotnet build "Fiap.Web.Api.Desperdicio.csproj" -c Release -o /app/build /p:ExcludeRestore=true

FROM build AS publish


RUN dotnet publish "Fiap.Web.Api.Desperdicio.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:NoBuild=true


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Web.Api.Desperdicio.dll"]
