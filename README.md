
# Banco Estrela ★

Projeto não finalizado.
Início em 23/05/2024

## Sumário
1. [Introdução](#introducao)
1. [Funcionalidades](#funcionalidades)
1. [Tecnologias Utilizadas](#tecnologias-utilizadas)
1. [Instalação](#instalação)
1. [Como Usar](#como-usar)
1. [Contribuição](#contribuição)

## Introdução

O Banco estrela é um sistema de banco virtual desenvoldido para fins educacionais, como parte de um estudo sobre programação em C# utilizando .NET. O sistema permite que os usuários realizem operações como criação de conta, realizar login, depositar, sacar e gerenciar suas informações pessoais. 

## Funcionalidades
- **Cadastro de Usuário**: Permite que novos usuários se cadastrem no sistema fornecendo nome, sobrenome, CPF, data de nascimento, nome de usuário, senha e resposta para uma pergunta de segurança.

- **Login**: Permite que usuários cadastrados façam login no sistema usando seu nome de usuário e senha.

- **Operações Bancárias**: Os usuários podem realizar as seguintes operações:
    - Depositar dinheiro em sua conta.
    - Sacar dinheiro de sua conta, desde que tenham saldo disponível.
    - Verificar saldo atual.
    - Listar todas as transações realizadas.

- **Recuperação de Senha**: Os usuários podem recuperar sua senha respondendo à pergunta de segurança previamente configurada.

## Tecnologias utilizadas:

- **C#**: Linguagem de programação principal.
- **.NET Core**: Plataforma utilizada para desenvolver a aplicação.
- **Entity Framework Core**: Framework de mapeamento objeto-relacional _(ORM)_ para acessar o banco de dados.
- **SQLite**: Banco de dados embutido utilizado para armazenar informações dos usuários.
- **Console Application**: Interface de linha de comando utilizada para interação com o usuário.
- **Newtonsoft.Json**: Biblioteca utilizada para manipulação de dados JSON, usada para interpretar o retorno da API de geração de CPF.

## Instalação

Clone o repositório para a sua máquina:

```bash
git clone https://github.com/LXSCA7/ProjetoBanco.git
```

Vá para a pasta do projeto:

```cd
cd ProjetoBanco
```

Compile e execute o projeto:

```
dotnet run
```

## Como usar

1. Ao iniciar o programa, você será apresentado a um menu principal com opções numéricas.
1. Selecione a opção desejada digitando o número correspondente e pressionando Enter.
1. Siga as instruções na tela para cada operação específica (por exemplo, criar conta, fazer login, realizar operações bancárias).

## Testes com CPF de teste

Para testar o sistema sem a necessidade de inserir um CPF válido, foi implementada a funcionalidade de utilizar um CPF gerado automaticamente por uma [API](https://api.invertexto.com/api-gerador-pessoas) Ao digitar 'test' ou 'teste' no campo de CPF durante o cadastro, o sistema consulta essa API para obter um CPF válido, utilizando a classe `CPFGenerator.cs`. Esta classe possui um método assíncrono `GetCpfAsync()` para fazer a consulta à API e um método síncrono `GetCpf()` que retorna o CPF obtido como uma string, utilizando o pacote Newtonsoft.Json para manipulação dos dados JSON retornados pela API.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues para relatar problemas, sugestões de melhorias ou para discutir novas funcionalidades que possam ser adicionadas ao projeto. Se deseja contribuir diretamente, siga estes passos:

1. Faça um fork do projeto.
1. Crie uma branch para sua feature (git checkout -b feature/nova-feature).
1. Commit suas mudanças (git commit -am 'Adiciona nova feature').
1. Faça o push para a branch (git push origin feature/nova-feature).
1. Crie um novo Pull Request.
