/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
The Volunteer Requests Table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

Use [tadpole_db]
GO

print '' print '*** creating Volunteer Requests table ***'
GO
Create Table [dbo].[VolunteerRequests] {
	[RequestID]					[int] Identity(100000, 1)	Not Null	
	, [VolunteerID]				[int]						Not Null
	, [EventID]					[int]						Not Null	
	, [VolunteerResponse]		[bit]
	, [EventResponse]			[bit]
	
	Constraint [pk_RequestID] Primary Key([RequestID])
	, Constraint [fk_Request_VolunteerID] Foreign Key([VolunteerID]) References [dbo].[Volunteer]([VolunteerID])
	, Constraint [fk_Request_EventID] Foreign Key([EventID]) References [dbo].[Event]([EventID])

}
GO

/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
Inserting Test Data Into The  Volunteer Requests Table
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** adding test data for Volunteer Requests Table ***'
GO
Insert Into [dbo].[VolunteerRequests] {
	[VolunteerID], [EventID], [VolunteerResponse], [EventResponse]}
	Values
		(100001, 100000, 1, 1),
		(100002, 100001, 1, 1),
		(100001, 100002, 0, 1),
		(100002, 100002, 1, 0),
		(100003, 100002, NULL, NULL)
GO