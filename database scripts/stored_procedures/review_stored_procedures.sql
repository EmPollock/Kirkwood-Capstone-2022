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