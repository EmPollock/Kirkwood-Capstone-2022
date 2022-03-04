/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
File containing the LocationImage table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
LocationImage table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating LocationImage table'
CREATE TABLE [dbo].[LocationImage] (
	[LocationID]		[int]                    	NOT NULL,
	[ImageName]			[nvarchar](200) 			NOT NULL,

	CONSTRAINT [fk_LocationImage_LocationID] FOREIGN KEY([LocationID])
        REFERENCES [Location]([LocationID])
)
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/02/04
 
 Description:
 Test records for LocationImage table
***************************************************************
Derrick Nagy
 Updated: 2022/03/02

 Description: 
 Added then commented out images for testing
****************************************************************/
print '' print '*** test records for LocationImage table'
GO
INSERT INTO [dbo].[LocationImage] (					
    [LocationID],
    [ImageName]			
)VALUES 
	(100000, "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"),
    (100000, "7263a839-3428-49f2-b26f-875d3811ef85.jpg")
	-- added for testing purposes
	-- ,
    -- (100002, "lincolnway-park.jpg"),
    -- (100002, "lincolnway-park-street-parking.jpg"),
    -- (100003, "the-hotel-at-kirkwood.jpg"),
    -- (100003, "the-hotel-at-kirkwood-parking.jpg")
	
GO
