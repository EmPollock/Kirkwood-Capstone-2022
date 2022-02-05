USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
File containing the stored procedures for volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all the volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_all_volunteers'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteers]
AS
	BEGIN
		SELECT 
			[Volunteers].[VolunteerID],
			[Users].[GivenName],
			[Users].[FamilyName],			
			[Users].[UserState],		
			[Users].[City],			
			[Users].[Zip],
			[VolunteerType].[RoleID]
		FROM [Volunteers] 
		JOIN [Users] ON [Volunteers].[UserID] = [Users].[UserID]
		JOIN [VolunteerType] ON [Volunteers].[VolunteerID] = [VolunteerType].[VolunteerID]
	END	
GO



/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all reviews for the volunteers
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_all_volunteer_reviews'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteer_reviews]
AS
	BEGIN
		SELECT 
			[VolunteerReviews].[VolunteerID],
			AVG([VolunteerReviews].[Rating])
		FROM [VolunteerReviews] 
		GROUP BY [VolunteerID]
	END	
GO
