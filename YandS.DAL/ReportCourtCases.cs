using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YandS.DAL
{
    public class ReportCourtCases : BaseDAL
    {
        public DataSet getCourtCaseDetail(RepCaseParameterForm objParameters)
        {
            DataSet _resultDS = new DataSet();
            try
            {
                DataTable _result = new DataTable();
                SqlParameter[] parameterList = new SqlParameter[]
                                                        {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@ClientCode", objParameters.ClientCode),
                                                         new SqlParameter("@AgainstCode", objParameters.AgainstCode),
                                                         new SqlParameter("@ClaimAmountFrom", objParameters.ClaimAmountFrom),
                                                         new SqlParameter("@ClaimAmountTo", objParameters.ClaimAmountTo),
                                                         new SqlParameter("@CaseTypeCode", objParameters.CaseTypeCode),
                                                         new SqlParameter("@CaseLevelCode", objParameters.CaseLevelCode),
                                                         new SqlParameter("@CaseStatus", objParameters.CaseStatus),
                                                         new SqlParameter("@ReceptionDateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@ReceptionDateTo", objParameters.ReceptionDateTo),
                                                         new SqlParameter("@RegistrationDateFrom", objParameters.RegistrationDateFrom),
                                                         new SqlParameter("@RegistrationDateTo", objParameters.RegistrationDateTo),
                                                         new SqlParameter("@JudgmentDateFrom", objParameters.JudgmentDateFrom),
                                                         new SqlParameter("@JudgmentDateTo", objParameters.JudgmentDateTo),
                                                         new SqlParameter("@PartialViewName", objParameters.PartialViewName),
                                                         new SqlParameter("@ApealByWho", objParameters.ApealByWho),
                                                         new SqlParameter("@GovernorateId", objParameters.GovernorateId),
                                                         new SqlParameter("@AgainstInsurance", objParameters.AgainstInsurance),
                                                         new SqlParameter("@ODBBankBranch", objParameters.ODBBankBranch),
                                                         new SqlParameter("@ClientCaseType", objParameters.ClientCaseType),
                                                         new SqlParameter("@OmaniExp", objParameters.OmaniExp),
                                                         new SqlParameter("@EnforcementlevelId", objParameters.EnforcementlevelId),
                                                         new SqlParameter("@ReOpenEnforcement", objParameters.ReOpenEnforcement),
                                                         new SqlParameter("@CourtLocationid", objParameters.CourtLocationid),
                                                         new SqlParameter("@ReportLang", objParameters.ClickedButtonName),
                                                         new SqlParameter("@ReasonCode", objParameters.ReasonCode),
                                                         new SqlParameter("@LoanManager", objParameters.LoanManager)
                                                        };
                _resultDS = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCaseDetailForReport", parameterList);
                _result = _resultDS.Tables[0];

            }
            catch(Exception ex)
            {
                string ErrMessage =  ex.Message;
                string ErrStackTrac = ex.StackTrace;
            }
            return _resultDS;

        }
        public DataSet getCaseFollowup(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@CaseLevelCode", objParameters.CaseLevelCode),
                                                         new SqlParameter("@HearingDateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@HearingDateTo", objParameters.ReceptionDateTo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCaseFollowupSchedule", parameterList);
        }
        public DataSet getCourtCasesByStatus(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@CaseStatus", objParameters.CaseStatus),
                                                         new SqlParameter("@ReceptionDateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@ReceptionDateTo", objParameters.ReceptionDateTo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCasesByStatus", parameterList);
        }
        public DataSet getClientWiseInvRep(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@ClientCode", objParameters.ClientCode),
                                                         new SqlParameter("@ReceptionDateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@ReceptionDateTo", objParameters.ReceptionDateTo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetClientWiseInvRep", parameterList);
        }
        public DataSet getPVReportPivot(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@DateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@DateTo", objParameters.ReceptionDateTo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPVReportPivot", parameterList);
        }
        public DataSet getInvoiceReport(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@DateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@DateTo", objParameters.ReceptionDateTo),
                                                         new SqlParameter("@ClientCode", objParameters.ClientCode),
                                                         new SqlParameter("@AgainstCode", objParameters.AgainstCode),
                                                         new SqlParameter("@ODBBankBranch", objParameters.ODBBankBranch),
                                                         new SqlParameter("@InvoiceStatus", objParameters.InvoiceStatus),
                                                         new SqlParameter("@ClientCaseType", objParameters.ClientCaseType),
                                                         new SqlParameter("@FeeTypeId", objParameters.FeeTypeId)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetInvoiceReport", parameterList);
        }
        public DataSet getPVListReport(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@DateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@DateTo", objParameters.ReceptionDateTo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPVListReport", parameterList);
        }
        public DataSet getPVIntraTransReport(RepCaseParameterForm objParameters)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", objParameters.Location),
                                                         new SqlParameter("@DateFrom", objParameters.ReceptionDateFrom),
                                                         new SqlParameter("@DateTo", objParameters.ReceptionDateTo),
                                                         new SqlParameter("@VoucherType", objParameters.VoucherType),
                                                         new SqlParameter("@PayTo", objParameters.Payment_To)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPVTransReport", parameterList);
        }

        public string GenerateExcelStream(string P_TemplateName, string P_Heading, DataSet ds, ref MemoryStream ms, string InvoiceRefNo = null)
        {
            IFormatProvider formatProvider = new CultureInfo("en-GB", true);

            string Result = "SUCCESS";
            string CurrentDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", formatProvider);

            try
            {
                if (string.IsNullOrEmpty(P_TemplateName))
                {
                    int rowstart = 1;
                    int colstart = 1;
                    int rowend = rowstart + 1;
                    int colend = ds.Tables[0].Columns.Count;

                    using (ExcelPackage pck = new ExcelPackage(ms))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ExcelReport");

                        ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                        ws.Cells[rowstart, colstart, rowend, colend].Value = "Y & S Associates";
                        ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Size = 20;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                        ws.Cells[3, colstart, 3, colend].Merge = true;
                        ws.Cells[3, colstart, 3, colend].Value = P_Heading;
                        ws.Cells[3, colstart, 3, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[3, colstart, 3, colend].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        ws.Cells[3, colstart, 3, colend].Style.Font.Bold = true;
                        ws.Cells[3, colstart, 3, colend].Style.Font.Size = 14;
                        ws.Cells[3, colstart, 3, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[3, colstart, 3, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);
                        ws.Cells[3, colstart, 3, colend].Style.WrapText = true;
                        ws.Row(3).Height = 45;

                        ws.Cells[4, colend - 1].Value = "Run DateTime";
                        ws.Cells[4, colend].Value = CurrentDateTime;

                        var modelRows = ds.Tables[0].Rows.Count + 2;
                        string modelRange = "A5:AB" + modelRows.ToString();
                        var modelTable = ws.Cells[modelRange];
                        modelTable.Style.WrapText = true;

                        ws.Cells["A5"].LoadFromDataTable(ds.Tables[0], true, OfficeOpenXml.Table.TableStyles.Medium16);
                        //ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        int[] ColSize = new[] { 9, 25, 25, 15, 12, 12, 20, 20, 12, 12, 15, 15, 20, 15, 12, 12, 12, 12, 20, 12, 15, 12, 50, 150, 15, 15, 15, 15 };
                        int colNumber = 0;
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            colNumber++;
                            if (col.DataType == typeof(DateTime))
                                ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";

                            ws.Column(colNumber).Width = ColSize[colNumber -1];
                        }


                        modelTable.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        pck.SaveAs(ms);
                    }

                }
                else
                {
                    if (P_TemplateName == "INVOICEREPORT")
                    {
                        int rowstart = 2;
                        int colstart = 1;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            string BranchDisp = ds.Tables[1].Rows[0].Field<string>(0);
                            string ClientDisp = ds.Tables[1].Rows[0].Field<string>(1);
                            string AgainstDisp = ds.Tables[1].Rows[0].Field<string>(2);

                            var wb = pck.Workbook; //Not workSHEET
                            var namedCellBranch = wb.Names["LOCATION"];
                            var namedCellClient = wb.Names["CLIENT"];
                            var namedCellAgainst = wb.Names["AGAINST"];
                            var namedCellInvoiceRefNo = wb.Names["InvoiceRefNo"];

                            namedCellBranch.Value = BranchDisp;
                            namedCellClient.Value = ClientDisp;
                            namedCellAgainst.Value = AgainstDisp;
                            namedCellInvoiceRefNo.Value = InvoiceRefNo;

                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ExcelReport");
                            //ws.Cells[2, 1, 2, 3].Merge = true;
                            //ws.Cells[2, 1, 2, 3].Value = BranchDisp;
                            //ws.Cells[2, 1, 2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            //ws.Cells[2, 5, 2, 7].Merge = true;
                            //ws.Cells[2, 5, 2, 7].Value = ClientDisp;
                            //ws.Cells[2, 5, 2, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            //ws.Cells[2, 9, 2, 11].Merge = true;
                            //ws.Cells[2, 9, 2, 11].Value = AgainstDisp;
                            //ws.Cells[2, 9, 2, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


                            //ws.Cells[4, colend - 1].Value = "Run DateTime";
                            //ws.Cells[4, colend].Value = CurrentDateTime;

                            ws.Cells["A11"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 10;
                            string modelRange = "A11:W" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            int colNumber = 0;
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                colNumber++;
                                if (col.DataType == typeof(DateTime))
                                {
                                    ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                                }
                                else
                                {
                                    ws.Column(colNumber).Style.WrapText = true;
                                }
                            }
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                            //pck.Save();
                        }
                    }
                    else if (P_TemplateName == "INVOICEREPORTAHLI")
                    {

                        int rowstart = 2;
                        int colstart = 1;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {

                            decimal InvAmt = ds.Tables[1].Rows[0].Field<decimal>(0);
                            string BranchDisp = ds.Tables[1].Rows[0].Field<string>(1);
                            string AccountDisp = ds.Tables[1].Rows[0].Field<string>(2);
                            string InvAmtinWords = NumberToWords(Convert.ToDouble(InvAmt));


                            string HeaderNoteDisp = string.Empty;
                            string HeaderNote = @"Date: {0}

The Bank has filed legal cases against the below mentioned customers, for which lawyers from Yahyaei & Salt have represented our interests. We seek your approval to debit the following customer accounts with the legal fees as per the details provided below:

";
                            HeaderNoteDisp = string.Format(HeaderNote, DateTime.Today.ToString("dd/MM/yyyy"));

                            string FooterNoteDisp = string.Empty;
                            string FooterNote = @"To: Account Services
Kindly debit the legal charges accounts of all of the above customers and credit the total amount of RO {0}  (Rial Omani {1}   Only) to AL YAHYAEI & SALT - {2} BRANCH , Account No. {3}  at Ahli Bank / Main Branch.";

                            FooterNoteDisp = string.Format(FooterNote, InvAmt, InvAmtinWords, BranchDisp, AccountDisp);

                            var wb = pck.Workbook; //Not workSHEET
                            var namedCellHeader = wb.Names["ReportHeader"];

                            namedCellHeader.Value = HeaderNoteDisp;

                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A12"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 11;
                            string modelRange = "A12:I" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];
                            string SumRange = "D12:D" + modelRows.ToString();

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            int colNumber = 0;
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                colNumber++;
                                if (col.DataType == typeof(DateTime))
                                {
                                    ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                                }
                                else
                                {
                                    ws.Column(colNumber).Style.WrapText = true;
                                }
                            }
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Merge = true;
                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Value = "TOTAL";

                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells[modelRows + 1, 1, modelRows + 1, 3].Style.Font.Bold = true;

                            ws.Cells[modelRows + 1, 4].Formula = string.Format("=SUM({0})", SumRange);

                            ws.Cells[modelRows + 1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws.Cells[modelRows + 1, 4].Style.Font.Bold = true;

                            int startFooterRow = modelRows + 3;
                            int endFooterRow = startFooterRow +6;

                            string FooterRange = string.Format(@"A{0}:I{1}", startFooterRow, endFooterRow);
                            var FooterTable = ws.Cells[FooterRange];

                            FooterTable.Merge = true;
                            FooterTable.Style.Font.Bold = true;
                            FooterTable.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            FooterTable.Value = FooterNoteDisp;

                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                            //pck.Save();
                        }

                    }
                    else if (P_TemplateName == "NBO_REPORT")
                    {

                        int rowstart = 3;
                        int colstart = 1;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            //ExcelWorksheet ws = pck.Workbook.Worksheets["NBO_REPORT"];
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A3"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "A3:AP" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            
                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }
                    }
                    else if (P_TemplateName == "UF_REPORT")
                    {

                        int rowstart = 2;
                        int colstart = 1;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A2"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "A3:M" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            

                            //int colNumber = 0;
                            //foreach (DataColumn col in ds.Tables[0].Columns)
                            //{
                            //    colNumber++;
                            //    if (col.DataType == typeof(DateTime))
                            //    {
                            //        ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                            //    }
                            //    else
                            //    {
                            //        ws.Column(colNumber).Style.WrapText = true;
                            //    }
                            //}
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            //ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                            //pck.Save();
                        }

                    }
                    else if (P_TemplateName == "ShortDataEN")
                    {

                        int rowstart = 3;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                            var dt = ds.Tables[0];

                            var modelCells = ws.Cells["A3"];
                            var modelRows = dt.Rows.Count + 2;
                            string modelRangeSheet = "A1:M" + modelRows.ToString();
                            string modelRange = "A3:M" + modelRows.ToString();
                            var modelTableSheet = ws.Cells[modelRangeSheet];
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            System.Drawing.Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

                            // Fill worksheet with data to export
                            modelCells.LoadFromDataTable(dt, false);


                            for (var row = rowstart; row < dt.Rows.Count + rowstart; row++)
                            {
                                int pos = row % 2;
                                var FillRange = string.Format(@"A{0}:M{0}", row);
                                ws.Cells[FillRange].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                switch (pos)
                                {
                                    case 0:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                        break;
                                    case 1:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        break;

                                }
                            }
                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }

                    }
                    else if (P_TemplateName == "ShortDataAR")
                    {

                        int rowstart = 3;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            var dt = ds.Tables[0];

                            var modelCells = ws.Cells["A3"];
                            var modelRows = dt.Rows.Count + 2;
                            string modelRangeSheet = "A1:P" + modelRows.ToString();
                            string modelRange = "A3:P" + modelRows.ToString();
                            var modelTableSheet = ws.Cells[modelRangeSheet];
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            System.Drawing.Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

                            // Fill worksheet with data to export
                            modelCells.LoadFromDataTable(dt, false);
                            for (var row = rowstart; row < dt.Rows.Count + rowstart; row++)
                            {
                                int pos = row % 2;
                                var FillRange = string.Format(@"A{0}:P{0}", row);
                                ws.Cells[FillRange].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                switch (pos)
                                {
                                    case 0:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                        break;
                                    case 1:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        break;

                                }
                            }

                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }

                    }
                    else if (P_TemplateName == "BD_REPORT")
                    {

                        int rowstart = 2;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A2"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "A2:AB" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }

                    }
                    else if (P_TemplateName == "ODB_REPORT")
                    {

                        int rowstart = 2;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A3"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "A3:P" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }

                    }
                    else if (P_TemplateName == "OMASCO_REPORT")
                    {

                        int rowstart = 2;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            var dt = ds.Tables[0];

                            var modelCells = ws.Cells["A5"];
                            var modelRows = dt.Rows.Count + 2;
                            string modelRangeSheet = "A1:N" + modelRows.ToString();
                            string modelRange = "A5:N" + modelRows.ToString();
                            var modelTableSheet = ws.Cells[modelRangeSheet];
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            System.Drawing.Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

                            // Fill worksheet with data to export
                            modelCells.LoadFromDataTable(dt, false);
                            for (var row = rowstart; row < dt.Rows.Count + rowstart; row++)
                            {
                                int pos = row % 2;
                                var FillRange = string.Format(@"A{0}:N{0}", row);
                                ws.Cells[FillRange].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                switch (pos)
                                {
                                    case 0:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                        break;
                                    case 1:
                                        ws.Cells[FillRange].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        break;

                                }
                            }

                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                        }

                    }
                    else if (P_TemplateName == "FAB_REPORT")
                    {

                        int rowstart = 3;
                        int colstart = 1;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["A3"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "A3:L" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



                            //int colNumber = 0;
                            //foreach (DataColumn col in ds.Tables[0].Columns)
                            //{
                            //    colNumber++;
                            //    if (col.DataType == typeof(DateTime))
                            //    {
                            //        ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                            //    }
                            //    else
                            //    {
                            //        ws.Column(colNumber).Style.WrapText = true;
                            //    }
                            //}
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            //ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            //ws.Cells[ws.Dimension.Address]
                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                            //pck.Save();
                        }

                    }
                    else if (P_TemplateName == "UFC_REPORT")
                    {

                        int rowstart = 3;
                        int colstart = 2;
                        int rowend = rowstart + 1;
                        int colend = ds.Tables[0].Columns.Count;
                        MemoryStream ResultStream = new MemoryStream();

                        using (ExcelPackage pck = new ExcelPackage(ResultStream, ms))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                            ws.Cells["B3"].LoadFromDataTable(ds.Tables[0], false);

                            var modelRows = ds.Tables[0].Rows.Count + 2;
                            string modelRange = "B3:AF" + modelRows.ToString();
                            var modelTable = ws.Cells[modelRange];

                            // Assign borders
                            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



                            //int colNumber = 0;
                            //foreach (DataColumn col in ds.Tables[0].Columns)
                            //{
                            //    colNumber++;
                            //    if (col.DataType == typeof(DateTime))
                            //    {
                            //        ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                            //    }
                            //    else
                            //    {
                            //        ws.Column(colNumber).Style.WrapText = true;
                            //    }
                            //}
                            ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            //ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            //ws.Cells[ws.Dimension.Address]
                            pck.SaveAs(ResultStream);
                            ms = ResultStream;
                            //pck.Save();
                        }

                    }

                }
            }
            catch (Exception EX)
            {
                if (!(EX is System.Threading.ThreadAbortException))
                {
                    System.Web.HttpContext.Current.Response.Write(EX.ToString());
                }
                Result = EX.Message;
            }
            return Result;
        }

        public List<CaseInvoiceList> getCaseInvoiceList(string UserLocation)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", UserLocation)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCaseInvoiceList", parameterList).Tables[0].DataTableToList<CaseInvoiceList>();
        }
        public List<CaseInvoiceList> getCaseInvoiceList(int start, string searchvalue, int Length, string SortColumn, string sortDirection, string UserLocation,int CaseId = 0)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                        new SqlParameter("@Location", UserLocation),
                                                        new SqlParameter("@Pageno", start),
                                                        new SqlParameter("@filter", searchvalue),
                                                        new SqlParameter("@pagesize", Length),
                                                        new SqlParameter("@Sorting", SortColumn),
                                                        new SqlParameter("@SortOrder", sortDirection),
                                                        new SqlParameter("@CaseId", CaseId)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCaseInvoiceListWithPaging", parameterList).Tables[0].DataTableToList<CaseInvoiceList>();
        }
        public List<CourtCaseListForIndex> getCourtCaseListForIndex(string UserLocation,int skip, int pageSize)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", UserLocation)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCaseListForIndex", parameterList).Tables[0].DataTableToList<CourtCaseListForIndex>();
        }
        public List<CourtCaseListForIndex> getCourtCaseListForIndex(string UserLocation)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", UserLocation)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCaseListForIndex", parameterList).Tables[0].DataTableToList<CourtCaseListForIndex>();
        }


        public CourtCaseDTView getCourtCaseListWithPaging(int start, string searchvalue, int Length, string SortColumn, string sortDirection, string UserLocation, string DataFor, string CaseLevel, DateTime DateFrom, DateTime DateTo, string CallerName, string CourtLocation, string Governorate, string ClientCode, string EnfStage, string EnfAuctionCode, string AgainstCode, string CaseStatus)
        {
            CourtCaseDTView _result = new CourtCaseDTView();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", UserLocation),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@CaseLevelFilter", CaseLevel),
                                                         new SqlParameter("@DateFrom", DateFrom),
                                                         new SqlParameter("@DateTo", DateTo),
                                                         new SqlParameter("@CallerName", CallerName),
                                                         new SqlParameter("@EnfCourtLocation", CourtLocation),
                                                         new SqlParameter("@EnfGovernorate", Governorate),
                                                         new SqlParameter("@EnfClientCode", ClientCode),
                                                         new SqlParameter("@EnfStage", EnfStage),
                                                         new SqlParameter("@EnfAuctionCode", EnfAuctionCode),
                                                         new SqlParameter("@AgainstCode", AgainstCode),
                                                         new SqlParameter("@CaseStatus", CaseStatus)
                                                    };

            DataSet DS = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCaseListWithPaging", parameterList);
            _result.data = DS.Tables[0].DataTableToList<CourtCaseListForIndex>();
            _result.MCTRecords = int.Parse(DS.Tables[1].Rows[0]["MCTRecords"].ToString());
            _result.SLLRecords = int.Parse(DS.Tables[1].Rows[0]["SLLRecords"].ToString());
            _result.recordsTotal = int.Parse(DS.Tables[1].Rows[0]["recordsTotal"].ToString());
            _result.recordsFiltered = int.Parse(DS.Tables[1].Rows[0]["recordsTotal"].ToString());

            return _result;

        }
        public List<CourtCaseListForIndex> getCourtCaseListInvoiceWithPaging(int start, string searchvalue, int Length, string SortColumn, string sortDirection, string UserLocation)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", UserLocation),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourtCaseListInvoiceWithPaging", parameterList).Tables[0].DataTableToList<CourtCaseListForIndex>();

        }
        public List<PetrolKMDetailVM> getPetrolKMDetailVM(string PaymentHead, string PayTo)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@PaymentHead", PaymentHead),
                                                         new SqlParameter("@PayTo", PayTo),
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPetrolKMDetailVM", parameterList).Tables[0].DataTableToList<PetrolKMDetailVM>();
        }
        public List<PayVoucherCreatedVM> getPayVoucherCreatedVM(string OfficeFileNo)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@OfficeFileNo", OfficeFileNo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPayVoucherCreatedVM", parameterList).Tables[0].DataTableToList<PayVoucherCreatedVM>();
        }
        public List<FeeDetailView> getFeeDetailView(int InvoiceId)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@P_InvoiceId", InvoiceId),
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetFeeDetailView", parameterList).Tables[0].DataTableToList<FeeDetailView>();
        }

        public DataTable getDashBoardData()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetDashBoardData").Tables[0];
        }

        public List<CaseInvoiceDetail> getCaseInvoiceDetail(int Caseid)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Caseid", Caseid)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCaseInvoiceDetail", parameterList).Tables[0].DataTableToList<CaseInvoiceDetail>();
        }
        public List<ClosureFormVM> getClosureFormDetail(int Caseid, string CaseLevel, string EmployeeNumber)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@CaseId", Caseid),
                                                         new SqlParameter("@CourtLevel", CaseLevel),
                                                         new SqlParameter("@StaffNumber", EmployeeNumber)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetClosureFormDetail", parameterList).Tables[0].DataTableToList<ClosureFormVM>();
        }

        public DataTable getInvoiceDetailDt(int InvoiceId)
        {
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@P_InvoiceId", InvoiceId),
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetInvoiceDetailDt", parameterList).Tables[0];
        }

        #region FOR PV

        public List<PVListForIndex> getPVListForIndexWithPaging(
                    int start, 
                    string searchvalue, 
                    int Length, 
                    string SortColumn, 
                    string sortDirection, 
                    string UserName,
                    string DataFor
                    )
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserName", UserName),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPayVoucherForIndex", parameterList).Tables[0].DataTableToList<PVListForIndex>();
        }

        public PayVoucherDTView getPayVoucherDataViewForNewIndex(
                    int start, 
                    string searchvalue, 
                    int Length, 
                    string SortColumn, 
                    string sortDirection, 
                    string UserName,
                    string DataFor
                    )
        {
            PayVoucherDTView _result = new PayVoucherDTView();

            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserName", UserName),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            DataSet DS =  SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPayVoucherForIndexNew", parameterList);
            _result.data = DS.Tables[0].DataTableToList<PayVoucherData>();
            _result.MuscatTTL = DS.Tables[1].Rows[0]["MuscatTTL"].ToString();
            _result.SalalahTTL = DS.Tables[1].Rows[0]["SalalahTTL"].ToString();
            _result.recordsTotal = int.Parse(DS.Tables[1].Rows[0]["recordsTotal"].ToString());
            _result.recordsFiltered = int.Parse(DS.Tables[1].Rows[0]["recordsTotal"].ToString());
            
            return _result;
        }

        public List<InvoiceNotificationDTView> getInvoiceNotificationDTView(
                    int start,
                    string searchvalue,
                    int Length,
                    string SortColumn,
                    string sortDirection,
                    string UserName,
                    string DataFor,
                    string LocationId
                    )
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserName", UserName),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Location", LocationId),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetInvoiceNotificationForIndex", parameterList).Tables[0].DataTableToList<InvoiceNotificationDTView>();
        }
        public DataTable getPVSummaryData()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPVSummaryData").Tables[0];
        }

        public DataTable getPVList(string LocationName,int VoucherNo)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@Location", LocationName),
                                                         new SqlParameter("@VoucherNo", VoucherNo)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetPayVoucherList", parameterList).Tables[0];
        }
        public List<DefendentTransferDTO> GetDefendentTransfer(int CaseId, string CaseLevel)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@CaseId", CaseId),
                                                         new SqlParameter("@CaseLevel", CaseLevel)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetDefendentTransfer", parameterList).Tables[0].DataTableToList<DefendentTransferDTO>();
        }

        public DataTable GetDetailTable(string DataFor,int SessionRollId, int CaseId = 0)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@SessionRollId", SessionRollId),
                                                         new SqlParameter("@CaseId", CaseId)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetDetailTable", parameterList).Tables[0];
        }

        public DataTable ProcessDefendentTransfer(DefendentTransferDTO objDTO)
        {
            DataTable _result = new DataTable();
            DateTime nullDate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);

            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserId", objDTO.Userid),
                                                         new SqlParameter("@DataFor", objDTO.DataFor),
                                                         new SqlParameter("@DefendentTransferId", objDTO.DefendentTransferId),
                                                         new SqlParameter("@CaseId", objDTO.CaseId),
                                                         new SqlParameter("@CaseLevelCode", objDTO.CaseLevelCode),
                                                         new SqlParameter("@TransferDate", objDTO.TransferDate ?? nullDate),
                                                         new SqlParameter("@Amount", objDTO.Amount),
                                                         new SqlParameter("@MoneyTrRequestDate", objDTO.MoneyTrRequestDate ?? nullDate),
                                                         new SqlParameter("@MoneyTrCompleteDate", objDTO.MoneyTrCompleteDate ?? nullDate),
                                                         new SqlParameter("@MoneyWith", objDTO.MoneyWith)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "ProcessDefendentTransfer", parameterList).Tables[0];
        }

        public DataTable ProcessCourtDecisionHistory(CourtDecisionHistoryDTO objDTO)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserId", objDTO.Userid),
                                                         new SqlParameter("@DataFor", objDTO.DataFor),
                                                         new SqlParameter("@Id", objDTO.Id),
                                                         new SqlParameter("@SessionRollId", objDTO.SessionRollId),
                                                         new SqlParameter("@CaseId", objDTO.CaseId),
                                                         new SqlParameter("@CurrentHearingDate", objDTO.CurrentHearingDate),
                                                         new SqlParameter("@CourtDecision", objDTO.CourtDecision),
                                                         new SqlParameter("@TransportationSource", objDTO.TransportationSource)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "ProcessCourtDecisionHistory", parameterList).Tables[0];
        }
        public DataTable CheckInvoiceDuplicate(InvoiceCheckingDTO objDTO)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@CaseId", objDTO.CaseId),
                                                         new SqlParameter("@InvoiceDate", objDTO.InvoiceDate),
                                                         new SqlParameter("@FeeTypeId", objDTO.FeeTypeId),
                                                         new SqlParameter("@FeeAmount", objDTO.FeeAmount)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "CheckInvoiceDuplicate", parameterList).Tables[0];
        }

        public DataTable ExecDuplicateCaseFile(string ValueToSearch, string TypeToSearch)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@ValueToSearch", ValueToSearch),
                                                         new SqlParameter("@TypeToSearch", TypeToSearch)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "CheckDuplicateCaseFile", parameterList).Tables[0];
        }

        public DefendentTransferDTO GetDefendentTransfer(int DefendentTransferId)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@DefendentTransferId", DefendentTransferId)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetDefendentTransferById", parameterList).Tables[0].ToObject<DefendentTransferDTO>();
        }
        #endregion

        #region FOR Case Registration

        public List<CaseRegistrationListForIndex> getCaseRegistrationListForIndexWithPaging(
                    int start, 
                    string searchvalue, 
                    int Length, 
                    string SortColumn, 
                    string sortDirection, 
                    string UserName,
                    string DataFor, string LocationId, string FileStatus
                    )
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserName", UserName),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Location", LocationId),
                                                         new SqlParameter("@FileStatus", FileStatus),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCaseRegistrationForIndex", parameterList).Tables[0].DataTableToList<CaseRegistrationListForIndex>();
        }

        public List<SessionRollListForIndex> getSessionRollForIndexWithPaging(
                    int start, 
                    string searchvalue, 
                    int Length, 
                    string SortColumn, 
                    string sortDirection, 
                    string UserName,
                    string DataFor, string LocationId, string SessionLevel, DateTime DateFrom, DateTime DateTo, string CountLocationId, string LawyerId, string ClientCode
                    )
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                                    {
                                                         new SqlParameter("@UserName", UserName),
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Location", LocationId),
                                                         new SqlParameter("@SessionLevel", SessionLevel),
                                                         new SqlParameter("@DateFrom", DateFrom),
                                                         new SqlParameter("@DateTo", DateTo),
                                                         new SqlParameter("@CountLocationId", CountLocationId),
                                                         new SqlParameter("@LawyerId", LawyerId),
                                                         new SqlParameter("@ClientCode", ClientCode),
                                                         new SqlParameter("@Pageno", start),
                                                         new SqlParameter("@filter", searchvalue),
                                                         new SqlParameter("@pagesize", Length),
                                                         new SqlParameter("@Sorting", SortColumn),
                                                         new SqlParameter("@SortOrder", sortDirection)
                                                    };
            _result = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetSessionsRollForIndexNew", parameterList).Tables[0];
            return _result.DataTableToList<SessionRollListForIndex>();
        }

        public int getTableTotal(string DataFor, string LocationId)
        {
            DataTable _result = new DataTable();
            SqlParameter[] parameterList = new SqlParameter[]
                                        {
                                                         new SqlParameter("@DataFor", DataFor),
                                                         new SqlParameter("@Location", LocationId)
                                        };
            _result = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetTableTotal", parameterList).Tables[0];
            int TableTotal = _result.Rows[0].Field<int>(0);
            return TableTotal;

        }

        #endregion

        #region Helper Methods
        public static string NumberToWords(double doubleNumber)
        {
            var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
            var beforeFloatingPointWord = $"{NumberToWords(beforeFloatingPoint)}";
            var chkZeroBaisa = (doubleNumber - beforeFloatingPoint) * 1000;

            if (chkZeroBaisa > 0)
            {
                var afterFloatingPointWord = $"Baizas {NumberToWords(Convert.ToInt32((doubleNumber - beforeFloatingPoint) * 1000))}";
                return $"{beforeFloatingPointWord} and {afterFloatingPointWord}";
            }
            else
            {
                return beforeFloatingPointWord;

            }
        }
        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + " billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }
        private static string SmallNumberToWord(int number, string words)
        {
            if (number <= 0) return words;
            if (words != "")
                words += " ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }

        #endregion
    }
}
