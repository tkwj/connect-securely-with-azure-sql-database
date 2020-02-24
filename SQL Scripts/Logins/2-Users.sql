--Create Contained User (no login)
--run this in User Database (not master)
CREATE USER Mike WITH PASSWORD = '<strong_password_here>';
ALTER ROLE db_datareader ADD MEMBER Mike; 

--Create User mapped from Login
--run this in master
CREATE LOGIN Bob WITH PASSWORD = '<strong_password_here>';
--run this in User Database (not master)
CREATE USER Bob FROM LOGIN Bob;
ALTER ROLE db_datareader ADD MEMBER Bob;


--Database Roles Members
SELECT DP1.name AS DatabaseRoleName,   
   isnull (DP2.name, 'No members') AS DatabaseUserName   
 FROM sys.database_role_members AS DRM  
 RIGHT OUTER JOIN sys.database_principals AS DP1  
   ON DRM.role_principal_id = DP1.principal_id  
 LEFT OUTER JOIN sys.database_principals AS DP2  
   ON DRM.member_principal_id = DP2.principal_id  
WHERE DP1.type = 'R'
ORDER BY DP1.name;  
