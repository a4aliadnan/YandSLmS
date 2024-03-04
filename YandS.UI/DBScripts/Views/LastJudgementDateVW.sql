ALTER view [dbo].[LastJudgementDateVW] as
Select SessionRollId,CaseId,JudgementsDate
from
	(
	Select SessionRollId,CaseId,JudgementsDate, rank() over (partition by CaseId order by JudgementsDate desc) as rnk 
	from
		(
		select SessionRollId,CaseId, PrimaryJudgementsDate as JudgementsDate
		FROM SessionsRolls 
		where PrimaryJudgements is not null
		and   DeletedOn is null
		and   SessionFileStatus = 'OFS-17'
		and   PrimaryJDReceiveDate is null
		and   PrimaryJudgementsDate is not null
		union all
		select SessionRollId,CaseId, AppealJudgementsDate as JudgementsDate
		FROM SessionsRolls 
		where AppealJudgements is not null
		and   DeletedOn is null
		and   SessionFileStatus = 'OFS-17'
		and   AppealJDReceiveDate is null
		and   AppealJudgementsDate is not null
		union all
		select SessionRollId,CaseId, SupremeJudgementsDate as JudgementsDate
		FROM SessionsRolls 
		where SupremeJudgements is not null
		and   DeletedOn is null
		and   SessionFileStatus = 'OFS-17'
		and   SupremeJDReceiveDate is null
		and   SupremeJudgementsDate is not null
		union all
		select SessionRollId,CaseId, EnforcementJudgementsDate as JudgementsDate
		FROM SessionsRolls 
		where EnforcementJudgements is not null
		and   DeletedOn is null
		and   SessionFileStatus = 'OFS-17'
		and   EnforcementJDReceiveDate is null
		and   EnforcementJudgementsDate is not null
		) DAT
	) MAST_DAT
where rnk = 1
GO

