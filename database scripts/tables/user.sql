USE [tadpole_db]
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Users Table
/
/ Updates:
/ Derrick Nagy
/ Updated: 2022/02/07
/
/ Description: 
/ Added the test records for "Finn" and "River"
/
/ Christopher Repko
/ Updated: 2022/02/07
/
/ Description: 
/ Changed default password
****************************************************************/
print '' print '*** creating Users Table ...'
/* Whatever you're adding goes here. */

/* user table section */
print '' print '***creating user table'
GO
CREATE TABLE [dbo].[Users](
	[UserID]			[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]			[nvarchar](50)				NOT NULL,
	[FamilyName]		[nvarchar](50)				NOT NULL,
	[Email]				[nvarchar](250)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT 
		'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342',
	[UserState] 		[char](2) 					NULL,
	[City]				[nvarchar](75) 				NULL,
	[Zip]				[int]						NULL,
	[UserPhoto]			[nvarchar](200)				NULL,
	[UserDescription]	[nvarchar](3000)			NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1,
	[DateCreated]		[DateTime]					NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** adding sample user records ***'
GO
INSERT INTO [dbo].[Users]
		([GivenName], [FamilyName], [Email], [UserState], [City], [Zip], [UserPhoto], [UserDescription])
	VALUES
		('Joanne', 'Smith', 'joanne@company.com', 'IA', 'Cedar Rapids', 52402, NULL, NULL)
		,('Finn', 'Human', 'finn@company.com', 'IA', 'Coralville', 52241, NULL, NULL)
		,('River', 'Blueberry Rainbow', 'river@company.com', 'IA', 'Boone', 50036, NULL, NULL)
GO
