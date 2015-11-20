Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports Orbelink.DBHandler

Namespace Orbelink.Service
    Public Class Paging
        'PAGINACION

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Enum Enum_PaginacionDireccion As Integer
            atras
            siguiente
        End Enum
        ''' <summary>
        ''' Carga la pagina actual en el datalist y devuelve el dataset con los registros actuales
        ''' </summary>
        ''' <param name="querystring"></param>
        ''' <param name="dtl_listado"></param>
        ''' <param name="lbl_TotalRegistros"></param>
        ''' <param name="currentIndex"></param>
        ''' <param name="pageSize"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function PagingSetDataSource(ByRef querystring As String, ByVal dtl_listado As DataList, ByRef lbl_TotalRegistros As Label, ByVal currentIndex As Integer, ByVal pageSize As Integer) As Data.DataSet
            Dim objConn As New SqlConnection(connection.connectionString)
            Dim dataAdapter As New SqlDataAdapter(querystring, objConn)
            objConn.Close()
            Dim dataset As New Data.DataSet
            dataAdapter.Fill(dataset)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    lbl_TotalRegistros.Text = dataset.Tables(0).Rows.Count
                    If pageSize <= 0 Then
                        pageSize = dataset.Tables(0).Rows.Count
                    End If
                    If dataAdapter.Fill(dataset, currentIndex, pageSize, "resultado") <> 0 Then
                        dtl_listado.DataSource = dataset.Tables("resultado").DefaultView
                        dtl_listado.DataBind()
                        dtl_listado.Visible = True
                    Else
                        lbl_TotalRegistros.Text = 0
                        dtl_listado.Visible = False
                    End If
                End If
            End If
            Return dataset
        End Function


        Public Function PagingSetDataRows(ByRef querystring As String, ByVal currentIndex As Integer, ByVal pageSize As Integer) As Data.DataSet
            Dim objConn As New SqlConnection(connection.connectionString)
            Dim dataAdapter As New SqlDataAdapter(querystring, objConn)
            objConn.Close()
            Dim dataset As New Data.DataSet
            Dim resultado As New Data.DataSet
            dataAdapter.Fill(dataset)
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    If pageSize <= 0 Then
                        pageSize = dataset.Tables(0).Rows.Count
                    End If
                    dataAdapter.Fill(resultado, currentIndex * pageSize, pageSize, "resultado")
                    resultado.Tables.Add(dataset.Tables(0).Copy)
                End If
            End If
            Return resultado

        End Function




        ''' <summary>
        ''' Paginacion de datalist con modo numerico, atras  y siguiente
        ''' </summary>
        ''' <param name="dtl_paging">DataList de paginacion</param>
        ''' <param name="lnk_siguiente">LinkButton Siguiente>></param>
        ''' <param name="lnk_atras">LinkButton Atras  </param>
        ''' <param name="totalRegistros">Total de registros del DataList al que se esta paginando</param>
        ''' <param name="pageSize">Registros por pagina del DataList al que se esta paginando</param>
        ''' <param name="pageButtonCount">Tamano del DataList de paginacion</param>
        ''' <param name="direccion">Direccion de paginacion</param>
        ''' <remarks></remarks>
        Public Sub PagingDataList(ByRef dtl_paging As DataList, ByRef lnk_siguiente As LinkButton, ByRef lnk_atras As LinkButton, ByVal totalRegistros As Integer, ByVal pageSize As Integer, ByVal pageButtonCount As Integer, Optional ByVal direccion As Enum_PaginacionDireccion = Enum_PaginacionDireccion.siguiente, Optional ByVal principio As Boolean = False)
            If pageButtonCount > 0 And pageSize > 0 Then
                Dim pages As Integer = Math.Truncate(totalRegistros / pageSize)
                Dim residuo As Integer = totalRegistros Mod pageSize
                Dim tamano As Integer = pageButtonCount 'cantidad de numeros entre << >>

                If principio Then
                    lnk_siguiente.Attributes.Remove("ultimo")
                    lnk_atras.Attributes.Remove("primero")
                End If

                If totalRegistros = 0 Then
                    lnk_siguiente.Visible = False
                    lnk_atras.Visible = False
                End If

                If pages = 0 Then
                    pages = 1
                ElseIf residuo > 0 Then
                    pages += 1
                End If

                If tamano < 0 Then
                    tamano = pages
                End If
                Dim inicio As Integer = 0

                If direccion = Enum_PaginacionDireccion.siguiente Then 'siguiente
                    If lnk_siguiente.Attributes("ultimo") IsNot Nothing Then
                        inicio = lnk_siguiente.Attributes("ultimo")
                    End If
                    If (inicio + tamano) >= pages Then
                        tamano = pages - inicio
                        lnk_siguiente.Visible = False
                    Else
                        lnk_siguiente.Visible = True
                    End If

                Else 'atras
                    If lnk_atras.Attributes("primero") IsNot Nothing Then
                        inicio = lnk_atras.Attributes("primero")
                    End If

                    If (inicio - tamano) >= pages Then
                        lnk_siguiente.Visible = False
                    Else
                        lnk_siguiente.Visible = True
                    End If
                End If

                If pages < tamano Then
                    tamano = pages
                End If

                Dim offset As Integer = 0
                If direccion = Enum_PaginacionDireccion.siguiente Then
                    offset = inicio
                Else
                    If inicio - tamano < 0 Then
                        offset = 0
                    Else
                        offset = inicio - tamano
                    End If
                End If

                If offset = 0 Then
                    lnk_atras.Visible = False
                Else
                    lnk_atras.Visible = True
                End If
                If pageButtonCount > 0 Then
                    PagingLlenarDataList(dtl_paging, offset, tamano, direccion)
                End If

                lnk_siguiente.Attributes("ultimo") = offset + tamano
                lnk_atras.Attributes("primero") = offset
            Else
                lnk_siguiente.Visible = False
                lnk_atras.Visible = False
            End If
        End Sub
        Private Sub PagingLlenarDataList(ByRef dtl_paging As DataList, ByVal offset As Integer, ByVal tamano As Integer, ByVal direccion As Enum_PaginacionDireccion)
            Dim datatable As New Data.DataTable
            datatable.Columns.Add("1")
            For counter = offset To offset + tamano - 1
                datatable.Rows.Add(counter)
            Next

            dtl_paging.DataSource = datatable
            dtl_paging.DataBind()
            PagingLimpiarCSSDatalist(dtl_paging)
            Dim offsetTemp As Integer = offset + 1
            For counter = 0 To dtl_paging.Items.Count - 1
                Dim lnk_num As LinkButton = dtl_paging.Items(counter).FindControl("lnk_num")
                lnk_num.Attributes.Add("index", counter)
                If counter = 0 And direccion = Enum_PaginacionDireccion.siguiente Then
                    lnk_num.CssClass = "PagingCurrentPage"
                Else
                    If counter = dtl_paging.Items.Count - 1 And direccion = Enum_PaginacionDireccion.atras Then
                        lnk_num.CssClass = "PagingCurrentPage"
                    End If
                End If
                lnk_num.Text = offsetTemp
                offsetTemp = offsetTemp + 1
            Next
        End Sub
        ''' <summary>
        '''A los linkButtons de la paginacion le pone la CssClass PagingPages
        ''' </summary>
        ''' <param name="dtl_paging"></param>
        ''' <remarks></remarks>
        Public Sub PagingLimpiarCSSDatalist(ByRef dtl_paging As DataList)
            Dim counter As Integer = 0
            For counter = 0 To dtl_paging.Items.Count - 1
                Dim lnk_num As LinkButton = dtl_paging.Items(counter).FindControl("lnk_num")
                lnk_num.CssClass = "PagingPages"
            Next
        End Sub
        ''' <summary>
        ''' Asigna indice actual de la paginacion
        ''' </summary>
        ''' <param name="dtl_paging"></param>
        ''' <param name="lblCurrentIndex"></param>
        ''' <param name="Index"></param>
        ''' <param name="pagesize"></param>
        ''' <param name="direccion"></param>
        ''' <remarks></remarks>
        Public Sub PagingSetCurrentIndex(ByRef dtl_paging As DataList, ByRef lblCurrentIndex As Label, ByVal Index As Integer, ByVal pagesize As Integer, Optional ByVal direccion As Enum_PaginacionDireccion = Enum_PaginacionDireccion.siguiente)
            If dtl_paging.Items.Count > 0 Then
                Dim lnk_num As LinkButton = dtl_paging.Items(Index).FindControl("lnk_num")
                lblCurrentIndex.Text = (CInt(lnk_num.Text) * pagesize) - pagesize
            End If
        End Sub
        ''' <summary>
        ''' Asigna indice actual de la paginacion
        ''' </summary>
        ''' <param name="lblCurrentIndex"></param>
        ''' <param name="lnk_num"></param>
        ''' <param name="pagesize"></param>
        ''' <remarks></remarks>
        Public Sub PagingSetCurrentIndex(ByRef lblCurrentIndex As Label, ByVal lnk_num As LinkButton, ByVal pagesize As Integer)
            lblCurrentIndex.Text = (lnk_num.Text * pagesize) - pagesize
            lnk_num.CssClass = "PagingCurrentPage"
        End Sub
        ''' <summary>
        ''' Asigna Page size del listado
        ''' </summary>
        ''' <param name="lblPageSize"></param>
        ''' <param name="pagesize"></param>
        ''' <remarks></remarks>
        Public Sub PagingSetPageSize(ByRef lblPageSize As Label, ByVal pagesize As Integer)
            lblPageSize.Text = pagesize
        End Sub
        ''' <summary>
        ''' Cantidad de paginas en datalist paginacion
        ''' </summary>
        ''' <param name="lbl_PageButtonCount"></param>
        ''' <param name="PageButtonCount"></param>
        ''' <remarks></remarks>
        Public Sub PagingSetPageButtonCount(ByRef lbl_PageButtonCount As Label, ByVal PageButtonCount As Integer)
            lbl_PageButtonCount.Text = PageButtonCount
        End Sub
    End Class
End Namespace