Public Class frmItem
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dpPType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dpManu As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkNew As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkUsed As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkRef As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents dgFeatures As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtModel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents fImage As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.btnSubmit.Attributes.Add("onclick", "return fnValidate();")
        SetBlankFeature()
    End Sub

    Private Function SetBlankFeature()
        Dim dtFeature As New DataTable
        Dim pkCol As DataColumn = dtFeature.Columns.Add("ID", Type.GetType("System.String"))
        dtFeature.Columns.Add("ftext", Type.GetType("System.String"))

        Dim loDR As DataRow = dtFeature.NewRow()
        loDR.Item(0) = dtFeature.Rows.Count
        loDR.Item(1) = String.Empty
        dtFeature.Rows.Add(loDR)

        ViewState("Feature") = dtFeature
        With dgFeatures
            .DataSource = dtFeature
            .DataBind()
        End With
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        dpPType.SelectedIndex = 0
        txtModel.Text = String.Empty
        txtDesc.Text = String.Empty
        fImage.Value = String.Empty
        dpManu.SelectedIndex = 0
        txtPrice.Text = String.Empty
        chkNew.Checked = False
        chkUsed.Checked = False
        chkRef.Checked = False
    End Sub
End Class
