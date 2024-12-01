DELETE FROM Estados
DELETE FROM Sede
DELETE FROM Tipo_Equipo
DELETE FROM Area
DELETE FROM Usuario

select * FROM Estados
select * FROM Sede
select * FROM Tipo_Equipo
select * FROM Area
select * FROM Usuario

DBCC CHECKIDENT ('Usuario', RESEED, 0);
DBCC CHECKIDENT ('Sede', RESEED, 0);
DBCC CHECKIDENT ('Tipo_Equipo', RESEED, 0);
DBCC CHECKIDENT ('Area', RESEED, 0);
DBCC CHECKIDENT ('Estados', RESEED, 0);