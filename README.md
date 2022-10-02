# AlterPro


=> Conexão com SQL Server
=> CQRS com MeddiatR
=> Microservices Menssageria com RabbitMQ
=> Registro de Log MongoDb
=> Docker Compose com criação de: SQL Server, MongoDb, RabbitMQ.

############################################################################################

1 - Clone o repositório

2 - Install docker desktop/docker cli

3 - Navegue até a pasta docker compose pelo prompt e digite: docker-compose up -d
	Deverá ser criada as imagens do SQL Server, MongoDb, RabbitMQ

4 - Abrir aplicação Prototype: No console de gerenciador de pacotes escolher o projeto:
	Infraestructure\Prototype.Infra.Data e digitar o comando:
	update-database para criação do banco (Prototype) no SQL Server
	
	Dados de conexão do banco:
	{
		Host: localhost
		Database: Prototype
		Username: sa
		Password: Secret@123
	}

	Fluxo:
	{ 
		Executar API Prototype, Criar Um Servidor, Ao criar servidor o processo irá disparar uma mensagem RabbitMQ para uma fila chamada: log_message
	}

	Dados de conexão RabbitMQ:
	{
		Host: http://localhost:15672
		Username: guest
		Password: guest
	}

5 - Abrir aplicação MessageConsumerApi: Ao Executar a aplicação o consumer começará a consumir as mensagens produzidas
	pela API prototype e irá salvar a mensagem como log de transação no mongo Db.
