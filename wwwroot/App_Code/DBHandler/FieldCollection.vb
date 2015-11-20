Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Contenedor de todos los campos de un DBTable
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class FieldCollection
        Inherits System.Collections.ObjectModel.Collection(Of Field)

        Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Aplica a todos los campos el valor para ToSelect
        ''' </summary>
        ''' <param name="toSelect"></param>
        ''' <remarks></remarks>
        Public Sub SelectAll(Optional ByVal toSelect As Boolean = True)
            Dim counter As Integer = 0
            For counter = 0 To MyBase.Items.Count - 1
                MyBase.Items(counter).ToSelect = toSelect
            Next
        End Sub

        ''' <summary>
        ''' Aplica a todos los campos el valor para ToUpdate
        ''' </summary>
        ''' <param name="toUpdate"></param>
        ''' <remarks></remarks>
        Public Sub UpdateAll(Optional ByVal toUpdate As Boolean = True)
            Dim counter As Integer = 0
            For counter = 0 To MyBase.Items.Count - 1
                MyBase.Items(counter).ToUpdate = toUpdate
            Next
        End Sub

        ''' <summary>
        ''' Ejecuta el metodo Clean de todos los fields contenidos. (No limpia la lista)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearAll()
            Dim counter As Integer = 0
            For counter = 0 To MyBase.Items.Count - 1
                MyBase.Items(counter).Clear()
            Next
        End Sub

    End Class

End Namespace
