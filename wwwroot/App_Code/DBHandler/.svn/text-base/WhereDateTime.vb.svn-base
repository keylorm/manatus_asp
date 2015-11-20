Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que maneja las condiciones del where
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class WhereDateTime
        Inherits Where

        Public Sub New(ByRef field As Orbelink.DBHandler.Field)
            MyBase.New(field)
        End Sub
       

        ''' <summary>
        ''' Condicion de SQL 'Like'
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Function LikeCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "LIKE '%" & QueryBuilder.DateForSQL(theCondition) & "%' ", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        ''' <summary>
        ''' Condicion de SQL 'Like'
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Sub LikeConditionBegin(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_)
            Dim condition As New Condition(Me, "LIKE '" & QueryBuilder.DateForSQL(theCondition) & "%' ", theRelation)
            _conditions.Add(condition)
        End Sub

        ''' <summary>
        ''' Condicion de SQL 'Like' que no diferencia las tildes
        ''' </summary>
        ''' <param name="theCondition">La condicion de like</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Function LikeIndiferentCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "LIKE '%" & QueryBuilder.DateForSQL(theCondition) & "%' ", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        ''' <summary>
        ''' Condicion de SQL '='
        ''' </summary>
        ''' <param name="theCondition">La condicion de igual</param>
        ''' <param name="theRelation">La relacion con la condicion anterior (AND / OR)</param>
        ''' <remarks></remarks>
        Public Overloads Function EqualCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "= '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function DiferentCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "<> '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function GreaterThanCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "> '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function LessThanCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "< '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function GreaterThanOrEqualCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, ">= '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function LessThanOrEqualCondition(ByVal theCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "<= '" & QueryBuilder.DateForSQL(theCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

        Public Overloads Function BetweenCondition(ByVal firstCondition As Date, ByVal secondCondition As Date, Optional ByVal theRelation As FieldRelations = FieldRelations.AND_) As Condition
            Dim condition As New Condition(Me, "BETWEEN '" & QueryBuilder.DateForSQL(firstCondition) & "'" & " AND '" & QueryBuilder.DateForSQL(secondCondition) & "'", theRelation)
            _conditions.Add(condition)
            Return condition
        End Function

    End Class
End Namespace