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
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_event'
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(
	@EventName			nvarchar(50)
	,@EventDescription	nvarchar(1000)
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
		(
			[EventName]				
			,[EventDescription]	
		)
		VALUES
		(@EventName, @EventDescription)		
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/01/23

Description:
Stored procedure to select active event from the events table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
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
			[DateCreated]
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
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_event_by_event_name_and_description'
GO
CREATE PROCEDURE [dbo].[sp_select_event_by_event_name_and_description] (
	@EventName 			nvarchar(50)
	,@EventDescription	nvarchar(1000)
)
AS
	BEGIN
		SELECT 
			[EventID],
			[EventName],
			[EventDescription],
			[DateCreated]
			
		FROM [dbo].[Event]
		WHERE [EventName] = @EventName
			AND [EventDescription] = @EventDescription
			AND [Active] = 1
	END	
GO

/***************************************************************
Jace Pettinger
Created: 2022/02/03

Description:
Stored procedure to update an event from the Event table

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_event_by_eventID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_by_eventID]
(
	@EventID 				[int],
	@OldEventName			[nvarchar](50),
	@OldEventDescription	[nvarchar](1000),
	@OldActive				[bit],
	@NewEventName			[nvarchar](50),
	@NewEventDescription	[nvarchar](1000),
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE	[Event]
		SET		
			[EventName] = @NewEventName,
			[EventDescription] = @NewEventDescription,
			[Active] = @NewActive
		WHERE 	
			[EventID] = @EventID
		  AND	
			@OldEventName = [EventName]
		  AND
			@OldEventDescription = [EventDescription]
		  AND
			@OldActive = [Active]
		RETURN @@ROWCOUNT
	END
GO

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