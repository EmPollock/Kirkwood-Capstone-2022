

REM DEPENDENCES
REM tables/event_date.sql depends on tables/event.sql


ECHO off

sqlcmd -S localhost -E -i drop_and_create_db.sql
sqlcmd -S localhost -E -i tables/event.sql
sqlcmd -S localhost -E -i tables/event_date.sql
sqlcmd -S localhost -E -i tables/user.sql
sqlcmd -S localhost -E -i tables/supplier.sql
sqlcmd -S localhost -E -i tables/task.sql
sqlcmd -S localhost -E -i stored_procedures/event_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/event_date_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/user_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/supplier_stored_procedures.sql
sqlcmd -S localhost -E -i stored_procedures/tasks_stored_procedures.sql




rem server is localhost

rem list depenecies after this line:
rem task.sql requires event.sql 

ECHO .
ECHO if no errors appear DB was created
PAUSE