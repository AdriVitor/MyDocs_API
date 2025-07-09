# MyDocs API 

A **MyDocs** é uma API moderna desenvolvida em **C# e ASP.NET Core** com o objetivo de facilitar o **armazenamento seguro de documentos** e a **emissão de alertas de vencimento de boletos**.

Este projeto foi criado para aplicar boas práticas de arquitetura, automação, conteinerização e cloud computing utilizando tecnologias atuais.

---

## 📌 Funcionalidades

- 📄 Armazenamento de documentos no **Azure Blob Storage**
- ⏰ Emissão de alertas para vencimento de boletos usando **Hangfire**
- 🔒 Segurança e autenticação JWT
- 🗄️ Persistência de dados em **Azure SQL Server**
- 🐳 Conteinerização com **Docker** e **Azure Container Registry**
- ☁️ Hospedagem em **Azure App Service**
- 🔄 CI/CD automatizado com **GitHub Actions** e **Azure Container Registry**

---

## 🛠️ Tecnologias Utilizadas

- **C# / ASP.NET Core**
- **xUnit e Moq**
- **Azure SQL Server**
- **Azure Blob Storage**
- **Azure App Service**
- **Docker**
- **Azure Container Registry**
- **Hangfire**
- **GitHub Actions**

---

## ⚙️ CI/CD com GitHub Actions
- O projeto conta com um pipeline automatizado utilizando GitHub Actions, que executa:
- Build da aplicação
- Execução de testes automatizados
- Build da imagem Docker
- Push da imagem para o Azure Container Registry
- Deploy automatizado no Azure App Service

---

## Como Executar Localmente

1. **Clone o repositório:**

   entre no bash
   git clone https://github.com/seu-usuario/MyDocs.git
   cd MyDocs

2. **Ajuste o arquivo appsettings.json com as informações de conexão ao Azure SQL, Azure Blob Storage e demais chaves necessárias.**

3. **Executa a aplicação localmente**

- dotnet build
- dotnet run

4. **Acesse o Swagger no navegador**

---

## Como executar com Docker

1. **Build da imagem Docker**

docker build -t mydocs-api .

2. **Execute o container**

docker run -d -p 5000:80 --name mydocs mydocs-api

3. **Acesse no navegador**