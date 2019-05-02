-- noinspection SqlNoDataSourceInspectionForFile
PRINT N'create_db.sql'

/*
BUILD DB
*/
  
DROP DATABASE IF EXISTS DataSinkDB
GO
CREATE DATABASE DataSinkDB
GO
USE DataSinkDB
GO


/*
BUILD TABLES
*/


CREATE TABLE Datum
(
  PkDatumId         INT	NOT NULL IDENTITY(1, 1) PRIMARY KEY,
)
GO
CREATE TABLE DatumAttrib
(
  PkDatumAttribId   INT	NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  
  Tag               varchar(64)   NOT NULL,
  Value             varchar(512),
  
  FkDatumId         int NOT NULL FOREIGN KEY REFERENCES Datum(PkDatumId),
)