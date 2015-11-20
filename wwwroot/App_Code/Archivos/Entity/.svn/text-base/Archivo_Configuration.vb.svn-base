Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Archivos

    Public Class Archivo_Configuration : Inherits DBTable

        Dim _tableName As String = "AR_Archivo_Configuration"

        Public ReadOnly Id_Configuration As New FieldInt("Id_Configuration", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_Archivo_Configuration", Me, Field.FieldType.VARCHAR_MULTILANG, 50, True)
        Public ReadOnly ImgWidth As New FieldInt("ImgWidth", Me)
        Public ReadOnly ImgHeight As New FieldInt("ImgHeight", Me)
        Public ReadOnly ThumWidth As New FieldInt("ThumWidth", Me)
        Public ReadOnly ThumHeight As New FieldInt("ThumHeight", Me)
        Public ReadOnly Compresion As New FieldInt("Compresion", Me)
        Public ReadOnly AutoPlay As New FieldTinyInt("AutoPlay", Me)
        Public ReadOnly Transition As New FieldString("Transition", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Interval As New FieldInt("Interval", Me)
        Public ReadOnly Lock As New FieldTinyInt("Lock", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theImgWidth As String, ByVal theImgHeight As String, ByVal theThumWidth As String, _
            ByVal theThumHeight As String, ByVal theCompresion As String, ByVal theAutoPlay As String, _
            ByVal theTransition As String, ByVal theInterval As String, ByVal theLock As String)
            Nombre.Value = theNombre
            ImgWidth.Value = theImgWidth
            ImgHeight.Value = theImgHeight
            ThumWidth.Value = theThumWidth
            ThumHeight.Value = theThumHeight
            Compresion.Value = theCompresion
            AutoPlay.Value = theAutoPlay
            Transition.Value = theTransition
            Interval.Value = theInterval
            Lock.Value = theLock
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Configuration, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Configuration)
            _fields.Add(Nombre)
            _fields.Add(ImgWidth)
            _fields.Add(ImgHeight)
            _fields.Add(ThumWidth)
            _fields.Add(ThumHeight)
            _fields.Add(Compresion)
            _fields.Add(AutoPlay)
            _fields.Add(Transition)
            _fields.Add(Interval)
            _fields.Add(Lock)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo_Configuration
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo_Configuration
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo_Configuration
            Dim temp As New Archivo_Configuration()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class

End Namespace