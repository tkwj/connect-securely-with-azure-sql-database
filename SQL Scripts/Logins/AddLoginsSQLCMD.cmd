sqlcmd -h-1 -S servername.database.windows.net -U username -P password -d database_name -Q "ALTER ROLE db_owner ADD MEMBER username;"
