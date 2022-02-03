<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ESNImageUpload.aspx.cs" Inherits="Sanvitti1.RMA.ESNImageUpload" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization (RMA) - Upload</title>
    <link href="/wsastyle.css" type="text/css" rel="stylesheet" />
     <link rel="stylesheet" type="text/css" href="/fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
    <script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    
	<script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
   <script type="text/javascript">
       $(document).ready(function () {


           $("#divPicture").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 650,
               width: 950,
               resizable: false,

               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });


          

       });


       function closedPictureDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divPicture").dialog('close');

           return false;
       }

       function openPictureDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 20;
           else
               top = 20;
           //top = top - 600;
           left = 150;
           $("#divPicture").dialog("option", "title", title);
           $("#divPicture").dialog("option", "position", [left, top]);

           $("#divPicture").dialog('open');

           //unblockPictureDialog();
       }
       function openPictureDialogAndBlock(title, linkID) {
           openPictureDialog(title, linkID);

           //block it to clean out the data
           $("#divPicture").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }
       function unblockPictureDialog() {
           $("#divPicture").unblock();
       }


       
        </script>

    <script type="text/javascript">
        function close_window() {
            window.close();
        }
        function validateFileSize() {
            var uploadControl = document.getElementById('<%= fuESNPic.ClientID %>');
            var filelen = uploadControl.value;

            var fileSize = document.getElementById('<%= hdnSize.ClientID %>').value;
            //alert(fileSize)
            if (filelen.length == 0) {

                //document.getElementById('dvMsg').style.display = "none";
                //document.getElementById('Div1').style.display = "block";
                alert('Upload file required!')
                return false;
            }
            else {
                if (uploadControl.files[0].size > fileSize) {
                    //document.getElementById('Div1').style.display = "none";
                    //document.getElementById('dvMsg').style.display = "block";
                    var fileSizeInMB = Math.floor(fileSize/1048576);
                    if (fileSizeInMB == 0)
                        fileSizeInMB = 2;

                    //alert(fileSizeInMB)
                    alert('Maximum size allowed is ' + fileSizeInMB + ' MB!');
                    return false;
                }
                else {
                    //document.getElementById('Div1').style.display = "none";
                   // document.getElementById('dvMsg').style.display = "none";
                    return true;
                }
            }
        }
     </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
    </table>

     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ></asp:ScriptManager> 


        <div id="divContainer">
         <div id="divPicture" style="display:none">
					
				<asp:UpdatePanel ID="upPicture" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phPicture" runat="server">
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                            <asp:HiddenField ID="hdnSize" runat="server" />
                            <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <br />

                                <table class="box" width="95%" align="center" cellspacing="5" cellpadding="5">
                                <tr>
                                <td class="copy10grey" >

                                    <asp:Image ID="imgESNPic" runat="server" />
                                </td>
                                </tr>
                                </table>
                            </td>
                            </tr>
                            </table>
                     </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
             </div>

                                
        </div>

    <asp:UpdatePanel ID="uPnl" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>
                   
                    
        <table width="95%" align="center" cellspacing="0" cellpadding="0"> 
        <tr>
            <td>
             
                <table style="text-align: left; width:100%;" align="center" class="copy10grey">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" ID="hdncount" />
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonlabel" align="left">
                            &nbsp; Return Merchandise Authorization (RMA) - Upload Triage Images
                        </td>
                    </tr>
                    
                </table>
    <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
        <tr bordercolor="#839abf">
            <td>
            <br />
            <asp:Label ID="lblPic" runat="server" CssClass="errormessage"></asp:Label>
                            <table class="box" width="100%" align="center" cellspacing="5" cellpadding="5">
                            <tr valign="top">
                                <td class="copy10grey" style="width:350px" align="right">
                                    RMA#: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                                <td class="copy10grey" style="width:150px" align="right">
                                    RMA Date: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblRMADate" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="copy10grey" style="width:350px" align="right">
                                    RMA Status: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblRMAStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                                <td class="copy10grey" style="width:150px" align="right">
                                    Company Name: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblCompanyName" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>    
                            <tr valign="top">
                                <td class="copy10grey" style="width:350px" align="right">
                                    ESN: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblESN" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                                <td class="copy10grey" style="width:150px" align="right">
                                    SKU#: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblSKU" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>    
                            <tr valign="top">
                                <td class="copy10grey" style="width:350px" align="right">
                                    ESN Status: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left" style="width:300px">
                                    <asp:Label ID="lblESNStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                                <td class="copy10grey" style="width:150px" align="right">
                                    Reason: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblReason" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>    
                            <%--<tr valign="top">
                                <td class="copy10grey" style="width:550px" align="right">
                                    ESN: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    
                                    
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="4" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="center" colspan="4">
                                    Upload file: &nbsp;&nbsp;&nbsp;&nbsp;
                                <%--</td>
                                <td class="copy10grey" align="left" colspan="2">--%>
                                    <asp:FileUpload ID="fuESNPic" runat="server" />
                                    
                                    <%--<div id="dvMsg" style="background-color:Red; color:White; width:190px; padding:3px; display:none;" >
                                    Maximum size allowed is 2 MB
                                    </div>  
                                    <div id="Div1" style="background-color:Red; color:White; width:190px; padding:3px; display:none;" >
                                    Upload file is required!
                                    </div>  --%>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="4" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" align="center" class="copy10grey">
                                    <asp:Button ID="btnPicture" runat="server" Text="  Upload  " CssClass="buybt" 
                                    OnClientClick="return validateFileSize();"   OnClick="btnPicture_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnPicCancel" runat="server" Text=" Cancel " CssClass="buybt"   
                                    OnClientClick="close_window();return false;"  />
                                   
                                </td>
                            </tr>

                            </table>
                            <br />
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
            <td>
            <asp:Panel ID="pnlDoc" runat="server"> 
    <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
        <tr bordercolor="#839abf">
            <td>
                <asp:Repeater ID="rptRMA" runat="server" OnItemDataBound="rptRMA_ItemDataBound">
                                    <HeaderTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="button">
                                            &nbsp;File Name
                                            </td>
                                            <td class="button">
                                            &nbsp;Upload Date
                                            </td>
                                            <td class="button">
                                            &nbsp;Uploaded By
                                            </td>
                                            <td class="button">
                                                &nbsp;
                                            </td>                
                                        </tr>
                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey" align="left" width="55%">
                                            <asp:LinkButton ID="lnlImage" CommandArgument='<%# Eval("FileName")%>'  
                                            OnCommand="DownloadRmaDoc_Click" runat="server" Text='<%# Eval("FileName")%>' >
                                            
                                            
                                            </asp:LinkButton>
                                            <%--<img src='../Documents/ESN/<%# Eval("FileName")%>' alt="" />--%>
                                        
                                        </td>
                                        <td class="copy10grey" align="left" width="20%">
                                        <%# Eval("CreateDate")%>
                                        </td>
                                        <td class="copy10grey" align="left" width="20%">
                                        <%# Eval("CreatedBy")%>
                                        </td>
                                        <td class="copy10grey" align="center" width="1%">
                                              <asp:ImageButton ID="imgDel" runat="server" OnClientClick="return confirm('Do you want to delete?');"  
                                              CommandName="Delete" AlternateText="Delete ESN Image" ToolTip="Delete ESN Image" 
                                                ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("PictureID") %>' OnCommand="imgDeleteImage_OnCommand"/>
                        
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
        </asp:Panel>    
            </td>
        </tr>
        </table>

             
</ContentTemplate>
                <Triggers>
                        <asp:PostBackTrigger ControlID="btnPicture" />
                        
                     
                 </Triggers>
            </asp:UpdatePanel>
     
        
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
                </asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>

    </form>
</body>
</html>
