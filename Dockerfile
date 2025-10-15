FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY Fiap.Web.Api.Desperdicio.sln .
COPY Fiap.Web.Api.Desperdicio.csproj .
COPY Fiap.Web.Api.Desperdicio.Tests/*.csproj Fiap.Web.Api.Desperdicio.Tests/

RUN dotnet restore "Fiap.Web.Api.Desperdicio.sln"

COPY . .

RUN rm -rf Fiap.Web.Api.Desperdicio.Tests


FROM build AS publish

RUN dotnet publish "Fiap.Web.Api.Desperdicio.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Web.Api.Desperdicio.dll"]
