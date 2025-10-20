# EasyDonate - Marketplace de Doações

Plataforma que conecta doadores e receptores em um marketplace de economia circular. Qualquer pessoa ou empresa pode doar qualquer item para quem precisa.

## 📋 Sobre o Projeto

EasyDonate é uma solução completa para facilitar doações entre pessoas, empresas e organizações. A plataforma permite que qualquer um doe itens que não usa mais para quem realmente precisa, criando uma economia circular sustentável.

## 🎯 Funcionalidades Principais

### Para Doadores
- Cadastro de doações com fotos
- Escolha de modalidade de entrega
- Gestão de solicitações
- Histórico completo de doações
- Sistema de avaliações

### Para Receptores
- Busca inteligente de doações
- Filtros por localização e categoria
- Solicitação de itens específicos
- Wishlist de necessidades
- Mapa interativo

### Categorias Suportadas
- Alimentos
- Roupas e Calçados
- Móveis e Eletrodomésticos
- Educação (livros, material escolar)
- Construção
- Brinquedos e Infantil
- Saúde (equipamentos)
- Dinheiro (PIX)
- Serviços (trabalho voluntário)
- Outros

## 🏗️ Arquitetura

### Backend - Microserviços
```
.
├── Api.Gateway          # Gateway de entrada
├── User.Api            # Gestão de usuários
├── Auth.Api            # Autenticação JWT
├── Donation.Api        # Gestão de doações (em desenvolvimento)
├── Request.Api         # Solicitações (planejado)
└── Chat.Api            # Mensagens (planejado)
```

### Frontend
```
.
├── src/
│   ├── components/     # Componentes reutilizáveis
│   ├── services/       # Chamadas API
│   ├── models/         # Interfaces TypeScript
│   └── pages/          # Páginas da aplicação
```

## 🛠️ Tecnologias

### Backend
- .NET 9
- PostgreSQL 18
- Entity Framework Core
- JWT Authentication
- Docker & Docker Compose
- AutoMapper
- PBKDF2 (hash de senhas)

### Frontend
- React 19
- TypeScript
- Vite
- Axios
- React Router (planejado)

### DevOps
- Docker
- Docker Compose
- Nginx
- Makefile (comandos úteis)

## 📦 Pré-requisitos

- Docker 20.10+
- Docker Compose 2.0+
- .NET SDK 9.0 (para desenvolvimento local)
- Node.js 22+ (para desenvolvimento local)
- Make (opcional, para usar comandos do Makefile)

## 🚀 Instalação e Execução

### 1. Clone o repositório
```bash
git clone https://github.com/H0wZy/web.easydonate.git
cd web.easydonate
```

### 2. Inicie a aplicação

**Usando Make (recomendado):**
```bash
make up
```

**Usando Docker Compose diretamente:**
```bash
docker compose up -d
```

### 3. Acesse a aplicação
- Frontend: http://localhost:3000
- Gateway API: http://localhost:5000
- User API: http://localhost:5290

## 📝 Comandos Úteis

Se você tem Make instalado:

```bash
make help          # Lista todos os comandos disponíveis
make build         # Build das imagens Docker
make up            # Inicia todos os containers
make down          # Para todos os containers
make restart       # Reinicia todos os containers
make logs          # Mostra logs de todos os serviços
make logs-frontend # Logs apenas do frontend
make logs-user     # Logs apenas do User API
make clean         # Remove containers, volumes e imagens
make ps            # Lista status dos containers
make health        # Verifica health dos serviços
make dev           # Modo desenvolvimento (logs visíveis)
```

Sem Make, use docker compose diretamente:
```bash
docker compose up -d
docker compose down
docker compose logs -f
docker compose ps
```

## 🗃️ Estrutura do Banco de Dados

### User Database
- `user` - Dados principais do usuário
- `donor` - Informações específicas de doadores
- `organization` - Informações de ONGs

### Donation Database (em desenvolvimento)
- `donation` - Dados das doações
- `donation_image` - Imagens das doações
- `donation_category` - Categorias

## 🔐 Autenticação

A API utiliza autenticação JWT. Para acessar endpoints protegidos:

1. Faça login através do endpoint `/api/Auth/login`
2. Receba o token na resposta
3. Inclua o token no header das requisições:
```
Authorization: Bearer {seu_token_aqui}
```

### Características do Token
- Expiração: 24 horas
- Algoritmo: HMAC SHA256
- Claims: IdUsuario, Email, UserType

## 📖 API Endpoints

### User API
```
GET    /api/User/GetAllUsers
GET    /api/User/GetUserById/{id}
GET    /api/User/GetUserByUsername/{username}
GET    /api/User/GetUserByEmail/{email}
POST   /api/User/CreateUser
PATCH  /api/User/UpdateUserById/{id}
DELETE /api/User/DeleteUserById/{id}
PATCH  /api/User/DisableUserById/{id}
```

### Auth API (em desenvolvimento)
```
POST   /api/Auth/login
POST   /api/Auth/refresh
POST   /api/Auth/logout
```

## 🧪 Desenvolvimento Local

### Backend (.NET)
```bash
cd backend/src/User.Api
dotnet restore
dotnet run
```

### Frontend (React)
```bash
cd frontend
npm install
npm run dev
```

## 🔒 Segurança

- Senhas criptografadas com PBKDF2 (10.000 iterações)
- Tokens JWT com expiração
- Validação dupla (cliente e servidor)
- CORS configurado
- HTTPS obrigatório em produção
- Rate limiting (planejado)

## 🎨 Padrões de Código

### Backend
- Clean Architecture
- Repository Pattern
- Dependency Injection
- DTOs para transferência de dados
- AutoMapper para mapeamento
- Response Models padronizados

### Frontend
- Componentes funcionais
- TypeScript strict mode
- Axios para chamadas HTTP
- Services pattern
- Interfaces para tipagem

## 🚧 Roadmap

### MVP - Fase 1 (Atual)
- [x] User.Api funcionando
- [x] Estrutura do projeto
- [x] Docker compose configurado
- [ ] Auth.Api completo
- [ ] Donation.Api básico
- [ ] Frontend: Login/Cadastro
- [ ] Frontend: Criar/Listar doações

### Fase 2
- [ ] Request.Api
- [ ] Sistema de solicitações
- [ ] Mapa básico
- [ ] Chat ou integração WhatsApp

### Fase 3
- [ ] Sistema de avaliações
- [ ] Wishlist
- [ ] Notificações
- [ ] Dashboard analytics

### Fase 4
- [ ] App mobile
- [ ] Planos premium
- [ ] Match automático IA
- [ ] API pública

## 🤝 Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT.

## 👥 Autores

- [Marcos Junior](https://github.com/H0wZy)
- [Guilherme Rodrigues](https://github.com/guilherme-rodrigues-de-queiroz)

## 📞 Contato

Para dúvidas ou sugestões, abra uma issue no GitHub.

---

**EasyDonate** - Conectando quem pode doar com quem precisa receber 💚