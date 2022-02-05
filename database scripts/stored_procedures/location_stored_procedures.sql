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