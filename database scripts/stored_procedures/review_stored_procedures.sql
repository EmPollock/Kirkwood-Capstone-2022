USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
File containing the stored procedures for Review
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
Stored procedure to select all the reviews associated with a
location
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_location_reviews'
GO
CREATE PROCEDURE [dbo].[sp_select_location_reviews]
(
    @LocationID     [int]
)
AS
	BEGIN
		SELECT 
			[LocationReview].[ReviewID],
            CONCAT([GivenName], ' ', [FamilyName]),
            [ReviewType],
            [Rating],
            [Review],
            [Review].[DateCreated],
            [Review].[Active]
		FROM [LocationReview]
        JOIN [Review] ON [LocationReview].[ReviewID] = [Review].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
        WHERE [LocationID] = @LocationID
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select all the reviews associated with a supplier
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_supplier_reviews'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_reviews]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[SupplierReview].[ReviewID],
            CONCAT([GivenName], ' ', [FamilyName]),
            [ReviewType],
            [Rating],
            [Review],
            [Review].[DateCreated],
            [Review].[Active]
		FROM [SupplierReview]
        JOIN [Review] ON [SupplierReview].[ReviewID] = [Review].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
        WHERE [SupplierID] = @SupplierID
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/01/26

Description:
Stored procedure to select all reviews for the volunteers
**************************************************************
Austin Timmerman
Updated: 2022/03/10

Description: 
Updated to match the current review
****************************************************************/
print '' print '*** creating sp_select_all_volunteer_reviews'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteer_reviews]
AS
	BEGIN
		SELECT 
			[VolunteerReview].[VolunteerID],
			AVG([Review].[Rating])
		FROM [VolunteerReview] JOIN [Review] ON [Review].[ReviewID] = [VolunteerReview].[ReviewID]
		GROUP BY [VolunteerID]
	END	
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/10

Description:
Stored procedure to select all reviews for a volunteer by volunteerID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_volunteer_reviews_by_volunteerID'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_reviews_by_volunteerID]
(
	@VolunteerID		[int]
)
AS
	BEGIN
		SELECT 
			[VolunteerReview].[ReviewID],
			[Review].[Rating],
			CONCAT([GivenName], " ", [FamilyName]) AS "FullName",
			[Review].[ReviewType],		
			[Review].[Review],
			[Review].[DateCreated]
		FROM [VolunteerReview] 
		JOIN [Review] ON [Review].[ReviewID] = [VolunteerReview].[ReviewID]
		JOIN [Users] ON [Users].[UserID] = [Review].[UserID]
		WHERE [Review].[Active] = 1
			   AND [VolunteerID] = @VolunteerID 
	END	
GO