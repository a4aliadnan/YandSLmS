<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A4InvoiceTemplate.aspx.cs" Inherits="YandS.UI.Template.A4InvoiceTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        table.MsoTableGridMain {
            border: solid windowtext 1.0pt;
            font-size: 11.0pt;
            font-family: "Calibri","sans-serif";
        }

        div.MsoNormalMain {
            margin-top: 0in;
            margin-right: 0in;
            margin-bottom: 8.0pt;
            margin-left: 0in;
            line-height: 107%;
            font-size: 11.0pt;
            font-family: "Calibri","sans-serif";
        }
        table.MsoTableGridInnerOne
	{border:solid windowtext 1.0pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
 p.MsoNormalInnerOne
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:8.0pt;
	margin-left:0in;
	line-height:107%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
 
table.MsoTableGridInnerTwo
	{border:solid windowtext 1.0pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
 p.MsoNormalInnerTwo
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:8.0pt;
	margin-left:0in;
	line-height:107%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
  
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" class="MsoTableGridMain" style="border-collapse: collapse; mso-table-layout-alt: fixed; border: none; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in; mso-border-insideh: none; mso-border-insidev: none">
                <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 1.5in; mso-height-rule: exactly">
                    <td style="width: 35.75pt; padding: 0in 0in 0in 0in; height: 1.5in; mso-height-rule: exactly" valign="top" width="48">
                        <p class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal"></p>
                    </td>
                    <td style="width: 7.0in; padding: 0in 0in 0in 0in; height: 1.5in; mso-height-rule: exactly" valign="top" width="672">
                        <p class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal"></p>
                    </td>
                    <td style="width: 40.7pt; padding: 0in 0in 0in 0in; height: 1.5in; mso-height-rule: exactly" valign="top" width="54">
                        <p class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal"></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow: 1; mso-yfti-lastrow: yes; height: 9.5in; mso-height-rule: exactly">
                    <td style="width: 35.75pt; padding: 0in 0in 0in 0in; height: 9.5in; mso-height-rule: exactly" valign="top" width="48">
                        <p class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal">
                        </p>
                    </td>
                    <td style="width: 7.0in; padding: 0in 0in 0in 0in; height: 9.5in; mso-height-rule: exactly" valign="top" width="672">
                        <div class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal">
                                        <table border="0" cellpadding="0" cellspacing="0" class="MsoTableGridInnerOne" style="border-collapse:collapse;mso-table-layout-alt:fixed;border:none;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 none;mso-border-insidev:none" width="686">
                <tr style="mso-yfti-irow:0;mso-yfti-firstrow:yes">
                    <td colspan="5" style="width:514.7pt;padding:0in 5.4pt 0in 5.4pt" valign="top" width="686">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <b><span style="font-size:22.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">Html.DisplayFor(model =&gt; model.ClientName)</span></b></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:1">
                    <td colspan="5" style="width:514.7pt;padding:0in 5.4pt 0in 5.4pt" valign="top" width="686">
                        <p class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:2;height:.4in;mso-height-rule:exactly">
                    <td style="width: 125.75pt; border: solid windowtext 1.0pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .4in; mso-height-rule: exactly" width="168">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">DATE</span></b></p>
                    </td>
                    <td style="width:.6in;border:none;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;
  height:.4in;mso-height-rule:exactly" width="58">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width: 145.8pt; border: solid windowtext 1.0pt; border-left: none; mso-border-left-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .4in; mso-height-rule: exactly" width="194">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">INVOICE NO.</span></b></p>
                    </td>
                    <td style="width:.6in;border:none;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;
  height:.4in;mso-height-rule:exactly" width="58">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width: 163.95pt; border: solid windowtext 1.0pt; border-left: none; mso-border-left-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .4in; mso-height-rule: exactly" width="219">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">FILE NO.</span></b></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:3;height:.3in">
                    <td style="width:125.75pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in" width="168">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">model.InvoiceDate)</span></p>
                    </td>
                    <td style="width:.6in;border:none;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;
  height:.3in" width="58">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width:145.8pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in" width="194">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">model.InvoiceNumber)</span></p>
                    </td>
                    <td style="width:.6in;border:none;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;
  height:.3in" width="58">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width:163.95pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in" width="219">
                        <p align="center" class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                            <span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">model.OfficeFileNo)</span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:4;mso-yfti-lastrow:yes">
                    <td colspan="5" style="width:514.7pt;padding:0in 5.4pt 0in 5.4pt" valign="top" width="686">
                        <p class="MsoNormalInnerOne" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                </tr>
            </table>

            <table border="1" cellpadding="0" cellspacing="0" class="MsoTableGrid" style="border-collapse:collapse;mso-table-layout-alt:fixed;border:none;
 mso-border-alt:solid windowtext 1.5pt;mso-yfti-tbllook:1184;mso-padding-alt:
 0in 5.75pt 0in 5.75pt;mso-border-insideh:.25pt solid windowtext;mso-border-insidev:
 .25pt solid windowtext">
                <tr style="mso-yfti-irow:0;mso-yfti-firstrow:yes;height:.35in">
                    <td style="width: 121.25pt; border-top: 1.5pt; border-left: 1.5pt; border-bottom: 1.0pt; border-right: 1.0pt; border-color: windowtext; border-style: solid; mso-border-top-alt: 1.5pt; mso-border-left-alt: 1.5pt; mso-border-bottom-alt: .25pt; mso-border-right-alt: .25pt; mso-border-color-alt: windowtext; mso-border-style-alt: solid; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">DEFENDANT</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:3.25in;border-top:solid windowtext 1.5pt;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .25pt;mso-border-alt:solid windowtext .25pt;
  mso-border-top-alt:solid windowtext 1.5pt;padding:0in 5.75pt 0in 5.75pt;
  height:.35in" width="312">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">Html.DisplayFor(model =&gt; model.Defendant)</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:147.75pt;border-top:solid windowtext 1.5pt;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.5pt;
  mso-border-left-alt:solid windowtext .25pt;mso-border-top-alt:1.5pt;
  mso-border-left-alt:.25pt;mso-border-bottom-alt:.25pt;mso-border-right-alt:
  1.5pt;mso-border-color-alt:windowtext;mso-border-style-alt:solid;padding:
  0in 5.75pt 0in 5.75pt;height:.35in" width="197">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">model.CaseAgainst)</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:1;height:.35in">
                    <td style="width: 121.25pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext .25pt; mso-border-alt: solid windowtext .25pt; mso-border-left-alt: solid windowtext 1.5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">CONTRACT DETAIL</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:3.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;padding:0in 5.75pt 0in 5.75pt;
  height:.35in" width="312">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">Html.DisplayFor(model =&gt; model.AccountContractNo)</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:147.75pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;mso-border-right-alt:solid windowtext 1.5pt;
  padding:0in 5.75pt 0in 5.75pt;height:.35in" width="197">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">model.ClientFileNo)</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:2;height:.35in">
                    <td style="width: 121.25pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext .25pt; mso-border-alt: solid windowtext .25pt; mso-border-left-alt: solid windowtext 1.5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">COURT DETAILS</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:3.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;padding:0in 5.75pt 0in 5.75pt;
  height:.35in" width="312">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">ViewBag.CourtType<o:p></o:p></span></p>
                    </td>
                    <td style="width:147.75pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;mso-border-right-alt:solid windowtext 1.5pt;
  padding:0in 5.75pt 0in 5.75pt;height:.35in" width="197">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">model.CourtRefNo)<o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:3;height:.35in">
                    <td style="width: 121.25pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext .25pt; mso-border-alt: solid windowtext .25pt; mso-border-left-alt: solid windowtext 1.5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">CASE DETAILS</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:3.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;padding:0in 5.75pt 0in 5.75pt;
  height:.35in" width="312">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">ViewBag.CaseTypeName<o:p></o:p></span></p>
                    </td>
                    <td style="width:147.75pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;mso-border-right-alt:solid windowtext 1.5pt;
  padding:0in 5.75pt 0in 5.75pt;height:.35in" width="197">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">ViewBag.ClientCaseTypeN<o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:4;height:.35in">
                    <td style="width: 121.25pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext .25pt; mso-border-alt: solid windowtext .25pt; mso-border-left-alt: solid windowtext 1.5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">ENFORCEMENT STAGE</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td style="width:3.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;padding:0in 5.75pt 0in 5.75pt;
  height:.35in" width="312">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">model.EnforcementStageName)<o:p></o:p></span></p>
                    </td>
                    <td style="width:147.75pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-alt:solid windowtext .25pt;mso-border-right-alt:solid windowtext 1.5pt;
  padding:0in 5.75pt 0in 5.75pt;height:.35in" width="197">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">model.EnforcementStageNo)<o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:5;mso-yfti-lastrow:yes;height:.35in">
                    <td style="width: 121.25pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext .25pt; mso-border-top-alt: .25pt; mso-border-left-alt: 1.5pt; mso-border-bottom-alt: 1.5pt; mso-border-right-alt: .25pt; mso-border-color-alt: windowtext; mso-border-style-alt: solid; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.75pt 0in 5.75pt; height: .35in" width="162">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size: 12.0pt; mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">CLAIM AMOUNT</span><span style="font-size:12.0pt"><o:p></o:p></span></p>
                    </td>
                    <td colspan="2" style="width:381.75pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext .25pt;mso-border-left-alt:solid windowtext .25pt;
  padding:0in 5.75pt 0in 5.75pt;height:.35in" width="509">
                        <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                            <span style="font-size:12.0pt;mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">@ViewBag.ClaimAmount&amp;nbsp;OMR<o:p></o:p></span></p>
                    </td>
                </tr>
            </table>

                        </div>
                    </td>
                    <td style="width: 40.7pt; padding: 0in 0in 0in 0in; height: 9.5in; mso-height-rule: exactly" valign="top" width="54">
                        <p class="MsoNormalMain" style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal">
                        </p>
                    </td>
                </tr>
            </table>
</div>
    </form>
</body>
</html>
