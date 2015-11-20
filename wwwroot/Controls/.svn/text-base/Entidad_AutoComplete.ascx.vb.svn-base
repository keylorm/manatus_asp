
Partial Class Controls_Entidad_AutoComplete
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Page.ClientScript.IsClientScriptBlockRegistered("Controls_Entidad_AutoComplete_script") Then
            Dim script As String = "function setValueOnAutoComplete(source, eventArgs) {" & System.Environment.NewLine
            script &= "var elValue = eventArgs.get_value();" & System.Environment.NewLine
            script &= "var elemento = document.getElementById(source.get_id());" & System.Environment.NewLine
            script &= "elemento.value = elValue;" & System.Environment.NewLine
            script &= "alert(elemento.value);" & System.Environment.NewLine
            script &= "}"
            Me.Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Controls_Entidad_AutoComplete_script", script, True)
        End If

        If Not IsPostBack Then
            hdn_Entidad.Value = 0
        End If

        ace_tbx_Entidad.FirstRowSelected = "true"
        ace_tbx_Entidad.TargetControlID = tbx_Entidad.ID
        ace_tbx_Entidad.ServiceMethod = "GetCompletionList"
        ace_tbx_Entidad.ServicePath = "~/Orbecatalog/Contactos/Entidades.asmx"
        ace_tbx_Entidad.OnClientItemSelected = "setValueOnAutoComplete"
        ace_tbx_Entidad.BehaviorID = hdn_Entidad.ClientID
        ace_tbx_Entidad.CompletionListCssClass = "autocomplete_completionListElement"
        ace_tbx_Entidad.CompletionListItemCssClass = "autocomplete_listItem"
        ace_tbx_Entidad.CompletionListHighlightedItemCssClass = "autocomplete_highlightedListI"
        tbx_Entidad.Attributes.Add("autocomplete", "off")
    End Sub

    Dim _tempContextKey As String
    Public Property ContextKey() As String
        Get
            If _tempContextKey <= 0 Then
                If ViewState(Me.ID & "vs_ContextKey") IsNot Nothing Then
                    _tempContextKey = ViewState(Me.ID & "vs_ContextKey")
                Else
                    ViewState.Add(Me.ID & "vs_ContextKey", 0)
                    _tempContextKey = 0
                End If
            End If
            Return _tempContextKey
        End Get
        Set(ByVal value As String)
            _tempContextKey = value
            ViewState(Me.ID & "vs_ContextKey") = _tempContextKey

            If _tempContextKey.Length > 0 Then
                ace_tbx_Entidad.ContextKey = ContextKey
                ace_tbx_Entidad.UseContextKey = True
            Else
                ace_tbx_Entidad.ContextKey = ""
                ace_tbx_Entidad.UseContextKey = False
            End If
        End Set
    End Property

    Public ReadOnly Property SelectedValue() As Integer
        Get
            Return hdn_Entidad.Value
        End Get
    End Property

    Public Sub SetSelectedEntidad(ByVal theId As Integer, ByVal theConnectionString As String)
        Dim entidadHandler As New Orbelink.Control.Entidades.EntidadHandler(theConnectionString)

        hdn_Entidad.Value = theId
        tbx_Entidad.Text = entidadHandler.ConsultarNombreEntidad(theId)
    End Sub

End Class
