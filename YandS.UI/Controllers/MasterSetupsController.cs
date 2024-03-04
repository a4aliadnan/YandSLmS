using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YandS.UI.Models;

namespace YandS.UI.Controllers
{
    [RBAC]
    public class MasterSetupsController : Controller
    {
        private RBACDbContext db = new RBACDbContext();

        public ActionResult Index(int? id)
        {

            #region Loading Code
            /*Load Master Data only for First Time*/

            //IList<MasterSetups> defaultMasters = new List<MasterSetups>();

            //defaultMasters.Add(new MasterSetups() { MstId = 1, Mst_Desc = "CourtType", Mst_Value = "1", DisplaySeq = 1, Remarks = "Created Court Type Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 2, Mst_Desc = "Banks", Mst_Value = "2", DisplaySeq = 1, Remarks = "Created Banks Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 3, Mst_Desc = "AccoutTitle", Mst_Value = "3", DisplaySeq = 1, Remarks = "Created Branch Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 4, Mst_Desc = "Location", Mst_Value = "4", DisplaySeq = 1, Remarks = "Created Location Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 5, Mst_Desc = "AccountNumber", Mst_Value = "5", DisplaySeq = 1, Remarks = "Created Court Type Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 6, Mst_Desc = "Gender", Mst_Value = "6", DisplaySeq = 1, Remarks = "Created AccountNumber Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 7, Mst_Desc = "PaymentHeads", Mst_Value = "7", DisplaySeq = 1, Remarks = "Created Gender Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 8, Mst_Desc = "PaymentMode", Mst_Value = "8", DisplaySeq = 1, Remarks = "Created PaymentHeads Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 9, Mst_Desc = "PaymentType", Mst_Value = "9", DisplaySeq = 1, Remarks = "Created PaymentMode Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 10, Mst_Desc = "ChequeStatus", Mst_Value = "10", DisplaySeq = 1, Remarks = "Created ChequeStatus Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 11, Mst_Desc = "CourtLocation", Mst_Value = "11", DisplaySeq = 1, Remarks = "Created CourtLocation Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 12, Mst_Desc = "CaseAgainst", Mst_Value = "12", DisplaySeq = 1, Remarks = "Created CaseAgainst Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 13, Mst_Desc = "ReceiveLevel", Mst_Value = "13", DisplaySeq = 1, Remarks = "Created ReceiveLevel Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 14, Mst_Desc = "CaseType", Mst_Value = "14", DisplaySeq = 1, Remarks = "Created CaseType Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 15, Mst_Desc = "CaseLevel", Mst_Value = "15", DisplaySeq = 1, Remarks = "Created CaseLevel Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 16, Mst_Desc = "ODBLoan", Mst_Value = "16", DisplaySeq = 1, Remarks = "Created ODBLoan Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 17, Mst_Desc = "CourtDepartment", Mst_Value = "17", DisplaySeq = 1, Remarks = "Created CourtDepartment Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstId = 18, Mst_Desc = "ODB Bank Branch", Mst_Value = "18", DisplaySeq = 1, Remarks = "Created ODB Bank Branch Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            //db.MasterSetup.AddRange(defaultMasters);
            //db.SaveChanges();


            ///*Load Detail Master Data only for First Time*/

            //IList<MasterSetups> defaultMasters = new List<MasterSetups>();

            ///*Court Type Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 1, Mst_Desc = "Please Select Court", Mst_Value = "0", DisplaySeq = 0, Remarks = "Created PrimaryCourt Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 1, Mst_Desc = "Primary Court", Mst_Value = "1", DisplaySeq = 1, Remarks = "Created PrimaryCourt Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 1, Mst_Desc = "Appeal Court", Mst_Value = "2", DisplaySeq = 2, Remarks = "Created AppealCourt Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 1, Mst_Desc = "Supreme Court", Mst_Value = "3", DisplaySeq = 3, Remarks = "Created SupremeCourt Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 1, Mst_Desc = "Enforcement Court", Mst_Value = "4", DisplaySeq = 4, Remarks = "Created EnforcementCourt Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Bank List Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Please Select Bank", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "National Bank of Oman", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank Muscat", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank Dhofar", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "HSBC Bank Oman", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "First Abu Dhabi Bank", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Oman Arab Bank", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Sohar International.", Mst_Value = "7", DisplaySeq = 7, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Ahli Bank.", Mst_Value = "8", DisplaySeq = 8, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank Nizwa.", Mst_Value = "9", DisplaySeq = 9, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Al Izz Islamic Bank", Mst_Value = "10", DisplaySeq = 10, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Habib Bank Limited", Mst_Value = "11", DisplaySeq = 11, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank Melli Iran", Mst_Value = "12", DisplaySeq = 12, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank of Baroda", Mst_Value = "13", DisplaySeq = 13, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Bank Saderat Iran", Mst_Value = "14", DisplaySeq = 14, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Standard Chartered Bank", Mst_Value = "15", DisplaySeq = 15, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 2, Mst_Desc = "Qatar National Bank", Mst_Value = "16", DisplaySeq = 16, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Account Titles Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "Please Select", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "CORPORATE ACCOUNT MUSCAT", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "CORPORATE A/C SALALAH", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "CONSULTATION ACCOUNT", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "NAJAHI A/C MUSCAT", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "NAJAHI A/C SALALAH", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 3, Mst_Desc = "MEETHAQ A/C", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Location List Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Please Select", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Muscat", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Muttrah", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Bawshar", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Seeb", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Amarat", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Qurayyat", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Khasab", Mst_Value = "7", DisplaySeq = 7, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Bukha", Mst_Value = "8", DisplaySeq = 8, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Daba Al Bayah", Mst_Value = "9", DisplaySeq = 9, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Madha", Mst_Value = "10", DisplaySeq = 10, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Nizwa", Mst_Value = "11", DisplaySeq = 11, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Samail", Mst_Value = "12", DisplaySeq = 12, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Bahla", Mst_Value = "13", DisplaySeq = 13, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Adam", Mst_Value = "14", DisplaySeq = 14, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Hamra", Mst_Value = "15", DisplaySeq = 15, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Manah", Mst_Value = "16", DisplaySeq = 16, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Izki", Mst_Value = "17", DisplaySeq = 17, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Bidbid", Mst_Value = "18", DisplaySeq = 18, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Ibri", Mst_Value = "19", DisplaySeq = 19, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Dhank", Mst_Value = "20", DisplaySeq = 20, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Younqil", Mst_Value = "21", DisplaySeq = 21, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Sohar", Mst_Value = "22", DisplaySeq = 22, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Shinas", Mst_Value = "23", DisplaySeq = 23, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Liwa", Mst_Value = "24", DisplaySeq = 24, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Saham", Mst_Value = "25", DisplaySeq = 25, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Khaburah", Mst_Value = "26", DisplaySeq = 26, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Suwayq", Mst_Value = "27", DisplaySeq = 27, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Nakhal", Mst_Value = "28", DisplaySeq = 28, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Wadi Al Maawil", Mst_Value = "29", DisplaySeq = 29, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Awabi", Mst_Value = "30", DisplaySeq = 30, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Musanaah", Mst_Value = "31", DisplaySeq = 31, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Barka", Mst_Value = "32", DisplaySeq = 32, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Rustaq", Mst_Value = "33", DisplaySeq = 33, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Buraimi", Mst_Value = "34", DisplaySeq = 34, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Mahdah", Mst_Value = "35", DisplaySeq = 35, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Sunynah", Mst_Value = "36", DisplaySeq = 36, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Haima", Mst_Value = "37", DisplaySeq = 37, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Duqm", Mst_Value = "38", DisplaySeq = 38, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Mahout", Mst_Value = "39", DisplaySeq = 39, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Jazur", Mst_Value = "40", DisplaySeq = 40, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Ibra", Mst_Value = "41", DisplaySeq = 41, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al-Mudhaibi", Mst_Value = "42", DisplaySeq = 42, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Bidiyah", Mst_Value = "43", DisplaySeq = 43, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Wadi Bani Khaled", Mst_Value = "44", DisplaySeq = 44, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Dema Wa Thaieen", Mst_Value = "45", DisplaySeq = 45, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Qabil", Mst_Value = "46", DisplaySeq = 46, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Masirah", Mst_Value = "47", DisplaySeq = 47, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Sur", Mst_Value = "48", DisplaySeq = 48, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Jalan Bani Bu Hassan", Mst_Value = "49", DisplaySeq = 49, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Jalan Bani Bu Ali", Mst_Value = "50", DisplaySeq = 50, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Al Kamil Wal Wafi", Mst_Value = "51", DisplaySeq = 51, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Salalah", Mst_Value = "52", DisplaySeq = 52, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Taqah", Mst_Value = "53", DisplaySeq = 53, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Mirbat", Mst_Value = "54", DisplaySeq = 54, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Thumrait", Mst_Value = "55", DisplaySeq = 55, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Sadah", Mst_Value = "56", DisplaySeq = 56, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Rakhyut", Mst_Value = "57", DisplaySeq = 57, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Dhalkut", Mst_Value = "58", DisplaySeq = 58, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 4, Mst_Desc = "Muqshin", Mst_Value = "59", DisplaySeq = 59, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Gender List Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 6, Mst_Desc = "Please Select Gender", Mst_Value = "0", DisplaySeq = 0, Remarks = "Created Male Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 6, Mst_Desc = "Male", Mst_Value = "1", DisplaySeq = 1, Remarks = "Created Male Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 6, Mst_Desc = "Female", Mst_Value = "2", DisplaySeq = 2, Remarks = "Created Female Master Table Entry", Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Payment Head Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "Please Select Payment Head", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "COURT FEES", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "STATIONARY", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "LAWYER TRANSPORTION", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "MAINTENANCE", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "PETROL", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "SALARY/BONUS", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "AL YAHYAEI A/C/ PARTNER'S CUT", Mst_Value = "7", DisplaySeq = 7, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "OFFICE RENT", Mst_Value = "8", DisplaySeq = 8, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "HOUSE RENT", Mst_Value = "9", DisplaySeq = 9, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "BANK TO BANK", Mst_Value = "10", DisplaySeq = 10, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "ELECTRICITY& WARER", Mst_Value = "11", DisplaySeq = 11, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "TELEPHONE/ WIFI", Mst_Value = "12", DisplaySeq = 12, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "LOAN", Mst_Value = "13", DisplaySeq = 13, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "ROP", Mst_Value = "14", DisplaySeq = 14, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "CAR RENT", Mst_Value = "15", DisplaySeq = 15, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "CAR/OFFICE MAINTAINANCE", Mst_Value = "16", DisplaySeq = 16, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "BANK'S COMMISSION", Mst_Value = "17", DisplaySeq = 17, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "HOTEL", Mst_Value = "18", DisplaySeq = 18, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "AIR TICKETS", Mst_Value = "19", DisplaySeq = 19, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "MARKETING/ ADVERTISING", Mst_Value = "20", DisplaySeq = 20, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "MEDICAL EXP.", Mst_Value = "21", DisplaySeq = 21, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "MR.SUPHI TRANSFER", Mst_Value = "22", DisplaySeq = 22, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "POSTAGE", Mst_Value = "23", DisplaySeq = 23, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 7, Mst_Desc = "PRIVATE MR MOHAMMED", Mst_Value = "24", DisplaySeq = 24, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Payment Mode Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 8, Mst_Desc = "Please Select Payment Mode", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 8, Mst_Desc = "Cheque", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 8, Mst_Desc = "Cash", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Payment Type Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 9, Mst_Desc = "Please Select Payment Type", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 9, Mst_Desc = "Normal", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 9, Mst_Desc = "Bank Transfer", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Cheque Status Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 10, Mst_Desc = "Please Select Cheque Status", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 10, Mst_Desc = "Cleared", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 10, Mst_Desc = "Bounce", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 10, Mst_Desc = "Not Deposit", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Case Against Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 12, Mst_Desc = "Please Select Case Against", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 12, Mst_Desc = "INDIVDUAL", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 12, Mst_Desc = "COMPANY", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 12, Mst_Desc = "AGANIST OUR CLIENT", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Receive Level Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 13, Mst_Desc = "Please Select Receive Level", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 13, Mst_Desc = "COURT LEVELS", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 13, Mst_Desc = "ENFORCEMENT", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Case Type Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "Please Select Case Type", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "COMMERCIAL", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "LABOR", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "CIVIL", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "CRIMINAL", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 14, Mst_Desc = "FAMILY LAW", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Case Level Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Please Select Case Level", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "TO BE REGISTER", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "BEFORE COURT", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "PRIMARY COURT", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "APPEAL COURT", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "SUPREME COURT", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "ENFORCEMENT", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Different Jurisdication Primary", Mst_Value = "7", DisplaySeq = 7, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Different Jurisdication Appeal", Mst_Value = "8", DisplaySeq = 8, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Supreme /2", Mst_Value = "9", DisplaySeq = 9, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "GOVERMENT Primary", Mst_Value = "10", DisplaySeq = 10, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Plaint Primary", Mst_Value = "11", DisplaySeq = 11, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Plaint Appeal", Mst_Value = "12", DisplaySeq = 12, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Other", Mst_Value = "13", DisplaySeq = 13, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Petition Primary", Mst_Value = "14", DisplaySeq = 14, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Petition Appeal", Mst_Value = "15", DisplaySeq = 15, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 15, Mst_Desc = "Close file", Mst_Value = "16", DisplaySeq = 16, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*ODB Loan Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 16, Mst_Desc = "Please Select ODB Loan", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 16, Mst_Desc = "GOVERMENT", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 16, Mst_Desc = "ODB", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 16, Mst_Desc = "SANAD", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 16, Mst_Desc = "RAFD", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*Court Department Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 17, Mst_Desc = "Please Select Court Department", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 17, Mst_Desc = "SINGLE", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 17, Mst_Desc = "THREE JUDGES", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            ///*ODB Bank Branch Loading*/
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "Please Select ODB Bank Branch", Mst_Value = "0", DisplaySeq = 0, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "MUSCAT BRANCH", Mst_Value = "1", DisplaySeq = 1, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "SEEB BRANCH", Mst_Value = "2", DisplaySeq = 2, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "AL BATINAH STATE", Mst_Value = "3", DisplaySeq = 3, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "AL SHARQIYAH STATE", Mst_Value = "4", DisplaySeq = 4, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "AL WUSTA STATE", Mst_Value = "5", DisplaySeq = 5, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "LEGAL DEPARTMENT", Mst_Value = "6", DisplaySeq = 6, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "MIRBAT", Mst_Value = "7", DisplaySeq = 7, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "SALALAH", Mst_Value = "8", DisplaySeq = 8, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });
            //defaultMasters.Add(new MasterSetups() { MstParentId = 18, Mst_Desc = "SOHAR", Mst_Value = "9", DisplaySeq = 9, Active_Flag = true, CreatedBy = 1, CreatedOn = DateTime.Now });

            //db.MasterSetup.AddRange(defaultMasters);
            //db.SaveChanges();

            #endregion

            if (id > 0)
            {
                ViewBag.ItemID = id;

                var masterSetups = db.MasterSetup.Include(m => m.Created).Include(m => m.Modified).Where(p => p.MstParentId == id);
                ViewBag.ParentDescription = masterSetups.Any() ? masterSetups.FirstOrDefault().Mst_Desc : null;
                return View(masterSetups.ToList());

            }
            else
            {
                id = null;
                var masterSetups = db.MasterSetup;
                ViewBag.ParentDescription = "Master Table";
                return View(masterSetups.Where(p => p.MstParentId == id).ToList());

            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSetups masterSetups = db.MasterSetup.Find(id);
            if (masterSetups == null)
            {
                return HttpNotFound();
            }
            return View(masterSetups);
        }

        public ActionResult Create(int? id)
        {

            if (id != null)
            {
                MasterSetups masterSetups = db.MasterSetup.Find(id);
                MasterSetups Detail = new MasterSetups();
                Detail.MstParentId = masterSetups.MstId;
                Detail.Active_Flag = true;
                ViewBag.ItemID = masterSetups.MstId;

                ViewBag.ParentDescription = masterSetups.Mst_Desc;
                if (masterSetups == null)
                {
                    return HttpNotFound();
                }
                return View(Detail);

            }
            return View();
        }

        // POST: MasterSetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSetups masterSetups)
        {

            //MasterSetups objSave = new MasterSetups();
            //MasterSetups objforeignKey = new MasterSetups();

            //objSave.Mst_Desc = masterSetups.Mst_Desc;
            //objSave.Mst_Value = masterSetups.Mst_Value;
            //objSave.DisplaySeq = masterSetups.DisplaySeq;
            //objSave.Remarks = masterSetups.Remarks;
            //objSave.Active_Flag = masterSetups.Active_Flag;

            //objforeignKey.MstId = masterSetups.Parent.MstId;
            //objforeignKey.Mst_Desc = masterSetups.Mst_Desc;
            //objforeignKey.Mst_Value = masterSetups.Mst_Value;
            //objforeignKey.DisplaySeq = masterSetups.DisplaySeq;
            //objforeignKey.Remarks = masterSetups.Remarks;
            //objforeignKey.Active_Flag = masterSetups.Active_Flag;


            //objSave.Parent = objforeignKey;


            //db.MasterSetup.Add(objSave);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (!IsDuplicateMstValue(masterSetups))
                {
                    db.MasterSetup.Add(masterSetups);
                    db.SaveChanges();

                    if (masterSetups.MstParentId == null)
                    {
                        MasterSetups objParentTableUpdate = new MasterSetups();
                        objParentTableUpdate = db.MasterSetup.Find(masterSetups.MstId);
                        objParentTableUpdate.Mst_Value = masterSetups.MstId.ToString();

                        db.Entry(objParentTableUpdate).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    return RedirectToAction("Index", new RouteValueDictionary(new { id = masterSetups.MstParentId }));
                }
                else
                {
                    ModelState.AddModelError("Mst_Value", "Duplicate Value not allowed");
                    return View(masterSetups);
                }
            }

            return View(masterSetups);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSetups masterSetups = db.MasterSetup.Find(id);
            ViewBag.ParentDescription = masterSetups.Mst_Desc;

            if (masterSetups == null)
            {
                return HttpNotFound();
            }
            return View(masterSetups);
        }

        // POST: MasterSetups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterSetups masterSetups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterSetups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new RouteValueDictionary(new { id = masterSetups.MstParentId }));

            }
            return View(masterSetups);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSetups masterSetups = db.MasterSetup.Find(id);
            if (masterSetups == null)
            {
                return HttpNotFound();
            }
            return View(masterSetups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSetups masterSetups = db.MasterSetup.Find(id);
            db.MasterSetup.Remove(masterSetups);
            db.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { id = masterSetups.MstParentId }));
        }

        /*Duplilcate Validation*/
        public bool IsDuplicateMstValue(MasterSetups model)
        {
            int MstId = model.MstId;
            int? MstParentId = model.MstParentId;
            string Mst_Desc = model.Mst_Desc;
            string Mst_Value = model.Mst_Value;
            bool result = false;
            if (MstParentId == null)
                return result;
            else
            {
                result = db.MasterSetup.Where(w => w.MstParentId == model.MstParentId).Any(u => u.Mst_Value == model.Mst_Value);
                return result;

            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
