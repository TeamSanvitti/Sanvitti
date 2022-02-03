<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KittingByESN.aspx.cs" Inherits="avii.KittingByESN" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Kitting </title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    	<style>

    .progresss {
          position: fixed !important;
          z-index: 9999 !important;
          top: 0px !important;
          left: 0px !important;
          background-color: #EEEEEE !important;
          width: 100% !important;
          height: 100% !important;
          filter: Alpha(Opacity=80) !important;
          opacity: 0.80 !important;
          -moz-cpacity: 0.80 !important;
      }
.modal
{
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    z-index: 100000000;
    opacity: 0.8;
    filter: alpha(opacity=80);
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}
.loadingcss
{    
    font-size: 18px;
    /*border: 1px solid red;*/
    /*width: 200px;
    height: 100px;*/
    display: none;
    position: fixed;
    /*background-color: White;*/
    z-index: 100000001;
    background-color:#CF4342;
}

  </style>
     <script type="text/javascript">        
         function ShowLoading() {

             var modal = $('<div  />');
             modal.addClass("modal");
             modal.attr("id", "modalSending");
             $('body').append(modal);
             var loading = $("#modalSending.loadingcss");
             loading.show();

             var top = '300px';
             var left = '820px';
             loading.css({ top: top, left: left, color: '#ffffff' });

             var tb = $("maintbl");
             tb.addClass("progresss");



             return true;


         }
         </script>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script>
        function set_focus1() {
            var img = document.getElementById("imgDateFrom");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("imgDateTo");
            var st = document.getElementById("txtESN");
            st.focus();
            img.click();
        }
    </script>

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
            &nbsp;&nbsp;Warehouse Kitting 
			</td>
		</tr>
    
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
        </tr> 
     </table>
      <%--<table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlPLT" runat="server"  DefaultButton="btnGetPallet" >
         
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">  
             <tr>
                <td class="copy10grey"  align="right" width="20%" >
                  <b> Fulfillment#:</b>
                </td>
                <td width="30%" >
                <asp:TextBox ID="txtESN"  CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <td  width="10%">
                    &nbsp;<asp:Button ID="btnGetPallet" runat="server" Text="Get Pallets" CssClass="button" OnClick="btnGetPallet_Click" CausesValidation="false" />
                     &nbsp;<asp:Button ID="btnCancel2" runat="server" Text="Get Pallets" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false" />
         
                </td>
                <td class="copy10grey"  align="right" width="10%" >
                 <strong>  <asp:Label ID="lblPallet" Visible="false" CssClass="copy10grey" runat="server" Text="Pallet:"></asp:Label></strong>
                </td>
                <td width="30%" >
                 <asp:DropDownList ID="ddlPallet" Visible="false"  CssClass="copy10grey" runat="server" Width="60%" >
                </asp:DropDownList>
                
                </td>   
                </tr>
            
            </table>


            </asp:Panel>
        
                </td>
            </tr>
            </table>--%>
        
         
             
            <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">  
             
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                   IMEI#:
                </td>
                <td width="30%" >
                    <asp:TextBox ID="txtESN"  CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <td  width="10%">
                       &nbsp;
                    <asp:Button ID="btnGetSKU" Visible="false" runat="server" Text="Get Kitted SKU(s)" CssClass="button" OnClick="btnGetSKU_Click" CausesValidation="false" />
         
                </td>
                <td class="copy10grey"  align="right" width="10%" >
                   <%--<strong>  <asp:Label ID="lblSKU" Visible="false" CssClass="copy10grey" runat="server" Text="Kitted SKU:"></asp:Label></strong>--%>
                    
                </td>
                <td width="30%" >
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowLoading();"/>
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            

                    <%--<asp:DropDownList ID="ddlSKU" Visible="false"  CssClass="copy10grey" runat="server" Width="60%" >
                    </asp:DropDownList>               
                
                    <asp:TextBox ID="txtBoxNo" Visible="false"  CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>--%>
                  
                </td>   
                </tr>
                 </table>
                <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" Visible="false" >
                <%--<table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">  
                <tr style="height:12px">
                <td colspan="4">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="4">
         
        </td>
        </tr>
           </table>--%>

                </asp:Panel>
        
                </td>
            </tr>
            </table>     
         
        
        <asp:Panel ID="pnlPO" runat="server" Visible="false">
              <br />
                <table  width="100%" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td width="39%">

                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td>

                     <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">   
                    <tr>
                        <td class="copy10grey" align="right" >
                            Kitted SKU:
                        </td>
                        <td align="left" >
                           <strong> <asp:Label ID="lblKittedSKU" runat="server" CssClass="copy10grey"></asp:Label></strong>
                        </td>
                        <td class="copy10grey" align="right" >
                            
                        </td>
                        <td align="left" >
                            
                        </td>
                
                    </tr>
                    <tr>
                        <td colspan="4"><hr /> </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" align="right" width="20%" >
                            Fulfillment#:
                        </td>
                        <td align="left" width="25%">
                            <asp:Label ID="lblPONum" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                        <td class="copy10grey" align="right" width="25%">
                            Fulfillment Date:
                        </td>
                        <td align="left" width="30%">
                            <asp:Label ID="lblPODate" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                
                    </tr>
                    
                         <tr>
                        <td class="copy10grey" align="right" >
                            Ordered Quantity:
                        </td>
                        <td align="left" >
                            <asp:Label ID="lblQty" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                        <td class="copy10grey" align="right" >
                            Requested Ship Date:
                        </td>
                        <td align="left" >
                          <asp:TextBox ID="txtShipDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtShipDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                    
                        </td>
                
                    </tr>
                         <tr>
                        <td class="copy10grey" align="right" >
                            Status:
                        </td>
                        <td align="left" >
                            <asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                        <td class="copy10grey" align="right" >
                            
                        </td>
                        <td align="left" >
                            
                        </td>
                
                    </tr>
                    </table>

                            </td>
                        </tr>
                    </table>  
                           </td>
                            <td width="1%">
                                &nbsp;
                            </td>
                            <td width="60%">
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td>

                    <asp:Repeater ID="rptKit" runat="server"  >
                    <HeaderTemplate>
                    <table border="0" width="100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td class="buttongrid"  width="2%" >
                            S.No.
                        </td>
                        <td class="buttongrid"  width="20%">
                            Category Name
                        </td>
                        
                        <td class="buttongrid"  width="28%">
                            RAW SKU
                        </td>
                        <td class="buttongrid"   width="40%" >
                            Product Name
                        </td>
                        <td class="buttongrid"    width="10%">
                            Quantity
                        </td>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="top"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  width="2%" align="right">
                                <%# Container.ItemIndex +  1 %>
                            </td>                    
                            <td  class="copy10grey"  width="20%" align="left">
                                <%# Eval("CategoryName")%>
                                
                            </td>
                            <td  class="copy10grey"  width="28%" align="left">
                                <%# Eval("SKU")%>    
                                
                            </td>
                            <td  class="copy10grey"  width="40%" align="left">
                                <%# Eval("ProductName")%>
                                
                            </td>
                            
                            <td  class="copy10grey"  width="10%" align="right">
                                <%# Eval("Quantity")%>    
                                
                            </td>
                            
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
                 </table>
            </asp:Panel>
           <asp:Panel ID="pnlLabel" runat="server" Visible="false">
            <br />
          <table align="center" style="text-align:left" width="100%">
            <tr>
                <td align="right">
                    &nbsp;<asp:Button ID="btnPallet" runat="server" Text="Pallet Label" CssClass="button"  OnClick="btnPallet_Click" CausesValidation="false"/>
                    &nbsp; <asp:Button ID="btnCarton" runat="server" Text="Master Carton Label" CssClass="button"  OnClick="btnCarton_Click" CausesValidation="false"/>
                    &nbsp; <asp:Button ID="btnPOSLabel" runat="server" Text="POS Label" CssClass="button"  OnClick="btnPOSLabel_Click" CausesValidation="false"/>
                    &nbsp;<asp:Button ID="btnBox" runat="server" Text="Box Label" CssClass="button"  CausesValidation="false" OnClick="btnBox_Click"/>                 
                </td>
            </tr>
            <tr>
              <td align="center">
                  <asp:Repeater ID="rptESN" runat="server"  >
                    <HeaderTemplate>
                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="buttongrid"  width="1%" >
                            S.No.
                        </td>
                        <td class="buttongrid"  width="11%">
                            IMEI
                        </td>
                        <td class="buttongrid"   width="11%" >
                            DEC
                        </td>
                        <td class="buttongrid"    width="11%">
                            HEX
                        </td>
                        <td class="buttongrid"    width="15%">
                            ContainerID
                        </td>
                        <td class="buttongrid"    width="15%">
                            PalletID
                        </td>
                        <td class="buttongrid"    width="5%">
                            BoxID
                        </td>
                         <td class="buttongrid"    width="5%">
                            Source
                        </td>
                        <td class="buttongrid"     width="10%">
                            &nbsp;
                        </td>
                       
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td  class="copy10grey"  >
                                <asp:Label ID="lblESN" runat="server" CssClass="copy10grey" Text='<%# Eval("ESN")%>'></asp:Label>
                                 
                                
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("DEC")%>    
                                
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("HEX")%>    
                                
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("ContainerID")%>    
                                
                            </td>                    
                            <td  class="copy10grey"  >
                                <%# Eval("PalletID")%>    
                                
                            </td> 
                            <td  class="copy10grey"  >
                                <%# Eval("BoxNumber")%>    
                                
                            </td>  
                             <td  class="copy10grey"  >
                                <%# Eval("EsnSource")%>    
                                
                            </td>
                            <td  class="errormessage"  >
                                <%# Eval("ErrorMessage")%>    
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
                               
              </td>
          </tr>
          <tr>
              <td align="center">
               <b>   <asp:CheckBox ID="chkVerify" CssClass="copy10grey" Text="Verified the BOM/Kitted box contents and labels" runat="server" /></b>
              </td>
          </tr>
          <tr>

                    <td  align="center">
                    
                            <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr>
                                   
                                    <td  align="center">
                                
                                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                       
                                        &nbsp;<asp:Button ID="Cancel1" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="Cancel1_Click" />
                                         &nbsp;
                                        <asp:Button ID="btnNewKitting" CssClass="button" runat="server" OnClick="btnNewKitting_Click" Text="New Kitting" />
                               
                                        </td>
                                </tr>
                        
                            </table>
                        
                    </td>
                    </tr>
                </table>
              
          </asp:Panel>
    
          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBox" />
            <asp:PostBackTrigger ControlID="btnCarton" />
            <asp:PostBackTrigger ControlID="btnPOSLabel" />
            <asp:PostBackTrigger ControlID="btnPallet" />
        </Triggers>
        </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br /><br /> <br />
            <br /> <br /><br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
        <script type="text/javascript">
            function StopProgress() {

                // $("div.modal").hide();

                var delayInMilliseconds = 13000; //7 second

                setTimeout(function () {

                    // alert('yo');

                    var tb = $("maintbl");
                    tb.removeClass("progresss");


                    var loading = $("#modalSending.loadingcss");
                    loading.hide();

                    //your code to be executed after 1 second
                }, delayInMilliseconds);

                // alert(loading);

            }

            //StopProgress();
        </script>
 
    </form>
</body>
</html>
