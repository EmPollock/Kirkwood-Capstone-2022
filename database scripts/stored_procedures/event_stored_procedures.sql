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
Created: 2022/01/30

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

