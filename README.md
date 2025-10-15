# 🌱 Projeto - API de Gerenciamento de Desperdício ESG

## 👥 Integrantes
Adriano de Camargo

---

## 🚀 Como executar localmente com Docker

Esta API de monitoramento de desperdício é um serviço **C# (.NET 8.0)** que utiliza **PostgreSQL** como banco de dados.  
Usamos o **Docker Compose** para orquestrar e isolar os ambientes (Staging e Produção).

> **Pré-requisitos:** Docker e Docker Compose instalados.

---

### 1. 🧩 Preparação (Ambiente Staging)

Baixe a imagem mais recente (gerada via CI/CD) e inicie os serviços (API + PostgreSQL) no modo Staging:

```bash
# Baixa a imagem mais recente do GHCR
docker pull ghcr.io/adriano2606/fiap.web.api.desperdicio/api-desperdicio-esg:latest

# Sobe o ambiente Staging
docker compose --env-file .env.staging up -d
```

A API estará acessível em:  
👉 [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

### 2. 🌐 Executando o Ambiente de Produção

Para simular o ambiente de **Produção**, basta utilizar o arquivo de variáveis específico, garantindo credenciais e isolamento do banco de dados.

Parar Staging:

```bash
docker compose down
```

Iniciar Produção (assumindo que o arquivo `.env.production` foi criado com senhas fortes):

```bash
docker compose --env-file .env.production up -d
```

A API estará acessível em:  
👉 [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## 🔗 Pipeline CI/CD (GitHub Actions)

Utilizamos o **GitHub Actions** para o pipeline de **Integração Contínua (CI)** e **Deployment Contínuo (CD)**.

### 🛠️ Ferramentas Utilizadas

| Categoria | Ferramenta / Tecnologia |
|------------|--------------------------|
| Workflow Engine | GitHub Actions |
| Linguagem | C# / .NET 8.0 |
| Container Registry | GitHub Container Registry (GHCR) |

---

### 🧱 Etapas do Pipeline (`ci-cd.yml`)

#### 1. Build (Integração Contínua)
- **Ação:** Compila o projeto C# e executa `dotnet restore`.
- **Resultado:** Garante que o código é estável e compilável.

#### 2. Testes Automatizados
- **Ação:** Executa todos os testes unitários/integrados (ex: `Fiap.Web.Api.Desperdicio.Tests`).
- **Resultado:** A imagem só é construída e enviada se os testes passarem.

#### 3. Build & Push da Imagem (Artefato)
- **Ação:** Constrói a imagem Docker final e faz push para o GHCR usando `GITHUB_TOKEN`.
- **Resultado:** Imagem final armazenada no GHCR, pronta para deploy.

---

## ⚙️ Funcionamento do Deployment (CD)

O deploy ocorre em dois ambientes: **Staging** e **Produção**.

- **Ferramenta:** Docker Compose  
- **Diferenciação:** Cada ambiente usa seu próprio `.env` e banco isolado  
  (`desperdicio_esg_staging` vs `desperdicio_esg_production`)

---

## 🐳 Containerização

### 🧰 Dockerfile (Estratégias Adotadas)

Utiliza **Multi-Stage Build** para reduzir o tamanho e aumentar a segurança.

| Stage | Imagem base | Descrição |
|--------|--------------|------------|
| Build | `dotnet/sdk:8.0` | Compila o código |
| Publish | — | Gera binários finais |
| Final | `dotnet/aspnet:8.0` | Executa a aplicação |

**Benefício:** Imagem mínima, sem compiladores ou código-fonte.

📄 **Cole aqui o conteúdo do seu `Dockerfile`:**

```dockerfile
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

```

---

### 🧩 docker-compose.yml (Orquestração)

O arquivo orquestra a aplicação e o banco de dados, configurando:

- Rede isolada (comunicação interna por hostname)
- Volumes persistentes (`postgres_data`)
- Mapeamento de portas (`8080:80`)

📄 **Cole aqui o conteúdo do seu `docker-compose.yml`:**

```yaml
version: '3.8'

services:
  api-desperdicio:
    build:
      context: ./
      dockerfile: Dockerfile
    ports:
      - "8080:8080"
    environment:
      - ConnectionStrings__DatabaseConnection=Host=db;Port=5432;Database=esgdb;Username=user;Password=password
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      db:
        condition: service_healthy
    networks:
      - esg-network
    container_name: api-desperdicio-1

  db:
    image: postgres:15-alpine
    restart: always
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: password
      POSTGRES_DB: esgdb
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - esg-network
    container_name: db-1
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U user -d esgdb"]
      interval: 5s
      timeout: 5s
      retries: 5
      start_period: 20s

networks:
  esg-network:
    driver: bridge

volumes:
  postgres_data:

```

---

## 🖼️ Prints do Funcionamento

⚠️ **Substitua as linhas abaixo pelas imagens reais.**

### 📸 Evidências do CI/CD (GitHub Actions)
- Print 1: Log do workflow (Build ✅, Testes ✅, Push ✅).

- <img width="2511" height="1127" alt="image" src="https://github.com/user-attachments/assets/7add16a0-51c1-41d5-87d8-3bd638bc9fe8" />

- 

### 🚀 Evidências do Deploy e Funcionamento
- Print 2: Deploy Staging (`docker compose --env-file .env.staging up -d`) e Swagger ativo.

<img width="1370" height="702" alt="staging" src="https://github.com/user-attachments/assets/d35b98f5-ef4e-4123-b3ed-605e2a5959a7" />

- Print 3: Deploy Produção (`docker compose --env-file .env.production up -d`) e requisição GET em `/alimentos`.
- 
<img width="1357" height="697" alt="production" src="https://github.com/user-attachments/assets/fed09949-c7a9-4b96-ae80-6a47ce625f20" />

- 

---

## 💻 Tecnologias Utilizadas

| Categoria | Tecnologia | Versão |
|------------|-------------|--------|
| Backend | C# / ASP.NET Core | 8.0 |
| Banco de Dados | PostgreSQL | 16-alpine |
| Containerização | Docker, Docker Compose | latest |
| CI/CD | GitHub Actions | — |
| Outros | Npgsql (Driver PostgreSQL) | — |

---

## ✅ Checklist de Entrega (Obrigatório)

| Item | OK |
|------|----|
| Projeto compactado em `.zip` com estrutura organizada | ✅ |
| Dockerfile funcional | ✅ |
| `docker-compose.yml` ou arquivos Kubernetes | ✅ |
| Pipeline com etapas de build, teste e deploy | ✅ |
| `README.md` com instruções e prints | ✅ |
| Documentação técnica com evidências (PDF ou PPT) | ✅ |
| Deploy realizado nos ambientes staging e produção | ✅ |

---

📘 **Autor:** *Adriano de Camargo*  
📅 **Data:** Outubro/2025  
