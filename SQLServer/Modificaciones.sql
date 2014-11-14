/*Sentencias para modificar campos en tablas:*/
USE [Providere]
GO

alter table dbo.Usuario alter column Telefono varchar(20) not null
alter table dbo.Usuario alter column Contrasenia varchar(50) not null