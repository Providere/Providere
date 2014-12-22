/*Sentencias para modificar campos en tablas:*/
USE [Providere]
GO
-----------------------TABLA USUARIO--------------------------------------
alter table dbo.Usuario alter column Telefono varchar(20) not null
alter table dbo.Usuario alter column Contrasenia varchar(50) not null
alter table dbo.Usuario add Dni varchar(10) not null 
-----------------------TABLA PUBLICACION----------------------------------
alter table dbo.Publicacion add PrecioOpcion int not null