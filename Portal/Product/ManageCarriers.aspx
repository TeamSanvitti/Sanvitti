<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCarriers.aspx.cs" Inherits="avii.Product.ManageCarriers" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Manage Carriers</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
    var dhxWins, w1;
    
    function displayImage(imgurl) {
        dhxWins = new dhtmlXWindows();
        dhxWins.enableAutoViewport(false);
        dhxWins.attachViewportTo("winVP");
        dhxWins.setImagePath("../../codebase/imgs/");
        if (imgurl != '') {
            w1 = dhxWins.createWindow("w1", 320, 100, 450, 350);
            w1.setText("View Image");

            w1.attachURL("imgpreview.aspx?url=../images/carrier/" + imgurl);
        }
        else
            alert('No image uploaded yet');

    }
    function Validate() {
        var carrier = document.getElementById("<%=txtCarrier.ClientID %>").value;
        if (carrier == '') {
            alert('Carrier can not be empty');
            return false;
        }

    }
	</script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:menu ID="HeadAdmin1" runat="server" ></menu:menu>
        </td>
    </tr>
    </table>
    <div id="winVP">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
            <td  class="copy10grey" align="left">
                <table width="100%">
                <tr>
                    <td class="buttonlabel" >
                        <strong>Manage Carriers</strong>        
                    </td>
                </tr>
                <tr><td>
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                </td></tr>
                </table>
            </td>
        </tr>
    </table>

    <br />

    <table width="95%" cellspacing="0" cellpadding="0" border="0" align="center">
    <tr>
        <td align="right">
            <asp:Button ID="btnAdd"  runat="server" OnClick="btnAddNew_Click" CssClass="buybt" Text="Add New" />
        </td>
    </tr>
    <tr style="height:400px; vertical-align:top">
        <td>
            <asp:GridView ID="gvCarriers" runat="server"   CssClass="gridGray1" AutoGenerateColumns="false" 
            Width="100%"  AllowPaging="false" PageSize="20" >
             <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <Columns>
            
                <asp:TemplateField HeaderText="Carrier Name" HeaderStyle-CssClass="button"  HeaderStyle-HorizontalAlign="Left"> 
                <ItemTemplate> 
                  <asp:Label ID="lblCarries" CssClass="copy10grey" runat="server" Text='<%# Eval("CarrierName") %>'></asp:Label> 
                </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Carrier Logo" HeaderStyle-CssClass="button" > 
                
                <ItemTemplate> 
                
                   &nbsp; <img id="displayimg" src="../images/view.png" onclick="displayImage('<%#  Eval("CarrierLogo")%>');" alt="" />
                </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Active"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="button"> 
                <ItemTemplate> 
                    <%# Convert.ToBoolean(Eval("active")) == true? "Active": "Inactive" %>
                        
                    <%--<asp:CheckBox ID="chkNewActive" Checked='<%# Eval("ACTIVE") %>'  runat="server" CssClass="copy10grey" />
                  --%>
                </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:ImageButton ToolTip="Edit Carrier" CausesValidation="false" 
                    OnCommand="imgEditCarrier_OnCommand" CommandArgument='<%# Eval("CarrierGUID") %>' 
                    ImageUrl="~/Images/edit.png" ID="imgEditCarrier"  runat="server" />
                    <asp:ImageButton ToolTip="Delete"   OnClientClick="return confirm('Are you sure delete this carrier?')"
                    CommandArgument='<%# Eval("CarrierGUID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" 
                    OnCommand="imgDelete_Commnad" runat="server" />
                        
                   </ItemTemplate>     
                </asp:TemplateField>       
                                           
            </Columns>
            </asp:GridView>
                   
        </td>
    </tr>
    </table>
    <table>
    <tr>
    <td align="center">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground" 
        CancelControlID="btnClose" runat="server" PopupControlID="pnlModelPoupp" 
        ID="mdlCarrier" TargetControlID="lnk"
         />
       <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel  ID="pnlModelPoupp" runat="server" CssClass="modal6Popup" >
       
      <table align="left" border="0"  width="100%">
      <tr>
        <td align="left" class="button">
       <strong> 
           <asp:Label ID="lblHeader" runat="server" Text="Add New" ></asp:Label>
        </strong>
        </td>
      
        <td align="center" width="40">
        <asp:Button ID="btnClose"  Height="28" CssClass="buybt" runat="server" Text="Close" CausesValidation="false"  />
         
        </td>
      </tr>
      </table>
      <table width="100%">
      <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="errormessage"></asp:Label>
        </td>
      </tr>
      </table>
      <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="98%" align="center" >

           <tr bordercolor="#839abf">
                <td>
      
      <table width="100%" cellpadding="5" cellspacing="5">
      <tr>
        <td class="copy10grey" align="left">
           <strong> Carrier Name:</strong>
        </td>
        <td >
        <asp:TextBox ID="txtCarrier" runat="server" Width="80%" CssClass="copy10grey" ></asp:TextBox> 
                
        </td>


      </tr>
      <tr>
        <td class="copy10grey" align="left">
            Carrier Logo:
        </td>
        <td >
        <asp:FileUpload ID="fuCarrier" CssClass="copy10grey" runat="server" />
        </td>


      </tr>
      <tr>
        <td class="copy10grey" align="left">
            &nbsp;
        </td>
        <td >
            <asp:CheckBox ID="chkActive" Text="Active" Checked="true" CssClass="copy10grey"  runat="server" />
        </td>


      </tr>
      <tr>
      <td >
          
      </td>
      </tr>
      </table>
       
        </td>
        </tr>
        </table>
        <br />
       <table width="100%" align="center">
        <tr>
            <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" OnClientClick="return Validate();" OnClick="btnSubmit_Click" Text="Submit" CssClass="buybt" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buybt"  />
            </td>
        </tr>
        </table>

      
      </asp:Panel>
      
    </td>
    </tr>
    
    </table>

    
    
    
    <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
    </div>
    </form>
</body>
</html>
