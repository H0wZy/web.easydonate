# =====================================
# Estágio Development
# =====================================
FROM node:22 AS development

WORKDIR /app

# Copiar package files e instalar dependências
COPY package*.json ./
RUN npm install

# Comando padrão para iniciar o servidor de desenvolvimento
CMD ["npm", "run", "dev"]


# =====================================
# Estágio 1: Build
# =====================================
FROM node:22 AS build

# Argumentos de build
ARG VITE_API_URL=http://localhost:5000

WORKDIR /app

# Copiar package files e instalar dependências
COPY package*.json ./

# Instala todas as dependências, incluindo dev
RUN npm ci

# Copiar código fonte
COPY . .

# Setar variável de ambiente e build
ENV VITE_API_URL=$VITE_API_URL
RUN npm run build

# =====================================
# Estágio 2: Production
# =====================================
FROM nginx:alpine AS final

# Instalar curl para healthchecks
RUN apk add --no-cache curl

# Copiar build para nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Copiar configuração customizada do nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expor porta 80
EXPOSE 80

# Healthcheck
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost/ || exit 1

CMD ["nginx", "-g", "daemon off;"]