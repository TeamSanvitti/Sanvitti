<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEsnRepositoryDetail.aspx.cs" Inherits="avii.Reports.CustomerEsnRepositoryDetail" %>

<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>.:: Aerovoice Inc. - ESN Repository Detail ::.</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
     	<script type="text/javascript">
     	    function CallPrint(strid) {
     	        var prtContent = document.getElementById(strid);
     	        var WinPrint = window.open('', '', 'letf=0,top=0,width=1024,height=690,toolbar=0,scrollbars=0,status=0');
     	        WinPrint.document.write('<link href="../aerostyle.css" type="text/css" rel="stylesheet" />');
     	        WinPrint.document.write(prtContent.innerHTML);
     	        WinPrint.document.close();
     	        WinPrint.focus();
     	        WinPrint.print();
     	        WinPrint.close();
     	        prtContent.innerHTML = strOldOne;


     	    }
     	    function Validate() {
     	        //if (flag == '1' || flag == '2') {
     	        var company = document.getElementById("<% =dpCompany.ClientID %>");
     	        //alert(company);
     	        if (company != 'null' && company.selectedIndex == 0) {
     	            alert('Customer is required!');
     	            return false;

     	        }
     	        //}
     	    }
        </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table><br />
    <table align="center" style="text-align:left" width="95%">
                <tr class="button" align="left">
                <td>&nbsp;ESN Repository Detail</td></tr>
             </table><br />
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
    
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"  Width="80%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    Duration:
                </td>
                <td width="40%">
                   <asp:DropDownList ID="ddlDuration" AutoPostBack="false"  runat="server" Class="copy10grey" Width="166px" >
                                <asp:ListItem  Value="1"  >Today</asp:ListItem>
                                <asp:ListItem  Value="7"  >One week</asp:ListItem>
                                <asp:ListItem  Value="15"  >Two week</asp:ListItem>
                                <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                <%--<asp:ListItem  Value="180">6 Months</asp:ListItem>
                                <asp:ListItem  Value="365">One Year</asp:ListItem>--%>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="lblDuration" runat="server" CssClass="copy10grey" ></asp:Label>
        
                    <br />
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Unused ESN only:
                </td>
                <td width="35%">
                    <asp:CheckBox ID="chkUnusedESN" runat="server" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    
                </td>
                <td width="40%">
                   
                </td> 
                </tr>

                <tr>
                <td colspan="5">
                    <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"  OnClientClick="return Validate();" OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
        </td>
        </tr>
        
            </table>

            </asp:Panel>
   
     </td>
     </tr>
     </table>       
      <%--</td>
      </tr>
      </table>--%>
      <br />
      <table align="center" style="text-align:left" width="95%">
      <tr>
       <td  align="center"  >
       <asp:Panel ID="pnlRMA" runat="server">

       <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
            <%--<input id="btnPrint" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript:CallPrint('esn');" Runat="Server"  /> &nbsp; &nbsp;--%>
                <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click"  />  
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" Visible="false" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        <div id="esn">
        <asp:GridView ID="gvESN" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true" AllowSorting="false"  
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>

                          <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
               
                <asp:TemplateField HeaderText="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <%# Eval("ESN")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%--<%# Eval("UploadDate")%>   
--%>
                        <%# Convert.ToDateTime(Eval("UploadDate")).ToShortDateString()%>    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("FulfillmentNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%--<%# Eval("FulfillmentDate")%>  --%>

                        <%# Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString()%>    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("FulfillmentStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RMA#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("RmaDate")).ToShortDateString()%>    
                        <%--<%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() %>   --%>
                    </ItemTemplate>
                </asp:TemplateField>
                           
                <asp:TemplateField HeaderText="RMA Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Esn Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaEsnStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        </div>
        <%--<asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>
--%>
            </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
      </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
    </Triggers>
            
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
        <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
        

    </form>
</body>
</html>
