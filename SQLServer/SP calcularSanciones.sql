Use Providere;

CREATE PROCEDURE calcularSanciones
AS

UPDATE FROM Usuario SET Estado = 3 
WHERE idUsuario IN (SELECT idUsuario FROM Denuncia WHERE Fecha > DATEADD(month, -1, GETDATE()))
AND idUsuario IN (
Select idUsuario WHERE AVG(Shoesize)

)


 List<Calificacion> calificacionesNegativas = cs.obtenerNegativasDeUsuario(usuario);

                int porcentajeDeDenuncias = (calificacionesNegativas.Count * 100) / usuario.Calificacion.Count;

                if (porcentajeDeDenuncias > 90)
                {
                    establecerSancion(usuario);
                }



GO