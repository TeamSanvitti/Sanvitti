<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="esnpopup.aspx.cs" Inherits="avii.RMA.esnpopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Choose Esn</title>
    <script type="text/javascript" language="javascript">
        function closepopup(skumaker) {
           // window.parent.document.hello();
            // return false;
            var iditem = window.parent.document.getElementById("hdnitemcode").value;
            window.parent.document.getElementById(iditem).innerHTML = document.getElementById("hdnitemcode").value;
//            var idmaker = window.parent.document.getElementById("hdnmaker").value;
//            window.parent.document.getElementById(idmaker).innerHTML = document.getElementById("hdnmaker").value;
            var idpodid = window.parent.document.getElementById("hdnpodid").value;
            window.parent.document.getElementById(idpodid).value = document.getElementById("hdnpodid").value;
            window.parent.document.getElementById("btnSubmitRMA").disabled=false;
        }
       
        function selectESN(obj) {
            // window.parent.document.hello();
            // return false;
            var oItem = obj.children;
            var theBox = (obj.type == "radio") ? obj : obj.children.item[0];

            xState = theBox.unchecked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "radio" && elm[i].id != theBox.id) {
                    elm[i].checked = xState;
                }
            }
            var objid = "rdbesn";
            //alert(obj.id.replace(objid, 'lblmaker'));
//            var objmaker = document.getElementById(obj.id.replace(objid,'lblmaker'));
//            document.getElementById('hdnmaker').value = objmaker.innerHTML;

            var objsku = document.getElementById(obj.id.replace(objid, 'lblsku'));
            document.getElementById('hdnitemcode').value = objsku.innerHTML;
            
            var objpodid = document.getElementById(obj.id.replace(objid,'hdnpod_id'));
            document.getElementById('hdnpodid').value = objpodid.value;
            
            var iditem = window.parent.document.getElementById("hdnitemcode").value;
            window.parent.document.getElementById(iditem).innerHTML = document.getElementById("hdnitemcode").value;
//            var idmaker = window.parent.document.getElementById("hdnmaker").value;
//            window.parent.document.getElementById(idmaker).innerHTML = document.getElementById("hdnmaker").value;

            var idlblitem = window.parent.document.getElementById("hdnlblitemcode").value;
            window.parent.document.getElementById(idlblitem).value = document.getElementById("hdnitemcode").value;
//            var idlblmaker = window.parent.document.getElementById("hdnlblmaker").value;
//            window.parent.document.getElementById(idlblmaker).value = document.getElementById("hdnmaker").value;
            
            var idpodid = window.parent.document.getElementById("hdnpodid").value;
            //alert(document.getElementById("hdnpodid").value);
            window.parent.document.getElementById(idpodid).value = document.getElementById("hdnpodid").value;
            //alert(window.parent.document.getElementById(idpodid).value);
            window.parent.document.getElementById("btnSubmitRMA").disabled = false;
        }
        
</script>
<LINK href="../../aerostyle.css" type="text/css" rel="stylesheet">
    <link href="../Styles.css" type="text/css" rel="stylesheet" />
    

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:HiddenField ID="hdnitemcode" runat="server" />
       <%-- <asp:HiddenField ID="hdnmaker" runat="server" />--%>
        
        <asp:HiddenField ID="hdnpodid" runat="server" />
        <asp:GridView ID="GVEsn" runat="server"  CssClass="copy10grey" Width="98%" AutoGenerateColumns="false">
        <Columns>
        <asp:TemplateField HeaderStyle-CssClass="buttongrid">
        <ItemTemplate>
            <asp:RadioButton runat="server" id="rdbesn"  CssClass="copy10grey"
                onclick="selectESN(this)">
            </asp:RadioButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ESN" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
         <asp:HiddenField ID="hdnpod_id" runat="server" Value='<%# Eval("pod_id") %>'/>
            <asp:Label runat="server" CssClass="copy10grey" Text='<%# Eval("esn") %>' id="lblEsn"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SKU#"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
           <asp:Label runat="server" CssClass="copy10grey" Text='<%# Eval("itemcode") %>' id="lblsku"></asp:Label>
           </ItemTemplate>
        </asp:TemplateField>
        
        <%--<asp:TemplateField HeaderText="MAKER"  HeaderStyle-CssClass="button" ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
            <asp:Label runat="server" CssClass="copy10grey" Text='<%# Eval("maker") %>' id="lblmaker"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
      </Columns>
      </asp:GridView>
        
    </div>
    </form>
</body>
</html>
