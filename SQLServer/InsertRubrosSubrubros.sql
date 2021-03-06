USE [Providere]
GO
/*******************TABLA RUBROS***************************/
SET IDENTITY_INSERT [dbo].[Rubro] ON
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (1, N'Niñera')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (2, N'Asistencia a mayores')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (3, N'Enfermería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (4, N'Plomería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (5, N'Gasista')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (6, N'Cerrajería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (7, N'Herrería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (8, N'Carpintería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (9, N'Vidriería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (10, N'Electricista')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (11, N'Albañilería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (12, N'Pintor')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (13, N'Jardinería')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (14, N'Fumigador')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (15, N'Instalación de piscinas')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (16, N'Limpieza de alfombras - Tapizados')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (17, N'Plastificado de pisos - Pulido mosaicos ')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (18, N'Destapador de pozo ciego')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (19, N'Instalación de sistemas de alarmas')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (20, N'Instalación de aire acondicionado')
INSERT [dbo].[Rubro] ([Id], [Nombre]) VALUES (21, N'Service electrodomesticos')
SET IDENTITY_INSERT [dbo].[Rubro] OFF

/*******************TABLA SUBRUBROS***************************/
SET IDENTITY_INSERT [dbo].[SubRubro] ON
INSERT [dbo].[SubRubro] ([Id], [Nombre], [IdRubro]) VALUES (1, N'Heladera', 21)
INSERT [dbo].[SubRubro] ([Id], [Nombre], [IdRubro]) VALUES (2, N'Cocina', 21)
INSERT [dbo].[SubRubro] ([Id], [Nombre], [IdRubro]) VALUES (3, N'Microondas', 21)
INSERT [dbo].[SubRubro] ([Id], [Nombre], [IdRubro]) VALUES (4, N'Aire acondicionado', 21)
INSERT [dbo].[SubRubro] ([Id], [Nombre], [IdRubro]) VALUES (5, N'Televisor - DVD ', 21)
SET IDENTITY_INSERT [dbo].[SubRubro] OFF

