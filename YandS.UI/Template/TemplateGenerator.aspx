<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateGenerator.aspx.cs" Inherits="YandS.UI.Template.TemplateGenerator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 35.1pt;
            width: 196pt;
            color: white;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #002060;
        }
        .auto-style2 {
            width: 126pt;
            color: white;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #002060;
        }
        .auto-style3 {
            width: 168pt;
            color: white;
            font-size: 12.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #002060;
        }
        .auto-style4 {
            height: 43.9pt;
            width: 112pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style5 {
            width: 280pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style6 {
            width: 98pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style7 {
            height: 21.95pt;
            width: 280pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style8 {
            height: 24.95pt;
            width: 112pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style9 {
            width: 140pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style10 {
            width: 182pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style11 {
            width: 98pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style12 {
            width: 280pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style13 {
            height: 30.0pt;
            width: 112pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style14 {
            height: 24.95pt;
            width: 112pt;
            color: white;
            font-size: 11.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #002060;
        }
        .auto-style15 {
            width: 280pt;
            color: red;
            font-size: 14.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style16 {
            width: 98pt;
            color: white;
            font-size: 11.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #002060;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;width:490pt" width="665">
                <colgroup>
                    <col span="35" style="mso-width-source:userset;mso-width-alt:694; width:14pt" width="19" />
                </colgroup>
                <tr height="46" style="mso-height-source:userset;height:35.1pt">
                    <td class="auto-style1" colspan="14" height="46" width="266">ENFORCEMENT UPDATE</td>
                    <td class="auto-style2" colspan="9" width="171">@OfficeFileNo</td>
                    <td class="auto-style3" colspan="12" dir="RTL" width="228">مستجدات التنفيذ</td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style4" colspan="8" height="58" rowspan="2" width="152">DEFENDANT</td>
                    <td class="auto-style5" colspan="20" width="380">@DefendentName</td>
                    <td class="auto-style6" colspan="7" dir="RTL" rowspan="2" width="133">الخصم</td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style7" colspan="20" dir="RTL" height="29" width="380">@الديفيندينت</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">CONTRACT NO.</td>
                    <td class="auto-style9" colspan="10" width="190">@AccountNo</td>
                    <td class="auto-style9" colspan="10" width="190">@ClientFileNo</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">رقم العقد</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">CLAIM AMOUNT</td>
                    <td class="auto-style10" colspan="13" width="247">@AgainstName</td>
                    <td class="auto-style11" colspan="7" width="133">@ClaimAmt</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">مبلغ المطالبة</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">ENFC REG. DATE</td>
                    <td class="auto-style12" colspan="20" width="380">RegistrationDate</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">تاريخ تسجيل التنفيذ</td>
                </tr>
                <tr height="40" style="mso-height-source:userset;height:30.0pt">
                    <td class="auto-style13" colspan="8" height="40" width="152">ENFC DETAILS</td>
                    <td class="auto-style9" colspan="10" width="190">@EnforcementNo</td>
                    <td class="auto-style9" colspan="10" width="190">@EnforcementCourt</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">بيانات التنفيذ</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">ENFORCEMENT STAGE<span style="mso-spacerun:yes">&nbsp;</span></td>
                    <td class="auto-style12" colspan="20" width="380">@Enforcementstage</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">مرحلة التنفيذ</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">PROPERTIES</td>
                    <td class="auto-style12" colspan="20" width="380">@PropertyDEtail</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">العقارات</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style8" colspan="8" height="33" width="152">UPDATE</td>
                    <td class="auto-style12" colspan="20" width="380">@CourtDecision</td>
                    <td class="auto-style6" colspan="7" dir="RTL" width="133">المستجدات</td>
                </tr>
                <tr height="33" style="mso-height-source:userset;height:24.95pt">
                    <td class="auto-style14" colspan="8" height="33" width="152">REQUIREMENT</td>
                    <td class="auto-style15" colspan="20" width="380">@Requirement</td>
                    <td class="auto-style16" colspan="7" dir="RTL" width="133">المطلوب</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
