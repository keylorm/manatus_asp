Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase Field que simula un campo de una tabla en una base de datos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FieldFloat
        Inherits Field

        Dim classID As String = "FieldFloat "
        Dim myValue As Double
        Dim myDefaultValue As Double
        Dim myWhere As WhereString

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
            Optional ByVal identity As Boolean = False, Optional ByVal theDefaultValue As Double = Double.MinValue)
            MyBase.New(theName, theParentTable, FieldType.FLOAT, allowNulls, identity)
            myWhere = New WhereString(Me)
            myDefaultValue = DefaultValue

            If theDefaultValue = Double.MinValue Then
                Me.Clear()
            End If
        End Sub

        ''' <summary>
        ''' Valor que toma este campo. Verifica dependiendo del tipo de campo
        ''' </summary>
        ''' <value>Valor del campo</value>
        ''' <returns>Valor del campo</returns>
        ''' <remarks></remarks>
        Public Property Value() As Double
            Get
                If Not _isValid Then
                    If Not _allowNulls And _isNull Then
                        Throw New Exception("Error on " & classID & ": Field doesn't allow null value.")
                    End If
                    Throw New Exception("Error on " & classID & ": Field isn't valid.")
                Else
                    Return myValue
                End If
            End Get
            Set(ByVal value As Double)
                myValue = value
                _isNull = False
                _isValid = True
                _toUpdate = True
            End Set
        End Property

        Public Overrides ReadOnly Property RenderValue() As String
            Get
                If _isValid Then
                    Return myValue.ToString
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Overrides ReadOnly Property RenderDefaultValue() As String
            Get
                If myDefaultValue <> Double.MinValue Then
                    Return myDefaultValue.ToString
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Overrides WriteOnly Property SetValue() As String
            Set(ByVal value As String)
                Try
                    Me.Value = CType(value, Double)
                Catch
                    Throw New Exception("Error on " & classID & ": Value must be Double.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Propiedad con el valor default en la tabla
        ''' </summary>
        ''' <value></value>
        ''' <returns>El valor default</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DefaultValue() As Double
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
            myValue = Double.MinValue
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