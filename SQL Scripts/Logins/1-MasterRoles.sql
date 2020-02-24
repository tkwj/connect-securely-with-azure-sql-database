--Adding users to master roles
--To create a database, the user must be a user based on a SQL Server login in the master database 
--or contained database user based on an Azure Active Directory

--DB Creator
CREATE LOGIN DbMgmtUser WITH PASSWORD = '<strong_password>';

CREATE USER DbMgmtUser FROM LOGIN DbMgmtUser;  -- To create a SQL Server user based on a SQL Server authentication login

ALTER ROLE dbmanager ADD MEMBER DbMgmtUser; 


--Login Creator
CREATE LOGIN LoginMgmtUser WITH PASSWORD = '<strong_password>';

CREATE USER LoginMgmtUser FROM LOGIN LoginMgmtUser;  

ALTER ROLE loginmanager ADD MEMBER LoginMgmtUser; 


--Available view
select * from sys.sql_logins



CREATE LOGIN AWorker WITH PASSWORD = 'tKwjbaw333333!!!';