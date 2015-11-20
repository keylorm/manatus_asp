Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que maneja las condiciones del having
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class Having

        Dim _field As Orbelink.DBHandler.Field
        Dim _conditions As ArrayList

        'Propiedades estaticas
        Public Const classIdentifier As String = "Orbelink.DBHandler.Having Error: "

        Public Enum FieldRelations As Integer
            AND_ = 0
            OR_ = 1
        End Enum

        Private Structure Condition
            Dim theCondition As String
            Dim theRelation As Integer
        End Structure

        Public Sub New(ByRef field As Orbelink.DBHandler.Field)
            _conditions = New ArrayList
            _field = field
        End Sub

        Public ReadOnly Property Strings(ByVal index As Integer) As String
            Get
                Return _conditions(index).theCondition
            End Get
        End Property

        Public ReadOnly Property Field() As Orbelink.DBHandler.Field
            Get
                Return _field
            End Get
        End Property

        Public ReadOnly Property Relations(ByVal index As Integer) As String
            Get
                Dim indiceRelation As Integer = _conditions(index).theRelation
                Select Case indiceRelation
                    Case FieldRelations.AND_
                        Return "AND "
                    Case FieldRelations.OR_
                        Return "OR "
                End Select
                Return "AND "
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return _conditions.Count
            End Get
        End Property

        ''' <summary>
        ''' Condicion de SQL '='
        ''' </summary>
        ''' <param name="theCondition">La condicion de igual</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Sub EqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            If theCondition.Length > 0 Then
                Dim condition As New Condition
                If _field.Type = Field.FieldType.INT Then
                    condition.theCondition = "= " & theCondition
                Else
                    condition.theCondition = "= '" & theCondition & "'"
                End If
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        Public Overloads Sub DiferentCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "DiferentCondition: " & theCondition & ": "
            Dim condition As New Condition
            If _field.Type = Field.FieldType.INT Then
                Dim theConditionInteger As Integer
                Try
                    theConditionInteger = CType(theCondition, Integer)
                    condition.theCondition = "<> " & theCondition
                Catch ex As Exception
                    Throw New Exception(excID & "Not a valid integer condition.")
                End Try
            Else
                condition.theCondition = "<> '" & theCondition & "'"
            End If
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub GreaterThanCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            If theCondition.Length > 0 Then
                Dim condition As New Condition
                If _field.Type = Orbelink.DBHandler.Field.FieldType.INT Then
                    condition.theCondition = "> " & theCondition
                Else
                    condition.theCondition = "> '" & theCondition & "'"
                End If
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        Public Overloads Sub LessThanCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            If theCondition.Length > 0 Then
                Dim condition As New Condition
                If _field.Type = Field.FieldType.INT Then
                    condition.theCondition = "< " & theCondition
                Else
                    condition.theCondition = "< '" & theCondition & "'"
                End If
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        Public Overloads Sub GreaterThanOrEqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            If theCondition.Length > 0 Then
                Dim condition As New Condition
                If _field.Type = Field.FieldType.INT Then
                    condition.theCondition = ">= " & theCondition
                Else
                    condition.theCondition = ">= '" & theCondition & "'"
                End If
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        Public Overloads Sub LessThanOrEqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            If theCondition.Length > 0 Then
                Dim condition As New Condition
                If _field.Type = Field.FieldType.INT Then
                    condition.theCondition = "<= " & theCondition
                Else
                    condition.theCondition = "<= '" & theCondition & "'"
                End If
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        Public Overloads Sub BetweenCondition(ByVal firstCondition As String, ByVal secondCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim excID As String = classIdentifier & "BetweenCondition: " & firstCondition & ": "
            If firstCondition.Length > 0 And secondCondition.Length > 0 Then
                Dim condition As New Condition
                condition.theCondition = "BETWEEN '" & firstCondition & "'" & " AND '" & secondCondition & "'"
                condition.theRelation = theRelation
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
        End Sub

        'Con Fields
        Public Overloads Sub EqualCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "= " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub DiferentCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "<> " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub GreaterThanCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "> " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub LessThanCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "< " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub GreaterThanOrEqualCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = ">= " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub LessThanOrEqualCondition(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "<= " & theField.ParentTable.TableName & "." & theField.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Overloads Sub BetweenCondition(ByVal theField1 As Orbelink.DBHandler.Field, ByVal theField2 As Orbelink.DBHandler.Field, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition
            condition.theCondition = "BETWEEN " & theField1.ParentTable.TableName & "." & theField1.Name & " AND " & theField2.ParentTable.TableName & "." & theField2.Name
            condition.theRelation = theRelation
            _conditions.Add(condition)
        End Sub

        Public Sub Clear()
            _conditions.Clear()
        End Sub

    End Class
End Namespace