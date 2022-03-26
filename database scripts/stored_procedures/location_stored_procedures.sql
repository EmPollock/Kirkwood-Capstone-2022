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
		WHERE [LocationID] = @LocationID AND [LocationActive] = 1
	END	
GO


/***************************************************************
Logan Baccam
Created: 2022/01/26

Description:
Stored procedure to retrieve Locations by locationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_location_by_name_and_address'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_name_and_address]
(
	@LocationName			[nvarchar](160),
	@LocationAddress1		[nvarchar](100)
	
)
AS
	BEGIN
		SELECT 
			[LocationID],
			[LocationName],
			[LocationDescription],
			[LocationPricingText],
			[LocationPhone],
			[LocationEmail],
			[LocationAddress1],
			[LocationActive]
		FROM
			[Location]
		WHERE @LocationName = LocationName
		AND @LocationAddress1 = LocationAddress1
		AND LocationActive = 1
	END	
GO

/***************************************************************
Logan Baccam
Created: 2022/01/22

Description:
Stored procedure to create a new Location record
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_location_by_name_address_city_state_zip'
GO
CREATE PROCEDURE [dbo].[sp_insert_location_by_name_address_city_state_zip]
(
	 @LocationName			[nvarchar](160)	
	,@LocationAddress1		[nvarchar](100)	
	,@LocationCity			[nvarchar](100)
    ,@LocationState			[nvarchar](100)
	,@LocationZipCode		[nvarchar](100)
	
)
AS
	BEGIN
		INSERT INTO [dbo].[Location]
		(
		[LocationName]						
		,[LocationAddress1]
		,[LocationCity]			
		,[LocationState]	
		,[LocationZipCode]
		)
		VALUES
		(@LocationName, @LocationAddress1, @LocationCity, @LocationState, @LocationZipCode)		
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/22

Description:
Stored procedure to deactivate a location in the Location table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_deactivate_location_by_locationID***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_location_by_locationID]
(
	@LocationID 				[int]
)
AS
	BEGIN
		UPDATE	[Location]
		SET	
			[LocationActive] = 0
		WHERE
			[LocationID] = @LocationID
		RETURN @@ROWCOUNT
	END
GO