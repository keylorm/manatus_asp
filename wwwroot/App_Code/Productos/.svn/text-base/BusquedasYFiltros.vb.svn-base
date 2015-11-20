Imports Microsoft.VisualBasic
Imports Orbelink.Control.Productos.ControladoraProductos

Namespace Orbelink.Control.Productos
    Public Class BusquedasYFiltros
        ''' <summary>
        ''' Carga el combo de ordenar por Aqui se pueden quitar opciones o cambiar el texto de cada opcion
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <remarks></remarks>
        Public Sub cargarDllOrderBy(ByRef dropdownlist As DropDownList)
            'Aqui se pueden eliminar los order by que no se ocupan y se le puede cambiar el Nombre que quieren que salga  en el combo  pero el value NO lo cambien
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Nombre.ToString, Enum_OrderBy.Nombre))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.PrecioUnitario.ToString, Enum_OrderBy.PrecioUnitario))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Desc_Corta.ToString, Enum_OrderBy.Desc_Corta))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Fecha.ToString, Enum_OrderBy.Fecha))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.enPrincipal.ToString, Enum_OrderBy.enPrincipal))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.SKU.ToString, Enum_OrderBy.SKU))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Activo.ToString, Enum_OrderBy.Activo))
            dropdownlist.Items.Add(New ListItem("Entidad", Enum_OrderBy.Id_Entidad))
            dropdownlist.Items.Add(New ListItem("Estado", Enum_OrderBy.Id_Estado))
            dropdownlist.Items.Add(New ListItem("Origen", Enum_OrderBy.Id_Origen))
            dropdownlist.Items.Add(New ListItem("Producto", Enum_OrderBy.Id_Producto))
            dropdownlist.Items.Add(New ListItem("Tipo Producto", Enum_OrderBy.Id_tipoProdcto))
            dropdownlist.Items.Add(New ListItem("Unidad", Enum_OrderBy.Id_Unidad))
        End Sub
        ''' <summary>
        '''  Carga el combo de ordenar ascendente o descendente se pueden quitar opciones o cambiar el texto de cada opcion
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <remarks></remarks>
        Public Sub cargarDllAsc(ByVal dropdownlist As DropDownList)
            dropdownlist.Items.Add(New ListItem(Enum_Asc.Ascendente.ToString, Enum_Asc.Ascendente))
            dropdownlist.Items.Add(New ListItem(Enum_Asc.Descendente.ToString, Enum_Asc.Descendente))
        End Sub
        ''' <summary>
        ''' Carga el combo de rangos de precios se pueden quitar opciones o cambiar el texto de cada opcion
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <param name="textoDefault"></param>
        ''' <remarks></remarks>
        Public Sub cargarDllPrecios(ByVal dropdownlist As DropDownList, Optional ByVal textoDefault As String = "-- Seleccione --")
            dropdownlist.Items.Add(New ListItem("0-10 ", "0-10"))
            dropdownlist.Items.Add(New ListItem("10-20 ", "10-20"))
            dropdownlist.Items.Add(New ListItem("20-50", "20-50"))
            dropdownlist.Items.Add(New ListItem("100-150", "100-150"))
            dropdownlist.Items.Add(New ListItem("300-600", "300-600"))
            If textoDefault.Length > 0 Then
                dropdownlist.Items.Add(New ListItem(textoDefault, 0))
                dropdownlist.SelectedIndex = dropdownlist.Items.Count - 1
            End If
        End Sub
        '''' <summary>
        '''' Devuelve la lista de atributos que se van a usar en la pagina. Se pueden agregar o quitar atributos
        '''' </summary>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Public Function selectListaAtributosCustom() As Integer()
        '    Dim TamanoLista As Integer = 2 ' OJO si van agregar mas atributos hay que cambiar este tamano de la lista

        '    Dim listaAtributos(TamanoLista) As Integer
        '    listaAtributos(0) = 1 'Estos son los ids de los atributos que se quieren
        '    listaAtributos(1) = 2
        '    listaAtributos(2) = 3
        '    Return listaAtributos
        'End Function
    End Class
End Namespace

