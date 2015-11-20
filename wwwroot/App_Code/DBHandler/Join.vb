Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que maneja las condiciones del where
    ''' </summary>
    ''' <version>5.1.1</version>
    ''' <date>02/28/2008</date>
    ''' <remarks></remarks>
    Public Class Join
        Dim lasCapsulas As System.Collections.Generic.List(Of JoinCapsule)

        'Propiedades estaticas
        Public Const classIdentifier As String = "Orbelink.DBHandler.Where Error: "

        Public Enum JoinTypes As Integer
            INNER_JOIN = 0
            LEFT_OUTER_JOIN = 1
            RIGHT_OUTER_JOIN = 2
        End Enum

        Public Structure JoinCapsule
            Dim fieldA As Field
            Dim fieldB As Field
            Dim joinType As JoinTypes
            Dim relation As String
            Dim continuacion As Boolean
        End Structure

        Sub New()
            lasCapsulas = New System.Collections.Generic.List(Of JoinCapsule)
        End Sub

        Public ReadOnly Property Capsules() As System.Collections.Generic.List(Of JoinCapsule)
            Get
                Return lasCapsulas
            End Get
        End Property

        Private Sub agregarCapsula(ByVal theCapsule As JoinCapsule)
            Dim insertado As Boolean = False
            For counter As Integer = 0 To lasCapsulas.Count - 1
                Dim capsula As JoinCapsule = lasCapsulas(counter)
                If capsula.fieldA.ParentTable.Equals(theCapsule.fieldA.ParentTable) And capsula.fieldB.ParentTable.Equals(theCapsule.fieldB.ParentTable) Then
                    theCapsule.continuacion = True
                    lasCapsulas.Insert(counter + 1, theCapsule)
                    insertado = True
                    Exit For
                End If
            Next

            If Not insertado Then
                theCapsule.continuacion = False
                lasCapsulas.Add(theCapsule)
            End If
        End Sub

        'Con Fields
        Public Overloads Sub EqualCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = "= "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

        Public Overloads Sub DiferentCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = "<> "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

        Public Overloads Sub GreaterThanCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = "> "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

        Public Overloads Sub LessThanCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = "< "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

        Public Overloads Sub GreaterThanOrEqualCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = ">= "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

        Public Overloads Sub LessThanOrEqualCondition(ByVal theFieldA As Orbelink.DBHandler.Field, ByVal theFieldB As Orbelink.DBHandler.Field, Optional ByVal theJoinType As JoinTypes = JoinTypes.INNER_JOIN)
            Dim joinCapsule As New JoinCapsule
            joinCapsule.relation = "<= "
            joinCapsule.fieldA = theFieldA
            joinCapsule.fieldB = theFieldB
            joinCapsule.joinType = theJoinType
            agregarCapsula(joinCapsule)
        End Sub

    End Class
End Namespace