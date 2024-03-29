SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetDetailTable]
@DataFor				VARCHAR(15),
@SessionRollId			INT,
@CaseId					INT = NULL
AS
BEGIN
  IF (@DataFor = 'CASEHIST')
  BEGIN
	SELECT Id,SessionRollId,CaseId,CurrentHearingDate,CourtDecision,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn from CourtDecisionHistory where SessionRollId = @SessionRollId Order by 1 desc
  END
  ELSE IF (@DataFor = 'CASEHISTTEXT')
  BEGIN
		DECLARE @DecisionHistory nvarchar(max)
		SET @DecisionHistory = NULL

		SELECT @DecisionHistory = COALESCE(@DecisionHistory + '','') + FORMAT (CurrentHearingDate, 'dd/MM/yyyy') + ' : ' + CourtDecision
		FROM CourtDecisionHistory
		where CaseId = @CaseId
		order by Id

		SELECT replace(REPLACE(@DecisionHistory, CHAR(13) , '<br />'),CHAR(10), '<br />')  as CaseHistory
  END
  ELSE IF (@DataFor = 'LASTJUDDETAIL')
  BEGIN
		SELECT CaseLevelCode,JudgementsDate,JudgementReceive,IsFavorable from JudgementDetailView where CaseId = @CaseId 
  END
  ELSE IF (@DataFor = 'CLOSACCOUNT')
  BEGIN
			Select
				CaseId,	OfficeFileNo,	ClientCode,	ClientName,	Defendant,	AgainstCode,	AgainstName,	ClaimAmount,
				AccountContractNo,	ClientFileNo,	InvoiceId,	InvoiceNumber,	InvoiceDate,	CourtType,	CourtName,	InvoiceStatus,
				InvoiceStatusName,	TransferType,	TransferNumber,	TransferDate,	Credit_Account,	CreditAccountName,	CourtLevelDisp,	InvoiceAmount
				from
				(
				Select	 CC.CaseId,CC.OfficeFileNo,CC.ClientCode,ClientMas.Mst_Desc as ClientName,CC.Defendant,CC.AgainstCode,AgainstMas.Mst_Desc as AgainstName,CC.ClaimAmount
						,CC.AccountContractNo,CC.ClientFileNo
						,CI.InvoiceId,CI.InvoiceNumber,CI.InvoiceDate,CI.CourtType,CourtTypeMas.Mst_Desc as CourtName,CI.InvoiceStatus,InvStatusMas.Mst_Desc as InvoiceStatusName
						,CI.TransferType,CI.TransferNumber,CI.TransferDate,CI.Credit_Account,BA.Mst_Desc as CreditAccountName
						,dbo.FnGetInvoiceCaseLevel(CC.CaseId,CI.CourtType) as CourtLevelDisp
						,dbo.FnGetInvoiceTotalWithVAT(CI.InvoiceId) as InvoiceAmount
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
				join CaseInvoices CI on CI.CaseId = CC.CaseId
				join MASTER_S CourtTypeMas on CI.CourtType = CourtTypeMas.Mst_Value and CourtTypeMas.MstParentId = 15
				join MASTER_S InvStatusMas on CI.InvoiceStatus = InvStatusMas.Mst_Value and InvStatusMas.MstParentId = 247
				join Link_Bank_Account_View BA on BA.LinkId = CI.Credit_Account
				where CC.CaseId = @CaseId
				) dat		
  END
END