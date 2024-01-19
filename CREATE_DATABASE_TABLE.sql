CREATE DATABASE [ClienteRaizen]
GO
USE [ClienteRaizen]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 19/01/2024 15:41:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Logradouro] [varchar](150) NOT NULL,
	[Complemento] [varchar](100) NULL,
	[Bairro] [varchar](100) NOT NULL,
	[Cidade] [varchar](100) NOT NULL,
	[UF] [char](2) NOT NULL,
	[CEP] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
