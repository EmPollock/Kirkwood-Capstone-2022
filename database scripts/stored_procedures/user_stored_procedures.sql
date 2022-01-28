USE [tadpole_db]
GO

/***************************************************************
Ramiro Pena
Created: 2022/01/24

Description:
File containing the stored procedures for users
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Store Procedure to Insert a User I/E Register
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_insert_User ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_User]
(
	@GivenName			[nvarchar](50),			
	@FamilyName			[nvarchar](50),			
	@Email				[nvarchar](250),
	@UserState 			[char](2), 				
	@City				[nvarchar](75), 			
	@Zip				[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[Users]
			([GivenName], [FamilyName], [Email], [UserState], [City], [Zip])
		VALUES
			(@GivenName, @FamilyName, @Email, @UserState, @City,
			@Zip)
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Stored Procedure for authentication Login
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

/* stored procedures for login */
print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email 				[nvarchar](100),
	@PasswordHash		[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([UserID]) AS 'Authenticated'
		FROM 	[Users]
		WHERE 	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	Active = 1
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Stored Procedure to Select a User
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email 				[nvarchar](100)
)
AS
	BEGIN
		SELECT 	[UserID], [GivenName], [FamilyName], [Email], [UserState],
			[City], [Zip], [Active]
		FROM 	[Users]
		WHERE 	@Email = [Email]
	END
GO

/***************************************************************
/ Ramiro Pena
/ Created: 2022/01/24
/ 
/ Description: Creating Store Procedure to Update a user's password from defualt.
/
/
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_update_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@Email 				[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[Users]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE 	@Email = [Email]
		  AND	@OldPasswordHash = [PasswordHash]
		RETURN @@ROWCOUNT
	END
GO