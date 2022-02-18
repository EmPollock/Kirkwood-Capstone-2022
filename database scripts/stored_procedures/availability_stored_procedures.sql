USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
File containing the stored procedures for availability

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
Stored procedure to select location availability by locationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_location_availability ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_availability]
(
	@LocationID		[int] 
)
AS
	BEGIN
		SELECT 
			[LocationAvailability].[AvailabilityID], 
			[LocationAvailability].[LocationID],
			[Availability].[AvailableDay], 
			[Availability].[AvailableTimeStart], 
			[Availability].[AvailableTimeEnd] 
		FROM [Availability] 
		JOIN [LocationAvailability] ON [Availability].[AvailabilityID] = [LocationAvailability].[AvailabilityID]
		WHERE [LocationID] = @LocationID
	END	
GO