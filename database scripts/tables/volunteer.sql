
/***************************************************************
Austin Timmerman
Created: 2022/01/25

Description:
File containing the volunteers table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


USE [tadpole_db]
GO


/***************************************************************
Austin Timmerman
Created: 2022/01/25

Description:
Volunteers table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating Volunteers table'
CREATE TABLE [dbo].[Volunteer] (
	[VolunteerID]		[int] IDENTITY(100000,1)	NOT NULL
	,[UserID]			[int] 						NOT NULL
	,[Active]			[bit]						NOT NULL DEFAULT 1

	CONSTRAINT [pk_VolunteerID] PRIMARY KEY([VolunteerID]),
	CONSTRAINT [fk_Volunteer_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
)
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/01/25
 
 Description:
 Test records for volunteer table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for Volunteers table'
GO
INSERT INTO [dbo].[Volunteer] (
	[UserID],
	[Active]	
)VALUES 
	(100000, 1)
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/01/26
 
 Description:
 VolunteerReviews table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating VolunteerReviews table'
GO
CREATE TABLE [dbo].[VolunteerReview] (
	[ReviewID]			[int]						NOT NULL,
	[EventID]			[int] 						NOT NULL,
	[VolunteerID]		[int]						NOT NULL,
	[Rating]			[int]						NOT NULL,
	[Comments]			[nvarchar](300)				NULL,
	
	CONSTRAINT [fk_VolunteerReview_EventID] FOREIGN KEY([EventID])
		REFERENCES [Event]([EventID]),
	CONSTRAINT [fk_VolunteerReview_VolunteerID] FOREIGN KEY([VolunteerID])
		REFERENCES [Volunteer]([VolunteerID])
)
GO



/***************************************************************
 Austin Timmerman
 Created: 2022/01/26
 
 Description:
 Test records for VolunteerReview table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for VolunteerReview table'
GO
INSERT INTO [dbo].[VolunteerReview] (
	[ReviewID],
	[EventID],
	[VolunteerID],
	[Rating],
	[Comments]
)VALUES 
	(100000, 100000, 100000, 5, null),
	(100001, 100000, 100000, 5, "Did great."),
	(100002, 100001, 100000, 1, "Terrible.")
GO


/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 Role table
***************************************************************
 <Derrick Nagy>
 Updated: 2022/02/10

 Description: 
 Commented this table out of this file and created a new file called role.sql
 in this directory that contains the same code.
****************************************************************/
/*
print '' print '*** creating Role table'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]			[nvarchar](50)				NOT NULL,
	[RoleDescription]	[nvarchar](300)				NULL,
	
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO
*/


/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 Test records for Role table
***************************************************************
 <Derrick Nagy>
 Updated: 2022/02/10

 Description: 
 Commented this insert out of this file and created a new file called role.sql
 in this directory that contains the same code.
****************************************************************/
/*
print '' print '*** test records for Role table'
GO
INSERT INTO [dbo].[Role] (
	[RoleID],
	[RoleDescription]
)VALUES 
	("Open Volunteer", "Volunteer role used for any sort of volunteer work"),
	("Specific Volunteer", "Volunteer role used when a volunteer only is used for specific skills"),
	("Supply Donor", "Volunteer role used when a volunteer donates specific materials")
GO
*/

/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 VolunteerType table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** creating VolunteerType table'
GO
CREATE TABLE [dbo].[VolunteerType] (
	[VolunteerID]		[int]						NOT NULL,
	[RoleID]			[nvarchar](50) 						NOT NULL,
	
	CONSTRAINT [fk_VolunteerType_VolunteerID] FOREIGN KEY([VolunteerID])
		REFERENCES [Volunteer]([VolunteerID]),
	CONSTRAINT [fk_VolunteerType_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [Role]([RoleID])
)
GO



/***************************************************************
 Austin Timmerman
 Created: 2022/01/29
 
 Description:
 Test records for VolunteerType table
***************************************************************
 <Updater Name>
 Updated: yyyy/mm/dd

 Description: 
****************************************************************/
print '' print '*** test records for VolunteerType table'
GO
INSERT INTO [dbo].[VolunteerType] (
	[VolunteerID],
	[RoleID]
)VALUES 
	(100000, "Open Volunteer")
GO
