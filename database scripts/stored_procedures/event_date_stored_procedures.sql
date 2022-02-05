USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
File containing the stored procedures for event dates

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
Stored procedure to insert an event date into the event date table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_event_date'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_date]
(
	@EventDateID			[Date] 
	,@EventID			[int] 
	,@StartTime			[Time](0) 
	,@EndTime			[Time](0) 
)
AS
	BEGIN
		INSERT INTO [dbo].[EventDate]
		(
			[EventDateID]
			,[EventID]	
			,[StartTime] 
			,[EndTime]	 
		)
		VALUES
		(
			@EventDateID
			,@EventID
			,@StartTime
			,@EndTime
		)
	END	
GO

/*
sp_select_event_dates_by_eventID	@EventID	int	IEventDateAccessor

*/
/***************************************************************
Derrick Nagy
Created: 2022/01/30

Description:
Stored procedure to select the information about the dates for events
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_dates_by_eventID'
GO
CREATE PROCEDURE [dbo].[sp_select_event_dates_by_eventID](
	@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[EventDateID]
			,[EventID]	
			,[StartTime] 
			,[EndTime]			
		FROM [dbo].[EventDate]
		WHERE [Active] = 1	
			AND [EventID] = @EventID
	END	
GO


/***************************************************************
Emma Pollock
Created: 2022/02/02

Description:
Stored procedure to select the information about a specific date 
	for an event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_date_by_event_dateID_and_eventID'
GO
CREATE PROCEDURE [dbo].[sp_select_event_date_by_event_dateID_and_eventID](
	@EventDateID		[Date]
	,@EventID			[int] 
)
AS
	BEGIN
		SELECT 
			[StartTime] 
			,[EndTime]			
		FROM [dbo].[EventDate]
		WHERE [Active] = 1
			AND [EventDateID] = @EventDateID
			AND [EventID] = @EventID
	END	
GO