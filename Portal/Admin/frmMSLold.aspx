<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="frmMSL.aspx.cs" Inherits="avii.Admin.frmMSL" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="Detail" Src="~/Controls/PODetails.ascx" %>
<%@ Register TagPrefix="RMA" TagName="Detail" Src="~/Controls/RMADetails.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Aerovoice Inc. - ESN Query</title>
    <LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="../avI.js"></script>
	<script language="javascript" src="../avI.js"></script>
	<script type="text/javascript">
	    function ValidateSpace(evt) {
	        //alert(evt.keyCode)
	        var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
	        if (charCodes == 32)
	            return false;
           
        }
	    function ToggleControl(obj)
	    {
	        var fileUploadObj = document.getElementById("<%=flUpload.ClientID %>");
	        //alert(obj.value);
	        var esnObj = document.getElementById("<%=txtESN.ClientID %>");
	        var trObj = document.getElementById("trParameter");






var chkObj = document.getElementById("<%=chkRepository.ClientID %>");
	        var nextNode = document.getElementById("<%=chkRepository.ClientID %>").nextSibling;

	        
	        
	        if(obj.value=="ESN")
	        {
	            fileUploadObj.style.display = "none";
	            esnObj.style.display = "block"
	            chkObj.style.display = "block"
	            trObj.style.display = "block";
	            lblUploadMsg.style.display = "none";
	            if (nextNode != null)
	                nextNode.style.display = "block";

	        }
	        else
	        {
	            fileUploadObj.style.display = "block";
	            esnObj.style.display = "none"
	            chkObj.style.display = "none"
	            trObj.style.display = "none";
	            lblUploadMsg.style.display = "block";
	            if (nextNode != null)
	                nextNode.style.display = "none"

	        }
	        //fileUploadObj.setAttribute("display", "block");
	        //esnObj.setAttribute("display", "none");
	        
	        
	    }
	</script> 
    <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="/dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="/dhtmlxwindow/dhtmlxcommon.js" type="text/javascript"></script>
	<script src="/dhtmlxwindow/dhtmlxwindows.js" type="text/javascript"></script>
	
	<script src="/dhtmlxwindow/dhtmlxcontainer.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        var dhxWins, w1;
        function doOnLoad(esn) {
            dhxWins = new dhtmlXWindows();
            dhxWins.enableAutoViewport(false);
            dhxWins.attachViewportTo("winVP");
            dhxWins.setImagePath("../../codebase/imgs/");



            if (esn != "") {
                w1 = dhxWins.createWindow("w1", 103, 10, 755, 400);
                w1.setText("View ESN Log");
                w1.attachURL("ViewEsnlog.aspx?esn=" + esn);
            }


        }
	</script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>

    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;ESN Query
		    </td>
        </tr>
</table>
<div id="winVP"  >
<table>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
        <tr><td  class="copy10grey">- Enter partial or complete ESN# to get the result.<br />- Upload Excel file with list of ESN, MEID or AKey values to search multiple records.
<br />- Check the repository checkbox to get the search result only from the repository.
</td></tr>
    </table>
    <div>
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
           <tr bordercolor="#839abf">
                <td>
    <table  cellSpacing="3" cellPadding="3" width="100%" border="0">
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td align="right" class="copy10grey" width="40%">
            <asp:DropDownList ID="dpMslSelect" runat="server" onchange="ToggleControl(this);" 
                    > 
                <asp:ListItem Selected="True" Text="ESN" Value="ESN"></asp:ListItem>
                <asp:ListItem Text="ESN by Upload" Value="ESN by Upload" ></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;
             </td>
             <td align="left" class="copy10grey" width="60%">
             <asp:TextBox CssClass="copy10grey" Width="70%" ID="txtESN" onkeypress="return ValidateSpace(event);" runat="server" MaxLength="30"></asp:TextBox>

            <asp:CheckBox ID="chkRepository" runat="server" CssClass="copy10grey" Text="Repository only"  />
                <asp:FileUpload CssClass="copy10grey" Width="60%" ID="flUpload" runat="server" />
             
             </td>
            

        </tr>
        <tr id="trParameter">
        <td align="right" class="copy10grey" width="40%">MEID:&nbsp;</td>
            <td class="copy10grey">
            <asp:TextBox CssClass="copy10grey" Width="36%" ID="txtMEID" onkeypress="return ValidateSpace(event);" runat="server" MaxLength="30"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp; AKEY:&nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="copy10grey" Width="37%" ID="txtAKEY" onkeypress="return ValidateSpace(event);" runat="server" MaxLength="30"></asp:TextBox>
    
            </td>
            
        </tr>
        <tr>
            <td width="40%"></td>
            <td><span id="lblUploadMsg"  style="display:none" runat="server">Please upload Excel file (.xls) with format: <b>ESN, MEID, AKEY</b></span></td>
        </tr>
        <tr>
        <td align="center" colspan="2"> 
        <hr />
        </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click"/>&nbsp;
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        </table>
        </td>
        </tr>
        
        </table>
        <table  cellSpacing="0" cellPadding="0" width="95%" align="center">
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>
            <asp:Panel ID="pnlUploadESN" runat="server">
            <table width="100%" align="center">
            <tr>
                <td   width="100%" >
                
                <table bordercolor="#839abf" border="1" style="border-top:0" cellSpacing="0" cellPadding="0" width="99%" >
                <tr bordercolor="#839abf">
                    <td width="100%">   
                    <asp:Repeater ID="rptESN" runat="server">
            
            <HeaderTemplate>
            <table width="100%" align="center" cellSpacing="0" cellPadding="0">
            <tr>
                <td class="button" width="20">
                
                </td>
                <td class="button" align="left">
                    ESN
                </td>
                <td class="button" align="left">
                    MSL Number
                </td>
                <td class="button" align="left"  >
                    Product Code
                </td>
                <td class="button" align="left">
                    UPC
                </td>
                <td class="button" align="left">
                    MEID
                </td>
                <td class="button" align="left">
                    HEX
                </td>
                <td class="button" align="left">
                    AKEY
                </td>
                <td class="button" align="left">
                    PO#
                </td>
                <td class="button" align="left">
                    RMA#
                </td>
                <td class="button" align="left" style="display:none">
                    &nbsp;
                </td>
                <%--<td class="button" align="left" style="display:none">
                    &nbsp;
                </td>
                <td class="button" align="left" style="display:none">
                    &nbsp;
                </td>--%>
                
            </tr>
            
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
                <td width="20" align="left">
                    <asp:CheckBox ID="chkESN" Checked="true" runat="server" CssClass="copy10grey" />
                </td>
                <td align="left">
                    <asp:Label ID="lblESN" runat="server" Text='<%# Eval("esn") %>' CssClass="copy10grey"></asp:Label>
                    
                    
                </td>
                <td class="copy10grey" align="left">
                <%#Eval("MSLNumber")%>
                </td>
                <td class="copy10grey" align="left"  >
                <%#Eval("Item_code")%>
                
                </td>
                <td class="copy10grey" align="left">
                <%#Eval("UPC")%>
                </td>
                <td class="copy10grey" align="left">
                <%#Eval("MEID")%>
                </td>
                <td class="copy10grey" align="left"> 
                <%#Eval("HEX")%>
                </td>
                <td class="copy10grey" align="left">
                <%#Eval("AKEY")%>
                </td>
                <td class="copy10grey">
                <asp:Label ID="lblPO" runat="server" Text='<%# Eval("po_id") %>' Visible="false" CssClass="copy10grey"></asp:Label>
                
                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("po_id")) ==0 ? false : true %>' ToolTip="View PO" 
                CausesValidation="false" OnCommand="imgViewPO_Click" CommandArgument='<%# Eval("po_id") %>' ID="lnkPO"  runat="server" Text='<%#Eval("PurchaseOrderNumber")%>' />
                        
                </td>
                <td class="copy10grey">
                <asp:Label ID="lblRma" runat="server" Text='<%# Eval("rmaGUID") %>' Visible="false"  CssClass="copy10grey"></asp:Label>
                <asp:LinkButton ToolTip="View RMA" Visible='<%# Convert.ToInt32(Eval("rmaGuid")) ==0 ? false : true %>' 
                CausesValidation="false" OnCommand="imgViewRMA_Click" CommandArgument='<%# Eval("rmaGuid") %>' ID="lnkRMA"  runat="server" Text='<%#Eval("RmaNumber")%>' >
                        
                </asp:LinkButton>
                </td>
                <td style="display:none">
                                <img src="/Images/view.png" onclick="doOnLoad('<%# DataBinder.Eval(Container.DataItem, "ESN")%>')" id="view" alt="View ESN Log" />
                                
                                </td>                        
                            <%--<td style="display:none">
                                <asp:ImageButton Visible='<%# Convert.ToInt32(Eval("po_id")) ==0 ? false : true %>' ToolTip="View PO" CausesValidation="false" OnCommand="imgViewPO_Click" CommandArgument='<%# Eval("po_id") %>' ImageUrl="~/Images/edit.png" ID="imgPO"  runat="server" />
                        
                                </td>                        
                            <td style="display:none">
  
                                 <asp:ImageButton ToolTip="View RMA" Visible='<%# Convert.ToInt32(Eval("rmaGuid")) ==0 ? false : true %>' CausesValidation="false" OnCommand="imgViewRMA_Click" CommandArgument='<%# Eval("rmaGuid") %>' ImageUrl="~/Images/edit.png" ID="imgRMA"  runat="server" />
                        
                                    
                                </td>  --%>
            </tr>    
            </ItemTemplate>
            <FooterTemplate>
            </table>
            </FooterTemplate>
            </asp:Repeater>
                    </td>
                </tr>
            </table>
            </td>
            </tr>
            <tr>
            <td>
                &nbsp;
            </td>
            </tr>
            <tr>
                <td  align="center">
                 <asp:Button ID="btnDelete" CssClass="button" OnClick="btnDelete_Click" runat="server" Text="Delete" />
           
                </td>
            </tr>
            </table>   
             </asp:Panel>
             <table height="400" width="100%">
             <tr valign="top">
                <td>
                
               
            <asp:GridView ID="gvMSL" runat="server" Width="100%" GridLines="Both" AllowPaging="false"  AutoGenerateColumns="false">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <Columns>
                            <asp:TemplateField HeaderText="ESN#" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ESN")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="MSL#" SortExpression="MSLNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                <ItemTemplate><%#Eval("MSLNumber")%></ItemTemplate>
                            </asp:TemplateField> 
			    <asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%#Eval("Item_code")%></ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="AVSO" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                <ItemTemplate><%#Eval("AerovoiceSalesOrderNumber")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="MEID" SortExpression="Meid"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Meid")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="HEX" SortExpression="HEX"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "HEX")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="AKEY" SortExpression="AKEY"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "AKEY")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="AVPO" SortExpression="AVPO"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "AVPO")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
 <asp:TemplateField HeaderText="Otksl" SortExpression="Otksl"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Otksl")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="ICC ID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "icc_id")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="LTE IMSI" SortExpression="lte_imsi"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "lte_imsi")%>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="PO#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("po_id")) ==0 ? false : true %>' ToolTip="View PO" CausesValidation="false" OnCommand="imgViewPO_Click" CommandArgument='<%# Eval("po_id") %>'  ID="lnkPO"  runat="server" Text='<%#Eval("PurchaseOrderNumber")%>' />
                 
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Rma#" SortExpression="RmaNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                               <asp:LinkButton ToolTip="View RMA" Visible='<%# Convert.ToInt32(Eval("rmaGuid")) ==0 ? false : true %>' CausesValidation="false" OnCommand="imgViewRMA_Click" CommandArgument='<%# Eval("rmaGuid") %>'  ID="lnkRMA"  runat="server" Text='<%#Eval("RmaNumber")%>' ></asp:LinkButton>
                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            

                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="View Log">
                                <ItemTemplate>
                                <img src="/Images/view.png" onclick="doOnLoad('<%# DataBinder.Eval(Container.DataItem, "ESN")%>')" id="view" alt="View ESN Log" />
                                
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <%--<asp:TemplateField Visible="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="View PO">
                                <ItemTemplate>
                                <asp:ImageButton Visible='<%# Convert.ToInt32(Eval("po_id")) ==0 ? false : true %>' ToolTip="View PO" CausesValidation="false" OnCommand="imgViewPO_Click" CommandArgument='<%# Eval("po_id") %>' ImageUrl="~/Images/edit.png" ID="imgPO"  runat="server" />
                        
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="View RMA">
                                <ItemTemplate>
  
                                 <asp:ImageButton ToolTip="View RMA" Visible='<%# Convert.ToInt32(Eval("rmaGuid")) ==0 ? false : true %>' CausesValidation="false" OnCommand="imgViewRMA_Click" CommandArgument='<%# Eval("rmaGuid") %>' ImageUrl="~/Images/edit.png" ID="imgRMA"  runat="server" />
                        
                                    
                                </ItemTemplate>
                            </asp:TemplateField> --%>                       
                        </Columns>
                    </asp:GridView>
                 </td>
             </tr>
             </table>
        </td></tr>
    </table>
    
    </div>
    </div>
    <table>
        <tr>
            <td>
                <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal5Background"
        CancelControlID="btnClose"  runat="server" PopupControlID="pnlModelPoupp" 
        ID="ModalPopupExtender1" TargetControlID="lnk"
         />
        <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel ID="pnlModelPoupp" runat="server" CssClass="modal5Popup" >
            <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
            <table width="100%">
            <tr>
                <td class="button">
                    Fulfillment Detail
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Panel ID="pnlPO" runat="server">
                    
                    <PO:Detail id="podetail" runat="server" ></PO:Detail>
                    </asp:Panel>

                </td>
            </tr>
            </table>
            
            </div>
        </asp:Panel>
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td>
                <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal5Background"
        CancelControlID="btnClose1"  runat="server" PopupControlID="pnlRMAModelPoupp" 
        ID="ModalPopupExtender2" TargetControlID="lnk1"
         />
        <asp:LinkButton ID="lnk1" runat="server" ></asp:LinkButton>
        <asp:Panel ID="pnlRMAModelPoupp" runat="server" CssClass="modal5Popup" >
            <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
            <table width="100%">
            <tr>
                <td class="button">
                    RMA Detail
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Panel ID="pnlRMA" runat="server">
                    
                    <RMA:Detail id="rma1" runat="server" ></RMA:Detail>
                    </asp:Panel>

                </td>
            </tr>
            </table>
            
            </div>
        </asp:Panel>
            </td>
        </tr>
        </table>
    <script type="text/javascript">
    function HideControl()
	    {
	        var searchIndex = document.getElementById("<%=dpMslSelect.ClientID %>").selectedIndex;
	        if(searchIndex==0)
	        {
	            var fileUploadObj = document.getElementById("<%=flUpload.ClientID %>");
	            fileUploadObj.style.display = "none";
	        }
	        else
	        {
	            var esnObj = document.getElementById("<%=txtESN.ClientID %>");
	            var trObj = document.getElementById("trParameter");

	            esnObj.style.display = "none";
	            trObj.style.display = "none";
	        }
	    }  
	    HideControl();
	    </script>
    </form>
</body>
</html>
