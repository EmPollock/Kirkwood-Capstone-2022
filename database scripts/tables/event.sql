
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
	,[LocationID]		[int]						NOT NULL
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
 Derrick Nagy
 Updated: 2202/02/06

 Description: 
 Added more events
 ***************************************************************
 Derrick Nagy
 Updated: 2202/02/10

 Description: 
 Added event id to inserts
****************************************************************/

print '' print '*** test records for Event table'
GO
INSERT INTO [dbo].[Event] (
	[EventName],
	[EventDescription],	
	[LocationID]
)VALUES 
	('Scottish Highland Games', 'Event created for the Scottish Highland games in Cedar Rapids, IA',100000),
	('Clean Up the Park','An event to organize a way to clean up the local park.',100000),
	('Coachella in the Corridor','A music festival to raise money for local charities.',100000),
	('Meeting of the C-Sharpians','Convention for C# coding enthusiasts.',100000),
	('Ragbrai Stop Mason City','The event plans for Ragbrai in Mason City 2022.',100000),
	('Jazzfest','Live jazz performances and food vendors downtown Iowa City.',100000),
	('Bix7 2021','7 mile race in Davenport, Iowa ',100000),
	('Spelling Bee for the Bees','A spelling bee contest to raise money for a bee sanctuary.',100000)
GO

