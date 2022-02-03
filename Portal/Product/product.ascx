<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="product.ascx.cs" Inherits="avii.product.WebUserControl1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<table cellspacing="0" style="width: 100%">
<tr>
<td>


<asp:ScriptManager ID="ScriptManager1" runat="server"  >
</asp:ScriptManager>
<asp:UpdatePanel ID="UpSearch" runat="server" >
    <ContentTemplate>
<table cellspacing="0" style="width: 100%" align="center">
			<tr>
				<td class="auto-style25"><strong>Search Products</strong>
                
                </td>

			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
            <tr>
				<td class="auto-style27">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Keywords</td>
			</tr>
            <tr>
				<td align="center">
                
                
                    <asp:TextBox ID="txtSearch" runat="server" style="width: 179px; height:30px; border:0px;" CssClass="copy10grey" ></asp:TextBox>
                </td>
			</tr>
			<tr>
				<td class="auto-style27" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Carriers</td>
			</tr>
			<tr>
				<td align="center">
                <asp:DropDownList ID="ddltechnology" runat="server" style="width: 180px; height:30px; border:0px;"
                            onselectedindexchanged="ddltechnology_SelectedIndexChanged" AutoPostBack="true"  >
                        </asp:DropDownList>
<%--
					<select name="Carriers0" style="width: 180px; height:30px; border:0px;">
					<option selected="selected">Select Carriers ...</option>
					<option>CDMA</option>
					<option>GSM</option>
					</select>--%>
                    
                    </td>
			</tr>
			<tr>
				<td class="auto-style27">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone OEM</td>
			</tr>
			<tr>
				<td align="center">
                <asp:DropDownList ID="ddlmaker" runat="server" style="width: 180px; height:30px; border:0px;" AutoPostBack="True" Enabled="false"
                            onselectedindexchanged="ddmaker_SelectedIndexChanged">
                       
                        </asp:DropDownList>
                        
					<%--<select name="Phone Maker" style="width: 180px; height:30px; border:0px;">
					<option selected="selected">Select Phone Maker ...</option>
					<option>Blackberry</option>
					<option>Credle point</option>
					<option>Huawei</option>
					<option>LG</option>
					<option>Motorola</option>
					<option>Novatel</option>
					<option>Pantech</option>
					<option>OCD</option>
					<option>Samsung</option>
					<option>Sierra Wireless</option>
					<option>ZTE</option>
					</select>--%>
                    </td>
			</tr>
			<tr>
				<td class="auto-style27">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
				Phone Model</td>
			</tr>
			<tr>
				<td align="center">
                    <asp:DropDownList ID="ddlmodel" runat="server"  Enabled="False" style="width: 180px; height:30px; border:0px;">
                    </asp:DropDownList>
					<%--<select name="Carriers1" style="width: 180px; height:30px; border:0px;">
					<option selected="selected">Select Model ...</option>
					<option>None</option>
					<option>None</option>
					</select>--%>
                    </td>
			</tr>
		</table>
<%--
<table width="200" border="0" cellpadding="0" cellspacing="0" class="copy10grey" align="center">
    <tr>
	    <td align="center" bgcolor="#dee7f6" class="button" width="101%">
		    
            <div id="searchHeader">Search Products</div>
            </td>
    </tr>
	<tr>
        <td>
			<div align="center" style="width:100%">
		        <img src="../images/5_pic_3.jpg" width="178" height="90" hspace="1" vspace="0">
	        </div>
        </td>
    </tr>
</table>

               <table width="200" cellpadding="1" cellspacing="3" border="0">
                   
                
                <tr>
                    <td class="copy10grey" align="left">
                        Technology
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="left">
                    
                        
                       </td>
                </tr>
                <tr>
                    <td  align="left" class="copy10grey">
                        Phone Maker
                    </td>
                </tr>
                <tr>
                    <td align="left" class="copy10grey">
                        
                       
                        
                    </td>
                </tr>
                <tr>
                    <td  class="copy10grey" align="left">
                       Phone Model
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="left">
                        
                      
                    </td>
                </tr>                        
                <tr >
                    <td align="center">
                        
                    </td>
                </tr>
                
                </table>--%>
    </ContentTemplate>
</asp:UpdatePanel>
</td>			
</tr>

<tr>
<td>
                   <table cellspacing="0" style="width: 100%">
                   <tr>
                    <td align="center">
                    <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="button" 
                            onclick="btnsearch_Click" UseSubmitBehavior="False" /> 
                    </td>
                   </tr>
                   </table>
                   
                   
</td>			
</tr>

</table>
