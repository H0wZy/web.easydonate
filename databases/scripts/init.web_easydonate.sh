#!/bin/bash
set -e

# Script para criar múltiplos databases no PostgreSQL

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    -- Criar schemas separados para cada microserviço
    CREATE SCHEMA IF NOT EXISTS "user";
    CREATE SCHEMA IF NOT EXISTS "auth";
    
    -- Garantir permissões
    GRANT ALL PRIVILEGES ON SCHEMA "user" TO $POSTGRES_USER;
    GRANT ALL PRIVILEGES ON SCHEMA "auth" TO $POSTGRES_USER;
    
    -- Mensagem de sucesso
    SELECT 'Databases e schemas criados com sucesso!' as status;
EOSQL

echo "Inicialização do banco de dados concluída!"