# Multi-Stage Build para aplica��o .NET 8.0

# === 1. BUILD STAGE (sdk) ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia apenas o arquivo .csproj, usando um coringa para garantir que o Docker encontre.
# O nome do projeto (Fiap.Web.Api.Desperdicio) � usado como refer�ncia.
COPY *.csproj ./

# 2. Restaura as depend�ncias (baixa o Npgsql, etc.)
RUN dotnet restore

# 3. Copia o restante do c�digo fonte
COPY . .

# 4. Constr�i a aplica��o
RUN dotnet build -c Release -o /app/build

# === 2. PUBLISH STAGE (Publica a aplica��o) ===
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# === 3. FINAL STAGE (aspnet runtime) ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Web.Api.Desperdicio.dll"]
