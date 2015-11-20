Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Control.Publicaciones

    Public Class ControladoraPublicacionesSearch

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Enum Enum_Aprobada As Integer
            No
            Si
            Todos
        End Enum
        Enum Enum_Principal As Integer
            No
            Si
            Todos
        End Enum
        Enum Enum_Visible As Integer
            No
            Si
            Todos
        End Enum
        Enum Enum_OrderBy As Integer
            Titulo
            Id_Publicacion
            Id_tipoPublicacion
            Id_Entidad
            Visible
            Aprobada
            Corta
            Id_Categoria
            Id_Estado
            Fecha
            FechaInicio
            IncluirRSS
            Link
        End Enum
        Enum Enum_Asc As Integer
            Ascendente
            Descendente
        End Enum
        Enum Enum_Clase
            Entidades
            Estados
            Categorias
            TipoPublicacion
            'Grupos
            Vacio
        End Enum

        ''' <summary>
        ''' Recibe un combo y lo llena con la clase requerida. Si tiene padre se le manda como parametro
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <param name="clase"></param>
        ''' <param name="id_Padre"></param>
        ''' <param name="textoDefault"></param>
        ''' <remarks></remarks>
        Public Sub cargarCombo(ByRef dropdownlist As DropDownList, ByVal clase As Enum_Clase, Optional ByVal id_Padre As Integer = 0, Optional ByVal textoDefault As String = "-- Seleccione --")
            limpiarCombo(dropdownlist, "")
            Dim controladora As New ControladoraPublicaciones(connection.connectionString)
            Select Case clase
                Case Enum_Clase.TipoPublicacion
                    controladora.cargarTipoPublicacion(dropdownlist)
                Case Enum_Clase.Categorias
                    controladora.cargarCategorias(dropdownlist, id_Padre)
                Case Enum_Clase.Estados
                    controladora.cargarEstados(dropdownlist)
                Case Enum_Clase.Entidades
                    controladora.cargarEntidades(dropdownlist)
                    'Case Enum_Clase.Grupos
                    '    cargarGrupos(dropdownlist, id_Padre)
            End Select

            If textoDefault.Length > 0 Then
                dropdownlist.Items.Add(New ListItem(textoDefault, 0))
                dropdownlist.SelectedIndex = dropdownlist.Items.Count - 1
            End If
        End Sub
        ''' <summary>
        ''' Limpia los items de un combo
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <remarks></remarks>
        Public Sub limpiarCombo(ByRef dropdownlist As DropDownList, Optional ByVal textoDefault As String = "-- Seleccione --")
            dropdownlist.Items.Clear()

            If textoDefault.Length > 0 Then
                dropdownlist.Items.Add(New ListItem(textoDefault, 0))
                dropdownlist.SelectedIndex = dropdownlist.Items.Count - 1
            End If
        End Sub

        ''' <summary>
        ''' Select Publicaciones Segun los parametros desde otra pagina
        ''' </summary>
        ''' <param name="parametros"></param>
        ''' <param name="Visible"></param>
        ''' <param name="Aprobada"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectPublicaciones(ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Visible As Enum_Visible, ByVal Aprobada As Enum_Aprobada, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Return selectParametros(False, parametros, Visible, Aprobada, orderby, asc, orderby2, asc2)
        End Function

        ''' <summary>
        ''' Select Publicaciones Con Imagenes Principales Segun los parametros desde otra pagina
        ''' </summary>
        ''' <param name="parametros"></param>
        ''' <param name="Visible"></param>
        ''' <param name="Aprobada"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectPublicacionesConImagenesPrincipales(ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Visible As Enum_Visible, ByVal Aprobada As Enum_Aprobada, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Return selectParametros(True, parametros, Visible, Aprobada, orderby, asc, orderby2, asc2)
        End Function

        Private Function selectParametros(ByVal incluirImagenesPrincipales As Boolean, ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Visible As Enum_Visible, ByVal Aprobada As Enum_Aprobada, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Dim id_tipoPublicacion As Integer
            Dim id_entidad As Integer
            Dim id_categoria As Integer
            Dim id_estado As Integer
            Dim nombre As String = ""
            Dim lista_keywords As String = ""

            If Not parametros.Item("categoria1erNivel") Is Nothing Then
                id_categoria = parametros.Item("tipoPublicacion1erNivel")
            End If
            If Not parametros.Item("categoria2doNivel") Is Nothing Then
                id_categoria = parametros.Item("tipoPublicacion2doNivel")
            End If
            If Not parametros.Item("categoria3erNivel") Is Nothing Then
                id_categoria = parametros.Item("tipoPublicacion3erNivel")
            End If

            If Not parametros.Item("id_entidad") Is Nothing Then
                id_entidad = parametros.Item("id_entidad")
            End If

            If Not parametros.Item("id_tipoPublicacion") Is Nothing Then
                id_tipoPublicacion = parametros.Item("id_tipoPublicacion")
            End If

            If Not parametros.Item("id_estado") Is Nothing Then
                id_estado = parametros.Item("id_estado")
            End If

            If Not parametros.Item("nombre") Is Nothing Then
                nombre = parametros.Item("nombre")
            End If
            If incluirImagenesPrincipales Then
                Return selectPublicacionesConImagenesPrincipales(id_tipoPublicacion, id_entidad, id_categoria, id_estado, nombre, Visible, Aprobada, orderby, asc, orderby2, asc2)
            Else
                Return selectPublicaciones(id_tipoPublicacion, id_entidad, id_categoria, id_estado, nombre, Visible, Aprobada, orderby, asc, orderby2, asc2)
            End If
        End Function

        ''' <summary>
        ''' Devuelve el querystring de la consulta de Publicaciones
        ''' </summary>
        ''' <param name="id_tipoPublicacion"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_categoria"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="Visible"></param>
        ''' <param name="Enum_Aprobada"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectPublicaciones(ByVal id_tipoPublicacion As Integer, ByVal id_entidad As Integer, ByVal id_categoria As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal Visible As Enum_Visible, ByVal Enum_Aprobada As Enum_Aprobada, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder

            Dim Publicacion As New Publicacion
            AddFiltrosPublicacion(Publicacion, id_tipoPublicacion, id_entidad, id_categoria, id_estado, nombre, Visible, Enum_Aprobada)
            Publicacion.Fields.SelectAll()
            queryBuilder.From.Add(Publicacion)
            AddOrderByPublicacion(queryBuilder, orderby, asc)
            If orderby <> orderby2 Then
                AddOrderByPublicacion(queryBuilder, orderby2, asc2)
            End If

            'TipoPublicacion
            Dim TipoPublicacion As New TipoPublicacion
            TipoPublicacion.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_tipoPublicacion, TipoPublicacion.Id_TipoPublicacion)
            queryBuilder.From.Add(TipoPublicacion)

            'Categorias
            Dim Categorias As New Categorias
            Categorias.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_Categoria, Categorias.Id_Categoria)
            queryBuilder.From.Add(Categorias)

            'Estado
            Dim Estado_Publicacion As New Estado_Publicacion
            Estado_Publicacion.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_Estado_Publicacion, Estado_Publicacion.Id_Estado_Publicacion)
            queryBuilder.From.Add(Estado_Publicacion)

            'Entidad
            Dim Entidad As New Entidad
            Entidad.NombreDisplay.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.id_Entidad, Entidad.Id_entidad)
            queryBuilder.From.Add(Entidad)

            Return queryBuilder
        End Function
        ''' <summary>
        ''' Devuelve el querystring de la consulta de Publicaciones
        ''' </summary>
        ''' <param name="id_tipoPublicacion"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_categoria"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="Visible"></param>
        ''' <param name="Enum_Aprobada"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectPublicacionesConImagenesPrincipales(ByVal id_tipoPublicacion As Integer, ByVal id_entidad As Integer, ByVal id_categoria As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal Visible As Enum_Visible, ByVal Enum_Aprobada As Enum_Aprobada, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Titulo, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder

            Dim Publicacion As New Publicacion
            AddFiltrosPublicacion(Publicacion, id_tipoPublicacion, id_entidad, id_categoria, id_estado, nombre, Visible, Enum_Aprobada)
            Publicacion.Fields.SelectAll()
            queryBuilder.From.Add(Publicacion)
            AddOrderByPublicacion(queryBuilder, orderby, asc)
            If orderby <> orderby2 Then
                AddOrderByPublicacion(queryBuilder, orderby2, asc2)
            End If

            'TipoPublicacion
            Dim TipoPublicacion As New TipoPublicacion
            TipoPublicacion.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_tipoPublicacion, TipoPublicacion.Id_TipoPublicacion)
            queryBuilder.From.Add(TipoPublicacion)

            'Categorias
            Dim Categorias As New Categorias
            Categorias.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_Categoria, Categorias.Id_Categoria)
            queryBuilder.From.Add(Categorias)

            'Estado
            Dim Estado_Publicacion As New Estado_Publicacion
            Estado_Publicacion.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.Id_Estado_Publicacion, Estado_Publicacion.Id_Estado_Publicacion)
            queryBuilder.From.Add(Estado_Publicacion)

            'Entidad
            Dim Entidad As New Entidad
            Entidad.NombreDisplay.ToSelect = True
            queryBuilder.Join.EqualCondition(Publicacion.id_Entidad, Entidad.Id_entidad)
            queryBuilder.From.Add(Entidad)

            Return queryBuilder
        End Function
        Private Sub AddFiltrosPublicacion(ByRef Publicacion As Publicacion, ByRef id_tipoPublicacion As Integer, ByRef id_entidad As Integer, ByRef id_categoria As Integer, ByRef id_estado As Integer, ByRef nombre As String, ByRef Visible As Enum_Visible, ByRef Aprobada As Enum_Aprobada)

            'Busca si hay filtro
            If id_tipoPublicacion <> 0 Then
                Publicacion.Id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
            End If

            If id_entidad <> 0 Then
                Publicacion.id_Entidad.Where.EqualCondition(id_entidad)
            End If

            If Visible <> Enum_Visible.Todos Then
                Publicacion.Visible.Where.EqualCondition(Visible)
            End If

            If Aprobada <> Enum_Aprobada.Todos Then
                Publicacion.Aprobada.Where.EqualCondition(Aprobada)
            End If

            If nombre.Length > 0 Then
                queryBuilder.GroupConditions(Publicacion.Titulo.Where.LikeCondition(nombre), Publicacion.Corta.Where.LikeCondition(nombre, Where.FieldRelations.OR_), Where.FieldRelations.OR_)
                'Publicacion.Nombre.Where.LikeCondition(nombre)
                'Publicacion.DescCorta_Publicacion.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
                'Publicacion.DescLarga_Publicacion.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
            End If

            If id_categoria <> 0 Then
                Publicacion.Id_Categoria.Where.EqualCondition(id_categoria)
            End If

            If id_estado <> 0 Then
                Publicacion.Id_Estado_Publicacion.Where.EqualCondition(id_estado)
            End If

        End Sub

        ''' <summary>
        '''  Le agrega al query por referencia un order by por parametro
        ''' </summary>
        ''' <param name="querybuilder"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <remarks></remarks>
        Private Sub AddOrderByPublicacion(ByRef querybuilder As QueryBuilder, ByVal orderby As Enum_OrderBy, ByVal asc As Enum_Asc)
            Dim Publicacion As New Publicacion
            Dim asc2 As Boolean
            If asc = Enum_Asc.Ascendente Then
                asc2 = True
            Else
                asc2 = False
            End If
            Select Case orderby
                Case Enum_OrderBy.Titulo
                    querybuilder.Orderby.Add(Publicacion.Titulo, asc2)
                Case Enum_OrderBy.Visible
                    querybuilder.Orderby.Add(Publicacion.Visible, asc2)
                Case Enum_OrderBy.Corta
                    querybuilder.Orderby.Add(Publicacion.Corta, asc2)
                Case Enum_OrderBy.Aprobada
                    querybuilder.Orderby.Add(Publicacion.Aprobada, asc2)
                Case Enum_OrderBy.Id_Publicacion
                    querybuilder.Orderby.Add(Publicacion.Id_Publicacion, asc2)
                Case Enum_OrderBy.Id_Entidad
                    querybuilder.Orderby.Add(Publicacion.id_Entidad, asc2)
                Case Enum_OrderBy.Id_Estado
                    querybuilder.Orderby.Add(Publicacion.Id_Estado_Publicacion, asc2)
                Case Enum_OrderBy.Id_Categoria
                    querybuilder.Orderby.Add(Publicacion.Id_Categoria, asc)
                Case Enum_OrderBy.Id_tipoPublicacion
                    querybuilder.Orderby.Add(Publicacion.Id_tipoPublicacion, asc2)
                Case Enum_OrderBy.Fecha
                    querybuilder.Orderby.Add(Publicacion.Fecha, asc2)
                Case Enum_OrderBy.FechaInicio
                    querybuilder.Orderby.Add(Publicacion.FechaInicio, asc2)
                Case Enum_OrderBy.IncluirRSS
                    querybuilder.Orderby.Add(Publicacion.IncluirRSS, asc2)
                Case Enum_OrderBy.Link
                    querybuilder.Orderby.Add(Publicacion.Link, asc2)

            End Select
        End Sub

        ''' <summary>
        ''' Devuelve el querystring de los archivos de una publicacion
        ''' </summary>
        ''' <param name="id_publicacion"></param>
        ''' <param name="principal"></param>
        ''' <param name="filetype"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectArchivosPublicacion(ByVal id_publicacion As Integer, Optional ByVal principal As Enum_Principal = Enum_Principal.Todos, Optional ByVal filetype As Orbelink.Control.Archivos.ArchivoHandler.ArchivoType = -1) As QueryBuilder
            Dim archivo As New Archivo
            Dim archivo_Publicacion As New Archivo_Publicacion
            archivo_Publicacion.id_Publicacion.ToSelect = True
            archivo_Publicacion.id_Publicacion.Where.EqualCondition(id_publicacion)
            archivo.Fields.SelectAll()
            If principal <> Enum_Principal.Todos Then
                If principal = Enum_Principal.Si Then
                    archivo_Publicacion.Principal.Where.EqualCondition(1)
                Else
                    archivo_Publicacion.Principal.Where.EqualCondition(0)
                End If

            End If

            If filetype <> -1 Then
                archivo.FileType.Where.EqualCondition(filetype)
            End If

            queryBuilder.Join.EqualCondition(archivo.Id_Archivo, archivo_Publicacion.id_Archivo)

            queryBuilder.From.Add(archivo)
            queryBuilder.From.Add(archivo_Publicacion)

            Return queryBuilder
        End Function

        ''' <summary>
        ''' Devuelve los parametros de busqueda para contanenarlos al aspx de destino
        ''' </summary>
        ''' <param name="categoria1erNivel"></param>
        ''' <param name="categoria2doNivel"></param>
        ''' <param name="categoria3erNivel"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_TipoPublicacion"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="Visible"></param>
        ''' <param name="Enum_Aprobada"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ArmarQueryStringPublicaciones(ByVal categoria1erNivel As Integer, ByVal categoria2doNivel As Integer, ByVal categoria3erNivel As Integer, ByVal id_entidad As Integer, ByVal id_TipoPublicacion As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal Visible As Enum_Visible, ByVal Enum_Aprobada As Enum_Aprobada) As String
            'Dim queryString As String = "Publicaciones.aspx"
            Dim queryString As String = ""
            If categoria1erNivel <> 0 Then
                queryString &= "?categoria1erNivel=" & categoria1erNivel
            End If
            If categoria2doNivel <> 0 Then
                queryString &= "&categoria2doNivel=" & categoria2doNivel
            End If
            If categoria3erNivel <> 0 Then
                queryString &= "&categoria3erNivel=" & categoria3erNivel
            End If
            If id_entidad <> 0 Then
                queryString &= "&id_entidad=" & id_entidad
            End If
            If id_TipoPublicacion <> 0 Then
                queryString &= "&id_tipoPublicacion=" & id_TipoPublicacion
            End If
            If id_estado Then
                queryString &= "&id_estado=" & id_estado
            End If

            If nombre.Length > 0 Then
                queryString &= "&nombre=" & nombre
            End If

            If Visible <> Enum_Visible.Si Then
                queryString &= "&Visible=" & Visible
            End If
            If Enum_Aprobada <> Enum_Aprobada.Todos Then
                queryString &= "&Enum_Aprobada=" & Enum_Aprobada
            End If

            If queryString.StartsWith("&") Then
                queryString = queryString.Remove(0, 1)
                queryString = queryString.Insert(0, "?")
            End If
            Return queryString

        End Function
        ''' <summary>
        ''' Carga los parametros de busqueda en los filtros
        ''' </summary>
        ''' <param name="parametros"></param>
        ''' <param name="ddl_Categoria1erNivel"></param>
        ''' <param name="ddl_Categoria2doNivel"></param>
        ''' <param name="ddl_Categoria3erNivel"></param>
        ''' <param name="ddl_Entidad"></param>
        ''' <param name="ddl_TipoPublicacion"></param>
        ''' <param name="ddl_Estado"></param>
        ''' <param name="tbx_nombre"></param>
        ''' <remarks></remarks>
        Public Sub cargarParametrosEnFiltros(ByVal parametros As System.Collections.Specialized.NameValueCollection, Optional ByRef ddl_Categoria1erNivel As DropDownList = Nothing, Optional ByRef ddl_Categoria2doNivel As DropDownList = Nothing, Optional ByRef ddl_Categoria3erNivel As DropDownList = Nothing, Optional ByRef ddl_Entidad As DropDownList = Nothing, Optional ByRef ddl_TipoPublicacion As DropDownList = Nothing, Optional ByRef ddl_Estado As DropDownList = Nothing, Optional ByRef tbx_nombre As TextBox = Nothing)

            If Not parametros.Item("categoria1erNivel") Is Nothing And ddl_Categoria1erNivel IsNot Nothing Then
                ddl_Categoria1erNivel.SelectedValue = parametros.Item("categoria1erNivel")
                If ddl_Categoria2doNivel IsNot Nothing Then
                    cargarCombo(ddl_Categoria2doNivel, Enum_Clase.Categorias, ddl_Categoria1erNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("categoria2doNivel") Is Nothing And ddl_Categoria2doNivel IsNot Nothing Then
                ddl_Categoria2doNivel.SelectedValue = parametros.Item("categoria2doNivel")
                If ddl_Categoria3erNivel IsNot Nothing Then
                    cargarCombo(ddl_Categoria3erNivel, Enum_Clase.Categorias, ddl_Categoria2doNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("categoria3erNivel") Is Nothing And ddl_Categoria3erNivel IsNot Nothing Then
                ddl_Categoria3erNivel.SelectedValue = parametros.Item("categoria3erNivel")
            End If

            If Not parametros.Item("id_entidad") Is Nothing And ddl_Entidad IsNot Nothing Then
                ddl_Entidad.SelectedValue = parametros.Item("id_entidad")
            End If

            If Not parametros.Item("id_tipoPublicacion") Is Nothing And ddl_TipoPublicacion IsNot Nothing Then
                ddl_TipoPublicacion.SelectedValue = parametros.Item("id_tipoPublicacion")
            End If

            If Not parametros.Item("id_estado") Is Nothing And ddl_Estado IsNot Nothing Then
                ddl_Estado.SelectedValue = parametros.Item("id_estado")
            End If

            If Not parametros.Item("nombre") Is Nothing And tbx_nombre IsNot Nothing Then
                tbx_nombre.Text = parametros.Item("nombre")
            End If

            'If Not parametros.Item("Visible") Is Nothing Then
            'End If

            'If Not parametros.Item("Enum_Aprobada") Is Nothing Then
            'End If

        End Sub

    End Class

End Namespace