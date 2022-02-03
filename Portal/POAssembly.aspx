<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POAssembly.aspx.cs" Inherits="avii.POAssembly" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Generate Container ID
			</td>
		</tr>
    
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
	        <td>
             <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
        </tr> 
     </table>
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
         <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">   
            <tr>
                <td class="copy10grey"  align="right">
                   ESN#:
                </td>
                <td>
                <asp:TextBox ID="txtESN"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   BOX#:
                </td>
                <td>
                <asp:TextBox ID="txtBoxNo"   CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>   
                </tr>
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
            </table>
            </asp:Panel>
        
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>

                </td>
            </tr>
            </table>
            
       
    
          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBoxID" />
        </Triggers>
        </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
    </form>
</body>
</html>
