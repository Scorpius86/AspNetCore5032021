CREATE TABLE [Blog].[Post]
(
	PostId INT NOT NULL IDENTITY,
	Titulo VARCHAR(200) NOT NULL,
	Resumen VARCHAR(MAX) NOT NULL,
	Contenido VARCHAR(MAX) NOT NULL,
	UsuarioIdPropietario NVARCHAR(450) NOT NULL,
	UsuarioIdCreacion NVARCHAR(450) NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioIdActualizacion NVARCHAR(450) NOT NULL,
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Post PRIMARY KEY (PostId),
	CONSTRAINT FK_PostUsuarioPropietario FOREIGN KEY (UsuarioIdPropietario) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_PostUsuarioCreacion FOREIGN KEY (UsuarioIdCreacion) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_PostUsuarioActualizacion FOREIGN KEY (UsuarioIdActualizacion) REFERENCES [Security].[Users](Id)

)
