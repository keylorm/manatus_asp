Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que maneja las condiciones del where
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public MustInherit Class Where

        Protected _field As Orbelink.DBHandler.Field
        Protected _conditions As System.Collections.Generic.List(Of Condition)

        'Propiedades estaticas
        Public Const classIdentifier As String = "Orbelink.DBHandler.Where Error: "

        Public Enum FieldRelations As Integer
            AND_ = 0
            OR_ = 1
        End Enum

        Public Sub New(ByRef field As Orbelink.DBHandler.Field)
            _conditions = New System.Collections.Generic.List(Of Condition)
            _field = field
        End Sub

        Public ReadOnly Property Conditions() As System.Collections.Generic.List(Of Condition)
            Get
                Return _conditions
            End Get
        End Property

        Friend ReadOnly Property Field() As Orbelink.DBHandler.Field
            Get
                Return _field
            End Get
        End Property

        Public Function InCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                condition = New Condition(Me, "IN (" & theCondition & ") ", theRelation)
                _conditions.Add(condition)
            End If
            Return condition
        End Function

        Public Function NotInCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                condition = New Condition(Me, "NOT IN (" & theCondition & ") ", theRelation)
                _conditions.Add(condition)
            End If
            Return condition
        End Function

        Public Function IsNullCondition(Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "IS NULL ", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Function IsNotNullCondition(Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "IS NOT NULL ", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        'Con Fields
        Public Overloads Function EqualCondition_OnSameTable(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            If Me.Field.ParentTable.Equals(theField.ParentTable) Then
                Dim condition As New Condition(Me, "= " & theField.ParentTable.TableName & "." & theField.Name, theRelation)
                _conditions.Add(condition)
                Return condition
            Else
                Throw New Exception(classIdentifier & "Parent tables not the same.")
            End If
        End Function

        Public Overloads Function DiferentCondition_OnSameTable(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            If Me.Field.ParentTable.Equals(theField.ParentTable) Then
                Dim condition As New Condition(Me, "<> " & theField.ParentTable.TableName & "." & theField.Name, theRelation)
                _conditions.Add(condition)
                Return condition
            Else
                Throw New Exception(classIdentifier & "Parent tables not the same.")
            End If
        End Function

        Public Overloads Function EqualCondition_OuterScript(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "= " & theField.ParentTable.TableName & "." & theField.Name, theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function DiferentCondition_OuterScript(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "<> " & theField.ParentTable.TableName & "." & theField.Name, theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Sub Clear()
            _conditions.Clear()
        End Sub

    End Class
End Namespace