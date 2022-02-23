/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
File containing the Review table
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
Review table
**************************************************************
Kris Howell
Updated: 2022/02/18

Description: 
Set Active field default to 1
****************************************************************/

print '' print '*** creating Review table'
CREATE TABLE [dbo].[Review] (
	[ReviewID]			[int] IDENTITY(100000,1)	NOT NULL,
	[UserID]			[int] 						NOT NULL,
	[ReviewType]		[nvarchar](20)				NOT NULL,
	[Rating]			[int]						NOT NULL,
	[Review]			[nvarchar](3000)			NULL,
	[DateCreated]		[DateTime]				    NOT NULL DEFAULT GETDATE(),
	[Active]			[bit]						NOT NULL DEFAULT 1,

	CONSTRAINT [pk_ReviewID] PRIMARY KEY([ReviewID]),
	CONSTRAINT [fk_Review_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID]),
	
	CHECK([Rating] > 0 AND [Rating] < 6)
)
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/02/04
 
 Description:
 Test records for Review table
***************************************************************
 Christopher Repko
 Updated: 2022/02/11
<<<<<<< HEAD

 Description: 
 Additional test records for SupplierReview join.
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd
=======
>>>>>>> origin/main

 Description: 
 Additional test records for SupplierReview join.
***************************************************************
Kris Howell
 Updated: 2022/02/18

 Description: 
 Removed insert active field, let new default to 1 handle it.
****************************************************************/
print '' print '*** test records for Review table'
GO
INSERT INTO [dbo].[Review] (					
    [UserID],		
    [ReviewType],	
    [Rating],			
    [Review]
)VALUES 
	(100000, "Location Review", 3, "Enjoyable place to visit"),
	(100000, "Supplier Review", 5, "Amazing place!"),
	(100000, "Supplier Review", 1, "Didn't like it.")
GO


/***************************************************************
Austin Timmerman
Created: 2022/02/04

Description:
LocationReview table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating LocationReview table'
CREATE TABLE [dbo].[LocationReview] (
	[ReviewID]			[int]                     	NOT NULL,
	[LocationID]		[int] 						NOT NULL,

	CONSTRAINT [fk_LocationReview_ReviewID] FOREIGN KEY([ReviewID])
        REFERENCES [Review]([ReviewID]),
	CONSTRAINT [fk_LocationReview_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [Location]([LocationID])
)
GO

/***************************************************************
 Austin Timmerman
 Created: 2022/02/04
 
 Description:
 Test records for LocationReview table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for LocationReview table'
GO
INSERT INTO [dbo].[LocationReview] (					
    [ReviewID],
    [LocationID]
)VALUES 
	(100000, 100000)
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
SupplierReview table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating SupplierReview table'
CREATE TABLE [dbo].[SupplierReview] (
	[ReviewID]			[int]                     	NOT NULL,
	[SupplierID]		[int] 						NOT NULL,

	CONSTRAINT [fk_SupplierReview_ReviewID] FOREIGN KEY([ReviewID])
        REFERENCES [Review]([ReviewID]),
	CONSTRAINT [fk_SupplierReview_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [Supplier]([SupplierID])
)
GO

/***************************************************************
 Christopher Repko
 Created: 2022/02/11
 
 Description:
 Test records for SupplierReview table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for SupplierReview table'
GO
INSERT INTO [dbo].[SupplierReview] (					
    [ReviewID],
    [SupplierID]
)VALUES 
	(100001, 100000),
	(100002, 100000)
GO
