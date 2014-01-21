USE [RLPlanning]
GO
/****** Object:  UserDefinedDataType [dbo].[URL]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'URL' AND ss.name = N'dbo')
CREATE TYPE [dbo].[URL] FROM [nvarchar](255) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TString_50]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TString_50' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TString_50] FROM [nvarchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TString_200]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TString_200' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TString_200] FROM [nvarchar](200) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TString_1000]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TString_1000' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TString_1000] FROM [nvarchar](1000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TString_100]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TString_100' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TString_100] FROM [nvarchar](100) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TString]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TString' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TString] FROM [nvarchar](255) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TInt32]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TInt32' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TInt32] FROM [int] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TDateTime]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TDateTime' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TDateTime] FROM [datetime] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TBoolean]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TBoolean' AND ss.name = N'dbo')
CREATE TYPE [dbo].[TBoolean] FROM [bit] NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[SortOrder]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'SortOrder' AND ss.name = N'dbo')
CREATE TYPE [dbo].[SortOrder] FROM [int] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[ObjName]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'ObjName' AND ss.name = N'dbo')
CREATE TYPE [dbo].[ObjName] FROM [nvarchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[ObjID]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'ObjID' AND ss.name = N'dbo')
CREATE TYPE [dbo].[ObjID] FROM [nvarchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[ObjCode]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'ObjCode' AND ss.name = N'dbo')
CREATE TYPE [dbo].[ObjCode] FROM [nvarchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[Description]    Script Date: 08/16/2012 19:24:56 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'Description' AND ss.name = N'dbo')
CREATE TYPE [dbo].[Description] FROM [nvarchar](1000) NULL
GO
