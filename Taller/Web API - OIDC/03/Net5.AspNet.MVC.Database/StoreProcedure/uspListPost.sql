CREATE PROCEDURE [Blog].[uspListPost]	
AS
	SELECT [PostId]
      ,[Titulo]
      ,[Contenido]
      ,[UsuarioIdPropietario]
      ,[UsuarioIdCreacion]
      ,[FechaCreacion]
      ,[UsuarioIdActualizacion]
      ,[FechaActualizacion]
  FROM [Blog].[Post]

