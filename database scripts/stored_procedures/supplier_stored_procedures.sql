USE [tadpole_db]
GO

/***************************************************************
Kris Howell
Created: 2022/01/27

Description:
File containing the stored procedures for suppliers
****************************************************************/


/**************************************************************
Logan Baccam
created 2022/04/20
Description:
Stored procedure to insert a new supplier
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_insert_supplier ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier]
(
	@UserID					[int]
	,@SupplierName			[nvarchar](160)	
	,@SupplierDescription	[nvarchar](3000)	
	,@SupplierPhone			[nvarchar](15)	
	,@SupplierEmail			[nvarchar](250)	
	,@SupplierAddress1		[nvarchar](100)	
	,@SupplierCity			[nvarchar](100)
    ,@SupplierState			[nvarchar](100)
	,@SupplierZipCode		[nvarchar](100)
	
)
AS
	BEGIN
		INSERT INTO [dbo].[Supplier]
		(
		[UserID]
		,[SupplierName]		
		,[SupplierDescription]
		,[SupplierPhone]		
		,[SupplierEmail]	
		,[SupplierAddress1]	
		,[SupplierCity]		
		,[SupplierState]		
		,[SupplierZipCode]
		)
		VALUES
		(
		@UserID
		,@SupplierName		
		,@SupplierDescription
		,@SupplierPhone		
		,@SupplierEmail		
		,@SupplierAddress1	
		,@SupplierCity		
		,@SupplierState		
		,@SupplierZipCode
		)		
	END
	GO

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