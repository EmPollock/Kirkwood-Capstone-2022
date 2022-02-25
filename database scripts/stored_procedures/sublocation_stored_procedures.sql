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
Austin Timmerman
Created: 2022/02/22

Description:
Stored procedure to select sublocations by location id
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_sublocations_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_select_sublocations_by_locationID](
	@LocationID			[int] 
)
AS
	BEGIN
		SELECT 
			[SublocationID],
			[SublocationName],		
			[SublocationDescription],	
			[Active]			
		FROM [dbo].[Sublocation]
		WHERE [LocationID] = @LocationID
	END	
GO