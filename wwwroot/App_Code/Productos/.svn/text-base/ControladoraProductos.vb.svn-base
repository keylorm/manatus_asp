Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Service
Imports Orbelink.Control.Archivos
Imports System.Data
Imports System.Data.SqlClient
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Namespace Orbelink.Control.Productos

    Public Class ControladoraProductos

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        'Productos
        Public Function ConsultarProducto(ByVal id_Producto As Integer) As Producto
            Dim dataSet As Data.DataSet
            Dim producto As New Producto

            producto.Fields.SelectAll()
            producto.Id_Producto.Where.EqualCondition(id_Producto)
            queryBuilder.From.Add(producto)

            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, producto)
                    Return producto
                End If
            End If
            Return Nothing
        End Function

        Enum Enum_enPrincipal As Integer
            No
            Si
            Todos
        End Enum

        Enum Enum_Principal As Integer
            No
            Si
            Todos
        End Enum

        Enum Enum_Activo As Integer
            No
            Si
            Todos
        End Enum

        Enum Enum_Precio As Integer
            minimo
            maximo
        End Enum

        Enum Enum_OrderBy As Integer
            Nombre
            Id_Producto
            Id_tipoProdcto
            Id_Entidad
            Activo
            enPrincipal
            Desc_Corta
            Id_Origen
            Id_Estado
            Id_Unidad
            PrecioUnitario
            SKU
            Fecha
        End Enum

        Enum Enum_Asc As Integer
            Ascendente
            Descendente
        End Enum


        Enum Enum_clase
            Entidades
            Estados
            Keywords
            Origenes
            TipoProducto
            Unidades
            Grupos
            Vacio
        End Enum

        Enum Enum_clasesCheckList
            Keywords
        End Enum
        ''' <summary>
        ''' Recibe un combo y lo llena con la clase requerida. Si tiene padre se le manda como parametro
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <param name="clase"></param>
        ''' <param name="id_Padre"></param>
        ''' <param name="textoDefault"></param>
        ''' <remarks></remarks>
        Public Sub cargarCombo(ByRef dropdownlist As DropDownList, ByVal clase As Enum_clase, Optional ByVal id_Padre As Integer = 0, Optional ByVal textoDefault As String = "-- Seleccione --")
            limpiarCombo(dropdownlist, "")
            Select Case clase
                Case Enum_clase.TipoProducto
                    cargarTipoProductos(dropdownlist, id_Padre)
                Case Enum_clase.Origenes
                    cargarOrigenes(dropdownlist, id_Padre)
                Case Enum_clase.Estados
                    cargarEstados(dropdownlist)
                Case Enum_clase.Entidades
                    cargarEntidades(dropdownlist)
                Case Enum_clase.Keywords
                    cargarKeywords(dropdownlist)
                Case Enum_clase.Grupos
                    cargarGrupos(dropdownlist, id_Padre)
                Case Enum_clase.Unidades
                    cargarUnidades(dropdownlist)
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
        ''' Recibe un checkboxlist y lo llena con la clase requerida. Si tiene padre se le manda como parametro
        ''' </summary>
        ''' <param name="chklist"></param>
        ''' <param name="clase"></param>
        ''' <remarks></remarks>
        Public Sub cargarCheckBoxList(ByRef chklist As CheckBoxList, ByVal clase As Enum_clasesCheckList, Optional ByVal lista_Ids As String = "")
            Select Case clase
                Case Enum_clasesCheckList.Keywords
                    cargarKeywords(chklist, lista_Ids)
            End Select
        End Sub
        Public Sub cargarTexboxKeywordsSegunCheckBoxListKeywords(ByVal tbx_keywords As TextBox, ByVal chklst_keywords As CheckBoxList)
            tbx_keywords.Text = ""
            If tbx_keywords.Attributes("lista_keywords") IsNot Nothing Then
                tbx_keywords.Attributes("lista_keywords") = ""
            Else
                tbx_keywords.Attributes.Add("lista_keywords", "")
            End If

            Dim chekeados As Integer = 0
            For i As Integer = 0 To chklst_keywords.Items.Count - 1
                If chklst_keywords.Items(i).Selected = True Then
                    If chekeados = 0 Then
                        tbx_keywords.Text = chklst_keywords.Items(i).Text
                        tbx_keywords.Attributes("lista_keywords") = chklst_keywords.Items(i).Value
                    Else
                        tbx_keywords.Text += ", " & chklst_keywords.Items(i).Text
                        tbx_keywords.Attributes("lista_keywords") += "," & chklst_keywords.Items(i).Value
                    End If
                    chekeados += 1
                End If
            Next
        End Sub
        Private Sub cargarEntidades(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Entidad As New Entity.Entidades.Entidad
            Entidad.NombreDisplay.ToSelect = True
            Entidad.Id_entidad.ToSelect = True

            Dim consulta As String = queryBuilder.SelectQuery(Entidad)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Entidad.NombreDisplay.Name
                    dropdownlist.DataValueField = Entidad.Id_entidad.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Private Sub cargarEstados(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Estados_Productos As New Estados_Productos
            Estados_Productos.Nombre.ToSelect = True
            Estados_Productos.Id_Estado_Producto.ToSelect = True

            Dim consulta As String = queryBuilder.SelectQuery(Estados_Productos)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Estados_Productos.Nombre.Name
                    dropdownlist.DataValueField = Estados_Productos.Id_Estado_Producto.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Private Sub cargarGrupos(ByRef dropdownlist As DropDownList, Optional ByVal id_Padre As Integer = 0)
            Dim dataSet As New Data.DataSet
            Dim Grupos_Productos As New Grupos_Productos
            Grupos_Productos.Nombre.ToSelect = True
            Grupos_Productos.Id_Grupo.ToSelect = True

            If id_Padre = 0 Then
                Grupos_Productos.Id_padre.Where.EqualCondition_OnSameTable(Grupos_Productos.Id_Grupo)
            Else
                Grupos_Productos.Id_padre.Where.EqualCondition(id_Padre)
                Grupos_Productos.Id_Grupo.Where.DiferentCondition(id_Padre)
            End If

            Dim consulta As String = queryBuilder.SelectQuery(Grupos_Productos)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Grupos_Productos.Nombre.Name
                    dropdownlist.DataValueField = Grupos_Productos.Id_Grupo.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Private Sub cargarKeywords(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Keywords As New Keywords
            Keywords.Nombre.ToSelect = True
            Keywords.Id_Keyword.ToSelect = True

            Dim consulta As String = queryBuilder.SelectQuery(Keywords)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Keywords.Nombre.Name
                    dropdownlist.DataValueField = Keywords.Id_Keyword.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Private Sub cargarKeywords(ByRef chklist As CheckBoxList, ByVal lista_keywords As String)
            Dim dataset As New Data.DataSet
            Dim keywords As New Keywords
            keywords.Id_Keyword.ToSelect = True
            keywords.Nombre.ToSelect = True
            queryBuilder.From.Add(keywords)
            Dim result As String = queryBuilder.RelationalSelectQuery()
            dataset = connection.executeSelect(result)

            If dataset.Tables(0).Rows.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    chklist.DataSource = dataset
                    chklist.DataTextField = keywords.Nombre.Name
                    chklist.DataValueField = keywords.Id_Keyword.Name
                    chklist.DataBind()
                    If lista_keywords.Length > 0 Then
                        Dim arreglo As String() = lista_keywords.Split(",")
                        For countChklist As Integer = 0 To chklist.Items.Count - 1
                            For countArray As Integer = 0 To arreglo.Length - 1
                                If chklist.Items(countChklist).Value = arreglo(countArray) Then
                                    chklist.Items(countChklist).Selected = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If
            End If

        End Sub
        Private Sub cargarOrigenes(ByRef dropdownlist As DropDownList, Optional ByVal id_Padre As Integer = 0)
            Dim dataSet As New Data.DataSet
            Dim Origenes As New Origenes
            Origenes.Nombre.ToSelect = True
            Origenes.Id_Origen.ToSelect = True
            If id_Padre = 0 Then
                Origenes.Id_padre.Where.EqualCondition_OnSameTable(Origenes.Id_Origen)
            Else
                Origenes.Id_padre.Where.EqualCondition(id_Padre)
                Origenes.Id_Origen.Where.DiferentCondition(id_Padre)
            End If
            Dim consulta As String = queryBuilder.SelectQuery(Origenes)
            dataSet = connection.executeSelect(consulta)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Origenes.Nombre.Name
                    dropdownlist.DataValueField = Origenes.Id_Origen.Name
                    dropdownlist.DataBind()
                End If
            End If
        End Sub
        Private Sub cargarTipoProductos(ByRef dropdownlist As DropDownList, Optional ByVal id_Padre As Integer = 0)
            Dim dataSet As New Data.DataSet
            Dim TipoProducto As New TipoProducto
            TipoProducto.Nombre.ToSelect = True
            TipoProducto.Id_TipoProducto.ToSelect = True
            If id_Padre = 0 Then
                TipoProducto.Id_padre.Where.EqualCondition_OnSameTable(TipoProducto.Id_TipoProducto)
            Else
                TipoProducto.Id_padre.Where.EqualCondition(id_Padre)
                TipoProducto.Id_TipoProducto.Where.DiferentCondition(id_Padre)
            End If
            Dim consulta As String = queryBuilder.SelectQuery(TipoProducto)
            dataSet = connection.executeSelect(consulta)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = TipoProducto.Nombre.Name
                    dropdownlist.DataValueField = TipoProducto.Id_TipoProducto.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Private Sub cargarUnidades(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Unidades As New Moneda
            Unidades.Nombre.ToSelect = True
            Unidades.Id_Moneda.ToSelect = True

            Dim consulta As String = queryBuilder.SelectQuery(Unidades)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Unidades.Nombre.Name
                    dropdownlist.DataValueField = Unidades.Id_Moneda.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub


        ''' <summary>
        ''' Select Productos Segun los parametros desde otra pagina
        ''' </summary>
        ''' <param name="parametros"></param>
        ''' <param name="Activo"></param>
        ''' <param name="enPrincipal"></param>
        ''' <param name="incluirImagenesPrincipales"></param>
        ''' <param name="orderby"></param>
        ''' <param name="Asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectProductos(ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, ByVal incluirImagenesPrincipales As Boolean, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal Asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Return selectParametros(False, parametros, Activo, enPrincipal, orderby, Asc, orderby2, asc2)
        End Function
        ''' <summary>
        ''' Select Productos Con Imagenes Principales Segun los parametros desde otra pagina
        ''' </summary>
        ''' <param name="parametros"></param>
        ''' <param name="Activo"></param>
        ''' <param name="enPrincipal"></param>
        ''' <param name="orderby"></param>
        ''' <param name="Asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectProductosConImagenesPrincipales(ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal Asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Return selectParametros(True, parametros, Activo, enPrincipal, orderby, Asc, orderby2, asc2)
        End Function
        Private Function selectParametros(ByVal incluirImagenesPrincipales As Boolean, ByVal parametros As System.Collections.Specialized.NameValueCollection, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal Asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Dim id_tipoProducto As Integer
            Dim id_entidad As Integer
            Dim id_origen As Integer
            Dim id_estado As Integer
            Dim nombre As String = ""
            Dim rangoPrecios As String = ""
            Dim SKU As String = ""
            Dim lista_keywords As String = ""

            If Not parametros.Item("tipoProducto1erNivel") Is Nothing Then
                id_tipoProducto = parametros.Item("tipoProducto1erNivel")
            End If
            If Not parametros.Item("tipoProducto2doNivel") Is Nothing Then
                id_tipoProducto = parametros.Item("tipoProducto2doNivel")
            End If
            If Not parametros.Item("tipoProducto3erNivel") Is Nothing Then
                id_tipoProducto = parametros.Item("tipoProducto3erNivel")
            End If

            If Not parametros.Item("id_entidad") Is Nothing Then
                id_entidad = parametros.Item("id_entidad")
            End If

            If Not parametros.Item("id_origen1erNivel") Is Nothing Then
                id_origen = parametros.Item("id_origen1erNivel")
            End If

            If Not parametros.Item("id_origen2doNivel") Is Nothing Then
                id_origen = parametros.Item("id_origen2doNivel")
            End If

            If Not parametros.Item("id_origen3erNivel") Is Nothing Then
                id_origen = parametros.Item("id_origen3erNivel")
            End If

            If Not parametros.Item("id_estado") Is Nothing Then
                id_estado = parametros.Item("id_estado")
            End If

            If Not parametros.Item("nombre") Is Nothing Then
                nombre = parametros.Item("nombre")
            End If

            If Not parametros.Item("rangoPrecios") Is Nothing Then
                rangoPrecios = parametros.Item("rangoPrecios")
            End If

            If Not parametros.Item("SKU") Is Nothing Then
                SKU = parametros.Item("SKU")
            End If

            If Not parametros.Item("lista_keywords") Is Nothing Then
                lista_keywords = parametros.Item("lista_keywords")
            End If

            If incluirImagenesPrincipales Then
                Return selectProductosConImagenesPrincipales(id_tipoProducto, id_entidad, id_origen, id_estado, nombre, rangoPrecios, SKU, Activo, enPrincipal, lista_keywords, orderby, Asc, orderby2, asc2)
            Else
                Return selectProductos(id_tipoProducto, id_entidad, id_origen, id_estado, nombre, rangoPrecios, SKU, Activo, enPrincipal, lista_keywords, orderby, Asc, orderby2, asc2)
            End If
        End Function

        ''' <summary>
        ''' Devuelve el querystring de la consulta de Productos
        ''' </summary>
        ''' <param name="id_tipoProducto"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_origen"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="rangoPrecios"></param>
        ''' <param name="SKU"></param>
        ''' <param name="Activo"></param>
        ''' <param name="enPrincipal"></param>
        ''' <param name="lista_keywords"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectProductos(ByVal id_tipoProducto As Integer, ByVal id_entidad As Integer, ByVal id_origen As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal rangoPrecios As String, ByVal SKU As String, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, Optional ByVal lista_keywords As String = "", Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder

            Dim dataset As New Data.DataSet
            Dim producto As New Producto

            'Consulta con SelectAll
            AddFiltrosProducto(producto, id_tipoProducto, id_entidad, id_origen, id_estado, nombre, rangoPrecios, SKU, Activo, enPrincipal, lista_keywords)
            producto.Fields.SelectAll()
            queryBuilder.From.Add(producto)

            AddOrderByProducto(queryBuilder, orderby, asc)
            If orderby <> orderby2 Then
                AddOrderByProducto(queryBuilder, orderby2, asc2)
            End If
            'If incluirAtributos Then
            '    If listaAtributos IsNot Nothing And listaAtributos.Length > 0 Then
            '        AgregarAtributosProductos(dataset, result, listaAtributos)
            '        'Else
            '        '    AgregarAtributosProductos(dataset, querystring, True)
            '    End If
            'End If

            'TipoProducto
            Dim TipoProducto As New TipoProducto
            TipoProducto.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.Id_tipoProducto, TipoProducto.Id_TipoProducto)
            queryBuilder.From.Add(TipoProducto)

            'Origenes
            Dim origen As New Origenes
            origen.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Origen, origen.Id_Origen)
            queryBuilder.From.Add(origen)

            'Estado
            Dim Estados_Productos As New Estados_Productos
            Estados_Productos.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Estado, Estados_Productos.Id_Estado_Producto)
            queryBuilder.From.Add(Estados_Productos)

            'Unidades
            Dim Unidades As New Moneda
            Unidades.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.Id_Moneda, Unidades.Id_Moneda)
            queryBuilder.From.Add(Unidades)

            'Entidad
            Dim Entidad As New Entity.Entidades.Entidad
            Entidad.NombreDisplay.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Entidad, Entidad.Id_entidad)
            queryBuilder.From.Add(Entidad)

            Return queryBuilder
        End Function
        ''' <summary>
        ''' Devuelve el querystring de la consulta de Productos Con Imagenes Principales
        ''' </summary>
        ''' <param name="id_tipoProducto"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_origen"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="rangoPrecios"></param>
        ''' <param name="SKU"></param>
        ''' <param name="Activo"></param>
        ''' <param name="enPrincipal"></param>
        ''' <param name="lista_keywords"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <param name="orderby2"></param>
        ''' <param name="asc2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectProductosConImagenesPrincipales(ByVal id_tipoProducto As Integer, ByVal id_entidad As Integer, ByVal id_origen As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal rangoPrecios As String, ByVal SKU As String, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, Optional ByVal lista_keywords As String = "", Optional ByVal orderby As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc As Enum_Asc = Enum_Asc.Ascendente, Optional ByVal orderby2 As Enum_OrderBy = Enum_OrderBy.Nombre, Optional ByVal asc2 As Enum_Asc = Enum_Asc.Ascendente) As QueryBuilder
            Dim dataset As New Data.DataSet
            Dim producto As New Producto

            AddFiltrosProducto(producto, id_tipoProducto, id_entidad, id_origen, id_estado, nombre, rangoPrecios, SKU, Activo, enPrincipal, lista_keywords)
            producto.Fields.SelectAll()
            queryBuilder.From.Add(producto)

            AddOrderByProducto(queryBuilder, orderby, asc)
            If orderby <> orderby2 Then
                AddOrderByProducto(queryBuilder, orderby2, asc2)
            End If

            'Archivos
            Dim Archivo_Producto As New Archivo_Producto
            Dim archivos As New Archivo
            archivos.FileName.ToSelect = True
            archivos.Extension.ToSelect = True
            queryBuilder.Join.EqualCondition(Archivo_Producto.id_Producto, producto.Id_Producto, Join.JoinTypes.RIGHT_OUTER_JOIN)
            Archivo_Producto.Principal.Where.EqualCondition(1)
            Archivo_Producto.Principal.Where.IsNullCondition(Where.FieldRelations.OR_)
            queryBuilder.Join.EqualCondition(Archivo_Producto.id_Archivo, archivos.Id_Archivo, Join.JoinTypes.LEFT_OUTER_JOIN)
            queryBuilder.From.Add(Archivo_Producto)
            queryBuilder.From.Add(archivos)

            'TipoProducto
            Dim TipoProducto As New TipoProducto
            TipoProducto.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.Id_tipoProducto, TipoProducto.Id_TipoProducto)
            queryBuilder.From.Add(TipoProducto)

            'Origenes
            Dim origen As New Origenes
            origen.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Origen, origen.Id_Origen)
            queryBuilder.From.Add(origen)

            'Estado
            Dim Estados_Productos As New Estados_Productos
            Estados_Productos.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Estado, Estados_Productos.Id_Estado_Producto)
            queryBuilder.From.Add(Estados_Productos)

            'Unidades
            Dim Unidades As New Moneda
            Unidades.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.Id_Moneda, Unidades.Id_Moneda)
            queryBuilder.From.Add(Unidades)

            'Entidad
            Dim Entidad As New Entity.Entidades.Entidad
            Entidad.NombreDisplay.ToSelect = True
            queryBuilder.Join.EqualCondition(producto.id_Entidad, Entidad.Id_entidad)
            queryBuilder.From.Add(Entidad)

            Return queryBuilder
        End Function
        Private Sub AddFiltrosProducto(ByRef Producto As Producto, ByRef id_tipoProducto As Integer, ByRef id_entidad As Integer, ByRef id_origen As Integer, ByRef id_estado As Integer, ByRef nombre As String, ByRef rangoPrecios As String, ByRef SKU As String, ByRef Activo As Enum_Activo, ByRef enPrincipal As Enum_enPrincipal, ByRef lista_keywords As String)

            'Busca si hay filtro
            If id_tipoProducto <> 0 Then
                Producto.Id_tipoProducto.Where.EqualCondition(id_tipoProducto)
            End If

            If id_entidad <> 0 Then
                Producto.id_Entidad.Where.EqualCondition(id_entidad)
            End If

            If Activo <> Enum_Activo.Todos Then
                Producto.Activo.Where.EqualCondition(Activo)
            End If

            If enPrincipal <> Enum_enPrincipal.Todos Then
                Producto.enPrinc.Where.EqualCondition(enPrincipal)
            End If

            If nombre.Length > 0 Then
                queryBuilder.GroupConditions(Producto.Nombre.Where.LikeCondition(nombre), Producto.DescCorta_Producto.Where.LikeCondition(nombre, Where.FieldRelations.OR_), Where.FieldRelations.OR_)
                'Producto.Nombre.Where.LikeCondition(nombre)
                'Producto.DescCorta_Producto.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
                'Producto.DescLarga_Producto.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
            End If

            If id_origen <> 0 Then
                Producto.id_Origen.Where.EqualCondition(id_origen)
            End If

            If id_estado <> 0 Then
                Producto.id_Estado.Where.EqualCondition(id_estado)
            End If

            If rangoPrecios.Length > 0 And rangoPrecios <> "0" Then
                Producto.Precio_Unitario.Where.GreaterThanOrEqualCondition(getPrecio(rangoPrecios, Enum_Precio.minimo))
                Producto.Precio_Unitario.Where.LessThanOrEqualCondition(getPrecio(rangoPrecios, Enum_Precio.maximo))
            End If

            If SKU.Length > 0 Then
                Producto.SKU.Where.EqualCondition(SKU)
            End If

            If lista_keywords <> "" Then
                Dim keywords_Producto As New Keywords_Producto
                keywords_Producto.Id_Keyword.Where.InCondition(lista_keywords)
                queryBuilder.Join.EqualCondition(Producto.Id_Producto, keywords_Producto.Id_Producto)
                queryBuilder.From.Add(keywords_Producto)
            End If

        End Sub
        ''' <summary>
        ''' Le agrega al query por referencia un order by por parametro
        ''' </summary>
        ''' <param name="querybuilder"></param>
        ''' <param name="orderby"></param>
        ''' <param name="asc"></param>
        ''' <remarks></remarks>
        ''' 
        Private Sub AddOrderByProducto(ByRef querybuilder As QueryBuilder, ByVal orderby As Enum_OrderBy, ByVal asc As Enum_Asc)
            Dim producto As New Producto
            Dim asc2 As Boolean
            If asc = Enum_Asc.Ascendente Then
                asc2 = True
            Else
                asc2 = False
            End If
            Select Case orderby
                Case Enum_OrderBy.Nombre
                    querybuilder.Orderby.Add(producto.Nombre, asc2)
                Case Enum_OrderBy.Activo
                    querybuilder.Orderby.Add(producto.Activo, asc2)
                Case Enum_OrderBy.Desc_Corta
                    querybuilder.Orderby.Add(producto.DescCorta_Producto, asc2)
                Case Enum_OrderBy.enPrincipal
                    querybuilder.Orderby.Add(producto.enPrinc, asc2)
                Case Enum_OrderBy.Id_Producto
                    querybuilder.Orderby.Add(producto.Id_Producto, asc2)
                Case Enum_OrderBy.Id_Entidad
                    querybuilder.Orderby.Add(producto.id_Entidad, asc2)
                Case Enum_OrderBy.Id_Estado
                    querybuilder.Orderby.Add(producto.id_Estado, asc2)
                Case Enum_OrderBy.Id_Origen
                    querybuilder.Orderby.Add(producto.id_Origen, asc)
                Case Enum_OrderBy.Id_tipoProdcto
                    querybuilder.Orderby.Add(producto.Id_tipoProducto, asc2)
                Case Enum_OrderBy.Id_Unidad
                    querybuilder.Orderby.Add(producto.Id_Moneda)
                Case Enum_OrderBy.PrecioUnitario
                    querybuilder.Orderby.Add(producto.Precio_Unitario, asc2)
                Case Enum_OrderBy.Fecha
                    querybuilder.Orderby.Add(producto.Fecha, asc2)
            End Select
        End Sub


        ''' <summary>
        ''' Devuelve el querystring de los archivos de un Producto
        ''' </summary>
        ''' <param name="id_Producto"></param>
        ''' <param name="principal"></param>
        ''' <param name="filetype"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectArchivosProducto(ByVal id_Producto As Integer, Optional ByVal principal As Enum_Principal = Enum_Principal.Todos, Optional ByVal filetype As ArchivoHandler.ArchivoType = -1) As QueryBuilder

            Dim archivo As New Archivo
            Dim archivo_Producto As New Archivo_Producto
            archivo_Producto.id_Producto.ToSelect = True
            archivo_Producto.id_Producto.Where.EqualCondition(id_Producto)
            archivo.Fields.SelectAll()
            If principal <> Enum_Principal.Todos Then
                If principal = Enum_Principal.Si Then
                    archivo_Producto.Principal.Where.EqualCondition(1)
                Else
                    archivo_Producto.Principal.Where.EqualCondition(0)
                End If
            End If

            If filetype <> -1 Then
                archivo.FileType.Where.EqualCondition(filetype)
            End If

            queryBuilder.Join.EqualCondition(archivo.Id_Archivo, archivo_Producto.id_Archivo)

            queryBuilder.From.Add(archivo)
            queryBuilder.From.Add(archivo_Producto)

            Return queryBuilder
        End Function
        ''' <summary>
        ''' Agrega todos los atributos que esten relacionados con algun producto
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <param name="querystringProductos"></param>
        ''' <remarks></remarks>
        Public Sub AgregarAtributosProductos(ByRef dataset As Data.DataSet, ByVal querystringProductos As String)
            Dim producto As New Producto
            querystringProductos = querystringProductos.Remove(0, querystringProductos.IndexOf("FROM"))
            querystringProductos = querystringProductos.Insert(0, "Select  " & producto.TableName & "." & producto.Id_Producto.Name & " ")
            querystringProductos = querystringProductos.Remove(querystringProductos.LastIndexOf("ORDER"))
            Dim Atributos_Producto As New Atributos_Producto
            Dim Atributos_Para_Productos As New Atributos_Para_Productos
            Dim listaAtrubutos As Data.DataTable = selectAtributosParaProductos()
            If listaAtrubutos IsNot Nothing Then
                If listaAtrubutos.Rows.Count > 0 Then
                    For countAtributos As Integer = 0 To listaAtrubutos.Rows.Count - 1
                        ObjectBuilder.CreateObject(listaAtrubutos, countAtributos, Atributos_Para_Productos)
                        Dim datasetAtributos As Data.DataTable = selectAtributosProductos(querystringProductos, Atributos_Para_Productos.Id_atributo.Value)
                        If datasetAtributos IsNot Nothing Then
                            If datasetAtributos.Rows.Count > 0 Then
                                Dim columName As String = Atributos_Producto.Id_atributo.Name & Atributos_Para_Productos.Id_atributo.Value
                                Dim X As New Data.DataColumn(columName, Type.GetType("System.String"))
                                dataset.Tables(0).Columns.Add(X)
                                For countAtributosProductos As Integer = 0 To datasetAtributos.Rows.Count - 1
                                    For countProductos As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                                        If datasetAtributos.Rows(countAtributosProductos).Item(Atributos_Producto.Id_Producto.Name) = dataset.Tables(0).Rows(countProductos).Item(producto.Id_Producto.Name) Then
                                            dataset.Tables(0).Rows(countProductos).Item(columName) = datasetAtributos.Rows(countAtributosProductos).Item(Atributos_Producto.Valor.Name)
                                            Exit For
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    Next
                End If
            End If

        End Sub
        ''' <summary>
        ''' Agrega un atributos especifico como una columna mas y se le puede cambiar ese nombre cambiando el parametro Nombre_Atributo
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <param name="querystringProductos"></param>
        ''' <param name="idAtributo"></param>
        ''' <param name="Nombre_Atributo"></param>
        ''' <remarks></remarks>
        Public Sub AgregarAtributosProductos(ByRef dataset As Data.DataSet, ByVal querystringProductos As String, ByVal idAtributo As Integer, Optional ByVal Nombre_Atributo As String = "id_atributo & Atributos_Producto.Id_atributo.value")
            Dim producto As New Producto
            querystringProductos = querystringProductos.Remove(0, querystringProductos.IndexOf("FROM"))
            querystringProductos = querystringProductos.Insert(0, "Select  " & producto.TableName & "." & producto.Id_Producto.Name & " ")
            querystringProductos = querystringProductos.Remove(querystringProductos.LastIndexOf("ORDER"))
            Dim Atributos_Producto As New Atributos_Producto
            Dim Atributos_Para_Productos As New Atributos_Para_Productos

            Dim datasetAtributos As Data.DataTable = selectAtributosProductos(querystringProductos, idAtributo)
            If datasetAtributos IsNot Nothing Then
                If datasetAtributos.Rows.Count > 0 Then
                    Dim columName As String = ""
                    If Nombre_Atributo.Length > 0 Then
                        columName = Nombre_Atributo
                    Else
                        columName = Atributos_Producto.Id_atributo.Name & idAtributo
                    End If
                    If dataset.Tables(0).Columns(columName) IsNot Nothing Then
                        Throw New Exception("Error la columna " & columName & " ya existe")
                    Else
                        Dim X As New Data.DataColumn(columName, Type.GetType("System.String"))
                        dataset.Tables(0).Columns.Add(X)
                        For countAtributosProductos As Integer = 0 To datasetAtributos.Rows.Count - 1
                            For countProductos As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                                If datasetAtributos.Rows(countAtributosProductos).Item(Atributos_Producto.Id_Producto.Name) = dataset.Tables(0).Rows(countProductos).Item(producto.Id_Producto.Name) Then
                                    dataset.Tables(0).Rows(countProductos).Item(columName) = datasetAtributos.Rows(countAtributosProductos).Item(Atributos_Producto.Valor.Name)
                                    Exit For
                                End If
                            Next
                        Next
                    End If

                End If
            End If

        End Sub
        Public Function selectAtributosProductos(ByVal querystringProductos As String, Optional ByVal Id_atributo As Integer = 0) As Data.DataTable

            Dim dataset As Data.DataSet

            Dim Atributos_Producto As New Atributos_Producto
            Dim Atributos_Para_Productos As New Atributos_Para_Productos

            If Id_atributo <> 0 Then
                Atributos_Para_Productos.Id_atributo.Where.EqualCondition(Id_atributo)
            End If
            Atributos_Producto.Id_Producto.Where.InCondition(querystringProductos)
            Atributos_Producto.Valor.ToSelect = True
            Atributos_Producto.Id_Producto.ToSelect = True
            queryBuilder.Join.EqualCondition(Atributos_Producto.Id_atributo, Atributos_Para_Productos.Id_atributo)

            queryBuilder.From.Add(Atributos_Producto)
            queryBuilder.From.Add(Atributos_Para_Productos)
            Dim result As String = queryBuilder.RelationalSelectQuery
            dataset = connection.executeSelect(result)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Return dataset.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function
        Public Function selectAtributosParaProductos() As Data.DataTable

            Dim dataset As Data.DataSet
            Dim Atributos_Para_Productos As New Atributos_Para_Productos
            Atributos_Para_Productos.Id_atributo.ToSelect = True

            queryBuilder.From.Add(Atributos_Para_Productos)
            Dim result As String = queryBuilder.RelationalSelectQuery
            dataset = connection.executeSelect(result)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Return dataset.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Devuelve los parametros de busqueda para contanenarlos al aspx de destino
        ''' </summary>
        ''' <param name="id_tipoProducto1erNivel"></param>
        ''' <param name="id_tipoProducto2doNivel"></param>
        ''' <param name="id_tipoProducto3erNivel"></param>
        ''' <param name="id_entidad"></param>
        ''' <param name="id_origen1erNivel"></param>
        ''' <param name="id_origen2doNivel"></param>
        ''' <param name="id_origen3erNivel"></param>
        ''' <param name="id_estado"></param>
        ''' <param name="nombre"></param>
        ''' <param name="rangoPrecios"></param>
        ''' <param name="SKU"></param>
        ''' <param name="Activo"></param>
        ''' <param name="enPrincipal"></param>
        ''' <param name="lista_keywords"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ArmarQueryStringProductos(ByVal id_tipoProducto1erNivel As Integer, ByVal id_tipoProducto2doNivel As Integer, ByVal id_tipoProducto3erNivel As Integer, ByVal id_entidad As Integer, ByVal id_origen1erNivel As Integer, ByVal id_origen2doNivel As Integer, ByVal id_origen3erNivel As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal rangoPrecios As String, ByVal SKU As String, ByVal Activo As Enum_Activo, ByVal enPrincipal As Enum_enPrincipal, Optional ByVal lista_keywords As String = "") As String
            'Dim queryString As String = "Productos.aspx"
            Dim queryString As String = ""
            If id_tipoProducto1erNivel <> 0 Then
                queryString &= "?tipoProducto1erNivel=" & id_tipoProducto1erNivel
            End If
            If id_tipoProducto2doNivel <> 0 Then
                queryString &= "&tipoProducto2doNivel=" & id_tipoProducto2doNivel
            End If
            If id_tipoProducto3erNivel <> 0 Then
                queryString &= "&tipoProducto3erNivel=" & id_tipoProducto3erNivel
            End If
            If id_entidad <> 0 Then
                queryString &= "&id_entidad=" & id_entidad
            End If
            If id_origen1erNivel <> 0 Then
                queryString &= "&id_origen1erNivel=" & id_origen1erNivel
            End If
            If id_origen2doNivel <> 0 Then
                queryString &= "&id_origen2doNivel=" & id_origen2doNivel
            End If
            If id_origen3erNivel <> 0 Then
                queryString &= "&id_origen3erNivel=" & id_origen3erNivel
            End If
            If id_estado Then
                queryString &= "&id_estado=" & id_estado
            End If
            If rangoPrecios <> "0" And rangoPrecios <> "" Then
                queryString &= "&rangoPrecios=" & rangoPrecios
            End If
            If nombre.Length > 0 Then
                queryString &= "&nombre=" & nombre
            End If
            If SKU.Length > 0 Then
                queryString &= "&SKU=" & SKU
            End If
            If Activo <> ControladoraProductos.Enum_Activo.Si Then
                queryString &= "&Activo=" & Activo
            End If
            If enPrincipal <> ControladoraProductos.Enum_enPrincipal.Todos Then
                queryString &= "&enPrincipal=" & enPrincipal
            End If

            If lista_keywords.Length > 0 Then
                queryString &= "&lista_keywords=" & lista_keywords
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
        ''' <param name="ddl_TipoProducto1erNivel"></param>
        ''' <param name="ddl_TipoProducto2doNivel"></param>
        ''' <param name="ddl_TipoProducto3erNivel"></param>
        ''' <param name="ddl_Entidad"></param>
        ''' <param name="ddl_origen1erNivel"></param>
        ''' <param name="ddl_origen2doNivel"></param>
        ''' <param name="ddl_origen3erNivel"></param>
        ''' <param name="ddl_Estado"></param>
        ''' <param name="ddl_Precios"></param>
        ''' <param name="tbx_nombre"></param>
        ''' <param name="tbx_SKU"></param>
        ''' <param name="tbx_keywords"></param>
        ''' <param name="chkboxlist_keywords"></param>
        ''' <remarks></remarks>
        Public Sub cargarParametrosEnFiltros(ByVal parametros As System.Collections.Specialized.NameValueCollection, Optional ByRef ddl_TipoProducto1erNivel As DropDownList = Nothing, Optional ByRef ddl_TipoProducto2doNivel As DropDownList = Nothing, Optional ByRef ddl_TipoProducto3erNivel As DropDownList = Nothing, Optional ByRef ddl_Entidad As DropDownList = Nothing, Optional ByRef ddl_origen1erNivel As DropDownList = Nothing, Optional ByRef ddl_origen2doNivel As DropDownList = Nothing, Optional ByRef ddl_origen3erNivel As DropDownList = Nothing, Optional ByRef ddl_Estado As DropDownList = Nothing, Optional ByRef ddl_Precios As DropDownList = Nothing, Optional ByRef tbx_nombre As TextBox = Nothing, Optional ByRef tbx_SKU As TextBox = Nothing, Optional ByRef tbx_keywords As TextBox = Nothing, Optional ByRef chkboxlist_keywords As CheckBoxList = Nothing)

            If Not parametros.Item("tipoProducto1erNivel") Is Nothing And ddl_TipoProducto1erNivel IsNot Nothing Then
                ddl_TipoProducto1erNivel.SelectedValue = parametros.Item("tipoProducto1erNivel")
                If ddl_TipoProducto2doNivel IsNot Nothing Then
                    cargarCombo(ddl_TipoProducto2doNivel, Enum_clase.TipoProducto, ddl_TipoProducto1erNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("tipoProducto2doNivel") Is Nothing And ddl_TipoProducto2doNivel IsNot Nothing Then
                ddl_TipoProducto2doNivel.SelectedValue = parametros.Item("tipoProducto2doNivel")
                If ddl_TipoProducto3erNivel IsNot Nothing Then
                    cargarCombo(ddl_TipoProducto3erNivel, Enum_clase.TipoProducto, ddl_TipoProducto2doNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("tipoProducto3erNivel") Is Nothing And ddl_TipoProducto3erNivel IsNot Nothing Then
                ddl_TipoProducto3erNivel.SelectedValue = parametros.Item("tipoProducto3erNivel")
            End If

            If Not parametros.Item("id_entidad") Is Nothing And ddl_Entidad IsNot Nothing Then
                ddl_Entidad.SelectedValue = parametros.Item("id_entidad")
            End If

            If Not parametros.Item("id_origen1erNivel") Is Nothing And ddl_origen1erNivel IsNot Nothing Then
                ddl_origen1erNivel.SelectedValue = parametros.Item("id_origen1erNivel")
                If ddl_origen2doNivel IsNot Nothing Then
                    cargarCombo(ddl_origen2doNivel, Enum_clase.Origenes, ddl_origen1erNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("id_origen2doNivel") Is Nothing And ddl_origen2doNivel IsNot Nothing Then
                ddl_origen2doNivel.SelectedValue = parametros.Item("id_origen2doNivel")
                If ddl_origen3erNivel IsNot Nothing Then
                    cargarCombo(ddl_origen3erNivel, Enum_clase.Origenes, ddl_origen2doNivel.SelectedValue)
                End If
            End If

            If Not parametros.Item("id_origen3erNivel") Is Nothing And ddl_origen3erNivel IsNot Nothing Then
                ddl_origen3erNivel.SelectedValue = parametros.Item("id_origen3erNivel")
            End If

            If Not parametros.Item("id_estado") Is Nothing And ddl_Estado IsNot Nothing Then
                ddl_Estado.SelectedValue = parametros.Item("id_estado")
            End If

            If Not parametros.Item("nombre") Is Nothing And tbx_nombre IsNot Nothing Then
                tbx_nombre.Text = parametros.Item("nombre")
            End If

            If Not parametros.Item("rangoPrecios") Is Nothing Then
                ddl_Precios.SelectedValue = parametros.Item("rangoPrecios")
            End If

            If Not parametros.Item("SKU") Is Nothing And tbx_SKU IsNot Nothing Then
                tbx_SKU.Text = parametros.Item("SKU")
            End If

            If Not parametros.Item("Activo") Is Nothing Then
            End If

            If Not parametros.Item("enPrincipal") Is Nothing Then
            End If

            If Not parametros.Item("lista_keywords") Is Nothing And tbx_keywords IsNot Nothing Then
                tbx_keywords.Text = parametros.Item("lista_keywords")
                tbx_keywords.Attributes("lista_keywords") = parametros.Item("lista_keywords")
                If chkboxlist_keywords IsNot Nothing Then
                    cargarCheckBoxList(chkboxlist_keywords, Enum_clasesCheckList.Keywords, parametros.Item("lista_keywords"))
                    cargarTexboxKeywordsSegunCheckBoxListKeywords(tbx_keywords, chkboxlist_keywords)
                End If

            End If

        End Sub

        ''' <summary>
        ''' Esta funcion recibe un string con los rangos y devuelve el minimo o el maximo. El caracter que los separa es (-)
        ''' </summary>
        ''' <param name="rangoPrecios"></param>
        ''' <param name="precio"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPrecio(ByVal rangoPrecios As String, ByVal precio As Enum_Precio) As String
            Dim array As String() = rangoPrecios.Split("-")
            Dim result As String = "0"
            If array.Length > 0 Then
                If precio = ControladoraProductos.Enum_Precio.minimo Then
                    result = array(0)
                End If
                If precio = ControladoraProductos.Enum_Precio.maximo Then
                    result = array(1)
                End If
            End If
            Return result
        End Function
    End Class
End Namespace