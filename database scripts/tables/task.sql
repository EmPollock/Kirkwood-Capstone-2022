USE [tadpole_db]
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the Priority table
/
***************************************************************
/ Mike Cahow
/ Updated: 2022/01/23
/
/ Description: Creating Sample Priority Records
****************************************************************/


print '' print '*** creating Priority table...'
CREATE TABLE [dbo].[Priority] (
	[PriorityID]		[int] IDENTITY(1,1)				NOT NULL,
	[Description]		[nvarchar](10)					NOT NULL,
	CONSTRAINT [pk_PriorityID] PRIMARY KEY ([PriorityID])
)
GO

print '' print '*** test records for Priority table...'
INSERT INTO [dbo].[Priority]
			([Description])
		VALUES
			('High'),
			('Medium'),
			('Low')
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description:
/ Creating Table for Task, user story 1023_Task List Create
****************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/


print '' print '*** creating Task table...'
CREATE TABLE [dbo].[Task] (
	[TaskID]			[int] IDENTITY(100000, 1)		NOT NULL,
	[Name]				[nvarchar](50)					NOT NULL,
	[Description]		[nvarchar](255)					NULL,
	[DueDate]			[datetime]						NULL
									DEFAULT GETDATE(),
	[Priority]			[int]							NOT NULL,
	[CompletionDate]	[datetime]						NULL,
	[ProofID]			[int]							NULL,
	[IsDone]			[bit]							NOT NULL
									DEFAULT 0,
	[TaskAssignmentID]	[int]							NULL,
	[EventID]			[int]							NOT NULL,
	[Active]			[bit]							NOT NULL
									DEFAULT 1,
	CONSTRAINT [fk_Priority_PriorityID] FOREIGN KEY ([Priority])
		REFERENCES [dbo].[Priority] ([PriorityID]),
	CONSTRAINT [fk_Event_EventID] FOREIGN KEY ([EventID])
		REFERENCES [dbo].[Event] ([EventID]),
	CONSTRAINT [pk_TaskID] PRIMARY KEY([TaskID])
)
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the TaskAssignment table
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/


print '' print '*** creating TaskAssignment Table...'
CREATE TABLE [dbo].[TaskAssignment] (
	[TaskAssignmentID]	[int] IDENTITY(100000, 1)		NOT NULL,
	[DateAssigned]		[datetime]						NOT NULL
									DEFAULT GETDATE(),
	[TaskID]			[int]							NOT NULL,
	[UserID]			[int]							NULL,
	[RoleID]			[int]							NULL,
	CONSTRAINT [fk_Task_TaskID] FOREIGN KEY ([TaskID])
		REFERENCES [dbo].[Task] ([TaskID]),
	CONSTRAINT [pk_TaskAssignmentID] PRIMARY KEY ([TaskAssignmentID])
)
GO

/***************************************************************
/ Mike Cahow
/ Created: 2022/01/22
/ 
/ Description: Creating the TaskSortTag table
/
***************************************************************
/ <Updater Name>
/ Updated: yyyy/mm/dd
/
/ Description: 
****************************************************************/

print '' print '*** creating TaskSortTag table...'
CREATE TABLE [dbo].[TaskSortTag] (
	[TaskSortTagID]		[int] IDENTITY(100000, 1)		NOT NULL,
	[Task]				[int]							NOT NULL,
	[Name]				[nvarchar](250)					NOT NULL,
	CONSTRAINT [fk_Task_Task] FOREIGN KEY ([Task])
		REFERENCES [dbo].[Task] ([TaskID]),
	CONSTRAINT [pk_TaskSortTagID] PRIMARY KEY ([TaskSortTagID])
)
GO


