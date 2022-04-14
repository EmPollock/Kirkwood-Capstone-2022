/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
File containing the service table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
Service table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Service table'
CREATE TABLE [dbo].[Service] (
	[ServiceID]				[int] IDENTITY(100000,1)	NOT NULL,
	[SupplierID]			[int]						NOT NULL,
	[ServiceName]			[nvarchar](160)				NOT NULL,
	[Price]					[decimal](10,2)				NOT NULL,
	[Description]			[nvarchar](3000)			NULL,
	[ServiceImageName]		[nvarchar](200)				NULL,

	CONSTRAINT [pk_ServiceID] PRIMARY KEY([ServiceID]),
	CONSTRAINT [fk_Service_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier] ([SupplierID]) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/03/02
 
 Description:
 Test records for service
***************************************************************
 Derrick Nagy
 Updated: 2022/04/05

 Description: 
 Added test records:
 	('100002', 'Catering', 15.00, "Catering for an event. Price is approximate price per person, minimum 10 people. Choose from menu ahead of time.", null),
	('100002', 'Food cart', 25.00, "Food cart to be available at the location. Price per hour. Food not included.", null),
	('100002', 'Food cart with food included', 200.00, "Food cart to be available at the location. Full price for food for 10 people. Avaliable for one hour.", null)
****************************************************************/
print '' print '*** test records for Service'

GO
INSERT INTO [dbo].[Service] (					
    [SupplierID],		
    [ServiceName],		
    [Price],				
    [Description],		
    [ServiceImageName]
)VALUES 
	('100000', 'Service One', 10.32, "The number one service in the world.", "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"),
	('100000', 'Service Two', 100.71, "The number two service in the world.", "7263a839-3428-49f2-b26f-875d3811ef85.jpg"),
	('100000', 'Service Three', 6.35, "The number three service in the world.", null),
	('100001', 'Service Four', 1.35, "The number four service in the world.", "7263a839-3428-49f2-b26f-875d3811ef85.jpg"),
	('100001', 'Service Five', 3.30, "The number five service in the world.", "7263a839-3428-49f2-b26f-875d3811ef85.jpg"),
	('100000', 'Service Six', 11.30, "The number six service in the world.", "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"),
	('100000', 'Service Seven', 4.10, "The number seven service in the world.", null),
	('100002', 'Catering', 15.00, "Catering for an event. Price is approximate price per person, minimum 10 people. Choose from menu ahead of time.", null),
	('100002', 'Food cart', 25.00, "Food cart to be available at the location. Price per hour. Food not included.", null),
	('100002', 'Food cart with food included', 200.00, "Food cart to be available at the location. Full price for food for 10 people. Avaliable for one hour.", null)
	
GO