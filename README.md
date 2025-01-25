
# Paper Universe

Paper Universe é um sistema de login que desenvolvi com o objetivo de colocar em prática os conceitos de autênticação, autorização e testes de unidade.


## Funcionalidades

- Criação de conta
- Verificação de conta
- Autenticação
- Reenvio do código de Verificação
- Exibição dos dados pessoais
- Atualização dos dados pessoais
- Atualização da senha
- Recuperação da senha
- Exclusão da conta


## Stack utilizada


**Back-end:** C#, ASP.NET 8, SQLServer 2022, Entity Framework


## Dependências

- Entitty Framework v8.0.10 
- Flunt v2.0.5
- SecureIdentity v1.0.4
- MediatR v12.4.1
## Rodando os testes

Para rodar os testes, é necessário acessar o PaperUniverse.Tests e rodar o seguinte comando.

```bash
  dotnet test
```


## Aprendizados

Aprendi a configurar o Swagger para usar o Bearer Token como o meio de autenticação e como usar o nome completo dos Schemas como Id para evitar o para não haber conflito entre os Schemas que tem o mesmo nome só que estão em lugares diferentes.

Também aprendi a fazer testes de unidade um pouco mais complexos.

