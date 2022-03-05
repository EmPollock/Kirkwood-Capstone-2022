USE [tadpole_db]
GO

/***************************************************************
Alaina Gilson
Created: 2022/02/25

Description:
Stored procedure to insert an entrance into the entrance table
**************************************************************/

print '' print '*** creating sp_insert_entrance'
GO
CREATE PROCEDURE [dbo].[sp_insert_entrance]
(
	@LocationID		int,
	@EntranceName	nvarchar(100),
	@Description	nvarchar(255)
)
AS
	BEGIN
		INSERT INTO [dbo].[Entrance]
		(
			[LocationID],
			[EntranceName],
			[Description]
		)
		VALUES
		(
			@LocationID,
			@EntranceName,
			@Description
		)		
	END	
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

/***************************************************************
Alaina Gilson
Created: 2022/02/25

Description:
Stored procedure to update an entrance by entrance id
**************************************************************/

print '' print '*** creating sp_update_entrance_by_entranceID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_entrance_by_entranceID]
(
	@EntranceID 			[int],
	@OldLocationID 			[int],
	@OldEntranceName		[nvarchar](100),
	@OldDescription			[nvarchar](255),
	@NewLocationID 			[int],
	@NewEntranceName		[nvarchar](100),
	@NewDescription			[nvarchar](255)
)
AS
	BEGIN
		UPDATE [dbo].[Entrance]
		SET		
			[LocationID] = @NewLocationID,
			[EntranceName] = @NewEntranceName,
			[Description] = @NewDescription
		WHERE 	
			[EntranceID] = @EntranceID
		  AND	
			@OldLocationID = [LocationID]
		  AND
			@OldEntranceName = [EntranceName]
		  AND
			@OldDescription = [Description]
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Alaina Gilson
Created: 2022/02/25

Description:
Stored procedure to delete an entrance by entrance id
**************************************************************/

print '' print '*** creating sp_delete_entrance'
GO
CREATE PROCEDURE [dbo].[sp_delete_entrance]
(
	@EntranceID		int
)
AS
	BEGIN
		DELETE FROM [dbo].[Entrance]
		WHERE [EntranceID] = @EntranceID
	END
GO  