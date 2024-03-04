<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelTemplatePrintForm.aspx.cs" Inherits="YandS.UI.Template.ExcelTemplatePrintForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

td
	{border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-top:1px;
	        padding-right:1px;
	        padding-left:1px;
	        color:black;
	        font-size:11.0pt;
	        font-weight:400;
	        font-style:normal;
	        text-decoration:none;
	        font-family:Calibri, sans-serif;
	        text-align:general;
	        vertical-align:bottom;
	        white-space:nowrap;
	}
        .auto-style1 {
            color: black;
            font-size: 65.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style2 {
            width: 364pt;
            color: black;
            font-size: 40.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: initial !important;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style3 {
            width: 364pt;
            color: black;
            font-size: 48.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space:  initial !important;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style4 {
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 5px;
            padding-right: 1px;
            padding-top: 1px;
            background: #A6A6A6;
        }
        .auto-style5 {
            width: 224pt;
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space:  initial !important;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style6 {
            color: black;
            font-size: 18.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: right;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 5px;
            padding-top: 1px;
            background: #A6A6A6;
        }
        .auto-style7 {
            color: black;
            font-size: 8.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:532pt" width="722">
                <colgroup>
                    <col span="38" style="width:14pt" width="19" />
                </colgroup>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt;width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                    <td style="width:14pt" width="19"></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="160" style="mso-height-source:userset;height:120.0pt">
                    <td height="160" style="height:120.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style1" colspan="26">25/07/2021</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="mso-height-source:userset;height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="160" style="mso-height-source:userset;height:120.0pt">
                    <td height="160" style="height:120.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style2" colspan="26" width="494">WORK REQUIRED is new</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="mso-height-source:userset;height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="160" style="mso-height-source:userset;height:120.0pt">
                    <td height="160" style="height:120.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style3" colspan="26" width="494">NOTES NOTES NOTES NOTES</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="66" style="mso-height-source:userset;height:50.1pt">
                    <td height="66" style="height:50.1pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style4" colspan="6">FILE NO.</td>
                    <td class="auto-style5" colspan="16" width="304">S1557</td>
                    <td class="auto-style6" colspan="6" dir="RTL">رقم الملف</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="66" style="mso-height-source:userset;height:50.1pt">
                    <td height="66" style="height:50.1pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style4" colspan="6">CLIENT</td>
                    <td class="auto-style5" colspan="16" width="304">OMAN DEVELOPMENT BANK</td>
                    <td class="auto-style6" colspan="6" dir="RTL">الموكل</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="66" style="mso-height-source:userset;height:50.1pt">
                    <td height="66" style="height:50.1pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style4" colspan="6">DEFENDANT<span style="mso-spacerun:yes">&nbsp;</span></td>
                    <td class="auto-style5" colspan="16" width="304"><span style="mso-spacerun:yes">&nbsp;</span>DHAWAHI SALALAH MODERN TRD.</td>
                    <td class="auto-style6" colspan="6" dir="RTL">الخصم</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="66" style="mso-height-source:userset;height:50.1pt">
                    <td height="66" style="height:50.1pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style4" colspan="6">COURT</td>
                    <td class="auto-style5" colspan="16" width="304">MUSCAT</td>
                    <td class="auto-style6" colspan="6" dir="RTL">المحكمة</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="66" style="mso-height-source:userset;height:50.1pt">
                    <td height="66" style="height:50.1pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style4" colspan="6">JUG. DATE</td>
                    <td class="auto-style5" colspan="16" width="304">08/08/2021</td>
                    <td class="auto-style6" colspan="6" dir="RTL">تاريخ الحكم</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style7" colspan="6">6</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="auto-style7" colspan="6">08/08/2021</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr height="20" style="height:15.0pt">
                    <td height="20" style="height:15.0pt"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
