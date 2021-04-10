CREATE TABLE [Classroom].[Course]
(
	CourseId INT NOT NULL IDENTITY,
	[Description] VARCHAR(250) NOT NULL,	
	CreationUserId NVARCHAR(450) NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdateUserId NVARCHAR(450) NOT NULL,
	UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),	

	CONSTRAINT PK_Course PRIMARY KEY (CourseId),
	CONSTRAINT FK_CourseCreationUser FOREIGN KEY (CreationUserId) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_CourseUpdateUser FOREIGN KEY (UpdateUserId) REFERENCES [Security].[Users](Id)
)
