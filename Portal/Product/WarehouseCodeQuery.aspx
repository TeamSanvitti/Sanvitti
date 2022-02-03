<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseCodeQuery.aspx.cs" Inherits="avii.WarehouseCodeQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Code Query</title>
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
    <link href="../dhtmlwindow.css" type="text/css" rel="stylesheet" />
	<link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	
	
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
        var dhxWins, w1;
        function openpopup(warehouseCode) {
            //alert(userid);
            dhxWins = new dhtmlXWindows();
            dhxWins.enableAutoViewport(false);
            dhxWins.attachViewportTo("winVP");
            dhxWins.setImagePath("../../codebase/imgs/");
            w1 = dhxWins.createWindow("w1", 320, 1, 550, 400);

            w1.setText("Assigned Items");
            //alert(userid);
            w1.attachURL("WarehouseCodeItems.aspx?wh=" + warehouseCode);
        }
	</script>
    <script type="text/javascript">


        function Validate() {

            var warehouseCode = document.getElementById("<%=txtWhCode.ClientID %>");

            var customer = document.getElementById("<%=dpCompany.ClientID %>");

            if (customer.selectedIndex == 0) {
                alert("Please select a customer");
                //customer.focus();
                return false;
            }
            if (warehouseCode.value == "") {
                alert("Warehouse code can not be blank");
                warehouseCode.focus();
                return false;
            }



        }
    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" >
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
    <tr>
	    <td><head:MenuHeader id="MenuHeader11" runat="server"></head:MenuHeader>				
		</td>
	</tr> 
    </table>    
    <div id="winVP" style="z-index:1" >
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate><br />
    <table   width="95%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			        Warehouse Code Query
                        <asp:HiddenField ID="hdnEdit" runat="server" />
                        <asp:HiddenField ID="hdnDel" runat="server" />
			        </td>
                </tr>
                
               <tr>
               <td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
               <tr><td class="copy10grey">
                - Please select your search
                criterial to narrow down the search and record selection.<br />
                - Atleast one search criteria should be selected.
                
                </td></tr>
            </table>
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
    
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
        <tr bordercolor="#839abf">
            <td>
                <table class="box" width="100%" align="center" cellpadding="3" cellspacing="3">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey"  width="15%">
                            Customer Name:
                        </td>
                        <td  width="35%">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="copy10grey">
                                </asp:DropDownList>
                        </td>
                        <td class="copy10grey"  width="15%">
                           Warehouse Code:
                        </td>
                        <td  width="35%">
                            <asp:TextBox runat="server" ID="txtWarehouseCode" CssClass="copy10grey" Width="80%" />
                                                    
                        </td>
                    </tr>

                        <tr>
            <td colspan="4">
                <hr />
                </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_click"
                    CssClass="buybt" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                    CssClass="buybt" />
                    <br />
            </td>
        </tr>
                </table>
            </td>
        </tr>
                               
    </table>
	</asp:Panel>
            
    <table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr >
        <td>
        
        
            <asp:GridView runat="server" ID="gvWarehouse" AutoGenerateColumns="False" 
             PageSize="50" AllowPaging="true" Width="100%"  
             CellPadding="3" 
            GridLines="Vertical" DataKeyNames="WarehouseCodeGUID"  >
            <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button"   />
             <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
            <%-- <FooterStyle CssClass="white"  />--%>
            <Columns>
                
                <asp:BoundField DataField="CompanyName" ItemStyle-Width="40%"  HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />
                <asp:BoundField DataField="warehouseCode" HeaderText="Warehouse Code" ItemStyle-HorizontalAlign="Right" />
         
                <asp:TemplateField ItemStyle-Width="70" HeaderText="Status" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("active")) == true? "Active": "Inactive" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <img src="../images/view.png" title="Assigned Items" alt="Assigned Items"  onclick="openpopup(<%# Eval("warehouseCode") %>);"/>
                        <asp:ImageButton ToolTip="Edit"   CommandArgument='<%# Eval("WarehouseCodeGUID") %>' ImageUrl="~/Images/edit.png" ID="ImageButton1" OnCommand="imgEdit_Commnad" runat="server" />
                        <asp:ImageButton ToolTip="Delete"   OnClientClick="return confirm('Delete this Warehouse Code?');" CommandArgument='<%# Eval("WarehouseCodeGUID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
                        
                    </ItemTemplate>
                </asp:TemplateField>
        
                </Columns>
                </asp:GridView>
   
                </td>
            </tr>
       
            </table>    
        <table>
        <tr>
            <td>
         <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal4Background" 
        CancelControlID="poClose"  runat="server" PopupControlID="pnlPOPopUp" 
        ID="mdlPopup5" TargetControlID="lnkPO"
         />
        <asp:LinkButton ID="lnkPO" runat="server" ></asp:LinkButton>
        <asp:Panel ID="pnlPOPopUp" runat="server" CssClass="modal4Popup" >
            <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
            <table width="100%">
            <tr>
                <td class="button">
                    Edit Warehouse Code
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="poClose" runat="server" Text="Close" CssClass="buybt" />
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Panel ID="pnlPO" runat="server">
                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                    <td >
                        
                        <table width="100%">
                        <tr height="6">
                            <td class="copy10grey">
                                &nbsp;</td>
                        </tr>
                        <tr>
                        <td width="3%"></td>
                        <td class="copy10grey" width="15%" >
                            Customer Name: &nbsp;<span class="errormessage">*</span></td>
                        <td class="style1" width="50%">
                            <asp:DropDownList ID="dpCompany" TabIndex="1" runat="server" CssClass="copy10grey">
                                </asp:DropDownList>
                        </td>
                        <td class="copy10grey"  width="15%">
                            Warehouse Code:&nbsp;<span class="errormessage">*</span></td>
                        <td width="32%">
                            <asp:TextBox ID="txtWhCode"  Width="50%"  MaxLength="5" CssClass="copy10grey" TabIndex="2" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td>
                            
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActive" TabIndex="3" CssClass="copy10grey" Text="Active" runat="server" />
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        
                    <tr height="6">
                            <td class="copy10grey">
                                &nbsp;</td>
                        </tr>
                        </table>    
                    </td>
                    </tr>
                
                    </table>
                    <br />
                    <table width="100%" align="center" >
                    <tr>
			            <td align="center" >
			                <asp:Button ID="btnSubmit" runat="server" TabIndex="18"  CssClass="buybt" OnClientClick="return Validate();" 
                                            Text="   Submit   " onclick="btnSubmit_Click" />&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " 
                                onclick="btnCancel2_Click" />
			            </td>
			        </tr>
			        </table> 
        
                    </asp:Panel>

               </td>
            </tr>
            </table>
            
            </div>
        </asp:Panel>
            </td>
        </tr>
        </table>
          
     
     
     
     </ContentTemplate>
     </asp:UpdatePanel>     
     <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
     <br />
     <br />
<br />
<br />
     <br />
     <br />
<br />
     <br /><br />
<br />
     <br />
     <br />
     <br /><br />
<br />
     <br /
     </div>   


     <table width="100%" align="center">
     <tr>
        <td>
            <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
        </td>
     </tr>
     </table>
    </form>
</body>
</html>
