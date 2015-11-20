Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que arma los querys para crear tablas
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class ScriptBuilder

        Public Const classIdentifier As String = "Orbelink.DBHandler.ScriptBuilder"

        Dim _ForeingKeys As System.Collections.Generic.List(Of String)
        Dim _DBTablesCollection As System.Collections.Generic.List(Of Orbelink.DBHandler.DBTable)
        Dim output As StreamWriter

        ''' <summary>
        ''' QueryBuilder Constructor
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal theOutputFilePath As String)
            _ForeingKeys = New System.Collections.Generic.List(Of String)
            _DBTablesCollection = New System.Collections.Generic.List(Of Orbelink.DBHandler.DBTable)
            output = New StreamWriter(theOutputFilePath, False)
        End Sub

        Public Sub Clear()
            _ForeingKeys.Clear()
            _DBTablesCollection.Clear()
        End Sub

        ''' <summary>
        ''' Propiedad que contine instancia de DBTables para generarles el script
        ''' </summary>
        ''' <value></value>
        ''' <returns>El arreglo que contiene las tablas instacias</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DBTablesCollection() As System.Collections.Generic.List(Of Orbelink.DBHandler.DBTable)
            Get
                Return _DBTablesCollection
            End Get
        End Property

        'Funciones
        ''' <summary>
        ''' Metodo que genera el select query, tomando los campos que 
        ''' contienen el SelectArray y el WhereArray
        ''' </summary>
        ''' <remarks>El selectArray debe tener al menos alguna instancia de DBTable. 
        ''' Ademas elimina del arrreglo los elementos una vez creado el query.</remarks>
        Public Function GenerateScript() As Boolean
            Dim todoBien As Boolean = True

            For Each theDBTable As DBTable In _DBTablesCollection
                Dim theScript As String = ""

                Dim ifSelectQuery As String = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='" & theDBTable.TableName & "'"
                theScript &= "/*************/" & System.Environment.NewLine
                theScript &= "IF NOT EXISTS(" & ifSelectQuery & ")" & System.Environment.NewLine
                theScript &= "BEGIN" & System.Environment.NewLine
                theScript &= "        " & BuildCreateScript(theDBTable) & System.Environment.NewLine
                theScript &= "END" & System.Environment.NewLine

                output.WriteLine(theScript)
            Next

            For Each script As String In _ForeingKeys
                If script.Length > 0 Then
                    Try
                        output.WriteLine(script)
                    Catch ex As Exception
                        todoBien = False
                    End Try
                End If
            Next

            Clear()
            output.Close()
            Return todoBien
        End Function

        Private Function BuildCreateScript(ByRef theDBTable As Orbelink.DBHandler.DBTable) As String
            Dim sqlQuery As String = ""
            sqlQuery = "CREATE TABLE [" & theDBTable.TableName & "]("

            'Los campos de la base de datos

            For counter As Integer = 0 To theDBTable.Fields.Count - 1
                Dim theField As Orbelink.DBHandler.Field = theDBTable.Fields(counter)

                If counter > 0 Then
                    sqlQuery &= " ,"
                End If

                If theField.Type = Field.FieldType.VARCHAR_MULTILANG Or theField.Type = Field.FieldType.TEXT_MULTILANG Then

                    'Ciclo sobre todos los lenguages posibles
                    For sufixCounter As Integer = 0 To LanguageHandler.UsedLanguagesSufix.Length - 1
                        Dim sufix As String = LanguageHandler.UsedLanguagesSufix(sufixCounter)

                        If sufixCounter > 0 Then
                            sqlQuery &= " ,"
                        End If

                        sqlQuery &= System.Environment.NewLine

                        sqlQuery &= "[" & theField.RealName & sufix & "]"
                        sqlQuery &= " " & FieldTypeToString(theField)

                        If Not theField.AllowNulls Then
                            sqlQuery &= " NOT NULL"
                        End If

                    Next
                Else
                    sqlQuery &= System.Environment.NewLine
                    sqlQuery &= "[" & theField.Name & "]"
                    sqlQuery &= " " & FieldTypeToString(theField)

                    If theField.Identity Then
                        sqlQuery &= " IDENTITY(1, 1)"
                    End If

                    If Not theField.AllowNulls Then
                        sqlQuery &= " NOT NULL"
                    End If

                    Dim theDefaultValue As String = theField.RenderDefaultValue
                    If theDefaultValue IsNot Nothing Then
                        If theDefaultValue.Length > 0 Then
                            sqlQuery &= " DEFAULT (" & theDefaultValue & ")"
                        End If
                    End If
                End If
            Next

            sqlQuery &= PrimaryKeyToString(theDBTable)
            sqlQuery &= UniqueKeysToString(theDBTable)

            _ForeingKeys.Add(BuildForeingKeys(theDBTable))

            sqlQuery &= System.Environment.NewLine & "); "
            Return sqlQuery
        End Function

        Private Function FieldTypeToString(ByVal theField As Orbelink.DBHandler.Field) As String
            Dim sqlQuery As String = ""

            Select Case theField.Type
                Case Field.FieldType.VARCHAR
                    Dim theStringField As FieldString = theField
                    If theStringField.Length <= 0 Then
                        Throw New Exception("Error on " & classIdentifier & ": " & theField.RealName & " can't have this length")
                    End If
                    sqlQuery = "[varchar](" & theStringField.Length & ")"
                Case Field.FieldType.VARCHAR_MULTILANG
                    Dim theStringField As FieldString = theField
                    If theStringField.Length <= 0 Then
                        Throw New Exception("Error on " & classIdentifier & ": " & theField.RealName & " can't have this length")
                    End If
                    sqlQuery = "[varchar](" & theStringField.Length & ")"
                Case Field.FieldType.TEXT
                    sqlQuery = "[text]"
                Case Field.FieldType.TEXT_MULTILANG
                    sqlQuery = "[text]"
                Case Field.FieldType.INT
                    sqlQuery = "[int]"
                Case Field.FieldType.FLOAT
                    sqlQuery = "[float]"
                Case Field.FieldType.DATETIME
                    sqlQuery = "[datetime]"
                Case Field.FieldType.TINYINT
                    sqlQuery = "[tinyint]"
            End Select

            Return sqlQuery
        End Function

        Private Function PrimaryKeyToString(ByVal theDBTable As Orbelink.DBHandler.DBTable) As String
            Dim sqlQuery As String = ""

            If theDBTable.PrimaryKey IsNot Nothing Then
                sqlQuery &= System.Environment.NewLine
                sqlQuery &= ", CONSTRAINT PK_" & theDBTable.TableName & " PRIMARY KEY "
                If theDBTable.PrimaryKey.Clustered Then
                    sqlQuery &= "CLUSTERED "
                Else
                    sqlQuery &= "NONCLUSTERED"
                End If
                sqlQuery &= "("

                'Ciclo sobre los campos de esta llave
                For counter As Integer = 0 To theDBTable.PrimaryKey.Count - 1
                    Dim pkStructrure As Orbelink.DBHandler.PrimaryKey.PrimaryKeyStructrure = theDBTable.PrimaryKey(counter)

                    'Verifica que no sea multilenguaje
                    If pkStructrure.Field.Type = Field.FieldType.VARCHAR_MULTILANG Or pkStructrure.Field.Type = Field.FieldType.TEXT_MULTILANG Then
                        Throw New Exception("Error on " & classIdentifier & ": A primary key field can't allow multilanguage.")
                    End If

                    If counter > 0 Then
                        sqlQuery &= ", "
                    End If

                    sqlQuery &= System.Environment.NewLine
                    sqlQuery &= "[" & pkStructrure.Field.Name & "]"
                    If pkStructrure.Ascendent Then
                        sqlQuery &= " ASC "
                    Else
                        sqlQuery &= " DSC "
                    End If
                Next
                sqlQuery &= System.Environment.NewLine & ") "
            End If

            Return sqlQuery
        End Function

        Private Function UniqueKeysToString(ByVal theDBTable As Orbelink.DBHandler.DBTable) As String
            Dim sqlQuery As String = ""

            If theDBTable.UniqueKeys IsNot Nothing Then
                For counterKeys As Integer = 0 To theDBTable.UniqueKeys.Count - 1
                    Dim theUniqueKey As UniqueKey = theDBTable.UniqueKeys(counterKeys)

                    'Comandos sql para unique key
                    sqlQuery &= System.Environment.NewLine
                    sqlQuery &= ", CONSTRAINT UN_" & theDBTable.TableName & counterKeys & " UNIQUE "
                    If theUniqueKey.Clustered Then
                        sqlQuery &= "CLUSTERED "
                    Else
                        sqlQuery &= "NONCLUSTERED"
                    End If
                    sqlQuery &= "("

                    'Ciclo sobre los campos de esta llave
                    For counter As Integer = 0 To theUniqueKey.Count - 1
                        Dim unkStructrure As Orbelink.DBHandler.UniqueKey.UniqueKeyStructure = theUniqueKey(counter)

                        'Verifica que no sea multilenguaje
                        If unkStructrure.Field.Type = Field.FieldType.VARCHAR_MULTILANG Or unkStructrure.Field.Type = Field.FieldType.TEXT_MULTILANG Then
                            Throw New Exception("Error on " & classIdentifier & ": A unique key field can't allow multilanguage.")
                        End If

                        If counter > 0 Then
                            sqlQuery &= ", "
                        End If

                        sqlQuery &= System.Environment.NewLine
                        sqlQuery &= "[" & unkStructrure.Field.Name & "]"
                        If unkStructrure.Ascendent Then
                            sqlQuery &= " ASC "
                        Else
                            sqlQuery &= " DSC "
                        End If
                    Next
                    sqlQuery &= System.Environment.NewLine & ") "
                Next
            End If

            Return sqlQuery
        End Function

        Private Function BuildForeingKeys(ByVal theDBTable As Orbelink.DBHandler.DBTable) As String
            Dim constraintQuery As String = ""
            Dim referencesQuery As String = ""
            Dim foreingKeyName As String = ""
            Dim alterTableQuery As String = ""
            Dim sqlQuery As String = ""
            Dim deleteRuleQuery As String = ""

            If theDBTable.ForeingKeys IsNot Nothing Then
                For counterKeys As Integer = 0 To theDBTable.ForeingKeys.Count - 1
                    Dim theForeingKey As ForeingKey = theDBTable.ForeingKeys(counterKeys)

                    'Constraint para foreing key
                    foreingKeyName = "FK_" & theDBTable.TableName & "__" & theForeingKey.ReferencesToTable.TableName & counterKeys

                    Dim ifSelectQuery As String = "SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = '" & foreingKeyName & "'"

                    'sqlQuery &= "/*************/" & System.Environment.NewLine
                    sqlQuery &= "IF NOT EXISTS(" & ifSelectQuery & ")" & System.Environment.NewLine
                    sqlQuery &= "BEGIN" & System.Environment.NewLine

                    'Alter table para ejecutarlos al final
                    alterTableQuery = "ALTER TABLE [" & theDBTable.TableName & "]"

                    constraintQuery = "CONSTRAINT [" & foreingKeyName & "] FOREIGN KEY "
                    constraintQuery &= "("

                    'References query
                    referencesQuery = System.Environment.NewLine
                    referencesQuery &= "REFERENCES [" & theForeingKey.ReferencesToTable.TableName & "] "
                    referencesQuery &= "("

                    If theForeingKey.OnDeleteRule = ForeingKey.DeleteRule.CASCADE Then
                        deleteRuleQuery = "ON DELETE CASCADE"
                    ElseIf theForeingKey.OnDeleteRule = ForeingKey.DeleteRule.SET_NULL Then
                        deleteRuleQuery = "ON DELETE SET NULL"
                    ElseIf theForeingKey.OnDeleteRule = ForeingKey.DeleteRule.SET_DEFAULT Then
                        deleteRuleQuery = "ON DELETE SET DEFAULT"
                    Else
                        deleteRuleQuery = ""
                    End If

                    'Ciclo sobre los campos de esta llave
                    For counter As Integer = 0 To theForeingKey.Count - 1
                        Dim fkStructrure As Orbelink.DBHandler.ForeingKey.ForeingKeyStructure = theForeingKey(counter)

                        'Verifica que no sea multilenguaje
                        If fkStructrure.Field.Type = Field.FieldType.VARCHAR_MULTILANG Or fkStructrure.Field.Type = Field.FieldType.TEXT_MULTILANG Then
                            Throw New Exception("Error on " & classIdentifier & ": A foreing key field can't allow multilanguage.")
                        End If

                        If counter > 0 Then
                            constraintQuery &= ", "
                            referencesQuery &= ", "
                        End If

                        constraintQuery &= "[" & fkStructrure.Field.Name & "]"
                        referencesQuery &= "[" & fkStructrure.ReferencesTo.Name & "]"

                    Next
                    constraintQuery &= ") "
                    referencesQuery &= ") "

                    sqlQuery &= alterTableQuery & " WITH CHECK ADD " & constraintQuery
                    sqlQuery &= referencesQuery
                    sqlQuery &= System.Environment.NewLine & deleteRuleQuery & System.Environment.NewLine
                    sqlQuery &= alterTableQuery & " CHECK CONSTRAINT [" & foreingKeyName & "]" & System.Environment.NewLine

                    sqlQuery &= "END" & System.Environment.NewLine
                    'sqlQuery &= "GO" & System.Environment.NewLine
                    sqlQuery &= System.Environment.NewLine & System.Environment.NewLine

                Next
            End If

            Return sqlQuery
        End Function
    End Class

End Namespace