<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDiscartedSKU.aspx.cs" Inherits="avii.DiscardedSKU.ManageDiscartedSKU" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Discarded SKU</title>
    <script type="text/javascript">

     
        function isNumberKey(evt) {
            var receivedQty = document.getElementById("<% =txtQty.ClientID %>").value;
            //alert(receivedQty.length);
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (receivedQty.length < 1) {
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) {
                    charCodes = 0;
                    return false;
                }
                //return true;
            }
            else
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
            return true;
        }
    </script>
       
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"> 
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
            &nbsp;&nbsp;Manage Discarded SKU
			</td>
		</tr>
    </table>
     <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlSKU" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td >
                <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            </td>
        </tr>             
     </table> 
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSubmit" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
            <tr>
                <td  class="copy10grey" align="right" width="15%">
                        Customer: &nbsp;</td>
                <td align="left"  width="35%">
                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="70%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>

                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right" width="15%">
                    SKU#:
                </td>
                <td  width="35%">
                    <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey"  Width="62%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSKU_SelectedIndexChanged"  ></asp:DropDownList>
                 
                </td>
            </tr>
            
                <tr id="trSKU" visible="false" runat="server">
                    <td colspan="5">
                    
                     <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
			            <td  bgcolor="#dee7f6" class="buttonlabel">
                        &nbsp;&nbsp;Discarded SKU
			            </td>
		            </tr>
                    <tr>
                        <%--<td width="10%">

                        </td>--%>
                        
                        <td width="100%">

                        
                     <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="buttonlabel" width="20%">
                                Category Name           
                        </td>
                        <td class="buttonlabel" width="20%">
                            SKU
                        </td>
                        
                        <td class="buttonlabel" width="30%">
                                 Product Name
                                               
                        </td>
                        <td class="buttonlabel" width="10%">
                            Current Stock
                        </td>

                    </tr>
                    <tr>
                        <td class="copy10grey" >
                            <asp:Label ID="lblCategoryName" runat="server" CssClass="copy10grey" ></asp:Label>                  
                        </td>
                        <td class="copy10grey" >
                            <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" ></asp:Label>                  
                        </td>
                        <td class="copy10grey" >
                            <asp:Label ID="lblItemName" runat="server" CssClass="copy10grey" ></asp:Label>                  
                        </td>
                        
                        <td class="copy10grey" >
                            <asp:Label ID="lblCurrentStock" runat="server" CssClass="copy10grey" ></asp:Label>                  
                        </td>
                    </tr>
                    </table>
                            </td>
                        
<%--                        <td width="10%">

                        </td>--%>
                    </tr>
                    </table>
                    </td>
                </tr>
            <tr>
                <td class="copy10grey"  align="right">
                  Module:
                </td>
                <td>
                     <asp:DropDownList ID="ddlModule" runat="server" CssClass="copy10grey" Width="60%" >
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Adjustment" Value="Adjustment"></asp:ListItem>                        
                    </asp:DropDownList>
                    
                                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Requested By :
                </td>
                <td>
                            <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="62%">
                        </asp:DropDownList>       
                </td>   
                </tr>
            <tr>
                <td class="copy10grey"  align="right">
                   Quantity:
                </td>
                <td>
                     <asp:TextBox ID="txtQty" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="5"  Width="70%"></asp:TextBox>
                                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Approved:
                </td>
                <td>
                      <asp:CheckBox ID="chkAproveLan" CssClass="copy10grey" Checked="true" Text="" runat="server" >
                      </asp:CheckBox>                                              
                </td>   
                </tr>    
            <tr valign="top">
                <td valign="top" class="copy10grey"  align="right">
                   Comment:
                </td>
                <td colspan="4">
                     <asp:TextBox ID="txtComments" runat="server"  TextMode="MultiLine" CssClass="copy10grey" Rows="4"   Width="85%"></asp:TextBox>
                                
                </td>
                <%--<td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Requested By :
                </td>
                <td>
                     
                </td>--%>   
                </tr>    

                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <div class="loadingcss" align="center" id="modalSending" style="display:none">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" CausesValidation="false"  OnClientClick="return ShowSendingProgress();"/>
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>  
        
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>--%>
        </asp:UpdatePanel>
	
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
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
