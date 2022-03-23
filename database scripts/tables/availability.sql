/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
File containing the availability table
**************************************************************
Kris Howell
Updated: 2022/03/03

Description: 
Restructuring for weekly availability + exceptions
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/22

Description:
Availability table
**************************************************************
Kris Howell
Updated: 2022/03/03

Description: 
Restructuring for weekly availability
NOTE FOR INPUT VALIDATION FOR INSERT -- 
	Must validate that at least one day is selected
****************************************************************/

print '' print '*** creating Availability table'
CREATE TABLE [dbo].[Availability] (
	[AvailabilityID]		[int]IDENTITY(100000,1)		NOT NULL,
	[TimeStart]				[time]						NOT NULL,
	[TimeEnd]				[time]						NOT NULL,
	[Sunday]				[bit] 						NOT NULL,
	[Monday]				[bit] 						NOT NULL,
	[Tuesday]				[bit] 						NOT NULL,
	[Wednesday]				[bit] 						NOT NULL,
	[Thursday]				[bit] 						NOT NULL,
	[Friday]				[bit] 						NOT NULL,
	[Saturday]				[bit] 						NOT NULL,

	CONSTRAINT [pk_Availability_AvailabilityID] PRIMARY KEY([AvailabilityID])
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/09
 
 Description:
 Test records for availability dates
***************************************************************
 Kris Howell
 Updated: 2022/03/03

 Description: 
 Updating for new weekly availability structure
****************************************************************/
print '' print '*** test records for weekly Availability'
GO
INSERT INTO [dbo].[Availability] (
	[TimeStart],
	[TimeEnd],
	[Sunday],
	[Monday],
	[Tuesday],
	[Wednesday],
	[Thursday],
	[Friday],
	[Saturday]
)VALUES 
	('11:30', '21:00', 0, 1, 1, 1, 1, 0, 0),
	('11:30', '23:00', 1, 0, 0, 0, 0, 1, 1),
	('08:30', '11:30', 1, 0, 0, 0, 0, 0, 1)	
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
AvailabilityException table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating AvailabilityException table'
CREATE TABLE [dbo].[AvailabilityException] (
	[ExceptionID]			[int]IDENTITY(100000,1)		NOT NULL,
	[ExceptionDate]			[date]						NOT NULL,
	[TimeStart]				[time]						NULL,
	[TimeEnd]				[time]						NULL,

	CONSTRAINT [pk_AvailabilityException_ExceptionID] PRIMARY KEY([ExceptionID])
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for availability exception dates
****************************************************************/
print '' print '*** test records for Availability Exceptions'
GO
INSERT INTO [dbo].[AvailabilityException] (
	[ExceptionDate],
	[TimeStart],
	[TimeEnd]
)VALUES 
	('2022-03-04', '8:45', '13:00'),
	('2022-03-03', null, null),
	('2022-03-02', '06:30', '8:30'),
	('2022-03-02', '22:00', '23:00')
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

print '' print '*** creating LocationAvailability table'
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
 Kris Howell
 Updated: 2022/03/03

 Description: 
 Update test records to new availability structure
****************************************************************/
print '' print '*** test records for LocationAvailability'
GO
INSERT INTO [dbo].[LocationAvailability] (	
	[LocationID],
	[AvailabilityID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
LocationAvailabilityException table
****************************************************************/

print '' print '*** creating LocationAvailabilityException table'
CREATE TABLE [dbo].[LocationAvailabilityException] (
	[LocationID]			[int]						NOT NULL,
	[ExceptionID]			[int] 						NOT NULL,

	CONSTRAINT [fk_LocationAvailabilityException_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location]([LocationID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_LocationAvailabilityException_ExceptionID] FOREIGN KEY([ExceptionID])
		REFERENCES [dbo].[AvailabilityException]([ExceptionID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for LocationAvailabilityException dates
****************************************************************/
print '' print '*** test records for LocationAvailabilityException'
GO
INSERT INTO [dbo].[LocationAvailabilityException] (	
	[LocationID],
	[ExceptionID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002),
	(100000, 100003)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
SupplierAvailability table
****************************************************************/

print '' print '*** creating SupplierAvailability table'
CREATE TABLE [dbo].[SupplierAvailability] (
	[SupplierID]			[int]						NOT NULL,
	[AvailabilityID]		[int] 						NOT NULL,

	CONSTRAINT [fk_SupplierAvailability_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier]([SupplierID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_SupplierAvailability_AvailabilityID] FOREIGN KEY([AvailabilityID])
		REFERENCES [dbo].[Availability]([AvailabilityID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for SupplierAvailability
****************************************************************/
print '' print '*** test records for SupplierAvailability'
GO
INSERT INTO [dbo].[SupplierAvailability] (	
	[SupplierID],
	[AvailabilityID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002)
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
SupplierAvailabilityException table
****************************************************************/

print '' print '*** creating SupplierAvailabilityException table'
CREATE TABLE [dbo].[SupplierAvailabilityException] (
	[SupplierID]			[int]						NOT NULL,
	[ExceptionID]			[int] 						NOT NULL,

	CONSTRAINT [fk_SupplierAvailabilityException_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier]([SupplierID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_SupplierAvailabilityException_ExceptionID] FOREIGN KEY([ExceptionID])
		REFERENCES [dbo].[AvailabilityException]([ExceptionID])
		ON DELETE CASCADE
)
GO

/***************************************************************
 Kris Howell
 Created: 2022/03/03
 
 Description:
 Test records for SupplierAvailabilityException dates
****************************************************************/
print '' print '*** test records for SupplierAvailabilityException'
GO
INSERT INTO [dbo].[SupplierAvailabilityException] (	
	[SupplierID],
	[ExceptionID]
)VALUES 
	(100000, 100000),
	(100000, 100001),
	(100000, 100002),
	(100000, 100003)
GO