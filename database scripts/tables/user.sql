USE [tadpole_db]
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Users Table
***************************************************************
/ Updates:
/ Derrick Nagy
/ Updated: 2022/02/07
/
/ Description: 
/ Added the test records for "Finn" and "River"
***************************************************************
/ Updates:
/ Derrick Nagy
/ Updated: 2022/04/05
/
/ Description: 
/ Added the test records for Mark Paterno
****************************************************************/
print '' print '*** creating Users Table ...'
print '' print '***creating user table'
GO
CREATE TABLE [dbo].[Users](
	[UserID]			[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]			[nvarchar](50)				NOT NULL,
	[FamilyName]		[nvarchar](50)				NOT NULL,
	[Email]				[nvarchar](250)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT 
		'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
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
		,('Mark', 'Paterno', 'marcosgrilledcheese@fake.com', 'IA', 'Iowa City', 52245, NULL, NULL)
		
GO
