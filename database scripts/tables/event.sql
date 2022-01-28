
/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
File containing the event table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Derrick Nagy
Created: 2022/01/22

Description:
Event table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Event table'
CREATE TABLE [dbo].[Event] (
	[EventID]			[int] IDENTITY(100000,1)	NOT NULL
	,[EventName]		[nvarchar](50)				NOT NULL
	,[EventDescription]	[nvarchar](1000)			NOT NULL
	,[DateCreated]		[DateTime]					NOT NULL DEFAULT CURRENT_TIMESTAMP 
	,[Active]			[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_EventID] PRIMARY KEY([EventID])
)
GO


/***************************************************************
 Derrick Nagy
 Created: 2022/01/22
 
 Description:
 Test records for event table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for Event table'
GO
INSERT INTO [dbo].[Event] (
	[EventName],
	[EventDescription]	
)VALUES 
	('Scottish Highland Games', 'Event created for the Scottish Highland games in Cedar Rapids, IA'),
	('Clean Up the Park','An event to organize a way to clean up the local park.'),
	('Coachella in the Corridor','A music festival to raise money for local charities.')
GO

