USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
File containing the stored procedures for suppliers
****************************************************************/

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
Stored procedure to select all active suppliers 
from the suppliers table
****************************************************************
Kris Howell
Updated: 2022/02/18

Description:
Add city, state, and zip code to select
****************************************************************/
print '' print '*** creating sp_select_active_suppliers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_suppliers]
AS
	BEGIN
		SELECT 
			[SupplierID]
			,[UserID]
			,[SupplierName]
			,[SupplierDescription]
			,[SupplierPhone]
			,[SupplierEmail]
			,[SupplierTypeID]
			,[SupplierAddress1]
			,[SupplierAddress2]
			,[SupplierCity]
			,[SupplierState]
			,[SupplierZipCode]
		FROM [dbo].[Supplier]
		WHERE [Active] = 1	
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select a supplier's tags 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_select_supplier_tags ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_tags]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[TagID]
		FROM [dbo].[SupplierTag]
		WHERE [SupplierID] = @SupplierID	
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/02/11

Description:
Stored procedure to select a supplier's images 
from the suppliers table
****************************************************************/
print '' print '*** creating sp_select_supplier_images ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_images]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[ImageName]
		FROM [dbo].[SupplierImage]
		WHERE [SupplierID] = @SupplierID	
	END	
GO



/***************************************************************
Logan Baccam
Created: 2022/04/03

Description:
stored procedure to retrieve a supplier by id
****************************************************************/
print '' print '*** creating sp_select_supplier_by_supplierID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_by_supplierID]
(
    @SupplierID     [int]
)
AS
	BEGIN
		SELECT 
			[SupplierID],
			[SupplierName],
			[SupplierDescription],
			[SupplierPhone],
			[SupplierEmail],
			[SupplierTypeID],
			[SupplierAddress1],
			[SupplierCity],
			[SupplierState]
		FROM [dbo].[Supplier]
		WHERE [SupplierID] = @SupplierID	
	END	
GO

/***************************************************************
Christopher Repko
Created: 2022/04/14

Description:
stored procedure to insert a supplier
****************************************************************/
print '' print '*** creating sp_insert_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier]
(
    @UserID     			[int]
	@SupplierName     		[nvarchar](160)	
	@SupplierDescription	[nvarchar](3000)
	@SupplierPhone     		[nvarchar](15)	
	@SupplierEmail     		[nvarchar](250)	
	@SupplierType			[nvarchar](10)
	@SupplierAddress1     	[nvarchar](100)
	@SupplierAddress2     	[nvarchar](100)
	@City     				[nvarchar](100)
	@SupplierState     		[nvarchar](100)
	@Zip	     			[nvarchar](100)
)
AS
	BEGIN
		insert into [dbo].[Supplier] 
		(
			UserID,
			SupplierName,
			SupplierDescription,
			SupplierPhone,
			SupplierEmail,
			SupplierTypeID,
			SupplierAddress1,
			SupplierAddress2,
			SupplierCity,
			SupplierState,
			SupplierZipCode
		)
		values
		(
			@UserID,
			@SupplierName,
			@SupplierDescription,
			@SupplierPhone,
			@SupplierEmail,	
			@SupplierType,
			@SupplierAddress1, 
			@SupplierAddress2,
			@City,
			@SupplierState,
			@Zip
		)
	END	
GO