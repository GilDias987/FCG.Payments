# 🎮 FIAP Cloud Games - PaymentsAPI

Responsável pelo processamento e validação financeira das transações de compra de jogos.

## 1. Funcionalidades
* Processamento assíncrono de pagamentos.
* Simulação de integração com gateways de pagamento.

## 2. Fluxo Orientado a Eventos
Este serviço atua como um processador intermediário no fluxo de checkout.

* **Consumidos:**
    * `OrderPlacedEvent`: Recebe a intenção de compra para iniciar o processamento financeiro.
* **Publicados:**
    * `PaymentProcessedEvent`: Publicado após o processamento, informando o status final (`Approved` ou `Rejected`).

## 3. Tecnologias
* **Linguagem:** .NET 10
* **Banco de Dados:** SQL Server
* **Mensageria:** RabbitMQ (via MassTransit)
* **Padrões:** MediatR, FluentValidation
* **Documentação:** Swagger
* **Orquestração:** Docker & Kubernetes

## 4. Variáveis de Ambiente
| Variável | Descrição | Exemplo |
| :--- | :--- | :--- |
| `ConnectionStrings__DefaultConnection` | String de conexão com SQL Server | `Server=db;Database=PaymentsDb;...` |
| `RabbitMQ__Host` | Host do Broker de Mensageria | `rabbitmq://rabbitmq-service` |
| `PaymentSettings__SimulationDelay` | Tempo simulado de processamento | `2000` |

## 👥 Integrantes
- **Nome do Grupo:**: 33.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).

