# Multi-Stage Build para aplicação .NET 8.0

# === 1. BUILD STAGE (sdk) ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia apenas o arquivo .csproj, usando um coringa para garantir que o Docker encontre.
# O nome do projeto (Fiap.Web.Api.Desperdicio) é usado como referência.
COPY *.csproj ./

# 2. Restaura as dependências (baixa o Npgsql, etc.)
RUN dotnet restore

# 3. Copia o restante do código fonte
COPY . .

# 4. Constrói a aplicação
RUN dotnet build -c Release -o /app/build

# === 2. PUBLISH STAGE (Publica a aplicação) ===
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# === 3. FINAL STAGE (aspnet runtime) ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Web.Api.Desperdicio.dll"]
