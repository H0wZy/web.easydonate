# Define o shell padrão como bash (essencial para MINGW64)
SHELL := /bin/bash
.PHONY: help build up down restart logs clean ps health migrate shell-frontend shell-gateway shell-user shell-auth shell-db rebuild dev rebuild-service stop-service restart-service logs-service

# Cores
ESC    := $(shell echo -e "\033")
GREEN  := $(ESC)[0;32m
YELLOW := $(ESC)[0;33m
BLUE   := $(ESC)[0;34m
NC     := $(ESC)[0m

# Carrega variáveis do .env
include .env
export

help: ## Mostra esta mensagem de ajuda
	@echo "$(GREEN)Comandos disponíveis:$(NC)"
	@grep -E -h '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "  $(YELLOW)%-20s$(NC) %s\n", $$1, $$2}'
	@echo ""
	@echo "$(BLUE)Comandos com serviços específicos:$(NC)"
	@echo "  $(YELLOW)rebuild-service$(NC)  SERVICE=nome  - Rebuild de um serviço específico"
	@echo "  $(YELLOW)stop-service$(NC)     SERVICE=nome  - Para um serviço específico"
	@echo "  $(YELLOW)restart-service$(NC)  SERVICE=nome  - Reinicia um serviço específico"
	@echo "  $(YELLOW)logs-service$(NC)     SERVICE=nome  - Logs de um serviço específico"
	@echo ""
	@echo "$(BLUE)Exemplos:$(NC)"
	@echo "  make rebuild-service SERVICE=frontend"
	@echo "  make restart-service SERVICE=user-api"
	@echo "  make logs-service SERVICE=frontend"

build: ## Build de todas as imagens Docker
	@echo "$(GREEN)Building imagens Docker...$(NC)"
	docker compose build

up: ## Sobe todos os containers
	@echo "$(GREEN)Iniciando containers...$(NC)"
	docker compose up -d
	@echo "$(GREEN)Containers iniciados! Acesse:$(NC)"
	@echo "  Frontend:    http://localhost:${FRONTEND_PORT}"
	@echo "  Gateway:     http://localhost:${GATEWAY_API_PORT}"
	@echo "  User API:    http://localhost:${USER_API_PORT}/scalar"
	@echo "  Auth API:    http://localhost:${AUTH_API_PORT}/scalar"
	@echo "  PostgreSQL:  localhost:${USER_DB_PORT}"

stop: ## Para todos os containers
	@echo "$(YELLOW)Parando containers...$(NC)"
	docker compose stop

down: ## Para todos os containers
	@echo "$(YELLOW)Derrubando containers...$(NC)"
	docker compose down

restart: down up ## Reinicia todos os containers

logs: ## Mostra logs de todos os containers
	docker compose logs -f

logs-frontend: ## Logs apenas do frontend
	docker compose logs -f frontend

logs-gateway: ## Logs apenas do gateway
	docker compose logs -f api-gateway

logs-user: ## Logs apenas do User API
	docker compose logs -f user-api

logs-auth: ## Logs apenas do Auth API
	docker compose logs -f auth-api

logs-db: ## Logs apenas do PostgreSQL
	docker compose logs -f user-db

clean: ## Remove containers, volumes e imagens
	@echo "$(YELLOW)Limpando ambiente Docker...$(NC)"
	docker compose down -v --rmi all
	@echo "$(GREEN)Limpeza concluída!$(NC)"

ps: ## Lista status dos containers
	docker compose ps

health: ## Verifica health dos serviços
	@echo "$(GREEN)Status dos serviços:$(NC)"
	docker compose ps --format "table {{.Name}}\t{{.Status}}\t{{.Ports}}"

migrate: ## Executa migrations do banco de dados
	@echo "$(GREEN)Executando migrations...$(NC)"
	docker compose exec user-api dotnet ef database update
	@echo "$(GREEN)Migrations concluídas!$(NC)"

shell-frontend: ## Acessa shell do container frontend
	docker compose exec frontend sh

shell-gateway: ## Acessa shell do container gateway
	docker compose exec api-gateway sh

shell-user: ## Acessa shell do container user-api
	docker compose exec user-api sh

shell-auth: ## Acessa shell do container auth-api
	docker compose exec auth-api sh

shell-db: ## Acessa shell do PostgreSQL
	docker compose exec user-db psql -U ${USER_DB_USER} -d ${USER_DB}

rebuild: ## Rebuild completo (limpa + build + up)
	@echo "$(YELLOW)Iniciando rebuild completo...$(NC)"
	@$(MAKE) clean
	@$(MAKE) build
	@$(MAKE) up
	@echo "$(GREEN)Rebuild concluído!$(NC)"

dev: ## Modo desenvolvimento (logs visíveis)
	docker compose up

# ============================================
# Comandos para serviços específicos
# ============================================

rebuild-service: ## Rebuild de um serviço específico (ex: make rebuild-service SERVICE=frontend)
ifndef SERVICE
	@echo "$(YELLOW)Erro: Especifique o serviço com SERVICE=nome$(NC)"
	@echo "Exemplos:"
	@echo "  make rebuild-service SERVICE=frontend"
	@echo "  make rebuild-service SERVICE=user-api"
	@echo "  make rebuild-service SERVICE=auth-api"
	@echo "  make rebuild-service SERVICE=api-gateway"
else
	@echo "$(YELLOW)Parando $(SERVICE)...$(NC)"
	docker compose stop $(SERVICE)
	@echo "$(YELLOW)Removendo container $(SERVICE)...$(NC)"
	docker compose rm -f $(SERVICE)
	@echo "$(GREEN)Rebuilding $(SERVICE)...$(NC)"
	docker compose build $(SERVICE)
	@echo "$(GREEN)Iniciando $(SERVICE)...$(NC)"
	docker compose up -d $(SERVICE)
	@echo "$(GREEN)$(SERVICE) recriado com sucesso!$(NC)"
endif

stop-service: ## Para um serviço específico (ex: make stop-service SERVICE=frontend)
ifndef SERVICE
	@echo "$(YELLOW)Erro: Especifique o serviço com SERVICE=nome$(NC)"
else
	@echo "$(YELLOW)Parando $(SERVICE)...$(NC)"
	docker compose stop $(SERVICE)
	@echo "$(GREEN)$(SERVICE) parado!$(NC)"
endif

restart-service: ## Reinicia um serviço específico (ex: make restart-service SERVICE=frontend)
ifndef SERVICE
	@echo "$(YELLOW)Erro: Especifique o serviço com SERVICE=nome$(NC)"
else
	@echo "$(YELLOW)Reiniciando $(SERVICE)...$(NC)"
	docker compose restart $(SERVICE)
	@echo "$(GREEN)$(SERVICE) reiniciado!$(NC)"
endif

logs-service: ## Logs de um serviço específico (ex: make logs-service SERVICE=frontend)
ifndef SERVICE
	@echo "$(YELLOW)Erro: Especifique o serviço com SERVICE=nome$(NC)"
else
	docker compose logs -f $(SERVICE)
endif

# Atalhos para serviços comuns
rebuild-frontend: ## Rebuild apenas do frontend
	@$(MAKE) rebuild-service SERVICE=frontend

rebuild-gateway: ## Rebuild apenas do gateway
	@$(MAKE) rebuild-service SERVICE=api-gateway

rebuild-user: ## Rebuild apenas do user-api
	@$(MAKE) rebuild-service SERVICE=user-api

rebuild-auth: ## Rebuild apenas do auth-api
	@$(MAKE) rebuild-service SERVICE=auth-api

rebuild-db: ## Rebuild apenas do banco de dados
	@$(MAKE) rebuild-service SERVICE=user-db
