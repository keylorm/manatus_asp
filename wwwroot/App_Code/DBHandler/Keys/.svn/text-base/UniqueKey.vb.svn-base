Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Una llave unica
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UniqueKey
        Inherits System.Collections.ObjectModel.Collection(Of UniqueKeyStructure)

        Dim _Clustered As Boolean

        Public Structure UniqueKeyStructure
            Dim Field As Field
            Dim Ascendent As Boolean
        End Structure

        Public Sub New(ByRef UKField As Field, ByVal Ascendent As Boolean, Optional ByVal clustered As Boolean = False)
            MyBase.New()
            _Clustered = clustered
            Dim theUNK As UniqueKeyStructure
            theUNK.Field = UKField
            theUNK.Ascendent = Ascendent
            MyBase.Add(theUNK)
        End Sub

        Public Sub Add_UK(ByRef UKField As Field, ByVal Ascendent As Boolean)
            Dim theUNK As UniqueKeyStructure
            theUNK.Field = UKField
            theUNK.Ascendent = Ascendent
            MyBase.Add(theUNK)
        End Sub

        Public ReadOnly Property Clustered() As Boolean
            Get
                Return _Clustered
            End Get
        End Property
    End Class

End Namespace
