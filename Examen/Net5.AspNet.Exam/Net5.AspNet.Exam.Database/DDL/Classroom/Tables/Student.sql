CREATE TABLE [Classroom].[Student]
(
	StudentId INT NOT NULL IDENTITY,
	UserId NVARCHAR(450) NOT NULL,    
	FirstName VARCHAR(250) NOT NULL,
	LastName VARCHAR(250) NOT NULL,
	SurName VARCHAR(250) NOT NULL,
	CreationUserId NVARCHAR(450) NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdateUserId NVARCHAR(450) NOT NULL,
	UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),	

	CONSTRAINT PK_Student PRIMARY KEY (StudentId),
	CONSTRAINT FK_StudentUser  FOREIGN KEY (UserId) REFERENCES [Security].[Users](Id),
	CONSTRAINT UC_StudentUser UNIQUE (UserId),
	CONSTRAINT FK_StudentCreationUser FOREIGN KEY (CreationUserId) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_StudentUpdateUser FOREIGN KEY (UpdateUserId) REFERENCES [Security].[Users](Id),

	CONSTRAINT CK_Student CHECK ([Security].[CheckRoleByUserId](UserId,'Student') = 'True')
)
