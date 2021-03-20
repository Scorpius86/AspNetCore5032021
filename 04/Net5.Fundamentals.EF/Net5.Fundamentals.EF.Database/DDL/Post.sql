CREATE TABLE [Blog].[Post]
(
	PostId INT NOT NULL IDENTITY,
	Titulo VARCHAR(200) NOT NULL,
	Contenido VARCHAR(MAX) NOT NULL,
	UsuarioIdPropietario INT NOT NULL,
	UsuarioIdCreacion INT NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioIdActualizacion INT NOT NULL,
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Post PRIMARY KEY (PostId),
	CONSTRAINT FK_PostUsuarioPropietario FOREIGN KEY (UsuarioIdPropietario) REFERENCES Blog.Usuario(UsuarioId),
	CONSTRAINT FK_PostUsuarioCreacion FOREIGN KEY (UsuarioIdCreacion) REFERENCES Blog.Usuario(UsuarioId),
	CONSTRAINT FK_PostUsuarioActualizacion FOREIGN KEY (UsuarioIdActualizacion) REFERENCES Blog.Usuario(UsuarioId)

)
