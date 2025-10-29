# Teste Técnico Muralis
Este repositório contempla a entrega do desafio técnico para a vaga de analista desenvolvedor .net, com o prazo inicial de 24/10 até 30/10, porém por contra-tempos o projeto foi iniciado em 28/10 e finalizado em 29/10.

## O projeto
### Rodando o projeto pela primeira vez
### Estrutura do Projeto

```text
teste_tecnico/
├── Controllers/           → Recebe requisições HTTP
├── Domains/               → Entidades de negócio (Cliente, Contato, Endereco)
├── DTOs/                  → Objetos de transferência de dados
├── Interfaces/            → Contratos (services e repositories)
├── Repositories/          → Implementações de acesso ao banco
├── Services/              → Lógica de negócio
├── Data/                  → Contexto do EF Core (AppDbContext)
├── Migrations/            → Scripts de migração do banco
├── Profiles/              → Mapeamentos (AutoMapper)
├── Program.cs             → Ponto de entrada da aplicação
└── appsettings.json       → Configurações
```

### Camadas Principais (Fluxo Simplificado)

```text
[Controller] 
    ↓ (recebe request)
[Service] → [Repository] → [DbContext]
    ↑ (retorna DTO)
[DTO] ← [Domain]
```

- **Controller**: Leve — apenas recebe as requisições e despacha para a camada de serviços.  
- **Service**: Aplica regras de negócio.  
- **Repository**: Acessa o banco (via DbContext).  
- **Domain**: Entidades puras, sem lógica de persistência (responsabilidade do repository).  
- **DTO**: Dados serializados para APIs.  
### Demo

### Postman

## Motivos de decisões técnicas
### Por que usar Sqlite?
Como o escopo do teste não exigia algo robusto, umas vez que era uma implementação simples, o sqlite cumpre bem esse papel sendo um banco simples de integrar, sem muitas necessidades de ambiente na hora de usar, leve e prático para testes como este.
### Por que não dockerizar?
Tive receio de dockerizar a solução e acabar gerando muitos passos para quem fosse testar, o que poderia gerar um trabalho desnecessário, além disso como não fazia parte do escopo inicial seria mais um extra do que uma necessidade neste momento.

## Observações pessoais
Foi minha primeira vez usando mappers em .net, portanto é possivel que não tenha feito o melhor uso desta tecnologia.
Foi extremamente interessante e divertido aplicar conceitos que desenvolvi fora do c# (durante meu estágio em outra stack) em c#, utilizando alguns recursos que não eram habituais para mim quando c# era minha stack principal e não tinha tanta experiência
Demorei um pouco para entregar o teste pois de sexta (24/10) até segunda (27/10) tive contratempos e compromissos pessoais, mas espero que gostem do meu teste! uma ótima avaliação para vocês, e obrigado pela oportunidade!
