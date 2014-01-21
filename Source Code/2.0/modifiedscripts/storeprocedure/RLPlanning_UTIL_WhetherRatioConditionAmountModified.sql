USE [RLPlanning]
GO

/****** Object:  StoredProcedure [dbo].[RLPlanning_UTIL_WhetherRatioConditionAmountModified]    Script Date: 07/28/2012 10:40:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RLPlanning_UTIL_WhetherRatioConditionAmountModified]
@TimeIntervalID nvarchar(50),
@LastTimeIntervalID nvarchar(50),
@Result BIT OUTPUT	--0��ʾû�仯��1��ʾ�б仯
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Result = 0
	--�Ƚ����������Ƿ���ͬ
	DECLARE @ConditionAmountSum int, @LastConditionAmountSum int
	SELECT @ConditionAmountSum=COUNT(*) FROM ConditionAmount WHERE TimeIntervalID=@TimeIntervalID
	SELECT @LastConditionAmountSum=COUNT(*) FROM ConditionAmount WHERE TimeIntervalID=@LastTimeIntervalID
	
	--���������һ��ֱ�ӷ���
	IF @ConditionAmountSum<>@LastConditionAmountSum
	BEGIN
		SET @Result=1
		RETURN
	END
	
	--�����Ƚ�
	DECLARE @ConditionAmountCursor CURSOR
	SET @ConditionAmountCursor = CURSOR SCROLL FOR
	SELECT ISNULL(ConditionDesc,''), ISNULL(AmountFormulaDesc,'') FROM ConditionAmount
	WHERE TimeIntervalID=@TimeIntervalID
	
	DECLARE @ConditionDesc nvarchar(1000), @AmountFormulaDesc nvarchar(1000), @LastAmountFormulaDesc nvarchar(1000)
	
	OPEN @ConditionAmountCursor
	FETCH NEXT FROM @ConditionAmountCursor INTO @ConditionDesc, @AmountFormulaDesc
	WHILE @@FETCH_STATUS=0
	BEGIN
		SELECT @LastAmountFormulaDesc=ISNULL(AmountFormulaDesc,'') FROM ConditionAmount 
		WHERE TimeIntervalID=@LastTimeIntervalID
		AND ConditionDesc=@ConditionDesc
		
		-- ����в�ͬ���򷵻�
		IF @LastAmountFormulaDesc<>@AmountFormulaDesc
		BEGIN
			CLOSE @ConditionAmountCursor
			DEALLOCATE @ConditionAmountCursor
			SET @Result=1
			RETURN
		END
		
		FETCH NEXT FROM @ConditionAmountCursor INTO @ConditionDesc, @AmountFormulaDesc
	END
	CLOSE @ConditionAmountCursor
	DEALLOCATE @ConditionAmountCursor
END

GO


