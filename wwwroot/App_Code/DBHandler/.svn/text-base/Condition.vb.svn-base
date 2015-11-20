Imports Microsoft.VisualBasic

Namespace Orbelink.DBHandler
    Public Class Condition

        Dim _parentWhere As Where
        Dim _condition As String
        Dim _relation As Where.FieldRelations

        Friend Sub New(ByRef theParentWhere As Where, ByVal theCondition As String, ByVal theRelation As Where.FieldRelations)
            _parentWhere = theParentWhere
            _condition = theCondition
            _relation = theRelation
        End Sub

        Public ReadOnly Property ParentWhere() As Where
            Get
                Return _parentWhere
            End Get
        End Property

        Public ReadOnly Property Condition() As String
            Get
                Return _condition
            End Get
        End Property

        Public ReadOnly Property Relation() As Where.FieldRelations
            Get
                Return _relation
            End Get
        End Property

    End Class

End Namespace