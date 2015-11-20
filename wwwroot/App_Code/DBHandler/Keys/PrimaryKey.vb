Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Una llave principal
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class PrimaryKey
        Inherits System.Collections.ObjectModel.Collection(Of PrimaryKeyStructrure)

        Dim _Clustered As Boolean

        Public Structure PrimaryKeyStructrure
            Dim Field As Field
            Dim Ascendent As Boolean
        End Structure

        Public Sub New(ByVal First_PKField As Field, ByVal Asc As Boolean, Optional ByVal clustered As Boolean = True)
            MyBase.New()
            _Clustered = clustered
            Dim FirstPrimaryKeyStructrure As PrimaryKeyStructrure
            FirstPrimaryKeyStructrure.Field = First_PKField
            FirstPrimaryKeyStructrure.Ascendent = Asc
            MyBase.Add(FirstPrimaryKeyStructrure)
        End Sub

        Public Sub Add_PK(ByVal PKField As Field, ByVal Asc As Boolean)
            Dim thePK As PrimaryKeyStructrure
            thePK.Field = PKField
            thePK.Ascendent = Asc
            MyBase.Add(thePK)
        End Sub

        Public ReadOnly Property Clustered() As Boolean
            Get
                Return _Clustered
            End Get
        End Property
    End Class

End Namespace
