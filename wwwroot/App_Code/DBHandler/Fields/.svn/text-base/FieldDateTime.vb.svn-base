Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase Field que simula un campo de una tabla en una base de datos
    ''' </summary>
    ''' <version>1.25</version>
    ''' <date>09/22/2006</date>
    ''' <remarks></remarks>
    Public Class FieldDateTime
        Inherits Field

        Dim classID As String = "FieldDateTime "
        Dim myValue As Date
        Dim myDefaultValue As Date
        Dim myWhere As WhereDateTime

        Private Shared NullValue As Date = Date.MinValue

        ''' <summary>
        ''' Constructor que recibe las propiedades del campo en la tabla de la DB
        ''' </summary>
        ''' <param name="theName">El nombre del campo en la tabla</param>
        ''' <param name="theParentTable">El nombre de la tabla a la que pertenece</param>
        ''' <param name="allowNulls">Si el campo acepta nulos</param>
        ''' <param name="identity">Si el campo es un identidad con autoincremento</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal theName As String, ByVal theParentTable As DBTable, _
            Optional ByVal allowNulls As Boolean = False, _
            Optional ByVal identity As Boolean = False, Optional ByVal defaultValue As Date = Nothing)
            MyBase.New(theName, theParentTable, FieldType.DATETIME, allowNulls, identity)
            myWhere = New WhereDateTime(Me)
            myDefaultValue = defaultValue

            If defaultValue = NullValue Then
                Me.Clear()
            End If
        End Sub

        ''' <summary>
        ''' Valor que toma este campo. Verifica dependiendo del tipo de campo
        ''' </summary>
        ''' <value>Valor del campo</value>
        ''' <returns>Valor del campo</returns>
        ''' <remarks></remarks>
        Public Property ValueUtc() As Date
            Get
                If Not _isValid Then
                    If Not _allowNulls And _isNull Then
                        Throw New Exception("Error on " & classID & ": Field " & Name & " doesn't allow null value.")
                    End If
                    Throw New Exception("Error on " & classID & ": Field " & Name & " isn't valid.")
                Else
                    Return myValue
                End If
            End Get
            Set(ByVal value As Date)
                _toUpdate = True
                If value <> New Date(1900, 1, 1) And value <> New Date(1, 1, 1) Then
                    myValue = value
                    _isNull = False
                    _isValid = True
                Else
                    _isNull = False
                    _isValid = False
                End If
            End Set
        End Property

        Public Property ValueLocalized() As Date
            Get
                Return Orbelink.DateAndTime.DateHandler.ToLocalizedDateFromUtc(ValueUtc)
            End Get
            Set(ByVal value As Date)
                ValueUtc = Orbelink.DateAndTime.DateHandler.ToUtcFromLocalizedDate(value)
            End Set
        End Property

        Public Overrides ReadOnly Property RenderValue() As String
            Get
                If _isValid Then
                    Return QueryBuilder.DateForSQL(myValue)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Overrides ReadOnly Property RenderDefaultValue() As String
            Get
                If myDefaultValue > NullValue Then
                    Return QueryBuilder.DateForSQL(myDefaultValue)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Overrides WriteOnly Property SetValue() As String
            Set(ByVal value As String)
                Try
                    Me.ValueUtc = CType(value, Date)
                    'Dim tempDate As Date = CType(value, Date)
                    'Me.Value = TimeZoneInfo.ConvertTimeFromUtc(tempDate, SecurityHandler.CurrentTimeZone)
                Catch
                    Throw New Exception("Error on " & classID & ": Value must be date.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Propiedad con el valor default en la tabla
        ''' </summary>
        ''' <value></value>
        ''' <returns>El valor default</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DefaultValue() As Date
            Get
                Return myDefaultValue
            End Get
        End Property

        ''' <summary>
        ''' Limpia el campo
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub Clear()
            MyBase.ClearBase()
            If myWhere IsNot Nothing Then
                myWhere.Clear()
            End If
            myValue = NullValue
            _isNull = True
            _isValid = False
            _toUpdate = False
        End Sub

        Public ReadOnly Property Where() As WhereDateTime
            Get
                Return myWhere
            End Get
        End Property

        Public Overrides ReadOnly Property WherePrivate() As Where
            Get
                Return myWhere
            End Get
        End Property

    End Class

End Namespace