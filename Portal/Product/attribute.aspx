<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attribute.aspx.cs" Inherits="avii.product.attribute" %>
  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attributes</title>
    <LINK href="../../aerostyle.css" type="text/css" rel="stylesheet">
    <link href="../Styles.css" type="text/css" rel="stylesheet" />
      <script type="text/javascript" language="javascript">
        function updateAttribute(obj) {
       
            objattributeGUID = document.getElementById(obj.id.replace('lnkEdit', 'hdnAttributeGUID0'));
            objattributeName = document.getElementById(obj.id.replace('lnkEdit', 'lblAttribute'));
            objActive=document.getElementById(obj.id.replace('lnkEdit','hdnActive'));
            document.getElementById('hdnattributeGUID').value = objattributeGUID.value;
            document.getElementById('txtAttribute').value = objattributeName.innerHTML;
            var vActive = document.getElementById('chkActive');
            if (objActive.value == "False")
            {
                vActive.checked = false;
                
            }
            else
            {
                vActive.checked=true;
            }
            return false;
            
        }
        function IsValidate()
        {
            var Attribute = document.getElementById("<% =txtAttribute.ClientID%>");
            if (Attribute.value == "") {
                alert('Attribute name can not be empty!');
                return false;
            }
             var vActive = document.getElementById('chkActive');
            if (objActive.checked == false)
            {
                var result=confirm("Inactive attribute will not be available for any Item");
                if(result==false)
                return false;
            }
            var lblMsg = document.getElementById("lblMessage").innerHTML;
            if (lblMsg == "Attribute already exists")
                return false;
                
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
<%--Add Attributes--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <table cellpadding="0" cellspacing="0" border="0" align="center">
        <tr class="button">
            <td colspan="2">
            Add/Edit Attribute
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <br /><br />
                <asp:Label ID="lblMessage" runat="server" CssClass="errormessage"></asp:Label>
            </td>
        </tr>
        <tr>
		    <td class="copy10grey">
			   Attribute Name:
			</td>
			<td >
			    <asp:TextBox ID="txtAttribute" runat="server" class="copy10grey" 
                    ontextchanged="txtAttribute_TextChanged" AutoPostBack="true" MaxLength="50"></asp:TextBox>
			    <asp:HiddenField ID="hdnattributeGuid" runat="server" />
			</td>
		</tr>
		<tr>
		    <td class="copy10grey">
		        Active:
		    </td>
		    <td>
                <asp:CheckBox ID="chkActive" runat="server" TextAlign="Left"/>
	    	</td>
		</tr>
		<tr>
		    <td align="center" colspan="2">
		    <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="button" 
                    onclick="btnSubmit_Click" OnClientClick="return IsValidate()"/>
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" 
                    Text="Cancel" onclick="btnCancel_Click" />
		    </td>
		</tr>
	</table>
	</ContentTemplate>
	</asp:UpdatePanel>
	<br /><br />
<%--Edit/Delete Attributes--%>
	<table align="center">
	    <tr>
	        <td>
	       
	        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <table width="100%">
                <tr>
                    <td>
                        
                        <asp:GridView ID="GvAttribute" runat="server" Width="100%" 
                            CssClass="copy10grey" AutoGenerateColumns="False" 
                          >
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Attribute" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                <ItemTemplate>
                                
                                    <asp:HiddenField ID="hdnAttributeGUID0" Value='<%#Eval("AttributeGUID")%>' 
                                        runat="server" />
                                     <asp:Label ID="lblAttribute" CssClass="copy10grey" runat="server" Text='<%#Eval("AttributeName")%>'></asp:Label>
                                 </ItemTemplate>
                              
                            <HeaderStyle HorizontalAlign="Left" CssClass="button" ></HeaderStyle>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnActive" Value='<%#Eval("Active")%>' 
                                        runat="server" />
                                    <asp:Image ID="imgActive" ImageUrl='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "active"))>0 ? "../images/tick.png" : "../images/cancel.gif" %>' runat="server" />
                                 </ItemTemplate>
                               
                            <HeaderStyle HorizontalAlign="Left" CssClass="button"></HeaderStyle>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="button"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70">
                                <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" CssClass="copy10grey" CommandArgument='<%# Eval("AttributeGUID") %>' OnClientClick="return updateAttribute(this)"  runat="server">Edit</asp:LinkButton>
                                        </td>
                                        <Td>
                                             <asp:LinkButton ID="lnkDelete" CssClass="copy10grey" OnCommand="Delete_click" CommandArgument='<%# Eval("AttributeGUID") %>' OnClientClick="return confirm('Delete this Attribute?');"  runat="server">Delete</asp:LinkButton>
                                    
                                        </td>
                                    </tr>
                                </table>
                                      </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" CssClass="button" Width="70px"></HeaderStyle>

<ItemStyle CssClass="copy10grey"></ItemStyle>
                            </asp:TemplateField>

                            
                        </Columns>
                    </asp:GridView>
                    </td>
                </tr>
                </table></ContentTemplate>
                </asp:UpdatePanel>
	        </td>
	    </tr>
	</table>
	

    </div>
    </form>
</body>
</html>
