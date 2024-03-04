SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CASE_MANAGEMENT-GET_CASE_DETAIL]
@Pageno				INT=1,
@filter				NVARCHAR(500)='',
@pagesize			INT=20,  
@Sorting			NVARCHAR(500)='CC.OfficeFileNo',
@SortOrder			NVARCHAR(500)='desc',
@UserName			NVARCHAR(15),
@DataFor			NVARCHAR(50),
@Location			NVARCHAR(1),
@CaseLevelCode		NVARCHAR(3) = '0'
AS
DECLARE
@FilterText			NVARCHAR(MAX) = '',
@FinalQuery			NVARCHAR(MAX),
@FinalSummaryQuery	NVARCHAR(MAX),
@FetchLimit			NVARCHAR(MAX),
@TableColumn		NVARCHAR(MAX)  = ' CaseId,DetailId,OfficeFileNo,ClientName,Defendant,AccountContractNo,ClientFileNo,CaseLevelCode,CaseLevelName,CourtRefNo,CurrentHearingDate,CourtDecision,NextHearingDate, OfficeFileStatus, FileStatusName, SessionRollId ',
@TableQuery			NVARCHAR(MAX),
@AdditionalWhere	NVARCHAR(MAX) = ' where 1=1',
@QueryFirstPart		NVARCHAR(MAX),
@SelectColumns		NVARCHAR(MAX),
@SummaryQuery		NVARCHAR(MAX),
@JoinTables			NVARCHAR(MAX),
@Where				NVARCHAR(MAX)


BEGIN

	IF @CaseLevelCode = '5'
		begin
			SET @SelectColumns = '
			FROM (
				  SELECT 
				  CC.CaseId
				 ,CCD.EnforcementId DetailId
				 ,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant,CC.AccountContractNo,CC.ClientFileNo
				 ,case when CCD.CurrentDisputeLevelandType in (''3'',''6'') then ''SUPREME DISPUTE'' else ''SUPREME'' end  AS CaseLevelCode
				 ,case when CCD.CurrentDisputeLevelandType in (''3'',''6'') then ''SUPREME DISPUTE'' else ''SUPREME'' end  AS CaseLevelName
				 ,CNW.CASE_NO as CourtRefNo
				 ,case when ISNULL(CC.OfficeFileStatus,''0'') = ''0'' then NULL else FileStatus.Mst_Desc end as FileStatusName
				 ,CC.CurrentHearingDate, CC.CourtDecision, CC.NextHearingDate, CC.OfficeFileStatus
				 ,ISNULL(SR.SessionRollId,0) as SessionRollId
				 from CourtCases as CC
				 '

			SET @SummaryQuery = '
					SELECT
					COUNT(*) as recordsTotal,
					count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords,
					count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords
					from CourtCases as CC
					'

			SET @JoinTables	= '
				Inner Join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				Left  Join CourtCasesEnforcements CCD on CCD.CaseId = CC.CaseId and CCD.CurrentDisputeLevelandType in (''3'',''6'')
				Left  join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573 
				Left  Join CaseNosVW CNW on CNW.CaseId = CC.CaseId
				Left  Join SessionsRolls SR on SR.CaseId = CC.CaseId and SR.DeletedOn is null
					'

			SET	@Where = ' 
					where CC.CaseStatus = ''1''
					AND	  1 = 
						case 
							when CC.CaseLevelCode = ''5'' then 1 
							else 
								case when CC.OfficeFileStatus = ''OFS-20'' AND CCD.CurrentDisputeLevelandType in (''3'',''6'') then 1 else 0 
							end
						end
					AND   (LEFT(CC.OfficeFileNo,1) = '''+@Location+''' OR '''+@Location+''' = ''A'')
					'
		end
	ELSE
		begin
			if @DataFor = 'AGNCLIENT'
				BEGIN
					SET @SelectColumns = '
					FROM (
						  SELECT CC.CaseId,CCD.DetailId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant,CC.AccountContractNo,CC.ClientFileNo
								,CC.CaseLevelCode
								,CaseLevelMas.Mst_Desc as CaseLevelName
								,CNW.CASE_NO as CourtRefNo
								,CC.CurrentHearingDate, CC.CourtDecision, CC.NextHearingDate, CC.OfficeFileStatus
								,case when ( CC.OfficeFileStatus = ''0'' OR CC.OfficeFileStatus is null) then NULL else FileStatus.Mst_Desc end as FileStatusName
								,ISNULL(SR.SessionRollId,0) as SessionRollId
								from CourtCases as CC
								'

					SET @SummaryQuery = '
											SELECT
											COUNT(*) as recordsTotal,
											count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords,
											count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords
											from CourtCases as CC
											'

					SET @JoinTables	= ' 
											Inner Join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
											Left  Join CourtCasesDetailVW CCD on CCD.CaseId = CC.CaseId and CCD.CaseLevelCode = CC.CaseLevelCode
											Left  join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573 
											Left  Join CaseNosVW CNW on CNW.CaseId = CC.CaseId
											Left  Join SessionsRolls SR on SR.CaseId = CC.CaseId and SR.DeletedOn is null
											Inner Join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
											'

					SET	@Where = ' 
											where CC.CaseStatus		= ''1''
											AND   CC.AgainstCode	= ''3''
											AND   (LEFT(CC.OfficeFileNo,1) = '''+@Location+''' OR '''+@Location+''' = ''A'')
											'

				END
			else
				BEGIN
					SET @SelectColumns = '
					FROM (
						  SELECT CC.CaseId,CCD.DetailId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant,CC.AccountContractNo,CC.ClientFileNo,CC.CaseLevelCode
								,CNW.CASE_NO as CourtRefNo
								,CC.CurrentHearingDate, CC.CourtDecision, CC.NextHearingDate, CC.OfficeFileStatus
								,case when ( CC.OfficeFileStatus = ''0'' OR CC.OfficeFileStatus is null) then NULL else FileStatus.Mst_Desc end as FileStatusName
								,ISNULL(SR.SessionRollId,0) as SessionRollId
								from CourtCases as CC
								'

					SET @SummaryQuery = '
											SELECT
											COUNT(*) as recordsTotal,
											count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords,
											count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords
											from CourtCases as CC
											'

					SET @JoinTables	= ' 
											Inner Join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
											Left  Join CourtCasesDetails CCD on CCD.CaseId = CC.CaseId and CCD.CaseLevelCode = CC.CaseLevelCode
											Left  join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573 
											Left  Join CaseNosVW CNW on CNW.CaseId = CC.CaseId
											Left  Join SessionsRolls SR on SR.CaseId = CC.CaseId and SR.DeletedOn is null
											'

					SET	@Where = ' 
											where CC.CaseStatus = ''1''
											AND	  CC.CaseLevelCode = '''+@CaseLevelCode+'''
											AND   (LEFT(CC.OfficeFileNo,1) = '''+@Location+''' OR '''+@Location+''' = ''A'')
											'

				END
		end


	SET @QueryFirstPart = '
SELECT '+@TableColumn+' 
 FROM 
  ( 
   SELECT ROW_NUMBER() OVER (order by CurrentHearingDate desc) as RowNum, * 
    from 
	  ( 
	   SELECT '
	SET @TableQuery  = @SelectColumns+@JoinTables+@Where

	IF(@filter !='')
	BEGIN
		if @DataFor = 'AGNCLIENT'
			begin 
				SET @FilterText = '
						AND (
							CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CC.CourtDecision like N''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CNW.CASE_NO like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR FileStatus.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR CaseLevelMas.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							)
							'
			end
		else
			begin
				SET @FilterText = '
						AND (
							CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CC.CourtDecision like N''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CNW.CASE_NO like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR FileStatus.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							)
							'
			end

	END


	IF(@pagesize > 0)
	BEGIN
		SET @pagesize = @pagesize + @Pageno
		SET @Pageno = @Pageno + 1
		SET @FetchLimit = 'WHERE RowNum >= '+CONVERT(varchar,@Pageno)+' AND RowNum <= '+CONVERT(varchar,@pagesize)
	END
	ELSE
	BEGIN	
		SET @FetchLimit = ' ' 
	END

BEGIN	
	SET @FinalQuery = @QueryFirstPart + @TableColumn + @TableQuery + @FilterText + 
			' ) AS DAT ' + 
			  @AdditionalWhere + 
	  ' ) AS RowConstrainedResult 
	) AS FINAL ' + @FetchLimit+' ORDER BY RowNum '
	
	SET @FinalSummaryQuery = @SummaryQuery+@JoinTables+@Where+@FilterText
END

print @FinalQuery
exec (@FinalQuery)
print @FinalSummaryQuery
exec (@FinalSummaryQuery)

END


