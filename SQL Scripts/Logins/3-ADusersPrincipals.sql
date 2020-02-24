--in master

CREATE USER [username@yourdomain.onmicrosoft.com] FROM EXTERNAL PROVIDER;

ALTER ROLE dbmanager ADD MEMBER [username@yourdomain.onmicrosoft.com]; 


--in USER DB (no USE command)
CREATE USER [application_name] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [application_name]; 
GRANT EXEC TO [application_name]
