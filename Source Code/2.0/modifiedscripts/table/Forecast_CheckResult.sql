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
	[TaskType] [int] NOT NULL,					--1��E, 0��I
	[ErrorType] [nvarchar](100) NULL,			--��������
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [nvarchar](50) NULL,
	[ContractNo] [dbo].[ObjID] NULL,
	[ContractSnapShotID] [dbo].[ObjID] NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,					--�Ƿ��Ѷ�
	[IsSolved] [bit] NOT NULL,					--�Ƿ��ѽ��
	[UserOrGroupID] [uniqueidentifier] NOT NULL,--�������û�������ID
	[FixUserID] [uniqueidentifier] NULL,		--����������û�ID
	[CalEndDate] [datetime] NOT NULL,			--Ԥ�����ʱ��
	[TaskFinishTime] [datetime] NULL,			--��������ʱ��
 CONSTRAINT [PK_Forecast_CheckResult] PRIMARY KEY CLUSTERED 
(
	[CheckID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


