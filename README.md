## Sobre o projeto


Esta **API**, desenvolvida utilizando **.NET 8**, adota os principios do **Domain-Driven Design (DDD)** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalhando informações como título, data e hora, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em um banco de dados MySQL.

A arquitetura da API baseia-se em **REST**, utilizando métodos HTTP padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes **NuGet** utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisiçlio/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado nos testes de unidade para tomar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **Fluent Validation** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM **(Object-Relational Mapper)** que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas **SQL**



## Features
-  **Domain-Drive-Design (DDD)**: Estrutura modular que facilita o entendimento do domínio da aplicação.
- **Testes de Unidade**: Teste abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Geração de relatórios**: Capacidade de exportar relatórios detalhado para **PDF** e **Excel**, oferecendo uma análise visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface que facilita a integração e o teste por parte dos desenvolvedores.

## Construindo com

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

<!-- teste-->


## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

* Visual Studio versão 2022+ ou Visual Studio Code
* Windows 10+ Linux/MacOS com [.NET SDK][dot-net-sdk] instalado 
* MySql Sever

## Instalação

1. Clone o repositório: 
    ```sh
    git clone https://github.com/PhSM-PhEN/cashflow.git
    ```
2. Preencha as informações no arquivo `appsettings.Development.Json`.
3. Execute a API e aproveite o seu teste.

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
<!-- Image-->
[hero-image]: Image/heroimage.png
<!-- Badges-->

[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo-dotnet&logoColor-fff&style-for-the-badge

[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo-windows&logoColor-fff&style-for-the-badge

[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo-visualstudio&logoColor=fff&style-for-the-badg 

[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo-mysql&logoColor-fff&style-for-the-badge

[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo-swagger&logoColor=000&style-for-the-badge 
