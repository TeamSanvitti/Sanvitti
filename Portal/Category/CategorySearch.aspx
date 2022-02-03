<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategorySearch.aspx.cs" Inherits="avii.Category.CategorySearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category Search</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script>
        function ToggleOther(number) {
            if (number == 1) {
                var base = document.getElementById("chkBase").checked;
                if (base)
                    document.getElementById("chkLeaf").checked = false;

            }
            else
                if (number == 2) {
                    var leaf = document.getElementById("chkLeaf").checked;
                    if (leaf)
                        document.getElementById("chkBase").checked = false;

                }

        }
        $(document).ready(function () {
            $("#txtCategory").keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
            //$( "#txtCategory" ).keypress(function(e) {
            //    var key = e.keyCode;
            //    if (key >= 48 && key <= 57) {
            //        e.preventDefault();
            //    }
            //});
        });
        </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
            
        </td>
     </tr>
     </table>

    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        
        <tr valign="top">
           
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Category Search</td></tr>
             </table>
        
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server"   >
    <ContentTemplate>
    <table  align="center" style="text-align:left" width="95%" cellSpacing="0" cellPadding="0">
     <tr>
        <td align="left">
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
     
           
            <tr  >
                <td class="copy10grey" align="right" width="15%">
                    Parent Category:
                </td>
                <td width="35%">
                
                            <asp:DropDownList ID="ddlParent" CssClass="copy10grey" runat="server" Width="70%">
                                <%--<asp:ListItem Text="" Value="-1"></asp:ListItem>--%>
	                        </asp:DropDownList>      
                
                </td>                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    
                </td>
                <td width="35%">
                   
                   
                </td>   
                
                    
                </tr>
       
                <tr >
                
                <td class="copy10grey" align="right" width="15%">
                    Category Name:
                </td>
                <td width="35%">
                
                    <asp:TextBox ID="txtCategory" runat="server"  CssClass="copy10grey" MaxLength="50"  Width="70%"></asp:TextBox>
                
                </td>

                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Status:
                </td>
                <td width="35%">
                   
                     <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="70%">
                         <asp:ListItem Text="" Value="-1"></asp:ListItem>
                         <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
	                 </asp:DropDownList>  
        
                </td>   
                
                    
                </tr>
           <tr  >
                <td class="copy10grey" align="right" width="15%">
                    Base Categories Only:
                </td>
                <td width="35%">
                
                    <asp:CheckBox ID="chkBase" runat="server" onclick="ToggleOther(1)" ></asp:CheckBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Leaf Categories Only:
                </td>
                <td width="35%">
                    <asp:CheckBox ID="chkLeaf" runat="server" onclick="ToggleOther(2)" ></asp:CheckBox>
                   
                </td>   
                
                    
                </tr>
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                  
        </td>
        </tr>
        </table>
            </asp:Panel>
            </td>
          </tr>
         </table>
            <br />
      <table align="center" style="text-align:left" width="95%">
      <tr>
     <tr>
                <td  align="center"  colspan="5">
                    
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
    
                                <asp:GridView ID="gvCategory"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvCategory_PageIndexChanging" PageSize="100" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvCategory_Sorting">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                      <%# Container.DataItemIndex + 1%>                  
                                </ItemTemplate>
                            </asp:TemplateField> 

                                <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        
                                        <%# Eval("P_CategoryName")%>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="buttonlabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("CategoryDesc")%>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              
                                <asp:TemplateField HeaderText="ESN Required" SortExpression="ESNRequired" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img1" alt="" src='<%# Convert.ToBoolean(Eval("ESNRequired"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                  
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Kitted Box" SortExpression="KittedBox" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img2" alt="" src='<%# Convert.ToBoolean(Eval("KittedBox"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="SIM Required" SortExpression="SIMRequired" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img3" alt="" src='<%# Convert.ToBoolean(Eval("SIMRequired"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Is SIM" SortExpression="IsSIM" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img4" alt="" src='<%# Convert.ToBoolean(Eval("IsSIM"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                  
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="RMA Allowed" SortExpression="RMAAllowed" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img5" alt="" src='<%# Convert.ToBoolean(Eval("RMAAllowed"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                  
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="SKU Mapping" SortExpression="SKUMapping" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img6" alt="" src='<%# Convert.ToBoolean(Eval("SKUMapping"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Re-Stocking" SortExpression="ReStocking" HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img7" alt="" src='<%# Convert.ToBoolean(Eval("ReStocking"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Forecasting" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="buttonlabel" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                          <img id="img8" alt="" src='<%# Convert.ToBoolean(Eval("Forecasting"))==true ? "../images/tick.png" : "../images/cancel.gif" %>' />                                                                           
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Eval("Status")%>
                                        </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttonlabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Edit" ImageUrl="~/images/edit.png" Visible='<%# Convert.ToInt32(Eval("IsEdit")) > 1 ? false : true %>' CommandArgument='<%# Eval("CategoryGUID") %>' OnCommand="imgEdit_Command" />
                                        <asp:ImageButton ID="imgDel" runat="server"  OnClientClick="return confirm('Are you sure want to delete this category?');" Visible='<%# Convert.ToInt32(Eval("IsDelete"))==1 ? false : true %>' AlternateText="Delete" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("CategoryGUID") %>' OnCommand="imgDel_Command" />                       
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
   
    </td>
      </tr>
      </table>   
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
