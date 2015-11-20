Imports Microsoft.VisualBasic
Imports Orbelink.Control.Publicaciones.ControladoraPublicacionesSearch

Namespace Orbelink.Control.Publicaciones
    Public Class BusquedasYFiltros
        ''' <summary>
        ''' Carga el combo de ordenar por Aqui se pueden quitar opciones o cambiar el texto de cada opcion
        ''' </summary>
        ''' <param name="dropdownlist"></param>
        ''' <remarks></remarks>
        Public Sub cargarDllOrderBy(ByRef dropdownlist As DropDownList)
            'Aqui se pueden eliminar los order by que no se ocupan y se le puede cambiar el Nombre que quieren que salga  en el combo  pero el value NO lo cambien
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Titulo.ToString, Enum_OrderBy.Titulo))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Corta.ToString, Enum_OrderBy.Corta))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Fecha.ToString, Enum_OrderBy.Fecha))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.FechaInicio.ToString, Enum_OrderBy.FechaInicio))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Aprobada.ToString, Enum_OrderBy.Aprobada))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Visible.ToString, Enum_OrderBy.Visible))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.IncluirRSS.ToString, Enum_OrderBy.IncluirRSS))
            dropdownlist.Items.Add(New ListItem(Enum_OrderBy.Link.ToString, Enum_OrderBy.Link))
            dropdownlist.Items.Add(New ListItem("Entidad", Enum_OrderBy.Id_Entidad))
            dropdownlist.Items.Add(New ListItem("Estado", Enum_OrderBy.Id_Estado))
            dropdownlist.Items.Add(New ListItem("Tipo Publicacion", Enum_OrderBy.Id_tipoPublicacion))
            dropdownlist.Items.Add(New ListItem("Publicacion", Enum_OrderBy.Id_Publicacion))
            dropdownlist.Items.Add(New ListItem("Categoria", Enum_OrderBy.Id_Categoria))

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
        ''' Devuelve la lista de atributos que se van a usar en la pagina. Se pueden agregar o quitar atributos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function selectListaAtributosCustom() As Integer()
            Dim TamanoLista As Integer = 2 ' OJO si van agregar mas atributos hay que cambiar este tamano de la lista

            Dim listaAtributos(TamanoLista) As Integer
            listaAtributos(0) = 1 'Estos son los ids de los atributos que se quieren
            listaAtributos(1) = 2
            listaAtributos(2) = 3
            Return listaAtributos
        End Function
    End Class
End Namespace

