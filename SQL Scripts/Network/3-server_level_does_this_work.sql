--Can we change a default login?
select * from sys.sql_logins

ALTER LOGIN [mylogin] WITH DEFAULT_DATABASE = [mydatabase]

--Does this work?
select * from ufcdata.sys.database_firewall_rules;
