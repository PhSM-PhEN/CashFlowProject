# 💰 CashFlow - API de Gerenciamento de Despesas

## 📋 Sobre o Projeto

O **CashFlow** é uma API robusta desenvolvida em **.NET 8** que implementa os princípios do **Domain-Driven Design (DDD)** para oferecer uma solução estruturada e escalável no gerenciamento de despesas pessoais.

### 🎯 Principais Objetivos

- Permitir que usuários registrem suas despesas com detalhes completos (título, data/hora, descrição, valor e tipo de pagamento)
- Armazenar dados de forma segura em banco de dados MySQL
- Gerar relatórios detalhados em PDF e Excel para análise financeira
- Fornecer uma API RESTful bem documentada e fácil de integrar

### 🛠️ Stack Tecnológico

A aplicação utiliza tecnologias modernas e consolidadas:

- **AutoMapper**: Mapeamento entre objetos de domínio e DTOs
- **Fluent Validation**: Validação de requisições com regras intuitivas
- **FluentAssertions**: Testes unitários mais legíveis e expressivos
- **Entity Framework Core**: ORM para interações com banco de dados
- **Swagger/OpenAPI**: Documentação interativa dos endpoints
- **MySQL**: Banco de dados relacional para persistência de dados

---

## ✨ Features

- ✅ **Domain-Driven Design (DDD)**: Arquitetura modular e bem organizada
- ✅ **Autenticação & Autorização**: Sistema seguro de login com tokens
- ✅ **CRUD de Despesas**: Criar, ler, atualizar e deletar despesas
- ✅ **Geração de Relatórios**: Exportar dados em PDF e Excel
- ✅ **Testes Abrangentes**: Testes unitários e de integração com FluentAssertions
- ✅ **API RESTful**: Endpoints bem estruturados e documentados
- ✅ **Documentação Swagger**: Interface interativa para testar a API

---

## 🏗️ Construído Com

![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![Windows Badge](https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge)
![Visual Studio Badge](https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge)
![MySQL Badge](https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge)
![Swagger Badge](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge)

---

## 📦 Pré-requisitos

Antes de começar, certifique-se de que você possui:

- **Visual Studio** 2022+ ou **Visual Studio Code**
- **Windows 10+** / **Linux/MacOS** com [.NET SDK 8.0][dot-net-sdk] instalado
- **MySQL Server** 5.7 ou superior
- **Git** instalado

---

## 🚀 Como Começar

### 1️⃣ Clone o Repositório

```bash
git clone https://github.com/PhSM-PhEN/CashFlowProject.git
cd CashFlowProject
```

### 2️⃣ Configure o Banco de Dados

1. Abra `appsettings.Development.json`
2. Preencha a string de conexão do MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=cashflow;User=root;Password=sua_senha;"
}
```

### 3️⃣ Instale as Dependências

```bash
dotnet restore
```

### 4️⃣ Aplique as Migrações

```bash
dotnet ef database update
```

### 5️⃣ Execute a Aplicação

```bash
dotnet run --project src/CashFlow.API/CashFlow.API.csproj
```

A API estará disponível em `https://localhost:7001` (ou a porta configurada)

---

## 📚 Estrutura do Projeto

```
CashFlow/
├── src/
│   ├── CashFlow.API/           # Camada de apresentação (Controllers)
│   ├── CashFlow.Application/   # Casos de uso e lógica de aplicação
│   ├── CashFlow.Domain/        # Entidades e regras de negócio
│   ├── CashFlow.Infrastructure/# Acesso a dados e serviços externos
│   ├── CashFlow.Communication/ # DTOs e objetos de comunicação
│   └── CashFlow.Exception/     # Exceções customizadas
├── tests/
│   ├── UseCase.Tests/          # Testes de casos de uso
│   ├── Validators.Tests/       # Testes de validações
│   ├── WebApi.Test/            # Testes de integração da API
│   └── CommonTestUtilities/    # Utilitários compartilhados
└── README.md
```

---

## 🔑 Endpoints Principais

### 👤 Autenticação
- `POST /api/login` - Realizar login
- `POST /api/users` - Registrar novo usuário

### 💸 Despesas
- `GET /api/expenses` - Listar despesas
- `POST /api/expenses` - Criar nova despesa
- `GET /api/expenses/{id}` - Obter detalhes de uma despesa
- `PUT /api/expenses/{id}` - Atualizar despesa
- `DELETE /api/expenses/{id}` - Deletar despesa

### 📊 Relatórios
- `GET /api/reports/pdf` - Gerar relatório em PDF
- `GET /api/reports/excel` - Gerar relatório em Excel

---

## 🧪 Testes

Para executar todos os testes:

```bash
dotnet test
```

Para executar testes de um projeto específico:

```bash
dotnet test tests/UseCase.Tests/UseCase.Tests.csproj
```

Para executar com cobertura de código:

```bash
dotnet test /p:CollectCoverage=true
```

---

## 📖 Documentação

A documentação interativa da API está disponível via Swagger/OpenAPI:

```
http://localhost:7001/swagger/index.html
```




---

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

## 👨‍💻 Autor

**PhSM-PhEN** - [GitHub](https://github.com/PhSM-PhEN)

---

## 📞 Suporte

Se tiver dúvidas ou encontrar problemas, abra uma [Issue](https://github.com/PhSM-PhEN/CashFlowProject/issues) no repositório.

---

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
