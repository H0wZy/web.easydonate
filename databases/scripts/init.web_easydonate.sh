#!/bin/bash
set -e

# Script para criar múltiplos databases no PostgreSQL

psql -v ON_ERROR_STOP=1 --username "$USER_DB_USER" --dbname "$USER_DB" <<-EOSQL

    -- Criar schemas caso não existam
    CREATE SCHEMA IF NOT EXISTS "user";
    CREATE SCHEMA IF NOT EXISTS "auth";

    -- Garantir permissões
    GRANT ALL PRIVILEGES ON SCHEMA "user" TO "$USER_DB_USER";
    GRANT ALL PRIVILEGES ON SCHEMA "auth" TO "$USER_DB_USER";

    -- Mensagem de sucesso
    SELECT 'Databases e schemas criados com sucesso!' as status;
EOSQL

echo "Inicialização do banco de dados \"$USER_DB\" concluída!"