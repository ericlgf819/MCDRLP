USE [RLPlanning]
GO

/****** Object:  Table [dbo].[Forecast_Sales]    Script Date: 07/04/2012 16:21:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Forecast_Sales](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO


