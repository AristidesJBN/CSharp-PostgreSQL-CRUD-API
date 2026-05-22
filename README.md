# Avaliação B1 de Desenvolvimento Web Back-end

Projeto: EscolaAPI

Professor: Marcelo Varela

Alunos:
- Aristides Jeronimo de Brito Neto
- Klyngher Emidio Bezerra Cabral

## Como rodar a API

1. Abra o terminal na pasta do projeto:
   ```bash
   cd C:\Users\anagi\Documents\Neto\Backend\EscolaAPI
   ```
2. Execute o comando:
   ```bash
   dotnet run
   ```
3. A API será iniciada em `http://localhost:5255` (conforme `launchSettings.json`).

## Como subir o código SQL

Use o script abaixo em seu banco PostgreSQL para criar a tabela e inserir dados de teste:

```sql
CREATE TABLE IF NOT EXISTS "Alunos" (
  "Id" serial PRIMARY KEY,
  "Nome" text NOT NULL,
  "Email" text NOT NULL,
  "Curso" text NOT NULL,
  "DataNascimento" timestamp with time zone NOT NULL
);

INSERT INTO "Alunos" ("Nome", "Email", "Curso", "DataNascimento") VALUES
  ('Ana Silva', 'ana.silva@example.com', 'Matemática', '2004-03-10T00:00:00Z'),
  ('Bruno Costa', 'bruno.costa@example.com', 'Física', '2003-07-22T00:00:00Z'),
  ('Carla Oliveira', 'carla.oliveira@example.com', 'Química', '2005-01-15T00:00:00Z');
```

### Observação

- Certifique-se de ter PostgreSQL rodando.
- Ajuste a conexão em `appsettings.json` caso necessário.
- Depois de rodar o script, faça login em `POST /api/auth/login` e use o token para acessar `GET /api/alunos`.

## Testes (exemplos)

Antes de testar os endpoints protejidos, obtenha um token:

1. Login (gera token):

```bash
curl -X POST http://localhost:5255/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"123456"}'
```

Resposta (exemplo):
```json
{ "token": "<seu_jwt_aqui>" }
```

Use o token nos exemplos abaixo no header `Authorization: Bearer <token>`.

1) GET /api/alunos - Listar todos

```bash
curl -H "Authorization: Bearer <token>" http://localhost:5255/api/alunos
```

2) GET /api/alunos/{id} - Buscar por ID

```bash
curl -H "Authorization: Bearer <token>" http://localhost:5255/api/alunos/1
```

3) POST /api/alunos - Inserir aluno

```bash
curl -X POST http://localhost:5255/api/alunos \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <token>" \
  -d '{"nome":"Novo Aluno","email":"novo@example.com","curso":"Engenharia","dataNascimento":"2006-05-01T00:00:00Z"}'
```

4) PUT /api/alunos/{id} - Atualizar aluno

Envie somente os campos a atualizar (não é obrigatório enviar `id` no body):

```bash
curl -X PUT http://localhost:5255/api/alunos/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <token>" \
  -d '{"nome":"Nome Atualizado","email":"atualizado@example.com","curso":"TI","dataNascimento":"2004-09-30T00:00:00Z"}'
```

5) DELETE /api/alunos/{id} - Remover aluno

```bash
curl -X DELETE http://localhost:5255/api/alunos/1 \
  -H "Authorization: Bearer <token>"
```

Dica: no Insomnia/Postman use a aba `Auth` → `Bearer Token` e cole o token para facilitar os testes.
