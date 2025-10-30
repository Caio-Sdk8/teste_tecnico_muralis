# Teste Técnico Muralis
Este repositório contempla a entrega do desafio técnico para a vaga de analista desenvolvedor .net, com o prazo inicial de 24/10 até 30/10, porém por contra-tempos o projeto foi iniciado em 28/10 e finalizado em 29/10.

## O projeto
### Rodando o projeto pela primeira vez
#### Pré-Requisitos
Certifique-se de que você tenha os seguintes itens instalados:
1. Git: Para clonar o repositório. <br/>
Instalação: <a href="https://git-scm.com/book/en/v2/Getting-Started-Installing-Git">Instruções para instalar o Git.</a>

2. .NET/C#: Linguagem de programação utilizada no projeto. <br/>
Instalação: <a href="https://dotnet.microsoft.com/pt-br/download">Instruções para instalar o .NET.<a/> <br/>
<strong>Certifique-se de que tenha instalado a versão 9 ou superiores.</strong>

3. IDE: Recomendo o uso de <a href="https://visualstudio.microsoft.com/pt-br/vs/community">Visual Studio Community</a>, que foi a ide usada para desenvolver e também na exibição do video tutorial, porem outras ides podem servir para rodar o projeto, mas necessitando de mais passos.

#### Como rodar o projeto?
Agora com todas as ferramentas necessárias para rodar o projeto, veja o video tutorial abaixo e siga calmamente os passos até que a api esteja em pleno funcionamento na sua máquina.


https://github.com/user-attachments/assets/17bcab24-5014-47f4-a493-3a82528d2d3f


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
├── Tests/                 → Testes unitários simples
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

### Postman
Necessário instalar o [Postman](https://www.postman.com/downloads) e fazer login/criar conta, para seguir os passos abaixo e testar as requisições por ele, veja abaixo como utilizar a coleção de requisições postman disponibilizadas nesse repositório.


https://github.com/user-attachments/assets/163b9d16-9e28-48e7-8269-dd52b584822f



## Motivos de decisões técnicas
### Por que usar Sqlite?
Como o escopo do teste não exigia algo robusto, umas vez que era uma implementação simples, o sqlite cumpre bem esse papel sendo um banco simples de integrar, sem muitas necessidades de ambiente na hora de usar, leve e prático para testes como este.
### Por que não dockerizar?
Tive receio de dockerizar a solução e acabar gerando muitos passos para quem fosse testar, o que poderia gerar um trabalho desnecessário, além disso como não fazia parte do escopo inicial seria mais um extra do que uma necessidade neste momento.
### Por que permitir a criação de vários contatos mas apenas um endereço?
O diagrama da situação problema não deixada a cardinalidade explicita, por tanto era uma decisão pessoal como seguir a partir dali, e o ponto chave que me fez tomar essa decisão foi pelo simples fato do contato ter tipo, com a existência de um tipo fica clara a possibilidade de um mesmo cliente ter multiplos contatos, de tipos diferentes, como telefone, e-mail, site e afins, enquanto o endereço é apenas o endereço e nada a mais, permitir mais de um endereço não faria sentido pois não existiria um propósito claro para cada um utilizando somente os atributos exigidos no diagrama. Porém da maneira que o sistema foi feito e projetado é simples alterar isso e possibilitar a criação de multiplos endereços.

## Observações pessoais
Foi minha primeira vez usando mappers em .net, portanto é possivel que não tenha feito o melhor uso desta tecnologia.
Foi extremamente interessante e divertido aplicar conceitos que desenvolvi fora do c# (durante meu estágio em outra stack) em c#, utilizando alguns recursos que não eram habituais para mim quando c# era minha stack principal e não tinha tanta experiência
Demorei um pouco para entregar o teste pois de sexta (24/10) até segunda (27/10) tive contratempos e compromissos pessoais, mas espero que gostem do meu teste! uma ótima avaliação para vocês, e obrigado pela oportunidade!
