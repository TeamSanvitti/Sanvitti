<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemForm.ascx.cs" Inherits="avii.Controls.ItemForm" %>
<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
							<tr bordercolor="#839abf">
								<td>
<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
	<TR>
	    <TD colSpan="6">
		    <asp:Label id="lblError" runat="server" CssClass="errormessage"></asp:Label></TD>
    </TR>
    <tr>
        <td>
            <table style="width:100%;">
                <tr>
                    <td  class="copy10grey">
                        Phone Maker:</td>
                    <TD width="1%"><font class="RequiredField">*</font></TD>
                    <td class="copy10grey">
                        <asp:DropDownList ID="dpPhoneMaker" CssClass="copy10grey" runat="server" >
                            <asp:ListItem  Text="Brand" Value="Brand"></asp:ListItem>
                            <asp:ListItem  Text="OEM" Value="OEM"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td  class="copy10grey">
                        Item Code:</td>
                    <TD width="1%"><font class="RequiredField">*</font></TD>
                    <td>
                        <asp:TextBox ID="txtItemCode"  onkeypress="return fnValueValidate(event,'s');"  CssClass="copy10grey" runat="server" MaxLength="30"></asp:TextBox>
                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="reqItemCode" runat="server" 
                            ControlToValidate="txtItemCode" CssClass="errormessage" Display="Dynamic" 
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        Phone Model:</td>
                    <TD width="1%"><font class="RequiredField">*</font></TD>
                    <td>
                        <asp:TextBox ID="txtPhoneModel" onkeypress="return fnValueValidate(event,'s');"  runat="server"  CssClass="copy10grey" MaxLength="50"></asp:TextBox>
                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtItemCode" CssClass="errormessage" Display="Dynamic" 
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                    <td class="copy10grey">
                        UPC:</td>
                    <TD width="1%"><font class="RequiredField">*</font></TD>
                    <td>
                        <asp:TextBox ID="txtUPC" runat="server" onkeypress="return fnValueValidate(event,'n');"  CssClass="copy10grey" MaxLength="50"></asp:TextBox>
                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="reqUPC" runat="server" 
                            ControlToValidate="txtUPC" CssClass="errormessage" Display="Dynamic" 
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        SKU#:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td colspan="7" class="copy10grey">
                        <asp:TextBox ID="txtSKU" runat="server" 
                            onkeypress="return fnValueValidate(event,'s');"  CssClass="copy10grey" 
                            MaxLength="500" Width="20%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        Phone Description:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td colspan="7" class="copy10grey">
                        <asp:TextBox ID="txtDesc" runat="server" 
                            onkeypress="return fnValueValidate(event,'s');"  CssClass="copy10grey" 
                            MaxLength="500" Width="80%"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">Color:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td class="copy10grey">
                        <asp:TextBox ID="txtColor" runat="server" onkeypress="return fnValueValidate(event,'s');"  CssClass="copy10grey" MaxLength="30"></asp:TextBox>
                    </td>
                    <td class="copy10grey">Technology:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td>
                        <asp:DropDownList ID="dpTechnology" CssClass="copy10grey" runat="server">
                            <asp:ListItem Value =""></asp:ListItem>                            
                            <asp:ListItem Value ="CDMA">CDMA</asp:ListItem>
                            <asp:ListItem Value ="GSM">GSM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">Company:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td>
                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server">
                            <asp:ListItem Value =""></asp:ListItem>                            
                            <asp:ListItem Value ="2">iWireless</asp:ListItem>
                            <asp:ListItem Value ="1">VioOne</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
              
              
                <tr>
                    <td class="copy10grey">Warehouse Code:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td class="copy10grey">
                        <asp:TextBox ID="txtWhCode" onkeypress="return fnValueValidate(event,'s');"  CssClass="copy10grey" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                    <td class="copy10grey">Device Type:</td>
                    <TD width="1%"><font class="RequiredField"></font></TD>
                    <td>
                        <asp:DropDownList ID="dpDeviceType" runat="server" CssClass="copy10grey">
                            <asp:ListItem Text="Phone" Value="Phone"></asp:ListItem>
                            <asp:ListItem Text="PDA" Value="PDA"></asp:ListItem>
                            <asp:ListItem Text="Accessory" Value="Accessory"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td Class="copy10grey" >
                        Active:
                    </td>
                    <td></td>
                    <td><asp:CheckBox ID="chkActive" runat="server" 
                            oncheckedchanged="chkActive_CheckedChanged" /></td>
                    <td Class="copy10grey" >
                        
                    </td>
                    <td></td>
                    <td><asp:CheckBox ID="chkPhone" runat="server" Visible="False"/></td>
                </tr>
                <tr>
                    <td Class="copy10grey" >
                        Closeout:
                    </td>
                    <td></td>
                    <td><asp:CheckBox ID="chkCloseout" runat="server" /></td>
                    <td Class="copy10grey" >
                        
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add New Item"  
                            Visible="false" onclick="btnAdd_Click"
                            />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                            onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" 
                            onclick="btnCancel_Click"  />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
<p>
    <input id="hdnItemID" type="hidden" runat="server" />
</p>

</td>
</tr></table>