Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que genera dinamicamente los querys
    ''' </summary>
    ''' <remarks></remarks>
    Public Class QueryBuilder

        Public Const classIdentifier As String = "QueryBuilder"

        Dim _fromCollection As System.Collections.Generic.List(Of DBTable)

        ''' <summary>
        ''' OrderByArray que contiene los Field para ordenar la consulta
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Orderby As OrderByCollection

        Dim _groupedWhereFields As System.Collections.Generic.List(Of GroupedFields)
        Dim _myWhere As WhereString
        Dim _selectedFields As System.Collections.Generic.List(Of Field)
        Dim _groupedFields As System.Collections.Generic.List(Of Field)
        Dim _aggregateFields As System.Collections.Generic.List(Of Field)
        Dim _top As Integer
        Dim _distinct As Boolean
        Dim _mustGroup As Boolean
        Dim _identityEnabled As Boolean
        Dim _join As Join

        Public Structure GroupedFields
            Dim fieldA As Field
            Dim fieldB As Field
        End Structure

        ''' <summary>
        ''' QueryBuilder Constructor
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            _fromCollection = New System.Collections.Generic.List(Of DBTable)
            Orderby = New OrderByCollection
            _groupedWhereFields = New System.Collections.Generic.List(Of GroupedFields)
            _myWhere = New WhereString(Nothing)
            _selectedFields = New System.Collections.Generic.List(Of Field)
            _aggregateFields = New System.Collections.Generic.List(Of Field)
            _groupedFields = New System.Collections.Generic.List(Of Field)
            _join = New Join
            _identityEnabled = True
        End Sub

        ''' <summary>
        ''' Limpia todas las instancias necesarios para generar las consultas
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            _fromCollection.Clear()
            Orderby.Clear()
            _groupedWhereFields.Clear()

            _selectedFields.Clear()
            _aggregateFields.Clear()
            _groupedFields.Clear()
            _join.Capsules.Clear()

            _top = 0
            _distinct = False
            _mustGroup = False
        End Sub

        ''' <summary>
        ''' Propiedad que contine instancia de DBTables para seleccionar
        ''' </summary>
        ''' <value></value>
        ''' <returns>El arreglo que contiene las tablas instacias</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property From() As System.Collections.Generic.List(Of DBTable)
            Get
                Return _fromCollection
            End Get
        End Property

        ''' <summary>
        ''' Contiene la logica para generar joins
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Join() As Join
            Get
                Return _join
            End Get
        End Property

        ''' <summary>
        ''' Valor que indica la cantidad de registros a devolver.
        ''' </summary>
        ''' <value>El top</value>
        ''' <returns>El top</returns>
        ''' <remarks></remarks>
        Public Property Top() As Integer
            Get
                Return _top
            End Get
            Set(ByVal value As Integer)
                _top = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que indica si se seleccionara con distinct
        ''' </summary>
        ''' <value>Si se seleccionara con distinct</value>
        ''' <returns>Si se seleccionara con distinct</returns>
        ''' <remarks></remarks>
        Public Property Distinct() As Boolean
            Get
                Return _distinct
            End Get
            Set(ByVal value As Boolean)
                _distinct = value
            End Set
        End Property

        ''' <summary>
        ''' Si las consultas se hacen tomando encuenta los Field que son identity
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public WriteOnly Property IdentityEnabled() As Boolean
            Set(ByVal value As Boolean)
                _identityEnabled = value
            End Set
        End Property

        'Funciones
        ''' <summary>
        ''' Metodo que genera el select query, tomando los campos que 
        ''' contienen el SelectArray y el WhereArray
        ''' </summary>
        ''' <returns>El Query hecho</returns>
        ''' <remarks>El selectArray debe tener al menos alguna instancia de DBTable. 
        ''' Ademas elimina del arrreglo los elementos una vez creado el query.</remarks>
        Public Function RelationalSelectQuery() As String
            Dim finalQuery As New StringBuilder
            Dim baseSelectCounter As Integer = 0

            'Busca que seleccionar
            CollectFields()

            'De aqui en adelante se pueden hacer threats
            baseSelectCounter = buildSelectString(finalQuery, baseSelectCounter)
            baseSelectCounter = buildAggregateSelectString(finalQuery, baseSelectCounter)

            buildFromString(finalQuery)

            buildWhere(finalQuery)
            buildGroupString(finalQuery)
            buildHaving(finalQuery)
            buildOrderBy(finalQuery)
            'Hasta aqui los threads

            Clear()
            Return finalQuery.ToString
        End Function

        ''' <summary>
        ''' Metodo que genera el select query, de una instancia de un DBTable
        ''' Toma las condiciones de esta instancia
        ''' </summary>
        ''' <param name="theRegistry">La instancia</param>
        ''' <returns>El Query hecho</returns>
        ''' <remarks></remarks>
        Public Function SelectQuery(ByRef theRegistry As DBTable) As String
            From.Add(theRegistry)
            Return RelationalSelectQuery()
        End Function

        ''' <summary>
        ''' Metodo que genera el insert query, de una instancia de un DBTable
        ''' Toma las valores de esta instancia
        ''' </summary>
        ''' <param name="theRegistry">La instancia</param>
        ''' <returns>El Query hecho</returns>
        ''' <remarks>Los campos obligatorias deben tener un valor valido</remarks>
        Public Function InsertQuery(ByRef theRegistry As DBTable) As String
            Dim sqlQuery As String = ""
            Dim sqlQuery_temp As String = ""
            Dim first As Boolean = True
            Dim counter As Integer = 0
            Dim theField As Field

            For counter = 0 To theRegistry.Fields.Count - 1
                theField = theRegistry.Fields(counter)
                sqlQuery_temp &= verifyFieldFor_Insert(theField, first)
                If sqlQuery_temp.Length > 0 Then
                    first = False
                End If
            Next

            If sqlQuery_temp.Length > 0 Then        'Verifica si se inserta algo
                sqlQuery = "INSERT INTO " & theRegistry.TableName & " (" & sqlQuery_temp & ") "

                'Los valores
                first = True
                counter = 0
                sqlQuery_temp = ""
                For counter = 0 To theRegistry.Fields.Count - 1
                    theField = theRegistry.Fields(counter)
                    sqlQuery_temp &= verifyFieldFor_Values(theField, first)
                    If sqlQuery_temp.Length > 0 Then
                        first = False
                    End If
                Next
                If sqlQuery_temp.Length > 0 Then
                    sqlQuery_temp &= ")"
                End If
            Else
                Me.Clear()
                Throw New Exception(classIdentifier & "InsertQuery: " & theRegistry.TableName & ": " & "No fields to insert.")
            End If
            sqlQuery &= sqlQuery_temp

            theRegistry.Fields.ClearAll()
            Clear()
            Return sqlQuery
        End Function

        ''' <summary>
        ''' Metodo que genera el update query, de una instancia de un DBTable
        ''' Toma las condiciones de esta instancia
        ''' </summary>
        ''' <param name="theRegistry">La instancia</param>
        ''' <returns>El Query hecho</returns>
        ''' <remarks>Debe tener al menos alguna condicion</remarks>
        Public Function UpdateQuery(ByRef theRegistry As DBTable) As String
            'Dim sqlQuery As String = ""
            'Dim sqlQuery_temp As String = ""

            Dim query As New StringBuilder
            Dim first As Boolean = True
            Dim counter As Integer = 0
            Dim theField As Field

            query.Append("UPDATE ")
            query.Append(theRegistry.TableName)
            query.Append(" ")

            For counter = 0 To theRegistry.Fields.Count - 1
                theField = theRegistry.Fields(counter)
                If Not verifyFieldFor_Update(query, theField, first) Then
                    first = False
                End If
            Next

            If Not first Then        'Verifica si hay algo para update
                _fromCollection.Add(theRegistry)
                buildFromString(query)

                If Not buildWhere(query) Then
                    Throw New Exception(classIdentifier & "UpdateQuery: " & theRegistry.TableName & ": " & "No where clause.")
                End If
            Else
                Me.Clear()
                Throw New Exception(classIdentifier & "UpdateQuery: " & theRegistry.TableName & ": " & "No information to update.")
            End If

            theRegistry.Fields.ClearAll()
            Clear()
            Return query.ToString
        End Function

        ''' <summary>
        ''' Metodo que genera el delete query, de una instancia de un DBTable
        ''' Toma las condiciones de esta instancia
        ''' </summary>
        ''' <param name="theRegistry">La instancia</param>
        ''' <returns>El Query hecho</returns>
        ''' <remarks></remarks>
        Public Function DeleteQuery(ByRef theRegistry As DBTable) As String
            Dim query As New StringBuilder
            'Dim sqlQuery_temp As String = ""

            query.Append("DELETE FROM ")
            query.Append(theRegistry.TableName)
            query.Append(" ")
            _fromCollection.Add(theRegistry)
            If Not buildWhere(query) Then
                Me.Clear()
                Throw New Exception(classIdentifier & "SelectQuery: " & theRegistry.TableName & ": " & "No where conditions.")
            End If
            _fromCollection.Clear()

            theRegistry.Fields.ClearAll()
            Clear()
            Return query.ToString
        End Function

        ''' <summary>
        ''' Agrupa dos Fields para una condicion.
        ''' </summary>
        ''' <param name="theFieldA">El field primero</param>
        ''' <param name="theFieldB">El field segundo</param>
        ''' <remarks></remarks>
        Public Sub GroupFieldsConditions(ByVal theFieldA As Field, ByVal theFieldB As Field)
            If Not theFieldA.Equals(theFieldB) Then
                Dim theGroupedFields As New GroupedFields
                theGroupedFields.fieldA = theFieldA
                theGroupedFields.fieldB = theFieldB
                _groupedWhereFields.Add(theGroupedFields)
            End If
        End Sub

        ''' <summary>
        ''' Agrupa dos condiciones
        ''' </summary>
        ''' <param name="theConditionA"></param>
        ''' <param name="theConditionB"></param>
        ''' <param name="theRelation">La relacion de esta nueva condicion hacia "afuera"</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GroupConditions(ByVal theConditionA As Condition, ByVal theConditionB As Condition, Optional ByVal theRelation As Where.FieldRelations = Where.FieldRelations.AND_) As Condition
            Dim condicionGenerada As String = buildGroupedCondition(theConditionA, theConditionB)
            Dim temp As New Condition(_myWhere, condicionGenerada, theRelation)

            If theConditionA.ParentWhere IsNot Nothing Then
                theConditionA.ParentWhere.Conditions.Remove(theConditionA)
            End If

            If theConditionB.ParentWhere IsNot Nothing Then
                theConditionB.ParentWhere.Conditions.Remove(theConditionB)
            End If

            _myWhere.Conditions.Add(temp)
            Return temp
        End Function

        ''' <summary>
        ''' Busca que Field's debe seleccionar, y verifica si alguno esta con funciones agregadas
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CollectFields()
            Dim excID As String = classIdentifier & "CollectFields: "
            Dim theInstance As DBTable
            Dim theField As Field
            Dim haySeleccionados As Boolean = False

            'Busca que seleccionar
            For counter As Integer = 0 To _fromCollection.Count() - 1
                theInstance = _fromCollection.Item(counter)
                For fieldsCounter As Integer = 0 To theInstance.Fields.Count - 1
                    theField = theInstance.Fields(fieldsCounter)
                    If theField.ToSelect Then
                        haySeleccionados = True
                        If theField.AggregateFunction = Field.AggregateFunctionList.NORMAL Then
                            'Agregar al array de select
                            _selectedFields.Add(theField)
                        Else
                            'Agregar al array de select con funciones agregadas
                            _aggregateFields.Add(theField)
                            _mustGroup = True
                        End If

                    End If
                Next
            Next

            If Not haySeleccionados Then
                Me.Clear()
                Throw New Exception(excID & "Nothing to select.")
            End If
        End Sub

        'Builders
        ''' <summary>
        ''' Genera la parte del select con los campos del query.
        ''' </summary>
        ''' <param name="queryBase">El stringbuilder del query</param>
        ''' <param name="baseSelectCounter">Cuantos campos hay seleccionados previo a este llamado</param>
        ''' <returns>La cantidad de campos selecionados</returns>
        ''' <remarks></remarks>
        Private Function buildSelectString(ByVal queryBase As StringBuilder, ByVal baseSelectCounter As Integer) As Integer
            queryBase.Append("SELECT ")

            'Si hay distinct
            If _distinct Then
                queryBase.Append("DISTINCT ")
            End If

            'Si hay top
            If _top <> 0 Then
                queryBase.Append("TOP (")
                queryBase.Append(_top)
                queryBase.Append(") ")
            End If

            For counter As Integer = 0 To _selectedFields.Count() - 1
                If counter > 0 Then
                    queryBase.Append(", ")
                End If

                'Abre Trims
                If _selectedFields(counter).LTrim Then
                    queryBase.Append("LTRIM(")
                    If _selectedFields(counter).AsName.Length = 0 Then
                        Throw New Exception(_selectedFields(counter).ParentTable.TableName & "." & _selectedFields(counter).Name & " must have an AS for trim function.")
                    End If
                End If

                If _selectedFields(counter).RTrim Then
                    queryBase.Append("RTRIM(")
                    If _selectedFields(counter).AsName.Length = 0 Then
                        Throw New Exception(_selectedFields(counter).ParentTable.TableName & "." & _selectedFields(counter).Name & " must have an AS for trim function.")
                    End If
                End If

                'Tabla
                queryBase.Append(buildFieldName(_selectedFields(counter), True, False))
                _selectedFields(counter)._SelectIndex = baseSelectCounter

                'Cierra Trims
                If _selectedFields(counter).RTrim Then
                    queryBase.Append(")")
                End If
                If _selectedFields(counter).LTrim Then
                    queryBase.Append(")")
                End If

                'Si esta concatenado. Revisar
                If _selectedFields(counter).ConcatenatedString.Length > 0 Then
                    queryBase.Append(" + '")
                    queryBase.Append(_selectedFields(counter).ConcatenatedString)
                    queryBase.Append("' + ")
                    queryBase.Append(buildFieldName(_selectedFields(counter).ConcatenatedField, True, False))
                End If

                'Si tiene AS el campo, o si la tabla tiene un AS
                If _selectedFields(counter).AsName.Length > 0 Then
                    queryBase.Append(" AS ")
                    queryBase.Append(_selectedFields(counter).AsName)
                Else
                    If _selectedFields(counter).ParentTable.AsName.Length > 0 Then
                        Dim theAsName As String = _selectedFields(counter).ParentTable.AsName & "_" & _selectedFields(counter).Name
                        _selectedFields(counter).AsName = theAsName
                        queryBase.Append(" AS ")
                        queryBase.Append(theAsName)
                    End If
                End If

                If _mustGroup Then
                    _groupedFields.Add(_selectedFields(counter))
                End If

                baseSelectCounter += 1
            Next

            Return baseSelectCounter
        End Function

        ''' <summary>
        ''' Continua generando la parte del select de los campos con funciones agregadas.
        ''' </summary>
        ''' <param name="queryBase">El stringbuilder del query</param>
        ''' <param name="baseSelectCounter">Cuantos campos hay seleccionados previo a este llamado</param>
        ''' <returns>La cantidad de campos selecionados</returns>
        ''' <remarks></remarks>
        Private Function buildAggregateSelectString(ByVal queryBase As StringBuilder, ByVal baseSelectCounter As Integer) As Integer
            If _aggregateFields.Count > 0 Then
                If _selectedFields.Count > 0 Then
                    queryBase.Append(", ")
                Else
                    queryBase.Append(" ")
                End If

                For counter As Integer = 0 To _aggregateFields.Count() - 1
                    If counter > 0 Then
                        queryBase.Append(", ")
                    End If

                    _aggregateFields(counter)._SelectIndex = baseSelectCounter
                    buildField_Aggregate(queryBase, _aggregateFields(counter))

                    'Si tiene AS el campo
                    If _aggregateFields(counter).AsName.Length > 0 Then
                        queryBase.Append(" AS ")
                        queryBase.Append(_aggregateFields(counter).AsName)
                    Else
                        Me.Clear()
                        Throw New Exception(classIdentifier & "buildAggregateSelectString: " & _aggregateFields(counter).Name & ": Must have an As Name when selected by aggregate function")
                    End If

                    baseSelectCounter += 1
                Next
            End If

            Return baseSelectCounter
        End Function

        ''' <summary>
        ''' Genera la parte del FROM del query, asi como los joins existentes
        ''' </summary>
        ''' <param name="fromQuery">El stringbuilder del query</param>
        ''' <remarks></remarks>
        Private Sub buildFromString(ByVal fromQuery As StringBuilder)

            'Dim theInstance As DBTable
            Dim usedDBTables As New ArrayList
            Dim primero As Boolean = True

            fromQuery.Append(" FROM ")

            'Buscar los joins
            For Each joinCapsule As Join.JoinCapsule In _join.Capsules
                Dim contieneA As Boolean = False
                Dim contieneB As Boolean = False
                Dim laDBTableActual As DBTable = Nothing

                'Buscar si existe A
                If Not joinCapsule.continuacion Then
                    'No es continuacion, agrega todo la parte de las tablas
                    If joinCapsule.fieldA.ParentTable.AsName.Length > 0 Then
                        contieneA = usedDBTables.Contains(joinCapsule.fieldA.ParentTable.AsName)
                    Else
                        contieneA = usedDBTables.Contains(joinCapsule.fieldA.ParentTable.TableName)
                    End If

                    'Buscar si existe B
                    If joinCapsule.fieldB.ParentTable.AsName.Length > 0 Then
                        contieneB = usedDBTables.Contains(joinCapsule.fieldB.ParentTable.AsName)
                    Else
                        contieneB = usedDBTables.Contains(joinCapsule.fieldB.ParentTable.TableName)
                    End If

                    If Not primero Then
                        If Not contieneA Then
                            laDBTableActual = joinCapsule.fieldA.ParentTable
                        ElseIf Not contieneB Then
                            laDBTableActual = joinCapsule.fieldB.ParentTable
                        End If
                    End If

                    'Tabla A
                    If primero Then
                        fromQuery.Append(joinCapsule.fieldA.ParentTable.TableName)
                        If joinCapsule.fieldA.ParentTable.AsName.Length > 0 Then
                            fromQuery.Append(" AS ")
                            fromQuery.Append(joinCapsule.fieldA.ParentTable.AsName)
                            usedDBTables.Add(joinCapsule.fieldA.ParentTable.AsName)
                        Else
                            usedDBTables.Add(joinCapsule.fieldA.ParentTable.TableName)
                        End If
                    End If

                    'Tipo de join
                    If primero Or laDBTableActual IsNot Nothing Then
                        Select Case joinCapsule.joinType
                            Case DBHandler.Join.JoinTypes.INNER_JOIN
                                fromQuery.Append(" INNER JOIN ")
                            Case DBHandler.Join.JoinTypes.LEFT_OUTER_JOIN
                                fromQuery.Append(" LEFT OUTER JOIN ")
                            Case DBHandler.Join.JoinTypes.RIGHT_OUTER_JOIN
                                fromQuery.Append(" RIGHT OUTER JOIN ")
                        End Select
                    End If

                    If primero Then
                        'Tabla B
                        fromQuery.Append(joinCapsule.fieldB.ParentTable.TableName)
                        If joinCapsule.fieldB.ParentTable.AsName.Length > 0 Then
                            fromQuery.Append(" AS ")
                            fromQuery.Append(joinCapsule.fieldB.ParentTable.AsName)
                            usedDBTables.Add(joinCapsule.fieldB.ParentTable.AsName)
                        Else
                            usedDBTables.Add(joinCapsule.fieldB.ParentTable.TableName)
                        End If
                    End If

                    If laDBTableActual IsNot Nothing Then
                        fromQuery.Append(laDBTableActual.TableName)
                        If laDBTableActual.AsName.Length > 0 Then
                            fromQuery.Append(" AS ")
                            fromQuery.Append(laDBTableActual.AsName)
                            usedDBTables.Add(laDBTableActual.AsName)
                        Else
                            usedDBTables.Add(laDBTableActual.TableName)
                        End If
                    End If
                End If

                'On part
                If primero Or laDBTableActual IsNot Nothing Or joinCapsule.continuacion Then
                    If Not joinCapsule.continuacion Then
                        fromQuery.Append(" ON ")
                    Else
                        fromQuery.Append(" AND ")
                    End If

                    fromQuery.Append(buildFieldName(joinCapsule.fieldA, True, False))
                    fromQuery.Append(joinCapsule.relation)
                    fromQuery.Append(buildFieldName(joinCapsule.fieldB, True, False))
                End If

                If primero Then
                    primero = False
                End If
            Next

            'Construye por el normal FromCollection
            For Each theInstance As DBTable In _fromCollection
                Dim yaExiste As Boolean = False
                If theInstance.AsName.Length > 0 Then
                    yaExiste = usedDBTables.Contains(theInstance.AsName)
                Else
                    yaExiste = usedDBTables.Contains(theInstance.TableName)
                End If

                If Not yaExiste Then
                    If Not primero Then
                        fromQuery.Append(", ")
                    Else
                        primero = False
                    End If

                    fromQuery.Append(theInstance.TableName)
                    If theInstance.AsName.Length > 0 Then
                        fromQuery.Append(" AS ")
                        fromQuery.Append(theInstance.AsName)
                        usedDBTables.Add(theInstance.AsName)
                    Else
                        usedDBTables.Add(theInstance.TableName)
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' Genera la parte de GROUP BY de existir campos seleccionados con funcioens agregadas.
        ''' </summary>
        ''' <param name="finalQuery">El stringbuilder del query</param>
        ''' <remarks></remarks>
        Private Sub buildGroupString(ByVal finalQuery As StringBuilder)
            If _groupedFields.Count() > 0 Then
                finalQuery.Append(" GROUP BY ")

                For counter As Integer = 0 To _groupedFields.Count() - 1
                    If counter > 0 Then
                        finalQuery.Append(", ")
                    End If

                    'Nombre del campo
                    finalQuery.Append(buildFieldName(_groupedFields(counter), True, False))

                    If _groupedFields(counter).ConcatenatedField IsNot Nothing Then
                        finalQuery.Append(", ")
                        finalQuery.Append(buildFieldName(_groupedFields(counter).ConcatenatedField, True, False))
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Genera las condiciones agrupadas registradas al querybuilder.
        ''' </summary>
        ''' <param name="query">El stringbuilder del query</param>
        ''' <param name="first">Si es el primer where a generar</param>
        ''' <returns>El mismo valor de si es primero o no</returns>
        ''' <remarks></remarks>
        Private Function buildMyWhere(ByRef query As StringBuilder, ByVal first As Boolean) As Boolean
            For Each currentCondition As Condition In _myWhere.Conditions
                If first Then
                    first = False
                Else
                    query.Append(buildRelation(currentCondition.Relation))
                End If
                query.Append(currentCondition.Condition)
            Next
            _myWhere.Clear()

            Return first
        End Function

        ''' <summary>
        ''' Genera la parte del WHERE del query. Tiene tres secciones logicas. 
        ''' </summary>
        ''' <param name="queryBase">El stringbuilder del query</param>
        ''' <returns>Si hay filtros</returns>
        ''' <remarks></remarks>
        Private Function buildWhere(ByVal queryBase As StringBuilder) As Boolean
            Dim first As Boolean = True
            Dim query As New StringBuilder
            Dim theInstance As DBTable
            Dim theField As Field

            query.Append(" WHERE ")
            first = buildMyWhere(query, first)
            first = buildGroupedWhere(query, first)

            'Recorre los campos del from
            For counter As Integer = 0 To _fromCollection.Count() - 1
                theInstance = _fromCollection.Item(counter)
                For fieldsCounter As Integer = 0 To theInstance.Fields.Count - 1
                    theField = theInstance.Fields(fieldsCounter)

                    If verifyFieldFor_Where(query, theField, Not first) Then
                        first = False
                    End If
                    theField.WherePrivate.Clear()
                Next
            Next

            If first Then
                Return False
            Else
                queryBase.Append(query.ToString)
                Return True
            End If
        End Function

        ''' <summary>
        ''' Genera el where con las condiciones agrupadas de los campos.
        ''' </summary>
        ''' <param name="query">El stringbuilder del query</param>
        ''' <param name="first">Si es el primer where a generar</param>
        ''' <returns>El mismo valor de si es primero o no</returns>
        ''' <remarks></remarks>
        Private Function buildGroupedWhere(ByRef query As StringBuilder, ByVal first As Boolean) As Boolean
            'Agrupa los Fields
            For counter As Integer = 0 To _groupedWhereFields.Count() - 1
                Dim theGroupedFields As GroupedFields = _groupedWhereFields(counter)
                Dim theCondition As Condition = theGroupedFields.fieldA.WherePrivate.Conditions(0)

                If first Then
                    first = False
                Else
                    query.Append(buildRelation(theCondition.Relation))
                End If

                query.Append("(")
                verifyFieldFor_Where(query, theGroupedFields.fieldA, False)
                verifyFieldFor_Where(query, theGroupedFields.fieldB, True)
                query.Append(") ")

                theGroupedFields.fieldA.WherePrivate.Clear()
                theGroupedFields.fieldB.WherePrivate.Clear()
            Next

            Return first
        End Function

        ''' <summary>
        ''' Genera la parte del having del query
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub buildHaving(ByRef query As StringBuilder)
            Dim first As Boolean = True
            'Dim theField As Field

            'Recorre primero los agrupados
            'Dim stringGrupo As String = ""
            'For counter As Integer = 0 To _groupedHavingFields.Count() - 1
            '    theField = _groupedHavingFields(counter)
            '    If counter Mod 2 = 0 Then
            '        stringGrupo = "("
            '    Else
            '        stringGrupo = ")"
            '    End If
            '    havingQuery &= verifyFieldFor_Having(theField, first, stringGrupo)
            '    If havingQuery.Length > 0 Then
            '        first = False
            '    End If
            '    theField.Having.Clear()
            'Next

            'Recorre los campos del 
            For counter As Integer = 0 To _aggregateFields.Count - 1
                first = verifyFieldFor_Having(query, _aggregateFields(counter), first)
            Next
        End Sub

        ''' <summary>
        ''' Genera la parte del order by del query
        ''' </summary>
        ''' <param name="finalQuery">El stringbuilder del query</param>
        ''' <remarks></remarks>
        Private Sub buildOrderBy(ByVal finalQuery As StringBuilder)
            Dim counter As Integer

            For counter = 0 To Orderby.Count - 1
                If counter = 0 Then
                    finalQuery.Append(" ORDER BY ")
                Else
                    finalQuery.Append(", ")
                End If

                'ojo: cuando es agregate debe ser con as name
                If Orderby(counter).Field.AggregateFunction <> Field.AggregateFunctionList.NORMAL Then
                    finalQuery.Append(buildFieldName(Orderby(counter).Field, True, True))
                Else
                    finalQuery.Append(buildFieldName(Orderby(counter).Field, True, False))
                End If
                finalQuery.Append(" ")
                finalQuery.Append(Orderby(counter).Order)
            Next
        End Sub

        Private Sub buildField_Aggregate(ByRef query As StringBuilder, ByRef theField As Field)
            'Tipo de funcion agregada
            Select Case theField.AggregateFunction
                Case Field.AggregateFunctionList.COUNT
                    query.Append("COUNT(")
                Case Field.AggregateFunctionList.SUMMATION
                    query.Append("SUM(")
                Case Field.AggregateFunctionList.AVERAGE
                    query.Append("AVG(")
                Case Field.AggregateFunctionList.MAXIMUM
                    query.Append("MAX(")
                Case Field.AggregateFunctionList.MINIMUM
                    query.Append("MIN(")
            End Select

            query.Append(buildFieldName(theField, True, False))

            'Cierra funcion agregada
            query.Append(")")
        End Sub

        Private Function buildFieldValue(ByRef theField As Field) As String
            Dim fieldValue As String

            'Verifica que tenga algun valor
            If theField.IsValid Then
                If Not theField.IsNull Then
                    fieldValue = buildFieldValueByType(theField.RenderValue, theField.Type)
                Else
                    If theField.AllowNulls Then
                        fieldValue = "NULL "
                    Else
                        'If forInsert Then
                        'fieldValue = buildFieldValueByType(theField.Default, theField.Type)
                        'Else

                        Throw New Exception(classIdentifier & "buildFieldValue: " & theField.Name & ": " & "Field doesn't allow nulls")
                        'End If
                    End If
                End If
            Else
                If theField.AllowNulls Then
                    fieldValue = "NULL "
                Else
                    Me.Clear()
                    Throw New Exception(classIdentifier & "buildFieldValue: " & theField.Name & ": " & "Field is not valid and doesn't allow nulls")
                End If
            End If

            Return fieldValue
        End Function

        Private Function buildFieldValueByType(ByVal theValue As String, ByVal theType As Field.FieldType) As String
            Dim fieldValue As String
            Select Case theType
                Case Field.FieldType.INT
                    fieldValue = theValue & " "

                Case Field.FieldType.VARCHAR
                    'Escapar el caracter '
                    If theValue.Length > 0 Then
                        fieldValue = "'" & theValue.Replace("'", "''") & "' "
                    Else
                        fieldValue = "'" & theValue & "' "
                    End If

                Case Field.FieldType.TEXT
                    'Escapar el caracter '
                    If theValue.Length > 0 Then
                        fieldValue = "'" & theValue.Replace("'", "''") & "' "
                    Else
                        fieldValue = "'" & theValue & "' "
                    End If

                Case Field.FieldType.VARCHAR_MULTILANG
                    'Escapar el caracter '
                    If theValue.Length > 0 Then
                        fieldValue = "'" & theValue.Replace("'", "''") & "' "
                    Else
                        fieldValue = "'" & theValue & "' "
                    End If

                Case Field.FieldType.TEXT_MULTILANG
                    'Escapar el caracter '
                    If theValue.Length > 0 Then
                        fieldValue = "'" & theValue.Replace("'", "''") & "' "
                    Else
                        fieldValue = "'" & theValue & "' "
                    End If

                Case Field.FieldType.DATETIME
                    fieldValue = "'" & theValue & "' "

                Case Field.FieldType.FLOAT
                    theValue = theValue.Replace(","c, "."c)
                    fieldValue = theValue & " "

                Case Field.FieldType.TINYINT
                    fieldValue = theValue & " "

                Case Else
                    fieldValue = "'" & theValue & "' "

            End Select
            Return fieldValue
        End Function

        Private Function buildFieldName(ByRef theField As Field, ByVal CheckForTableAsName As Boolean, ByVal CheckForFieldAsName As Boolean) As String
            Dim result As String = ""

            If theField.AsName.Length > 0 And CheckForFieldAsName Then
                result &= theField.AsName
            Else
                'Tabla
                If theField.ParentTable.AsName.Length > 0 And CheckForTableAsName Then
                    result &= theField.ParentTable.AsName
                Else
                    result &= theField.ParentTable.TableName
                End If

                'Verifica si el campo permite multilenguaje
                result &= "." & theField.Name
            End If

            Return result
        End Function

        Private Function buildRelation(ByVal theRelation As Where.FieldRelations) As String
            Select Case theRelation
                Case Where.FieldRelations.AND_
                    Return " AND "
                Case Where.FieldRelations.OR_
                    Return " OR "
                Case Else
                    Return " AND "
            End Select
        End Function

        Private Function buildGroupedCondition(ByVal theConditionA As Condition, ByVal theConditionB As Condition) As String
            Dim whereBuilt As New StringBuilder

            'Genera las condiciones agrupadas
            whereBuilt.Append("(")

            'condicion A
            If theConditionA.ParentWhere.Field IsNot Nothing Then
                whereBuilt.Append(buildFieldName(theConditionA.ParentWhere.Field, True, False))
                whereBuilt.Append(" ")
            End If
            whereBuilt.Append(theConditionA.Condition)

            'Relacion
            whereBuilt.Append(buildRelation(theConditionB.Relation))

            'condicion B
            If theConditionB.ParentWhere.Field IsNot Nothing Then
                whereBuilt.Append(buildFieldName(theConditionB.ParentWhere.Field, True, False))
                whereBuilt.Append(" ")
            End If
            whereBuilt.Append(theConditionB.Condition)

            whereBuilt.Append(") ")

            Return whereBuilt.ToString
        End Function

        Private Sub buildFieldOperation(ByRef theQuery As StringBuilder, ByRef theField As Field)
            If theField.Operation IsNot Nothing Then
                theQuery.Append(buildFieldName(theField.Operation.BaseField, True, True))
                theQuery.Append(theField.Operation.OperationString)
            End If
        End Sub

        'Verifiers
        Private Function verifyFieldFor_Having(ByRef query As StringBuilder, ByVal theField As Field, ByVal first As Boolean, Optional ByVal groupString As String = "") As Boolean
            Dim counter As Integer

            'Recorre todas las condiciones
            For counter = 0 To theField.Having.Count - 1
                If first Then
                    query.Append(" HAVING ")
                    first = False
                Else
                    query.Append(theField.Having.Relations(counter))
                End If

                'Abre parentesis del field
                If counter = 0 Then
                    'Abre parentesis del grupo
                    If groupString = "(" Then
                        query.Append("(")
                    End If

                    query.Append("(")
                End If

                buildField_Aggregate(query, theField)

                'Escribe la condicion de having
                query.Append(" ")
                query.Append(theField.Having.Strings(counter))

                'Termina parentesis del field
                If counter = theField.Having.Count - 1 Then
                    query.Append(") ")

                    'Cierra parentesis del grupo
                    If groupString = ")" Then
                        query.Append(") ")
                    End If
                Else
                    query.Append(" ")
                End If
            Next
            Return first
        End Function

        Private Function verifyFieldFor_Where(ByRef query As StringBuilder, ByVal theField As Field, ByVal writeFirstRelation As Boolean) As Boolean
            'Recorre todas las condiciones
            Dim tieneConditions As Boolean = False

            For counter As Integer = 0 To theField.WherePrivate.Conditions.Count - 1
                Dim theCondition As Condition = theField.WherePrivate.Conditions(counter)
                tieneConditions = True

                If writeFirstRelation Then
                    query.Append(buildRelation(theCondition.Relation))
                    writeFirstRelation = False
                End If

                If counter = 0 Then
                    'Abre parentesis del field
                    query.Append("(")
                Else
                    query.Append(buildRelation(theCondition.Relation))
                End If

                query.Append(buildFieldName(theField, True, False))
                query.Append(" " & theCondition.Condition)

                'Termina parentesis del field
                If counter = theField.WherePrivate.Conditions.Count - 1 Then
                    query.Append(") ")
                Else
                    query.Append(" ")
                End If
            Next

            Return tieneConditions
        End Function

        Private Function verifyFieldFor_Update(ByVal query As StringBuilder, ByVal theField As Field, ByVal first As Boolean) As Boolean
            If theField.ToUpdate And Not theField.Identity Then
                If first Then
                    query.Append("SET ")
                Else
                    query.Append(", ")
                End If

                query.Append(buildFieldName(theField, False, False))
                query.Append(" = ")
                If theField.Operation Is Nothing Then
                    query.Append(buildFieldValue(theField))
                Else
                    buildFieldOperation(query, theField)
                End If
                Return False
            End If
            Return True
        End Function

        Private Function verifyFieldFor_Insert(ByVal theField As Field, ByVal first As Boolean) As String
            Dim result As String = ""
            If (Not theField.Identity Or Not _identityEnabled) And theField.IsValid Then

                If theField.Type = Field.FieldType.VARCHAR_MULTILANG Or theField.Type = Field.FieldType.TEXT_MULTILANG Then
                    'hacer un ciclo por los lenguajes
                    For counter As Integer = 0 To LanguageHandler.UsedLanguagesSufix.Length - 1
                        Dim currentSufix As String = LanguageHandler.UsedLanguagesSufix(counter)

                        If first Then
                            result = ""
                            first = False
                        Else
                            result &= ", "
                        End If
                        result &= theField.ParentTable.TableName & "." & theField.RealName & currentSufix & " "
                    Next
                Else
                    'Un campo sin multilenguaje
                    If first Then
                        result = ""
                    Else
                        result = ", "
                    End If

                    result &= theField.ParentTable.TableName & "." & theField.Name & " "
                End If
            End If
            Return result
        End Function

        Private Function verifyFieldFor_Values(ByVal theField As Field, ByVal first As Boolean) As String
            Dim result As String = ""
            If (Not theField.Identity Or Not _identityEnabled) And theField.IsValid Then
                If theField.Type = Field.FieldType.VARCHAR_MULTILANG Or theField.Type = Field.FieldType.TEXT_MULTILANG Then
                    'hacer un ciclo por los lenguajes
                    For counter As Integer = 0 To LanguageHandler.UsedLanguagesSufix.Length - 1
                        If first Then
                            result = "VALUES ("
                            first = False
                        Else
                            result &= ", "
                        End If
                        result &= buildFieldValue(theField)
                    Next
                Else
                    'Un campo sin multilenguaje
                    If first Then
                        result = "VALUES ("
                    Else
                        result = ", "
                    End If

                    result &= buildFieldValue(theField)
                End If

            End If
            Return result
        End Function

        'Friends
        Friend Shared Function DateForSQL(ByVal theDate As Date) As String
            Dim laFecha As String
            laFecha = theDate.Month & "/"
            laFecha &= theDate.Day & "/"
            laFecha &= theDate.Year
            'If withHour Then
            laFecha &= " " & theDate.Hour & ":" & theDate.Minute & ":" & theDate.Second
            'End If
            Return laFecha
        End Function
    End Class

End Namespace