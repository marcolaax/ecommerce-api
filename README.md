# Ecommerce API

Sistema de gerenciamento de produtos e pedidos feito em .NET 10 com MongoDB.

## O que precisa ter instalado

- .NET 10 SDK
- MongoDB rodando na porta 27017

## Como rodar

```bash
cd EcommerceApi
dotnet run
```

Depois acessa o Swagger em `http://localhost:5124/swagger`

## Variáveis de ambiente

| Variável | Exemplo |
|---|---|
| `MONGO_CONNECTION` | `mongodb://localhost:27017` |
| `Jwt__Secret` | `minha-chave-secreta` |

Se não definir, usa os valores do `appsettings.json`.

## Frontend

Abre o arquivo `frontend/index.html` direto no navegador com a API rodando.
