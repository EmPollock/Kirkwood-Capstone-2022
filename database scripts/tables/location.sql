/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
File containing the location table
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/02/03

Description:
Location table
****************************************************************/

print '' print '*** creating Location table ***'
CREATE TABLE [dbo].[Location] (
	[LocationID]			[int] IDENTITY(100000,1)	NOT NULL
	,[UserID]				[int]						NULL
	,[LocationName]			[nvarchar](160)				NOT NULL
	,[LocationDescription]	[nvarchar](3000)			NULL
	,[LocationPricingText]	[nvarchar](3000)			NULL
	,[LocationPhone]		[nvarchar](15)				NULL
	,[LocationEmail]		[nvarchar](250)				NULL
	,[LocationAddress1]		[nvarchar](100)				NOT NULL
	,[LocationAddress2]		[nvarchar](100)				NULL
	,[LocationCity]			[nvarchar](100)				NOT NULL
	,[LocationState]		[nvarchar](100)				NOT NULL
	,[LocationZipCode]		[nvarchar](100)				NOT NULL
	,[LocationImagePath]	[nvarchar](200)				NULL
	,[LocationActive]		[bit]						NOT NULL DEFAULT 1
	

	CONSTRAINT [pk_LocationID] PRIMARY KEY([LocationID])
	,CONSTRAINT [fk_UserID_Location] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
	,CONSTRAINT [ak_LocationAddress1] UNIQUE([LocationAddress1])
)
GO

/***************************************************************
Kris Howell
Created: 2022/02/03
 
Description:
Test records for location table
****************************************************************/
print '' print '*** test records for Location table ***'
GO
INSERT INTO [dbo].[Location] (
	[UserID]
	,[LocationName]
	,[LocationDescription]
	,[LocationPricingText]
	,[LocationPhone]		
	,[LocationEmail]		
	,[LocationAddress1]		
	,[LocationAddress2]		
	,[LocationCity]			
	,[LocationState]		
	,[LocationZipCode]		
	,[LocationImagePath]
)VALUES 
	(100000, "Locations R Us", "I'm a Locations R Us kid.", "5 bucks a night.", "888-888-8888", "locationsrus@locations.com", "123 Location Ave", null, "Cedar Rapids", "Iowa", "52404", "http://imagehost.com/locationsrus.png"),
	(100000, "Testy2", "This is a testy place.", "Why pay for a test?", "888-883-8888", "test@locations.com", "123 Test Ave", null, "Cedar Rapids", "Iowa", "52404", "http://imagehost.com/testy.png")
GO
