CREATE TABLE [Blog].[Comentario]
(
	ComentarioId INT NOT NULL IDENTITY,
	PostId INT NOT NULL,
	Contenido VARCHAR(MAX) NULL,
	UsuarioIdPropietario INT NOT NULL,
	UsuarioIdCreacion INT NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioIdActualizacion INT NOT NULL,
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Comentario PRIMARY KEY (ComentarioId),
	CONSTRAINT FK_ComentarioPost FOREIGN KEY (PostId) REFERENCES Blog.Post(PostId),
	CONSTRAINT FK_ComentarioUsuarioPropietario FOREIGN KEY (UsuarioIdPropietario) REFERENCES Blog.Usuario(UsuarioId),
	CONSTRAINT FK_ComentarioUsuarioCreacion FOREIGN KEY (UsuarioIdCreacion) REFERENCES Blog.Usuario(UsuarioId),
	CONSTRAINT FK_ComentarioUsuarioActualizacion FOREIGN KEY (UsuarioIdActualizacion) REFERENCES Blog.Usuario(UsuarioId)
)
