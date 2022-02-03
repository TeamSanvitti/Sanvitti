<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsnAuthorization.aspx.cs" Inherits="avii.ESN.EsnAuthorization" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>ESN Authorization</title>
     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
   
    <script>
        function doReadonly(evt) {

            evt.keyCode = 0;
            return false;
        }

        function set_focus() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlRawSKU");
            st.focus();
            img.click();
        }
        function Validate() {

            var customer = document.getElementById("<% =dpCompany.ClientID %>");
            var kittedSKU = document.getElementById("<% =ddlKitSKU.ClientID %>");
            var rawSKU = document.getElementById("<% =ddlRawSKU.ClientID %>");
            var runNumber = document.getElementById("<% =txtRunNumber.ClientID %>");

            if (customer.selectedIndex == 0) {
                alert('Customer is required!');
                return false;
            }
            if (rawSKU.selectedIndex == 0) {
                alert('RAW SKU is required!');
                return false;
            }
            if (kittedSKU.selectedIndex == 0) {
                alert('Kitted SKU is required!');
                return false;
            }
            if (runNumber.value == '') {
                alert('Run number is required!');
                
                return false;
            }
        }
    </script>
</head>
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN Authorization
		    </td>
        </tr>
    </table> 
    <table  align="center" style="text-align:left" width="95%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
     <asp:Panel ID="pnlUpload" runat="server" Visible="true">
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0"  >
      <tr>
        <td align="center">
            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                <td class="copy10grey" align="right" width="15%">                
                    <b>Customer:</b> &nbsp;
                </td>
                <td align="left" width="35%">
                    <asp:DropDownList ID="dpCompany" OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                        AutoPostBack="true" CssClass="copy10grey" runat="server" Width="80%">
					</asp:DropDownList>                                    
                </td>
                <td class="copy10grey" align="right" width="15%">                
                   <b>Planned Provisioing Date</b>&nbsp;
                </td>
                <td align="left" width="35%">
                        <asp:TextBox ID="txtShipDate" runat="server" Width="40%" onfocus="set_focus();" onkeypress="return doReadonly(event);"
                                CssClass="copy10grey" MaxLength="15" Text="" />
                            <img id="img1" alt="" onclick="document.getElementById('<%=txtShipDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipDate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                src="../../fullfillment/calendar/sscalendar.jpg" />
                                             
                </td>
            </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">                
                    <b>Raw SKU:</b> &nbsp;
                </td>
                <td align="left" width="35%">
                    <asp:DropDownList ID="ddlRawSKU" CssClass="copy10grey" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRawSKU_SelectedIndexChanged" runat="server" Width="80%">
					</asp:DropDownList>                                                                       
                </td>
                <td class="copy10grey" align="right" width="15%">                
                   <b>  <asp:Label ID="lblKittedSKU" runat="server" CssClass="copy10grey" Text="Kitted SKU:"></asp:Label></b>
                    &nbsp;
                </td>
                <td align="left" width="35%">
                    <asp:DropDownList ID="ddlKitSKU" CssClass="copy10grey" runat="server" Width="80%">
					</asp:DropDownList>                                    
                </td>
            </tr>
            <tr>
                <td  class="copy10grey" align="right" >
                    <b>Upload ESN file: </b>&nbsp;</td>
                <td align="left" >
                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="55%" /></td>
                <td class="copy10grey" align="right" width="15%">                
                    <b> Run Number:</b>
                    &nbsp;
                </td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtRunNumber" CssClass="copy10grey" runat="server" Width="80%">
					</asp:TextBox>                                    
                </td>
            </tr>
            <tr>
                <td class="copy10grey" align="right">
                    File format sample: &nbsp;
                            </td>
                <td class="copy10grey" align="left">
                    Seq.No.,BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber,BoxID <asp:LinkButton ID="lnkDownload" runat="server"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                    <%--<asp:Label ID="lblUploadDate" runat="server" Text=",uploaddate"></asp:Label>--%>

                </td>
                <td></td>
                <td></td>
                </tr>
                                        
            <tr width="100%">
                <td colspan="4" align="center" width="100%">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="copy10grey" align="center" colspan="4" width="100%">                                    
                             <asp:Button ID="btnUploadValidate"  runat="server" Text="Upload" OnClientClick="return Validate();" CssClass="button" OnClick="btnUploadValidate_Click" Visible="true" />       
                            &nbsp;  
                            <asp:Button ID="btnGenerate"  runat="server" Text="Generate XML" CssClass="button" OnClick="btnGenerate_Click" Visible="false" />       
                            &nbsp;
                            <asp:Button ID="btnGenerateCSV"  runat="server" Text="Generate CSV" CssClass="button" OnClick="btnGenerateCSV_Click" Visible="false" />       
                           &nbsp; <asp:Button ID="btnPOSLabel"  runat="server" Text="POS LEBEL" CssClass="button" OnClick="btnPOSLabel_Click" Visible="false" />       
                            &nbsp;<asp:Button ID="btn_Cancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btn_Cancel_Click" />
                </td>
            </tr>
            </table>
            
        </td>
      </tr>
     </table>
     </asp:Panel>
     
     <asp:Panel ID="plnFSearch" runat="server" Visible="false">
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0" id="trUpload" >
      <tr>
        <td align="center">
    
                        <table width="100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td class="copy10grey" align="right" width="15%">                
                                <b>Raw SKU:</b> &nbsp;
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblSKU" CssClass="copy10grey" runat="server" >
								</asp:Label>                                    
                            </td>
                            <td class="copy10grey" align="right" width="15%">                
                                <b> Kitted SKU:</b> &nbsp;
                            </td>
                            <td align="left" width="35%">
                                <asp:DropDownList ID="ddlSKU" CssClass="copy10grey" runat="server" Width="80%">
								</asp:DropDownList>                                    
                            </td>
                        </tr>
                        <tr width="100%">
                            <td colspan="4" align="center" width="100%">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" align="center" colspan="4" width="100%">                                    
                                    
                                     <asp:Button ID="btnDownload"  runat="server" Text="Download" CssClass="button" OnClick="btnDownload_Click" Visible="false" />       
                                     &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
             </td>
      </tr>
    </table>
            </asp:Panel>
    
       
            

                                
<%--                                <asp:Repeater ID="rptESNs" runat="server" Visible="true" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="95%" cellpadding="1" cellspacing="1" align="center">
                                <tr>
                                    <td class="buttongrid" width="2%">
                                        S.No.
                                    </td>
                                    <td class="buttongrid"  width="18%">
                                        SKU
                                    </td>
                                    <td class="buttongrid"  width="25%">
                                        ESN
                                    </td>
                                    <td class="buttongrid" width="25%">
                                        HEX
                                    </td>
                                    <td class="buttongrid" width="15%">
                                       DEC
                                    </td>
                                    
                                    <td class="buttongrid" width="15%">
                                    </td>
                                    
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ESN")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("MeidHex")%>    
                                            </span>
                                        </td>
                                         <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("MeidDec")%>    
                                            </span>
                                        </td>
                                        
                                         <td valign="bottom" class="errormessage"  >
                                        <span width="100%" class="errormessage">
                                            <%# Eval("ErrorMessage")%>    
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>--%>

         

    </form>
</body>
</html>
