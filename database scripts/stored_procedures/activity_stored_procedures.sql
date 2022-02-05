/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
File containing the stored procedures for Activities

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
Stored procedure to select all activities for an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activities_by_eventID'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID](
	@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[SublocationID]		
			,[EventDateID]							
		FROM [dbo].[Activity]
		WHERE [EventID] = @EventID
	END	
GO