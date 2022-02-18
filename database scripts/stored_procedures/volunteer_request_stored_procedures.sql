USE [tadpole_db]
GO
/***************************************************************
Vinayak Deshpande
Created: 2022/01/26

Description:
Stored Procedure for viewing all volunteer requests
related to event
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_all_requests_by_eventID ***'
GO
Create PROCEDURE [dbo].[sp_select_all_requests_by_eventID]
(
	@EventID int
)
AS
	BEGIN
		SELECT 
			[RequestID],
			[VolunteerID],
			[TaskID],
			[VolunteerResponse],
			[EventResponse]
		From [dbo].[VolunteerRequest]
		Join [dbo].[Task]
		ON [VolunteerRequest].[TaskID] = [Task].[TaskID]
		Where [Task].[EventID] = @EventID
		Order By [RequestID] ASC
	END
GO

print'' print'*****AAAAAxxxxxnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnxxxxxxxxxxxx'
GO
