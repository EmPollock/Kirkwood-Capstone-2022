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

print '' print '*** creating sp_select_all_requests_for_event_by_eventID ***'
GO
Create PROCEDURE [dbo].[sp_select_all_requests_for_event_by_eventID]
{
	@EventID int
}
AS
	BEGIN
		SELECT 
			[RequestID],
			[VolunteerID],
			[EventID],
			[VolunteerResponse],
			[EventResponse]
		From [dbo].[VolunteerRequests]
		Where [EventID] = @EventID
		Order By [RequestID] ASC
	END
GO