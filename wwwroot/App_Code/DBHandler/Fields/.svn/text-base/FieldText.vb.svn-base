Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase Field que simula un campo de una tabla en una base de datos
    ''' </summary>
    ''' <version>1.25</version>
    ''' <date>09/22/2006</date>
    ''' <remarks></remarks>
    Public Class FieldText
        Inherits Field

        Dim classID As String = "FieldText "
        Dim myValue As String
        'Dim myDefaultValue As String
        'Dim myLength As Integer
        'Dim _length As Integer
        Dim myWhere As WhereString

        ''' <summary>
        ''' Constructor que recibe las propiedades del campo en la tabla de la DB
        ''' </summary>
        ''' <param name="theName">El nombre del campo en la tabla</param>
        ''' <param name="theParentTable">El nombre de la tabla a la que pertenece</param>
        ''' <param name="allowNulls">Si el campo acepta nulos</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal theName As String, ByVal theParentTable As DBTable, ByVal IsMultiLang As Boolean, Optional ByVal allowNulls As Boolean = False)
            MyBase.New(theName, theParentTable, IIf(IsMultiLang, FieldType.TEXT_MUlTILANG, FieldType.TEXT), allowNulls, False)

            myWhere = New WhereString(Me)
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
                If value <> Nothing Then
                    myValue = value
                    _isNull = False
                    _isValid = True
                    _toUpdate = True
                ElseIf _allowNulls Then
                    myValue = Nothing
                    _isNull = True
                    _isValid = True
                    _toUpdate = True
                Else
                    _isNull = True
                    _toUpdate = False
                    _isValid = False
                    myValue = Nothing
                    Throw New Exception("Error on " & classID & ": Field " & Name & " doesn't allow null value.")
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property RenderValue() As String
            Get
                Return myValue
            End Get
        End Property

        Public Overrides ReadOnly Property RenderDefaultValue() As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides WriteOnly Property SetValue() As String
            Set(ByVal value As String)
                Me.Value = value
            End Set
        End Property

        '''' <summary>
        '''' Propiedad con el valor default en la tabla
        '''' </summary>
        '''' <value></value>
        '''' <returns>El valor default</returns>
        '''' <remarks></remarks>
        'Public Shadows ReadOnly Property DefaultValue() As String
        '    Get
        '        Return myDefaultValue
        '    End Get
        'End Property

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

    End Class

End Namespace