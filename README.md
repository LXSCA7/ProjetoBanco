# Projeto Banco

Início em 23/05/2024

Projeto de um Banco fictício, chamado de Banco Estrela. O projeto foi feito apenas para fins de estudo sobre banco de dados, CRUD e integração de APIs com .NET usando EntityFramework. Por se tratar de um **projeto totalmente fictício e realizado apenas para estudos**, ***NÃO*** há qualquer tipo de segurança envolvendo os usernames e senhas.

# Sumário:

1. [Funcionamento do código](#funcionamento-do-código)
1. [Menu de opções]()
1. []()
1. [Tabela SQL](#tabela)


## Funcionamento do código:

Ao iniciar o programa, uma interface do banco em console é exibida pro usuário com as seguintes opções:

```
+------------------------------------------+
|                                          |
| [1] Criar uma conta                      |
| [2] Fazer login                          |
| [3] Encerrar sua conta                   |
| [4] Realizar um depósito de outro banco  |
|                                          |
| [5] Sair                                 |
|                                          |
+------------------------------------------+
```

## Sobre cada opção:
### [1] Criar uma conta

É basicamente o que o nome diz, ao selecionar essa opção, o console é apagado e é feito um pedido pro usuário inserir:

```
Insira seu nome:
Insira seu sobrenome:
Insira data de nascimento:
```

Ao pedir a data de nascimento pro usuário, o programa fará uma rápida verificação de idade, onde se for menor que 18 anos a criação da conta é cancelada.

```
Tudo certo! Vamos prosseguir com a criação da sua conta...

Insira seu nome de usuário:

// verificação de nome de usuário

Insira uma senha forte
    - MIN 8 CARACTERES
    - LETRA MINÚSCULA E MAIÚSCULA
    - CARACTER ESPECIAL
Insira a senha: 

// verificação de senha forte.
```

As verificações em comentários acima, consistem no seguinte: 

- Verificação de nome de usuário
    - Um loop do-while, onde caso alguém já tenha registrado aquele username, é feito um novo pedido para o usuário
- Verificação de senha forte:
    - Um loop do-while, dessa vez ele percorre a senha e verifica se ela cumpre os seguintes requisitos:
        - Mínimo de 8 caracteres
        - Pelo menos uma letra minúscula e uma maiúscula
        - Pelo menos um caracter especial

Se todos os requisitos forem cumpridos, a seguinte mensagem deverá aparecer na tela:

```
Suas redenciais cadastradas com sucesso! Seja bem-vindo(a) ao Banco Estrela!
```

E novamente o menu de opções aparece.

---

### [2] Fazer login

Ao escolher a opção dois, mais uma vez o console é limpo e é pedido pra inserir:

```
Insira seu nome de usuário:

// verifica se o nome de usuario esta no banco de dados

Insira sua senha:

// verifica se a senha condiz com a senha do banco de dados

```

Caso as duas verificações sejam validas, a seguinte mensagem será escrita na tela:

```
Olá [NOME], seja bem-vindo(a) de volta!
```

Em seguida outro menu de opções irá aparecer.

---

### [3] Encerrar sua conta

---

### [4] Realizar um depósito de outro banco

---

### [5] Sair

Encerra o programa.



## Tabela:

| Id | Username | Senha | NomeCliente | SobrenomeCliente |  Saldo  |
| -- | -------- | ----- | ----------- | ---------------- |  -----  |
| 1  | usuario01 | ***** | Nome       | Sobrenome        | 9999.99 | 
| 2  | usuario02 | ***** | Nome       | Sobrenome        | 9999.99 | 