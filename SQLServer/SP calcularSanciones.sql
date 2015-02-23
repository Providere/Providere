Use Providere;


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_calcularSanciones') AND type in (N'P', N'PC'))
  DROP PROCEDURE SP_calcularSanciones
GO
create PROCEDURE SP_calcularSanciones
AS
BEGIN

UPDATE Usuario SET IdEstado = 3 
WHERE Id IN (
  Select   
  c.idcalificado
  FROM calificacion as c
  INNER JOIN denuncia as d
  ON c.id = d.idCalificacion
  WHERE 
  c.idTipoEvaluacion = 3 AND
  c.fechaCalificacion > DATEADD(month, -1, GETDATE())
   group by c.idcalificado
  HAVING 
  (Select Count(idTipoEvaluacion) as cantNegativas From calificacion as df where c.idcalificado = df.idcalificado  aND idTipoEvaluacion = 3 group by df.idcalificado) > 5 
  AND 
  (Count(idTipoEvaluacion)* 100 / (Select Count(*) From calificacion as cc where c.idcalificado = cc.idcalificado aND idTipoEvaluacion = 3  group by cc.idcalificado)) >= 60
  )

END
GO