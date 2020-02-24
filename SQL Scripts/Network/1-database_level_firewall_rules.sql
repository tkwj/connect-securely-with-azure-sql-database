
--Create or Update a  database level rule
EXECUTE sp_set_database_firewall_rule N'VM in remote data center', '192.0.2.10', '192.0.2.10';  

--View database level rules
select * from sys.database_firewall_rules;

--Delete database level rule

EXECUTE sp_delete_database_firewall_rule N'VM in remote data center';
