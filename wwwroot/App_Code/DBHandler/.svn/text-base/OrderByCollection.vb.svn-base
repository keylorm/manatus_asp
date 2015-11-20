Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Contenedor de todos los campos por lo que se ordenara una consulta
    ''' </summary>
    ''' <version>5.1</version>
    ''' <date>12/06/2007</date>
    ''' <remarks></remarks>
    Public Class OrderByCollection
        Inherits System.Collections.ObjectModel.Collection(Of OrderByStructure)

        'Dim classIdentifier As String = "Orbelink.DBHandler.OrderByCollection Error: "

        Public Structure OrderByStructure
            Dim Field As Field
            Dim Order As String
        End Structure

        Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Agrega un field al ordenamiento
        ''' </summary>
        ''' <param name="theField">El field a agregar</param>
        ''' <param name="asc">Si es ascendente</param>
        ''' <remarks></remarks>
        Public Overloads Sub Add(ByVal theField As Orbelink.DBHandler.Field, Optional ByVal asc As Boolean = True)
            Dim theOrderByStructure As OrderByStructure
            theOrderByStructure.Field = theField
            If asc Then
                theOrderByStructure.Order = "ASC"
            Else
                theOrderByStructure.Order = "DESC"
            End If
            MyBase.Add(theOrderByStructure)
        End Sub

    End Class

End Namespace