USE [tadpole_db]
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
File containing the stored procedures for events
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Stored procedure to insert an event into the events table
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_insert_event'
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(
	@EventName			nvarchar(50),
	@EventDescription	nvarchar(1000),
	@TotalBudget		money
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
		(
			[EventName],			
			[EventDescription],
			[TotalBudget]
		)
		VALUES
		(
			@EventName, 
			@EventDescription,
			@TotalBudget
		)		
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/01/23

Description:
Stored procedure to select active event from the events table
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events]
AS
	BEGIN
		SELECT 
			[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID]
		FROM [dbo].[Event]
		WHERE [Active] = 1	
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Stored procedure to select an event from the Event table by the EventTitle and EventDescription

sp_select_event_by_event_name_and_description	@EventName 	nvarchar(50)
												@EventDescription	nvarchar(1000)	
	
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_select_event_by_event_name_and_description'
GO
CREATE PROCEDURE [dbo].[sp_select_event_by_event_name_and_description] 
(
	@EventName 			nvarchar(50),
	@EventDescription	nvarchar(1000)
)
AS
	BEGIN
		SELECT 
			[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID]
		FROM [dbo].[Event]
		WHERE [EventName] = @EventName
			AND [EventDescription] = @EventDescription
			AND [Active] = 1
		ORDER BY [DateCreated] DESC
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/03

Description:
Stored procedure to update an event from the Event table

**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget fields
****************************************************************/

print '' print '*** creating sp_update_event_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_by_eventID]
(
	@EventID 				[int],
	@OldEventName			[nvarchar](50),
	@OldEventDescription	[nvarchar](1000),
	@OldTotalBudget			[money],
	@OldActive				[bit],
	@NewEventName			[nvarchar](50),
	@NewEventDescription	[nvarchar](1000),
	@NewTotalBudget			[money],
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE	[Event]
		SET		
			[EventName] = @NewEventName,
			[EventDescription] = @NewEventDescription,
			[TotalBudget] = @NewTotalBudget,
			[Active] = @NewActive
		WHERE 	
			[EventID] = @EventID
		  AND	
			@OldEventName = [EventName]
		  AND
			@OldEventDescription = [EventDescription]
		  AND
			@OldTotalBudget = [TotalBudget]
		  AND
			@OldActive = [Active]
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active events from the events table from the future and past
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_and_future_event_dates'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_and_future_event_dates]
AS
	BEGIN
		SELECT 
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID],
			[EventDate].[EventDateID]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
		WHERE [Event].[Active] = 1
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active upcoming event from the events table
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_upcoming_dates'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_upcoming_dates]
AS
	BEGIN
		SELECT 
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID],
			[EventDate].[EventDateID]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
		WHERE [Event].[Active] = 1
			AND [EventDateID] >= GETDATE()
		ORDER BY [Event].[EventID] ASC
		
	END	
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/06

Description:
Stored procedure to select active past events from the events 
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_dates'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_dates]
AS
	BEGIN
		SELECT 
			[Event].[EventID],
			[EventName],
			[EventDescription],
			[DateCreated],
			[TotalBudget],
			[LocationID],
			[EventDate].[EventDateID]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
		WHERE [Event].[Active] = 1
			AND [EventDateID] < GETDATE()
		ORDER BY [Event].[EventID] ASC
		
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active upcoming event from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_upcoming_dates_for_user'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_upcoming_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
		WHERE [Event].[Active] = 1
			AND [EventDateID] >= GETDATE()
			AND [UserEvent].[EventID] = [Event].[EventID]
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
GO



/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active past events from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_dates_for_user'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID]
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
		WHERE [Event].[Active] = 1
			AND [EventDateID] < GETDATE()
			AND [UserEvent].[EventID] = [Event].[EventID]
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
GO


/***************************************************************
Derrick Nagy
Created: 2022/02/08

Description:
Stored procedure to select active past and upcoming events from the events table for a user
**************************************************************
<Jace Pettinger>
Updated: 2022/02/15

Description: 
Adding LocationID to the selected values
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field
****************************************************************/
print '' print '*** creating sp_select_active_events_for_past_and_upcoming_dates_for_user'
GO
CREATE PROCEDURE [dbo].[sp_select_active_events_for_past_and_upcoming_dates_for_user]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[UserEvent].[EventID],
			[Event].[EventName],
			[Event].[EventDescription],
			[Event].[DateCreated],
			[Event].[TotalBudget],
			[Event].[LocationID],
			[EventDate].[EventDateID]			
		FROM [dbo].[Event]
			JOIN [dbo].[EventDate] ON [EventDate].[EventID] = [Event].[EventID]
			JOIN [dbo].[UserEvent] ON [UserEvent].[UserID] = @UserID
		WHERE [Event].[Active] = 1			
			AND [UserEvent].[EventID] = [Event].[EventID]
		ORDER BY [UserEvent].[EventID] ASC
		
	END	
	
/***************************************************************
Christopher Repko
Created: 2022/02/09

Description:
Stored procedure to update an event's location data
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_update_event_location_by_event_id ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_location_by_event_id]
(
	@EventID 				[int],
	@OldLocationID			[int],
	@LocationID				[int]
)
AS
	BEGIN
		UPDATE	[Event]
		SET		
			[LocationID] = @LocationID
		WHERE 	
			[EventID] = @EventID
		  AND	
			(
				@OldLocationID = [LocationID] OR
				(
					@OldLocationID IS NULL AND
					[LocationID] IS NULL
				)
			)
		RETURN @@ROWCOUNT
	END
GO

/***************************************************************
Derrick Nagy
Created: 2022/02/17

Description:
Stored procedure to insert an event into the events table that returns the EventID
**************************************************************
Alaina Gilson
Updated: 2022/02/22

Description: 
Added TotalBudget field. Also updated formatting
****************************************************************/
print '' print '*** creating sp_insert_event_return_event_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_return_event_id]
(
	@EventName			nvarchar(50),
	@EventDescription	nvarchar(1000),
	@TotalBudget		money
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
		(
			[EventName],				
			[EventDescription],
			[TotalBudget]
		)
		OUTPUT Inserted.EventID
		VALUES
		(
			@EventName, 
			@EventDescription,
			@TotalBudget
		)		

	END	
GO