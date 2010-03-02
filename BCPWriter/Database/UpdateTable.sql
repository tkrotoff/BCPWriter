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

CREATE TABLE [dbo].[Table_1](
	[column] uniqueidentifier  NULL
) ON [PRIMARY]

GO

INSERT INTO [dbo].[Table_1]([column]) VALUES('6F9619FF-8B86-D011-B42D-00C04FC964FF')
