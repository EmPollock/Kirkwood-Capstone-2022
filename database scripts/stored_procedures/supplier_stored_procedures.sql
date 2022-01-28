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