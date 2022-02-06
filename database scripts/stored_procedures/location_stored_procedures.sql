USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
File containing the stored procedures for locations
****************************************************************/

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
Stored procedure to select all active locations 
from the locations table
****************************************************************/
print '' print '*** creating sp_select_active_locations ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_locations]
AS
	BEGIN
		SELECT 
			[LocationID]			
			,[UserID]				
			,[LocationName]			
			,[LocationDescription]	
			,[LocationPricingText]	
			,[LocationPhone]		
			,[LocationEmail]		
			,[LocationAddress1]		
			,[LocationAddress2]		
			,[LocationCity]			
			,[LocationState]		
			,[LocationZipCode]		
			,[LocationImagePath]	
		FROM [dbo].[Location]
		WHERE [LocationActive] = 1
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/02

Description:
Stored procedure to select a location by LocationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_location_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_locationID]
(
	@LocationID		[int]
)
AS
	BEGIN
		SELECT 
			[UserID],				
			[LocationName],			
			[LocationDescription],	
			[LocationPricingText],	
			[LocationPhone],		
			[LocationEmail],			
            [LocationAddress1],		
            [LocationAddress2],		
            [LocationCity],			
            [LocationState],			
            [LocationZipCode],		
			[LocationImagePath],		
			[LocationActive]		
		FROM [Location] 
		WHERE [LocationID] = @LocationID
	END	
GO