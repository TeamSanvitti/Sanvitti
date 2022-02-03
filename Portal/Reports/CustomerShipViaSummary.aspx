<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerShipViaSummary.aspx.cs" Inherits="avii.Reports.CustomerShipViaSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global Inc. - Customer Shipment Summary ::.</title>

     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
				<script type="text/javascript">

				    function set_focus1() {
				        var img = document.getElementById("imgFromtDate");
				        var st = document.getElementById("btnSearch");
				        st.focus();
				        img.click();
				    }
				    function set_focus2() {
				        var img = document.getElementById("imgToDate");
				        var st = document.getElementById("btnSearch");
				        st.focus();
				        img.click();
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
                <td>&nbsp;Customer Shipment Summary</td></tr>
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
     <table width="100%" border="0" class="box" align="center" cellpadding="4" cellspacing="4">
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
                    
                </td>

                <td width="40%">
                    
                   
                </td>   
                
                    
                </tr>

                <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                
                
                </td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="10%">Date To:</td>
                
                <td align="left" width="40%">
                    <asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="45%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
                <tr>
                <td colspan="5">
                    <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validate();" OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
        </td>
        </tr>
        
            </table>

            </asp:Panel>
   
     </td>
     </tr>
     </table>       
    
      <table align="center" style="text-align:left" width="100%">
      <tr>
                <td  align="center"  >
    <table width="115%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server"  ></asp:Label>
            </td>
        </tr>
       
    <tr>
        <td>
         <asp:GridView ID="gvShipVia" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="false" AllowSorting="false"  
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>
                    <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
                          <%--<%# Container.DataItemIndex + 1%>--%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
        
                <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    
                         <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                 
                 
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>    
                







                <asp:TemplateField HeaderText="FDGE" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDGE")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                        <%# Eval("FDX2DASR")%>  
                        </div> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DCOD" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                        <%# Eval("FDX2DCOD")%>
                    </div>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                        <%# Eval("FDX2DDSR")%> 
                    </div>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DHASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDX2DHASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DHDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDX2DHDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DHNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDX2DHNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDX2DNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX3DSAV" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDX3DSAV")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDX2DNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                





                <asp:TemplateField HeaderText="FDXEXCOD" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXCOD")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXEXDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXEXHASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXHASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXEXHDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXHDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXEXHNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXHNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                




                <asp:TemplateField HeaderText="FDXEXNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXEXNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXGRASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXGRASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXGRCOD" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXGRCOD")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXGRDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXGRDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXHDASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXHDASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                
                




                 <asp:TemplateField HeaderText="FDXHDDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXHDDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXHDNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXHDNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXINEC" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXINEC")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXINPR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXINPR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXPOASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPOASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                




                 <asp:TemplateField HeaderText="FDXPOCOD" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPOCOD")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXPODSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPODSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXPOHASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPOHASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXPOHDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPOHDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXPOHNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPOHNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
               




                <asp:TemplateField HeaderText="FDXPONOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXPONOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSDASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSDDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSDHASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDHASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSDHDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDHDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
               




                <asp:TemplateField HeaderText="FDXSDHNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDHNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSDNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSDNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSOASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSOASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSOCOD" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSOCOD")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSODSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSODSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
               




                <asp:TemplateField HeaderText="FDXSOHASR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSOHASR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSOHDSR" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSOHDSR")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSOHNOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSOHNOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FDXSONOS" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FDXSONOS")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FED1DPM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FED1DPM")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
               


                <asp:TemplateField HeaderText="FED2DPM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FED2DPM")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FedExSaturdayDeliver" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FedExSaturdayDeliver")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FedExSaver3day" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("FedExSaver3day")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                




                <asp:TemplateField HeaderText="IndirectSign" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("IndirectSign")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PriorityOvernite" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("PriorityOvernite")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
               
               
                <asp:TemplateField HeaderText="UPSBlue" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("UPSBlue")%>
                    </div></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UPSGround" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("UPSGround")%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UPSRed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("UPSRed")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-BackColor="Yellow" ControlStyle-BackColor="Yellow"  ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;<%# Eval("Total")%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
       
        </td>
    </tr>
    </table>

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
        <br /><br /> <br />
            <br /> <br />
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
    