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

## 4. Configuração do Ambiente
Para que a aplicação funcione corretamente, edite o arquivo `appsettings.Development.json` seguindo o modelo abaixo:

```json
{
  "ConnectionStrings": {
    "ConnectionStrings": "Server=payments-sqlserver,1433;Initial Catalog=db_fcg_payments;Persist Security Info=False;User ID=sa;Password=pass@123;Encrypt=False;Pooling=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "ChaveSuperSecretaComMaisDe32CaracteresAqui12345",
    "Issuer": "FCG-Users"
  },
  "Rabbitmq": {
    "Url": "localhost",
    "Username": "admin",
    "Password": "admin123"
  },
  "AllowedHosts": "*"
}
```

## 👥 Integrantes
- **Nome do Grupo:**: 33.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).

