<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OEMQuery.aspx.cs" Inherits="avii.OEM.OEMQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OEM Query</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" language="javascript">

        function ConfirmDelete(obj) {

            var vFlag;
            objMaker = document.getElementById(obj.id.replace('imgDelete', 'hdnMakerCount'));

            if (objMaker.value != "0") {
                vFlag = alert('OEM is already in use, you cannot delete?');
                return false;
            }
            else {
                vFlag = confirm('Delete this OEM?');
                if (vFlag)
                    return true;
                else
                    return false;
            }
        }
        
        </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"    >
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
    <tr>
	    <td><head:MenuHeader id="MenuHeader11" runat="server"></head:MenuHeader>				
		</td>
	</tr> 
    </table>    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate><br />
    <table   width="95%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			        OEM Query
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
                                                    OEM Name:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtMakerName" CssClass="copy10grey"  Width="80%" />
                                                </td>
                                                <td class="copy10grey"  width="15%">
                                                    Short Name:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtShortName" CssClass="copy10grey" Width="80%" />
                                                    
                                                </td>
                                            </tr>
<tr>
                                                <td class="copy10grey">
                                
                                                    Active: 
                                                    </td>                            
                                                    <td>
                                                        <asp:DropDownList ID="ddlActive" Width="80%" runat="server">
                                                        <asp:ListItem Value="-1" Text="" ></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Active" ></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                        </asp:DropDownList>
                            
                            
                                                    </td>
                                                    <td class="copy10grey">
                                                    Catalog OEM:
                            
                                                    </td>                            
                                                    <td>
                                                    <asp:DropDownList ID="ddlCatalog" Width="80%" runat="server">
                                                        <asp:ListItem Value="-1" Text="" ></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Show" ></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="Do not show"></asp:ListItem>
                                                        </asp:DropDownList>
                            
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
                                            CssClass="button" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            CssClass="button" />
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
    <tr>
        <td>
        
        
            <asp:GridView runat="server" ID="gvMakers" AutoGenerateColumns="False" 
             PageSize="50" AllowPaging="true" Width="100%"  
             CellPadding="3" 
            GridLines="Vertical" DataKeyNames="MakerGUID"  >
            <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="buttongrid"   />
             <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
            <%-- <FooterStyle CssClass="white"  />--%>
            <Columns>
                
                <asp:BoundField DataField="ShortName" ItemStyle-Width="150"  HeaderStyle-HorizontalAlign="Left" HeaderText="Short Name" />
                <asp:BoundField DataField="MakerName" HeaderText="OEM Name" />
                
        
            <%--     <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>
                <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                
                <%--<asp:BoundField DataField="CreatedDate" ItemStyle-Width="40" HeaderText="Created Date" />
        
                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
                <asp:BoundField DataField="ModifiedDate" ItemStyle-Width="40" HeaderText="Modified Date" />
                --%>
         
         
                <asp:TemplateField ItemStyle-Width="70" HeaderText="Status">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnMakerCount"  Value='<%# Eval("MakerCount") %>' runat="server" />
                        <%# Convert.ToBoolean(Eval("active")) == true? "Active": "Inactive" %>
                        
                    </ItemTemplate>
                </asp:TemplateField>
<asp:TemplateField ItemStyle-Width="100" HeaderText="Show under Catalog">
                    <ItemTemplate>
                        
                        <%# Convert.ToBoolean(Eval("ShowunderCatalog")) == true ? "Show" : "Do not show"%>
                        
                    </ItemTemplate>
                </asp:TemplateField>
        
                <asp:TemplateField ItemStyle-Width="70" HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        
                        <asp:ImageButton ToolTip="View Image"  CausesValidation="false" CommandArgument='<%# Eval("makerGUID") %>' ImageUrl="~/Images/view.png" ID="imgView" OnCommand="imgView_Commnad" runat="server" />
                        
                    </ItemTemplate>
                </asp:TemplateField>
        
                <asp:TemplateField ItemStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        
                        <asp:ImageButton ToolTip="Edit"   CommandArgument='<%# Eval("makerGUID") %>' ImageUrl="~/Images/edit.png" ID="ImageButton1" OnCommand="imgEdit_Commnad" runat="server" />
                        <asp:ImageButton ToolTip="Delete"   OnClientClick="return ConfirmDelete(this);" CommandArgument='<%# Eval("makerGUID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
                        
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
        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal2Background" 
        CancelControlID="btnClose" runat="server" PopupControlID="pnlModelPopup" 
        ID="ModalPopupExtender1" TargetControlID="lnk"
         />
        <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel  ID="pnlModelPopup" runat="server" CssClass="modal2Popup"   >
      
      
      <div style="overflow:auto; height:520px; width:100%; border: 0px solid #839abf" >
      
      <table align="center" border="0"  width="100%">
      <tr>
        <td>
        
        
      <table align="center" border="0" width="100%" >
      <tr>
        <td align="right" >
       
        
            <asp:Button ID="btnClose" CssClass="button" Height="28" runat="server" Text="Close" CausesValidation="false"  />
        
         
        </td>
      </tr>
      </table>
      </td>
      </tr>
      <tr>
        <td align="center">
        
        
      <table align="center" border="0" width="80%"> 
      <tr>
      <td align="center" >
          <asp:Image ID="imgMaker" runat="server" />
      </td>
      </tr>
      </table>
      </td>
      </tr>
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


        <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
    <tr>
	    <td>
        <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>			
		</td>
	</tr> 
    </table>    
    
    </form>
</body>
</html>
