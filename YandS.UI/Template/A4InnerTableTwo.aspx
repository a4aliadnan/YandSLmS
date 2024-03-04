<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A4InnerTableTwo.aspx.cs" Inherits="YandS.UI.Template.A4InnerTableTwo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


    table.MsoTableGrid
	{border:solid windowtext 1.0pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
 p.MsoNormal
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

            <table border="0" cellpadding="0" cellspacing="0" class="MsoTableGrid" width="686">
                <tr style="mso-yfti-irow:13;height:.35in">
                    <td colspan="4" style="width: 238.15pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-alt: solid windowtext 1.5pt; mso-border-right-alt: solid windowtext .25pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="318">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">DESCRIPTION<o:p></o:p></span></b></p>
                    </td>
                    <td style="width: 1.0in; border-top: none; border-left: none; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-left-alt: solid windowtext .25pt; mso-border-top-alt: 1.5pt; mso-border-left-alt: .25pt; mso-border-bottom-alt: 1.5pt; mso-border-right-alt: .25pt; mso-border-color-alt: windowtext; mso-border-style-alt: solid; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="96">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">AMOUNT<o:p></o:p></span></b></p>
                    </td>
                    <td style="width: 42.95pt; border-top: none; border-left: none; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-left-alt: solid windowtext .25pt; mso-border-top-alt: 1.5pt; mso-border-left-alt: .25pt; mso-border-bottom-alt: 1.5pt; mso-border-right-alt: .25pt; mso-border-color-alt: windowtext; mso-border-style-alt: solid; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="57">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">VAT<o:p></o:p></span></b></p>
                    </td>
                    <td style="width: 59.4pt; border-top: none; border-left: none; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-left-alt: solid windowtext .25pt; mso-border-top-alt: 1.5pt; mso-border-left-alt: .25pt; mso-border-bottom-alt: 1.5pt; mso-border-right-alt: .25pt; mso-border-color-alt: windowtext; mso-border-style-alt: solid; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="79">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">VAT AMT<o:p></o:p></span></b></p>
                    </td>
                    <td style="width: 102.2pt; border-top: none; border-left: none; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.5pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-left-alt: solid windowtext .25pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="136">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black">TOTAL<o:p></o:p></span></b></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:14;height:.35in">
                    <td style="width:59.45pt;border-top:none;border-left:solid windowtext 1.5pt;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-alt:solid windowtext 1.5pt;
  mso-border-right-alt:solid windowtext .25pt;padding:0in 5.4pt 0in 5.4pt;
  height:.35in" width="79">
                        <p class="MsoNormal">
                            <span style="mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;
  color:black">@ViewBag</span></p>
                    </td>
                    <td style="width:67.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-top-alt:1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:
  1.5pt;mso-border-right-alt:.25pt;mso-border-color-alt:windowtext;mso-border-style-alt:
  solid;padding:0in 5.4pt 0in 5.4pt;height:.35in" width="90">
                        <p class="MsoNormal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width:39.85pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-top-alt:1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:
  1.5pt;mso-border-right-alt:.25pt;mso-border-color-alt:windowtext;mso-border-style-alt:
  solid;padding:0in 5.4pt 0in 5.4pt;height:.35in" width="53">
                        <p class="MsoNormal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width:71.65pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-top-alt:1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:
  1.5pt;mso-border-right-alt:.25pt;mso-border-color-alt:windowtext;mso-border-style-alt:
  solid;padding:0in 5.4pt 0in 5.4pt;height:.35in" width="96">
                        <p class="MsoNormal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                    <td style="width:1.0in;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;mso-border-top-alt:
  1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:1.5pt;mso-border-right-alt:
  .25pt;mso-border-color-alt:windowtext;mso-border-style-alt:solid;padding:
  0in 5.4pt 0in 5.4pt;height:.35in" width="96">
                        <p align="center" class="MsoNormal">
                            <span style="mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">@ViewBag</span></p>
                    </td>
                    <td style="width:42.95pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-top-alt:1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:
  1.5pt;mso-border-right-alt:.25pt;mso-border-color-alt:windowtext;mso-border-style-alt:
  solid;padding:0in 5.4pt 0in 5.4pt;height:.35in" width="57">
                        <p align="center" class="MsoNormal">
                            <span style="mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">@View</span></p>
                    </td>
                    <td style="width:59.4pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  mso-border-top-alt:1.5pt;mso-border-left-alt:.25pt;mso-border-bottom-alt:
  1.5pt;mso-border-right-alt:.25pt;mso-border-color-alt:windowtext;mso-border-style-alt:
  solid;padding:0in 5.4pt 0in 5.4pt;height:.35in" width="79">
                        <p align="center" class="MsoNormal">
                            <span style="mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">@Vie</span></p>
                    </td>
                    <td style="width:102.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  padding:0in 5.4pt 0in 5.4pt;height:.35in" width="136">
                        <p align="center" class="MsoNormal">
                            <span style="mso-bidi-font-family:Calibri;
  mso-bidi-theme-font:minor-latin;color:black">@ViewBag</span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:15;height:.35in">
                    <td colspan="8" style="width: 514.7pt; border: solid windowtext 1.5pt; border-top: none; mso-border-top-alt: solid windowtext 1.5pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="686">
                        <p align="center" class="MsoNormal">
                            <span style="font-size:14.0pt">GRAND TOTAL</span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:16;height:.35in">
                    <td colspan="6" style="width:353.1pt;border-top:none;border-left:
  solid windowtext 1.5pt;border-bottom:solid windowtext 1.5pt;border-right:
  solid windowtext 1.0pt;mso-border-top-alt:solid windowtext 1.5pt;mso-border-alt:
  solid windowtext 1.5pt;mso-border-right-alt:solid windowtext .25pt;
  padding:0in 5.4pt 0in 5.4pt;height:.35in" width="471">
                        <p class="MsoNormal">
                            <span style="font-size:14.0pt;mso-ascii-font-family:Calibri;
  mso-hansi-font-family:Calibri;mso-bidi-font-family:Calibri;color:black">THREE HUNDRED SEVENTEEN RIAL OMANI and SEVEN HUNDRED EIGHTY BAIZA</span><span style="font-size:12.0pt;mso-bidi-font-family:Calibri;mso-bidi-theme-font:
  minor-latin;color:black"><o:p></o:p></span></p>
                    </td>
                    <td colspan="2" style="width:161.6pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.5pt;border-right:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext 1.5pt;mso-border-left-alt:solid windowtext .25pt;
  padding:0in 5.4pt 0in 5.4pt;height:.35in" width="215">
                        <p align="center" class="MsoNormal">
                            <b><span style="font-size:16.0pt;
  mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri;mso-bidi-font-family:
  Calibri;color:black">317.78&nbsp;OMR</span></b><span style="font-size:12.0pt;
  mso-bidi-font-family:Calibri;mso-bidi-theme-font:minor-latin;color:black"><o:p></o:p></span></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:17;height:.35in">
                    <td colspan="8" style="width:514.7pt;border:none;border-bottom:solid windowtext 1.5pt;
  mso-border-top-alt:solid windowtext 1.5pt;padding:0in 5.4pt 0in 5.4pt;
  height:.35in" width="686">
                        <p align="center" class="MsoNormal">
                            <o:p>&nbsp;</o:p></p>
                    </td>
                </tr>
                <tr style="mso-yfti-irow:18;mso-yfti-lastrow:yes;height:.35in">
                    <td colspan="6" style="width: 353.1pt; border-top: none; border-left: solid windowtext 1.5pt; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.0pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-alt: solid windowtext 1.5pt; mso-border-right-alt: solid windowtext .25pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="471">
                        <p align="center" class="MsoNormal">
                            <b><span style="mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">TRANSFER TO&nbsp;NATIONAL BANK OF OMAN : 1016749891001</span></b></p>
                    </td>
                    <td colspan="2" style="width: 161.6pt; border-top: none; border-left: none; border-bottom: solid windowtext 1.5pt; border-right: solid windowtext 1.5pt; mso-border-top-alt: solid windowtext 1.5pt; mso-border-left-alt: solid windowtext .25pt; background: #D9D9D9; mso-background-themecolor: background1; mso-background-themeshade: 217; padding: 0in 5.4pt 0in 5.4pt; height: .35in" width="215">
                        <p align="center" class="MsoNormal">
                            <b><span style="mso-ascii-font-family: Calibri; mso-hansi-font-family: Calibri; mso-bidi-font-family: Calibri; color: black; background: #D9D9D9">VATIN : OM1100096787</span></b></p>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
