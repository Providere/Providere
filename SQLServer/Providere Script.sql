USE [Providere]
GO
/****** Object:  Table [dbo].[Rubro]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rubro](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rubro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoEvaluacion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoEvaluacion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TipoEvaluacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TipoEvaluacion] ON
INSERT [dbo].[TipoEvaluacion] ([Id], [Tipo]) VALUES (1, N'Positivo')
INSERT [dbo].[TipoEvaluacion] ([Id], [Tipo]) VALUES (2, N'Neutro')
INSERT [dbo].[TipoEvaluacion] ([Id], [Tipo]) VALUES (3, N'Negativo')
SET IDENTITY_INSERT [dbo].[TipoEvaluacion] OFF
/****** Object:  Table [dbo].[TipoCalificacion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoCalificacion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[TipoDeUsuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoCalificacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TipoCalificacion] ON
INSERT [dbo].[TipoCalificacion] ([Id], [TipoDeUsuario]) VALUES (1, N'Para el prestador')
INSERT [dbo].[TipoCalificacion] ([Id], [TipoDeUsuario]) VALUES (2, N'Para el cliente')
SET IDENTITY_INSERT [dbo].[TipoCalificacion] OFF
/****** Object:  Table [dbo].[Estado]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[TipoEstado] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Estado] ON
INSERT [dbo].[Estado] ([Id], [TipoEstado]) VALUES (1, N'Activo')
INSERT [dbo].[Estado] ([Id], [TipoEstado]) VALUES (2, N'Bloqueado')
INSERT [dbo].[Estado] ([Id], [TipoEstado]) VALUES (3, N'Deshabilitado')
INSERT [dbo].[Estado] ([Id], [TipoEstado]) VALUES (4, N'Inactivo')
SET IDENTITY_INSERT [dbo].[Estado] OFF
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Mail] [varchar](50) NOT NULL,
	[Contrasenia] [varchar](10) NOT NULL,
	[Ubicacion] [varchar](200) NOT NULL,
	[Telefono] [tinyint] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaActivacion] [datetime] NOT NULL,
	[CodActivacion] [varchar](100) NOT NULL,
	[FechaCambioEstado] [datetime] NOT NULL,
	[IdEstado] [smallint] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubRubro]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubRubro](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdRubro] [smallint] NOT NULL,
 CONSTRAINT [PK_SubRubro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sancion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sancion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[IdUsuario] [smallint] NOT NULL,
 CONSTRAINT [PK_Sancion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publicacion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publicacion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NOT NULL,
	[Descripcion] [varchar](1000) NOT NULL,
	[Precio] [smallmoney] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaEdicion] [datetime] NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[IdUsuario] [smallint] NOT NULL,
	[IdRubro] [smallint] NOT NULL,
	[IdSubRubro] [smallint] NULL,
 CONSTRAINT [PK_Publicacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Puntaje]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puntaje](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Positivo] [smallint] NOT NULL,
	[Neutro] [smallint] NOT NULL,
	[Negativo] [smallint] NOT NULL,
	[Total] [smallint] NOT NULL,
	[IdPublicacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Puntaje] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreguntaRespuesta]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PreguntaRespuesta](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Pregunta] [varchar](500) NOT NULL,
	[Respuesta] [varchar](500) NULL,
	[IdUsuario] [smallint] NOT NULL,
	[IdPublicacion] [smallint] NOT NULL,
 CONSTRAINT [PK_PreguntaRespuesta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Imagen]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Imagen](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdPublicacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Imagen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contratacion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contratacion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [smallint] NOT NULL,
	[IdPublicacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Contratacion_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calificacion]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Calificacion](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[IdCalificador] [smallint] NOT NULL,
	[IdCalificado] [smallint] NOT NULL,
	[IdContratacion] [smallint] NOT NULL,
	[IdTipoEvaluacion] [smallint] NOT NULL,
	[IdTipoCalificacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Calificacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Denuncia]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Denuncia](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[IdCalificacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Denuncia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Replica]    Script Date: 10/19/2014 20:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Replica](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Comentario] [varchar](200) NOT NULL,
	[IdCalificacion] [smallint] NOT NULL,
 CONSTRAINT [PK_Replica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Usuario_Estado]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estado]
GO
/****** Object:  ForeignKey [FK_SubRubro_Rubro]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[SubRubro]  WITH CHECK ADD  CONSTRAINT [FK_SubRubro_Rubro] FOREIGN KEY([IdRubro])
REFERENCES [dbo].[Rubro] ([Id])
GO
ALTER TABLE [dbo].[SubRubro] CHECK CONSTRAINT [FK_SubRubro_Rubro]
GO
/****** Object:  ForeignKey [FK_Sancion_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Sancion]  WITH CHECK ADD  CONSTRAINT [FK_Sancion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Sancion] CHECK CONSTRAINT [FK_Sancion_Usuario]
GO
/****** Object:  ForeignKey [FK_Publicacion_Rubro]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Publicacion]  WITH CHECK ADD  CONSTRAINT [FK_Publicacion_Rubro] FOREIGN KEY([IdRubro])
REFERENCES [dbo].[Rubro] ([Id])
GO
ALTER TABLE [dbo].[Publicacion] CHECK CONSTRAINT [FK_Publicacion_Rubro]
GO
/****** Object:  ForeignKey [FK_Publicacion_SubRubro]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Publicacion]  WITH CHECK ADD  CONSTRAINT [FK_Publicacion_SubRubro] FOREIGN KEY([IdSubRubro])
REFERENCES [dbo].[SubRubro] ([Id])
GO
ALTER TABLE [dbo].[Publicacion] CHECK CONSTRAINT [FK_Publicacion_SubRubro]
GO
/****** Object:  ForeignKey [FK_Publicacion_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Publicacion]  WITH CHECK ADD  CONSTRAINT [FK_Publicacion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Publicacion] CHECK CONSTRAINT [FK_Publicacion_Usuario]
GO
/****** Object:  ForeignKey [FK_Puntaje_Publicacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Puntaje]  WITH CHECK ADD  CONSTRAINT [FK_Puntaje_Publicacion] FOREIGN KEY([IdPublicacion])
REFERENCES [dbo].[Publicacion] ([Id])
GO
ALTER TABLE [dbo].[Puntaje] CHECK CONSTRAINT [FK_Puntaje_Publicacion]
GO
/****** Object:  ForeignKey [FK_PreguntaRespuesta_Publicacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[PreguntaRespuesta]  WITH CHECK ADD  CONSTRAINT [FK_PreguntaRespuesta_Publicacion] FOREIGN KEY([IdPublicacion])
REFERENCES [dbo].[Publicacion] ([Id])
GO
ALTER TABLE [dbo].[PreguntaRespuesta] CHECK CONSTRAINT [FK_PreguntaRespuesta_Publicacion]
GO
/****** Object:  ForeignKey [FK_PreguntaRespuesta_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[PreguntaRespuesta]  WITH CHECK ADD  CONSTRAINT [FK_PreguntaRespuesta_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[PreguntaRespuesta] CHECK CONSTRAINT [FK_PreguntaRespuesta_Usuario]
GO
/****** Object:  ForeignKey [FK_Imagen_Publicacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Imagen]  WITH CHECK ADD  CONSTRAINT [FK_Imagen_Publicacion] FOREIGN KEY([IdPublicacion])
REFERENCES [dbo].[Publicacion] ([Id])
GO
ALTER TABLE [dbo].[Imagen] CHECK CONSTRAINT [FK_Imagen_Publicacion]
GO
/****** Object:  ForeignKey [FK_Contratacion_Publicacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Contratacion]  WITH CHECK ADD  CONSTRAINT [FK_Contratacion_Publicacion] FOREIGN KEY([IdPublicacion])
REFERENCES [dbo].[Publicacion] ([Id])
GO
ALTER TABLE [dbo].[Contratacion] CHECK CONSTRAINT [FK_Contratacion_Publicacion]
GO
/****** Object:  ForeignKey [FK_Contratacion_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Contratacion]  WITH CHECK ADD  CONSTRAINT [FK_Contratacion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Contratacion] CHECK CONSTRAINT [FK_Contratacion_Usuario]
GO
/****** Object:  ForeignKey [FK_Calificacion_Contratacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_Contratacion] FOREIGN KEY([IdContratacion])
REFERENCES [dbo].[Contratacion] ([Id])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_Contratacion]
GO
/****** Object:  ForeignKey [FK_Calificacion_TipoCalificacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_TipoCalificacion] FOREIGN KEY([IdTipoCalificacion])
REFERENCES [dbo].[TipoCalificacion] ([Id])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_TipoCalificacion]
GO
/****** Object:  ForeignKey [FK_Calificacion_TipoEvaluacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificacion_TipoEvaluacion] FOREIGN KEY([IdTipoEvaluacion])
REFERENCES [dbo].[TipoEvaluacion] ([Id])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificacion_TipoEvaluacion]
GO
/****** Object:  ForeignKey [FK_Calificado_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificado_Usuario] FOREIGN KEY([IdCalificado])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificado_Usuario]
GO
/****** Object:  ForeignKey [FK_Calificador_Usuario]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Calificacion]  WITH CHECK ADD  CONSTRAINT [FK_Calificador_Usuario] FOREIGN KEY([IdCalificador])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Calificacion] CHECK CONSTRAINT [FK_Calificador_Usuario]
GO
/****** Object:  ForeignKey [FK_Denuncia_Calificacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Denuncia]  WITH CHECK ADD  CONSTRAINT [FK_Denuncia_Calificacion] FOREIGN KEY([IdCalificacion])
REFERENCES [dbo].[Calificacion] ([Id])
GO
ALTER TABLE [dbo].[Denuncia] CHECK CONSTRAINT [FK_Denuncia_Calificacion]
GO
/****** Object:  ForeignKey [FK_Replica_Calificacion]    Script Date: 10/19/2014 20:06:43 ******/
ALTER TABLE [dbo].[Replica]  WITH CHECK ADD  CONSTRAINT [FK_Replica_Calificacion] FOREIGN KEY([IdCalificacion])
REFERENCES [dbo].[Calificacion] ([Id])
GO
ALTER TABLE [dbo].[Replica] CHECK CONSTRAINT [FK_Replica_Calificacion]
GO
