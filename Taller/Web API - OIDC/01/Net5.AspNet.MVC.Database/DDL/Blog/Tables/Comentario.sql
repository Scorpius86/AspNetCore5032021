CREATE TABLE [Blog].[Comentario]
(
	ComentarioId INT NOT NULL IDENTITY,
	PostId INT NOT NULL,
	Contenido VARCHAR(MAX) NULL,
	UsuarioIdPropietario NVARCHAR(450) NOT NULL,
	UsuarioIdCreacion NVARCHAR(450) NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioIdActualizacion NVARCHAR(450) NOT NULL,
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Comentario PRIMARY KEY (ComentarioId),
	CONSTRAINT FK_ComentarioPost FOREIGN KEY (PostId) REFERENCES Blog.Post(PostId) ON DELETE CASCADE,
	CONSTRAINT FK_ComentarioUsuarioPropietario FOREIGN KEY (UsuarioIdPropietario) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_ComentarioUsuarioCreacion FOREIGN KEY (UsuarioIdCreacion) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_ComentarioUsuarioActualizacion FOREIGN KEY (UsuarioIdActualizacion) REFERENCES [Security].[Users](Id)
)
