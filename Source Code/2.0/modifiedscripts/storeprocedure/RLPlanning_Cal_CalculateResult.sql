USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_Cal_CalculateResult]    Script Date: 08/12/2012 16:21:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_Cal_CalculateResult]
@UserID nvarchar(50),
@OperateTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT C.CompanyCode, C.CompanyName, S.StoreNo,
	CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN '餐厅' ELSE '甜品店' END AS EntityType,
	CASE WHEN FC.KioskNo IS NULL OR FC.KioskNo='' THEN S.SimpleName ELSE K.KioskName END AS EntityName,
	FC.ErrorType,
	CASE WHEN FC.ErrorType='合同缺失' THEN '修改合同' 
	WHEN FC.ErrorType='Sales数据不全' THEN '录入销售数据' ELSE NULL END AS Operation,
	FC.KioskNo, FC.ContractNo, FC.CheckID, FC.ContractSnapShotID, FC.EntityID --不显示，用于弹出修正界面用
	FROM Forecast_CheckResult FC
	INNER JOIN SRLS_TB_Master_Store S ON FC.StoreNo=S.StoreNo
	INNER JOIN SRLS_TB_Master_Company C ON S.CompanyCode=C.CompanyCode
	LEFT JOIN SRLS_TB_Master_Kiosk K ON K.KioskNo=FC.KioskNo
	WHERE FC.IsSolved=0 AND FC.UserOrGroupID=@UserID AND FC.CreateTime>@OperateTime
	ORDER BY FC.StoreNo
END



GO


