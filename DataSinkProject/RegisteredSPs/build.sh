#!/usr/bin/env bash

cd "/home/idan/Documents/DataSinkProject/DataSinkProject/RegisteredSPs/"

ExecSqlFile="/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Password1 -i"


$ExecSqlFile procs_misc.sql
$ExecSqlFile procs_enron.sql
$ExecSqlFile procs_test_data.sql