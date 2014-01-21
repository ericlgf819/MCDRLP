USE [RLPlanning]
GO

/****** Object:  Table [dbo].[RLPlanning_Cal_TmpTbl]    Script Date: 07/20/2012 15:23:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RLPlanning_Cal_TmpTbl](
	[StoreNo] [nvarchar](50) NOT NULL,
	[KioskNo] [nvarchar](50) NULL,
	[CalEndDate] [date] NOT NULL
) ON [PRIMARY]

GO


