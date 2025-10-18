.PHONY: help build up down restart logs clean ps health migrate shell-frontend shell-gateway shell-user shell-auth shell-db rebuild dev

# Cores
GREEN  := \033[0;32m
YELLOW := \033[0;33m
NC     := \033[0m

# Carrega variáveis do .env
include .env
export

help: ## Mostra esta mensagem de ajuda
	@echo "$(GREEN)Comandos disponíveis:$(NC)"
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "  $(YELLOW)%-15s$(NC) %s\n", $$1, $$2}'

build: ## Build de todas as imagens Docker
	@echo "$(GREEN)Building imagens Docker...$(NC)"
	docker compose build

up: ## Sobe todos os containers
	@echo "$(GREEN)Iniciando containers...$(NC)"
	docker compose up -d
	@echo "$(GREEN)Containers iniciados! Acesse:$(NC)"
	@echo "  Frontend:    http://localhost:${FRONTEND_PORT}"
	@echo "  Gateway:     http://localhost:${GATEWAY_API_PORT}"
	@echo "  User API:    http://localhost:${USER_API_PORT}"
	@echo "  Auth API:    http://localhost:${AUTH_API_PORT}"
	@echo "  PostgreSQL:  localhost:${POSTGRES_PORT}"

down: ## Para todos os containers
	@echo "$(YELLOW)Parando containers...$(NC)"
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
	docker compose logs -f postgres

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
	docker compose exec postgres psql -U ${POSTGRES_USER} -d ${POSTGRES_DB}

rebuild: ## Rebuild completo (limpa + build + up)
	@echo "$(YELLOW)Iniciando rebuild completo...$(NC)"
	@$(MAKE) clean
	@$(MAKE) build
	@$(MAKE) up
	@echo "$(GREEN)Rebuild concluído!$(NC)"

dev: ## Modo desenvolvimento (logs visíveis)
	docker compose up
