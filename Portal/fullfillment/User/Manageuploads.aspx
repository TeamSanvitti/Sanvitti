<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manageuploads.aspx.cs" Inherits="avii.fullfillment.User.Manageuploads" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="/admin/admhead.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Lan Global ::.</title>
    <link rel="stylesheet" href="./../calendar/dhtmlgoodies_calendar.css" media="screen"></link>
    <script type="text/javascript" src="./../calendar/dhtmlgoodies_calendar.js"></script>
    <link href="/aerostyle.css" rel="stylesheet" type="text/css">
    <script language="javascript" src="/avI.js" type="text/javascript"></script>
   
    <script type="text/javascript" language="javascript">

    function DispCal()
    {
		document.getElementById('<%=txtStartDate.ClientID%>').value = ''; 
		displayCalendar(document.getElementById('<%=txtStartDate.ClientID%>'),'mm/dd/yyyy',this,true);
    }
    
    function KeyDownHandler(btn)
    {

       if (event.keyCode == 13)
        {
            event.returnValue=false;
            event.cancel = true;
            btn.click();
        }
    }
    </script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnSearch)">
    <form id="form1" runat="server" method="post" >
        <table cellSpacing="0" cellPadding="0" width="780" align="center" border="0">
        	<tr>
				<td><head:menuheader id="HeadUser" runat="server"></head:menuheader>
				<head1:menuheader1 id="HeadAdmin" runat="server"></head1:menuheader1>
				</td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Manage Uploads/Files
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>							
			<tr><td align="center">
			
			<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                <tr bordercolor="#839abf">
                <td>
                        <table style="width: 100%" class="textblack">
                            <tr><td style="height:10px"><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"> </asp:Label></td></tr>
                            <tr>
                                <td id="TD1" runat="server" align="center">
                                
									<table border="0" width="100%">
										<tr>
											<td align="right" style="width: 100px" class="copy10grey" >From Date</td>
											<td  class="copy10grey" >
												<asp:TextBox ID="txtStartDate" runat="server" CssClass="txfield1" ReadOnly="True"></asp:TextBox><nobr />
												<img id="imgStartDate" onclick="document.getElementById('<%=txtStartDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtStartDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../calendar/sscalendar.jpg" />
											</td>
											<td align="right" class="copy10grey" >To Date</td>
											<td>
												<asp:TextBox ID="txtEndDate" runat="server" CssClass="txfield1" ReadOnly="True"></asp:TextBox><nobr />
												<img id="imgEndDate" onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../calendar/sscalendar.jpg" />
											</td>
										</tr>
										<tr runat="server" id="trUsers">
											<td  align="right" class="copy10grey">Username</td>
											<td  class="copy10grey" ><asp:DropDownList ID="ddlUsername" runat="server" Width="155px" CssClass="txfield1"></asp:DropDownList>
											</td>
											
										</tr>
										<tr>
										    <td  align="right" class="copy10grey">Upload Type</td>
											<td>
											    <asp:DropDownList ID="ddlReadStatus" runat="server" CssClass="txfield1">
											        <asp:ListItem Text="Files Upload" Value="U"></asp:ListItem>
											        <asp:ListItem Text="Files Receive" Value="R" Selected=True></asp:ListItem>
											    </asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td  class="copy10grey"  align="right" >
												Status</td>
											<td >
												<asp:DropDownList ID="ddlStatus" runat="server" Width="155px" CssClass="txfield1"></asp:DropDownList></td>
											<td>
										    </td>
										</tr>
										<tr><td>&nbsp;</td></tr>
										<tr>
										    <td colspan="4" align="center" >
										        <asp:Button ID="btnSearch" Width="190px" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
										        &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnAddNew" Width="190px" runat="server" CssClass="button" OnClick="btnAddNew_Click"
                                                    Text="Upload New File" />
										    </td>
										</tr>
									</table>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        </td>
                        </tr>
                        </table> 
                </td>
            </tr>
            <tr>
            <td align="center">
            <table width="100%" >
              <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvUserComments" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                                        CssClass="textblack" Width="100%" BorderColor="#989898" DataKeyNames="UploadedFileID"
                                        CellPadding="4" OnRowDataBound="gvUserComments_RowDataBound" OnRowCommand="gvUserComments_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkStatus" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Username" ItemStyle-CssClass="copy11" HeaderText="Uploaded By" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="copy11" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Filename", "~/UploadedData/{0}") %>'
                                                         Text='<%#DataBinder.Eval(Container.DataItem, "ActualFileName") %>'></asp:LinkButton>
                                                    <asp:Label ID="fname"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Filename") %>' Visible="false"></asp:Label>
                                  
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Filename
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Comments" ItemStyle-CssClass="copy11"  HeaderText="Comments" />
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="txfield1">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" Visible="false" CssClass="copy11" ID="lblUserStatus"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label Style="display: none"  CssClass="copy11"  runat="server" ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="0px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UploadedDate" ItemStyle-CssClass="copy11"  HeaderText="Uploaded Date" />
                                            <asp:TemplateField>
                                                <HeaderTemplate >
                                                    Email Address
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label CssClass="copy11" runat="server" ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="SubTitle" BackColor="#EAEAEA" />
                                        <AlternatingRowStyle BackColor="#EAEAEA" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" >
                                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Visible=false Text="Delete Files" OnClick="btnDelete_Click" />&nbsp;&nbsp;
                                    &nbsp;<asp:Button ID="btnSubmit" Width="190px" runat="server" CssClass="button" OnClick="btnSubmit_Click"
                                        Text="Submit" Visible="False" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
            
            </table>
            
            </td>
            </tr>
            <tr><td><br/><br/></td></tr>
				<tr>
					<td>
						&nbsp;
					</td>
				</tr>
				<tr>
					<td>
						<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter></td>
				</tr>
        </table>
    </form>
</body>
</html>
