USE DataSinkDB
GO

/*---------------------------
VIEW_RAND
---------------------------*/
DROP VIEW IF EXISTS VIEW_RAND
GO
CREATE VIEW VIEW_RAND
AS
SELECT
  D.PkDatumId AS 'Id', RandAttrib.Value AS 'Num'

FROM
  Datum D

    INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'RandNum'
  ) AS RandAttrib ON RandAttrib.FkDatumId = D.PkDatumId

    INNER JOIN (
    SELECT  da.FkDatumId
    FROM    DatumAttrib da
    WHERE   da.Tag = 'DataSet' AND da.Value = 'RandomNumber'
  ) AS DataSet ON DataSet.FkDatumId = D.PkDatumId

GO 
  /*---------------------------
  PROC_ALL_RAND
  ---------------------------*/
  DROP PROCEDURE IF EXISTS PROC_ALL_RAND
  GO
  CREATE PROCEDURE PROC_ALL_RAND
  AS
  SELECT *
  FROM VIEW_RAND
  GO


/*---------------------------
VIEW_INC
---------------------------*/
DROP VIEW IF EXISTS VIEW_INC
GO
CREATE VIEW VIEW_INC
AS
SELECT
  D.PkDatumId AS 'Id', RandAttrib.Value AS 'Num'

FROM
  Datum D

    INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'IncNum'
  ) AS IncAttrib ON IncAttrib.FkDatumId = D.PkDatumId

    INNER JOIN (
    SELECT  da.FkDatumId
    FROM    DatumAttrib da
    WHERE   da.Tag = 'DataSet' AND da.Value = 'IncNumber'
  ) AS DataSet ON DataSet.FkDatumId = D.PkDatumId

GO
  /*---------------------------
  PROC_ALL_INC
  ---------------------------*/
  DROP PROCEDURE IF EXISTS PROC_ALL_INC
  GO
  CREATE PROCEDURE PROC_ALL_INC
  AS
  SELECT *
  FROM VIEW_RAND
  GO