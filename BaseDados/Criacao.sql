USE [PickPrato]
GO
/****** Object:  Table [dbo].[Classificacao]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classificacao](
	[cliente] [varchar](20) NOT NULL,
	[prato] [int] NOT NULL,
	[classificacao] [int] NULL,
	[comentario] [varchar](160) NULL,
 CONSTRAINT [PK_Classificacao] PRIMARY KEY CLUSTERED 
(
	[cliente] ASC,
	[prato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[username] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[fotografia] [varchar](max) NULL,
	[cidade] [varchar](45) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fotografia]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fotografia](
	[idFotografia] [int] IDENTITY(1,1) NOT NULL,
	[fotografia] [varchar](max) NOT NULL,
	[restaurante] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Fotografia] PRIMARY KEY CLUSTERED 
(
	[idFotografia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingrediente]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingrediente](
	[designacao] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Ingrediente] PRIMARY KEY CLUSTERED 
(
	[designacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prato]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prato](
	[idPrato] [int] IDENTITY(1,1) NOT NULL,
	[designacao] [varchar](45) NOT NULL,
	[tipoComida] [varchar](45) NOT NULL,
	[preco] [float] NOT NULL,
	[classificacao] [float] NULL,
	[restaurante] [varchar](20) NULL,
	[fotografia] [varchar](max) NULL,
 CONSTRAINT [PK_Prato] PRIMARY KEY CLUSTERED 
(
	[idPrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prato_possui_Ingrediente]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prato_possui_Ingrediente](
	[prato] [int] NOT NULL,
	[ingrediente] [varchar](128) NOT NULL,
	[customizavel] [tinyint] NOT NULL,
 CONSTRAINT [PK_Prato_possui_Ingrediente] PRIMARY KEY CLUSTERED 
(
	[prato] ASC,
	[ingrediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preferencias]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preferencias](
	[cliente] [varchar](20) NOT NULL,
	[ingrediente] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Preferencias] PRIMARY KEY CLUSTERED 
(
	[cliente] ASC,
	[ingrediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurante]    Script Date: 08/06/2017 15:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurante](
	[proprietario] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[localizacao] [varchar](60) NOT NULL,
	[nome] [varchar](45) NOT NULL,
	[telefone] [varchar](9) NOT NULL,
	[email] [varchar](45) NOT NULL,
	[estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Restaurante] PRIMARY KEY CLUSTERED 
(
	[proprietario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classificacao]  WITH CHECK ADD  CONSTRAINT [FK_Classificacao_Cliente] FOREIGN KEY([cliente])
REFERENCES [dbo].[Cliente] ([username])
GO
ALTER TABLE [dbo].[Classificacao] CHECK CONSTRAINT [FK_Classificacao_Cliente]
GO
ALTER TABLE [dbo].[Classificacao]  WITH CHECK ADD  CONSTRAINT [FK_Classificacao_Prato] FOREIGN KEY([prato])
REFERENCES [dbo].[Prato] ([idPrato])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classificacao] CHECK CONSTRAINT [FK_Classificacao_Prato]
GO
ALTER TABLE [dbo].[Fotografia]  WITH CHECK ADD  CONSTRAINT [FK_Fotografia_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([proprietario])
GO
ALTER TABLE [dbo].[Fotografia] CHECK CONSTRAINT [FK_Fotografia_Restaurante]
GO
ALTER TABLE [dbo].[Prato]  WITH CHECK ADD  CONSTRAINT [FK_Prato_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([proprietario])
GO
ALTER TABLE [dbo].[Prato] CHECK CONSTRAINT [FK_Prato_Restaurante]
GO
ALTER TABLE [dbo].[Prato_possui_Ingrediente]  WITH CHECK ADD  CONSTRAINT [FK_Prato_possui_Ingrediente_Ingrediente] FOREIGN KEY([ingrediente])
REFERENCES [dbo].[Ingrediente] ([designacao])
GO
ALTER TABLE [dbo].[Prato_possui_Ingrediente] CHECK CONSTRAINT [FK_Prato_possui_Ingrediente_Ingrediente]
GO
ALTER TABLE [dbo].[Prato_possui_Ingrediente]  WITH CHECK ADD  CONSTRAINT [FK_Prato_possui_Ingrediente_Prato] FOREIGN KEY([prato])
REFERENCES [dbo].[Prato] ([idPrato])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prato_possui_Ingrediente] CHECK CONSTRAINT [FK_Prato_possui_Ingrediente_Prato]
GO
ALTER TABLE [dbo].[Preferencias]  WITH CHECK ADD  CONSTRAINT [FK_Preferencias_Cliente] FOREIGN KEY([cliente])
REFERENCES [dbo].[Cliente] ([username])
GO
ALTER TABLE [dbo].[Preferencias] CHECK CONSTRAINT [FK_Preferencias_Cliente]
GO
ALTER TABLE [dbo].[Preferencias]  WITH CHECK ADD  CONSTRAINT [FK_Preferencias_Ingrediente] FOREIGN KEY([ingrediente])
REFERENCES [dbo].[Ingrediente] ([designacao])
GO
ALTER TABLE [dbo].[Preferencias] CHECK CONSTRAINT [FK_Preferencias_Ingrediente]
GO
