USE [RLPlanning]
GO

/****** Object:  Table [dbo].[Import_Sales]    Script Date: 07/04/2012 15:25:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Import_Sales](
	[Company] [nvarchar](50) NULL,
	[餐厅编号] [nvarchar](50) NULL,
	[Store] [nvarchar](500) NULL,
	[Kiosk] [nvarchar](500) NULL,
	[年度] [nvarchar](50) NULL,
	[1月] [nvarchar](500) NULL,
	[2月] [nvarchar](500) NULL,
	[3月] [nvarchar](500) NULL,
	[4月] [nvarchar](500) NULL,
	[5月] [nvarchar](500) NULL,
	[6月] [nvarchar](500) NULL,
	[7月] [nvarchar](500) NULL,
	[8月] [nvarchar](500) NULL,
	[9月] [nvarchar](500) NULL,
	[10月] [nvarchar](500) NULL,
	[11月] [nvarchar](500) NULL,
	[12月] [nvarchar](500) NULL
) ON [PRIMARY]

GO


