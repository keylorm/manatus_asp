Imports Orbelink.DBHandler
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Partial Class Controls_DetalleProducto
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass
    'Dim level As Integer

    Public Sub SetVariables(ByVal theQueryBuilder As Orbelink.DBHandler.QueryBuilder, _
                           ByVal theConnection As Orbelink.DBHandler.SQLServer, _
                           ByVal theSecurityHandler As Orbelink.Control.Security.SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As Orbelink.Orbecatalog6.MasterPageBaseClass, _
                           ByVal theLevel As Integer)
        queryBuilder = theQueryBuilder
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
        'level = theLevel
    End Sub

    Public Sub cargarInformacion(ByVal id_producto As Integer)
        Dim dataSet As New Data.DataSet
        Dim producto As New Producto
        Dim tipoProducto As New TipoProducto
        Dim entidad As New Entidad
        Dim unidad As New Moneda
        producto.Fields.SelectAll()
        tipoProducto.Nombre.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        unidad.Simbolo.ToSelect = True

        queryBuilder.Join.EqualCondition(producto.Id_tipoProducto, tipoProducto.Id_TipoProducto)
        queryBuilder.Join.EqualCondition(producto.id_Entidad, entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(producto.Id_Moneda, unidad.Id_Moneda)
        producto.Id_Producto.Where.EqualCondition(id_producto)

        queryBuilder.From.Add(producto)
        queryBuilder.From.Add(tipoProducto)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(unidad)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, producto)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, tipoProducto)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, unidad)

                Me.lbl_nombre.Text = producto.Nombre.Value
                Me.lbl_precio.Text = unidad.Simbolo.Value & producto.Precio_Unitario.Value
                Me.lbl_Sku.Text = producto.SKU.Value
                Me.lbl_entidadid.Text = entidad.NombreDisplay.Value
                Me.lbl_descripcion.Text = producto.DescCorta_Producto.Value
                Me.lbl_tipo.Text = tipoProducto.Nombre.Value

                cargarkeywords(producto.Id_Producto.Value)
                cargarImagenes(producto.Id_Producto.Value)
            End If
        End If
    End Sub

    Protected Sub cargarkeywords(ByVal id_producto As Integer)
        Dim keywords As New Keywords
        Dim Keywords_Producto As New Keywords_Producto
        Dim dataSet As New Data.DataSet
        keywords.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Keywords_Producto.Id_Keyword, keywords.Id_Keyword)
        Keywords_Producto.Id_Producto.Where.EqualCondition(id_producto)

        queryBuilder.From.Add(keywords)
        queryBuilder.From.Add(Keywords_Producto)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                DL_Keyword.DataSource = dataSet
                DL_Keyword.DataBind()

                Dim resul_keyWord As ArrayList = TransformDataTable(dataSet.Tables(0), keywords)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1

                    Dim act_keyword As Keywords = resul_keyWord(counter)
                    Dim nombre As Label = DL_Keyword.Items(counter).FindControl("lbl_nombreKeyword")
                    nombre.Text = act_keyword.Nombre.Value
                Next
            Else
                Me.lbl_keywords.Visible = True
            End If
        End If
    End Sub

    Protected Sub cargarImagenes(ByVal id_producto As Integer)
        Dim archivos As New Archivo
        Dim Archivo_Producto As New Archivo_Producto
        Dim dataSet As New Data.DataSet

        archivos.Fields.SelectAll()
        Archivo_Producto.Principal.ToSelect = True
        queryBuilder.Join.EqualCondition(archivos.Id_Archivo, Archivo_Producto.id_Archivo)
        Archivo_Producto.id_Producto.Where.EqualCondition(id_producto)
        queryBuilder.From.Add(archivos)
        queryBuilder.From.Add(Archivo_Producto)
        queryBuilder.Top = 3
        queryBuilder.Orderby.Add(Archivo_Producto.Principal, False)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim result_archivos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), archivos)
                Dim result_arch_produ As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Producto)
                dl_imagenes.DataSource = dataSet
                dl_imagenes.DataBind()
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_archivo As Archivo = result_archivos(counter)
                    Dim act_arch_produ As Archivo_Producto = result_arch_produ(counter)
                    Dim imagen As Image = dl_imagenes.Items(counter).FindControl("img_archivo")
                    ArchivoHandler.GetArchivoImage(imagen, act_archivo.FileName.Value, act_archivo.Extension.Value, 1, True)

                    Dim nombre As Label = dl_imagenes.Items(counter).FindControl("lbl_imgNombre")
                    Dim peso As Label = dl_imagenes.Items(counter).FindControl("lbl_imgTamano")
                    Dim fecha As Label = dl_imagenes.Items(counter).FindControl("lbl_imgFecha")
                    Dim nombre2 As String = act_archivo.Nombre.Value
                    If act_archivo.Nombre.Value.Length > 12 Then
                        nombre2 = nombre2.Substring(0, 12)
                    End If
                    nombre.Text = nombre2
                    peso.Text = act_archivo.Size.Value
                    fecha.Text = act_archivo.Fecha.ValueLocalized

                    If act_arch_produ.Principal.Value > 0 Then
                        Dim imagenP As New Image
                        ArchivoHandler.GetArchivoImage(imagenP, act_archivo.FileName.Value, act_archivo.Extension.Value, 1, True)
                        img_principal.Src = imagenP.ImageUrl
                    End If
                Next
            Else
                Me.archivos.Visible = False
            End If
        Else
            Me.archivos.Visible = False
        End If
    End Sub
End Class
