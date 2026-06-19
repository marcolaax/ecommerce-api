# 🛹 Skate Store — API + Frontend

Sistema de e-commerce para uma loja de skate, com gerenciamento de produtos e pedidos, autenticação JWT e interface web assíncrona.

---

## Domínio

A aplicação simula uma loja de skate com duas entidades principais:

- **Produtos** — shapes, roupas, tênis, protetores e acessórios, com categorias, estoque e disponibilidade.
- **Pedidos** — registro de compras dos clientes, com status (pendente, concluído, cancelado).

Também há autenticação de usuários via JWT para proteger as operações administrativas.

---

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) rodando na porta `27017`

---

## Como executar localmente

```bash
# 1. Entre na pasta do projeto
cd EcommerceApi

# 2. Rode a aplicação
dotnet run
```

A API estará disponível em `http://localhost:5124`.  
O frontend abre automaticamente em `http://localhost:5124/`.

---

## Documentação Swagger

Com a aplicação rodando, acesse:

```
http://localhost:5124/swagger
```

Todos os endpoints estão documentados com parâmetros, corpo de requisição e exemplos de resposta.

---

## Endpoints principais

### Auth
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/registro` | Cria um novo usuário |
| POST | `/api/auth/login` | Retorna o token JWT |

### Produtos
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/api/produtos` | — | Lista todos os produtos |
| GET | `/api/produtos/{id}` | — | Busca produto por ID |
| GET | `/api/produtos/disponiveis` | — | Lista produtos disponíveis |
| GET | `/api/produtos/categoria/{cat}` | — | Filtra por categoria |
| POST | `/api/produtos` |  | Cria novo produto |
| PUT | `/api/produtos/{id}` |  | Atualiza produto completo |
| PATCH | `/api/produtos/{id}/disponibilidade` |  | Atualiza disponibilidade |
| DELETE | `/api/produtos/{id}` |  | Remove produto |

### Pedidos
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/api/pedidos` |  | Lista todos os pedidos |
| GET | `/api/pedidos/{id}` |  | Busca pedido por ID |
| POST | `/api/pedidos` |  | Cria novo pedido |
| PUT | `/api/pedidos/{id}` |  | Atualiza pedido completo |
| PATCH | `/api/pedidos/{id}/status` |  | Atualiza status do pedido |
| DELETE | `/api/pedidos/{id}` |  | Remove pedido |

---

## Variáveis de ambiente

Você pode configurar via variáveis de ambiente ou diretamente no `appsettings.json`:

| Variável | Exemplo | Descrição |
|----------|---------|-----------|
| `MONGO_CONNECTION` | `mongodb://localhost:27017` | String de conexão do MongoDB |
| `Jwt__Secret` | `admin12@356` | Chave de assinatura do JWT |
| `Jwt__Issuer` | `SkateStoreApi` | Emissor do token |
| `Jwt__Audience` | `SkateStoreApp` | Audiência do token |

---

## Frontend

O frontend é servido automaticamente pela própria API em `http://localhost:5124/`.

Funcionalidades:
- Catálogo de produtos com filtros por categoria
- Detalhe de produto
- Cadastro, edição e exclusão de produtos
- Gestão de pedidos com troca de status
- Login e registro de usuário
- Toda navegação é **assíncrona** via `fetch()` — sem recarregamento de página

---

## Estrutura do projeto

```
EcommerceApi/
├── Controllers/
│   ├── AuthController.cs
│   ├── ProdutosController.cs
│   └── PedidosController.cs
├── Models/
│   ├── Produto.cs
│   ├── Pedido.cs
│   └── Usuario.cs
├── wwwroot/
│   └── index.html
├── Program.cs
└── appsettings.json
```