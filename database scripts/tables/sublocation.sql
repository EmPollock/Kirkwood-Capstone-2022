
/***************************************************************
Emma Pollock
Created: 2022/01/31

Description:
File containing the sublocation table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Emma Pollock
Created: 2022/01/31

Description:
Sublocation table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Sublocation table'
CREATE TABLE [dbo].[Sublocation] (
	[SublocationID]				[int] IDENTITY(100000,1)	NOT NULL
	,[LocationID] 				[int]						NOT NULL
	,[SublocationName]			[nvarchar](160)				NOT NULL
	,[SublocationDescription]	[nvarchar](1000)			NULL
	,[Active]					[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_SublocationID] PRIMARY KEY([SublocationID])
	,CONSTRAINT [fk_Sublocation_Location] FOREIGN KEY([LocationID])
		REFERENCES [dbo].[Location]([LocationID]) ON UPDATE CASCADE
)
GO

/***************************************************************
 Emma Pollock
 Created: 2022/02/02
 
 Description:
 Test records for sublocation
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
***************************************************************/
print '' print '*** test records for Sublocations'
GO
INSERT INTO [dbo].[Sublocation] (
	[LocationID] 				
	,[SublocationName]			
	,[SublocationDescription]						
	
)VALUES 
	(100000 ,'Sublocation 1', 'The first sublocation')
	,(100000, 'Sublocation 2', 'The second sublocation')
	,(100000,'Sublocation 3', 'The third sublocation')
	,(100000,'Sublocation 4', 'The fourth sublocation')
	
GO