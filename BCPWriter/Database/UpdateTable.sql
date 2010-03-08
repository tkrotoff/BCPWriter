USE [BCPTest]
GO

IF OBJECT_ID('BCPTest','U') IS NOT NULL DROP TABLE BCPTest

CREATE TABLE BCPTest (
col0 uniqueidentifier
)

GO

INSERT INTO BCPTest VALUES
(NULL)
