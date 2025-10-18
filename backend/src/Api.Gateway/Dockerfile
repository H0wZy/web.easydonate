# =====================================
# Estágio 1: Build
# =====================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["Api.Gateway/Api.Gateway.csproj", "Api.Gateway/"]
RUN dotnet restore "Api.Gateway/Api.Gateway.csproj"

# Copiar todo o código e compilar
COPY . .
WORKDIR "/src/Api.Gateway"
RUN dotnet build "Api.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# =====================================
# Estágio 2: Publish
# =====================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.Gateway.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# =====================================
# Estágio 3: Runtime
# =====================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Instalar curl para healthchecks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copiar artefatos publicados
COPY --from=publish /app/publish .

# Criar usuário não-root para segurança
RUN useradd -m -u 1001 appuser && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "Api.Gateway.dll"]