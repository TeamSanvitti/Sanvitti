<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsnManagement.aspx.cs" Inherits="avii.Admin.EsnManagement" ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%--<%@ Register TagPrefix="menu" TagName="HeadAdmin" Src="~/Admin/admHead.ascx" %>--%>
<%@ Register TagPrefix="menu" TagName="HeadAdmin" Src="~/dhtmlxmenu/menuControl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>ESN Management</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="/avI.js"></script> 
		 <link rel="stylesheet"  type="text/css" href="/fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		<script type="text/javascript">
		    function isNumberKey(evt,obj) {
		        
		        var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
		        if (obj.id == "txtPONum") {
		            if ((charCodes > 47 && charCodes < 58) || charCodes == 44) {

		                return true;
		            }
		        }
		        else {
		            if (charCodes > 47 && charCodes < 58) {

		                return true;
		            }
		         }
		        return false;
		    }
		    function minlength(obj) {
		        var txtesn = obj.value;
		        if (txtesn.length < 8) {
		            alert("ESN should not be less than 8 digits!");
		            txtesn = "";
		            return false;
		        }
		     }
		    function checkblank() {
		        var hdnmsl = document.getElementById("hdnmsl").value;
		        var hdnesn = document.getElementById("hdnesn").value;
		       
		        var txtmsl;
		        var flag = 0;
		        var flag1 = 0;
		        var confirmit=true;
		        var all = document.getElementsByTagName("span");
		        for (var i = 0; i < all.length; i++) {
		            if (all[i].id.indexOf("lblMsl") > -1) {
		               
		                if (all[i].innerHTML == hdnmsl)
		                    flag = 1;
		                else if (all[i].innerHTML == hdnesn)
		                    flag1 = 1;
		            }
		        }
		            if (flag == 1 && flag1 == 1)
		                confirmit = confirm("Some MSL are missing and some ESN are invalid.Do u want to continue?");
		            else if (flag == 1)
		                confirmit = confirm("Some MSL are missing.Do u want to continue?");
		            else if (flag1 == 1)
		                confirmit = confirm("Some ESN are invalid.Do u want to continue?");

		            if (!confirmit) {
		                return false;
		            }
		            
		            return true;
		             
		            
		    }
		</script>
        <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
        <script type="text/javascript" >
            $(document).AjaxReady(function () {
                $("#[id$=txtFromDate]").focusin(function (event) {

                    $('#imgFromtDate').click();
                    event.preventDefault();

                });
                $("#[id$=txtFromDate]").keypress(function (event) {
                    $('#imgFromtDate').click();
                    event.preventDefault();

                });
                $('#txtToDate').focusin(function (event) {
                    $('#imgToDate').click();
                    event.preventDefault();

                });
                $('#txtToDate').keypress(function (event) {
                    $('#imgToDate').click();
                    event.preventDefault();

                });
                

            });
            $(document).ready(function () {
                $("#[id$=txtFromDate]").focusin(function (event) {

                    $('#imgFromtDate').click();
                    event.preventDefault();

                });
                $("#[id$=txtFromDate]").keypress(function (event) {
                    $('#imgFromtDate').click();
                    event.preventDefault();

                });
                $('#txtToDate').focusin(function (event) {
                    $('#imgToDate').click();
                    event.preventDefault();

                });
                $('#txtToDate').keypress(function (event) {
                    $('#imgToDate').click();
                    event.preventDefault();

                });
                

            });
        </script>
        
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
    <div align="center" width="95%">
            <TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
				<TR>
					<TD>
					<head1:MenuHeader ID="MenuHeader" runat="server" />
                <menu:HeadAdmin ID="HeadAdmin" runat="server" />
					</TD>
				</TR>
			</TABLE>
			
			    <br />
            <table  cellSpacing="1" cellPadding="1" width="95%">
                <tr>
			        <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN Management
			        </td>
                </tr>
                <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                <tr><td class="copy10grey">
                    - Please enter multiple Purchase Order#s in Comma (,) seperated format.<br />
                    - Please save your transactions before moving to next page.
                    
                </td></tr>
            </table>
    <asp:HiddenField ID="hdnmsl" runat="server" />
            <asp:HiddenField ID="hdnesn" runat="server" />
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" >
                <tr bordercolor="#839abf">
                    <td>
                        <table cellSpacing="1" cellPadding="1" width="100%"  >
                        <tr width="6">
                        <td width="6">
                            &nbsp;
                        </td>
                        </tr>
                             <tr>
                                <td align="right" class="copy10grey" width="15%">
                                    Purchase Order#:</td>
                                <td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtPONum" Width="80%"  MaxLength="2000" runat="server" CssClass="copy10grey" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey" width="15%">
                                    PO Date From:</td>
                                <td width="5">
                                </td>
                                <td width="35%">
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="80%"  CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                </td>
                                    <td align="right" width="10%" class="copy10grey">PO Date To:</td>
                                <td></td>
                                <td width="40%">
                                   <asp:TextBox ID="txtToDate" Width="80%"  runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                
                                </td>               
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey">
                                    PO Status:</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="dpStatusList" runat="server" class="copy10grey" Width="80%">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                                <td align="right" class="copy10grey">
                                    Company:</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey" Width="80%" >
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="iWireless" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="telSpace" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey">
                                    ESN:</td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtEsn" onkeypress="return isNumberKey(event,this);" onchange="return minlength(this);"
                                        runat="server"  CssClass="copy10grey" MaxLength="35" Width="80%"></asp:TextBox>
                                </td>
                                <td align="right" class="copy10grey">
                                MSL Number:</td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtMslNumber" runat="server" onkeypress="return isNumberKey(event,this);" CssClass="copy10grey" MaxLength="32" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey">
                                    Search for Unassigned MDN only:
                                </td>
                                <td colspan="5">
                                
                                    <asp:CheckBox ID="chkMDN" runat="server" Text=" " CssClass="copy10grey" TextAlign="Left"/>
                                </td>
                            </tr>
                            <tr><td colspan="6"><hr /></td></tr>
                            <tr><td colspan="6" align="center">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search Unassigned ESN " OnClick="btnSearch_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Search" OnClick="btnCancel_Click" /><br />      
	                        </td></tr>

            </table>
            	    </td>
            	 </tr>
           </table>
            	 
            <table width="100%">
                <tr>
                    <td align="right" class="style1">
                                    <asp:Button ID="btnClear" runat="server" CssClass="button"  Visible="false"
                                        Text="Clear ESN Assignment" onclick="btnClear_Click" OnClientClick= "return confirm('Clear Assignment will clear the current assignments not the existing assignments.Do you want to continue?');"/>&nbsp;&nbsp;
                                    <asp:Button ID="btnValidateESN" runat="server" CssClass="button" 
                                        Text="Validate Assigned  ESN" Visible="false" onclick="btnValidateESN_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Enabled="false" CssClass="button" 
                                        Text="Submit Assignment" Visible="false" onclick="btnSubmit_Click" OnClientClick="return checkblank();"/>
                                    
                        <asp:HiddenField ID="hdnvalidate" runat="server" />
                    </td>
                </tr>
            <tr><td >
            <table width="100%"><tr valign="top"><td><asp:Label ID="lblCounttext" runat="server" CssClass="copy10grey" Text="No. of records: " Visible="false"></asp:Label>
               <asp:Label ID="lblCount" runat="server" CssClass="copy10grey"></asp:Label> 
               </td><td align="right">
                
                <asp:Panel ID="pnlPagesize" runat="server"  CssClass="copy10grey" Visible="false">
                Select Page Size:<asp:DropDownList ID="ddlPageSize" runat="server" CssClass="copy10grey"
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>75</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>125</asp:ListItem>
                        <asp:ListItem>150</asp:ListItem>
                        <asp:ListItem>175</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel></td></tr></table>
            </td></tr>
                <tr>
                    <td colspan="6">                    
                            <asp:GridView ID="grdEsn"   AutoGenerateColumns="false" Width="100%"  
                                DataKeyNames="PO_ID"  ShowFooter="false" runat="server" GridLines="Both" 
                                onrowdatabound="grdEsn_RowDataBound" AllowPaging="true" PageSize="50" 
                              
                                onpageindexchanging="grdEsn_PageIndexChanging" onrowediting="grdEsn_RowEditing"> 
                                 <PagerStyle ForeColor="black" Font-Size="XX-Small"/>
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                            <FooterStyle CssClass="white"  />
                            <Columns>  
                                                      
                                <asp:TemplateField HeaderText="PO#" SortExpression="PO_Num" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnpoid" runat="server" Value='<%# Eval("PO_ID")%>'/>
                                        <asp:HiddenField ID="hdnpodid" runat="server" Value= '<%# Eval("POD_ID")%>'/>
                                        <%# Eval("PONumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Date"  SortExpression="po_date" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="6%">
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PODate", "{0:d}")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU#" SortExpression="ITEM_CODE" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    <ItemTemplate>
                                        <%# Eval("Itemcode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPC" SortExpression="UPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("UPC")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEsn" Width="100%" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("ESN") %>' runat="server" onchange="return minlength(this);"></asp:TextBox>
                                        <asp:Label ID="lblEsn" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("ESN") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="MslNumber" SortExpression="MSL" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMsl" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("mslNumber") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="FM UPC" SortExpression="fmupc" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfmupc" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("fmupc") %>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtfmupc"  Width="100%"  CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("fmupc") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="MDN" SortExpression="MDN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        
                                        <asp:TextBox ID="txtMdn"  Width="100%"  CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("Mdn") %>' runat="server" TabIndex="1000"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="MSID" SortExpression="MSID" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMSID"  Width="100%"  CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("msid") %>' runat="server" TabIndex="1000"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                             </Columns> 
                            </asp:GridView>
                            
                    </td>
                </tr>
            </table>
                </div>    
        <table width="95%" align="center">
        <tr><td>&nbsp;</td></tr>
				<tr>
            <td><td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td></td>
          </tr>
				        
        </table>            

    </form>
</body>
</html>
