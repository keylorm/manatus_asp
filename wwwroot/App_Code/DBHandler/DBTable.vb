Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler
    ''' <summary>
    ''' Simula una tabla en la base de datos
    ''' </summary>
    ''' <version>5.2</version>
    ''' <remarks></remarks>
    Public MustInherit Class DBTable

        Protected _asName As String = ""
        Protected _isView As Boolean = False
        Protected _fields As Orbelink.DBHandler.FieldCollection
        Protected _primaryKey As Orbelink.DBHandler.PrimaryKey
        Protected _foreingKeys As System.Collections.ObjectModel.Collection(Of ForeingKey)
        Protected _uniqueKeys As System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)

        Protected MustOverride Sub SetKeysForScripting()

        Public ReadOnly Property Fields() As Orbelink.DBHandler.FieldCollection
            Get
                Return _fields
            End Get
        End Property

        Public MustOverride ReadOnly Property TableName() As String

        Public Property AsName() As String
            Get
                Return _asName
            End Get
            Set(ByVal value As String)
                _asName = value
            End Set
        End Property

        Public ReadOnly Property IsView() As Boolean
            Get
                Return _isView
            End Get
        End Property

        Public ReadOnly Property PrimaryKey() As Orbelink.DBHandler.PrimaryKey
            Get
                Return _primaryKey
            End Get
        End Property

        Public ReadOnly Property ForeingKeys() As System.Collections.ObjectModel.Collection(Of ForeingKey)
            Get
                Return _foreingKeys
            End Get
        End Property

        Public ReadOnly Property UniqueKeys() As System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
            Get
                Return _uniqueKeys
            End Get
        End Property

        MustOverride Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable

    End Class

End Namespace
