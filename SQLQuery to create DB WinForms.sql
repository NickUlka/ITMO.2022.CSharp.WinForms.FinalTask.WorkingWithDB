USE [master]
GO
CREATE DATABASE [WinForms]
GO
USE [WinForms]
GO
CREATE TABLE dbo.[People] ( 
	[Id] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] varchar(20) NULL,
	[age] smallint NULL)
ON [PRIMARY];
GO
INSERT INTO dbo.[People]([Name], [age])
  VALUES ('Ann',  12),
		('Ken',   9),
		('Mark',  43),
		('Lola',  30),
		('Mary',  31),
		('David',  21),
		('Gary',  49),
		('Antonio',  24),
		('Luisa',   29),
		('Robert',   60),
		('Sarah',  97);
		GO
		Select * from People