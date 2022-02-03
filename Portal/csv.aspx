<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csv.aspx.cs" Inherits="avii.csv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
<table width="760" cellpadding="0" cellspacing="0" border="0" align="center">
			<tr>
				<td>

				</td>
			</tr>
			<tr>
				<td style="height: 123px">
                    <table>
                        <tr>
                            <td>PO#:
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtPO" runat="server"></asp:TextBox>
                              </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSubmit" runat="server" Text="Get CSV Data" OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 222px">
                                <asp:TextBox ID="txtCsv" runat="server" TextMode="MultiLine" Width="745px" Height="516px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
				</td>
			</tr>
			<tr>
				<td>
					
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
