# OficinasHSD API Backend

Este documento descreve como configurar e executar a API backend do projeto OficinasHSD.

## Arquitetura

O projeto utiliza a **Clean Architecture**, garantindo separação de responsabilidades por camadas:

* **Domain**: regras de negócio e entidades centrais.
* **Application**: contratos, serviços e casos de uso.
* **Infrastructure**: acesso a banco de dados, implementação de repositórios e configurações.
* **API**: camada de apresentação com controllers e endpoints.

## Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* SQL Server (Developer ou Express)
* Visual Studio, Visual Studio Code ou outro editor de sua preferência

## 1. Restaurar o banco de dados

O projeto inclui um arquivo de backup `DB_HSD.bak` na raiz do repositório. Restaure o banco no SQL Server Management Studio (SSMS)

## 2. Ajustar a Connection String

No arquivo `appsettings.Development.json`, localize a seção `"ConnectionStrings"` e ajuste conforme seu ambiente:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=DB_HSD;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

* **Server**: instância do SQL Server (ex: `localhost`, `localhost\SQLEXPRESS`, ou IP).
* **Database**: nome do banco (`DB_HSD`).

## 3. Configurar a Secret Key

A API utiliza JWT para autenticação e requer uma chave secreta (`SecretKey`). Para gerar uma chave de forma simples, Siga os passos:

1. Gere uma chave segura via shell:

   ```bash
   # usando OpenSSL
   openssl rand -base64 32

   # ou no PowerShell
   [Convert]::ToBase64String((New-Object Byte[] 32 | %{Get-Random -Maximum 256}))
   ```

2. No `appsettings.Development.json`, localize a seção `JwtSettings` ou similar e substitua:

   ```json
   "JwtSettings": {
     "Secret": "SUA_CHAVE_AQUI",
     "Issuer": "OficinasHSD.API",
     "Audience": "OficinasHSD.Client",
     "ExpiryMinutes": 60
   }
   ```

## 4. Aplicar Migrations (Opcional)

Caso deseje recriar o banco ou aplicar mudanças, use as migrations do Entity Framework:

```bash
dotnet ef database update --context OficinasHSDDBContext --project OficinasHSD.Infrastructure --startup-project OficinasHSD.API
```

## 5. Executar a API

1. Por padrão, a API estará disponível em:

   * HTTP:  `https://localhost:44322`

## 6. Testar Endpoints Testar Endpoints

* **Registrar usuário**: `POST /Usuario/Registrar` com JSON `{ "username": "usuario", "passwordHash": "senha" }`
* **Login**: `POST /Usuario/Login` com JSON `{ "username": "usuario", "passwordHash": "senha" }`

Use tools como Postman, Insomnia ou o próprio frontend para testar.

-------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------

# OficinasHSD Frontend

Este documento descreve como configurar e executar a aplicação frontend do projeto **OficinasHSD**, desenvolvida em **HTML**, **CSS** e **JavaScript** (básico).

---

## 1. Pré‑requisitos

* Navegador moderno (Chrome, Firefox, Edge).
* Conexão com a API backend em execução.
* (Opcional) Extensão Live Server para VS Code ou outro servidor HTTP estático.

---

## 2. Configurar URL da API

No arquivo `JS/api.js`, defina a **URL base** da sua API. Por padrão(Ja esta configurado para a porta: 44322):

```js
const BASE_URL = 'https://localhost:44322';
```

Se o seu backend estiver rodando em outra porta ou host, ajuste:

```js
// Exemplo: API rodando em http://localhost:5000
const BASE_URL = 'http://localhost:5000';
```

---

## 3. Executar o Frontend

### Opção A: Abrir diretamente no navegador

1. Abra o **Explorer** (Windows) ou **Finder** (macOS).
2. Navegue até a pasta `frontend/`.
3. Dê duplo‑clique em `login.html` para abrir no navegador.

### Opção B: Servidor HTTP estático

1. Instale a extensão **Live Server** no VS Code ou outro servidor (ex: http‑server via npm).

2. Abra a pasta `frontend/` no VS Code.

3. Clique em **Go Live** (Live Server)

4. Acesse `http://127.0.0.1:8080` (ou porta informada) no navegador.

---

## 4. Fluxo de Uso
1. **Login**: abra `login.html`, informe credenciais e faça login.
2. **Registro**: abra `register.html`, preencha usuário e senha, clique em **Cadastrar**.
3. **Listagem**: ao logar, você será redirecionado para `index.html`, que carrega a lista de oficinas.
4. **Adicionar**: clique em **Nova Oficina**, preencha dados e salve.
5. **Editar**: clique no botão de editar em cada card, modifique e salve.
6. **Excluir**: clique no ícone de lixeira para remover.
7. **Detalhes**: clique em **Ver Detalhes** para abrir modal de informações.

Mensagens de sucesso/erro aparecem como **toasts** no canto superior direito.

---

Qualquer dúvida, estou a disposição!
