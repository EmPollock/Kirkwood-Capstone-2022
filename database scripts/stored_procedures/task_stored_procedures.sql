USE [tadpole_db]
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for selecting tasks
/ 				by TaskID
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_task_by_taskID...'
GO
CREATE PROCEDURE [dbo].[sp_select_task_by_taskID]
(
	@TaskID			[int]
)
AS
	BEGIN
	
		SELECT	[TaskID], [Name], [Task].[Description], [DueDate],
				[Priority].[Description], [CompletionDate], [Task].[Active],
				[ProofID], [IsDone], [TaskAssignmentID], [Event].[EventID]
		FROM	[dbo].[Task] JOIN [dbo].[Priority]
					ON [Task].[Priority] = [Priority].[PriorityID]
				JOIN [dbo].[Event]
					ON [Task].[EventID] = [Event].[EventID]
		WHERE	[TaskID] = @TaskID
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for selecting the
/ 				assigned user by the TaskAssignmentID
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_assigned_user_by_task_assignmentID...'
GO
CREATE PROCEDURE [dbo].[sp_select_assigned_user_by_task_assignmentID]
(
	@EventID			[int],
	@TaskID				[int],
	@TaskAssignmentID	[int]
)
AS
	BEGIN
		
		SELECT	[TaskAssignment].[UserID]
		FROM	[dbo].[TaskAssignment] JOIN [dbo].[Task]
					ON [Task].[TaskAssignmentID] = [TaskAssignment].[TaskAssignmentID]
				JOIN [dbo].[Event]
					ON [Task].[EventID] = [Event].[EventID]
		WHERE	[Task].[EventID] = @EventID
		 AND	[TaskAssignment].[TaskID] = @TaskID
		 AND	[TaskAssignment].[TaskAssignmentID] = @TaskAssignmentID
		 
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for selecting tasks by
/					the eventID
/
***************************************************************
/ <Mike Cahow>
/ Updated: 2022/01/31
/
/ Description: Updating to display TasksVM so numbers aren't 
/				the only things displayed
/
****************************************************************/

print '' print '*** creating sp_select_active_tasks_by_eventID...'
GO
CREATE PROCEDURE [dbo].[sp_select_active_tasks_by_eventID]
(
	@EventID			[int]
)
AS
	BEGIN
	
		SELECT	[TaskID], [Name], [Task].[Description], [DueDate],
				[Task].[Priority], [Priority].[Description], [Event].[EventName]
		FROM	[dbo].[Task] JOIN [dbo].[Priority]
					ON [Task].[Priority] = [Priority].[PriorityID]
				JOIN [dbo].[Event]
					ON [Task].[EventID] = [Event].[EventID]
		WHERE	[Task].[EventID] = @EventID
		 AND	[Task].[Active] = 1
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for selecting tasks by
/				their due dates
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_tasks_by_due_date...'
GO
CREATE PROCEDURE [dbo].[sp_select_active_tasks_by_due_date]
(
	@DueDate			[datetime]
)
AS
	BEGIN
	
		SELECT	[TaskID], [Name], [Task].[Description], [DueDate],
				[Priority].[Description], [CompletionDate], [Task].[Active],
				[ProofID], [IsDone], [TaskAssignmentID], [Event].[EventID]
		FROM	[dbo].[Task] JOIN [dbo].[Priority]
					ON [Task].[Priority] = [Priority].[PriorityID]
				JOIN [dbo].[Event]
					ON [Task].[EventID] = [Event].[EventID]
		WHERE	[DueDate] = @DueDate
		 AND	[Task].[Active] = 1
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for selecting tasks by
/				their level of priority
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_active_tasks_by_priority...'
GO
CREATE PROCEDURE [dbo].[sp_select_active_tasks_by_priority]
(
	@Priority			[int]
)
AS
	BEGIN
	
		SELECT	[TaskID], [Name], [Task].[Description], [DueDate],
				[Priority].[Description], [CompletionDate], [Task].[Active],
				[ProofID], [IsDone], [TaskAssignmentID], [Event].[EventName]
		FROM	[dbo].[Task] JOIN [dbo].[Priority]
					ON [Task].[Priority] = [Priority].[PriorityID]
				JOIN [dbo].[Event]
					ON [Task].[EventID] = [Event].[EventID]
		WHERE	[Task].[Priority] = @Priority
		 AND	[Task].[Active] = 1
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/23
/ 
/ Description: Creating Stored Procedure for selecting the three
/				values for Priority to put on display
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_select_all_priorities...'
GO
CREATE PROCEDURE [dbo].[sp_select_all_priorities]
AS
	BEGIN
	
		SELECT	[PriorityID], [Description]
		FROM	[dbo].[Priority]
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for updating a tasks 
/				progress by updating it's date completed
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_update_task_progress_by_completion_date...'
GO
CREATE PROCEDURE [dbo].[sp_update_task_progress_by_completion_date]
(
	@TaskID				[int],
	@CompletionDate		[datetime]
)
AS
	BEGIN
	
		UPDATE	[Task]
		SET		[CompletionDate] = @CompletionDate,
				[IsDone] = 1
		WHERE	[TaskID] = @TaskID
		 AND	[Active] = 1
		
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for creating a new task
/				 for a specific eventID
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_insert_new_task_by_eventID...'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_task_by_eventID]
(
	@EventID		[int],
	@Name			[nvarchar](50),
	@Description	[nvarchar](255),
	@DueDate		[datetime],
	@Priority		[int]
)
AS
	BEGIN
	
		INSERT INTO	[dbo].[Task]
				([Name], [Description], [DueDate],
					[Priority], [EventID])
			VALUES
				(@Name, @Description, @DueDate,
					@Priority, @EventID)
	
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/02/07
/ 
/ Description: Creating Stored Procedure for updating tasks
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print'*** creating sp_update_task'
GO
CREATE PROCEDURE [dbo].[sp_update_task]
(
	@EventID		[int],
	@TaskID			[int],
	@OldName		[nvarchar](50),
	@OldDescription	[nvarchar](255),
	@OldDueDate		[DateTime],
	@OldPriority	[int],
	@OldActive		[bit],
	@NewName		[nvarchar](50),
	@NewDescription	[nvarchar](255),
	@NewDueDate		[DateTime],
	@NewPriority	[int],
	@NewActive		[bit]
)
AS
	BEGIN
		
		UPDATE	[Task]
		SET		[Name] 			= @NewName,
				[Description]	= @NewDescription,
				[DueDate]		= @NewDueDate,
				[Priority]		= @NewPriority,
				[Active]		= @NewActive
		WHERE	[EventID]		= @EventID
		 AND	[TaskID]		= @TaskID
		 AND	[Name] 			= @OldName
		 AND	[Description]	= @OldDescription
		 AND	[DueDate]		= @OldDueDate
		 AND	[Priority]		= @OldPriority
		 AND	[Active]		= @OldActive
		 
	END
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating Stored Procedure for deactivating task
/				by the completion date
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating sp_deactivate_task_by_completion_date'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_task_by_completion_date]
(
	@TaskID			[int],
	@CompletionDate	[datetime]
)
AS
	BEGIN
	
		UPDATE	[Task]
		SET		[Active] = 0
		WHERE	[TaskID] = @TaskID
		 AND	[CompletionDate] = @CompletionDate
		 
	END
GO