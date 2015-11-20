Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase Field que simula un campo de una tabla en una base de datos
    ''' </summary>
    ''' <version>1.25</version>
    ''' <date>09/22/2006</date>
    ''' <remarks></remarks>
    Public Class FieldString
        Inherits Field

        Dim classID As String = "FieldString "
        Dim myValue As String
        Dim myDefaultValue As String
        Dim myLength As Integer
        Dim _length As Integer
        Dim myWhere As WhereString

        ''' <summary>
        ''' Constructor que recibe las propiedades del campo en la tabla de la DB
        ''' </summary>
        ''' <param name="theName">El nombre del campo en la tabla</param>
        ''' <param name="theParentTable">El nombre de la tabla a la que pertenece</param>
        ''' <param name="allowNulls">Si el campo acepta nulos</param>
        ''' <param name="identity">Si el campo es un identidad con autoincremento</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal theName As String, ByVal theParentTable As DBTable, ByVal theFieldType As Orbelink.DBHandler.FieldString.FieldType, _
                       ByVal theLength As Integer, Optional ByVal allowNulls As Boolean = False, _
                        Optional ByVal identity As Boolean = False, Optional ByVal defaultValue As String = Nothing)
            MyBase.New(theName, theParentTable, theFieldType, allowNulls, identity)

            myDefaultValue = defaultValue
            myWhere = New WhereString(Me)
            _length = theLength
            Me.Clear()
        End Sub

        Public Sub New(ByVal theName As String, ByVal theParentTable As DBTable, ByVal IsMultiLang As Boolean, _
                       ByVal theLength As Integer, Optional ByVal allowNulls As Boolean = False)
            MyBase.New(theName, theParentTable, IIf(IsMultiLang, FieldType.VARCHAR_MULTILANG, FieldType.VARCHAR), allowNulls, False)
            myDefaultValue = Nothing
            myWhere = New WhereString(Me)
            _length = theLength
            Me.Clear()
        End Sub

        ''' <summary>
        ''' Valor que toma este campo. Verifica dependiendo del tipo de campo
        ''' </summary>
        ''' <value>Valor del campo</value>
        ''' <returns>Valor del campo</returns>
        ''' <remarks></remarks>
        Public Property Value() As String
            Get
                If myValue Is Nothing Then
                    Return ""
                End If
                Return myValue
            End Get
            Set(ByVal value As String)
                SetMyvalue(value, True)
            End Set
        End Property

        Public Overrides ReadOnly Property RenderValue() As String
            Get
                Return myValue
            End Get
        End Property

        Public Overrides ReadOnly Property RenderDefaultValue() As String
            Get
                Return myDefaultValue
            End Get
        End Property

        Public Overrides WriteOnly Property SetValue() As String
            Set(ByVal value As String)
                SetMyValue(value, False)
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que indica el largo del campo (De ser necesario un largo)
        ''' </summary>
        ''' <returns>El Largo del campo</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Length() As Integer
            Get
                Return _length
            End Get
        End Property

        ''' <summary>
        ''' Propiedad con el valor default en la tabla
        ''' </summary>
        ''' <value></value>
        ''' <returns>El valor default</returns>
        ''' <remarks></remarks>
        Public Shadows ReadOnly Property DefaultValue() As String
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
            myValue = Nothing
            _isNull = True
            _isValid = False
            _toUpdate = False
        End Sub

        Public ReadOnly Property Where() As WhereString
            Get
                Return myWhere
            End Get
        End Property

        Public Overrides ReadOnly Property WherePrivate() As Where
            Get
                Return myWhere
            End Get
        End Property

        Private Sub SetMyValue(ByVal value As String, ByVal validateLength As Boolean)
            If value <> Nothing Then
                If value.Length <= _length Or Not validateLength Then
                    myValue = value
                    _isNull = False
                    _isValid = True
                    _toUpdate = True
                Else
                    _isNull = True
                    _toUpdate = False
                    _isValid = False
                    myValue = Nothing
                    Throw New Exception("Error on " & classID & ":  Field " & Name & " length can't be larger than " & _length & ".")
                End If
            ElseIf _allowNulls Then
                myValue = Nothing
                _isNull = True
                _isValid = True
                _toUpdate = True
            ElseIf myDefaultValue <> Nothing Then
                myValue = myDefaultValue
                _isNull = False
                _isValid = True
                _toUpdate = True
            Else
                _isNull = True
                _toUpdate = False
                _isValid = False
                myValue = Nothing
                Throw New Exception("Error on " & classID & ": Field " & Name & " doesn't allow null value.")
            End If
        End Sub
    End Class

End Namespace