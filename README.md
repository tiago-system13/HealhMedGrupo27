# HACKATHON 4NETT

Bem-vindo ao Projeto Health&Med do Grupo 27!

Neste arquivo README, você encontrará informações úteis sobre o funcionamento do projeto.

## Índice

- [Sobre](#sobre)
- [Tecnologias e Frameworks](#tecnologias-e-frameworks)
- [Requisitos Funcionais](#requisitos-funcionais)
- [Requisitos Não Funcionais](#requisitos-não-funcionais)
- [Em funcionamento](#em-funcionamento)
- [Conclusão](#conclusão)

## Sobre

A Health&Med, uma startup fictícia inovadora no setor de saúde, está desenvolvendo um novo sistema que irá revolucionar a Telemedicina no país.

A ideia do projeto é construir uma API para atender as necessidades do negocio e poder apoiar a empresa a escalar sua operação.

Seguindo o seguinte diagrama Diagrama da arquitetura:

![](.Assets/DesenhodaSoluçãoMVP.png)

## Tecnologias e Frameworks 

Neste projeto, foi utilizado as seguintes tecnologias:

- C# 
- .NET 8
- SQL Server

## Requisitos Funcionais

Todos os requisitos funcionais estão sendo processados diretamente pela API.

- Autenticação do Usuário (Médico)
- Cadastro/Edição de Horários Disponíveis (Médico)
- Aceite ou Recusa de Consultas Médicas (Médico)
- Autenticação do Usuário (Paciente)
- Busca por Médicos (Paciente)
- Agendamento de Consultas (Paciente)

## Requisitos Não Funcionais

- Alta Disponibilidade:
O sistema deve estar disponível 24/7 devido à sua natureza crítica no setor de saúde.

- Escalabilidade:
O sistema deve ser capaz de lidar com alta demanda, especialmente para profissionais muito procurados.
O sistema deve suportar até 20.000 usuários simultâneos em horários de pico.

- Segurança:
A proteção dos dados sensíveis dos pacientes deve seguir as melhores práticas de segurança da informação.

## Em funcionamento

1. Clone este repositório: `git clone https://github.com/tiago-system13/HealhMedGrupo27`
2. Navegue até o diretório do projeto e abra o arquivo .SLN com o visual studio 2022+
3. Instale os pacote NuGet dependentes

Agora com o aplicativo em execução sera possivel visualisar a interface do Swagger onde estarão expostos todos os endpoints disponiveis da API, contendo todas as informações necessarias para começar a fazer as requisições.

![](.Assets/NuGet.png)

## Conclusão

Este projeto foi desenvolvido como desafio final da turma da Pós Tech da FIAP, no curso de Arquitetura de Sistemas .NET com Azure, no ano de 2025!
