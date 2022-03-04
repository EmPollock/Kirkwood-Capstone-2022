
	
USE [tadpole_db]
GO
-- sp_select_parking_lots_by_locationID	@LocationID	int
print '' print '*** creating sp_select_parking_lots_by_locationID'
GO
/***************************************************************
Derrick Nagy
Created: 2022/03/02

Description:
Stored procedure fo selecting parking lots by the LocationID
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
CREATE PROCEDURE [dbo].[sp_select_parking_lots_by_locationID]
(
	@LocationID 	[int]
)
AS
	BEGIN
		SELECT 
			[ParkingLotID]	
			,[ParkingLot].[LocationID]	
			,[Name]			
			,[Description]	
			,[ImageName]	
			,[Location].[LocationName]

		FROM [dbo].[ParkingLot]
			JOIN [dbo].[Location] ON [Location].[LocationID] = [ParkingLot].[LocationID]
		WHERE [Active] = 1
			AND [ParkingLot].[LocationID] = @LocationID
		ORDER BY [ParkingLotID] DESC
	END	
GO

/*
sp_insert_parking_lot	@LotID	int
	@LotName	nvarchar(160)
	@LotDescription	nvarchar(3000)
	@LotImagePath	nvarchar(200)
	@Active	bit
*/
	
/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Stored procedure to insert an event into the events table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
<Decription> 
****************************************************************/
print '' print '*** creating sp_insert_parking_lot'
GO
CREATE PROCEDURE [dbo].[sp_insert_parking_lot]
(
	@LocationID			int
	,@Name				nvarchar(160)
	,@Description		nvarchar(3000)
	,@ImageName			nvarchar(200)
)
AS
	BEGIN
		INSERT INTO [dbo].[ParkingLot] (	
			[LocationID]		
			,[Name]				
			,[Description]		
			,[ImageName]
		)
		OUTPUT Inserted.ParkingLotID
		VALUES 
		(
			@LocationID
			,@Name
			,@Description
			,@ImageName
		)
		
	END	
GO