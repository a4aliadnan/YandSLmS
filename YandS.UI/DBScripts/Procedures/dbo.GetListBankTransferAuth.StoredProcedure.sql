SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [GetListBankTransferAuth]
 AS
Select MstId, MstParentId,	Mst_Desc,	Mst_Value,	DisplaySeq, Active_Flag,Remarks, CreatedBy, CreatedOn,UpdatedBy, UpdatedOn
from v_BankTransferAuth
order by DisplaySeq

GO
