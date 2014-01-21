USE [RLPlanning]
GO

/****** Object:  Table [dbo].[Forecast_CheckResult]    Script Date: 07/19/2012 16:29:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Forecast_CheckResult](
	[CheckID] [dbo].[ObjID] NOT NULL,
	[TaskNo] [nvarchar](50) NOT NULL,
	[TaskType] [int] NOT NULL,					--1是E, 0是I
	[ErrorType] [nvarchar](100) NULL,			--问题类型
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [nvarchar](50) NULL,
	[ContractNo] [dbo].[ObjID] NULL,
	[ContractSnapShotID] [dbo].[ObjID] NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,					--是否已读
	[IsSolved] [bit] NOT NULL,					--是否已解决
	[UserOrGroupID] [uniqueidentifier] NOT NULL,--操作的用户或者组ID
	[FixUserID] [uniqueidentifier] NULL,		--修正错误的用户ID
	[CalEndDate] [datetime] NOT NULL,			--预测结束时间
	[TaskFinishTime] [datetime] NULL,			--任务修正时间
 CONSTRAINT [PK_Forecast_CheckResult] PRIMARY KEY CLUSTERED 
(
	[CheckID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


