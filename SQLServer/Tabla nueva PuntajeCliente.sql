USE [Providere]
GO
/****** Object:  Table [dbo].[PuntajeCliente]    Script Date: 02/15/2015 18:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PuntajeCliente](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Positivo] [smallint] NOT NULL,
	[Neutro] [smallint] NOT NULL,
	[Negativo] [smallint] NOT NULL,
	[Total] [smallint] NOT NULL,
	[IdUsuario] [smallint] NOT NULL,
 CONSTRAINT [PK_PuntajeCliente_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_PuntajeCliente_Usuario]    Script Date: 02/15/2015 18:11:25 ******/
ALTER TABLE [dbo].[PuntajeCliente]  WITH CHECK ADD  CONSTRAINT [FK_PuntajeCliente_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[PuntajeCliente] CHECK CONSTRAINT [FK_PuntajeCliente_Usuario]
GO