/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
File containing the availability table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/22

Description:
Availability table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Availability Date'
CREATE TABLE [dbo].[Availability] (
	[AvailabilityID]		[int]IDENTITY(100000,1)		NOT NULL,
	[AvailableDay]			[date] 						NOT NULL,
	[AvailableTimeStart]	[time]						NOT NULL,
	[AvailableTimeEnd]		[time]						NOT NULL,

	CONSTRAINT [pk_Availability_AvailabilityID] PRIMARY KEY([AvailabilityID])
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/09
 
 Description:
 Test records for availability dates
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for Availability Dates'
GO
INSERT INTO [dbo].[Availability] (	
	[AvailableDay],		
	[AvailableTimeStart],
	[AvailableTimeEnd]
)VALUES 
	('2022-01-29', '08:30', '20:30'),
	('2022-01-30', '08:30', '20:30'),
	('2022-01-31', '08:30', '20:30'),
	('2022-04-29', '06:15', '10:45'),	
	('2022-06-01', '12:00', '20:00'),
	('2022-06-02', '12:00', '20:00')
	
GO

/***************************************************************
Austin Timmerman
Created: 2022/01/22

Description:
LocationAvailability table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating LocationAvailability Date'
CREATE TABLE [dbo].[LocationAvailability] (
	[LocationID]			[int]						NOT NULL,
	[AvailabilityID]		[int] 						NOT NULL,

	CONSTRAINT [fk_LocationAvailability_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location]([LocationID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_LocationAvailability_AvailabilityID] FOREIGN KEY([AvailabilityID])
		REFERENCES [dbo].[Availability]([AvailabilityID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/09
 
 Description:
 Test records for locationavailability dates
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for LocationAvailability Dates'
GO
INSERT INTO [dbo].[LocationAvailability] (	
	[LocationID],	
	[AvailabilityID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002),
	(100000, 100003),	
	(100000, 100004),
	(100000, 100005)
	
GO