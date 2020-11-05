CREATE DATABASE [apoyo1];
USE [apoyo1]
CREATE TABLE [dbo].[Persona](
[Identificacion] [nvarchar](10) NOT NULL PRIMARY KEY,
[Nombre] [nvarchar](25) NULL,
[Apellido] [nvarchar](25) NULL,
[Sexo] [nvarchar](10) NULL,
[Edad] [int] NULL,
[Departamento] [nvarchar](15) NULL,
[Ciudad] [nvarchar](15) NULL,
[ApoyoId] [nvarchar](10) NULL,
)

CREATE TABLE [dbo].[Ayuda](
[ApoyoId] [int] IDENTITY(1,1) PRIMARY KEY,
[ValorApoyo] [decimal](25) NULL,
[ModalidadApoyo] [nvarchar](25) NULL,
[Fecha] [date] NULL,
)

Insert Into Ayuda (ValorApoyo,ModalidadApoyo, Fecha)values (10000,'economico','10-12-2020')

select * from Persona
select * from ayuda
drop table Ayuda

delete from ayuda
delete from persona
GO
COMMIT