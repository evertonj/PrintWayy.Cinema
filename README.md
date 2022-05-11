# PrintWayy.Cinema

Para autenticação utilizar Usuário: admin e Senha: admin

Utilizei um usuário fixo para fins de validação do conhecimento.

No projeto PrintWayy.Cinema.Presentation.BlazorServer utilize os comandos abaixo para criar a senha do LiteDB de acordo com sua escolha:
```cmd
dotnet user-secrets init
```
```cmd
dotnet user-secrets set "LiteDB:RepositoryKey" "Sua_Senha"
```
Observação a key "LiteDB:RepositoryKey" não pode ser alterada.
