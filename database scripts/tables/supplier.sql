/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
File containing the supplier table
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
SupplierTypeID table
****************************************************************/

print '' print '*** creating SupplierType table ***'
CREATE TABLE [dbo].[SupplierType] (
	[SupplierTypeID]		[nvarchar](10)				NOT NULL
	,[Description]			[nvarchar](500)				NOT NULL

	CONSTRAINT [pk_SupplierTypeID] PRIMARY KEY([SupplierTypeID])
)
GO

/***************************************************************
Kris Howell
Created: 2022/01/27
 
Description:
Test records for supplier type table
****************************************************************/
print '' print '*** test records for SupplierType table ***'
GO
INSERT INTO [dbo].[SupplierType] (
	[SupplierTypeID]
	,[Description]
)VALUES 
	("Vendor", "Supplier looking to set up at an event and sell their goods/services")
GO



/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
Supplier table
****************************************************************/

print '' print '*** creating Supplier table ***'
CREATE TABLE [dbo].[Supplier] (
	[SupplierID]			[int] IDENTITY(100000,1)	NOT NULL
	,[UserID]				[int]						NOT NULL
	,[SupplierName]			[nvarchar](160)				NOT NULL
	,[SupplierDescription]	[nvarchar](3000)			NULL
	,[SupplierPhone]		[nvarchar](15)				NOT NULL
	,[SupplierEmail]		[nvarchar](250)				NOT NULL
	,[SupplierTypeID]		[nvarchar](10)				NOT NULL
	,[SupplierAddress1]		[nvarchar](100)				NULL
	,[SupplierAddress2]		[nvarchar](100)				NULL
	,[Active]				[bit]						NOT NULL DEFAULT 1
	

	CONSTRAINT [pk_SupplierID] PRIMARY KEY([SupplierID])
	,CONSTRAINT [fk_SupplierTypeID] FOREIGN KEY([SupplierTypeID])
		REFERENCES [SupplierType]([SupplierTypeID])
	,CONSTRAINT [fk_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
	,CONSTRAINT [ak_SupplierEmail] UNIQUE([SupplierEmail])
	,CONSTRAINT [ak_SupplierAddress1] UNIQUE([SupplierAddress1])
)
GO

/***************************************************************
Kris Howell
Created: 2022/01/27
 
Description:
Test records for supplier table
****************************************************************/
print '' print '*** test records for Supplier table ***'
GO
INSERT INTO [dbo].[Supplier] (
	[UserID]
	,[SupplierName]
	,[SupplierDescription]
	,[SupplierPhone]
	,[SupplierEmail]
	,[SupplierTypeID]
	,[SupplierAddress1]
	,[SupplierAddress2]
)VALUES 
	(100000, "McSupplier", "I'm liking it.", "999-999-9999", "mcsupplier@suppliers.com", "Vendor", "123 McSupplier Lane", null)
GO

