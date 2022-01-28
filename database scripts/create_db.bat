

ECHO off

sqlcmd -S localhost -E -i drop_and_create_db.sql
sqlcmd -S localhost -E -i tables/event.sql
sqlcmd -S localhost -E -i stored_procedures/event_stored_procedures.sql




rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE