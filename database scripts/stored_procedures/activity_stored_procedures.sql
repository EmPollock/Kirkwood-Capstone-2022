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
			,[Activity].[SublocationID]
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
Logan Baccam
Created: 2022/02/13

Description:
Stored procedure to select all activities for past and future dates
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_activities_for_past_and_upcoming_dates'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_for_past_and_upcoming_dates]
AS
	BEGIN
		SELECT 		
			 
			[Activity].[ActivityID],
			[Activity].[ActivityName],
			[Activity].[ActivityDescription],
			[Activity].[StartTime],
			[Activity].[EndTime],
			[Activity].[ActivityImageName],
			[Sublocation].[SublocationID],
			[Sublocation].[SublocationName],
			[Activity].[EventID],
			[Activity].[EventDateID],
			[Event].[EventName]


			FROM Activity
			JOIN Sublocation ON Sublocation.SublocationID = Activity.SublocationID
			JOIN [Event] ON [Event].EventID = [Activity].[EventID]
			
			
			
			AND [Activity].[PublicActivity] = 1
			ORDER BY [Activity].[EventDateID] DESC
		
	END	
GO
/***************************************************************
Logan Baccam
Created: 2022/02/13

Description:
Stored procedure to select all event activities from the activities table for a user
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
**************************************************************** */
print '' print '*** creating sp_select_all_activities_for_user'
GO
CREATE PROCEDURE [dbo].[sp_select_all_activities_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserActivity].ActivityID
			,[Activity].[ActivityName]
			,[Activity].[ActivityDescription]
			,[Activity].[StartTime]
			,[Activity].[EndTime]
			,[Activity].[ActivityImageName]
			,[Sublocation].[SublocationID]
			,[Sublocation].[SublocationName]
			,[Event].[EventID]
			,[Activity].[EventDateID]
			,[Event].[EventName]
			,[Activity].[PublicActivity]

	FROM Activity
	JOIN [UserActivity] ON [UserActivity].[ActivityID] =[Activity].[ActivityID]
	JOIN [Event] ON [Event].[EventID] = [Activity].[EventID] 
	JOIN [Sublocation] ON [Sublocation].[SublocationID] = [Activity].[SublocationID] 
	
	AND [UserActivity].[UserID] = @UserID
	ORDER BY [Activity].[EventDateID] DESC	
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/24

Description:
Stored procedure to select all activities by sublocationID
>>>>>>> origin/main
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

/***************************************************************
Logan Baccam
Created: 2022/02/24

Description:
Stored procedure to select all activities in viewmodel for viewing an events activities
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_activities_by_eventID_for_activityvm'
GO
CREATE PROCEDURE [dbo].[sp_select_activities_by_eventID_for_activityvm](
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
			,[Activity].[SublocationID]
			,[EventDateID]		
			,[Sublocation].[SublocationName]
		FROM [dbo].[Activity]
		JOIN [Sublocation] ON [Sublocation].[SublocationID] = [Activity].[SublocationID]
		WHERE [EventID] = @EventID
	END	
GO