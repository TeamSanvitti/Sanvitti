<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferOrderRequest.aspx.cs" Inherits="avii.InternalInventory.TransferOrderRequest" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Request</title>
     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
         

         <script type="text/javascript">
             function alphaNumericCheck(evt) {
                 var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
                 //alert(charCodes);
                 if (charCodes == 8 || charCodes == 9 || charCodes == 46 || (105 >= charCodes && charCodes >= 96)
                     || (90 >= charCodes && charCodes >= 65)
                     || (57 >= charCodes && charCodes >= 48)
                     || (122 >= charCodes && charCodes >= 97)) {
                     return true;
                 }
                 else {
                     return false;
                 }
             }

             function isNumberKey(evt) {
                 var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                 if (charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) {
                     charCodes = 0;
                     return false;
                 }
                 //return true;
                 if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                     charCodes = 0;
                     return false;
                 }
                 return true;
             }
         function formatParentCatDropDown(objddl) {

             for (i = 0; i < objddl.options.length; i++) {
                 objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
             }
         }
        function OpenNewPage(url) {
            window.open(url);
        }

        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("txtOrderDate");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function ReadOnly1(evt) {
            var img = document.getElementById("img1");
            img.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnly2(evt) {
            var img2 = document.getElementById("img2");
            img2.click();
            evt.keyCode = 0;
            return false;

        }
         </script>

</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
            &nbsp;&nbsp;Transfer Order Request
			</td>
		</tr>    
    </table>
    <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
            <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	        <tr>                    
                    <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                </tr> 
            </table>
            
            <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                  <%-- <b> Tranfer Order:</b>--%>
                        <b>Source Customer:</b>

                </td>
                <td width="30%" >
                   
                    <asp:DropDownList ID="dpSourceCompany" TabIndex="3"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="dpSourceCompany_SelectedIndexChanged">									
                    </asp:DropDownList>  
                    <%--<asp:TextBox ID="txtTranferOrderNo" TabIndex="1" onkeypress="return alphaNumericCheck(event);"  runat="server" CssClass="copy10grey" Width="60%" >									
                    </asp:TextBox>--%>                 

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                      <b>Order Date:</b>
                </td>
                <td width="35%" >
                     <asp:TextBox ID="txtOrderDate" runat="server" TabIndex="2" onkeydown="return ReadOnly1(event);"  onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                     <img id="img1" alt="" onclick="document.getElementById('<%=txtOrderDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtOrderDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                   
                    </td>   
                </tr>
                 <%--<tr>
                <td class="copy10grey"  align="right" width="20%" >
                   <b>Source Customer:</b>

                </td>
                <td width="30%" >               

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                     
                </td>
                <td width="35%" >

                    </td>   
                </tr>--%>
                 <tr valign="top">
                <td class="copy10grey"  align="right" width="20%" >
                  <b> Category Name:</b>

                </td>
                <td width="30%" >
                    <asp:DropDownList ID="ddlSourceCategory" TabIndex="4"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSourceCategory_SelectedIndexChanged">									
                    </asp:DropDownList>                 

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                      <b>SKU: </b>
                </td>
                <td width="35%" class="copy10grey"  >
                    <asp:DropDownList ID="ddlSourceSKU" TabIndex="5"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSourceSKU_SelectedIndexChanged">									
                    </asp:DropDownList>                 
                     
                    </td>   
                </tr>
                <tr >
                <td class="copy10grey"  align="right" width="20%" >
                  <b> Product Name:</b>

                </td>
                <td width="30%" >
                    <asp:label ID="lblProductname"  CssClass="copy10grey" runat="server"   ></asp:label>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                    <b>Current Stock:</b>
                </td>
                <td width="35%" class="copy10grey"  >
                 <asp:label ID="lblSCurrentStock"  CssClass="copy10grey" runat="server"   ></asp:label>
                     
                    </td>   
                </tr>
                <tr >
                <td class="copy10grey"  align="right" width="20%" >
                  <b> Model Number:</b>

                </td>
                <td width="30%" >
                    <asp:label ID="lblModelNumber"  CssClass="copy10grey" runat="server"   ></asp:label>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                    <b>UPC:</b>
                </td>
                <td width="35%" class="copy10grey"  >
                 <asp:label ID="lblUPC"  CssClass="copy10grey" runat="server"   ></asp:label>
                     
                    </td>   
                </tr>

                </table>
               
            </td>
            </tr>
            </table>
                <br />

            <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
            
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                   <b>Destination Customer:</b>

                </td>
                <td width="30%" >
                    <asp:DropDownList ID="ddlDestinationCompany" TabIndex="6"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDestinationCompany_SelectedIndexChanged">									
                    </asp:DropDownList>                 

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                                           <b>SKU: </b>

                </td>
                <td width="35%" >
                    <asp:DropDownList ID="ddlDestinationSKU" TabIndex="8"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDestinationSKU_SelectedIndexChanged" >									
                    </asp:DropDownList>                 
                    
                    </td>   
                </tr>
                 <%--<tr>
                <td class="copy10grey"  align="right" width="20%" >
                   <b>Category Name:</b>

                </td>
                <td width="30%" >
                    <asp:DropDownList ID="ddlDestinationCategory" TabIndex="7"  runat="server" CssClass="copy10grey" Width="60%" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDestinationCategory_SelectedIndexChanged">									
                    </asp:DropDownList>                 

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                </td>
                <td width="35%" >
                    
                     </td>   
                </tr>--%>
                 
                <tr>
                <td class="copy10grey"  align="right" width="20%" >
                <b>Requested Quantity:</b>

                </td>
                <td width="30%" >
                     <asp:TextBox ID="txtRequestQty"  CssClass="copy10grey" MaxLength="4" onkeydown="return isNumberKey(event);" TabIndex="9" runat="server"   Width="60%" ></asp:TextBox>

                    
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                      Current Stock:
                </td>
                <td width="35%" >
                    
                     <asp:label ID="lblDCurrentStock"  CssClass="copy10grey" runat="server"   ></asp:label>


                      
                </td>   
                </tr>
                 </table>
                </td>
                </tr>
                </table>
                <br />
                <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
            
                 <tr valign="top">
                <td class="copy10grey"  align="right" width="20%" >
                    <b>Requested By:</b>
                </td>
                <td width="30%" >    
                       <asp:DropDownList ID="ddlUsers" TabIndex="10" runat="server" Width="60%" CssClass="copy10grey">                         
                    </asp:DropDownList>

              
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="14%" >
                   Comment: 
                </td>
                <td width="35%" >
                      <asp:TextBox ID="txtComment" runat="server" TabIndex="11" CssClass="copy10grey" TextMode="MultiLine" Width="94%" Rows="3"></asp:TextBox>

                </td>   
                </tr>
                    </Table>
                </td>
                </tr>
                 </table>
                     <br />
          
                <table width="100%" align="center" >
                <tr>
			        <td align="center" >
                       <%-- <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div>--%>
                       
			            <asp:Button ID="btnSubmit" runat="server" TabIndex="18" Visible="true" CssClass="button" Text="   Submit   " onclick="btnSubmit_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="button" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />
                        
			        </td>
			    </tr>
			    </table> 
                
                   
            </ContentTemplate>
           <Triggers>
<%--               <asp:PostBackTrigger ControlID="btnUploadValidate" />--%>
           </Triggers>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        formatParentCatDropDown(document.getElementById("<%=ddlSourceCategory.ClientID%>"));
        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    formatParentCatDropDown(document.getElementById("<%=ddlSourceCategory.ClientID %>"));
                    
                }
            });
        };
    </script>
</body>
</html>
