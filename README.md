# DevTrack

Aplicação web desenvolvida em **ASP.NET Core MVC** para visualizar e acompanhar atividades de desenvolvedores no GitHub.

Este projeto foi criado com o objetivo de praticar:

- ASP.NET Core
- C#
- Consumo de API (GitHub API)
- Estrutura MVC
- Docker
- Deploy com Render

---

## Funcionalidades

O sistema oferece as seguintes funcionalidades:

1. **Buscar Usuário GitHub**  
   Permite consultar informações públicas de um usuário.

2. **Exibir Perfil**  
   Mostra dados como nome, bio, seguidores e repositórios.

3. **Dashboard de Atividades**  
   Lista eventos recentes (commits, forks, etc).

4. **Dark Mode**  
   Alternância entre tema claro e escuro.

---

## Estrutura do Projeto

```
DevTrack
│
├── DevTrack
│   ├── Controllers
│   ├── Models
│   ├── Services
│   ├── Views
│   ├── wwwroot
│   ├── Program.cs
│   └── appsettings.json
│
├── Dockerfile
├── .dockerignore
├── .gitignore
├── DevTrack.slnx
└── README.md
```

---

## Requisitos

- .NET 8 SDK
- Visual Studio ou VS Code
- Git (opcional)
- Docker

---

## Como Executar

Clone o repositório:

```bash
git clone https://github.com/GuilhermeAlvesVR/DevTrack.git
```

Entre na pasta do projeto:

```bash
cd DevTrack
```

Execute o projeto:

```bash
dotnet run --project DevTrack
```

---

## Como Executar com Docker

Build da imagem:

```bash
docker build -t devtrack .
```

Execute o container:

```bash
docker run -p 8080:80 devtrack
```

---

## Deploy

O projeto está disponível online em:

https://devtrack-wgo1.onrender.com

---

## Tecnologias Utilizadas

- C#
- ASP.NET Core MVC
- HTML / CSS / JavaScript
- GitHub API
- Docker
- Render

---

## Autor

Guilherme Alves  

GitHub:  
https://github.com/GuilhermeAlvesVR
