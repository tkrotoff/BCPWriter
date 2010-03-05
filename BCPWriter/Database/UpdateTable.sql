USE [BCPTest]
GO

/****** Object:  Table [dbo].[Table_1]    Script Date: 02/24/2010 10:44:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table_1]') AND type in (N'U'))
DROP TABLE [dbo].[Table_1]
GO

USE [BCPTest]
GO

/****** Object:  Table [dbo].[Table_1]    Script Date: 02/24/2010 10:44:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('BCPTest','U') IS NOT NULL DROP TABLE BCPTest

CREATE TABLE BCPTest (
col0 bigint,
col1 binary(50),
col2 char(10),
col3 date,
col4 datetime,
col5 datetime2,
col6 float,
col7 int,
col8 nchar(10),
col9 ntext,
col10 nvarchar(50),
col11 float,
col12 text,
col13 time,
col14 uniqueidentifier,
col15 varbinary(50),
col16 varchar(50),
col17 xml,
)

GO

INSERT INTO BCPTest VALUES (
9999999999,
CAST('binary' AS binary(50)),
'char',
'3/3/2010 12:00:00 AM',
'3/3/2010 2:52:00 PM',
'3/3/2010 2:52:00 PM',
9999999999.9,
9999,
'nchar',
'ntext',
'nvarchar',
1E+10,
'text',
'3/5/2010 2:52:00 PM',
'936da01f-9abd-4d9d-80c7-02af85c822a8',
CAST('varbinary' AS varbinary(50)),
'varchar',
'<content>XML</content>'
)
