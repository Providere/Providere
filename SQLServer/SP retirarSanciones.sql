Use Providere;


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_retirarSanciones') AND type in (N'P', N'PC'))
  DROP PROCEDURE SP_retirarSanciones
GO
create PROCEDURE SP_retirarSanciones
AS
BEGIN

UPDATE Usuario SET IdEstado = 1 
WHERE IdEstado = 3 

END
GO