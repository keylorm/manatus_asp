Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Data.SqlClient

Namespace Orbelink.DBHandler

    Public Class DBException : Inherits Exception
        Const errorString As String = "Orbelink.DBHandler Error: "
        Dim _Exception As SqlException
        Dim _query As String
        Dim _mensaje As String
        Dim _MyDBExceptionType As DBExceptionTypes

        Sub New(ByVal classID As String, ByVal theException As SqlException, ByVal theQuery As String)
            MyBase.New(theException.Message)
            'mensaje = obtenerMensaje(theException.Number, theException.Message)
            _mensaje = obtenerMensaje(obtenerDBExceptionType(theException.Number))
            _Exception = theException
            _query = theQuery
        End Sub

        Public ReadOnly Property Query() As String
            Get
                Return _query
            End Get
        End Property

        Public Overrides ReadOnly Property Message() As String
            Get
                Return _mensaje
            End Get
        End Property

        Public ReadOnly Property MyDBExceptionType() As DBExceptionTypes
            Get
                Return _MyDBExceptionType
            End Get
        End Property

        Public Enum DBExceptionTypes
            General
            Reference_Exception
            ServerConnecion
            TablaNoExiste
            FaltanCampos
            NoSeEncuentraAlgunCampo
            AtributoUnicoYaExiste
        End Enum

        Private Function obtenerDBExceptionType(ByVal errorNumber As Integer) As DBExceptionTypes
            Dim theExceptionType As DBExceptionTypes = DBExceptionTypes.General

            Select Case (errorNumber)
                Case -1
                    theExceptionType = DBExceptionTypes.ServerConnecion

                Case 53
                    theExceptionType = DBExceptionTypes.ServerConnecion

                Case 208
                    theExceptionType = DBExceptionTypes.TablaNoExiste

                Case 515
                    theExceptionType = DBExceptionTypes.FaltanCampos

                Case 547
                    theExceptionType = DBExceptionTypes.Reference_Exception

                Case 4104
                    theExceptionType = DBExceptionTypes.NoSeEncuentraAlgunCampo

                Case 2627
                    theExceptionType = DBExceptionTypes.AtributoUnicoYaExiste

            End Select
            Return theExceptionType
        End Function

        Private Function obtenerMensaje(ByVal theDBException As DBExceptionTypes) As String
            Dim nombre As String = theDBException.ToString
            Return Resources.DBHandler_Resources.ResourceManager.GetString(nombre)
        End Function

        Private Function obtenerMensaje(ByVal errorNumber As Integer, ByVal message As String) As String
            Dim mensaje As String = ""
            If errorNumber = 515 Then
                Try
                    Dim inicio As Integer = message.IndexOf("'", 0)
                    Dim al As Integer = inicio + 2
                    Dim final As Integer = message.IndexOf("'", al)
                    Dim falta As String = message.Substring(inicio + 1, final - inicio + 1)
                    mensaje = "Debe llenar todos los campos correspondientes" & " falta " & falta
                Catch ex As Exception
                    mensaje = "Debe llenar todos los campos correspondientes"
                End Try
                '"Cannot insert the value NULL into column n_TipoRelacion_En_PR_en, table Orbecatalog6.0.0.dbo.RE_TipoRelacion_Entidad_Producto; column does not allow nulls. INSERT fails."
            End If
            Return mensaje
        End Function

    End Class

End Namespace