/***************************************************************
Emma Pollock
Created: 2022/02/03

Description:
File containing the stored procedures for Sublocations

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
Stored procedure to select a specific Sublocation
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_sublocation_by_sublocationID'
GO
CREATE PROCEDURE [dbo].[sp_select_sublocation_by_sublocationID](
	@SublocationID			[int] 
)
AS
	BEGIN
		SELECT 			
			[SublocationName]			
			,[SublocationDescription]	
			,[Active]	
			,[LocationID]			
		FROM [dbo].[Sublocation]
		WHERE [SublocationID] = @SublocationID
	END	
GO

/***************************************************************
Logan Baccam
Created: 2022/02/23

Description:
Stored procedure to insert a new sublocation by locationID

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
***************************************************************/

print '' print '*** creating sp_insert_sublocation_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_insert_sublocation_by_locationID](
	@LocationID					[int]
	,@SublocationName			[nvarchar](160)
	,@SublocationDescription	[nvarchar](1000)
)
AS
	BEGIN
		INSERT INTO [dbo].[Sublocation](
			[LocationID],
			[SublocationName],
			[SublocationDescription]
		)
		VALUES
		(
			@LocationID,
			@SublocationName,
			@SublocationDescription
		)
END 
GO	
/***************************************************************
Christopher Repko
Created: 2022/02/24

Description:
Stored procedure to select a list of sublocations linked to a location
****************************************************************/

print '' print '*** creating sp_select_sublocations_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_select_sublocations_by_locationID](
	@LocationID			[int] 
)
AS
	BEGIN
		SELECT 			
			[SublocationName]			
			,[SublocationDescription]	
			,[Active]	
			,[SublocationID]			
		FROM [dbo].[Sublocation]
		WHERE [LocationID] = @LocationID
	END	
GO
