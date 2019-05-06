#!/usr/bin/env bash

cd "/home/idan/RiderProjects/DataSinkProject/DataSinkProject/SqlFiles"

ExecSqlFile="/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Password1 -i"


$ExecSqlFile create_db.sql

