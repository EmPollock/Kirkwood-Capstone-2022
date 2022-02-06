
ECHO off

sqlcmd -S localhost -E -i drop_and_create_db.sql
sqlcmd -S localhost -E -i tables/event.sql
sqlcmd -S localhost -E -i tables/event_date.sql
sqlcmd -S localhost -E -i tables/user.sql
sqlcmd -S localhost -E -i tables/supplier.sql
sqlcmd -S localhost -E -i tables/location.sql
sqlcmd -S localhost -E -i tables/volunteers.sql
sqlcmd -S localhost -E -i tables/task.sql
sqlcmd -S localhost -E -i tables/sublocation.sql
sqlcmd -S localhost -E -i tables/activity.sql
sqlcmd -S localhost -E -i tables/activity_result.sql
sqlcmd -S localhost -E -i tables/volunteer_request.sql
sqlcmd -S localhost -E -i tables/location_image.sql
sqlcmd -S localhost -E -i tables/review.sql
sqlcmd -S localhost -E -i stored_procedures/event_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/event_date_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/user_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/supplier_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/task_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/location_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/activity_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/sublocation_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/activity_result_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_request_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/location_image_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/review_stored_procedures.sql



rem server is localhost

rem list depenecies after this line:
rem task.sql requires event.sql
rem tables/event_date.sql depends on tables/event.sql
rem activity.sql requires event.sql, event_date.sql, and sublocation.sql 
rem activity_result.sql requires activity.sql
rem supplier.sql requires user.sql
rem location.sql requires user.sql

ECHO .
ECHO if no errors appear DB was created
PAUSE