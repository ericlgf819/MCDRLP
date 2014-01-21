USE [RLPlanning]
GO

/****** Object:  Table [dbo].[Forecast_Sales_ImportPart]    Script Date: 07/06/2012 16:30:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Forecast_Sales_ImportPart](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO


