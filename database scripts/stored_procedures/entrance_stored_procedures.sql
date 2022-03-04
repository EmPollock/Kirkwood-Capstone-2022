USE [tadpole_db]
GO

/***************************************************************
Alaina Gilson
Created: 2022/02/25

Description:
Stored procedure to select an entrance by location id
**************************************************************/

print '' print '*** creating sp_select_entrance_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_select_entrance_by_locationID]
(
	@LocationID		int
)
AS
	BEGIN
		SELECT 
			[EntranceID],
			[LocationID],	
			[EntranceName],	
			[Description]
		FROM [dbo].[Entrance]
		WHERE [LocationID] = @LocationID
		ORDER BY [EntranceName] DESC
	END
GO