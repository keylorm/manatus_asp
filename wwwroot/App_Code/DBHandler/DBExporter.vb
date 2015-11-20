Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Collections.Generic

Namespace Orbelink.DBHandler
    Public Class DBExporter

        Dim baseConnection As Orbelink.DBHandler.SQLServer
        Dim newConnection As Orbelink.DBHandler.SQLServer
        Dim FromPreviousMultilang As Boolean
        Dim replacePrefixes As Boolean

        Private _esteContador As Integer
        Private _esteLimite As Integer

        Public ReadOnly Property GetEstado() As String
            Get
                Return _esteContador & " / " & _esteLimite
            End Get
        End Property


        Sub New(ByRef theBaseConnection As Orbelink.DBHandler.SQLServer, ByVal theNewConnection As Orbelink.DBHandler.SQLServer, ByVal theFromPreviousMultilang As Boolean, ByVal thereplacePrefixes As Boolean)
            baseConnection = theBaseConnection
            newConnection = theNewConnection
            FromPreviousMultilang = theFromPreviousMultilang
            replacePrefixes = thereplacePrefixes
        End Sub

        Public Function ExportDBTable(ByRef theDBTable As Orbelink.DBHandler.DBTable, Optional ByVal theReplaceFieldNames As NameValueCollection = Nothing) As String
            Return ExportDBTable(theDBTable, Nothing, theReplaceFieldNames)
        End Function

        Public Function ExportDBTable(ByRef theDBTable As Orbelink.DBHandler.DBTable, ByVal theDBTablePrefix As String, Optional ByVal theReplaceFieldNames As NameValueCollection = Nothing) As String
            'Habilita para capturar los errores de tablas enexistentes
            baseConnection.catchNoTable = False

            Dim resultado As New StringBuilder
            Dim querybuilder As New QueryBuilder()
            querybuilder.IdentityEnabled = False

            resultado.Append(theDBTable.TableName)
            resultado.AppendLine()

            'Hace el select de la tabla origen
            Dim theSelectQuery As String = querybuilder.SelectQuery(theDBTable)
            theSelectQuery = theSelectQuery.Replace(",", " ,")
            theSelectQuery = theSelectQuery.Replace(" FROM", "  FROM")

            'Si tiene version antigual de multilang
            If FromPreviousMultilang Then
                theSelectQuery = theSelectQuery.Replace(Orbelink.DBHandler.LanguageHandler.CurrentLanguageSufix & " ", "")
            End If

            'Si reemplaza los prefijos
            If theDBTablePrefix IsNot Nothing Then
                If theDBTablePrefix.Length > 0 And replacePrefixes Then
                    theSelectQuery = theSelectQuery.Replace(theDBTablePrefix, "")
                End If
            End If

            'Reemplaza campos nuevos por viejos
            If theReplaceFieldNames IsNot Nothing Then
                For counterReplace As Integer = 0 To theReplaceFieldNames.Count - 1
                    Dim llave As String = theReplaceFieldNames.GetKey(counterReplace)
                    Dim valor As String = theReplaceFieldNames.Item(counterReplace)
                    theSelectQuery = theSelectQuery.ToLower.Replace(valor.ToLower, llave.ToLower)
                Next
            End If

            'Hace la consulta
            'Try
            Dim resultados As Data.DataTable = baseConnection.executeSelect_DT(theSelectQuery)
            Dim contadorLocal As Integer = 0

            'Revisa lo devuelto por la consulta a la tabla origen
            If resultados.Rows.Count > 0 Then
                'Enciende para permitir ingresar valores en un identity
                Dim haveIdentityProperty As Boolean = verifyIfHaveIdentityProperty(theDBTable)

                'Busca cuales valores sobreescribir
                Dim indicesFields As New List(Of Integer)
                For counterFields As Integer = 0 To theDBTable.Fields.Count - 1
                    Dim elField As Field = theDBTable.Fields(counterFields)
                    If elField.IsValid Then
                        indicesFields.Add(counterFields)
                    End If
                Next

                'Ciclo para todos los inserts
                For counter As Integer = 0 To resultados.Rows.Count - 1
                    Dim actual As DBTable = Orbelink.DBHandler.ObjectBuilder.CreateDBTable_BySelectIndex(resultados.Rows(counter), theDBTable)

                    Dim comando As New StringBuilder()
                    If haveIdentityProperty Then
                        comando.Append("SET IDENTITY_INSERT ")
                        comando.Append(actual.TableName)
                        comando.AppendLine(" ON")
                    End If

                    'Sobreescribe los valores
                    For Each indice As Integer In indicesFields
                        Dim elField As Field = theDBTable.Fields(indice)
                        actual.Fields(indice).SetValue = elField.RenderValue
                    Next

                    'Hace las consultas
                    Dim ifSelectQuery As String = querybuilder.SelectQuery(buildSelectWithPK(actual))
                    Dim insertQuery As String = querybuilder.InsertQuery(actual)

                    'Reemplaza campos viejos por nuevos
                    If theReplaceFieldNames IsNot Nothing Then
                        For counterReplace As Integer = 0 To theReplaceFieldNames.Count - 1
                            Dim llave As String = theReplaceFieldNames.GetKey(counterReplace)
                            Dim valor As String = theReplaceFieldNames.Item(counterReplace)
                            ifSelectQuery = ifSelectQuery.Replace(llave, valor)
                            insertQuery = insertQuery.Replace(llave, valor)
                        Next
                    End If

                    'Try
                    'Quita los tags necesarios.
                    'If FromPreviousMultilang Then
                    '    insertQuery = RemoveTags(insertQuery)
                    'End If

                    comando.Append("IF NOT EXISTS(")
                    comando.Append(ifSelectQuery)
                    comando.AppendLine(")")
                    comando.AppendLine("BEGIN")
                    comando.AppendLine(insertQuery)
                    comando.AppendLine("END")

                    If haveIdentityProperty Then
                        comando.Append("SET IDENTITY_INSERT ")
                        comando.Append(actual.TableName)
                        comando.AppendLine(" OFF")
                    End If

                    Try
                        Dim rowAffected As Integer = newConnection.executeCommand(comando.ToString)
                        _esteContador += rowAffected
                        If rowAffected > 0 Then
                            contadorLocal += rowAffected
                        End If
                    Catch ex As DBException
                        resultado.AppendLine("(--- Error con consulta: ")
                        resultado.AppendLine(ex.Query)
                        resultado.AppendLine("---)")
                    End Try
                Next
            End If
            resultado.AppendLine(contadorLocal & " / " & resultados.Rows.Count)
            resultado.AppendLine("----------------")
            baseConnection.catchNoTable = True
            Return resultado.ToString
        End Function

        Public Function InsertOnDBTable(ByRef theDBTable As Orbelink.DBHandler.DBTable) As String
            Dim resultado As New StringBuilder
            Dim querybuilder As New QueryBuilder()
            querybuilder.IdentityEnabled = False

            resultado.Append(theDBTable.TableName)
            resultado.AppendLine()

            'textWriter.WriteLine("/**************" & theDBTable.TableName & "*************/")

            'Enciende para permitir ingresar valores en un identity
            Dim haveIdentityProperty As Boolean = verifyIfHaveIdentityProperty(theDBTable)
            If haveIdentityProperty Then
                newConnection.executeCommand("SET IDENTITY_INSERT " & theDBTable.TableName & " ON")
            End If

            'Hace las consultas
            Dim ifSelectQuery As String = querybuilder.SelectQuery(buildSelectWithPK(theDBTable))
            Dim insertQuery As String = querybuilder.InsertQuery(theDBTable)

            Try
                Dim comando As New StringBuilder()
                comando.Append("IF NOT EXISTS(")
                comando.Append(ifSelectQuery)
                comando.AppendLine(")")
                comando.AppendLine("BEGIN")
                comando.AppendLine(insertQuery)
                comando.AppendLine("END")
                newConnection.executeCommand(comando.ToString)
            Catch ex As Exception
                resultado.Append("/** Error with this insert **/" & System.Environment.NewLine)
                resultado.Append(insertQuery & System.Environment.NewLine)
                resultado.Append("/** Error End **/" & System.Environment.NewLine)
            End Try

            'Appaga para permitir ingresar valores en un identity
            If haveIdentityProperty Then
                newConnection.executeCommand("SET IDENTITY_INSERT " & theDBTable.TableName & " OFF")
            End If

            resultado.Append("----------------" & System.Environment.NewLine)
            querybuilder.IdentityEnabled = True

            Return resultado.ToString
        End Function

        'Protected Function RemoveTags(ByVal theInsertQuery As String) As String
        '    theInsertQuery = theInsertQuery.Replace(" ,", ",")
        '    theInsertQuery = theInsertQuery.Replace(" )", ")")
        '    Dim primerAbreParentesis As Integer = theInsertQuery.IndexOf("(")
        '    Dim segundoAbreParentesis As Integer = theInsertQuery.IndexOf("(", primerAbreParentesis + 1)
        '    Dim primerCierraParentesis As Integer = theInsertQuery.IndexOf(")")
        '    Dim segundoCierraParentesis As Integer = theInsertQuery.LastIndexOf(")")

        '    Dim preInsertPartQuery As String = theInsertQuery.Substring(0, primerAbreParentesis)
        '    Dim insertPartQuery As String = theInsertQuery.Substring(primerAbreParentesis + 1, primerCierraParentesis - primerAbreParentesis - 1)
        '    Dim preValuesPartQuery As String = theInsertQuery.Substring(primerCierraParentesis + 1, segundoAbreParentesis - primerCierraParentesis - 1)
        '    Dim valuesPartQuery As String = theInsertQuery.Substring(segundoAbreParentesis + 1, segundoCierraParentesis - segundoAbreParentesis - 1)

        '    Dim insertPartSplitted As String() = insertPartQuery.Split(",")
        '    'Hacer mi propio split
        '    Dim valuesPartSplitted() As String = splitParaValues(valuesPartQuery)

        '    Dim idiomasRecorridos As Integer = 0
        '    'Dim valoresMultiLang As New System.Collections.Generic.List(Of ValorConMultiLang)
        '    Dim allTagsString As String = StripLanguageHandler.AddTags("", StripLanguageHandler.Tag.Espanol) & StripLanguageHandler.AddTags("", StripLanguageHandler.Tag.Ingles)
        '    Dim allTagsString2 As String = StripLanguageHandler.AddTags("", StripLanguageHandler.Tag.Ingles) & StripLanguageHandler.AddTags("", StripLanguageHandler.Tag.Espanol)

        '    For counter As Integer = 0 To insertPartSplitted.Length - 1
        '        Dim insertQueryActual As String = insertPartSplitted(counter)
        '        Dim valueQueryActual As String = valuesPartSplitted(counter)
        '        Dim esMultilaLang As Boolean = False
        '        Dim stringTemp As String = Nothing

        '        'Buscar si tiene idioma y cual seria.
        '        If insertQueryActual.Length > 3 Then
        '            Dim lastTreeChars As String = insertQueryActual.Substring(insertQueryActual.Length - 3, 3)
        '            If lastTreeChars = LanguageHandler.UsedLanguagesSufix(idiomasRecorridos) Then
        '                esMultilaLang = True

        '                'Hace el stripLanguage
        '                If LanguageHandler.UsedLanguages(idiomasRecorridos) = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL Then
        '                    stringTemp = StripLanguageHandler.StripLanguage(valueQueryActual, StripLanguageHandler.Tag.Espanol, StripLanguageHandler.StripType.Idioma)
        '                ElseIf LanguageHandler.UsedLanguages(idiomasRecorridos) = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
        '                    stringTemp = StripLanguageHandler.StripLanguage(valueQueryActual, StripLanguageHandler.Tag.Ingles, StripLanguageHandler.StripType.Idioma)
        '                End If

        '                If idiomasRecorridos < LanguageHandler.UsedLanguagesSufix.Length - 1 Then
        '                    idiomasRecorridos += 1
        '                Else
        '                    idiomasRecorridos = 0
        '                End If
        '            End If
        '        End If

        '        If Not esMultilaLang Then
        '            'Este campo no tiene multilanguage, deberia quitarse los tags
        '            stringTemp = StripLanguageHandler.StripLanguage(valueQueryActual, StripLanguageHandler.Tag.Espanol, StripLanguageHandler.StripType.Idioma)
        '        End If

        '        valueQueryActual = valueQueryActual.Replace(allTagsString, "")
        '        valueQueryActual = valueQueryActual.Replace(allTagsString2, "")

        '        If stringTemp IsNot Nothing Then
        '            If stringTemp.Length > 0 Then
        '                'Actualiza el valuesPartSplitted, Este campo es multilanguage
        '                valueQueryActual = "'" & stringTemp & "'"
        '            End If
        '        End If
        '        valuesPartSplitted(counter) = valueQueryActual
        '    Next

        '    'Genera el script para devolver
        '    Dim newQuery As String = ""
        '    newQuery &= preInsertPartQuery & "("
        '    For counter As Integer = 0 To insertPartSplitted.Length - 1
        '        newQuery &= insertPartSplitted(counter)
        '        If counter <> insertPartSplitted.Length - 1 Then
        '            newQuery &= ", "
        '        End If
        '    Next
        '    newQuery &= ")" & preValuesPartQuery & "("
        '    For counter As Integer = 0 To valuesPartSplitted.Length - 1
        '        newQuery &= valuesPartSplitted(counter) & " "
        '        If counter <> insertPartSplitted.Length - 1 Then
        '            newQuery &= ", "
        '        End If
        '    Next
        '    newQuery &= ")"

        '    Return newQuery
        'End Function

        'Protected Function splitParaValues(ByVal theValuesQuery As String) As String()
        '    Dim lastIndex As Integer = 0
        '    Dim sliptReal As String()
        '    Dim splitArray As New ArrayList

        '    Dim splitTemporal As String() = theValuesQuery.Split(",")

        '    Dim concating As String = ""
        '    Dim cantidadTags As Integer = 0
        '    Dim accarreaString As Boolean = False

        '    For counter As Integer = 0 To splitTemporal.Length - 1
        '        Dim actual As String = splitTemporal(counter)
        '        Dim primerCaracterValido As Boolean = True

        '        For counterChar As Integer = 0 To actual.Length - 1
        '            Dim charActual As Char = actual.Chars(counterChar)
        '            If charActual = "["c Then
        '                cantidadTags += 1
        '            End If

        '            'Verificador por si tiene comas en el medio.
        '            If charActual <> " "c And primerCaracterValido Then
        '                If charActual = "'"c Then
        '                    accarreaString = True
        '                Else
        '                    Try
        '                        Dim tempInt As Integer = CType(CType(charActual, String), Integer)
        '                    Catch ex As Exception
        '                        'accarreaString = True
        '                    End Try
        '                End If

        '                primerCaracterValido = False
        '            End If

        '            If counterChar = actual.Length - 1 Then
        '                If charActual = "'"c Then
        '                    accarreaString = False
        '                End If
        '            End If
        '        Next

        '        concating &= actual
        '        If cantidadTags Mod 2 = 0 And Not accarreaString Then
        '            splitArray.Add(concating)
        '            concating = ""
        '            cantidadTags = 0
        '        Else
        '            concating &= ","
        '        End If
        '    Next

        '    'Crear el arreglo de strings
        '    Dim splitRealTemp(splitArray.Count - 1) As String
        '    sliptReal = splitRealTemp
        '    For counter As Integer = 0 To splitArray.Count - 1
        '        sliptReal(counter) = splitArray(counter)
        '    Next

        '    Return sliptReal
        'End Function

        Protected Function buildSelectWithPK(ByRef theDBTable As Orbelink.DBHandler.DBTable) As Orbelink.DBHandler.DBTable
            Dim tempDBTable As Orbelink.DBHandler.DBTable = theDBTable.CreateInstance(True)

            For counter As Integer = 0 To tempDBTable.PrimaryKey.Count - 1
                Dim theField As Orbelink.DBHandler.Field = tempDBTable.PrimaryKey(counter).Field
                Dim theBaseField As Orbelink.DBHandler.Field = Nothing

                For Each field As Orbelink.DBHandler.Field In theDBTable.Fields
                    If theField.Name = field.Name Then
                        theBaseField = field
                        Exit For
                    End If
                Next

                If theBaseField IsNot Nothing Then
                    theField.ToSelect = True
                    Dim condition As New Condition(theField.WherePrivate, " = '" & theBaseField.RenderValue & "' ", Where.FieldRelations.AND_)
                    theField.WherePrivate.Conditions.Add(condition)
                Else
                    Throw New Exception("No se encuentra el field")
                End If

            Next

            Return tempDBTable
        End Function

        Protected Function verifyIfHaveIdentityProperty(ByRef theDBTable As Orbelink.DBHandler.DBTable) As Boolean
            Dim haveIdentity As Boolean = False
            For Each theField As Field In theDBTable.Fields
                If theField.Identity Then
                    haveIdentity = True
                    Exit For
                End If
            Next

            Return haveIdentity
        End Function
    End Class
End Namespace