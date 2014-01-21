USE [RLPlanning]
GO

/****** Object:  Table [dbo].[RLPlanning_RealSales]    Script Date: 07/13/2012 15:08:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RLPlanning_RealSales](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO


