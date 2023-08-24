USE [BankTrans]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GenerateMonthlyReportforAccount]
		@AcctNum = N'AC003',
		@TransMonth = 08

SELECT	'Return Value' = @return_value

GO

select Sum(Amount) from [BankTrans].[dbo].[AccountTrans]  where AccountID='AC003' and TransType='W' and MONTH(TrnDate)=08
(select Sum(Amount) from [BankTrans].[dbo].[AccountTrans]  where AccountID='AC003' and TransType='D' and MONTH(TrnDate)=08) - (select Sum(Amount) from [BankTrans].[dbo].[AccountTrans]  where AccountID='AC003' and TransType='W' and MONTH(TrnDate)=08)
