Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.Security
Imports System.Globalization
Imports System.Threading

Namespace Orbelink.Orbecatalog6

    Public Class PageBaseClass
        Inherits System.Web.UI.Page

        'Instancias de clases
        Protected connection As New SQLServer(Configuration.Config_DefaultConnectionString)
        Protected queryBuilder As QueryBuilder = New QueryBuilder()
        Protected securityHandler As SecurityHandler = New SecurityHandler(Configuration.Config_DefaultConnectionString)
        Protected MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass

        Public Event okbutton As Orbelink.Orbecatalog6.MasterPageBaseClass.PopUpOKButton
        Dim _id_Actual As Integer

        Public Structure tabsInformation
            Dim direccion As String
            Dim iframe As String
        End Structure

        Public Enum ButtonsTheme As Integer
            Salvar = 0
            Modificar = 1
            Borrar = 2
            Cancelar = 3
        End Enum

        'Paginas
        ''' <summary>
        ''' Contiene la informacion de la cultura en uso
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentCulture() As CultureInfo
            Get
                Return Thread.CurrentThread.CurrentCulture
            End Get
            Set(ByVal value As CultureInfo)
                Thread.CurrentThread.CurrentUICulture = value
                Thread.CurrentThread.CurrentCulture = value
            End Set
        End Property

        ''' <summary>
        ''' Asigna el tema visual y la cultura en uso
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
            If Request.QueryString("popUp") IsNot Nothing Then
                MasterPageFile = "~/Orbecatalog/popUp.Master"
            End If
            Theme = securityHandler.Theme
            'myCultureInfo = CultureInfo.CurrentCulture

            UICulture = LanguageHandler.CurrentLanguageCultureCode
            Culture = LanguageHandler.CurrentLanguageCultureCode
            Dim cultinfo As CultureInfo = CultureInfo.GetCultureInfo(LanguageHandler.CurrentLanguageCultureCode)
            CurrentCulture = cultinfo
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            MyMaster = CType(Page.Master, MasterPageBaseClass)
            AddHandler okbutton, AddressOf PopUpOkEventHandler
            If MyMaster IsNot Nothing Then
                MyMaster.limpiarMensajes()
                MyMaster.RegistrarPopUpOk(okbuttonEvent)
            End If
        End Sub

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            Session("codigoPop") = Session("pantalla")
        End Sub

        Protected Sub AddScripts(ByVal level As Integer)
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            'Agrega Javascript al head
            Dim jsScript As New Literal
            jsScript.ID = "jsScript"
            jsScript.Text = System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/orbeEvents.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/interface.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"

            jsScript.Text &= System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/tiny_mce/tiny_mce_gzip.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"

            jsScript.Text &= System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/textoEditor.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"

            jsScript.Text &= System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/MenuHorizontal2Niveles.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<!--[if lt IE 7]>"
            jsScript.Text &= System.Environment.NewLine & "<script defer type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/pngfix.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<![endif]-->"

            Header.Controls.Add(jsScript)
            Dim refresh As New HtmlMeta
            refresh.HttpEquiv = "refresh"
            refresh.Content = ((Configuration.TimeOut_Minutes * 60) + 1) '& ";URL=http://psicobyte.com/html/"

            'Header.Controls.Add(refresh)
        End Sub

        Protected Sub SetThemeToExpandeCollapse(ByVal level As Integer, ByRef theImage As Image, ByVal theControlToShow As String, ByVal expanded As Boolean)
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            theImage.Attributes.Add("onclick", "javascript:toggleLayer('" & theControlToShow & "', this)")
            theImage.ImageUrl = prefijoNivel & "App_Themes/" & securityHandler.Theme

            If expanded Then
                theImage.ImageUrl &= "/images/info-expanded.gif"
            Else
                theImage.ImageUrl &= "/images/info-collapsed.gif"
            End If
        End Sub

        Protected Function contadorElementos(ByVal offset As Integer, ByVal pageSize As Integer, ByVal total As Integer) As String
            Dim resultado As String = "No hay elementos."
            Dim limite As Integer
            If (pageSize + offset) < total Then
                limite = pageSize + offset
            Else
                limite = total
            End If
            If total > 0 Then
                resultado = "<b>" & (offset + 1) & " - " & (limite) & "</b> de " & total & " elementos."
            End If
            Return resultado
        End Function

        Protected Sub ClearDataGrid(ByRef theDataGrid As DataGrid, ByVal clearInfo As Boolean)
            If clearInfo Then
                Dim indice As Integer = theDataGrid.Attributes("SelectedIndex")
                If indice >= 0 Then
                    If indice < theDataGrid.Items.Count Then
                        If indice Mod 2 = 0 Then
                            theDataGrid.Items(indice).CssClass = "tablaResultados_Item"
                        Else
                            theDataGrid.Items(indice).CssClass = "tablaResultados_Alternate"
                        End If
                        theDataGrid.Items(indice).Attributes("onmouseover") = "setRowFocus(this, " & indice & ", false)"
                        theDataGrid.Items(indice).Attributes("onmouseout") = "unsetRowFocus(this, " & indice & ", false)"
                    End If
                End If
                theDataGrid.Attributes("SelectedIndex") = -1
                theDataGrid.Attributes("SelectedPage") = -1
            Else
                If theDataGrid.Attributes("SelectedPage") = theDataGrid.CurrentPageIndex Then
                    If theDataGrid.Attributes("SelectedIndex") >= 0 Then
                        theDataGrid.Items(theDataGrid.Attributes("SelectedIndex")).CssClass = "tablaResultados_Selected"
                    End If
                End If
            End If
        End Sub

        Protected Sub SelectFromValue(ByRef theDataGrid As DataGrid, ByRef theDataTable As Data.DataTable, ByVal theColumName As String, ByVal valueToFind As String)
            Dim indiceColumna As Integer = theDataTable.Columns(theColumName).Ordinal
            If valueToFind > 0 And indiceColumna >= 0 Then
                Dim indiceData As Integer = -1
                Dim counter As Integer
                For counter = 0 To theDataTable.Rows.Count - 1
                    If valueToFind = theDataTable.Rows(counter).Item(indiceColumna) Then
                        indiceData = counter
                        Exit For
                    End If
                Next

                If indiceData >= 0 Then
                    Dim pagina As Integer = indiceData \ theDataGrid.PageSize
                    Dim indiceItem As Integer = indiceData Mod theDataGrid.PageSize
                    theDataGrid.CurrentPageIndex = pagina
                    theDataGrid.Attributes("SelectedIndex") = indiceItem
                    theDataGrid.Attributes("SelectedPage") = pagina
                    theDataGrid.Attributes("SelectedValue") = valueToFind

                    theDataGrid.DataBind()

                    theDataGrid.Items(indiceItem).CssClass = "tablaResultados_Selected"
                    theDataGrid.Items(indiceItem).Attributes("onmouseover") = "setRowFocus(this, " & indiceItem & ", true)"
                    theDataGrid.Items(indiceItem).Attributes("onmouseout") = "unsetRowFocus(this, " & indiceItem & ", true)"
                End If
            End If
        End Sub

        Protected Sub EditCommandDataGrid(ByRef theDataGrid As DataGrid, ByVal newItemIndex As Integer)
            theDataGrid.Attributes("SelectedIndex") = newItemIndex
            theDataGrid.Attributes("SelectedPage") = theDataGrid.CurrentPageIndex
            theDataGrid.Items(newItemIndex).CssClass = "tablaResultados_Selected"
            theDataGrid.Items(newItemIndex).Attributes("onmouseover") = "setRowFocus(this, " & newItemIndex & ", true)"
            theDataGrid.Items(newItemIndex).Attributes("onmouseout") = "unsetRowFocus(this, " & newItemIndex & ", true)"
        End Sub

        Protected Sub PageIndexChange(ByRef theDataGrid As DataGrid)
            Dim indice As Integer = theDataGrid.Attributes("SelectedIndex")

            If indice >= 0 Then
                If theDataGrid.Attributes("SelectedPage") = theDataGrid.CurrentPageIndex Then
                    theDataGrid.Items(indice).CssClass = "tablaResultados_Selected"
                    theDataGrid.Items(indice).Attributes("onmouseover") = "setRowFocus(this, " & indice & ", true)"
                    theDataGrid.Items(indice).Attributes("onmouseout") = "unsetRowFocus(this, " & indice & ", true)"
                Else
                    If indice < theDataGrid.Items.Count Then
                        If indice Mod 2 = 0 Then
                            theDataGrid.Items(indice).CssClass = "tablaResultados_Item"
                        Else
                            theDataGrid.Items(indice).CssClass = "tablaResultados_Alternate"
                        End If
                        theDataGrid.Items(indice).Attributes("onmouseover") = "setRowFocus(this, " & indice & ", false)"
                        theDataGrid.Items(indice).Attributes("onmouseout") = "unsetRowFocus(this, " & indice & ", false)"
                    End If
                End If
            End If
        End Sub

        Protected Sub checkTodos(ByRef theDataGrid As DataGrid, ByVal toCheck As Boolean, Optional ByVal theCheckControlId As String = "chk_Control")
            For counter As Integer = 0 To theDataGrid.Items.Count - 1
                Dim chk_Control As CheckBox = theDataGrid.Items(counter).FindControl(theCheckControlId)
                If chk_Control.Enabled Then
                    chk_Control.Checked = toCheck
                End If
            Next
        End Sub

        Protected Sub tabsFunciones(ByVal funciones As ArrayList)

            Dim tab_actual As New tabsInformation

            Dim li_tabsFunciones As New Literal
            li_tabsFunciones.ID = "li_tabsFunciones"
            Dim scriptArray As String = "<script type=""text/javascript"" >"
            scriptArray &= "var arrayFunciones = new Array();"
            scriptArray &= "arrayFunciones[0] = """";"
            For counter As Integer = 0 To funciones.Count - 1
                tab_actual = funciones(counter)
                scriptArray &= "arrayFunciones[" & counter & "] = ""loadURLonIframe(sender, '" & tab_actual.iframe & "', '" & tab_actual.direccion & "')"";"
            Next
            scriptArray &= "</script>"
            li_tabsFunciones.Text = scriptArray
            Page.Header.Controls.Add(li_tabsFunciones)

        End Sub

        Public Overridable Sub PopUpOkEventHandler(ByVal param As String)

        End Sub

        ''' <summary>
        ''' Propiedad de viewstate que mantiene un id
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Property id_Actual() As Integer
            Get
                If _id_Actual <= 0 Then
                    If ViewState("id_Actual") IsNot Nothing Then
                        _id_Actual = ViewState("id_Actual")
                    Else
                        ViewState.Add("id_Actual", 0)
                        _id_Actual = 0
                    End If
                End If
                Return _id_Actual
            End Get
            Set(ByVal value As Integer)
                _id_Actual = value
                ViewState("id_Actual") = _id_Actual
            End Set
        End Property

        'Gridview
        Protected Sub BindGridView(ByVal datos As Data.DataTable, ByVal theGridView As GridView, ByVal theNoLabel As Label, ByVal theDataFieldNames() As String, ByVal theConsultarPage As String, ByVal asPopUp As Boolean)
            If theGridView.Columns.Count = 0 Then
                Dim consultarText As String = Resources.Orbecatalog_Resources.Consultar

                Dim linkField As New BoundField
                linkField.DataField = theDataFieldNames(0)

                'Ojo revisar si vienen varios fields
                Dim hrefFormatString As String = theConsultarPage & "?" & theDataFieldNames(0) & "={0}"

                If asPopUp Then
                    linkField.DataFormatString = "<a href=""" & MyMaster.obtenerIframeString(hrefFormatString) & """ >" & consultarText & "</a>"
                Else
                    linkField.DataFormatString = "<a href=""" & hrefFormatString & """ >" & consultarText & "</a>"
                End If
                linkField.HtmlEncodeFormatString = False
                linkField.HeaderText = consultarText
                theGridView.Columns.Add(linkField)
            End If
            BindGridView(datos, theGridView, theNoLabel, theDataFieldNames)
        End Sub

        Protected Sub BindGridView(ByVal datos As Data.DataTable, ByVal theGridView As GridView, ByVal theNoLabel As Label, ByVal theDataFieldName As String)
            Dim algo() As String = {theDataFieldName}
            BindGridView(datos, theGridView, theNoLabel, algo)
        End Sub

        Protected Sub BindGridView(ByVal datos As Data.DataTable, ByVal theGridView As GridView, ByVal theNoLabel As Label, ByVal theDataFieldNames() As String)
            If datos IsNot Nothing Then
                theGridView.AutoGenerateColumns = False
                If theGridView.Columns.Count < 2 Then
                    For Each columna As Data.DataColumn In datos.Columns
                        If columna.ColumnMapping <> Data.MappingType.Hidden Then
                            Dim fieldTemp As New BoundField
                            fieldTemp.DataField = columna.ColumnName
                            'fieldTemp.HeaderText = CType(GetGlobalResourceObject(theResourcesClass, columna.ColumnName), String)
                            fieldTemp.HeaderText = columna.ColumnName
                            If columna.DataType.Equals(Date.UtcNow.GetType) Then
                                fieldTemp.DataFormatString = "{0:" & CurrentCulture.DateTimeFormat.ShortDatePattern & "}"
                            End If
                            theGridView.Columns.Add(fieldTemp)
                        End If
                    Next
                    If theDataFieldNames IsNot Nothing Then
                        theGridView.DataKeyNames = theDataFieldNames
                    End If
                End If
                theGridView.DataSource = datos
                theGridView.DataBind()
                theGridView.Visible = True
            Else
                theGridView.Visible = False
            End If

            If theNoLabel IsNot Nothing Then
                theNoLabel.Visible = Not theGridView.Visible
            End If
        End Sub
    End Class
End Namespace