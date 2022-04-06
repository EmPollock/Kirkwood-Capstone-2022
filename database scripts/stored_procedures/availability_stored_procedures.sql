USE [tadpole_db]
GO

/***************************************************************
Austin Timmerman
Created: 2022/02/09

Description:
File containing the stored procedures for availability

**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

/***************************************************************
Kris Howell
Created: 2022/03/03

Description: 
Select supplier availability by SupplierID and date
****************************************************************/
print '' print '*** creating sp_select_availability_by_supplierID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_by_supplierID_and_date]
(
	@SupplierID			[int],
	@AvailabilityDate	[date]
)
AS
	BEGIN
		SELECT 
			[Availability].[AvailabilityID],
			[Availability].[TimeStart],
			[Availability].[TimeEnd]
		FROM [Availability] 
		JOIN [SupplierAvailability]
			ON [Availability].[AvailabilityID] = [SupplierAvailability].[AvailabilityID]
		WHERE 
			[SupplierAvailability].[SupplierID] = @SupplierID
			AND 
			(
				   DATEPART(WEEKDAY, @AvailabilityDate) = 1 AND [Availability].[Sunday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 2 AND [Availability].[Monday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 3 AND [Availability].[Tuesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 4 AND [Availability].[Wednesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 5 AND [Availability].[Thursday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 6 AND [Availability].[Friday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 7 AND [Availability].[Saturday] = 1
			)
	END	
GO

/***************************************************************
Kris Howell
Created: 2022/03/03

Description:
Stored procedure to select supplier availability exceptions 
by supplierID and date
****************************************************************/
print '' print '*** creating sp_select_availability_exception_by_supplierID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_exception_by_supplierID_and_date]
(
	@SupplierID		[int]
	,@ExceptionDate	[date]
)
AS
	BEGIN
		SELECT
			[AvailabilityException].[ExceptionID],
			[AvailabilityException].[TimeStart],
			[AvailabilityException].[TimeEnd]
		FROM [AvailabilityException]
		JOIN [SupplierAvailabilityException] 
			ON [AvailabilityException].[ExceptionID] = [SupplierAvailabilityException].[ExceptionID]
		WHERE [SupplierAvailabilityException].[SupplierID] = @SupplierID
			AND [AvailabilityException].[ExceptionDate] = @ExceptionDate
	END
GO

/***************************************************************
Kris Howell
Created: 2022/03/10

Description: 
Select location availability by LocationID and date
****************************************************************/
print '' print '*** creating sp_select_availability_by_locationID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_by_locationID_and_date]
(
	@LocationID			[int],
	@AvailabilityDate	[date]
)
AS
	BEGIN
		SELECT 
			[Availability].[AvailabilityID],
			[Availability].[TimeStart],
			[Availability].[TimeEnd]
		FROM [Availability] 
		JOIN [LocationAvailability]
			ON [Availability].[AvailabilityID] = [LocationAvailability].[AvailabilityID]
		WHERE 
			[LocationAvailability].[LocationID] = @LocationID
			AND 
			(
				   DATEPART(WEEKDAY, @AvailabilityDate) = 1 AND [Availability].[Sunday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 2 AND [Availability].[Monday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 3 AND [Availability].[Tuesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 4 AND [Availability].[Wednesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 5 AND [Availability].[Thursday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 6 AND [Availability].[Friday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 7 AND [Availability].[Saturday] = 1
			)
	END	
GO

/***************************************************************
Kris Howell
Created: 2022/03/10

Description:
Stored procedure to select location availability exceptions 
by locationID and date
****************************************************************/
print '' print '*** creating sp_select_availability_exception_by_locationID_and_date ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_exception_by_locationID_and_date]
(
	@LocationID		[int]
	,@ExceptionDate	[date]
)
AS
	BEGIN
		SELECT
			[AvailabilityException].[ExceptionID],
			[AvailabilityException].[TimeStart],
			[AvailabilityException].[TimeEnd]
		FROM [AvailabilityException]
		JOIN [LocationAvailabilityException] 
			ON [AvailabilityException].[ExceptionID] = [LocationAvailabilityException].[ExceptionID]
		WHERE [LocationAvailabilityException].[LocationID] = @LocationID
			AND [AvailabilityException].[ExceptionDate] = @ExceptionDate
	END
GO

-----------------------------------------------------------------------------------------------------

/***************************************************************
Austin Timmerman
Created: 2022/03/29

Description: 
Select volunteer availability by VolunteerID and date
****************************************************************/
print '' print '*** creating sp_select_availability_by_volunteerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_by_volunteerID_and_date]
(
	@VolunteerID		[int],
	@AvailabilityDate	[date]
)
AS
	BEGIN
		SELECT 
			[Availability].[AvailabilityID],
			[Availability].[TimeStart],
			[Availability].[TimeEnd]
		FROM [Availability] 
		JOIN [VolunteerAvailability]
			ON [Availability].[AvailabilityID] = [VolunteerAvailability].[AvailabilityID]
		WHERE 
			[VolunteerAvailability].[VolunteerID] = @VolunteerID
			AND 
			(
				   DATEPART(WEEKDAY, @AvailabilityDate) = 1 AND [Availability].[Sunday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 2 AND [Availability].[Monday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 3 AND [Availability].[Tuesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 4 AND [Availability].[Wednesday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 5 AND [Availability].[Thursday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 6 AND [Availability].[Friday] = 1
				OR DATEPART(WEEKDAY, @AvailabilityDate) = 7 AND [Availability].[Saturday] = 1
			)
	END	
GO

/***************************************************************
Austin Timmerman
Created: 2022/03/29

Description:
Stored procedure to select volunteer availability exceptions 
by volunteerID and date
****************************************************************/
print '' print '*** creating sp_select_availability_exception_by_volunteerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_exception_by_volunteerID_and_date]
(
	@VolunteerID		[int]
	,@ExceptionDate		[date]
)
AS
	BEGIN
		SELECT
			[AvailabilityException].[ExceptionID],
			[AvailabilityException].[TimeStart],
			[AvailabilityException].[TimeEnd]
		FROM [AvailabilityException]
		JOIN [VolunteerAvailabilityException] 
			ON [AvailabilityException].[ExceptionID] = [VolunteerAvailabilityException].[ExceptionID]
		WHERE [VolunteerAvailabilityException].[VolunteerID] = @VolunteerID
			AND [AvailabilityException].[ExceptionDate] = @ExceptionDate
	END
GO