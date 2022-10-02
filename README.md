# AlterPro


=> Conexão com SQL Server

=> CQRS com MediatR

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

	Dados de conexão RabbitMQ:
	{
		Host: http://localhost:15672
		Username: guest
		Password: guest
	}
	
	Fluxo:
	{ 
		Executar API Prototype, Criar um novo servidor, Atualizar um servidor existente, ou deletar um servidor. 
		Ao criar servidor o processo irá disparar uma mensagem RabbitMQ para uma fila chamada: log_message
	}

5 - Abrir aplicação MessageConsumerApi: Ao Executar a aplicação o consumer começará a consumir as mensagens produzidas
	pela API prototype e irá salvar a mensagem como log de transação no mongo Db.


**IMAGENS**
![Screenshot-7](https://github.com/hedu59/AlterPro/blob/main/7-FluxoDeTrabalho.png)
![Screenshot-1](https://github.com/hedu59/AlterPro/blob/main/1-DockerCompose.png)
![Screenshot-2](https://github.com/hedu59/AlterPro/blob/main/2-ImagensDocker.png)
![Screenshot-3](https://github.com/hedu59/AlterPro/blob/main/3-CriarServidor.png)
![Screenshot-4](https://github.com/hedu59/AlterPro/blob/main/4-MensagemCriada.png)
![Screenshot-5](https://github.com/hedu59/AlterPro/blob/main/5-CorpoDaMensagem.png)
![Screenshot-6](https://github.com/hedu59/AlterPro/blob/main/6-ConsumerAtivo.png)



