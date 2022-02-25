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

/***************************************************************
Emma Pollock
Created: 2022/02/05

Description:
Stored procedure to insert a new activity record
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print'*** creating sp_insert_activity'
GO
CREATE PROCEDURE [dbo].[sp_insert_activity](
	@ActivityName			[nvarchar](50)
	,@ActivityDescription	[nvarchar](250)
	,@PublicActivity		[bit]
	,@StartTime				[time]
	,@EndTime				[time]
	,@ActivityImageName		[nvarchar](25)
	,@SublocationID			[int]
	,@EventDateID			[date]
	,@EventID				[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[Activity]
		(
			[ActivityName]			
			,[ActivityDescription]	
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[ActivityImageName]	
			,[SublocationID]		
			,[EventDateID]	
			,[EventID]
		)
		VALUES
		(
			@ActivityName
			,@ActivityDescription
			,@PublicActivity
			,@StartTime
			,@EndTime
			,@ActivityImageName
			,@SublocationID
			,@EventDateID
			,@EventID
		)		
	END	
GO

/***************************************************************
Emma Pollock
Created: 2022/02/05

Description:
Stored procedure to select all activities for a specific date
	of an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activities_by_eventID_and_event_dateID'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID_and_event_dateID](
	@EventID			[int] 
	,@EventDateID		[date]
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
		FROM [dbo].[Activity]
		WHERE [EventID] = @EventID
			AND [EventDateID] = @EventDateID
	END	
GO


/***************************************************************
Austin Timmerman
Created: 2022/02/23

Description:
Stored procedure to select all activities for a specific sublocation
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_activities_by_sublocationID'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_sublocationID](
	@SublocationID		[int] 
)
AS
	BEGIN
		SELECT 
			[ActivityID]			
			,[ActivityName]			
			,[PublicActivity]		
			,[StartTime]			
			,[EndTime]				
			,[EventDateID]
		FROM [dbo].[Activity]
		WHERE [SublocationID] = @SublocationID
	END	
GO