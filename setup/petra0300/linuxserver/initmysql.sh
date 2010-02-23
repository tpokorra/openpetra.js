#!/bin/bash

echo "creating database and creating tables..."

echo "drop database if exists openpetra;" > /tmp/createtables-MySQL.sql
echo "create database if not exists openpetra;" >> /tmp/createtables-MySQL.sql
echo "use openpetra;" >> /tmp/createtables-MySQL.sql
cat db30/createtables-MySQL.sql >> /tmp/createtables-MySQL.sql
echo "GRANT SELECT,UPDATE,DELETE,INSERT ON * TO petraserver IDENTIFIED BY 'TOBESETBYINSTALLER'" >> /tmp/createtables-MySQL.sql
mysql -u root -p < /tmp/createtables-MySQL.sql
rm /tmp/createtables-MySQL.sql

echo "loading base data..."
echo "use openpetra;" > /tmp/demodata-MySQL.sql
cat db30/demodata-MySQL.sql >> /tmp/demodata-MySQL.sql
mysql -u root -p < /tmp/demodata-MySQL.sql
rm /tmp/demodata-MySQL.sql

echo "loading constraints..."
echo "use openpetra;" > /tmp/createconstraints-MySQL.sql
cat db30/createconstraints-MySQL.sql >> /tmp/createconstraints-MySQL.sql
mysql -u root -p < /tmp/createconstraints-MySQL.sql
rm /tmp/createconstraints-MySQL.sql