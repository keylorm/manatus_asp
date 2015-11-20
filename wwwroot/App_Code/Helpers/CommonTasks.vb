Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Web.HttpContext
Imports Orbelink.Orbecatalog6
Imports System.IO

Namespace Orbelink.Helpers

    Public Class CommonTasks

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        ''' <summary>
        ''' Regenera el string del get para agregarle la referencia a la pagina actual
        ''' </summary>
        ''' <param name="codigo_PantallaActual">Codigo de la pagina actual</param>
        ''' <param name="first">Si no hay parametros get antes</param>
        ''' <returns>El query get ya generado</returns>
        ''' <remarks></remarks>
        Shared Function RecreateQueryString(ByVal codigo_PantallaActual As String, Optional ByVal first As Boolean = False) As String
            Dim newQueryString As String = "&"
            If first Then
                newQueryString = "?"
            End If
            newQueryString &= "reference=" & codigo_PantallaActual

            Dim counter As Integer
            For counter = 0 To Current.Request.QueryString.Count - 1
                Dim llave As String = Current.Request.QueryString.AllKeys(counter)
                If llave <> "back" Then
                    'Mantiene el trace de referencias
                    If llave.Contains("reference") Then
                        Dim secuencia As Integer = 0
                        If llave.Length > 9 Then
                            secuencia = llave.Substring(9)
                        End If
                        newQueryString &= "&reference" & (secuencia + 1) & "="
                    Else
                        newQueryString &= "&" & llave & "="

                    End If
                    newQueryString &= Current.Request.QueryString(counter)
                End If
            Next
            Return newQueryString
        End Function

        ''' <summary>
        ''' Lee un archivo de excel y devuelve un dataset con la informacion
        ''' </summary>
        ''' <param name="hoja">Sheet del cual leer la informacion en excel</param>
        ''' <returns>Dataset con la informacion del archivo</returns>
        ''' <remarks></remarks>
        Shared Function cargadelexcelDS(ByRef uplControl As FileUpload, ByVal hoja As String) As Data.DataSet
            Dim ds As Data.DataSet = Nothing
            Try
                If uplControl.HasFile Then
                    ds = New Data.DataSet()
                    Dim filename As String = uplControl.FileName
                    Dim filePath As String = System.AppDomain.CurrentDomain.BaseDirectory & filename
                    uplControl.SaveAs(filePath)
                    Dim extension As String = filename.Substring(filename.LastIndexOf("."))

                    If extension.Equals(".xls") Then
                        Dim cnn As New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties=""Excel 8.0;HDR=No;IMEX=1; """)
                        cnn.Open()
                        Dim nombre As String = hoja & "$"
                        Dim da As New System.Data.OleDb.OleDbDataAdapter("Select * from [" & nombre & "]", cnn)
                        da.Fill(ds)
                        cnn.Close()
                    End If
                    File.Delete(filePath)
                End If
            Catch ex As Exception

            End Try
            Return ds
        End Function
    End Class
End Namespace