
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
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for Event table'
GO
INSERT INTO [dbo].[Event] (
	[EventName],
	[EventDescription],	
	[LocationID]
)VALUES 
	('Scottish Highland Games', 'Event created for the Scottish Highland games in Cedar Rapids, IA',100000),
	('Clean Up the Park','An event to organize a way to clean up the local park.', 100001),
	('Coachella in the Corridor','A music festival to raise money for local charities.', 100002)
GO


/***************************************************************
Derrick Nagy
Created: 2022/01/29

Description:
Event Date table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
/*
print '' print '*** creating Event Date'
CREATE TABLE [dbo].[EventDate] (
	[EventDateID]		[Date]						NOT NULL 
	,[EventID]			[int] 						NOT NULL
	,[StartTime]		[Time](0)					NOT NULL 
	,[EndTime]			[Time](0)					NOT NULL 
	,[Active]			[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_EventDateID_EventID] PRIMARY KEY([EventDateID],[EventID])
	, CONSTRAINT [fk_EventDate_EventID] FOREIGN KEY([EventID])
		REFERENCES [dbo].[Event]([EventID])
)
GO
*/
/***************************************************************
 Derrick Nagy
 Created: 2022/01/29
 
 Description:
 Test records for event dates
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
/*
print '' print '*** test records for Event Dates'
GO
INSERT INTO [dbo].[EventDate] (
	[EventDateID]
	,[EventID]
	,[StartTime]
	,[EndTime]
)VALUES 
	('2022-01-29', 100000,'08:30', '20:30')
	,('2022-01-30', 100000,'08:30', '20:30')
	,('2022-01-31', 100000,'08:30', '20:30')
	,('2022-04-29', 100001,'06:15', '10:45')	
	,('2022-06-01', 100002,'12:00', '11:00')
	,('2022-06-02', 100002,'12:00', '11:00')
	
GO
*/