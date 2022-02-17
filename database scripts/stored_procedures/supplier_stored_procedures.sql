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