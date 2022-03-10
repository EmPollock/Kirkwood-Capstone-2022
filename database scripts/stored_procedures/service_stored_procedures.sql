/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
File containing the stored procedures for Service

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/03/02

Description:
Stored procedure to select all services for a supplier
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_services_by_supplierID'
GO
CREATE PROCEDURE [dbo].[sp_select_services_by_supplierID](
	@SupplierID			[int] 
)
AS
	BEGIN
		SELECT 
			[ServiceID],					
			[ServiceName],		
			[Price],				
			[Description],		
			[ServiceImageName]
		FROM [dbo].[Service]
		WHERE [SupplierID] = @SupplierID
	END	
GO

