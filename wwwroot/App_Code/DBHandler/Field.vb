Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase Field que simula un campo de una tabla en una base de datos
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public MustInherit Class Field

        Dim classID As String = "Field "

        Friend _SelectIndex As Integer
        Protected _toUpdate As Boolean
        Protected _allowNulls As Boolean
        Protected _isValid As Boolean
        Protected _isNull As Boolean
        Public LTrim As Boolean
        Public RTrim As Boolean
        Dim _name As String
        Dim _parentTable As Orbelink.DBHandler.DBTable
        Dim _having As Having
        Dim _asName As String
        Dim _toSelect As Boolean
        Dim _aggregateFunction As AggregateFunctionList
        Dim _identity As Boolean
        Dim _type As FieldType
        Dim _concatenatedField As Orbelink.DBHandler.Field
        Dim _concatenatedString As String
        Dim _operation As OperationOnField

        ''' <summary>
        ''' Lista de los tipos de datos disponibles
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum FieldType As Integer
            VARCHAR
            VARCHAR_MULTILANG
            INT
            DATETIME
            FLOAT
            TINYINT
            TEXT_MULTILANG
            TEXT
        End Enum

        ''' <summary>
        ''' Metodo de seleccion para la consulta de este campo
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum AggregateFunctionList As Integer
            NORMAL = 0
            COUNT = 1
            SUMMATION = 2
            MAXIMUM = 3
            MINIMUM = 4
            AVERAGE = 5
        End Enum

        ''' <summary>
        ''' Constructor que recibe las propiedades del campo en la tabla de la DB
        ''' </summary>
        ''' <param name="theName">El nombre del campo en la tabla</param>
        ''' <param name="theParentTable">La tabla a la que pertenece</param>
        ''' <param name="type">Tipo de valor que contiene este campo</param>
        ''' <param name="allowNulls">Si el campo acepta nulos</param>
        ''' <param name="identity">Si el campo es un identidad con autoincremento</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal theName As String, ByVal theParentTable As Orbelink.DBHandler.DBTable, _
            ByVal type As FieldType, Optional ByVal allowNulls As Boolean = False, _
            Optional ByVal identity As Boolean = False)

            'Verifica que el tipo haya sido declarado.
            If theName.Length = 0 Or (0 > type > 3) Then
                Throw New Exception("Error on " & classID & ": No valid type.")
            Else
                _parentTable = theParentTable
                _name = theName
                _type = type
                _having = New Having(Me)
                _allowNulls = allowNulls
                _identity = identity
                _asName = ""
                _isNull = True
                Clear()
                classID &= _parentTable.TableName & "." & _name
                _SelectIndex = -1
            End If
        End Sub

        'Abstractas
        Public MustOverride ReadOnly Property RenderValue() As String

        Public MustOverride ReadOnly Property RenderDefaultValue() As String

        Public MustOverride WriteOnly Property SetValue() As String

        Public MustOverride ReadOnly Property WherePrivate() As Where

        ''' <summary>
        ''' Propiedad que contine las condiciones del having
        ''' </summary>
        ''' <value>La condicion</value>
        ''' <returns>La condicion</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Having() As Having
            Get
                Return _having
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que indica si han habido cambios sin guardar en DB
        ''' </summary>
        ''' <value>Si ya se actualizo en DB</value>
        ''' <returns>Si ha habido algun cambio sin guardar en DB</returns>
        ''' <remarks></remarks>
        Public Property ToUpdate() As Boolean
            Get
                Return _toUpdate
            End Get
            Set(ByVal value As Boolean)
                If _identity Then
                    _toUpdate = False
                ElseIf _isValid Or _allowNulls Then
                    _toUpdate = value
                Else
                    _toUpdate = False
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que indica si debe ser tomada en cuenta para el select
        ''' </summary>
        ''' <value>Para el select</value>
        ''' <returns>Para el select</returns>
        ''' <remarks></remarks>
        Public Property ToSelect() As Boolean
            Get
                Return _toSelect
            End Get
            Set(ByVal value As Boolean)
                _toSelect = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que indica como seleccionar el campo
        ''' </summary>
        ''' <value>Forma de selccionar el campo</value>
        ''' <returns>Forma de selccionar el campo</returns>
        ''' <remarks></remarks>
        Public Property AggregateFunction() As AggregateFunctionList
            Get
                Return _aggregateFunction
            End Get
            Set(ByVal value As AggregateFunctionList)
                _aggregateFunction = value
                _toSelect = True
                _asName = RealName & "Aggregate"
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que indica si el campo permite nulls
        ''' </summary>
        ''' <value></value>
        ''' <returns>Si permite nulls</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AllowNulls() As Boolean
            Get
                Return _allowNulls
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que indica si el campo es nulo
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsNull() As Boolean
            Get
                Return _isNull
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que contiene el la tabla a la que pertene este campo
        ''' </summary>
        ''' <value></value>
        ''' <returns>La tabla</returns>
        ''' <remarks></remarks>
        Friend ReadOnly Property ParentTable() As Orbelink.DBHandler.DBTable
            Get
                Return _parentTable
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que indica si el campo es una identidad
        ''' </summary>
        ''' <value></value>
        ''' <returns>Si es identidad</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Identity() As Boolean
            Get
                Return _identity
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que indica si el campo es valido
        ''' </summary>
        ''' <value></value>
        ''' <returns>Si permite nulls</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsValid() As Boolean
            Get
                Return _isValid
            End Get
        End Property

        ''' <summary>
        ''' Propiedad que indica el tipo de datos
        ''' </summary>
        ''' <value></value>
        ''' <returns>El tipo de datos</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Type() As Orbelink.DBHandler.Field.FieldType
            Get
                Return _type
            End Get
        End Property

        ''' <summary>
        ''' Propiedad con el nombre que tiene el campo en la tabla de la DB
        ''' </summary>
        ''' <value></value>
        ''' <returns>El nombre del campo</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            Get
                If Me.Type = FieldType.VARCHAR_MULTILANG Or Me.Type = FieldType.TEXT_MULTILANG Then
                    Return _name & Orbelink.DBHandler.LanguageHandler.CurrentLanguageSufix
                Else
                    Return _name
                End If
            End Get
        End Property

        ''' <summary>
        ''' Nombre real del campo, sin el sufijo de idioma
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend ReadOnly Property RealName() As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property SelectIndex() As Integer
            Get
                Return _SelectIndex
            End Get
        End Property

        ''' <summary>
        ''' Propiedad con el nombre temporal AS que tiene el campo en la tabla de la DB
        ''' </summary>
        ''' <value></value>
        ''' <returns>El nombre temporal AS del campo</returns>
        ''' <remarks></remarks>
        Public Property AsName() As String
            Get
                Return _asName
            End Get
            Set(ByVal value As String)
                If value <> _asName Then
                    If value.Length > 0 Then        'Verifica si esta vacio, y si acepta nulls
                        _asName = value
                    Else
                        Throw New Exception("Error on " & classID & ": AS Name must be a not empty string.")
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Campo con el que esta concatenado
        ''' </summary>
        ''' <value></value>
        ''' <returns>Campo concatenado</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ConcatenatedField() As Orbelink.DBHandler.Field
            Get
                Return _concatenatedField
            End Get
        End Property

        ''' <summary>
        ''' El string de los campos ya concatenados
        ''' </summary>
        ''' <value></value>
        ''' <returns>Strin de concatenados</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ConcatenatedString() As String
            Get
                Return _concatenatedString
            End Get
        End Property

        ''' <summary>
        ''' Limpia el campo
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub Clear()

        Protected Sub ClearBase()
            _toUpdate = False
            _toSelect = False
            _isValid = True
            _concatenatedString = ""
            _concatenatedField = Nothing

            _aggregateFunction = AggregateFunctionList.NORMAL
            _having.Clear()
        End Sub

        ''' <summary>
        ''' Concatena dos campos
        ''' </summary>
        ''' <param name="otherField">El otro campo</param>
        ''' <param name="concatString">String por el que se van a concatenar</param>
        ''' <param name="AsName">El As del resultado de la consulta</param>
        ''' <remarks></remarks>
        Public Sub Concatenate(ByRef otherField As Orbelink.DBHandler.Field, ByVal concatString As String, ByVal AsName As String)
            _concatenatedString = concatString
            _concatenatedField = otherField
            _asName = AsName
        End Sub

        Public Sub SetToNull()
            _isNull = True
            If _allowNulls Then
                _isValid = True
            Else
                _isValid = False
            End If
            _toUpdate = True
        End Sub


        'Operaciones
        Friend ReadOnly Property Operation() As OperationOnField
            Get
                Return _operation
            End Get
        End Property

        Public Sub SetUpdateOperation(ByVal theOperationString As String)
            _toUpdate = True
            _operation = New OperationOnField(Me, theOperationString)
        End Sub

        'Public Sub SetUpdateOperation(ByVal theOperationString As String, ByVal baseField As Field)
        '    _operation = New OperationOnField(baseField, theOperationString)
        'End Sub

    End Class

End Namespace