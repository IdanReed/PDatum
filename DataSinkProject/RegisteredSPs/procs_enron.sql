USE DataSinkDB
GO



/*---------------------------
VIEW_EMAILS
---------------------------*/
DROP VIEW IF EXISTS VIEW_EMAILS
GO
CREATE VIEW VIEW_EMAILS
AS
SELECT 
  D.PkDatumId AS 'EmailId', FromAttrib.Value AS 'From', ToAttrib.Value AS 'To', DateAttrib.Value AS 'Date', ServerTimeAttrib.Value AS 'TimePushed'

FROM 
  Datum D
  
  -- I know this is really inefficient/bad, but I've been having a tough time getting tables to pivot correctly
  
  INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'From'
  ) AS FromAttrib ON FromAttrib.FkDatumId = D.PkDatumId
  
  INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'To'
  ) AS ToAttrib   ON ToAttrib.FkDatumId = D.PkDatumId

  INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'Date'
  ) AS DateAttrib ON DateAttrib.FkDatumId = D.PkDatumId

  INNER JOIN (
    SELECT  da.FkDatumId, da.Value
    FROM    DatumAttrib da
    WHERE   da.Tag = 'ServerTime'
  ) AS ServerTimeAttrib ON ServerTimeAttrib.FkDatumId = D.PkDatumId

  INNER JOIN (
    SELECT  da.FkDatumId
    FROM    DatumAttrib da
    WHERE   da.Tag = 'DataSet' AND da.Value = 'EnronEmails'
  ) AS DataSet ON DataSet.FkDatumId = D.PkDatumId
GO


  /*---------------------------
  PROC_ALL_EMAILS
  ---------------------------*/
  DROP PROCEDURE IF EXISTS PROC_ALL_EMAILS
  GO
  CREATE PROCEDURE PROC_ALL_EMAILS
  AS
  SELECT VE.[From], VE.[To], VE.[Date]
  FROM VIEW_EMAILS VE
  GO
  
  /*---------------------------
  PROC_ALL_RECIPIENTS
  ---------------------------*/
  DROP PROCEDURE IF EXISTS PROC_ALL_RECIPIENTS
  GO
  CREATE PROCEDURE PROC_ALL_RECIPIENTS
  AS
  SELECT [To]
  FROM VIEW_EMAILS
  
    
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  

/*---------------------------
scratched
---------------------------*/

/*
DROP VIEW IF EXISTS EMAILS
GO
CREATE VIEW EMAILS
AS
  SELECT FkDatumId
  FROM DatumAttrib
  PIVOT
    (
      SELECT Value
      FOR Tag
      IN ([From], [To], [Date])
    )

WITH Emails_CTE (thisisid, thisistag)
AS
(
  SELECT DA.FkDatumId, (SELECT TOP 1 DA.Value WHERE DA.Tag = 'From')
  FROM DatumAttrib DA
  GROUP BY DA.FkDatumId
)
SELECT *
FROM Emails_CTE
GO
*/




/*
SELECT
D.PkDatumId AS 'EmailId', FromAttrib.Value AS 'From', ToAttrib.Value AS 'To'

FROM
Datum D

  INNER JOIN (
  SELECT  da.FkDatumId, da.Value
  FROM    DatumAttrib da
  WHERE   da.Tag = 'From'
) AS FromAttrib ON FromAttrib.FkDatumId=D.PkDatumId

  INNER JOIN (
  SELECT  da.FkDatumId, da.Value
  FROM    DatumAttrib da
  WHERE   da.Tag = 'To'
) AS ToAttrib ON ToAttrib.FkDatumId=D.PkDatumId
GO
    */

/*
(
    SELECT *
    FROM DatumAttrib DA
    WHERE DA.Tag = 'FROM') AS f ON D.PkDatumId = f.FkDatumId
    */
