CREATE TABLE [Classroom].[Grade]
(
	GradeId INT NOT NULL IDENTITY,
	[Value] DECIMAL NOT NULL,
	StudentId INT NOT NULL,
	CourseId INT NOT NULL,
	CreationUserId NVARCHAR(450) NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdateUserId NVARCHAR(450) NOT NULL,
	UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),	

	CONSTRAINT PK_Grade PRIMARY KEY (GradeId),
	CONSTRAINT FK_GradeStudent FOREIGN KEY (StudentId) REFERENCES [Classroom].[Student](StudentId),
	CONSTRAINT FK_GradeCourse FOREIGN KEY (CourseId) REFERENCES [Classroom].[Course](CourseId),
	CONSTRAINT FK_GradeCreationUser FOREIGN KEY (CreationUserId) REFERENCES [Security].[Users](Id),
	CONSTRAINT FK_GradeUpdateUser FOREIGN KEY (UpdateUserId) REFERENCES [Security].[Users](Id)
)
