<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelTemplateInvoiceNewFormat.aspx.cs" Inherits="YandS.UI.Template.ExcelTemplateInvoiceNewFormat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 21.95pt;
            width: 20pt;
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style2 {
            width: 20pt;
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style3 {
            height: 21.95pt;
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style4 {
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style5 {
            color: black;
            font-size: 20.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
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
        .auto-style6 {
            color: black;
            font-size: 14.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
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
        .auto-style7 {
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style8 {
            color: black;
            font-size: 16.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style9 {
            color: black;
            font-size: 16.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
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
        .auto-style10 {
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style11 {
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style12 {
            color: black;
            font-size: 14.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .auto-style13 {
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style14 {
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style15 {
            color: black;
            font-size: 16.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style16 {
            height: 18.0pt;
            color: black;
            font-size: 12.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Times New Roman", serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style17 {
            width: 320pt;
            color: black;
            font-size: 13.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: left;
            vertical-align: middle;
            white-space: normal;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style18 {
            color: black;
            font-size: 13.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style19 {
            width: 20pt;
            color: black;
            font-size: 13.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: left;
            vertical-align: middle;
            white-space: normal;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .auto-style20 {
            color: black;
            font-size: 13.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
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
        .auto-style21 {
            color: black;
            font-size: 18.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Sakkal Majalla";
            text-align: center;
            vertical-align: top;
            white-space: nowrap;
            border: .5pt solid windowtext;
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
 collapse;width:540pt" width="702">
                <colgroup>
                    <col span="27" style="mso-width-source:userset;mso-width-alt:950;
 width:20pt" width="26" />
                </colgroup>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style1" height="29" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                    <td class="auto-style2" width="26"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5" colspan="23" rowspan="2">@Html.Partial(&quot;_CaseInvoiceClientAddress&quot;, Model)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style6" colspan="23">VATIN : OM1100002437</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style7" colspan="8">DATE</td>
                    <td class="auto-style7" colspan="8">INVOICE NO.</td>
                    <td class="auto-style7" colspan="7">YANDS NO.</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style8" colspan="8">@Html.DisplayFor(model =&gt; model.InvoiceDate)</td>
                    <td class="auto-style8" colspan="8">@Html.DisplayFor(model =&gt; model.InvoiceNumber)</td>
                    <td class="auto-style8" colspan="7">@Html.DisplayFor(model =&gt; model.OfficeFileNo)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style7" colspan="23">CASE DETAILS</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style10" colspan="6">DEFEN+ PLANT</td>
                    <td class="auto-style11" colspan="17">@Html.DisplayFor(model =&gt; model.InvoiceDate)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style10" colspan="6">CLIENTS NO.</td>
                    <td class="auto-style11" colspan="17">@Html.DisplayFor(model =&gt; model.ClientNo)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style10" colspan="6">CASE DETAILS</td>
                    <td class="auto-style11" colspan="17">@Html.DisplayFor(model =&gt; model.CaseDetail)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style10" colspan="6">CLAIM AMOUNT</td>
                    <td class="auto-style11" colspan="17">@Html.DisplayFor(model =&gt; model.ClaimAmount)</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style7" colspan="23">INVOICE DETAILS</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style12" colspan="12">DISCRIPTION</td>
                    <td class="auto-style12" colspan="4">AMOUNT</td>
                    <td class="auto-style12" colspan="3">VAT 5%</td>
                    <td class="auto-style12" colspan="4">TOTAL</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style13" colspan="12">PRIMARY - LAWYER’S FEE - FULL FEES</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style14" colspan="3">9999.999</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style13" colspan="12">PRIMARY - LAWYER’S FEE - FULL FEES</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style14" colspan="3">9999.999</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style13" colspan="12">PRIMARY - LAWYER’S FEE - FULL FEES</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style14" colspan="3">9999.999</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style13" colspan="12">PRIMARY - LAWYER’S FEE - FULL FEES</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style14" colspan="3">9999.999</td>
                    <td class="auto-style14" colspan="4">999999.999</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style15" colspan="12">TOTAL</td>
                    <td class="auto-style15" colspan="4">1000000</td>
                    <td class="auto-style15" colspan="3">10000</td>
                    <td class="auto-style15" colspan="4">1000000</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style7" colspan="23">GRAND TOTAL</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style17" colspan="16" rowspan="2" width="416">ONE THOUSAND THREE HUNDRED SIXTY-ONE RIAL OMANI AND FIFTY BAIZA ONLY</td>
                    <td class="auto-style18" colspan="7" rowspan="2">1361.025 OMR</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style19" width="26"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style7" colspan="16" rowspan="2">TRANSFER THE INVOICE AMOUNT TO</td>
                    <td class="auto-style7" colspan="7" rowspan="2">VATIN</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="24" style="mso-height-source:userset;height:18.0pt">
                    <td class="auto-style16" height="24"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style21" colspan="16">&nbsp;</td>
                    <td class="auto-style21" colspan="7">&nbsp;</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
                <tr height="29" style="mso-height-source:userset;height:21.95pt">
                    <td class="auto-style3" height="29"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                    <td class="auto-style4"></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
