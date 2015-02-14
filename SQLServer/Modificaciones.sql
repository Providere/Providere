			/*Sentencias para modificar campos en tablas:*/
USE [Providere]
GO
-----------------------TABLA Usuario--------------------------------------
alter table dbo.Usuario alter column Telefono varchar(20) not null
alter table dbo.Usuario alter column Contrasenia varchar(50) not null
alter table dbo.Usuario add Dni varchar(10) not null 

-----------------------TABLA Publicacion----------------------------------
alter table dbo.Publicacion add PrecioOpcion int not null

-----------------------TABLA PreguntaRespuesta----------------------------
alter table dbo.PreguntaRespuesta add FechaRespuesta datetime null 
alter table dbo.PreguntaRespuesta add Estado tinyint not null

-----------------------TABLA Puntaje-------------------------------------
alter table dbo.Puntaje add FechaTotal datetime  null 

-----------------------TABLA Calificacion---------------------------------
alter table dbo.Calificacion add FechaCalificacion datetime not null
alter table dbo.Calificacion add Denunciado tinyint not null -- flag
alter table dbo.Calificacion add Replicado tinyint not null -- flag

-----------------------TABLA Contratacion---------------------------------
alter table dbo.Contratacion add Calificado tinyint not null -- flag


Ejecuten esto:

EXEC sp_RENAME 'Calificacion.Denunciado', 'FlagDenunciado', 'COLUMN'
EXEC sp_RENAME 'Calificacion.Replicado', 'FlagReplicado', 'COLUMN'
EXEC sp_RENAME 'Contratacion.Calificado', 'FlagCalificado', 'COLUMN'