--CREATE DATABASE dbmydocs
--GO

USE dbmydocs
GO

DROP TABLE IF EXISTS [USER]
GO
CREATE TABLE [USER](
	[ID] INT NOT NULL IDENTITY(1, 1),
	[NAME] VARCHAR(150) NOT NULL,
	[CPF] VARCHAR(14) NOT NULL,
	[DATE_OF_BIRTH] DATE NULL,
	[PHONE] VARCHAR(14) NULL,

	CONSTRAINT PK_USER PRIMARY KEY([ID])
);
GO

DROP TABLE IF EXISTS [USER_CREDENTIAL]
GO
CREATE TABLE [USER_CREDENTIAL](
	[ID_USER] INT NOT NULL,
	[EMAIL] VARCHAR(200) NOT NULL,
	[PASSWORD] VARCHAR(14) NOT NULL,

	CONSTRAINT PK_USER_CREDENTIAL PRIMARY KEY([ID_USER]),
	CONSTRAINT FK_USER_USER_CREDENTIAL FOREIGN KEY([ID_USER]) REFERENCES [USER]([ID])
);

DROP TABLE IF EXISTS ALERT
GO
CREATE TABLE ALERT(
	[ID] INT NOT NULL IDENTITY(1, 1),
	[ID_USER] INT NOT NULL,
	[NAME] VARCHAR(70) NOT NULL,
	[DESCRIPTION] VARCHAR(200) NULL,
	[NR_RECURRENCE] INT NOT NULL,
	[CREATION_DATE] DATE NOT NULL,
	[END_DATE] DATE NULL,
	[FIRST_DAY_SEND] DATE NOT NULL,
	[JOB_ID] VARCHAR(100) NULL,

	CONSTRAINT PK_ALERT PRIMARY KEY([ID]),
	CONSTRAINT FK_ALERT_USER FOREIGN KEY([ID_USER]) REFERENCES [USER]([ID])
);

DROP TABLE IF EXISTS [DOCUMENT]
GO
CREATE TABLE DOCUMENT(
	[ID] INT NOT NULL IDENTITY(1, 1),
	[ID_USER] INT NOT NULL,
	[FILE_NAME] VARCHAR(50) NOT NULL,
	[UNIQUE_FILE_NAME] VARCHAR(50) NOT NULL,
	[FILE_TYPE] VARCHAR(50) NOT NULL,
	[FILE_SIZE] BIGINT NOT NULL,
	[UPLOAD_DATE] DATE NOT NULL,

	CONSTRAINT PK_DOCUMENT PRIMARY KEY([ID]),
	CONSTRAINT FK_DOCUMENT_USER FOREIGN KEY([ID_USER]) REFERENCES [USER]([ID])
)

---------------------------------------------------------------------------------------------------------------------------------
DROP TABLE IF EXISTS EMAIL_TEMPLATE
GO
CREATE TABLE EMAIL_TEMPLATE(
	[ID] INT NOT NULL IDENTITY,
	[NAME] VARCHAR(100) NOT NULL,
	[SUBJECT] VARCHAR(150) NULL,
	[BODY] VARCHAR(MAX) NOT NULL,

	CONSTRAINT PK_EMAIL_TEMPLATE PRIMARY KEY([ID])
);
GO

INSERT INTO EMAIL_TEMPLATE VALUES ('Welcome', 'Boas Vindas - MyDocs', '<!DOCTYPE html>  <html lang="pt-BR">  <head>    <meta charset="UTF-8">    <title>Bem-vindo ao MyDocs</title>    <style>      body { font-family: Arial, sans-serif; background-color: #f4f4f4; color: #333; padding: 20px; }      .container { background-color: #fff; padding: 30px; border-radius: 8px; max-width: 600px; margin: auto; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }      h1 { color: #1e88e5; }      p { font-size: 16px; line-height: 1.5; }      .button { margin-top: 20px; display: inline-block; padding: 10px 20px; background-color: #1e88e5; color: #fff; text-decoration: none; border-radius: 5px; }    </style>  </head>  <body>    <div class="container">      <h1>Bem-vindo ao MyDocs!</h1>      <p>Olá, #USER_NAME#,</p>      <p>Estamos felizes em ter você com a gente. Com o <strong>MyDocs</strong>, seus documentos estarão organizados, seguros e sempre à sua disposição.</p>      <p>Comece agora mesmo acessando sua área de documentos.</p>      <p>Se precisar de ajuda, conte com a gente!</p>    </div>  </body>  </html>');

INSERT INTO EMAIL_TEMPLATE VALUES('Overdue Bill', 'Vencimento Boleto - MyDocs', '<!DOCTYPE html>  <html lang="pt-BR">  <head>    <meta charset="UTF-8">    <title>Boleto vence hoje</title>    <style>      body { font-family: Arial, sans-serif; background-color: #fff3cd; color: #856404; padding: 20px; }      .container { background-color: #fff; padding: 30px; border-radius: 8px; max-width: 600px; margin: auto; border: 1px solid #ffeeba; }      h2 { color: #d39e00; }      p { font-size: 16px; line-height: 1.5; }      .button { margin-top: 20px; display: inline-block; padding: 10px 20px; background-color: #d39e00; color: #fff; text-decoration: none; border-radius: 5px; }    </style>  </head>  <body>    <div class="container">      <h2>Atenção: Um Boleto seu Vence Hoje</h2>      <p>Olá, #USER_NAME#,</p>      <p>Identificamos que o boleto <strong>"#ALERT_NAME#"</strong> tem vencimento marcado para <strong>hoje</strong>.</p>      <p>Evite juros ou multas. Faça o pagamento o quanto antes!</p>      <p>Se você já pagou, desconsidere esta mensagem.</p>    </div>  </body>  </html>');
