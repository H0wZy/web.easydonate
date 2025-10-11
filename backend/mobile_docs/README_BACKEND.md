# API EasyDonate
API RESTful desenvolvida em .NET 8.0 para gerenciamento de doações, conectando doadores a ONGs de forma eficiente e segura.

## Sobre
A API EasyDonate é o backend de uma plataforma de doações que facilita a conexão entre pessoas que desejam doar e organizações que necessitam de ajuda. Com foco em segurança e performance, oferece endpoints completos para gestão de usuários, doações e agendamentos.

## Funcionalidades

### Gestão de Usuários
- Cadastro diferenciado para Doadores e ONGs
- Autenticação JWT com tokens seguros
- Atualização de perfil e upload de avatares
- Validação de CPF/CNPJ

### Sistema de Doações
- Registro de doações com múltiplos tipos (Alimentos, Roupas, Dinheiro, Outros)
- Agendamento de coletas com data e horário
- Acompanhamento de status (Pendente, Em andamento, Concluído)
- Histórico completo de doações

### Gestão de ONGs
- Cadastro com validação de dados
- Upload de logo institucional
- Atualização de informações e contatos
- Listagem com filtros

### Localização
- Cadastro de coordenadas geográficas
- Integração com mapas
- Busca por proximidade

## Tecnologias
- .NET 8.0 - Framework principal
- C# - Linguagem de programação
- MySQL - Banco de dados relacional
- Dapper - Micro ORM para acesso a dados
- JWT - Autenticação baseada em tokens
- BCrypt.Net - Criptografia de senhas
- Swagger - Documentação interativa da API
- Newtonsoft.Json - Serialização JSON

## Pré-requisitos
- .NET SDK 8.0 ou superior
- MySQL 8.0 ou superior
- Visual Studio 2022 ou VS Code
- Git

## Instalação
1. Clone o repositório:
```bash
git clone https://github.com/4techdevelopments/easydonate_api.git
```
```bash
cd api-easydonate
```

2. Restaure os pacotes NuGet:
```bash
dotnet restore
```

3. Configure o banco de dados MySQL executando o script:
```sql
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema easydonate
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema easydonate
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `easydonate` DEFAULT CHARACTER SET utf8mb3 ;
USE `easydonate` ;

-- -----------------------------------------------------
-- Table `easydonate`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`usuario` (
  `IdUsuario` INT NOT NULL AUTO_INCREMENT,
  `Email` VARCHAR(255) NOT NULL,
  `Senha` VARCHAR(60) NOT NULL,
  `TipoUsuario` VARCHAR(6) NOT NULL,
  `DataCriacao` DATETIME NOT NULL,
  `DataAlteracao` DATETIME NULL DEFAULT NULL,
  `Avatar` VARCHAR(255) NULL DEFAULT NULL,
  `DataConsentimentoTermos` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`IdUsuario`),
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `easydonate`.`doador`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`doador` (
  `IdDoador` INT NOT NULL AUTO_INCREMENT,
  `IdUsuario` INT NOT NULL,
  `TipoPessoa` VARCHAR(7) NOT NULL,
  `Nome` VARCHAR(255) NOT NULL,
  `NomeSocial` VARCHAR(255) NULL DEFAULT NULL,
  `Cpf` VARCHAR(11) NULL DEFAULT NULL,
  `Cnpj` VARCHAR(14) NULL DEFAULT NULL,
  `DataNascimento` DATE NOT NULL,
  `TipoDoacao` VARCHAR(255) NULL DEFAULT NULL,
  `Cep` VARCHAR(8) NOT NULL,
  `Rua` VARCHAR(100) NOT NULL,
  `Numero` VARCHAR(10) NULL DEFAULT NULL,
  `Complemento` VARCHAR(100) NULL DEFAULT NULL,
  `Bairro` VARCHAR(100) NOT NULL,
  `Cidade` VARCHAR(100) NOT NULL,
  `Estado` VARCHAR(2) NOT NULL,
  `Ddd` VARCHAR(2) NOT NULL,
  `Telefone` VARCHAR(8) NULL DEFAULT NULL,
  `TelefoneCelular` VARCHAR(9) NULL DEFAULT NULL,
  PRIMARY KEY (`IdDoador`),
  INDEX `fk_Doador_Usuario1_idx` (`IdUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Doador_Usuario1`
    FOREIGN KEY (`IdUsuario`)
    REFERENCES `easydonate`.`usuario` (`IdUsuario`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `easydonate`.`ong`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`ong` (
  `IdOng` INT NOT NULL AUTO_INCREMENT,
  `IdUsuario` INT NOT NULL,
  `Nome` VARCHAR(255) NOT NULL,
  `Cnpj` VARCHAR(14) NOT NULL,
  `TipoAtividade` VARCHAR(255) NOT NULL,
  `DescricaoMissao` VARCHAR(1000) NULL DEFAULT NULL,
  `Cep` VARCHAR(8) NOT NULL,
  `Rua` VARCHAR(100) NOT NULL,
  `Numero` VARCHAR(10) NULL DEFAULT NULL,
  `Complemento` VARCHAR(100) NULL DEFAULT NULL,
  `Bairro` VARCHAR(100) NOT NULL,
  `Cidade` VARCHAR(100) NOT NULL,
  `Estado` VARCHAR(2) NOT NULL,
  `Ddd` VARCHAR(2) NOT NULL,
  `Telefone` VARCHAR(8) NULL DEFAULT NULL,
  `TelefoneCelular` VARCHAR(9) NULL DEFAULT NULL,
  `RedeSocial` VARCHAR(255) NULL DEFAULT NULL,
  `Site` VARCHAR(255) NULL DEFAULT NULL,
  `ResponsavelCadastro` VARCHAR(255) NOT NULL,
  `ComprovanteRegistro` VARCHAR(255) NULL DEFAULT NULL,
  `Logo` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`IdOng`),
  UNIQUE INDEX `Cnpj_UNIQUE` (`Cnpj` ASC) VISIBLE,
  INDEX `fk_Ong_Usuario_idx` (`IdUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Ong_Usuario`
    FOREIGN KEY (`IdUsuario`)
    REFERENCES `easydonate`.`usuario` (`IdUsuario`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `easydonate`.`doacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`doacao` (
  `IdDoacao` INT NOT NULL AUTO_INCREMENT,
  `IdDoador` INT NOT NULL,
  `IdOng` INT NOT NULL,
  `TipoItem` VARCHAR(255) NOT NULL,
  `Quantidade` DECIMAL(10,2) NOT NULL,
  `Descricao` VARCHAR(255) NULL DEFAULT NULL,
  `DataHoraDoacao` DATETIME NOT NULL,
  PRIMARY KEY (`IdDoacao`),
  INDEX `fk_Doacao_Doador1_idx` (`IdDoador` ASC) VISIBLE,
  INDEX `fk_Doacao_Ong1_idx` (`IdOng` ASC) VISIBLE,
  CONSTRAINT `fk_Doacao_Doador1`
    FOREIGN KEY (`IdDoador`)
    REFERENCES `easydonate`.`doador` (`IdDoador`),
  CONSTRAINT `fk_Doacao_Ong1`
    FOREIGN KEY (`IdOng`)
    REFERENCES `easydonate`.`ong` (`IdOng`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `easydonate`.`agendamento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`agendamento` (
  `IdAgendamento` INT NOT NULL AUTO_INCREMENT,
  `IdDoacao` INT NOT NULL,
  `Status` VARCHAR(50) NOT NULL,
  `DataColeta` DATE NULL DEFAULT NULL,
  `HoraColeta` VARCHAR(5) NULL DEFAULT NULL,
  PRIMARY KEY (`IdAgendamento`),
  INDEX `fk_Agendamento_Doacao1_idx` (`IdDoacao` ASC) VISIBLE,
  CONSTRAINT `fk_Agendamento_Doacao1`
    FOREIGN KEY (`IdDoacao`)
    REFERENCES `easydonate`.`doacao` (`IdDoacao`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `easydonate`.`localizacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `easydonate`.`localizacao` (
  `IdLocalizacao` INT NOT NULL AUTO_INCREMENT,
  `IdOng` INT NOT NULL,
  `Latitude` VARCHAR(30) NULL DEFAULT NULL,
  `Longitude` VARCHAR(30) NULL DEFAULT NULL,
  PRIMARY KEY (`IdLocalizacao`),
  INDEX `fk_Localizacao_Ong1_idx` (`IdOng` ASC) VISIBLE,
  CONSTRAINT `fk_Localizacao_Ong1`
    FOREIGN KEY (`IdOng`)
    REFERENCES `easydonate`.`ong` (`IdOng`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
```

## Configuração
1. String de Conexão
- Edite o arquivo appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EasyDonate;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

2. Configuração JWT
- A chave JWT é gerada automaticamente no Program.cs. Para produção, considere usar uma chave fixa armazenada com segurança.

3. Executar a API
```bash
dotnet run
```
- A API estará disponível em:
```http
HTTP: http://localhost:5062
HTTPS: https://localhost:7058
Swagger: http://localhost:5062/swagger
```

## Estrutura do Projeto
```bash
Api.EasyDonate/
├── Controllers/           # Controladores REST
│   ├── AgendamentoController.cs
│   ├── DoacaoController.cs
│   ├── DoadorController.cs
│   ├── LocalizacaoController.cs
│   ├── LoginController.cs
│   ├── OngController.cs
│   ├── UploadController.cs
│   └── UsuarioController.cs
├── Repositorio/          # Camada de acesso a dados
│   ├── AgendamentoRepositorio.cs
│   ├── DoacaoRepositorio.cs
│   ├── DoadorRepositorio.cs
│   ├── LocalizacaoRepositorio.cs
│   ├── OngRepositorio.cs
│   └── UsuarioRepositorio.cs
├── Interface/            # Interfaces e contratos
│   └── ICrud.cs
├── Persistencia/         # Configuração de banco
│   └── ConexaoDB.cs
└── Program.cs           # Configuração principal

Api.Comum/               # Biblioteca compartilhada
├── Modelos/            # Classes de domínio
├── DTOs/               # Data Transfer Objects
└── JwtToken.cs         # Geração de tokens
```

## Endpoints
- Autenticação
- Login
```http
POST /api/Login
Content-Type: application/json
```

```json
{
  "email": "usuario@email.com",
  "senha": "Senha@123"
}
```

Resposta:

```json
{
  "token": "eyJhbGc...",
  "usuario": {
    "idUsuario": 1,
    "email": "usuario@email.com",
    "tipoUsuario": "Doador",
    "nome": "João",
    "avatar": "https://...",
    "doador": { ... }
  }
}
```

## Usuários
- Criar Usuário
```http
POST /api/Usuario
```

- Buscar Usuário
```http
GET /api/Usuario/{id}
Authorization: Bearer {token}
```

- Atualizar Usuário
```http
PUT /api/Usuario/{id}
Authorization: Bearer {token}
```

## Doadores
- Cadastrar Doador
```http
POST /api/Doador
Content-Type: application/json
```
```json
{
  "email": "doador@email.com",
  "senha": "Senha@123",
  "tipoUsuario": "Doador",
  "tipoPessoa": "PF",
  "nome": "João Silva",
  "cpf": "12345678901",
  "dataNascimento": "1990-01-01",
  "cep": "12345678",
  "rua": "Rua Exemplo",
  "numero": "123",
  "bairro": "Centro",
  "cidade": "Londrina",
  "estado": "PR",
  "ddd": "43",
  "telefoneCelular": "999999999"
}
```

## ONGs
- Cadastrar ONG
```http
POST /api/Ong
Content-Type: application/json
```

```json
{
  "email": "ong@email.com",
  "senha": "Senha@123",
  "tipoUsuario": "Ong",
  "nome": "ONG Exemplo",
  "cnpj": "12345678000199",
  "tipoAtividade": "Alimentos",
  "descricaoMissao": "Ajudamos famílias...",
  "cep": "12345678",
  "rua": "Rua da ONG",
  "numero": "456",
  "bairro": "Centro",
  "cidade": "Londrina",
  "estado": "PR",
  "ddd": "43",
  "telefone": "33334444",
  "responsavelCadastro": "Maria Santos"
}
```

- Listar ONGs
```http
GET /api/Ong
Authorization: Bearer {token}
```

## Doações
- Criar Doação
```http
POST /api/Doacao
Authorization: Bearer {token}
Content-Type: application/json
```

```json
{
  "idDoador": 1,
  "idOng": 2,
  "tipoItem": "Alimentos",
  "quantidade": 10.5,
  "descricao": "10kg de arroz",
  "status": "Pendente",
  "dataColeta": "2025-07-01",
  "horaColeta": "14:00"
}
```

- Listar Doações
```http
GET /api/Doacao
Authorization: Bearer {token}
```

## Agendamentos
- Atualizar Status
```http
PUT /api/Agendamento/{idDoacao}
Authorization: Bearer {token}
Content-Type: application/json
```

```json
{
  "status": "Concluido"
}
```

## Upload
- Upload Avatar
```http
PUT /api/Upload/Avatar/{idUsuario}
Authorization: Bearer {token}
Content-Type: application/json
```
```json
{
  "idUsuario": 1,
  "avatar": "https://url-da-imagem.com"
}
```

- Upload Logo ONG
```http
PUT /api/Upload/Logo/{idOng}
Authorization: Bearer {token}
Content-Type: application/json
```

```json
{
  "idOng": 1,
  "logo": "https://url-da-logo.com"
}
```

## Autenticação
- A API utiliza autenticação JWT (JSON Web Tokens). Para acessar endpoints protegidos:

- Faça login através do endpoint /api/Login
- Receba o token na resposta
- Inclua o token no header das requisições:
- Authorization: Bearer {seu_token_aqui}
- Características do Token:
- Expiração: 1 hora
- Algoritmo: HMAC SHA256
- Claims: IdUsuario, Email, TipoUsuario

## Modelos de Dados
- Usuario
```csharp
{
  IdUsuario: int,
  Email: string,
  Senha: string,
  TipoUsuario: string, // "Doador", "Ong", "ADM"
  DataCriacao: DateTime,
  DataAlteracao: DateTime,
  Avatar: string
}
````

- Doacao
```csharp
{
  IdDoacao: int,
  IdDoador: int,
  IdOng: int,
  TipoItem: string, // "Alimentos", "Roupas", "Dinheiro", "Outros"
  Quantidade: decimal,
  Descricao: string,
  DataHoraDoacao: DateTime
}
```

- Agendamento
```csharp
{
  IdAgendamento: int,
  IdDoacao: int,
  Status: string, // "Pendente", "Andamento", "Concluido", "Cancelado"
  DataColeta: DateTime,
  HoraColeta: string
}
```

## Segurança
- Senhas: Criptografadas com BCrypt (salt rounds: 10)
- SQL Injection: Prevenido através de prepared statements (Dapper)
- Validação: Dupla validação (cliente e servidor)
- HTTPS: Obrigatório em produção

## Autores
- Guilherme Rodrigues de Queiroz - Desenvolvimento Back-End
- Marcos Junior Bueno Selzer - Desenvolvimento Front-End

## Suporte
- Para suporte, envie um email para fourtech.easydonate@gmail.com ou abra uma issue no GitHub.

### ⭐ Se este projeto te ajudou, considere dar uma estrela!


