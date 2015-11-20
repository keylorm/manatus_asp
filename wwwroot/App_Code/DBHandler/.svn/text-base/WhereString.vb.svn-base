Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que maneja las condiciones del where
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class WhereString
        Inherits Where

        Public Sub New(ByRef field As Orbelink.DBHandler.Field)
            MyBase.New(field)
        End Sub

        ''' <summary>
        ''' Condicion de SQL '='
        ''' </summary>
        ''' <param name="theCondition">La condicion de igual</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Function EqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                If _field.Type = Field.FieldType.INT Then
                    condition = New Condition(Me, "= " & theCondition, theRelation)
                Else
                    condition = New Condition(Me, "= '" & theCondition.Replace("'", "''") & "'", theRelation)
                End If
                _conditions.Add(Condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return Condition
        End Function

        Public Overloads Function DiferentCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "DiferentCondition: " & theCondition & ": "
            Dim condition As Condition
            If _field.Type = Field.FieldType.INT Then
                Dim theConditionInteger As Integer
                Try
                    theConditionInteger = CType(theCondition, Integer)
                    condition = New Condition(Me, "<> " & theCondition, theRelation)
                Catch ex As Exception
                    Throw New Exception(excID & "Not a valid integer condition.")
                End Try
            Else
                condition = New Condition(Me, "<> '" & theCondition & "'", theRelation)
            End If
            _conditions.Add(condition)
            Return condition
        End Function

        Public Function GreaterThanCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            Dim condition As Condition
            If theCondition.Length > 0 Then
                If _field.Type = Field.FieldType.INT Then
                    condition = New Condition(Me, "> " & theCondition, theRelation)
                Else
                    condition = New Condition(Me, "> '" & theCondition.Replace("'", "''") & "'", theRelation)
                End If
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return Condition
        End Function

        Public Function LessThanCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            Dim condition As Condition
            If theCondition.Length > 0 Then
                If _field.Type = Field.FieldType.INT Then
                    condition = New Condition(Me, "< " & theCondition, theRelation)
                Else
                    condition = New Condition(Me, "< '" & theCondition.Replace("'", "''") & "'", theRelation)
                End If
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return condition
        End Function

        Public Function GreaterThanOrEqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                If _field.Type = Field.FieldType.INT Then
                    condition = New Condition(Me, ">= " & theCondition, theRelation)
                Else
                    condition = New Condition(Me, ">= '" & theCondition.Replace("'", "''") & "'", theRelation)
                End If
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return condition
        End Function

        Public Function LessThanOrEqualCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "EqualCondition: " & theCondition & ": "
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                If _field.Type = Field.FieldType.INT Then
                    condition = New Condition(Me, "<= " & theCondition, theRelation)
                Else
                    condition = New Condition(Me, "<= '" & theCondition.Replace("'", "''") & "'", theRelation)
                End If
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return condition
        End Function

        Public Function BetweenCondition(ByVal firstCondition As String, ByVal secondCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim excID As String = classIdentifier & "BetweenCondition: " & firstCondition & ": "
            Dim condition As Condition = Nothing
            If firstCondition.Length > 0 And secondCondition.Length > 0 Then
                condition = New Condition(Me, "BETWEEN '" & firstCondition.Replace("'", "''") & "'" & " AND '" & secondCondition.Replace("'", "''") & "'", theRelation)
                _conditions.Add(condition)
            Else
                Throw New Exception(excID & "Not a valid condition.")
            End If
            Return Condition
        End Function

        ''' <summary>
        ''' Condicion de SQL 'Like'
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Function LikeCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                condition = New Condition(Me, "LIKE '%" & theCondition.Replace("'", "''") & "%' ", theRelation)
                _conditions.Add(condition)
            End If
            Return Condition
        End Function

        ''' <summary>
        ''' Condicion de SQL 'Like'
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Function LikeConditionBegin(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                condition = New Condition(Me, "LIKE '" & theCondition.Replace("'", "''") & "%' ", theRelation)
                _conditions.Add(condition)
            End If
            Return condition
        End Function

        ''' <summary>
        ''' Condicion de SQL 'Like' que no diferencia las tildes
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Function LikeIndiferentCondition(ByVal theCondition As String, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As Condition = Nothing
            If theCondition.Length > 0 Then
                theCondition = theCondition.Replace("á", "a")
                theCondition = theCondition.Replace("é", "e")
                theCondition = theCondition.Replace("í", "i")
                theCondition = theCondition.Replace("ó", "o")
                theCondition = theCondition.Replace("ú", "u")

                theCondition = theCondition.Replace("a", "[""a"",""á""]")
                theCondition = theCondition.Replace("e", "[""e"",""é""]")
                theCondition = theCondition.Replace("i", "[""i"",""í""]")
                theCondition = theCondition.Replace("o", "[""o"",""ó""]")
                theCondition = theCondition.Replace("u", "[""u"",""ú""]")

                condition = New Condition(Me, "LIKE '%" & theCondition.Replace("'", "''") & "%' ", theRelation)
                _conditions.Add(condition)
            End If
            Return Condition
        End Function
    End Class
End Namespace