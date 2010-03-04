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
	[column1] xml NULL
) ON [PRIMARY]

GO

INSERT INTO [dbo].[Table_1]([column1]) VALUES('<holdings name="GBTRAN" date="20080502"><record abcdefghijklmn="" price="7,13" accruedint="0" coupon="" inc="" prch="" sale="" emv="918957,18" assettype="EQUITY" date="20080502" custom="TRANSACT1"/><record symbol="GB00B19NLV48" symtype="ISIN" portcode="GBTRAN" portname="EQUITIES GB" isin="GB00B19NLV48" holdings="470489" issuer="EXPERIAN GROUP LTD" ccy="GBP" ticker="EXPN.LN" price="4,01" accruedint="0" coupon="" inc="" prch="" sale="" emv="1890189,56" assettype="EQUITY" date="20080502" custom="TRANSACT1"/></holdings>')
