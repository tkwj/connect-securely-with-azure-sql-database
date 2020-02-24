--use the master database for these

--Create or Update a server level rule
EXECUTE sp_set_firewall_rule N'VM in remote data center', '192.0.2.10', '192.0.2.10';  

EXECUTE sp_set_firewall_rule N'azure', '0.0.0.0', '0.0.0.0';  

EXECUTE sp_set_firewall_rule N'whole internet', '0.0.0.0', '255.255.255.255';  



--View server level rules
select * from sys.firewall_rules;

--Delete server level rule

EXECUTE sp_delete_firewall_rule N'VM in remote data center';

EXECUTE sp_delete_firewall_rule N'azure';

EXECUTE sp_delete_firewall_rule N'whole internet';


--After making a server level rule change you can speed up the flushing of the server rules stored at the database level
--This needs to be run at the individual user database level, not master
DBCC FLUSHAUTHCACHE;

