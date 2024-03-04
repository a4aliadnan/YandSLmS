using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using YandS.DAL;
using YandS.UI.Models;
using RestSharp;
using System.Threading.Tasks;

namespace YandS.UI.Controllers
{
    public class CommonTaskController : Controller
    {
        private RBACDbContext db = new RBACDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private string OfficeFileFilterTBR = Helper.getOfficeFileFilterTBR();
        private string OfficeFileFilterAJP = Helper.getOfficeFileFilterAJP();
        private string OfficeFileFilterCLOSING = Helper.getOfficeFileFilterCLOSING();
        private string OfficeFileFilterENF = Helper.getOfficeFileFilterENF();

        public CommonTaskController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public CommonTaskController()
        {
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public ActionResult CreateMasterTableDetail(string Mst_Desc, int MstParentId, string Remarks = null)
        {
            //string[] values = new[] { "-2", "-1", "0" };

            //var ItemToBeAddList = db.MasterSetup.Where(m => m.MstParentId == MstParentId && !values.Contains(m.Mst_Value)).ToList();

            var ItemToBeAddList = db.MasterSetup.Where(m => m.MstParentId == MstParentId).ToList();
            int Mst_Value = ItemToBeAddList.Select(s => Convert.ToInt32(s.Mst_Value)).Max() + 1;
            int DisplaySeq = ItemToBeAddList.Select(s => Convert.ToInt32(s.DisplaySeq)).Max() + 5;

            MasterSetups ItemToBeAdd = new MasterSetups();

            ItemToBeAdd.MstParentId = MstParentId;
            ItemToBeAdd.Mst_Desc = Mst_Desc;
            ItemToBeAdd.Mst_Value = Mst_Value.ToString();
            ItemToBeAdd.Active_Flag = true;
            ItemToBeAdd.DisplaySeq = DisplaySeq;
            ItemToBeAdd.Remarks = Remarks;

            try
            {
                db.MasterSetup.Add(ItemToBeAdd);
                db.SaveChanges();

                return new JsonResult()
                {
                    Data = new { Message = "Success", id = Mst_Value.ToString(), name = Mst_Desc }
                };
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };
            }
        }

        [HttpPost]
        public ActionResult CreateMasterTableDetailForPayTo(string Mst_Desc, int MstParentId, string Remarks = null)
        {
            string[] values = new[] { "-2", "-1", "0" };

            var ItemToBeAddList = db.MasterSetup.Where(m => m.MstParentId == MstParentId && !values.Contains(m.Mst_Value)).ToList();
            //int Mst_Value = ItemToBeAddList.Count + 1;
            int Mst_Value = ItemToBeAddList.Select(s => Convert.ToInt32(s.Mst_Value.Replace("P", ""))).Max() + 1;

            MasterSetups ItemToBeAdd = new MasterSetups();

            ItemToBeAdd.MstParentId = MstParentId;
            ItemToBeAdd.Mst_Desc = Mst_Desc;
            string ItemToBeAddValue = string.Empty;
            if (MstParentId == 214)
                ItemToBeAddValue = string.Format(@"P{0}", Mst_Value.ToString());
            else
                ItemToBeAddValue = Mst_Value.ToString();

            ItemToBeAdd.Mst_Value = ItemToBeAddValue;
            ItemToBeAdd.Active_Flag = true;
            ItemToBeAdd.DisplaySeq = Mst_Value;
            ItemToBeAdd.Remarks = Remarks;

            try
            {
                db.MasterSetup.Add(ItemToBeAdd);
                db.SaveChanges();

                return new JsonResult()
                {
                    Data = new { Message = "Success", id = ItemToBeAddValue, name = Mst_Desc }
                };


            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }


        }
        [HttpPost]
        public ActionResult AddNewPayToDetail(int MstParentId, string PayToMstDesc,string PayToBankName,string PayToAccountNumber, string PayToEmail, string PayToContactNo, string PayToMessageLang, string PayMst_Value = null)
        {
            string BankName = string.Empty;
            string jsonRemarks = string.Empty;
            string MessageLangName = string.Empty;

            if (string.IsNullOrEmpty(PayMst_Value))
            {
                string[] values = new[] { "-2", "-1", "0" };

                var ItemToBeAddList = db.MasterSetup.Where(m => m.MstParentId == MstParentId && !values.Contains(m.Mst_Value)).ToList();
                //int Mst_Value = ItemToBeAddList.Count + 1;
                int Mst_Value = 1;
                int DisplaySeq = 1;

                if (ItemToBeAddList.Count > 0)
                {
                    Mst_Value = ItemToBeAddList.Select(s => Convert.ToInt32(s.Mst_Value.Replace("P", ""))).Max() + 1;
                    DisplaySeq = ItemToBeAddList.Select(s => Convert.ToInt32(s.DisplaySeq)).Max() + 5;
                }

                MasterSetups ItemToBeAdd = new MasterSetups();

                ItemToBeAdd.MstParentId = MstParentId;
                ItemToBeAdd.Mst_Desc = PayToMstDesc;
                string ItemToBeAddValue = string.Empty;

                if (MstParentId.ToString().In("1900","1902","1903"))
                {
                    ItemToBeAddValue = string.Format(@"P{0}", Mst_Value.ToString());
                    BankName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Banks && w.Mst_Value == PayToBankName).FirstOrDefault().Mst_Desc;

                    PayToRemarks objRemarks = new PayToRemarks();
                    objRemarks.PayToAccountNumber = PayToAccountNumber;
                    objRemarks.PayToBankCode = PayToBankName;
                    objRemarks.PayToBankName = BankName;
                    objRemarks.PayToContactNo = PayToContactNo;
                    objRemarks.PayToEmail = PayToEmail;
                    objRemarks.MessageLang = PayToMessageLang;

                    if (!string.IsNullOrEmpty(PayToMessageLang))
                        MessageLangName = Helper.GetLangForSelect().Where(w => w.Mst_Value == PayToMessageLang).FirstOrDefault().Mst_Desc.ToUpper();

                    objRemarks.MessageLangName = MessageLangName;

                    jsonRemarks = JsonConvert.SerializeObject(objRemarks);
                }
                else
                    ItemToBeAddValue = Mst_Value.ToString();

                ItemToBeAdd.Mst_Value = ItemToBeAddValue;
                ItemToBeAdd.Active_Flag = true;
                ItemToBeAdd.DisplaySeq = DisplaySeq;
                ItemToBeAdd.Remarks = jsonRemarks;

                try
                {
                    db.MasterSetup.Add(ItemToBeAdd);
                    db.SaveChanges();

                    return new JsonResult()
                    {
                        Data = new { Message = "Success", id = ItemToBeAddValue, name = PayToMstDesc }
                    };
                }
                catch (Exception e)
                {
                    return new JsonResult()
                    {
                        Data = new { Message = "Error", ErrorMessage = e.Message }
                    };

                }
            }
            else
            {
                try
                {
                    PayToRemarks objRemarks = new PayToRemarks();
                    BankName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Banks && w.Mst_Value == PayToBankName).FirstOrDefault().Mst_Desc;
                    objRemarks.PayToAccountNumber = PayToAccountNumber;
                    objRemarks.PayToBankCode = PayToBankName;
                    objRemarks.PayToBankName = BankName;
                    objRemarks.PayToContactNo = PayToContactNo;
                    objRemarks.PayToEmail = PayToEmail;
                    objRemarks.MessageLang = PayToMessageLang;

                    if (!string.IsNullOrEmpty(PayToMessageLang))
                        MessageLangName = Helper.GetLangForSelect().Where(w => w.Mst_Value == PayToMessageLang).FirstOrDefault().Mst_Desc.ToUpper();

                    objRemarks.MessageLangName = MessageLangName;

                    if (PayMst_Value.Contains("P"))
                    {
                        var PayTo = db.MasterSetup.Where(w => w.MstParentId == MstParentId && w.Mst_Value == PayMst_Value).FirstOrDefault();
                        
                        jsonRemarks = JsonConvert.SerializeObject(objRemarks);

                        db.Entry(PayTo).Entity.Remarks = jsonRemarks;
                        db.Entry(PayTo).State = EntityState.Modified;
                        db.SaveChanges();

                        return new JsonResult()
                        {
                            Data = new { Message = "Success", id = PayMst_Value, name = PayToMstDesc, NoNeedAppend = "Y" }
                        };
                    }
                    else
                    {
                        var Employee = db.Employees.Where(w => w.EmployeeNumber == PayMst_Value).FirstOrDefault();

                        db.Entry(Employee).Entity.AccountNumber = PayToAccountNumber;
                        db.Entry(Employee).Entity.BankName = PayToBankName;
                        db.Entry(Employee).Entity.ContactNumber = PayToContactNo;
                        db.Entry(Employee).Entity.Email = PayToEmail;
                        db.Entry(Employee).Entity.MessageLang = PayToMessageLang;

                        db.Entry(Employee).State = EntityState.Modified;
                        db.SaveChanges();

                        return new JsonResult()
                        {
                            Data = new { Message = "Success", id = PayMst_Value, name = PayToMstDesc, NoNeedAppend = "Y" }
                        };

                    }
                }
                catch (Exception e)
                {
                    return new JsonResult()
                    {
                        Data = new { Message = "Error", ErrorMessage = e.Message }
                    };

                }
            }
        }
        [HttpPost]
        public ActionResult GetPayToDetail(int MstParentId, string Mst_Value)
        {
            try
            {
                return Json(Helper.GetPayToDetail(MstParentId, Mst_Value));
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }
        }

        [HttpPost]
        public ActionResult GetInvoiceDocName(int InvoiceId)
        {

            try
            {
                string ReturnResult = string.Empty;

                string UploadRoot = Helper.GetStorageRoot;

                string UploadPath = Path.Combine(UploadRoot, "INVDocuments");

                DirectoryInfo d = new DirectoryInfo(UploadPath);
                var Image = d.GetFiles(InvoiceId + ".*").OrderByDescending(f => f.FullName).FirstOrDefault();

                if (Image == null)
                    ReturnResult = "#";
                else
                    ReturnResult = Image.ToString();

                return new JsonResult()
                {
                    Data = new { Message = "Success", FileName = ReturnResult }
                };


            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }


        }

        [HttpPost]
        public ActionResult GetPVDocName(int Id, string type)
        {
            string ReturnResult = string.Empty;

            string UploadRoot = Helper.GetStorageRoot;
            string UploadPath = string.Empty;
            PayVoucher payVoucher = db.PayVouchers.Find(Id);

            try
            {
                UploadPath = Path.Combine(UploadRoot, type);

                DirectoryInfo d = new DirectoryInfo(UploadPath);
                var Image = d.GetFiles(Id + ".*").OrderByDescending(f => f.FullName).FirstOrDefault();

                if (Image == null)
                    ReturnResult = "#";
                else
                    ReturnResult = Image.ToString();

            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }
            return new JsonResult()
            {
                Data = new { Message = "Success", FileName = ReturnResult }
            };


        }

        [HttpPost]
        public ActionResult GetMedicalBillDetail(string EmployeeNumber)
        {

            try
            {
                string ReturnResult = string.Empty;
                List<string> MedicalBillNo = new List<string>();

                var MedicalBills = db.PayVouchers.Where(w => w.Payment_To == EmployeeNumber && w.Payment_Head == "21").Select(s => s.Payment_Head_Remarks).ToList();

                if (MedicalBills == null)
                    ReturnResult = "No Previous Record";
                else
                {
                    foreach (string medicalBillNo in MedicalBills)
                    {
                        string[] split = medicalBillNo.Split(',');

                        foreach (string item in split)
                        {
                            MedicalBillNo.Add(item);
                        }
                    }
                }

                if (MedicalBillNo.Count > 0)
                {
                    return new JsonResult()
                    {
                        Data = new { Message = "Success", MedicalBillNos = MedicalBillNo.ToArray() }
                    };
                }
                else
                {
                    return new JsonResult()
                    {
                        Data = new { Message = "Success", MedicalBillNos = ReturnResult }
                    };
                }
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }
        }

        [HttpPost]
        public ActionResult GetPetrolKMDetail()
        {
            var request = HttpContext.Request;
            string P_EmployeeNo = string.Empty;
            try
            {
                P_EmployeeNo = request.Params["P_EmployeeNo"].ToString();
            }
            catch (Exception e)
            {

            }

            var ResultList = Helper.GetPetrolKMDetailVM("5", P_EmployeeNo);

            return Json(ResultList);
        }

        [HttpPost]
        public ActionResult GetFeeDetailView()
        {
            var request = HttpContext.Request;
            string P_InvoiceId = string.Empty;
            try
            {
                P_InvoiceId = request.Params["InvoiceId"].ToString();
            }
            catch (Exception e)
            {

            }

            var ResultList = Helper.GetFeeDetailView(Convert.ToInt32(P_InvoiceId));

            return Json(ResultList);
        }

        [HttpPost]
        public ActionResult GetCaseInvoiceDetail()
        {
            var request = HttpContext.Request;
            int CaseId = 0;
            try
            {
                CaseId = Convert.ToInt32(request.Params["P_CaseId"]);
            }
            catch (Exception e)
            {

            }

            var ResultList = Helper.GetCaseInvoiceDetail(CaseId);

            return Json(ResultList);
        }

        [HttpPost]
        public ActionResult LoadClientByClassification(string ClientClassificationId)
        {
            string[] values = new[] { "-2", "-1", "0" };
            List<MasterSetups> lst = db.MasterSetup.Where(m => m.MstParentId == 241 && m.Remarks == ClientClassificationId && !values.Contains(m.Mst_Value)).ToList();

            //selecting the desired columns
            var subCategoryToReturn = lst.Select(S => new
            {
                Mst_Value = S.Mst_Value,
                Mst_Desc = S.Mst_Desc
            });
            //returning JSON
            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult LoadPayFor(string P_Remark)
        {
            var ResultList = Helper.LoadPayFor(P_Remark);

            //selecting the desired columns
            var subCategoryToReturn = ResultList.Select(S => new
            {
                Mst_Value = S.Mst_Value,
                Mst_Desc = S.Mst_Desc
            });
            //returning JSON
            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult LoadCaseLevelByCaseId(string OfficeFileNo)
        {
            var RetList = Helper.GetCaseLevelList("D");

            //selecting the desired columns
            var subCategoryToReturn = RetList.Select(S => new
            {
                Mst_Value = S.Mst_Value,
                Mst_Desc = S.Mst_Desc
            });
            //returning JSON
            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);

            /*
            var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == OfficeFileNo).FirstOrDefault();
            if (courtCase != null)
            {
                using (var context = new RBACDbContext())
                {
                    List<MasterSetups> ResultList = new List<MasterSetups>();
                    SqlParameter pCaseId = new SqlParameter("@CaseId", courtCase.CaseId);

                    ResultList = context.Database.SqlQuery<MasterSetups>("GetCaseLevelForInvoice @CaseId", pCaseId).OrderBy(o => o.DisplaySeq).ToList();

                    //selecting the desired columns
                    var subCategoryToReturn = ResultList.Select(S => new
                    {
                        Mst_Value = S.Mst_Value,
                        Mst_Desc = S.Mst_Desc
                    });
                    //returning JSON
                    return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var Result = new MasterSetups();
                return this.Json(Result, JsonRequestBehavior.AllowGet);
            }
            */
        }

        [HttpPost]
        public ActionResult GetRefNumberCourt(string Id, string type, int CaseId)
        {
            string CourtRefNo = string.Empty;
            string LocName = string.Empty;
            try
            {
                if (type == "" && Convert.ToInt32(Id) > 2)
                {
                    if (Convert.ToInt32(Id) <= 6) //(PRIMARY / APEAL / SUPREME / ENFORCEMENT)
                    {
                        try
                        {
                            if (Convert.ToInt32(Id) == 6) //(ENFORCEMENT)
                            {
                                var CaseDetail = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();
                                if (CaseDetail != null)
                                {
                                    LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == CaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                                    CourtRefNo = string.Format(@"{0}^{1}", CaseDetail.EnforcementNo, LocName);

                                }

                            }
                            else //(PRIMARY / APEAL / SUPREME )
                            {
                                var CaseDetail = db.CourtCasesDetail.Where(w => w.CaseId == CaseId && w.CaseLevelCode == Id).FirstOrDefault();
                                if (CaseDetail != null)
                                {
                                    LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == CaseDetail.CourtLocationid).FirstOrDefault().Mst_Desc;
                                    CourtRefNo = string.Format(@"{0}^{1}", CaseDetail.CourtRefNo, LocName);

                                }

                            }

                        }
                        catch (Exception e)
                        {
                            CourtRefNo = "";

                        }
                    }
                }
                else if (type == "ENF" && Convert.ToInt32(Id) > 0) //ENFORCEMENT
                {
                    try
                    {
                        var vRdoEnfInvoiceInfo = db.CourtCasesEnforcement.Where(w => w.CaseId == CaseId).FirstOrDefault();

                        if (Id == "1")
                            CourtRefNo = vRdoEnfInvoiceInfo.ArrestOrderNo;
                        else if (Id == "2")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.PrimaryObjectionNo;
                        }
                        else if (Id == "3")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.ApealObjectionNo;
                        }
                        else if (Id == "4")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.SupremeObjectionNo;
                        }
                        else if (Id == "5")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.PrimaryPlaintNo;
                        }
                        else if (Id == "6")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.ApealPlaintNo;
                        }
                        else if (Id == "7")
                        {
                            CourtRefNo = vRdoEnfInvoiceInfo.SupremePlaintNo;
                        }
                    }
                    catch (Exception e)
                    {
                        CourtRefNo = "";

                    }
                }
                else //BEFORE COURT
                {
                    try
                    {
                        var vRdoBeforeCourt = db.CourtCase.Find(CaseId);

                        if (Id == "1")
                        {
                            LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == vRdoBeforeCourt.PoliceStation).FirstOrDefault().Mst_Desc;
                            CourtRefNo = string.Format(@"{0}^{1}", vRdoBeforeCourt.PoliceNo, LocName);
                        }
                        else if (Id == "2")
                        {
                            LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == vRdoBeforeCourt.PublicPlace).FirstOrDefault().Mst_Desc;
                            CourtRefNo = string.Format(@"{0}^{1}", vRdoBeforeCourt.PublicProsecutionNo, LocName);
                        }
                        else if (Id == "3")
                        {
                            LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == vRdoBeforeCourt.PAPCPlace).FirstOrDefault().Mst_Desc;
                            CourtRefNo = string.Format(@"{0}^{1}", vRdoBeforeCourt.PAPCNo, LocName);
                        }
                        else if (Id == "4")
                        {
                            LocName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Location && w.Mst_Value == vRdoBeforeCourt.LaborConflicPlace).FirstOrDefault().Mst_Desc;
                            CourtRefNo = string.Format(@"{0}^{1}", vRdoBeforeCourt.LaborConflicNo, LocName);
                        }
                    }
                    catch (Exception e)
                    {
                        CourtRefNo = "";
                    }
                }


                return new JsonResult()
                {
                    Data = new { Message = "Success", RetResult = CourtRefNo }
                };


            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }


        }

        [HttpPost]
        public ActionResult LoadFeeTypeDetail(string Id)
        {
            List<MasterSetups> lst = Helper.GetFeeTypeCascadeDetail(Id);

            //selecting the desired columns
            var subCategoryToReturn = lst.Select(S => new
            {
                Mst_Value = S.Mst_Value,
                Mst_Desc = S.Mst_Desc
            });
            //returning JSON
            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult LoadFileStatusddl(string Id)
        {
            List<MasterSetups> lst = Helper.GetFileStatusList(Id, false);

            //selecting the desired columns
            var subCategoryToReturn = lst.OrderBy(o => o.DisplaySeq).Select(S => new
            {
                Mst_Value = S.Mst_Value,
                Mst_Desc = S.Mst_Desc
            });
            //returning JSON
            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult GetRegisterCourtDetail(string OfficeFileNo, string ActionLevel)
        {
            CaseRegistrationVM RetResult = new CaseRegistrationVM();

            var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == OfficeFileNo).FirstOrDefault();

            if (courtCase != null)
            {
                var _courtCasesDetail = db.CourtCasesDetail.Where(w => w.CaseId == courtCase.CaseId && w.Courtid == ActionLevel).FirstOrDefault();

                if (_courtCasesDetail != null)
                {
                    RetResult.DetailId = _courtCasesDetail.DetailId;
                    RetResult.CaseId = _courtCasesDetail.CaseId;
                    RetResult.CourtReg_RegNo = _courtCasesDetail.CourtRefNo;
                    RetResult.CourtReg_RegCourt = _courtCasesDetail.CourtLocationid;
                    RetResult.CourtReg_RegDate = _courtCasesDetail.RegistrationDate;
                    RetResult.CourtReg_Regby = _courtCasesDetail.ApealByWho;
                    RetResult.CourtReg_ClaimAmount = courtCase.ClaimAmount;
                }
                else
                    RetResult.DetailId = 0;
            }
            return this.Json(RetResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCaseDetailByCaseId(string OfficeFileNo)
        {
            PayVoucher payVoucher = new PayVoucher();
            string RegistrationLevel = string.Empty;

            var courtCase = db.CourtCase.Where(w => w.OfficeFileNo == OfficeFileNo).FirstOrDefault();

            if (courtCase != null)
            {
                payVoucher.CaseId = courtCase.CaseId;
                payVoucher.OfficeFileNo = courtCase.OfficeFileNo;
                payVoucher.ClientName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Client && w.Mst_Value == courtCase.ClientCode).FirstOrDefault().Mst_Desc;
                payVoucher.Defendant = courtCase.Defendant;
                payVoucher.AccountContractNo = courtCase.AccountContractNo;
                payVoucher.ClientFileNo = courtCase.ClientFileNo;
                payVoucher.ReceptionDate = courtCase.ReceptionDate?.ToString("dd/MM/yyyy");
                payVoucher.ClaimAmount = courtCase.ClaimAmount;

                var caseRegisteredExists = db.CaseRegistration.Where(w => w.CaseId == courtCase.CaseId).Count();

                if (caseRegisteredExists > 0)
                {
                    if (caseRegisteredExists > 1)
                    {
                        var caseRegistered = db.CaseRegistration.Where(w => w.CaseId == courtCase.CaseId).ToList();
                        RegistrationLevel = string.Format(@"<p>PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN FOLLOWING ACTION LEVEL</p>", courtCase.OfficeFileNo);
                        string RegistrationLevelDetail = @"<ul>";

                        foreach (var items in caseRegistered)
                        {
                            string ActionLevelName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.ActionLevel && w.Mst_Value == items.ActionLevel).FirstOrDefault().Mst_Desc;

                            RegistrationLevelDetail += string.Format(@"<li>{0}</li>", ActionLevelName);
                        }
                        RegistrationLevelDetail += @"</ul>";
                        RegistrationLevel += RegistrationLevelDetail;
                    }
                    else
                    {
                        var caseRegistered = db.CaseRegistration.Where(w => w.CaseId == courtCase.CaseId).FirstOrDefault();
                        string ActionLevelName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.ActionLevel && w.Mst_Value == caseRegistered.ActionLevel).FirstOrDefault().Mst_Desc;
                        RegistrationLevel = string.Format(@"PLEASE NOTE FILE NO {0} IS ALREADY REGISTERED IN {1} LEVEL", courtCase.OfficeFileNo, ActionLevelName);
                    }
                }
            }
            payVoucher.RegistrationLevel = RegistrationLevel;

            //returning JSON
            return this.Json(payVoucher, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetExistingOfficeInTBR(string OfficeFileNo, string DataForTable)
        {
            try
            {
                string spName = @"[dbo].[REG_IN_PROG-GetExistingCase]";
                List<string> parName = new List<string>() { "@P_OfficeFileNo", "@P_DataForTable" };
                List<object> parValues = new List<object>() { OfficeFileNo, DataForTable };

                DataSet ds = Helper.getDataSet(parName.ToArray(), parValues.ToArray(), TableNames: new string[] { "data", "summary" }, procedureName: spName);
                DataTable dt = ds.Tables["data"];
                DataTable Summarydt = ds.Tables["summary"];

                var caseRegisteredExists = Summarydt.Rows.Count > 0 ? int.Parse(Summarydt.Rows[0]["caseRegisteredExists"].ToString()) : 0;

                var jsondata = dt.ToDictionary();

                return new JsonResult()
                {
                    Data = new { jsondata, caseRegisteredExists, ProcessFlag = "Y" }
                };
                //return Json(new { data = jsondata, caseRegisteredExists = caseRegisteredExists }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { ProcessMessage = e.Message, ProcessFlag = "N" }
                };
            }

        }
        [HttpPost]
        public ActionResult GetPayVoucherCreatedList()
        {
            var request = HttpContext.Request;
            string OfficeFileNo = string.Empty;
            try
            {
                OfficeFileNo = request.Params["P_OfficeFileNo"].ToString();
            }
            catch (Exception e)
            {

            }

            var ResultList = Helper.GetPayVoucherCreatedVM(OfficeFileNo);

            return Json(ResultList);

        }

        [HttpPost]
        public ActionResult GetEnforcementDetail(int CaseId, string EnforcemetDispute)
        {
            PayVoucher payVoucher = Helper.GetEnforcementDetail(CaseId, EnforcemetDispute);
            return this.Json(payVoucher, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult AjaxIndexDataPV(string DataFor)
        {
            var request = HttpContext.Request;
            List<PVListForIndex> data = new List<PVListForIndex>();

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;

            try
            {
                if (DataFor == "INTRA")
                {
                    data = Helper.GetPVList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor).ToList();
                    recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
                }
                else
                {
                    DataFor = string.Empty;
                    try
                    {
                        DataFor = request.Params["DataTableName"].ToString();
                        data = Helper.GetPVList(sortcoloumnIndex, start, searchvalue, Length, sortDirection, User.Identity.Name, DataFor).ToList();
                        recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;

                    }
                    catch (Exception e)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetSessionRollId(int CaseId, string CaseLevel)
        {
            return new JsonResult()
            {
                Data = new { SessionRollId = Helper.GetSessionRollId(CaseId, CaseLevel) }
            };
        }

        [HttpPost]
        public ActionResult GetRegisterId(int CaseId)
        {
            return new JsonResult()
            {
                Data = new { CaseRegistrationId = Helper.GetRegisterId(CaseId) }
            };
        }

        public ActionResult GetDefendentTransferData()
        {
            var request = HttpContext.Request;
            List<DefendentTransferDTO> data = new List<DefendentTransferDTO>();

            int Userid = HttpContext.User.Identity.GetUserId();
            int CaseId = int.Parse(request.Params["CaseId"].ToString());
            string CaseLevel = request.Params["CaseLevel"].ToString();

            data = Helper.GetDefendentTransfer(CaseId, CaseLevel);

            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var searchvalue = Request["search[value]"] ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var sortDirection = Request["order[0][dir]"] ?? "asc";
            var recordsTotal = 0;

            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetDetailTable()
        {
            var request = HttpContext.Request;
            string DataFor = string.Empty;
            int SessionRollId = 0;

            try { DataFor = request.Params["DataFor"].ToString(); } catch (Exception e) { }
            try { SessionRollId = int.Parse(request.Params["SessionRollId"].ToString()); } catch (Exception e) { }

            var ResultList = Helper.GetDetailTable(DataFor, SessionRollId);

            if (DataFor == "CASEHIST")
            {
                return Json(ResultList.DataTableToList<CourtDecisionHistoryDTO>());
            }

            return Json(ResultList);
        }

        [HttpPost]
        public ActionResult ProcessDefendentTransferData(DefendentTransferDTO objDTO)
        {
            try
            {
                //if (objDTO.MoneyTrRequestDate.HasValue)
                //{
                //    var courtCasesEnforcement = db.CourtCasesEnforcement.Where(w => w.CaseId == objDTO.CaseId).OrderByDescending(O => O.EnforcementId).FirstOrDefault();
                //    db.Entry(courtCasesEnforcement).Entity.MoneyTrRequestDate = objDTO.MoneyTrRequestDate;
                //    db.Entry(courtCasesEnforcement).State = EntityState.Modified;
                //    db.SaveChanges();
                //}

                objDTO.Userid = HttpContext.User.Identity.GetUserId();
                DataTable _result = Helper.ProcessDefendentTransfer(objDTO);

                string ProcessFlag = _result.Rows[0]["ProcessFlag"].ToString();
                string ProcessMessage = _result.Rows[0]["ProcessMessage"].ToString();


                return new JsonResult()
                {
                    Data = new { ProcessMessage, ProcessFlag }
                };
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { ProcessMessage = e.Message, ProcessFlag = "N" }
                };
            }

        }

        [HttpPost]
        public ActionResult CheckDuplicateDetail(string ValueToSearch, string TypeToSearch)
        {
            try
            {
                string strResult = Helper.checkDuplicateCaseFile(ValueToSearch, TypeToSearch);
                return Json(new { Message = strResult });
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {

            return new JsonResult
            {
                Data = "Beat Generated"
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessReset(int CaseId, string ResetName)
        {
            try
            {
                string strResult = Helper.ProcessResetYesNo(CaseId, ResetName);

                if (string.IsNullOrEmpty(strResult))
                    return Json(new { redirectTo = "SUCCESS" });
                else
                    return Json(new { errorMessage = strResult });
            }
            catch (Exception e)
            {
                return Json(new { errorMessage = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult submitTranslation(DecisionTranslationVM modal)
        {
            try
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                if (ModelState.IsValid)
                {
                    CourtCases courtCases = db.CourtCase.Find(modal.CaseId);
                    DecisionTranslation decisionTranslation = db.DecisionTranslation.Find(modal.TranslationId);

                    db.Entry(courtCases).Entity.CourtDecision = modal.CourtDecision;
                    db.Entry(courtCases).Entity.UpdateBoxDate = DateTime.UtcNow.AddHours(4);
                    db.Entry(courtCases).Entity.UpdateBoxBy = User.Identity.GetUserId();
                    db.Entry(courtCases).State = EntityState.Modified;

                    db.Entry(decisionTranslation).Entity.CourtDecision = modal.CourtDecision;
                    db.Entry(decisionTranslation).Entity.CourtDecisionTranslated = modal.CourtDecisionTranslated;
                    db.Entry(decisionTranslation).Entity.TranslationDone = true;
                    db.Entry(decisionTranslation).State = EntityState.Modified;

                    db.SaveChanges();


                    return Json(new { Category = "OK", Message = "Data Save" });
                }
                else
                    return Json(new { Category = "Error", Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()) });

            }
            catch (Exception e)
            {
                return Json(new { Category = "Error", Message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateUserPassword(string Password)
        {
            try
            {
                string UserName = User.Identity.Name;
                var model = new LoginViewModel();
                model.UserName = UserName;
                model.Password = Password;

                List<string> _errors = new List<string>();
                string ValidUserMessage = "";

                int USER_ID = 0;
                using (var context = new RBACDbContext())
                {
                    var users = context.Users.Where(w => w.UserName == model.UserName && !w.Inactive).FirstOrDefault();
                    if (users == null)
                    {
                        ValidUserMessage = "YOUR ACCOUNT IS BLOCKED, CONTACT YOUR ADMIN";
                        _errors.Add(ValidUserMessage);
                    }
                    else
                        USER_ID = users.Id;
                }

                if (USER_ID > 0)
                {

                    RBACStatus _retVal = this.Login(model, this.UserManager, this.SignInManager, out _errors);

                    switch (_retVal)
                    {
                        case RBACStatus.Success:
                            {
                                return Json(new { Category = "OK", Message = "Valid User" }, JsonRequestBehavior.AllowGet);
                            }
                        default:
                            return Json(new { Category = "Error", Message = "Invalid Password" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return Json(new { Category = "Error", Message = ValidUserMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Category = "Error", ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetSessionNotes(int P_SessionRollId)
        {
            var SessionNotes = Helper.GetSessionRemarks(P_SessionRollId);
            return this.Json(SessionNotes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadOfficeFileStatusddl(string Id, string IsDisute = null)
        {
            List<MasterSetups> lst = new List<MasterSetups>();

            if (string.IsNullOrEmpty(IsDisute))
            {
                lst = Helper.GetOfficeFileStatus(Id == "N" ? OfficeFileFilterTBR : OfficeFileFilterAJP);
            }
            else
            {
                lst = Helper.GetOfficeFileStatus(Id == "N" ? OfficeFileFilterTBR : OfficeFileFilterENF);
            }
            var subCategoryToReturn = lst.Select(S => new
            {
                S.Mst_Value,
                S.Mst_Desc
            });

            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadCourtByGovernorateDDL(string Id)
        {
            List<MasterSetups> lst = Helper.GetCourtByGovernorate(Id);

            var subCategoryToReturn = lst.Select(S => new
            {
                S.Mst_Value,
                S.Mst_Desc
            });

            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadDependentDDL(string Id, string SkipIds = null)
        {
            List<MasterSetups> lst = Helper.LoadDependentDDL(Id, SkipIds);

            var subCategoryToReturn = lst.Select(S => new
            {
                S.Mst_Value,
                S.Mst_Desc
            });

            return this.Json(subCategoryToReturn, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileClosure(ToBeRegisterVM modal, HttpPostedFileBase upload)
        {
            try
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                CourtCases courtCases = db.CourtCase.Where(w => w.CaseId == modal.CaseId).FirstOrDefault();
                string UploadRoot = Helper.GetStorageRoot;

                if (courtCases == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    ClosurePartialDetail closurePartialDetail = new ClosurePartialDetail();

                    switch (modal.FileClosureDetail.btnSave)
                    {
                        #region "ACCOUNTS DEPT PROCEEDURE"
                        case "btnClosureAccountsDepSave":

                            db.Entry(courtCases).Entity.AccountAuditBy = User.Identity.Name;
                            db.Entry(courtCases).Entity.AccountAuditDate = modal.FileClosureDetail.AccountAuditDate;
                            db.Entry(courtCases).Entity.CaseStatus = modal.FileClosureDetail.StatusCode;
                            db.Entry(courtCases).Entity.FinalOnvoiceOnHold = modal.FileClosureDetail.FinalOnvoiceOnHold;
                            db.Entry(courtCases).Entity.SuggestedDate = modal.FileClosureDetail.SuggestedDate;
                            db.Entry(courtCases).Entity.JudRecRedStamp = modal.FileClosureDetail.JudRecRedStamp;

                            break;
                        case "btnClosurePartialAccountsDepSave":
                            closurePartialDetail = db.ClosurePartialDetail.Find(modal.FileClosureDetail.PartDetailId);
                            if (closurePartialDetail == null)
                                return HttpNotFound();
                            else
                            {
                                closurePartialDetail.ClosureApproveddBy = User.Identity.Name;
                                closurePartialDetail.AccountAuditDate = modal.FileClosureDetail.AccountAuditDate;
                            }
                            break;
                        #endregion

                        #region "FINANCE FILE CLOSURE"
                        case "btnClosureAccountsFinSave":

                            db.Entry(courtCases).Entity.FinanceFileClosureDate = modal.FileClosureDetail.FinanceFileClosureDate;

                            break;
                        #endregion

                        #region "STORE THE FILE IN THE ARCHIVE"
                        case "btnClosureArchiveSave":

                            db.Entry(courtCases).Entity.StoreDate = modal.FileClosureDetail.StoreDate;
                            db.Entry(courtCases).Entity.ClosureArchieveddBy = User.Identity.Name;
                            //db.Entry(courtCases).Entity.StoreNotes = modal.FileClosureDetail.StoreNotes;

                            break;

                        case "btnClosurePartialArchiveSave":

                            closurePartialDetail = db.ClosurePartialDetail.Find(modal.FileClosureDetail.PartDetailId);
                            if (closurePartialDetail == null)
                                return HttpNotFound();
                            else
                            {
                                closurePartialDetail.ClosureArchieveddBy = User.Identity.Name;
                                closurePartialDetail.StoreDate = modal.FileClosureDetail.StoreDate;
                                closurePartialDetail.FileAllocation = modal.FileClosureDetail.FileAllocation;
                            }

                            break;
                        #endregion

                        #region "FINAL CLOSURE REQUEST"
                        case "btnFinalClosureSave":

                            CourtCaseStatusDetail StatusDetail = new CourtCaseStatusDetail();
                            StatusDetail.CaseId = modal.FileClosureDetail.CaseId;
                            StatusDetail.StatusCode = modal.FileClosureDetail.StatusCode;
                            StatusDetail.StatusDate = modal.FileClosureDetail.StatusDate;
                            StatusDetail.ReasonCode = modal.FileClosureDetail.ReasonCode;
                            StatusDetail.CurrentCaseLevel = courtCases.CaseLevelCode;
                            StatusDetail.ClosureLevel = courtCases.CaseLevelCode;
                            StatusDetail.CreatedBy = User.Identity.GetUserId();
                            StatusDetail.CreatedOn = DateTime.UtcNow.AddHours(4);
                            db.CourtCaseStatusDetail.Add(StatusDetail);
                            db.SaveChanges();

                            db.Entry(courtCases).Entity.ReasonCode = modal.FileClosureDetail.ReasonCode;
                            db.Entry(courtCases).Entity.OfficeFileStatus = modal.FileClosureDetail.ReasonCode;
                            db.Entry(courtCases).Entity.ClosureDate = modal.FileClosureDetail.StatusDate;
                            db.Entry(courtCases).Entity.ClosingNotes = modal.ClosingNotes;
                            db.Entry(courtCases).Entity.ClosingNotesDate = modal.ClosingNotesDate;
                            db.Entry(courtCases).Entity.ClosedbyStaff = User.Identity.Name;
                            db.Entry(courtCases).Entity.FinalClosureApprovalStatus = "0";
                            db.Entry(courtCases).Entity.FinalClosureInitiatedBy = User.Identity.Name;

                            if (upload != null && upload.ContentLength > 0)
                            {
                                string FileExtension = Path.GetExtension(upload.FileName);

                                string FileName = courtCases.CaseId + FileExtension;

                                string UploadPath = Path.Combine(UploadRoot, "ClosingEmail", FileName);

                                upload.SaveAs(UploadPath);
                            }

                            break;
                        #endregion

                        #region "FILE CLOSURE CONFIRM"
                        case "btnFinalClosurApprovaleSave":
                            db.Entry(courtCases).Entity.FinalClosureApproveddBy = User.Identity.Name;
                            db.Entry(courtCases).Entity.FinalClosureApprovalStatus = modal.FileClosureDetail.ClosureApprovalStatus;

                            break;
                        #endregion
                        
                        #region "PART OF FILE CLOSURE"
                        case "btnClosurePartialSave":
                            int PartNumber = db.ClosurePartialDetail.Where(w => w.CaseId == modal.CaseId).Count() + 1;

                            
                            closurePartialDetail.CaseId = modal.FileClosureDetail.CaseId;
                            closurePartialDetail.ClosurePartDate = modal.FileClosureDetail.ClosurePartDate;
                            closurePartialDetail.FileTypeClosure = modal.FileClosureDetail.FileTypeClosure;
                            closurePartialDetail.PartNo = PartNumber.ToString();
                            closurePartialDetail.ClosingNotesDate = modal.FileClosureDetail.ClosingNotesDate;
                            closurePartialDetail.ClosingNotes = modal.FileClosureDetail.ClosingNotes;
                            closurePartialDetail.ClosureInitiatedBy = User.Identity.Name;

                            db.ClosurePartialDetail.Add(closurePartialDetail);
                            db.SaveChanges();

                            break;
                            #endregion
                    }

                    if (modal.FileClosureDetail.btnSave.In("btnClosureAccountsDepSave", "btnClosureAccountsFinSave", "btnClosureArchiveSave", "btnFinalClosureSave", "btnFinalClosurApprovaleSave"))
                    {
                        db.Entry(courtCases).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    if (modal.FileClosureDetail.btnSave.In("btnClosurePartialAccountsDepSave", "btnClosurePartialArchiveSave"))
                    {
                        db.Entry(closurePartialDetail).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    if (courtCases.CaseStatus == "2")
                    {
                        Helper.ProcessCaseRegistrationProgress(courtCases, "FileClosureEnteryVM");
                    }

                    ViewBag.FileClosureCategory = new SelectList(Helper.GetFileClosureCategory(), "Mst_Value", "Mst_Desc", modal.PartialViewName);

                    switch (modal.PartialViewName)
                    {
                        #region "FINAL CLOSURE REQUEST"
                        case "ClosureFinal":

                            ViewBag.FileAllocation = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FileAllocation), "Mst_Value", "Mst_Desc", modal.FileClosureDetail.FileAllocation);
                            ViewBag.FinalOnvoiceOnHold = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.FileClosureDetail.FinalOnvoiceOnHold);
                            ViewBag.JudRecRedStamp = new SelectList(Helper.GetYesForSelect(), "Mst_Value", "Mst_Desc", modal.FileClosureDetail.JudRecRedStamp);
                            ViewBag.AccountAudit = new SelectList(Helper.GetAccountAuditSelect(), "Mst_Value", "Mst_Desc");
                            ViewBag.FileArchieved = new SelectList(Helper.GetStoreArchievedSelect(), "Mst_Value", "Mst_Desc");

                            string ClosingEmailDoc = Helper.GetClosingEmail_Doc(modal.CaseId);

                            if (ClosingEmailDoc == "#")
                            {
                                ViewBag.ViewClosingEmail = "AppHidden";
                            }
                            else
                            {
                                ViewBag.ViewClosingEmail = "";
                                ViewBag.ClosingEmailDoc = ClosingEmailDoc;
                            }

                            break;
                        #endregion

                        #region "PART OF FILE CLOSURE"
                        case "ClosurePartial":
                            ViewBag.FileTypeClosure = new SelectList(Helper.GetFileTypeClosure(), "Mst_Value", "Mst_Desc", modal.FileClosureDetail.FileTypeClosure);
                            ViewBag.FileAllocation = new SelectList(db.MasterSetup.Where(m => m.MstParentId == (int)MASTER_S.FileAllocation), "Mst_Value", "Mst_Desc", modal.FileClosureDetail.FileAllocation);
                            ViewBag.AccountAudit = new SelectList(Helper.GetAccountAuditSelect(), "Mst_Value", "Mst_Desc");
                            ViewBag.FileArchieved = new SelectList(Helper.GetStoreArchievedSelect(), "Mst_Value", "Mst_Desc");

                            

                            break;
                            #endregion
                    }

                    return Json(new { Category = modal.PartialViewName, Message = "" }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { Category = "Error", Message = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray()) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Category = "Error", ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ClosureFormView(int CaseId, string screen = null)
        {
            ClosureFormView data = new ClosureFormView();
            if (screen.In("ClosureFinalForm", "ClosurePartialForm"))
                data = Helper.GetClosureFormData(CaseId, screen);

            return View(data);
        }

        [HttpPost]
        public ActionResult SentWhatsApp(string mobileId, string message)
        {
            var options = new RestClientOptions("https://whatsapp247.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/send.php", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("api_key", "96891196581-c2cabd9d-8935-4b71-80e7-9665ba9c986a");
            request.AddParameter("mobile", "923351353873");
            request.AddParameter("message", "Dear Customer This Message is for testing from Application as your Payment has been proceed Dated 23/11/2023");
            RestResponse response = client.Execute(request);

            return Json(new { Category = "OK", Message = "Data Save" });
        }

        [HttpPost]
        public ActionResult TransferEmployeeToIndivisual(string PayMst_Value)
        {
            string BankName = string.Empty;
            string jsonRemarks = string.Empty;
            int MstParentId = 1903;
            MasterSetups ItemToBeAdd = new MasterSetups();
            try
            {
                if (!string.IsNullOrEmpty(PayMst_Value))
                {
                    var Employee = db.Employees.Where(w => w.EmployeeNumber == PayMst_Value).FirstOrDefault();

                    if (Employee != null)
                    {
                        string PayToMstDesc = Employee.FullName;
                        string PayToBankName = Employee.BankName;
                        string PayToAccountNumber = Employee.AccountNumber;
                        string PayToEmail = Employee.Email;
                        string PayToContactNo = Employee.ContactNumber;
                        string PayToMessageLang = Employee.MessageLang;

                        string MessageLangName = string.Empty;

                        if (!string.IsNullOrEmpty(PayToMessageLang))
                            MessageLangName = Helper.GetLangForSelect().Where(w => w.Mst_Value == PayToMessageLang).FirstOrDefault().Mst_Desc.ToUpper();

                        if (!string.IsNullOrEmpty(PayToBankName))
                            BankName = db.MasterSetup.Where(w => w.MstParentId == (int)MASTER_S.Banks && w.Mst_Value == PayToBankName).FirstOrDefault().Mst_Desc;


                        PayToRemarks objRemarks = new PayToRemarks();
                        objRemarks.PayToAccountNumber = PayToAccountNumber;
                        objRemarks.PayToBankCode = PayToBankName;
                        objRemarks.PayToBankName = BankName;
                        objRemarks.PayToContactNo = PayToContactNo;
                        objRemarks.PayToEmail = PayToEmail;
                        objRemarks.MessageLang = PayToMessageLang;
                        objRemarks.MessageLangName = MessageLangName;

                        jsonRemarks = JsonConvert.SerializeObject(objRemarks);

                        string[] values = new[] { "-2", "-1", "0" };

                        var ItemToBeAddList = db.MasterSetup.Where(m => m.MstParentId == MstParentId && !values.Contains(m.Mst_Value)).ToList();
                        int Mst_Value = 1;
                        int DisplaySeq = 1;

                        if (ItemToBeAddList.Count > 0)
                        {
                            Mst_Value = ItemToBeAddList.Select(s => Convert.ToInt32(s.Mst_Value.Replace("P", ""))).Max() + 1;
                            DisplaySeq = ItemToBeAddList.Select(s => Convert.ToInt32(s.DisplaySeq)).Max() + 5;
                        }
                       
                        ItemToBeAdd.MstParentId = MstParentId;
                        ItemToBeAdd.Mst_Desc = PayToMstDesc;

                        string ItemToBeAddValue = string.Format(@"P{0}", Mst_Value.ToString());

                        ItemToBeAdd.Mst_Value = ItemToBeAddValue;
                        ItemToBeAdd.Active_Flag = true;
                        ItemToBeAdd.DisplaySeq = DisplaySeq;
                        ItemToBeAdd.Remarks = jsonRemarks;

                        db.MasterSetup.Add(ItemToBeAdd);
                        db.SaveChanges();

                        db.Entry(Employee).Entity.DOR = DateTime.UtcNow.AddHours(4);

                        db.Entry(Employee).State = EntityState.Modified;
                        db.SaveChanges();

                        var User = db.Users.Where(w => w.UserName == PayMst_Value).FirstOrDefault();
                        db.Entry(User).Entity.Inactive = true;
                        db.Entry(User).Entity.LastModified = DateTime.UtcNow.AddHours(4);

                        db.Entry(User).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return new JsonResult()
                {
                    Data = new { Message = "Success" }
                };
            }
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { Message = "Error", ErrorMessage = e.Message }
                };

            }
        }
        [HttpPost]
        public ActionResult GetTop5PVByPayment()
        {
            var recordsTotal = 0;
            string PaymentTo = string.Empty;
            string Beneficiary = string.Empty;
            string ProcedureName = @"GetTop5PVByBeneficiary";
            var request = HttpContext.Request;
            try { PaymentTo = request.Params["PaymentTo"].ToString(); } catch (Exception ex) { PaymentTo = string.Empty; }
            try { Beneficiary = request.Params["Beneficiary"].ToString(); } catch (Exception ex) { Beneficiary = string.Empty; }

            List<string> parName = new List<string>();
            List<object> parValues = new List<object>();
            parName.AddRange(new[] { "@UserName", "@PaymentTo", "@Beneficiary" });
            parValues.AddRange(new[] { User.Identity.Name, PaymentTo, Beneficiary });

            var ds = Helper.getDataSet(parName.ToArray(), parValues.ToArray(), TableNames: new string[] { "data" }, procedureName: ProcedureName);

            DataTable dt = ds.Tables["data"];

            var jsondata = dt.ToDictionary();

            recordsTotal = dt.Rows.Count;

            return Json(new { data = jsondata, recordsTotal = recordsTotal }, JsonRequestBehavior.AllowGet);
        }
    }
}