USE [RLPlanning]
GO
/****** Object:  Table [dbo].[Import_SalesValidation]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import_SalesValidation](
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
/****** Object:  Table [dbo].[Import_SalesMessage]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import_SalesMessage](
	[CheckMessage] [nchar](1000) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Import_Sales]    Script Date: 08/16/2012 19:18:19 ******/
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
/****** Object:  Table [dbo].[Import_ContractMessage]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import_ContractMessage](
	[ExcelIndex] [nvarchar](500) NULL,
	[ContractIndex] [nvarchar](500) NULL,
	[Enflag] [nvarchar](500) NULL,
	[RelationData] [nvarchar](500) NULL,
	[CheckMessage] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Import_Contract]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import_Contract](
	[Excel行号] [nvarchar](500) NULL,
	[合同序号] [nvarchar](500) NULL,
	[合同备注] [nvarchar](500) NULL,
	[公司编号] [nvarchar](500) NULL,
	[公司名称] [nvarchar](500) NULL,
	[公司备注] [nvarchar](500) NULL,
	[业主编号] [nvarchar](500) NULL,
	[业主名称] [nvarchar](500) NULL,
	[实体类型] [nvarchar](500) NULL,
	[餐厅部门编号] [nvarchar](500) NULL,
	[实体名称] [nvarchar](500) NULL,
	[Kiosk编号] [nvarchar](500) NULL,
	[开业日] [nvarchar](500) NULL,
	[起租日] [nvarchar](500) NULL,
	[租赁到期日] [nvarchar](500) NULL,
	[是否出AP] [nvarchar](500) NULL,
	[出AP日期] [nvarchar](500) NULL,
	[地产部预估年Sales] [nvarchar](500) NULL,
	[保证金] [nvarchar](500) NULL,
	[税率] [nvarchar](500) NULL,
	[保证金备注] [nvarchar](500) NULL,
	[租金类型] [nvarchar](500) NULL,
	[是否纯百分比] [nvarchar](500) NULL,
	[摘要] [nvarchar](500) NULL,
	[备注] [nvarchar](500) NULL,
	[直线租金计算起始日] [nvarchar](500) NULL,
	[直线常数] [nvarchar](500) NULL,
	[支付类型] [nvarchar](500) NULL,
	[结算周期] [nvarchar](500) NULL,
	[租赁公历] [nvarchar](500) NULL,
	[首次DueDate] [nvarchar](500) NULL,
	[时间段开始] [nvarchar](500) NULL,
	[时间段结束] [nvarchar](500) NULL,
	[条件] [nvarchar](500) NULL,
	[公式] [nvarchar](500) NULL,
	[SQL条件] [nvarchar](500) NULL,
	[SQL公式] [nvarchar](500) NULL,
	[条件数字串] [nvarchar](500) NULL,
	[公式数字串] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_ClosePlanning]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_ClosePlanning](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CloseYear] [int] NOT NULL,
	[IsDetected] [bit] NOT NULL,
	[IsColsed] [bit] NOT NULL,
	[ClosedBy] [uniqueidentifier] NULL,
	[ClosedDate] [datetime] NULL,
 CONSTRAINT [PK_RLP_ClosePlanning] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_HistoryPassword]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_HistoryPassword](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Password] [nvarchar](32) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Department]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Department](
	[DeptCode] [nvarchar](32) NOT NULL,
	[DeptName] [nvarchar](32) NULL,
	[UpdateTime] [datetime] NULL,
	[Status] [nvarchar](8) NULL,
	[CompanyCode] [nvarchar](32) NULL,
 CONSTRAINT [PK__SRLS_TB_Basic_De__7C8480AE] PRIMARY KEY CLUSTERED 
(
	[DeptCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Company]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Company](
	[AreaID] [uniqueidentifier] NULL,
	[CompanyCode] [nvarchar](32) NOT NULL,
	[CompanyName] [nvarchar](32) NULL,
	[SimpleName] [nvarchar](16) NULL,
	[FixedSourceCode] [nvarchar](50) NULL,
	[FixedManageSourceCode] [nvarchar](50) NULL,
	[FixedServiceSourceCode] [nvarchar](50) NULL,
	[StraightLineSourceCode] [nvarchar](50) NULL,
	[RatioSourceCode] [nvarchar](50) NULL,
	[RatioManageSourceCode] [nvarchar](50) NULL,
	[RatioServiceSourceCode] [nvarchar](50) NULL,
	[ResponsibleUserID] [nvarchar](50) NULL,
	[ResponsibleEnglishName] [nvarchar](50) NULL,
	[Status] [nvarchar](8) NULL,
	[FromSRLS] [bit] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK__SRLS_TB_Basic_Co__0425A276] PRIMARY KEY CLUSTERED 
(
	[CompanyCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Area]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Area](
	[ID] [uniqueidentifier] NOT NULL,
	[AreaName] [nvarchar](32) NULL,
	[Remark] [nvarchar](512) NULL,
	[UpdateTime] [datetime] NULL,
	[ShowOrder] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sequence]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sequence](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[NextID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sequence] PRIMARY KEY NONCLUSTERED 
(
	[TableName] ASC,
	[FieldName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Backup_StoreSales]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Backup_StoreSales](
	[StoreSalesID] [varchar](36) NOT NULL,
	[StoreNo] [int] NULL,
	[Sales] [decimal](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[IsCASHSHEETClosed] [bit] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_BACKUP_STORESALES] PRIMARY KEY NONCLUSTERED 
(
	[StoreSalesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AllMonth]    Script Date: 08/16/2012 19:18:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllMonth](
	[Month] [nvarchar](2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 08/16/2012 19:18:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountNo] [nvarchar](100) NOT NULL,
	[AC_Desc] [nvarchar](2000) NULL,
	[Status] [nvarchar](50) NULL,
	[Open_Item_AC] [nvarchar](500) NULL,
	[StoreDept_Type] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY NONCLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KIOSK管理$]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KIOSK管理$](
	[Kiosk编号] [nvarchar](255) NULL,
	[名称] [nvarchar](255) NULL,
	[地址] [nvarchar](255) NULL,
	[类型] [nvarchar](255) NULL,
	[归属餐厅编号] [float] NULL,
	[是否从母店Sales中减除] [nvarchar](255) NULL,
	[状态] [nvarchar](255) NULL,
	[备注] [nvarchar](255) NULL,
	[生效日期] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KioskStoreRelationChangHistory]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KioskStoreRelationChangHistory](
	[ChangeID] [uniqueidentifier] NOT NULL,
	[KioskID] [uniqueidentifier] NOT NULL,
	[StoreNo] [nvarchar](50) NOT NULL,
	[ActiveDate] [date] NOT NULL,
	[IsNeedSubtractSalse] [bit] NOT NULL,
	[CreateUserEnglishName] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_KIOSKSTORERELATIONCHANGHIST] PRIMARY KEY NONCLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KioskSales列表$]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KioskSales列表$](
	[甜品站名称] [nvarchar](255) NULL,
	[开始日期] [datetime] NULL,
	[结束日期] [datetime] NULL,
	[SALES金额] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tr]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tr](
	[name] [varchar](50) NULL,
	[rows] [int] NULL,
	[reserved] [varchar](50) NULL,
	[data] [varchar](50) NULL,
	[index_size] [varchar](50) NULL,
	[unused] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[temp_proc_YearlyCompare2]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_proc_YearlyCompare2](
	[MyIndex] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](32) NULL,
	[AreaName] [nvarchar](32) NULL,
	[StoreOrDeptNo] [nvarchar](50) NULL,
	[KioskNo] [nvarchar](50) NULL,
	[RentType] [nvarchar](50) NULL,
	[CompareYear1] [decimal](38, 2) NULL,
	[CompareYear2] [decimal](38, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Remark]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Remark](
	[ID] [uniqueidentifier] NOT NULL,
	[SourceID] [uniqueidentifier] NULL,
	[Remark] [nvarchar](2000) NULL,
	[UserID] [uniqueidentifier] NULL,
	[EnglishName] [nvarchar](32) NULL,
	[UpdateTime] [datetime] NULL,
	[SourceType] [int] NULL,
 CONSTRAINT [PK__SRLS_TB_Master_R__1273C1CD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLPlanning_Cal_TmpTbl]    Script Date: 08/16/2012 19:18:19 ******/
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
/****** Object:  Table [dbo].[RLPlanning_Cal_TmpCompanyCodeTbl]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLPlanning_Cal_TmpCompanyCodeTbl](
	[CompanyCode] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_UserCompany]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_UserCompany](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CompanyCode] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_RLP_UserCompany] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_SynchronizationSchedule]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_SynchronizationSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SycDate] [date] NOT NULL,
	[CalculateEndDate] [date] NULL,
	[Remark] [text] NULL,
	[Status] [nvarchar](50) NOT NULL,
	[SynDetail] [text] NULL,
	[AddedBy] [uniqueidentifier] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_Tables]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_Tables](
	[ID] [int] NULL,
	[TableName] [nvarchar](64) NULL,
	[SimpleChinese] [nvarchar](32) NULL,
	[TraditionalChinese] [nvarchar](32) NULL,
	[English] [nvarchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_SerialNumber]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_SerialNumber](
	[ID] [uniqueidentifier] NULL,
	[SerialCode] [nvarchar](32) NULL,
	[SerialNumber] [int] NULL,
	[SerialRule] [nvarchar](32) NULL,
	[SerialLength] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_Login]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_Login](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[LoginTime] [datetime] NULL,
	[OutTime] [datetime] NULL,
	[HostName] [nvarchar](32) NULL,
	[IPAddress] [nvarchar](32) NULL,
 CONSTRAINT [PK_SRLS_TB_System_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_Log]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_Log](
	[ID] [uniqueidentifier] NOT NULL,
	[LogType] [nvarchar](100) NULL,
	[LogSource] [nvarchar](100) NULL,
	[LogTitle] [nvarchar](100) NULL,
	[LogMessage] [nvarchar](2048) NULL,
	[LogTime] [datetime] NULL,
	[UserID] [varchar](100) NULL,
	[EnglishName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SRLS_TB_System_DictionaryKey]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_DictionaryKey](
	[ID] [uniqueidentifier] NOT NULL,
	[KeyName] [nvarchar](32) NULL,
	[KeyValue] [nvarchar](32) NULL,
	[Description] [nvarchar](256) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_SRLS_TB_System_DictionaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_DictionaryItem]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_DictionaryItem](
	[ID] [varchar](36) NULL,
	[KeyID] [varchar](36) NULL,
	[ItemNameEnglish] [nvarchar](32) NULL,
	[ItemNameTChinese] [nvarchar](32) NULL,
	[ItemNameSChinese] [nvarchar](32) NULL,
	[ItemValue] [nvarchar](32) NULL,
	[Description] [nvarchar](2000) NULL,
	[Status] [nvarchar](50) NULL,
	[OrderIndex] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SRLS_TB_System_Columns]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_Columns](
	[ID] [int] NULL,
	[TableName] [nvarchar](64) NULL,
	[ColumnName] [nvarchar](32) NULL,
	[SimpleChinese] [nvarchar](32) NULL,
	[TraditionalChinese] [nvarchar](32) NULL,
	[English] [nvarchar](32) NULL,
	[AlwayDisplay] [int] NULL,
	[DisplayIndex] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_UserModule]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_UserModule](
	[ID] [uniqueidentifier] NOT NULL,
	[UserOrGroupID] [uniqueidentifier] NULL,
	[ModuleID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_UserFunction]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_UserFunction](
	[ID] [uniqueidentifier] NOT NULL,
	[UserOrGroupID] [uniqueidentifier] NULL,
	[FunctionID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_User]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[SystemID] [uniqueidentifier] NULL,
	[UserAccount] [nvarchar](32) NULL,
	[DisplayName] [nvarchar](32) NULL,
	[UpdateTime] [datetime] NULL,
	[IsEnable] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_System]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_System](
	[ID] [uniqueidentifier] NOT NULL,
	[SystemName] [nvarchar](32) NULL,
	[SystemCode] [nvarchar](32) NULL,
	[DBSource] [nvarchar](32) NULL,
	[DBAccount] [nvarchar](32) NULL,
	[DBPassword] [nvarchar](32) NULL,
	[DBName] [nvarchar](32) NULL,
	[DBTable] [nvarchar](32) NULL,
	[AccountField] [nvarchar](32) NULL,
	[DisplayField] [nvarchar](32) NULL,
	[Filter] [nvarchar](128) NULL,
	[Remark] [nvarchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_Module]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_Module](
	[ID] [uniqueidentifier] NOT NULL,
	[SystemID] [uniqueidentifier] NULL,
	[ModuleName] [nvarchar](32) NULL,
	[ModuleCode] [nvarchar](128) NULL,
	[SortIndex] [int] NULL,
 CONSTRAINT [PK__UUP_TB_Module__060DEAE8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_Logs]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_Logs](
	[ID] [uniqueidentifier] NOT NULL,
	[LogTitle] [nvarchar](256) NULL,
	[LogType] [nvarchar](32) NULL,
	[LogMessage] [nvarchar](2048) NULL,
	[LogTime] [datetime] NULL,
 CONSTRAINT [PK__UUP_TB_Logs__1920BF5C] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_GroupUser]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_GroupUser](
	[ID] [uniqueidentifier] NOT NULL,
	[GroupID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_Group]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_Group](
	[ID] [uniqueidentifier] NOT NULL,
	[SystemID] [uniqueidentifier] NULL,
	[GroupName] [nvarchar](32) NULL,
	[Remark] [nvarchar](512) NULL,
	[UpdateTime] [datetime] NULL,
	[IsEnable] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUP_TB_Function]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUP_TB_Function](
	[ID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[FunctionName] [nvarchar](32) NULL,
	[FunctionCode] [nvarchar](32) NULL,
	[FunctionType] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_System_UserLog]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SRLS_TB_System_UserLog](
	[ID] [dbo].[ObjID] NOT NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[StoreNo] [nvarchar](50) NULL,
	[SourceID] [nvarchar](50) NULL,
	[DataInfo] [xml] NULL,
	[TableName] [nvarchar](32) NULL,
	[LogType] [int] NULL,
	[UserID] [varchar](36) NULL,
	[EnglishName] [nvarchar](32) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SRLS_TB_SYSTEM_USERLOG] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmailList]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailList](
	[ListID] [dbo].[ObjID] NOT NULL,
	[EmailType] [dbo].[ObjName] NULL,
	[SentEmailAddress] [nvarchar](50) NULL,
	[SentorUserName] [nvarchar](50) NULL,
	[ReceiveEmailAddress] [nvarchar](500) NULL,
	[ReceiverUserName] [nvarchar](500) NULL,
	[CCEmailAddress] [nvarchar](500) NULL,
	[CCUserName] [nvarchar](500) NULL,
	[EmailTitle] [nvarchar](500) NULL,
	[EmailContent] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
	[SentTime] [datetime] NULL,
	[RelationData] [nvarchar](200) NULL,
 CONSTRAINT [PK_EMAILLIST] PRIMARY KEY NONCLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Vendor]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Vendor](
	[VendorNo] [dbo].[ObjID] NOT NULL,
	[VendorName] [nvarchar](200) NULL,
	[Flag] [nvarchar](32) NULL,
	[BlockPayMent] [nvarchar](50) NULL,
	[PayMentType] [nvarchar](50) NULL,
	[Status] [nvarchar](8) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SRLS_TB_MASTER_VENDOR] PRIMARY KEY NONCLUSTERED 
(
	[VendorNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_User]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](32) NULL,
	[Password] [nvarchar](32) NULL,
	[EcoinUserName] [nvarchar](50) NULL,
	[EnglishName] [nvarchar](32) NULL,
	[DeptCode] [nvarchar](32) NULL,
	[DeptName] [nvarchar](32) NULL,
	[GroupID] [uniqueidentifier] NULL,
	[ChineseName] [nvarchar](32) NULL,
	[ImmediateSupervisorUserID] [nvarchar](50) NULL,
	[ImmediateSupervisorEnglishName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Remark] [nvarchar](512) NULL,
	[Disabled] [bit] NULL,
	[Locked] [bit] NULL,
	[Deleted] [bit] NULL,
	[WrongTimes] [tinyint] NULL,
	[IsChangePWD] [bit] NULL,
	[PasswordUpdateDate] [datetime] NULL,
	[CheckUserID] [uniqueidentifier] NULL,
	[CheckEnglishName] [nvarchar](32) NULL,
	[ReCheckUserID] [uniqueidentifier] NULL,
	[ReCheckEnglishName] [nvarchar](32) NULL,
	[FromSRLS] [bit] NOT NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK__SRLS_TB_Basic_Us__7E6CC920] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Store]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Store](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[CompanyCode] [nvarchar](32) NULL,
	[StoreName] [nvarchar](200) NULL,
	[SimpleName] [nvarchar](50) NULL,
	[OpenDate] [date] NULL,
	[CloseDate] [date] NULL,
	[Status] [nvarchar](8) NULL,
	[SpecialStore] [nvarchar](32) NULL,
	[TempCloseDate] [nvarchar](64) NULL,
	[RentEndDate] [date] NULL,
	[UpdateTime] [datetime] NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[FromSRLS] [bit] NULL,
 CONSTRAINT [PK_SRLS_TB_MASTER_STORE] PRIMARY KEY NONCLUSTERED 
(
	[StoreNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_ClosePlanning_RealSales]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_ClosePlanning_RealSales](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_ClosePlanning_GLRecord]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_ClosePlanning_GLRecord](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[GLType] [nvarchar](100) NULL,
	[TypeCodeSnapshotID] [dbo].[ObjID] NULL,
	[TypeCodeName] [dbo].[ObjID] NULL,
	[ContractSnapshotID] [dbo].[ObjID] NULL,
	[ContractNO] [dbo].[ObjName] NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](32) NULL,
	[VendorNo] [dbo].[ObjID] NULL,
	[VendorName] [nvarchar](200) NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[EntityTypeName] [dbo].[ObjID] NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NULL,
	[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
	[RuleID] [dbo].[ObjID] NOT NULL,
	[RentType] [dbo].[ObjID] NULL,
	[AccountingCycle] [datetime] NULL,
	[IsCalculateSuccess] [bit] NULL,
	[HasMessage] [bit] NULL,
	[IsChange] [bit] NULL,
	[CycleStartDate] [date] NULL,
	[CycleEndDate] [date] NULL,
	[PayMentType] [nvarchar](50) NULL,
	[StoreEstimateSales] [decimal](18, 2) NULL,
	[KioskEstimateSales] [decimal](18, 2) NULL,
	[GLAmount] [decimal](18, 2) NULL,
	[CreatedCertificateTime] [datetime] NULL,
	[IsCASHSHEETClosed] [bit] NULL,
	[IsAjustment] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[ProcEndTime] [datetime] NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_RLP_ClosePlanning_GLRECORD] PRIMARY KEY NONCLUSTERED 
(
	[GLRecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLP_ClosePlanning_Forecast_Sales]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RLP_ClosePlanning_Forecast_Sales](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxRateInfo]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxRateInfo](
	[TaxRateInfoID] [dbo].[ObjID] NOT NULL,
	[TaxRate] [decimal](18, 6) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserID] [nvarchar](50) NULL,
	[UpdateUserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TAXRATEINFO] PRIMARY KEY NONCLUSTERED 
(
	[TaxRateInfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t](
	[AreaID] [nvarchar](50) NULL,
	[companyCode] [nvarchar](32) NULL,
	[Range] [dbo].[ObjID] NULL,
	[AreaName] [nvarchar](32) NULL,
	[CompanyName] [nvarchar](32) NULL,
	[StoreNo] [nvarchar](50) NULL,
	[KioskNo] [nvarchar](50) NULL,
	[RentType] [dbo].[ObjID] NULL,
	[GLAmount] [decimal](38, 2) NULL,
	[YearMonth] [varchar](8) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Parameters]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_Parameters](
	[ParamCode] [dbo].[ObjCode] NOT NULL,
	[ParamName] [dbo].[ObjName] NOT NULL,
	[ParamValue] [nvarchar](2000) NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[IsVisible] [bit] NULL,
 CONSTRAINT [PK_SYS_PARAMETERS] PRIMARY KEY NONCLUSTERED 
(
	[ParamCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_Operate_Log]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Operate_Log](
	[LogId] [dbo].[ObjID] NOT NULL,
	[Modules] [nvarchar](100) NULL,
	[Operate] [nvarchar](100) NULL,
	[OperateDesc] [nvarchar](2000) NULL,
	[DataKey] [nvarchar](50) NULL,
	[Operator] [nvarchar](50) NULL,
	[OperateDate] [datetime] NULL,
 CONSTRAINT [PK_SYS_OPERATE_LOG] PRIMARY KEY NONCLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'增加
   修改
   删除
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Operate_Log', @level2type=N'COLUMN',@level2name=N'Operate'
GO
/****** Object:  Table [dbo].[SYS_BizCode]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_BizCode](
	[BizName] [dbo].[ObjName] NOT NULL,
	[BizRule] [dbo].[TString_200] NOT NULL,
	[MaxValue] [dbo].[TInt32] NOT NULL,
	[Description] [dbo].[Description] NULL,
 CONSTRAINT [PK_SYS_BIZCODE] PRIMARY KEY NONCLUSTERED 
(
	[BizName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SYS_Attachments]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Attachments](
	[ID] [dbo].[ObjID] NOT NULL,
	[Category] [dbo].[TString] NOT NULL,
	[ObjectID] [dbo].[TString] NOT NULL,
	[FileName] [varchar](1024) NOT NULL,
	[FileType] [varchar](50) NOT NULL,
	[FileSize] [dbo].[TInt32] NOT NULL,
	[FilePath] [varchar](4096) NOT NULL,
	[Extend1] [dbo].[TString] NULL,
	[Extend2] [dbo].[TString] NULL,
	[Extend3] [dbo].[TString] NULL,
	[Extend4] [dbo].[TString] NULL,
	[Extend5] [dbo].[TString] NULL,
	[CreateUserID] [dbo].[ObjID] NOT NULL,
	[CreateUserName] [dbo].[ObjName] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserID] [dbo].[ObjID] NOT NULL,
	[ModifyUserName] [dbo].[ObjName] NOT NULL,
	[ModifyTime] [dbo].[TDateTime] NOT NULL,
 CONSTRAINT [PK_SYS_ATTACHMENTS] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KioskSalesTempTable]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KioskSalesTempTable](
	[TempID] [dbo].[ObjID] NOT NULL,
	[TempType] [nvarchar](200) NULL,
	[KioskID] [dbo].[ObjID] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[RelationData] [nvarchar](200) NULL,
 CONSTRAINT [PK_KIOSKSALESTEMPTABLE] PRIMARY KEY NONCLUSTERED 
(
	[TempID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CycleItem]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CycleItem](
	[CycleID] [dbo].[ObjID] NOT NULL,
	[SortIndex] [int] NULL,
	[CycleItemName] [dbo].[ObjName] NOT NULL,
	[CycleMonthCount] [int] NULL,
	[CycleType] [nvarchar](50) NULL,
 CONSTRAINT [PK_CYCLEITEM] PRIMARY KEY NONCLUSTERED 
(
	[CycleID] ASC,
	[CycleItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[ContractSnapshotID] [dbo].[ObjID] NOT NULL,
	[CompanyCode] [nvarchar](32) NULL,
	[ContractID] [dbo].[ObjID] NOT NULL,
	[ContractNO] [dbo].[ObjName] NULL,
	[Version] [nvarchar](50) NULL,
	[ContractName] [dbo].[ObjName] NULL,
	[CompanyName] [dbo].[ObjName] NULL,
	[CompanySimpleName] [nvarchar](50) NULL,
	[CompanyRemark] [nvarchar](2000) NULL,
	[Status] [nvarchar](50) NULL,
	[Remark] [nvarchar](2000) NULL,
	[UpdateInfo] [nvarchar](2000) NULL,
	[IsLocked] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserName] [nvarchar](50) NULL,
	[SnapshotCreateTime] [datetime] NULL,
	[IsSave] [bit] NULL,
	[PartComment] [dbo].[TString_1000] NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_CONTRACT] PRIMARY KEY NONCLUSTERED 
(
	[ContractSnapshotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityType]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityType](
	[EntityTypeName] [dbo].[ObjID] NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[ReTableName] [dbo].[ObjName] NULL,
	[ReKeyName] [nvarchar](50) NULL,
	[IsEnable] [bit] NOT NULL,
	[SortIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_ENTITYTYPE] PRIMARY KEY NONCLUSTERED 
(
	[EntityTypeName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APRecord]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APRecord](
	[APRecordID] [dbo].[ObjID] NOT NULL,
	[RelationAPRecordID] [dbo].[ObjID] NULL,
	[TypeCodeSnapshotID] [dbo].[ObjID] NULL,
	[TypeCodeName] [dbo].[ObjID] NULL,
	[ContractSnapshotID] [dbo].[ObjID] NULL,
	[ContractNO] [dbo].[ObjName] NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](32) NULL,
	[VendorNo] [dbo].[ObjID] NULL,
	[VendorName] [nvarchar](200) NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[EntityTypeName] [dbo].[ObjID] NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NULL,
	[RuleSnapshotID] [dbo].[ObjID] NULL,
	[RuleID] [dbo].[ObjID] NULL,
	[RentType] [dbo].[ObjID] NULL,
	[IsCalculateSuccess] [bit] NULL,
	[HasMessage] [bit] NULL,
	[IsChange] [bit] NULL,
	[DueDate] [datetime] NULL,
	[CycleStartDate] [datetime] NULL,
	[CycleEndDate] [datetime] NULL,
	[PayMentType] [nvarchar](50) NULL,
	[StoreSales] [decimal](18, 2) NULL,
	[KioskSales] [decimal](18, 2) NULL,
	[APAmount] [decimal](18, 2) NULL,
	[CreatedCertificateTime] [datetime] NULL,
	[IsCASHSHEETClosed] [bit] NULL,
	[IsAjustment] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[ProcEndTime] [datetime] NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_APRECORD] PRIMARY KEY NONCLUSTERED 
(
	[APRecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClosedRecord]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClosedRecord](
	[RecordID] [dbo].[ObjID] NOT NULL,
	[ClosedMonth] [nvarchar](50) NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_CLOSEDRECORD] PRIMARY KEY NONCLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CloseCheckResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CloseCheckResult](
	[ResultID] [dbo].[ObjID] NOT NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[CurrentMonth] [nvarchar](50) NULL,
	[EnFlag] [nvarchar](50) NULL,
	[ItemDesc] [nvarchar](1000) NULL,
	[IsPass] [bit] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CheckTime] [datetime] NULL,
 CONSTRAINT [PK_CLOSECHECKRESULT] PRIMARY KEY NONCLUSTERED 
(
	[ResultID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckItem]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckItem](
	[CheckItemID] [dbo].[ObjID] NOT NULL,
	[CheckType] [nvarchar](50) NULL,
	[OrderIndex] [int] NULL,
	[EnFlag] [nvarchar](50) NULL,
	[DetailCheckInfo] [nvarchar](2000) NULL,
	[Description] [nvarchar](500) NULL,
	[Remark] [nvarchar](2000) NULL,
 CONSTRAINT [PK_CHECKITEM] PRIMARY KEY NONCLUSTERED 
(
	[CheckItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashCloseInfo]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashCloseInfo](
	[InfoID] [dbo].[ObjID] NOT NULL,
	[ClosedMonth] [nvarchar](50) NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_CASHCLOSEINFO] PRIMARY KEY NONCLUSTERED 
(
	[InfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Backup_GLProcessParameterValue]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Backup_GLProcessParameterValue](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[ParameterName] [nvarchar](200) NULL,
	[ParameterValue] [nvarchar](500) NULL,
	[IsParameter] [bit] NULL,
	[SortIndex] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Backup_GLCertificate]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Backup_GLCertificate](
	[CertificateID] [dbo].[ObjID] NOT NULL,
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[Voucher] [nvarchar](50) NULL,
	[Co] [int] NULL,
	[AC_Mo] [nvarchar](50) NULL,
	[ScCd] [nvarchar](50) NULL,
	[TransDate] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[Site] [nvarchar](50) NULL,
	[Dept] [nvarchar](50) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[DescriptionInfo] [nvarchar](2000) NULL,
	[OpenItem] [nvarchar](500) NULL,
	[Ref1] [nvarchar](50) NULL,
	[Ref2] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Backup_EmailList]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Backup_EmailList](
	[ListID] [dbo].[ObjID] NOT NULL,
	[EmailType] [dbo].[ObjName] NULL,
	[SentEmailAddress] [nvarchar](50) NULL,
	[SentorUserName] [nvarchar](50) NULL,
	[ReceiveEmailAddress] [nvarchar](500) NULL,
	[ReceiverUserName] [nvarchar](500) NULL,
	[CCEmailAddress] [nvarchar](500) NULL,
	[CCUserName] [nvarchar](500) NULL,
	[EmailTitle] [nvarchar](500) NULL,
	[EmailContent] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
	[SentTime] [datetime] NULL,
	[RelationData] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RLPlanning_RealSales]    Script Date: 08/16/2012 19:18:19 ******/
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
/****** Object:  Table [dbo].[RentType]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentType](
	[RentTypeName] [dbo].[ObjID] NOT NULL,
	[WhichMonth] [nvarchar](50) NULL,
	[GLStartDate] [int] NULL,
	[Remark] [nvarchar](2000) NULL,
	[IsEnable] [bit] NOT NULL,
	[SortIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RENTTYPE] PRIMARY KEY NONCLUSTERED 
(
	[RentTypeName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatioTimeIntervalSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatioTimeIntervalSetting](
	[TimeIntervalID] [dbo].[ObjID] NOT NULL,
	[RuleSnapshotID] [dbo].[ObjID] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_RATIOTIMEINTERVALSETTING] PRIMARY KEY NONCLUSTERED 
(
	[TimeIntervalID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLRecord]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLRecord](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[GLType] [nvarchar](100) NULL,
	[TypeCodeSnapshotID] [dbo].[ObjID] NULL,
	[TypeCodeName] [dbo].[ObjID] NULL,
	[ContractSnapshotID] [dbo].[ObjID] NULL,
	[ContractNO] [dbo].[ObjName] NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](32) NULL,
	[VendorNo] [dbo].[ObjID] NULL,
	[VendorName] [nvarchar](200) NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[EntityTypeName] [dbo].[ObjID] NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NULL,
	[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
	[RuleID] [dbo].[ObjID] NOT NULL,
	[RentType] [dbo].[ObjID] NULL,
	[AccountingCycle] [datetime] NULL,
	[IsCalculateSuccess] [bit] NULL,
	[HasMessage] [bit] NULL,
	[IsChange] [bit] NULL,
	[CycleStartDate] [date] NULL,
	[CycleEndDate] [date] NULL,
	[PayMentType] [nvarchar](50) NULL,
	[StoreEstimateSales] [decimal](18, 2) NULL,
	[KioskEstimateSales] [decimal](18, 2) NULL,
	[GLAmount] [decimal](18, 2) NULL,
	[CreatedCertificateTime] [datetime] NULL,
	[IsCASHSHEETClosed] [bit] NULL,
	[IsAjustment] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[ProcEndTime] [datetime] NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_GLRECORD] PRIMARY KEY NONCLUSTERED 
(
	[GLRecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLAdjustment]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLAdjustment](
	[AdjustmentID] [dbo].[ObjID] NOT NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NOT NULL,
	[RuleSnapshotID] [dbo].[ObjID] NULL,
	[RuleID] [dbo].[ObjID] NULL,
	[RentType] [dbo].[ObjID] NULL,
	[AccountingCycle] [datetime] NULL,
	[AdjustmentDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
 CONSTRAINT [PK_GLADJUSTMENT] PRIMARY KEY NONCLUSTERED 
(
	[AdjustmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormulaParameter]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormulaParameter](
	[ParameterID] [dbo].[ObjID] NOT NULL,
	[ParameterName] [nvarchar](200) NULL,
	[Remark] [nvarchar](2000) NULL,
 CONSTRAINT [PK_FORMULAPARAMETER] PRIMARY KEY NONCLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forecast_Sales_ImportPart]    Script Date: 08/16/2012 19:18:19 ******/
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
/****** Object:  Table [dbo].[Forecast_Sales_BAK]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forecast_Sales_BAK](
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[SalesDate] [date] NOT NULL,
	[Sales] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forecast_Sales]    Script Date: 08/16/2012 19:18:19 ******/
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
/****** Object:  Table [dbo].[Forecast_CheckResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forecast_CheckResult](
	[CheckID] [dbo].[ObjID] NOT NULL,
	[TaskNo] [nvarchar](50) NOT NULL,
	[TaskType] [int] NOT NULL,
	[ErrorType] [nvarchar](100) NULL,
	[StoreNo] [dbo].[ObjID] NOT NULL,
	[KioskNo] [nvarchar](50) NULL,
	[ContractNo] [dbo].[ObjID] NULL,
	[ContractSnapShotID] [dbo].[ObjID] NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[IsSolved] [bit] NOT NULL,
	[UserOrGroupID] [uniqueidentifier] NOT NULL,
	[FixUserID] [uniqueidentifier] NULL,
	[CalEndDate] [datetime] NOT NULL,
	[TaskFinishTime] [datetime] NULL,
 CONSTRAINT [PK_Forecast_CheckResult] PRIMARY KEY CLUSTERED 
(
	[CheckID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLTimeIntervalInfo]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLTimeIntervalInfo](
	[InfoID] [dbo].[ObjID] NOT NULL,
	[GLRecordID] [dbo].[ObjID] NULL,
	[APRecordID] [dbo].[ObjID] NULL,
	[CycleStartDate] [datetime] NULL,
	[CycleEndDate] [datetime] NULL,
	[StoreEstimateSales] [decimal](18, 2) NULL,
	[KioskEstimateSales] [decimal](18, 2) NULL,
	[GLAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_GLTIMEINTERVALINFO] PRIMARY KEY NONCLUSTERED 
(
	[InfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLProcessParameterValue]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLProcessParameterValue](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[ParameterName] [nvarchar](200) NULL,
	[ParameterValue] [nvarchar](500) NULL,
	[IsParameter] [bit] NULL,
	[SortIndex] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLMessageResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLMessageResult](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[MessageID] [dbo].[ObjID] NOT NULL,
	[EnFlag] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsSolved] [bit] NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[CheckTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLException]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLException](
	[GLExceptionID] [dbo].[ObjID] NOT NULL,
	[GLRecordID] [dbo].[ObjID] NULL,
	[GLType] [nvarchar](100) NULL,
	[EnFlag] [nvarchar](50) NULL,
	[SettingRule] [nvarchar](1000) NULL,
	[DisplayValue] [nvarchar](1000) NULL,
	[ValueItem] [nvarchar](1000) NULL,
	[PreMonthValue] [decimal](18, 2) NULL,
	[ThisMonthValue] [decimal](18, 2) NULL,
	[IsMeetRule] [bit] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_GLEXCEPTION] PRIMARY KEY NONCLUSTERED 
(
	[GLExceptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLCheckResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLCheckResult](
	[GLRecordID] [dbo].[ObjID] NOT NULL,
	[CheckResultID] [dbo].[ObjID] NOT NULL,
	[EnFlag] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsSolved] [bit] NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[CheckTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GLCertificate]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLCertificate](
	[CertificateID] [dbo].[ObjID] NOT NULL,
	[GLRecordID] [dbo].[ObjID] NULL,
	[Voucher] [nvarchar](50) NULL,
	[Co] [int] NULL,
	[AC_Mo] [nvarchar](50) NULL,
	[ScCd] [nvarchar](50) NULL,
	[TransDate] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[Site] [nvarchar](50) NULL,
	[Dept] [nvarchar](50) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[DescriptionInfo] [nvarchar](2000) NULL,
	[OpenItem] [nvarchar](500) NULL,
	[Ref1] [nvarchar](50) NULL,
	[Ref2] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_GLCERTIFICATE] PRIMARY KEY NONCLUSTERED 
(
	[CertificateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APStoreUpCloseSalse]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APStoreUpCloseSalse](
	[APStoreUpCloseSalseID] [dbo].[ObjID] NOT NULL,
	[APRecordID] [dbo].[ObjID] NULL,
	[StoreNo] [int] NULL,
	[Sales] [decimal](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_APSTOREUPCLOSESALSE] PRIMARY KEY NONCLUSTERED 
(
	[APStoreUpCloseSalseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APProcessParameterValue]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APProcessParameterValue](
	[APRecordID] [dbo].[ObjID] NOT NULL,
	[ParameterName] [nvarchar](200) NULL,
	[ParameterValue] [nvarchar](500) NULL,
	[IsParameter] [bit] NULL,
	[SortIndex] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APMessageResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APMessageResult](
	[MessageID] [dbo].[ObjID] NOT NULL,
	[APRecordID] [dbo].[ObjID] NOT NULL,
	[EnFlag] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsSolved] [bit] NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[CheckTime] [datetime] NOT NULL,
 CONSTRAINT [PK_APMESSAGERESULT] PRIMARY KEY NONCLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APCheckResult]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APCheckResult](
	[CheckResultID] [dbo].[ObjID] NOT NULL,
	[APRecordID] [dbo].[ObjID] NOT NULL,
	[EnFlag] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsSolved] [bit] NOT NULL,
	[Remark] [nvarchar](2000) NULL,
	[CheckTime] [datetime] NOT NULL,
 CONSTRAINT [PK_APCHECKRESULT] PRIMARY KEY NONCLUSTERED 
(
	[CheckResultID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APCertificate]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APCertificate](
	[CertificateID] [dbo].[ObjID] NOT NULL,
	[APRecordID] [dbo].[ObjID] NULL,
	[MSIS_G_Voucher] [nvarchar](50) NULL,
	[CoNumber] [int] NULL,
	[VendorNumber] [nvarchar](32) NULL,
	[VendorName] [nvarchar](32) NULL,
	[InvoiceDigits] [nvarchar](50) NULL,
	[FaPiaoFlag] [nvarchar](50) NULL,
	[DescriptionInfo] [nvarchar](2000) NULL,
	[InvoiceDate] [nvarchar](50) NULL,
	[InvoiceAmount] [decimal](18, 2) NULL,
	[Quantity] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[TotalGrossAmount] [decimal](18, 2) NULL,
	[InventoryDescription] [nvarchar](2000) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[Site] [nvarchar](50) NULL,
	[Dep] [nvarchar](50) NULL,
	[OpenItem] [nvarchar](500) NULL,
	[DescInfo] [nvarchar](1000) NULL,
	[Ref1] [nvarchar](50) NULL,
	[Ref2] [nvarchar](50) NULL,
	[InputAmount] [decimal](18, 2) NULL,
	[UserID] [nvarchar](50) NULL,
	[ApprCode] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Pre_Approve] [nvarchar](50) NULL,
	[Imagelink] [nvarchar](200) NULL,
	[ImageSend] [nvarchar](50) NULL,
	[BarCode] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_APCERTIFICATE] PRIMARY KEY NONCLUSTERED 
(
	[CertificateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[EntityID] [dbo].[ObjID] NOT NULL,
	[EntityTypeName] [dbo].[ObjID] NOT NULL,
	[ContractSnapshotID] [dbo].[ObjID] NOT NULL,
	[EntityNo] [dbo].[ObjID] NULL,
	[EntityName] [dbo].[ObjName] NOT NULL,
	[StoreOrDept] [nvarchar](50) NULL,
	[StoreOrDeptNo] [nvarchar](50) NULL,
	[KioskNo] [nvarchar](50) NULL,
	[OpeningDate] [date] NULL,
	[RentStartDate] [date] NULL,
	[RentEndDate] [date] NULL,
	[IsCalculateAP] [bit] NULL,
	[APStartDate] [date] NULL,
	[Remark] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ENTITY] PRIMARY KEY NONCLUSTERED 
(
	[EntityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionAmount]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionAmount](
	[ConditionID] [dbo].[ObjID] NOT NULL,
	[TimeIntervalID] [dbo].[ObjID] NULL,
	[ConditionDesc] [nvarchar](1000) NULL,
	[AmountFormulaDesc] [nvarchar](1000) NULL,
	[SQLCondition] [nvarchar](1000) NULL,
	[SQLAmountFormula] [nvarchar](1000) NULL,
 CONSTRAINT [PK_CONDITIONAMOUNT] PRIMARY KEY NONCLUSTERED 
(
	[ConditionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreSales]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StoreSales](
	[StoreSalesID] [varchar](36) NOT NULL,
	[StoreNo] [dbo].[ObjID] NULL,
	[Sales] [decimal](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[IsCASHSHEETClosed] [bit] NULL,
	[UpdateTime] [datetime] NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_STORESALES] PRIMARY KEY NONCLUSTERED 
(
	[StoreSalesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SRLS_TB_Master_Kiosk]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SRLS_TB_Master_Kiosk](
	[KioskID] [dbo].[ObjID] NOT NULL,
	[StoreNo] [dbo].[ObjID] NULL,
	[KioskNo] [dbo].[ObjID] NULL,
	[TemStoreNo] [dbo].[ObjID] NULL,
	[ActiveDate] [date] NULL,
	[KioskName] [dbo].[ObjName] NULL,
	[SimpleName] [nvarchar](16) NULL,
	[Address] [nvarchar](500) NULL,
	[KioskType] [nvarchar](50) NULL,
	[Description] [nvarchar](512) NULL,
	[OpenDate] [date] NULL,
	[CloseDate] [date] NULL,
	[IsEnable] [bit] NULL,
	[IsLocked] [bit] NULL,
	[Status] [nvarchar](50) NULL,
	[IsNeedSubtractSalse] [bit] NULL,
	[PartComment] [dbo].[TString_1000] NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserName] [nvarchar](50) NULL,
	[FromSRLS] [bit] NULL,
 CONSTRAINT [PK_SRLS_TB_MASTER_KIOSK] PRIMARY KEY NONCLUSTERED 
(
	[KioskID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeCode]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeCode](
	[TypeCodeSnapshotID] [dbo].[ObjID] NOT NULL,
	[RentTypeName] [dbo].[ObjID] NOT NULL,
	[EntityTypeName] [dbo].[ObjID] NOT NULL,
	[TypeCodeName] [dbo].[ObjID] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[InvoicePrefix] [nvarchar](50) NULL,
	[Description] [nvarchar](2000) NULL,
	[UpdateInfo] [nvarchar](2000) NULL,
	[SortIndex] [int] NULL,
	[YTGLDebit] [nvarchar](100) NULL,
	[YTGLCredit] [nvarchar](100) NULL,
	[YTAPNormal] [nvarchar](100) NULL,
	[YTAPDifferences] [nvarchar](100) NULL,
	[YTRemark] [nvarchar](2000) NULL,
	[YFGLDebit] [nvarchar](100) NULL,
	[YFGLCredit] [nvarchar](100) NULL,
	[YFAPNormal] [nvarchar](100) NULL,
	[YFAPDifferences] [nvarchar](100) NULL,
	[YFRemak] [nvarchar](2000) NULL,
	[BFGLDebit] [nvarchar](100) NULL,
	[BFGLCredit] [nvarchar](100) NULL,
	[BFRemak] [nvarchar](2000) NULL,
	[ZXGLDebit] [nvarchar](100) NULL,
	[ZXGLCredit] [nvarchar](100) NULL,
	[ZXRemark] [nvarchar](2000) NULL,
	[IsLocked] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreatorName] [nvarchar](50) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserName] [nvarchar](50) NULL,
	[IsSave] [bit] NULL,
	[PartComment] [dbo].[TString_1000] NULL,
	[SnapshotCreateTime] [datetime] NULL,
 CONSTRAINT [PK_TYPECODE] PRIMARY KEY NONCLUSTERED 
(
	[TypeCodeSnapshotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorContract]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorContract](
	[VendorContractID] [dbo].[ObjID] NOT NULL,
	[ContractSnapshotID] [dbo].[ObjID] NULL,
	[VendorNo] [dbo].[ObjID] NOT NULL,
	[VendorName] [nvarchar](200) NULL,
	[PayMentType] [nvarchar](50) NULL,
	[IsVirtual] [bit] NOT NULL,
 CONSTRAINT [PK_VENDORCONTRACT] PRIMARY KEY NONCLUSTERED 
(
	[VendorContractID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorEntity]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorEntity](
	[VendorEntityID] [dbo].[ObjID] NOT NULL,
	[EntityID] [dbo].[ObjID] NULL,
	[VendorNo] [dbo].[ObjID] NOT NULL,
 CONSTRAINT [PK_VENDORENTITY] PRIMARY KEY NONCLUSTERED 
(
	[VendorEntityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KioskSalesCollection]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KioskSalesCollection](
	[CollectionID] [dbo].[ObjID] NOT NULL,
	[KioskID] [dbo].[ObjID] NULL,
	[WorkflowRelationID] [nvarchar](50) NULL,
	[Sales] [decimal](18, 2) NULL,
	[ZoneStartDate] [datetime] NULL,
	[ZoneEndDate] [datetime] NULL,
	[Remark] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
	[InputSalseUserEnglishName] [nvarchar](50) NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_KIOSKSALESCOLLECTION] PRIMARY KEY NONCLUSTERED 
(
	[CollectionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KioskSales]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KioskSales](
	[KioskSalesID] [varchar](36) NOT NULL,
	[KioskID] [dbo].[ObjID] NULL,
	[CollectionID] [dbo].[ObjID] NULL,
	[Sales] [decimal](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[ZoneStartDate] [datetime] NULL,
	[ZoneEndDate] [datetime] NULL,
	[IsAjustment] [bit] NULL,
	[Remark] [nvarchar](512) NULL,
	[CreateTime] [datetime] NULL,
	[InputSalseUserEnglishName] [nvarchar](50) NULL,
	[FromSRLS] [bit] NOT NULL,
 CONSTRAINT [PK_KIOSKSALES] PRIMARY KEY NONCLUSTERED 
(
	[KioskSalesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConditionAmountNumbers]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionAmountNumbers](
	[NumberID] [dbo].[ObjID] NOT NULL,
	[ConditionID] [dbo].[ObjID] NULL,
	[NumberValue] [decimal](18, 2) NULL,
	[NumberType] [nvarchar](50) NULL,
 CONSTRAINT [PK_CONDITIONAMOUNTNUMBERS] PRIMARY KEY NONCLUSTERED 
(
	[NumberID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityInfoSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityInfoSetting](
	[EntityInfoSettingID] [dbo].[ObjID] NOT NULL,
	[EntityID] [dbo].[ObjID] NOT NULL,
	[VendorNo] [dbo].[ObjID] NOT NULL,
	[RealestateSales] [decimal](18, 2) NULL,
	[MarginAmount] [decimal](18, 2) NULL,
	[MarginRemark] [nvarchar](2000) NULL,
	[TaxRate] [decimal](12, 8) NULL,
 CONSTRAINT [PK_ENTITYINFOSETTING] PRIMARY KEY NONCLUSTERED 
(
	[EntityInfoSettingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FixedRuleSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FixedRuleSetting](
	[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NULL,
	[RuleID] [dbo].[ObjID] NOT NULL,
	[RentType] [dbo].[ObjID] NOT NULL,
	[FirstDueDate] [datetime] NULL,
	[NextDueDate] [datetime] NULL,
	[NextAPStartDate] [datetime] NULL,
	[NextAPEndDate] [datetime] NULL,
	[NextGLStartDate] [datetime] NULL,
	[NextGLEndDate] [datetime] NULL,
	[PayType] [dbo].[ObjName] NULL,
	[ZXStartDate] [datetime] NULL,
	[ZXConstant] [decimal](18, 2) NULL,
	[Cycle] [dbo].[ObjName] NULL,
	[CycleMonthCount] [int] NULL,
	[Calendar] [dbo].[ObjName] NULL,
	[Description] [nvarchar](2000) NULL,
	[Remark] [nvarchar](2000) NULL,
	[SnapshotCreateTime] [datetime] NULL,
 CONSTRAINT [PK_FIXEDRULESETTING] PRIMARY KEY NONCLUSTERED 
(
	[RuleSnapshotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatioRuleSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatioRuleSetting](
	[RatioID] [dbo].[ObjID] NOT NULL,
	[EntityInfoSettingID] [dbo].[ObjID] NULL,
	[RentType] [dbo].[ObjID] NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[Remark] [nvarchar](2000) NULL,
 CONSTRAINT [PK_RATIORULESETTING] PRIMARY KEY NONCLUSTERED 
(
	[RatioID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FixedTimeIntervalSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FixedTimeIntervalSetting](
	[TimeIntervalID] [dbo].[ObjID] NOT NULL,
	[RuleSnapshotID] [dbo].[ObjID] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Cycle] [dbo].[ObjName] NULL,
	[CycleMonthCount] [int] NULL,
	[Calendar] [dbo].[ObjName] NULL,
 CONSTRAINT [PK_FIXEDTIMEINTERVALSETTING] PRIMARY KEY NONCLUSTERED 
(
	[TimeIntervalID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatioCycleSetting]    Script Date: 08/16/2012 19:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatioCycleSetting](
	[RatioID] [dbo].[ObjID] NULL,
	[RuleSnapshotID] [dbo].[ObjID] NOT NULL,
	[RuleID] [dbo].[ObjID] NOT NULL,
	[IsPure] [bit] NULL,
	[FirstDueDate] [datetime] NULL,
	[NextDueDate] [datetime] NULL,
	[NextAPStartDate] [datetime] NULL,
	[NextAPEndDate] [datetime] NULL,
	[NextGLStartDate] [datetime] NULL,
	[NextGLEndDate] [datetime] NULL,
	[PayType] [dbo].[ObjName] NULL,
	[ZXStartDate] [datetime] NULL,
	[Cycle] [dbo].[ObjName] NULL,
	[CycleMonthCount] [int] NULL,
	[Calendar] [dbo].[ObjName] NULL,
	[CycleType] [nvarchar](50) NULL,
	[SnapshotCreateTime] [datetime] NULL,
 CONSTRAINT [PK_RATIOCYCLESETTING] PRIMARY KEY NONCLUSTERED 
(
	[RuleSnapshotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__APRecord__FromSR__7B670A9A]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APRecord] ADD  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF__ClosedRec__FromS__40A56C28]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[ClosedRecord] ADD  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF__Contract__FromSR__6517D6C8]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[Contract] ADD  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF__GLRecord__FromSR__2F7AE026]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLRecord] ADD  CONSTRAINT [DF__GLRecord__FromSR__2F7AE026]  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF__KioskSale__FromS__0D25C822]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[KioskSalesCollection] ADD  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_KioskStoreRelationChangHistory_IsNeedSubtractSalse]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[KioskStoreRelationChangHistory] ADD  CONSTRAINT [DF_KioskStoreRelationChangHistory_IsNeedSubtractSalse]  DEFAULT ((0)) FOR [IsNeedSubtractSalse]
GO
/****** Object:  Default [DF_KioskStoreRelationChangHistory_CreateTime]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[KioskStoreRelationChangHistory] ADD  CONSTRAINT [DF_KioskStoreRelationChangHistory_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_RLP_ClosePlanning_IsDetected]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_ClosePlanning] ADD  CONSTRAINT [DF_RLP_ClosePlanning_IsDetected]  DEFAULT ((0)) FOR [IsDetected]
GO
/****** Object:  Default [DF_RLP_ClosePlanning_IsColsed]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_ClosePlanning] ADD  CONSTRAINT [DF_RLP_ClosePlanning_IsColsed]  DEFAULT ((0)) FOR [IsColsed]
GO
/****** Object:  Default [DF__RLP_ClosePlanning_GLRecord__FromSR__2F7AE026]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_ClosePlanning_GLRecord] ADD  CONSTRAINT [DF__RLP_ClosePlanning_GLRecord__FromSR__2F7AE026]  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_RLP_SynchronizationSchedule_Status]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_SynchronizationSchedule] ADD  CONSTRAINT [DF_RLP_SynchronizationSchedule_Status]  DEFAULT ('未启动') FOR [Status]
GO
/****** Object:  Default [DF_RLP_SynchronizationSchedule_AddedDate]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_SynchronizationSchedule] ADD  CONSTRAINT [DF_RLP_SynchronizationSchedule_AddedDate]  DEFAULT (getdate()) FOR [AddedDate]
GO
/****** Object:  Default [DF_RLP_SynchronizationSchedule_LastModifiedDate]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RLP_SynchronizationSchedule] ADD  CONSTRAINT [DF_RLP_SynchronizationSchedule_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
/****** Object:  Default [DF_SRLS_TB_Master_Company_FromSRLS]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_Company] ADD  CONSTRAINT [DF_SRLS_TB_Master_Company_FromSRLS]  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_SRLS_TB_Master_Kiosk_FromSRLS]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_Kiosk] ADD  CONSTRAINT [DF_SRLS_TB_Master_Kiosk_FromSRLS]  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_SRLS_TB_Master_Store_FromSRLS]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_Store] ADD  CONSTRAINT [DF_SRLS_TB_Master_Store_FromSRLS]  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_SRLS_TB_Master_User_FromSRLS]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_User] ADD  CONSTRAINT [DF_SRLS_TB_Master_User_FromSRLS]  DEFAULT ((0)) FOR [FromSRLS]
GO
/****** Object:  Default [DF_SRLS_TB_System_Columns_AlwayDisplay]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_System_Columns] ADD  CONSTRAINT [DF_SRLS_TB_System_Columns_AlwayDisplay]  DEFAULT ((0)) FOR [AlwayDisplay]
GO
/****** Object:  Default [DF__StoreSale__FromS__0C31A3E9]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[StoreSales] ADD  DEFAULT ((1)) FOR [FromSRLS]
GO
/****** Object:  ForeignKey [FK_APCERTIF_RELATIONS_APRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APCertificate]  WITH CHECK ADD  CONSTRAINT [FK_APCERTIF_RELATIONS_APRECORD] FOREIGN KEY([APRecordID])
REFERENCES [dbo].[APRecord] ([APRecordID])
GO
ALTER TABLE [dbo].[APCertificate] CHECK CONSTRAINT [FK_APCERTIF_RELATIONS_APRECORD]
GO
/****** Object:  ForeignKey [FK_APCHECKR_RELATIONS_APRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APCheckResult]  WITH CHECK ADD  CONSTRAINT [FK_APCHECKR_RELATIONS_APRECORD] FOREIGN KEY([APRecordID])
REFERENCES [dbo].[APRecord] ([APRecordID])
GO
ALTER TABLE [dbo].[APCheckResult] CHECK CONSTRAINT [FK_APCHECKR_RELATIONS_APRECORD]
GO
/****** Object:  ForeignKey [FK_APMESSAG_RELATIONS_APRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APMessageResult]  WITH CHECK ADD  CONSTRAINT [FK_APMESSAG_RELATIONS_APRECORD] FOREIGN KEY([APRecordID])
REFERENCES [dbo].[APRecord] ([APRecordID])
GO
ALTER TABLE [dbo].[APMessageResult] CHECK CONSTRAINT [FK_APMESSAG_RELATIONS_APRECORD]
GO
/****** Object:  ForeignKey [FK_APPROCES_RELATIONS_APRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APProcessParameterValue]  WITH CHECK ADD  CONSTRAINT [FK_APPROCES_RELATIONS_APRECORD] FOREIGN KEY([APRecordID])
REFERENCES [dbo].[APRecord] ([APRecordID])
GO
ALTER TABLE [dbo].[APProcessParameterValue] CHECK CONSTRAINT [FK_APPROCES_RELATIONS_APRECORD]
GO
/****** Object:  ForeignKey [FK_APSTOREU_RELATIONS_APRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[APStoreUpCloseSalse]  WITH CHECK ADD  CONSTRAINT [FK_APSTOREU_RELATIONS_APRECORD] FOREIGN KEY([APRecordID])
REFERENCES [dbo].[APRecord] ([APRecordID])
GO
ALTER TABLE [dbo].[APStoreUpCloseSalse] CHECK CONSTRAINT [FK_APSTOREU_RELATIONS_APRECORD]
GO
/****** Object:  ForeignKey [FK_CONDITIO_RELATIONS_RATIOTIM]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[ConditionAmount]  WITH CHECK ADD  CONSTRAINT [FK_CONDITIO_RELATIONS_RATIOTIM] FOREIGN KEY([TimeIntervalID])
REFERENCES [dbo].[RatioTimeIntervalSetting] ([TimeIntervalID])
GO
ALTER TABLE [dbo].[ConditionAmount] CHECK CONSTRAINT [FK_CONDITIO_RELATIONS_RATIOTIM]
GO
/****** Object:  ForeignKey [FK_CONDITIO_RELATIONS_CONDITIO]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[ConditionAmountNumbers]  WITH CHECK ADD  CONSTRAINT [FK_CONDITIO_RELATIONS_CONDITIO] FOREIGN KEY([ConditionID])
REFERENCES [dbo].[ConditionAmount] ([ConditionID])
GO
ALTER TABLE [dbo].[ConditionAmountNumbers] CHECK CONSTRAINT [FK_CONDITIO_RELATIONS_CONDITIO]
GO
/****** Object:  ForeignKey [FK_CONTRACT_RELATIONS_SRLS_TB_]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_CONTRACT_RELATIONS_SRLS_TB_] FOREIGN KEY([CompanyCode])
REFERENCES [dbo].[SRLS_TB_Master_Company] ([CompanyCode])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_CONTRACT_RELATIONS_SRLS_TB_]
GO
/****** Object:  ForeignKey [FK_ENTITY_RELATIONS_CONTRACT]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_ENTITY_RELATIONS_CONTRACT] FOREIGN KEY([ContractSnapshotID])
REFERENCES [dbo].[Contract] ([ContractSnapshotID])
GO
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_ENTITY_RELATIONS_CONTRACT]
GO
/****** Object:  ForeignKey [FK_ENTITY_RELATIONS_ENTITYTY]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_ENTITY_RELATIONS_ENTITYTY] FOREIGN KEY([EntityTypeName])
REFERENCES [dbo].[EntityType] ([EntityTypeName])
GO
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_ENTITY_RELATIONS_ENTITYTY]
GO
/****** Object:  ForeignKey [FK_ENTITYIN_RELATIONS_ENTITY]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[EntityInfoSetting]  WITH CHECK ADD  CONSTRAINT [FK_ENTITYIN_RELATIONS_ENTITY] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Entity] ([EntityID])
GO
ALTER TABLE [dbo].[EntityInfoSetting] CHECK CONSTRAINT [FK_ENTITYIN_RELATIONS_ENTITY]
GO
/****** Object:  ForeignKey [FK_FIXEDRUL_RELATIONS_ENTITYIN]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[FixedRuleSetting]  WITH CHECK ADD  CONSTRAINT [FK_FIXEDRUL_RELATIONS_ENTITYIN] FOREIGN KEY([EntityInfoSettingID])
REFERENCES [dbo].[EntityInfoSetting] ([EntityInfoSettingID])
GO
ALTER TABLE [dbo].[FixedRuleSetting] CHECK CONSTRAINT [FK_FIXEDRUL_RELATIONS_ENTITYIN]
GO
/****** Object:  ForeignKey [FK_FIXEDTIM_RELATIONS_FIXEDRUL]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[FixedTimeIntervalSetting]  WITH CHECK ADD  CONSTRAINT [FK_FIXEDTIM_RELATIONS_FIXEDRUL] FOREIGN KEY([RuleSnapshotID])
REFERENCES [dbo].[FixedRuleSetting] ([RuleSnapshotID])
GO
ALTER TABLE [dbo].[FixedTimeIntervalSetting] CHECK CONSTRAINT [FK_FIXEDTIM_RELATIONS_FIXEDRUL]
GO
/****** Object:  ForeignKey [FK_GLCERTIF_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLCertificate]  WITH CHECK ADD  CONSTRAINT [FK_GLCERTIF_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLCertificate] CHECK CONSTRAINT [FK_GLCERTIF_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_GLCHECKR_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLCheckResult]  WITH CHECK ADD  CONSTRAINT [FK_GLCHECKR_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLCheckResult] CHECK CONSTRAINT [FK_GLCHECKR_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_GLEXCEPT_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLException]  WITH CHECK ADD  CONSTRAINT [FK_GLEXCEPT_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLException] CHECK CONSTRAINT [FK_GLEXCEPT_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_GLMESSAG_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLMessageResult]  WITH CHECK ADD  CONSTRAINT [FK_GLMESSAG_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLMessageResult] CHECK CONSTRAINT [FK_GLMESSAG_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_GLPROCES_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLProcessParameterValue]  WITH CHECK ADD  CONSTRAINT [FK_GLPROCES_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLProcessParameterValue] CHECK CONSTRAINT [FK_GLPROCES_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_GLTIMEIN_RELATIONS_GLRECORD]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[GLTimeIntervalInfo]  WITH CHECK ADD  CONSTRAINT [FK_GLTIMEIN_RELATIONS_GLRECORD] FOREIGN KEY([GLRecordID])
REFERENCES [dbo].[GLRecord] ([GLRecordID])
GO
ALTER TABLE [dbo].[GLTimeIntervalInfo] CHECK CONSTRAINT [FK_GLTIMEIN_RELATIONS_GLRECORD]
GO
/****** Object:  ForeignKey [FK_KIOSKSAL_RELATIONS_SRLS_TB_2]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[KioskSales]  WITH CHECK ADD  CONSTRAINT [FK_KIOSKSAL_RELATIONS_SRLS_TB_2] FOREIGN KEY([KioskID])
REFERENCES [dbo].[SRLS_TB_Master_Kiosk] ([KioskID])
GO
ALTER TABLE [dbo].[KioskSales] CHECK CONSTRAINT [FK_KIOSKSAL_RELATIONS_SRLS_TB_2]
GO
/****** Object:  ForeignKey [FK_KIOSKSAL_RELATIONS_SRLS_TB_]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[KioskSalesCollection]  WITH CHECK ADD  CONSTRAINT [FK_KIOSKSAL_RELATIONS_SRLS_TB_] FOREIGN KEY([KioskID])
REFERENCES [dbo].[SRLS_TB_Master_Kiosk] ([KioskID])
GO
ALTER TABLE [dbo].[KioskSalesCollection] CHECK CONSTRAINT [FK_KIOSKSAL_RELATIONS_SRLS_TB_]
GO
/****** Object:  ForeignKey [FK_RATIOCYC_RELATIONS_RATIORUL]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RatioCycleSetting]  WITH CHECK ADD  CONSTRAINT [FK_RATIOCYC_RELATIONS_RATIORUL] FOREIGN KEY([RatioID])
REFERENCES [dbo].[RatioRuleSetting] ([RatioID])
GO
ALTER TABLE [dbo].[RatioCycleSetting] CHECK CONSTRAINT [FK_RATIOCYC_RELATIONS_RATIORUL]
GO
/****** Object:  ForeignKey [FK_RATIORUL_RELATIONS_ENTITYIN]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[RatioRuleSetting]  WITH CHECK ADD  CONSTRAINT [FK_RATIORUL_RELATIONS_ENTITYIN] FOREIGN KEY([EntityInfoSettingID])
REFERENCES [dbo].[EntityInfoSetting] ([EntityInfoSettingID])
GO
ALTER TABLE [dbo].[RatioRuleSetting] CHECK CONSTRAINT [FK_RATIORUL_RELATIONS_ENTITYIN]
GO
/****** Object:  ForeignKey [FK_SRLS_TB__RELATIONS_SRLS_TB_]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_Kiosk]  WITH CHECK ADD  CONSTRAINT [FK_SRLS_TB__RELATIONS_SRLS_TB_] FOREIGN KEY([StoreNo])
REFERENCES [dbo].[SRLS_TB_Master_Store] ([StoreNo])
GO
ALTER TABLE [dbo].[SRLS_TB_Master_Kiosk] CHECK CONSTRAINT [FK_SRLS_TB__RELATIONS_SRLS_TB_]
GO
/****** Object:  ForeignKey [FK_SRLS_TB__REFERENCE_SRLS_TB_6]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[SRLS_TB_Master_User]  WITH CHECK ADD  CONSTRAINT [FK_SRLS_TB__REFERENCE_SRLS_TB_6] FOREIGN KEY([DeptCode])
REFERENCES [dbo].[SRLS_TB_Master_Department] ([DeptCode])
GO
ALTER TABLE [dbo].[SRLS_TB_Master_User] CHECK CONSTRAINT [FK_SRLS_TB__REFERENCE_SRLS_TB_6]
GO
/****** Object:  ForeignKey [FK_SRLS_TB__REFERENCE_SRLS_TB_5]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[StoreSales]  WITH CHECK ADD  CONSTRAINT [FK_SRLS_TB__REFERENCE_SRLS_TB_5] FOREIGN KEY([StoreNo])
REFERENCES [dbo].[SRLS_TB_Master_Store] ([StoreNo])
GO
ALTER TABLE [dbo].[StoreSales] CHECK CONSTRAINT [FK_SRLS_TB__REFERENCE_SRLS_TB_5]
GO
/****** Object:  ForeignKey [FK_TYPECODE_RELATIONS_ENTITYTY]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[TypeCode]  WITH CHECK ADD  CONSTRAINT [FK_TYPECODE_RELATIONS_ENTITYTY] FOREIGN KEY([EntityTypeName])
REFERENCES [dbo].[EntityType] ([EntityTypeName])
GO
ALTER TABLE [dbo].[TypeCode] CHECK CONSTRAINT [FK_TYPECODE_RELATIONS_ENTITYTY]
GO
/****** Object:  ForeignKey [FK_TYPECODE_RELATIONS_RENTTYPE]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[TypeCode]  WITH CHECK ADD  CONSTRAINT [FK_TYPECODE_RELATIONS_RENTTYPE] FOREIGN KEY([RentTypeName])
REFERENCES [dbo].[RentType] ([RentTypeName])
GO
ALTER TABLE [dbo].[TypeCode] CHECK CONSTRAINT [FK_TYPECODE_RELATIONS_RENTTYPE]
GO
/****** Object:  ForeignKey [FK_VENDORCO_RELATIONS_CONTRACT]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[VendorContract]  WITH CHECK ADD  CONSTRAINT [FK_VENDORCO_RELATIONS_CONTRACT] FOREIGN KEY([ContractSnapshotID])
REFERENCES [dbo].[Contract] ([ContractSnapshotID])
GO
ALTER TABLE [dbo].[VendorContract] CHECK CONSTRAINT [FK_VENDORCO_RELATIONS_CONTRACT]
GO
/****** Object:  ForeignKey [FK_VENDOREN_RELATIONS_ENTITY]    Script Date: 08/16/2012 19:18:19 ******/
ALTER TABLE [dbo].[VendorEntity]  WITH CHECK ADD  CONSTRAINT [FK_VENDOREN_RELATIONS_ENTITY] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Entity] ([EntityID])
GO
ALTER TABLE [dbo].[VendorEntity] CHECK CONSTRAINT [FK_VENDOREN_RELATIONS_ENTITY]
GO
