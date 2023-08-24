USE [BankTrans]
GO

/****** Object:  StoredProcedure [dbo].[check_Balance]    Script Date: 21/8/2023 1:02:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[check_Balance]
	-- Add the parameters for the stored procedure here
	(@AcctNum varchar(50), @WithdrawAmount float)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT (Sum(Amount)-@WithdrawAmount) Bal from AccountTrans where AccountID= @AcctNum
	
	
	
END
GO


