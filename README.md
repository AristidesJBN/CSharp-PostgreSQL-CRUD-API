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
