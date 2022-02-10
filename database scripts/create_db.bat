rem server is localhost
ECHO off

sqlcmd -S localhost -E -i drop_and_create_db.sql
sqlcmd -S localhost -E -i tables/event.sql
sqlcmd -S localhost -E -i tables/event_date.sql
sqlcmd -S localhost -E -i tables/role.sql
sqlcmd -S localhost -E -i tables/user.sql
sqlcmd -S localhost -E -i tables/supplier.sql
sqlcmd -S localhost -E -i tables/location.sql
sqlcmd -S localhost -E -i tables/volunteers.sql
sqlcmd -S localhost -E -i tables/task.sql
sqlcmd -S localhost -E -i tables/volunteer_request.sql
sqlcmd -S localhost -E -i tables/user_role.sql
sqlcmd -S localhost -E -i tables/user_event.sql
sqlcmd -S localhost -E -i stored_procedures/event_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/event_date_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/user_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/supplier_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/location_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/tasks_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/volunteer_request_stored_procedures.sql

rem list depenecies after this line:
rem task.sql requires event.sql
rem supplier.sql requires user.sql
rem location.sql requires user.sql
REM tables/event_date.sql depends on tables/event.sql
REM tables/user_role depends on tables/user.sql, tables/role.sql
REM tables/user_event depends on tables/user.sql, tables/event.sql, and tables/role.sql
REM tables/volunteers.sql depends on tables/role.sql

REM PROPOSED CHANGED FOR TRACKING DEPENDENCES
:: ************************
:: FILES WHICH REQUIRE: event.sql
::	task.sql
::	event_date.sql
::  user_event.sql
::
:: ************************
:: FILES WHICH REQUIRE:  user.sql
:: 	supplier.sql
::  location.sql
::  user_role.sql
::  user_event.sql
::
:: ************************
:: FILES WHICH REQUIRE:  role.sql
::  user_role.sql
::  user_event.sql
::  volunteers.sql
::
:: ************************
:: FILES WHICH REQUIRE:  
:: 
:: 
:: 
:: ************************



ECHO .
ECHO if no errors appear DB was created
PAUSE