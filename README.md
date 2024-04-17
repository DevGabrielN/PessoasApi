# API de Gerenciamento de Pessoas

Este é um projeto de API desenvolvido em .NET 8 que gerencia dados de pessoas. Inclui documentação com Swagger, autenticação com Identity e uutiliza Entity Framework como ORM de Banco de Dados gravando os dados apenas em memória usando `UseInMemoryDatabase`.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

## Como rodar o projeto?
- Clone o projeto para sua máquina, usando o comando git clone git@github.com:DevGabrielN/PessoasApi.git
- Abra o projeto no Visual Studio, VS Code ou IDE se sua escolha, e defina o projeto People.API como o de inicialização
  - Ou abra o CMD, navege até a pasta do projeto People.API, e execute o comando: dotnet run --launch-profile https
- Acesse via `https://localhost:{PORTA}/swagger`

## Estrutura do projeto
- `People.API`
  - `People.API.Controllers`: Contém as controllers do sistema, bem como seus endpoints.
  - `Progam.cs`: Arquivo onde é configurado a utilização dos serviços: Swagger, Identity, FluentValidation, EntityFramework, AutoMapper e MediatR.
- `People.Domain`: Projeto onde é definido as regras de negócio mais importantes do sistema.
  - `People.Domain.Commands`: Contém a definição dos commands, bem como seus handlers, que executam de fato o command.
  - `People.Domain.DTOs`: São utilizados DTOs (Data Transfer Objects) para representar os dados de forma mais eficiente e segura.
  - `People.Domain.Entities`: Representam as entidades do banco de dados.
  - `People.Domain.Erros`: Define as mensagens padrões de erros para o Fluent Validation.
  - `People.Domain.Interfaces`: Aqui são definidos os contratos da aplicação
  - `People.Domain.Profiles`: Onde consta os mapeamentos do AutoMapper, que facilitam na conversão de objetos. Exemplo: User > ReadUserDto
  - `People.Domain.Validators`: Define os contratos de validações das entidades aplicando o conceito de Design By Contracts, utilizando o Fluent Validation. Isso permite criar validações complexas e de forma fluída dos objetos, facilitando a reutilização em todo o sistema. Por exemplo, o código para validar uma string como UF é o mesmo para validar o membro UF da classe Person. Essa segregação das validações de forma fluente facilita muito na criação de testes unitários.
  - `People.Domain.ValueObjects`: Esta seção trata dos objetos de valor. Os objetos de valor, essenciais no Domain-Driven Design (DDD), representam conceitos imutáveis, como endereços ou datas. Agrupá-los em uma camada específica facilita sua reutilização em todo o domínio da aplicação.
- `People.Infra`: Camada de infraestrutura e acesso ao banco de dados
  - `People.Infra.Data`: Contém os contexto de acesso a dados das entidades
  - `People.Infra.Repositories`: Alocam as implementações concretas das interfaces de repositório, aplicando o conceito do Design Pattern Repository. Este padrão é utilizado para separar a lógica de acesso a dados da lógica de negócios.


### Arquitetura DDD
O projeto foi desenvolvido utilizando a arquitetura DDD (Domain-Driven Design), que é uma abordagem de design de software que se concentra na modelagem do domínio do problema em questão. Dentro dessa arquitetura, foram aplicados os conceitos de Value Objects.

Value Objects são objetos imutáveis que representam valores que não possuem identidade própria além de suas características. Eles são importantes na modelagem de domínio para representar conceitos como datas, números, endereços, entre outros, e ajudam a garantir a consistência e integridade dos dados.

### Design Patterns utilizados:

- **Commands:** Utilizado para encapsular uma solicitação como um objeto, permitindo parametrizar clientes com diferentes solicitações. Foi utilizado para segregar as regras de negócio.

- **Mediator:** Foi utilizado para melhorar a eficiência dos commands, dessa forma enviando automaticámente o command para seu respectivo handler

- **Repository:** Utilizado para abstrair a lógica de acesso aos dados, fornecendo uma interface comum para acessar e manipular os dados. Promovendo assim a modularidade e facilitando a troca de tecnologias de armazenamento de dados sem afetar o restante da aplicação.

## Autenticação

A API utiliza o Identity para autenticação. É necessário cadastrar um usuário e fazer login, para ter acesso aos endpoints da API.

## Funcionalidades

- Criação de usuário com senha
- Autenticação de usuário e senha
- Cadastro de pessoa
- Busca pessoa por Id
- Busca lista de pessoas
- Busca pessoas por UF
- Atualiza dados de pessoa
- Exclui pessoa por Id

## Documentação da API

### Resultado Genérico

Todos os endpoints, idenpendente do status code sempre retorna uma mensagem padronizada, essa é uma boa prática que facilita a utilização da API no frontend.

Para padronizar os retornos da API, utiliza-se o tipo genérico GenericCommandResult<T>. Este tipo inclui as seguintes propriedades:

- `Success`: Indica se a operação foi bem-sucedida.
- `Date`: Data e hora da resposta.
- `Message`: Mensagem de retorno.
- `Data`: Dados retornados pela operação.
- `Errors`: Lista de erros ocorridos durante a operação.

### Lista de enpoints

- `POST /user/Create`: Cria um novo usuário.
  - Retorna:
    - Status 201 - Created se o usuário foi criado com sucesso.
    - Status 400 - Bad Request se houver problemas com os dados fornecidos.
    
- `POST /user/Login`: Realiza o login de um usuário.
  - Retorna:
    - Status 200 - OK se o login for bem-sucedido.
    - Status 400 - Bad Request se houver problemas com os dados fornecidos.

- `POST /people/CreatePerson`: Cria uma nova pessoa.
  - Campos obrigatórios:
    - FirstName
    - LastName
    - CPF
    - DateBirth
    - UF
  - Retorna:
    - Status 201 - Created se a pessoa foi criada com sucesso.
    - Status 400 - Bad Request se houver problemas com os dados fornecidos.

- `GET /people/GetById/{Id}`: Obtém uma pessoa pelo seu ID.
  - Parâmetro: Id (int).
  - Retorna:
    - Status 200 - OK com os dados da pessoa se encontrada.
    - Status 400 - Bad Request se Id for inválido.
    - Status 401 - Unauthorized (não autorizado)
    - Status 404 - Not Found se a pessoa não for encontrada.

- `GET /people`: Obtém a lista de todas as pessoas cadastradas.
  - Retorna:
    - Status 200 - OK com a lista de pessoas.
    - Status 401 - Unauthorized (não autorizado)
    - Status 404 - Not Found se não encontrar nenhum resultado

- `GET /people/GetByUF/{UF}`: Obtém pessoas de uma determinada UF.
  - Parâmetro: UF (string).
  - Retorna:
    - Status 200 - OK com a lista de pessoas da UF especificada.
    - Status 400 - Bad Request se a UF não for válida.
    - Status 401 - Unauthorized (não autorizado)
    - Status 404 - Not Found se não encontrar nenhum resultado 

- `DELETE /people/DeleteById/{Id}`: Exclui uma pessoa pelo seu ID.
  - Parâmetro: Id (int).
  - Retorna:
    - Status 200 - OK se a exclusão for bem-sucedida.
    - Status 401 - Unauthorized (não autorizado)
    - Status 400 - Bad Request se o Id for inválido.
    - Status 404 - Not Found se a pessoa não for encontrada.

- `PUT /people/{Id}`: Atualiza os dados de uma pessoa pelo seu ID.
  - Parâmetro: Id (int).
  - Campos obrigatórios:
    - FirstName
    - LastName
    - CPF
    - DateBirth
    - UF
  - Retorna:
    - Status 200 - OK com os dados atualizados da pessoa.
    - Status 400 - Bad Request se houver problemas com os dados fornecidos.
    - Status 401 - Unauthorized (não autorizado)
    - Status 404 - Not Found se a pessoa não for encontrada.