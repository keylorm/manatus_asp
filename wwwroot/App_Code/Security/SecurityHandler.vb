Imports Microsoft.VisualBasic
Imports System.Web.Security
Imports System.Web.HttpContext
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Security
Imports Orbelink.Entity.Orbecatalog

Namespace Orbelink.Control.Security

    Public Class SecurityHandler

        Dim id_Perfil As Integer = -1
        Dim id_Usuario As Integer = -1
        Dim id_EntidadAsociada As Integer = -1
        Dim _nivel As Integer = -1
        Dim _LaPantalla As Pantalla
        Dim _theme As String

        Dim _connString As String

        Public Enum VisibilidadPantalla
            NoVisible = 0
            EnMenu = 1
            EnSubMenu = 2
            EnMantenimiento = 3
        End Enum

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
            _theme = "Classic6"
        End Sub

        'Usuario
        ''' <summary>
        ''' El identificador del tipo de Usuario del usuario actual. 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>De ser cero es el administrador y de ser menos uno es que no se ha autenticado</remarks>
        ReadOnly Property Perfil() As Integer
            Get
                If id_Perfil < 0 Then
                    If Not Current Is Nothing Then
                        If Not Current.Session Is Nothing Then
                            If Not Current.Session("id_Perfil") Is Nothing Then
                                id_Perfil = Current.Session("id_Perfil")
                            End If
                        End If
                    End If
                End If
                Return id_Perfil
            End Get
        End Property

        ReadOnly Property EntidadAsociada() As Integer
            Get
                If id_EntidadAsociada < 0 Then
                    If Not Current Is Nothing Then
                        If Not Current.Session Is Nothing Then
                            If Not Current.Session("id_EntidadAsociada") Is Nothing Then
                                id_EntidadAsociada = Current.Session("id_EntidadAsociada")
                            End If
                        End If
                    End If
                End If
                Return id_EntidadAsociada
            End Get
        End Property

        ''' <summary>
        ''' Usuario que esta autenticado en el sistema
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Usuario() As Integer
            Get
                If id_Usuario < 0 Then
                    If Not Current Is Nothing Then
                        If Not Current.Session Is Nothing Then
                            If Not Current.Session("id_Usuario") Is Nothing Then
                                id_Usuario = Current.Session("id_Usuario")
                            End If
                        End If
                    End If
                End If
                If id_Usuario = 0 Then
                    Return Configuration.Config_DefaultIdEntidad
                Else
                    Return id_Usuario
                End If
            End Get
        End Property

        Public ReadOnly Property IsAdmin() As Boolean
            Get
                Dim algo As Integer = Usuario
                If id_Usuario = 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        <Obsolete("Favor usar la propiedad Usuario o EntidadAsociada. Esta sera descontinuada", False)> _
        ReadOnly Property Entidad() As Integer
            Get
                Return Usuario
            End Get
        End Property

        <Obsolete("Favor usar la propiedad Perfil o TipoEntidadAsociada. Esta sera descontinuada", False)> _
       ReadOnly Property TipoEntidad() As Integer
            Get
                Return Perfil
            End Get
        End Property

        ''' <summary>
        ''' nivel num�rico de acceso para el usuario autenticado seg�n su tipo de Usuario
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ClearanceLevel() As Integer
            Get
                If _nivel < 0 Then
                    If Not Current Is Nothing Then
                        If Not Current.Session Is Nothing Then
                            If Not Current.Session("_nivel") Is Nothing Then
                                _nivel = Current.Session("_nivel")
                            End If
                        End If
                    End If
                End If
                Return _nivel
            End Get
        End Property

        ''' <summary>
        ''' Cadena de texto con el nombre del tema visual en uso
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Theme() As String
            Get
                If Not Current Is Nothing Then
                    If Not Current.Request.Cookies("theme") Is Nothing Then
                        _theme = Current.Request.Cookies("theme").Value
                    ElseIf Not Current.Session("theme") Is Nothing Then
                        _theme = Current.Session("theme")
                        Current.Request.Cookies.Add(New HttpCookie("theme", _theme))
                    End If
                End If
                Return _theme
            End Get
            Set(ByVal value As String)
                Current.Session("theme") = value
                _theme = value
                If Current.Request.Cookies("theme") Is Nothing Then
                    Current.Request.Cookies.Add(New HttpCookie("theme", _theme))
                End If
            End Set
        End Property

        Sub LogOut()
            If Not Current Is Nothing Then
                Current.Session("id_Perfil") = -1
                Current.Session("id_Usuario") = -1
                Current.Session("_nivel") = -1
                id_Perfil = -1
                id_Usuario = -1
                _nivel = -1
            End If
        End Sub

        Public Function CrearUsuario(ByVal id_Usuario As String, ByVal nombreUsuario As String, ByVal password As String) As Boolean
            Dim usuario As New Usuario
            If nombreUsuario.Length > 0 And password.Length > 0 Then
                usuario.UserName.Value = nombreUsuario
                usuario.PasswordHash.Value = hashPassword(password)
                'Ojo deberia crearlo, pero todavia es una Usuario
                updateUsuario(usuario, id_Usuario)
            End If
        End Function

        Function LogIn(ByVal Username As String, ByVal Password As String, ByVal timeZoneId As String, ByVal theLanguage As Integer) As Boolean
            Orbelink.DateAndTime.DateHandler.CurrentTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)
            Orbelink.DBHandler.LanguageHandler.CurrentLanguage = theLanguage
            Return LogIn(Username, Password)
        End Function

        Function LogIn(ByVal elUsername As String, ByVal elPassword As String) As Boolean
            If elUsername.Length > 0 And elPassword.Length > 0 Then
                If elUsername = "SiteAdmin" Then
                    If Configuration.SiteAdminPasswordHash.CompareTo(hashPassword(elPassword)) = 0 Then
                        RegistrarUsuarioAutenticado(0, 0, 10, Configuration.Config_DefaultIdEntidad)
                        Return True
                    End If
                Else
                    Return VerificarUsuarioBD(elUsername, elPassword)
                End If
            End If
        End Function

        Function VerifyUser(ByRef lnk_User As HyperLink, ByRef lnk_LogOut As HyperLink, ByVal nivel As Integer) As Boolean
            Dim Usuario As New Usuario
            Dim sqlQuery As String
            Dim dataset As Data.DataSet
            Dim userOk As Boolean
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To nivel - 1
                prefijoNivel &= "../"
            Next

            If IsAdmin Then
                userOk = True
                lnk_User.Text = "Administrador del Sitio"
                lnk_User.Visible = True
            Else
                Usuario.UserName.ToSelect = True
                Usuario.Id_Usuario.ToSelect = True
                Usuario.Id_Usuario.Where.EqualCondition(id_Usuario)
                Usuario.Id_Perfil.Where.EqualCondition(id_Perfil)
                sqlQuery = queryBuilder.SelectQuery(Usuario)

                Dim connection As New SQLServer(_connString)
                dataset = connection.executeSelect(sqlQuery)
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataset.Tables(0), 0, Usuario)
                        userOk = True
                        lnk_User.Text = Usuario.UserName.Value
                        lnk_User.Visible = True
                        lnk_User.NavigateUrl = prefijoNivel & "orbeCatalog/Contactos/Entidad.aspx?id_Entidad=" & Usuario.Id_Usuario.Value
                    End If
                End If
            End If

            If Not lnk_LogOut Is Nothing Then
                If userOk Then
                    lnk_LogOut.NavigateUrl = "~/orbecatalog/Login.aspx?action=out"
                    lnk_LogOut.Text = "Log out"
                Else
                    lnk_LogOut.NavigateUrl = "~/orbecatalog/Login.aspx"
                    lnk_LogOut.Text = "Log in"
                End If
            End If

            Return userOk
        End Function

        'Usuario privadas
        Private Function updateUsuario(ByVal usuario As Usuario, ByVal id_Usuario As Integer) As Boolean
            Try
                usuario.UserName.ToUpdate = True
                usuario.PasswordHash.ToUpdate = True
                'Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                usuario.Id_Usuario.Where.EqualCondition(id_Usuario)
                Dim connection As New SQLServer(_connString)
                If connection.executeUpdate(query.UpdateQuery(usuario)) = 1 Then
                    Return True
                End If
            Catch exc As DBException
            End Try
            Return False
        End Function

        Private Function VerificarUsuarioBD(ByVal elUsername As String, ByVal elPassword As String) As Boolean
            Dim loginOk As Boolean = False
            Dim elUsuario As New Usuario
            Dim elPerfil As New Perfil

            elUsuario.Id_Usuario.ToSelect = True
            elUsuario.Id_Perfil.ToSelect = True
            elUsuario.UserName.ToSelect = True
            elUsuario.Theme.ToSelect = True
            elUsuario.PasswordHash.ToSelect = True
            elUsuario.UserName.Where.EqualCondition(elUsername)

            Dim query As New QueryBuilder
            query.Join.EqualCondition(elPerfil.Id_Perfil, elUsuario.Id_Perfil)
            elPerfil.ClearanceLevel.ToSelect = True

            query.From.Add(elUsuario)
            query.From.Add(elPerfil)
            Dim connection As New SQLServer(_connString)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                elUsuario = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), elUsuario)
                elPerfil = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), elPerfil)

                If elUsuario.PasswordHash.Value.CompareTo(hashPassword(elPassword)) = 0 Then
                    RegistrarUsuarioAutenticado(elUsuario.Id_Perfil.Value, elUsuario.Id_Usuario.Value, elPerfil.ClearanceLevel.Value, elUsuario.Id_Usuario.Value) 'ojo debe cambiar por entidad
                    If elUsuario.Theme.IsValid Then
                        Theme = elUsuario.Theme.Value
                    End If
                    loginOk = True
                End If
            End If
            Return loginOk
        End Function

        Public Function ConsultarUsuario(ByVal id_usuario As Integer) As Usuario
            Dim queryBuilder As New QueryBuilder()
            Dim dataTable As New Data.DataTable
            Dim Usuario As New Usuario

            Usuario.Fields.SelectAll()
            queryBuilder.From.Add(Usuario)
            Usuario.Id_Usuario.Where.EqualCondition(id_usuario)

            dataTable = New SQLServer(_connString).executeSelect_DT(queryBuilder.RelationalSelectQuery)

            If dataTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(dataTable.Rows(0), Usuario)
            End If
            Return Nothing
        End Function

        Public Function ConsultarUsername(ByVal theId_Usuario As Integer) As String
            Dim query As New QueryBuilder()
            Dim usuario As New Usuario
            usuario.Id_Usuario.Where.EqualCondition(theId_Usuario)
            usuario.UserName.ToSelect = True
            query.From.Add(usuario)
            Dim resultTable As Data.DataTable = New SQLServer(_connString).executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                usuario = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), usuario)
            End If
            Return usuario.UserName.Value
        End Function

        'Funciones capa logica
        Private Sub RegistrarUsuarioAutenticado(ByVal elId_Perfil As Integer, ByVal elId_Usuario As Integer, ByVal elClearenceLevel As Integer, ByVal elId_EntidadAsociada As Integer)
            Current.Session("id_Perfil") = elId_Perfil
            Current.Session("id_Usuario") = elId_Usuario
            Current.Session("id_EntidadAsociada") = elId_EntidadAsociada
            Current.Session("_nivel") = elClearenceLevel
            _nivel = elClearenceLevel
            id_Perfil = elId_Perfil
            id_Usuario = elId_Usuario
            id_EntidadAsociada = elId_EntidadAsociada
        End Sub

        'Pantallas
        ''' <summary>
        ''' Estructura con los permisos de acceso a la pantalla actual para el usuario.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property LaPantalla() As Pantalla
            Get
                Return _LaPantalla
            End Get
        End Property

        ''' <summary>
        ''' Permisos a la pantalla
        ''' </summary>
        ''' <remarks></remarks>
        Structure Pantalla
            Public Leer As Boolean
            Public Crear As Boolean
            Public Modificar As Boolean
            Public Borrar As Boolean
        End Structure

        Public Sub VerifyAveliableScreens(ByRef theDataList As DataList, ByVal nivel As Integer, ByVal codigo_Padre As String, _
                ByVal theDTLSonID As String, ByVal theHyperLinkID As String, Optional ByVal theImagePath As String = "")
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim sqlQuery As String
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To nivel - 1
                prefijoNivel &= "../"
            Next

            If Me.Perfil = 0 Then
                pantallas.Fields.SelectAll()
                queryBuilder.From.Add(pantallas)
            Else
                pantallas.Fields.SelectAll()
                queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
                pantallas.activo.Where.EqualCondition(1)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            If codigo_Padre = "" Or codigo_Padre = "0" Then
                pantallas.Codigo_Pantalla.Where.EqualCondition_OnSameTable(pantallas.codigo_Padre)
            Else
                pantallas.codigo_Padre.Where.EqualCondition(codigo_Padre)
                pantallas.Codigo_Pantalla.Where.DiferentCondition(codigo_Padre)
            End If

            queryBuilder.Orderby.Add(pantallas.orden)
            queryBuilder.Distinct = True
            sqlQuery = queryBuilder.RelationalSelectQuery

            Dim connection As New SQLServer(_connString)
            dataset = connection.executeSelect(sqlQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    theDataList.Visible = True
                    theDataList.DataSource = dataset
                    theDataList.DataBind()

                    Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), pantallas)
                    For counter = 0 To resultados.Count - 1
                        Dim actual As Pantallas = resultados(counter)
                        Dim link As HyperLink = theDataList.Items(counter).FindControl(theHyperLinkID)
                        Dim dtl_Menus As DataList = theDataList.Items(counter).FindControl(theDTLSonID)

                        link.NavigateUrl = prefijoNivel & actual.Link.Value
                        link.Text = CType(GetGlobalResourceObject(actual.ResourceClass.Value, actual.ResourceKey.Value), String)
                        If theImagePath.Length > 0 Then
                            link.ImageUrl = prefijoNivel & theImagePath
                        End If

                        If Not dtl_Menus Is Nothing Then
                            VerifyAveliableScreens(dtl_Menus, nivel, actual.Codigo_Pantalla.Value, theDTLSonID, theHyperLinkID, theImagePath)
                        End If
                    Next
                    theDataList.Visible = True
                Else
                    theDataList.Visible = False
                End If
            Else
                theDataList.Visible = False
            End If
        End Sub

        Public Sub cargarMenu(ByRef menuContainer As HtmlControl, ByRef SubmenuContainer As HtmlControl)
            menuContainer.Controls.Clear()
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            Dim elNivel = Current.Session("NivelPantalla")
            For counter As Integer = 0 To elNivel - 1
                prefijoNivel &= "../"
            Next

            pantallas.Visibilidad.Where.EqualCondition(VisibilidadPantalla.EnMenu)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If Me.Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantalla_Perfil.codigo_Pantalla, pantallas.Codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            pantallas.Codigo_Pantalla.Where.EqualCondition_OnSameTable(pantallas.codigo_Padre)
            queryBuilder.Orderby.Add(pantallas.orden, False)

            Dim consulta As String = queryBuilder.RelationalSelectQuery
            Dim connection As New SQLServer(_connString)
            Dim dataset As Data.DataSet = connection.executeSelect(consulta)

            'dataset.WriteXmlSchema("../sch.xml")
            'dataset.WriteXml("data.xml")

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), pantallas)
                    For counter = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim actual As Pantallas = resultados(counter)
                        'Dim elLink As String = prefijoNivel & actual.Link.Value

                        Dim a_LinkMenu As New HtmlAnchor()

                        a_LinkMenu.InnerText = CType(GetGlobalResourceObject(actual.ResourceClass.Value, actual.ResourceKey.Value), String)
                        a_LinkMenu.HRef = "~/" & actual.Link.Value

                        Dim pantallaActual As String = Current.Session("pantalla")
                        If actual.Codigo_Pantalla.Value.Substring(0, 2) = pantallaActual.Substring(0, 2) Then
                            a_LinkMenu.Attributes.Add("class", "selected")
                        End If
                        menuContainer.Controls.Add(a_LinkMenu)
                        cargarSubMenu(SubmenuContainer, counter, actual.Codigo_Pantalla.Value, 2)
                    Next
                End If
            End If

        End Sub

        Protected Sub cargarSubMenu(ByRef SubmenuContainer As HtmlControl, ByVal indiceSub As Integer, ByVal codigoPadre As String, Optional ByVal menu As Integer = 1)
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim queryBuilder As New QueryBuilder

            pantallas.Visibilidad.Where.EqualCondition(VisibilidadPantalla.EnSubMenu)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If Me.Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            Dim corte As Integer = codigoPadre.IndexOf("-")
            Dim prefijoSeccion As String = codigoPadre.Substring(0, corte)
            pantallas.Codigo_Pantalla.Where.LikeCondition(prefijoSeccion)
            pantallas.codigo_Padre.Where.EqualCondition(codigoPadre)

            queryBuilder.Top = 7
            queryBuilder.Orderby.Add(pantallas.orden)

            Dim div As New HtmlControls.HtmlGenericControl("div")
            div.ID = "submenu_" & indiceSub + 1
            div.Style.Item("text-align") = "right"

            Dim connection As New SQLServer(_connString)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                For counter As Integer = 0 To resultTable.Rows.Count - 1
                    Dim actual As Pantallas = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(counter), pantallas)

                    Dim a_LinkMenu As New HtmlAnchor()
                    a_LinkMenu.InnerText = CType(GetGlobalResourceObject(actual.ResourceClass.Value, actual.ResourceKey.Value), String)
                    a_LinkMenu.HRef = "~/" & actual.Link.Value
                    div.Controls.Add(a_LinkMenu)

                    Dim divisor As New Literal()
                    divisor.Text = " | "
                    div.Controls.Add(divisor)
                Next
            End If
            SubmenuContainer.Controls.Add(div)
        End Sub

        Public Sub cargarBusqueda(ByRef gridLista As DataList, ByVal imagenId As String, ByVal hiddenId As String)
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim dataset As Data.DataSet
            Dim queryBuilder As New QueryBuilder

            pantallas.Visibilidad.Where.DiferentCondition(VisibilidadPantalla.NoVisible)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If Me.Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantalla_Perfil.codigo_Pantalla, pantallas.Codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If
            pantallas.Codigo_Pantalla.Where.LikeCondition("-SR")
            queryBuilder.Orderby.Add(pantallas.orden, False)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            Dim connection As New SQLServer(_connString)
            dataset = connection.executeSelect(consulta)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    gridLista.DataSource = dataset
                    gridLista.DataBind()
                    Dim prefijoNivel As String = ""

                    Dim elNivel = Current.Session("NivelPantalla")
                    For counter = 0 To elNivel - 2
                        prefijoNivel &= "../"
                    Next

                    Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), New Pantallas)
                    For counter As Integer = 0 To gridLista.Items.Count - 1
                        Dim act_Pantallas As Pantallas = resultados(counter)

                        Dim search_text As Label = gridLista.Items(counter).FindControl("search_text")
                        Dim imagen As Image = gridLista.Items(counter).FindControl("search_img")
                        Dim prefijoPantalla As String = act_Pantallas.Codigo_Pantalla.Value

                        search_text.Text = CType(GetGlobalResourceObject(act_Pantallas.ResourceClass.Value, act_Pantallas.ResourceKey.Value), String)

                        Dim indiceCorte As Integer = prefijoPantalla.IndexOf("-")
                        prefijoPantalla = prefijoPantalla.Substring(0, indiceCorte).ToLower
                        'imagen.Attributes.Add("codigo", prefijoPantalla)
                        imagen.AlternateText = prefijoPantalla

                        imagen.ToolTip = CType(GetGlobalResourceObject(act_Pantallas.ResourceClass.Value, act_Pantallas.ResourceKey.Value), String)

                        imagen.Attributes.Add("onclick", "javascript:changeImage('" & imagen.ClientID & "','" & imagenId & "', '" & hiddenId & "')")
                        search_text.Attributes.Add("onclick", "javascript:changeImage('" & imagen.ClientID & "','" & imagenId & "', '" & hiddenId & "')")
                        imagen.ImageUrl = "~/orbecatalog/iconos/Modulos/" & prefijoPantalla & "_small.JPG"
                    Next
                End If
            End If
        End Sub

        Public Function VerifyAveliableScreens_ForMantenimiento(ByRef theTreeView As TreeView, ByVal nivel As Integer, ByVal prefijoSeccion As String, ByVal collapsed As Boolean, Optional ByVal menuNoSeQueHace As Integer = 1, Optional ByVal goDeep As Boolean = True, Optional ByVal paraMantenimiento As Boolean = False) As Boolean
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To nivel - 1
                prefijoNivel &= "../"
            Next

            pantallas.Visibilidad.Where.EqualCondition(VisibilidadPantalla.EnMantenimiento)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If Me.Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            If prefijoSeccion.Length > 0 Then
                pantallas.codigo_Padre.Where.LikeCondition(prefijoSeccion)
            End If

            queryBuilder.Orderby.Add(pantallas.orden)
            Dim connection As New SQLServer(_connString)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            Return DoVerifyAveliableScreensBind(dataset, theTreeView, prefijoNivel, prefijoSeccion, collapsed, False)
        End Function

        Function VerifyAveliableScreens(ByRef theTreeView As TreeView, ByVal nivel As Integer, ByVal prefijoSeccion As String, ByVal collapsed As Boolean) As Boolean
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To nivel - 1
                prefijoNivel &= "../"
            Next

            pantallas.Visibilidad.Where.EqualCondition(VisibilidadPantalla.EnMenu)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If Me.Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            If prefijoSeccion.Length > 0 Then
                pantallas.Codigo_Pantalla.Where.LikeCondition(prefijoSeccion)
            End If

            queryBuilder.Orderby.Add(pantallas.orden)
            Dim connection As New SQLServer(_connString)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            Return DoVerifyAveliableScreensBind(dataset, theTreeView, prefijoNivel, prefijoSeccion, collapsed, True)
        End Function

        Private Function DoVerifyAveliableScreensBind(ByVal dataset As Data.DataSet, ByRef theTreeView As TreeView, ByVal prefijoNivel As String, ByVal prefijoSeccion As String, ByVal collapsed As Boolean, ByVal goDeep As Boolean) As Boolean
            Dim resultado As Boolean = False
            Dim pantallas As New Pantallas

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), pantallas)
                    For counter = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim actual As Pantallas = resultados(counter)
                        Dim elLink As String = prefijoNivel & actual.Link.Value

                        Dim nombrePantalla As String = CType(GetGlobalResourceObject(actual.ResourceClass.Value, actual.ResourceKey.Value), String)
                        Dim hijo As New TreeNode(nombrePantalla, actual.Codigo_Pantalla.Value, "", elLink, "_self")
                        theTreeView.Nodes.Add(hijo)

                        If goDeep Then
                            VerifyAveliableScreens_Nodes(theTreeView.Nodes(counter), prefijoNivel)
                        End If

                        If collapsed Then
                            theTreeView.Nodes(counter).CollapseAll()
                        Else
                            theTreeView.Nodes(counter).ExpandAll()
                        End If
                    Next
                    theTreeView.Visible = True
                    resultado = True
                Else
                    theTreeView.Visible = False
                    resultado = False
                End If
            End If
            Return resultado
        End Function

        Private Sub VerifyAveliableScreens_Nodes(ByRef elTreeNode As TreeNode, ByVal prefijoNivel As String)
            Dim codigo_Padre As String = elTreeNode.Value
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim sqlQuery As String
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim queryBuilder As New QueryBuilder

            pantallas.Visibilidad.Where.EqualCondition(VisibilidadPantalla.EnSubMenu)
            pantallas.Fields.SelectAll()
            pantallas.activo.Where.EqualCondition(1)

            If id_Perfil = 0 Then
                queryBuilder.From.Add(pantallas)
            Else
                queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)

                pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                pantalla_Perfil.Leer.Where.EqualCondition(1)

                queryBuilder.From.Add(pantallas)
                queryBuilder.From.Add(pantalla_Perfil)
            End If

            If codigo_Padre.Length > 0 Then
                pantallas.codigo_Padre.Where.EqualCondition(codigo_Padre)
                pantallas.Codigo_Pantalla.Where.DiferentCondition_OnSameTable(pantallas.codigo_Padre)

                queryBuilder.Orderby.Add(pantallas.orden)
                sqlQuery = queryBuilder.RelationalSelectQuery
                Dim connection As New SQLServer(_connString)
                dataset = connection.executeSelect(sqlQuery)
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count > 0 Then
                        Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), pantallas)
                        For counter = 0 To dataset.Tables(0).Rows.Count - 1
                            Dim actual As Pantallas = resultados(counter)
                            Dim elLink As String = prefijoNivel & actual.Link.Value

                            Dim nombrePantalla As String = CType(GetGlobalResourceObject(actual.ResourceClass.Value, actual.ResourceKey.Value), String)
                            Dim hijo As New TreeNode(nombrePantalla, actual.Codigo_Pantalla.Value, "", elLink, "_self")
                            elTreeNode.ChildNodes.Add(hijo)
                            VerifyAveliableScreens_Nodes(hijo, prefijoNivel)
                            elTreeNode.Expand()
                        Next
                    End If
                End If
            End If
        End Sub

        Function PantallaByCodigo_Pantalla(ByVal codigo_Pantalla As String) As String
            Dim link As String = ""
            Dim pantallas As New Pantallas
            pantallas.Link.ToSelect = True
            Dim dataset As Data.DataSet
            Dim queryBuilder As New QueryBuilder

            pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
            pantallas.activo.Where.EqualCondition(1)
            pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
            Dim consulta As String = queryBuilder.SelectQuery(pantallas)
            Dim connection As New SQLServer(_connString)
            dataset = connection.executeSelect(consulta)
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantallas)
                    link = pantallas.Link.Value
                End If
            End If

            Return link
        End Function

        Function verificarAcceso(ByVal codigo_Pantalla As String) As Boolean
            Dim acceso As Boolean = False
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim pantallas As New Pantallas
            Dim dataset As Data.DataSet
            Dim queryBuilder As New QueryBuilder

            If Me.Perfil >= 0 Then
                If IsAdmin Then
                    pantallas.Codigo_Pantalla.ToSelect = True
                    pantallas.activo.Where.EqualCondition(1)
                    pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
                    Dim consulta As String = queryBuilder.SelectQuery(pantallas)
                    Dim connection As New SQLServer(_connString)
                    dataset = connection.executeSelect(consulta)
                    If dataset.Tables.Count > 0 Then
                        If dataset.Tables(0).Rows.Count > 0 Then
                            acceso = True
                        Else
                            acceso = False
                        End If
                    Else
                        acceso = False
                    End If
                ElseIf id_Perfil > 0 And id_Usuario > 0 Then
                    pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
                    pantallas.activo.Where.EqualCondition(1)

                    pantalla_Perfil.Fields.SelectAll()
                    queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
                    pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)

                    queryBuilder.From.Add(pantallas)
                    queryBuilder.From.Add(pantalla_Perfil)

                    Dim connection As New SQLServer(_connString)
                    dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)
                    If dataset.Tables.Count > 0 Then
                        If dataset.Tables(0).Rows.Count > 0 Then
                            ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantalla_Perfil)

                            If pantalla_Perfil.Leer.Value = "1" Then
                                acceso = True
                            End If

                            If pantalla_Perfil.Crear.Value = "1" Then
                                acceso = True
                            End If
                            If pantalla_Perfil.Modificar.Value = "1" Then
                                acceso = True
                            End If
                            If pantalla_Perfil.Borrar.Value = "1" Then
                                acceso = True
                            End If
                        End If
                    End If
                End If
            End If

            Return acceso
        End Function

        Sub VerifyPantalla(ByVal codigo_Pantalla As String, ByVal level As Integer, Optional ByVal isDefault As Boolean = False)
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim pantallas As New Pantallas
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            Current.Session("NivelPantalla") = level
            Current.Session("pantalla") = codigo_Pantalla
            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            If Me.Perfil >= 0 Then
                If IsAdmin Then
                    pantallas.Codigo_Pantalla.ToSelect = True
                    pantallas.activo.Where.EqualCondition(1)
                    pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
                    Dim consulta As String = queryBuilder.SelectQuery(pantallas)
                    Dim connection As New SQLServer(_connString)
                    dataset = connection.executeSelect(consulta)
                    If dataset.Tables.Count > 0 Then
                        If dataset.Tables(0).Rows.Count > 0 Then
                            _LaPantalla.Leer = True
                            _LaPantalla.Crear = True
                            _LaPantalla.Modificar = True
                            _LaPantalla.Borrar = True
                        Else
                            _LaPantalla.Leer = False
                            _LaPantalla.Crear = False
                            _LaPantalla.Modificar = False
                            _LaPantalla.Borrar = False
                        End If
                    Else
                        _LaPantalla.Leer = False
                        _LaPantalla.Crear = False
                        _LaPantalla.Modificar = False
                        _LaPantalla.Borrar = False
                    End If
                ElseIf id_Perfil > 0 And id_Usuario > 0 Then
                    pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
                    pantallas.activo.Where.EqualCondition(1)

                    pantalla_Perfil.Fields.SelectAll()
                    queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
                    pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)

                    queryBuilder.From.Add(pantallas)
                    queryBuilder.From.Add(pantalla_Perfil)

                    Dim connection As New SQLServer(_connString)
                    dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)
                    If dataset.Tables.Count > 0 Then
                        If dataset.Tables(0).Rows.Count > 0 Then
                            ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantalla_Perfil)

                            If pantalla_Perfil.Leer.Value = "1" Then
                                _LaPantalla.Leer = True
                            End If

                            If pantalla_Perfil.Crear.Value = "1" Then
                                _LaPantalla.Crear = True
                            End If
                            If pantalla_Perfil.Modificar.Value = "1" Then
                                _LaPantalla.Modificar = True
                            End If
                            If pantalla_Perfil.Borrar.Value = "1" Then
                                _LaPantalla.Borrar = True
                            End If
                        End If
                    End If
                End If

                If Not _LaPantalla.Leer Then
                    If isDefault Then
                        Current.Response.Redirect(prefijoNivel & "orbeCatalog/Login.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_Pantalla, True))
                    Else
                        Dim tempHyperlink As New HyperLink
                        VerifyReference(tempHyperlink, level, id_Perfil)
                        If tempHyperlink.Visible And tempHyperlink.NavigateUrl.Length > 0 Then
                            Current.Response.Redirect(tempHyperlink.NavigateUrl)
                        Else
                            Current.Response.Redirect(prefijoNivel & "orbeCatalog/Default.aspx")
                        End If
                    End If
                End If
            Else
                'Aqui se vencio la session
                Current.Response.Redirect(prefijoNivel & "orbeCatalog/Login.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_Pantalla, True))
            End If
        End Sub

        'Sub VerifyPantalla_WithDelegate(ByVal theDelegate As SaveSessionDelegate, ByVal codigo_Pantalla As String, ByVal level As Integer, Optional ByVal isDefault As Boolean = False)
        '    Dim pantalla_Perfil As New Pantalla_Perfil
        '    Dim pantallas As New Pantallas
        '    Dim dataset As Data.DataSet
        '    Dim counter As Integer
        '    Dim prefijoNivel As String = ""

        '    For counter = 0 To level - 1
        '        prefijoNivel &= "../"
        '    Next

        '    If Me.Perfil >= 0 Then
        '        If isadmin Then
        '            _LaPantalla.Leer = True
        '            _LaPantalla.Crear = True
        '            _LaPantalla.Modificar = True
        '            _LaPantalla.Borrar = True
        '        ElseIf id_Perfil > 0 And id_Usuario > 0 Then
        '            pantallas.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
        '            pantallas.activo.Where.EqualCondition(1)

        '            pantalla_Perfil.Fields.SelectAll()
        '            queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
        '            pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)

        '            queryBuilder.From.Add(pantallas)
        '            queryBuilder.From.Add(pantalla_Perfil)

        '            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        '            If dataset.Tables.Count > 0 Then
        '                If dataset.Tables(0).Rows.Count > 0 Then
        '                    ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantalla_Perfil)

        '                    If pantalla_Perfil.Leer.Value = "1" Then
        '                        _LaPantalla.Leer = True
        '                    End If

        '                    If pantalla_Perfil.Crear.Value = "1" Then
        '                        _LaPantalla.Crear = True
        '                    End If
        '                    If pantalla_Perfil.Modificar.Value = "1" Then
        '                        _LaPantalla.Modificar = True
        '                    End If
        '                    If pantalla_Perfil.Borrar.Value = "1" Then
        '                        _LaPantalla.Borrar = True
        '                    End If
        '                End If
        '            End If
        '        End If

        '        If Not _LaPantalla.Leer Then
        '            If isDefault Then
        '                Current.Response.Redirect(prefijoNivel & "orbeCatalog/Login.aspx") '& Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_Pantalla, True))
        '            Else
        '                Dim tempHyperlink As New HyperLink
        '                VerifyReference(tempHyperlink, level, id_Perfil)
        '                If tempHyperlink.Visible And tempHyperlink.NavigateUrl.Length > 0 Then
        '                    Current.Response.Redirect(tempHyperlink.NavigateUrl)
        '                Else
        '                    theDelegate.Invoke()
        '                    Current.Response.Redirect(prefijoNivel & "orbeCatalog/Default.aspx")
        '                End If
        '            End If
        '        End If
        '    Else
        '        Current.Response.Redirect(prefijoNivel & "orbeCatalog/Login.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_Pantalla, True))
        '    End If
        'End Sub

        'Acciones
        Sub VerifyActions(ByRef crear As System.Web.UI.WebControls.WebControl, ByRef modificar As System.Web.UI.WebControls.WebControl, ByRef borrar As System.Web.UI.WebControls.WebControl)
            If Not _LaPantalla.Crear Then
                If crear IsNot Nothing Then
                    crear.Enabled = False
                End If
            End If
            If Not _LaPantalla.Modificar Then
                If modificar IsNot Nothing Then
                    modificar.Enabled = False
                End If
            End If
            If Not _LaPantalla.Borrar Then
                If borrar IsNot Nothing Then
                    borrar.Enabled = False
                End If
            End If
        End Sub

        Sub VerifyActions(ByRef crear As Orbelink.WebControls.ActionButton, ByRef modificar As Orbelink.WebControls.ActionButton, ByRef borrar As Orbelink.WebControls.ActionButton)
            If Not _LaPantalla.Crear Then
                If crear IsNot Nothing Then
                    crear.Disabled = True
                End If
            End If
            If Not _LaPantalla.Modificar Then
                If modificar IsNot Nothing Then
                    modificar.Disabled = True
                End If
            End If
            If Not _LaPantalla.Borrar Then
                If borrar IsNot Nothing Then
                    borrar.Disabled = True
                End If
            End If
        End Sub

        'Referencias
        Public Sub VerifyReference(ByRef theHyperlink As HyperLink, ByVal level As Integer, ByVal id_Perfil As Integer)
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            If Not Current.Request.QueryString("reference") Is Nothing And Me.Perfil >= 0 Then
                pantallas.Fields.SelectAll()
                If id_Perfil = 0 Then
                    pantallas.Codigo_Pantalla.Where.EqualCondition(Current.Request.QueryString("reference"))
                ElseIf id_Perfil > 0 Then
                    queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
                    pantallas.activo.Where.EqualCondition(1)

                    pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                    pantalla_Perfil.codigo_Pantalla.Where.EqualCondition(Current.Request.QueryString("reference"))
                    pantalla_Perfil.Leer.Where.EqualCondition(1)

                    queryBuilder.From.Add(pantalla_Perfil)
                End If

                queryBuilder.Distinct = True
                queryBuilder.From.Add(pantallas)
                Dim connection As New SQLServer(_connString)
                dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

                theHyperlink.Visible = False
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantallas)
                        theHyperlink.Text &= " " & CType(GetGlobalResourceObject(pantallas.ResourceClass.Value, pantallas.ResourceKey.Value), String)
                        theHyperlink.NavigateUrl = prefijoNivel & pantallas.Link.Value
                        theHyperlink.Visible = True

                        'Retoma los query String del reference
                        Dim encontrado As Boolean
                        For counter = 0 To Current.Request.QueryString.Count - 1
                            Dim keytemp As String = Current.Request.QueryString.AllKeys(counter)
                            If encontrado Then
                                If keytemp <> "back" Then
                                    If keytemp.Contains("reference") Then
                                        Dim secuencia As Integer = keytemp.Substring(9)
                                        theHyperlink.NavigateUrl &= "&reference" & (secuencia - 1) & "="
                                    Else
                                        theHyperlink.NavigateUrl &= "&" & keytemp & "="

                                    End If
                                    theHyperlink.NavigateUrl &= Current.Request.QueryString(counter)
                                End If
                            End If
                            If keytemp = "reference" Then
                                encontrado = True
                                theHyperlink.NavigateUrl &= "?back=true"
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

        Sub VerifySearchFor(ByRef theHyperlink As HyperLink, ByVal level As Integer, ByVal id_Perfil As Integer)
            Dim pantallas As New Pantallas
            Dim pantalla_Perfil As New Pantalla_Perfil
            Dim dataset As Data.DataSet
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            theHyperlink.Visible = False
            Dim queryBuilder As New QueryBuilder

            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            If Not Current.Request.QueryString("searchFor") Is Nothing And Me.Perfil >= 0 Then
                pantallas.Fields.SelectAll()
                If id_Perfil = 0 Then
                    pantallas.Codigo_Pantalla.Where.EqualCondition(Current.Request.QueryString("searchFor"))
                ElseIf id_Perfil > 0 Then
                    queryBuilder.Join.EqualCondition(pantallas.Codigo_Pantalla, pantalla_Perfil.codigo_Pantalla)
                    pantallas.activo.Where.EqualCondition(1)

                    pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
                    pantalla_Perfil.codigo_Pantalla.Where.EqualCondition(Current.Request.QueryString("searchFor"))
                    pantalla_Perfil.Leer.Where.EqualCondition(1)

                    queryBuilder.From.Add(pantalla_Perfil)
                End If

                queryBuilder.Distinct = True
                queryBuilder.From.Add(pantallas)
                Dim connection As New SQLServer(_connString)
                dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

                theHyperlink.Visible = False
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataset.Tables(0), 0, pantallas)
                        theHyperlink.Text &= " " & CType(GetGlobalResourceObject(pantallas.ResourceClass.Value, pantallas.ResourceKey.Value), String)
                        theHyperlink.NavigateUrl = prefijoNivel & pantallas.Link.Value
                        theHyperlink.Visible = True
                        theHyperlink.NavigateUrl &= "?back=true"
                    End If
                End If
            End If
        End Sub

        'Utilidades
        Public Shared Function HashPassword(ByVal thePassword As String) As String
            Return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(thePassword, System.Web.Configuration.FormsAuthPasswordFormat.SHA1.ToString())
        End Function

        Public Shared Function GenerateRandomPassword(ByVal largo As Integer) As String
            Dim randomizer As New Random(Date.Now.Millisecond)
            Dim randomN As Double = randomizer.Next(Math.Pow(10, largo))
            Return Math.Round(randomN).ToString()
        End Function
    End Class

End Namespace