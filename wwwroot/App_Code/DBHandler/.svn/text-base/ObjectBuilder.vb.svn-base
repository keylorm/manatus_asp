Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Collections.Generic

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase que arma un instancias de dataTable
    ''' </summary>
    ''' <version>5.1</version>
    ''' <date>12/06/2007</date>
    ''' <remarks></remarks>
    Public Module ObjectBuilder

        Public Const classIdentifier As String = "Orbelink.DBHandler.ObjectBuilder"

        ''' <summary>
        ''' Crea una instancia de un DBTable con un indice espacifico de Row
        ''' </summary>
        ''' <param name="table">El dataTable de donde tomar la informacion</param>
        ''' <param name="theRow">El indice de la fila a tomar la informacion</param>
        ''' <param name="theRegistry">La instancia a la que colocarle la informacion</param>
        ''' <remarks></remarks>
        Public Sub CreateObject(ByVal table As Data.DataTable, ByVal theRow As Integer, ByVal theRegistry As Orbelink.DBHandler.DBTable)
            'Dim excID As String = "Orbelink.DBHandler.ObjectBuilder Error: TransformDataTable: "
            Dim columCounter As Integer = 0
            Dim fieldsCount As Integer
            Dim columnsCount As Integer = table.Columns.Count
            Dim rowsCount As Integer = table.Rows.Count

            If 0 <= theRow And theRow < rowsCount Then
                For columCounter = 0 To columnsCount - 1
                    For fieldsCount = 0 To theRegistry.Fields.Count - 1
                        Dim nombreFieldActual As String = VerifyFieldName(theRegistry.Fields(fieldsCount), True, False)
                        Dim nombreDataSet As String = table.Columns(columCounter).ColumnName

                        If nombreFieldActual.Equals(nombreDataSet) Then
                            If Not table.Rows(theRow).Item(columCounter).GetType.Name = "DBNull" Then
                                theRegistry.Fields(fieldsCount).SetValue = table.Rows(theRow).Item(columCounter)
                            Else
                                theRegistry.Fields(fieldsCount).Clear()
                            End If
                            Exit For
                        End If
                    Next
                Next
            Else
                theRegistry.Fields.ClearAll()
            End If
        End Sub

        Public Function CreateDBTable_BySelectIndex(ByVal theRow As Data.DataRow, ByVal theOriginalDBTable As Orbelink.DBHandler.DBTable) As DBTable
            Dim nuevo As DBTable = theOriginalDBTable.CreateInstance
            For fieldsCounter As Integer = 0 To theOriginalDBTable.Fields.Count - 1
                Dim originalField As Field = theOriginalDBTable.Fields(fieldsCounter)

                If originalField.SelectIndex >= 0 Then
                    If originalField.ToSelect And Not theRow.Item(originalField.SelectIndex).GetType.Name = "DBNull" Then
                        nuevo.Fields(fieldsCounter).SetValue = theRow.Item(originalField.SelectIndex)
                    Else
                        nuevo.Fields(fieldsCounter).Clear()
                    End If
                Else
                    nuevo.Fields(fieldsCounter).Clear()
                End If
            Next
            Return nuevo
        End Function

        ''' <summary>
        ''' Transforma un dataTable en un ArrayList de instancias de DBTable
        ''' </summary>
        ''' <param name="table">El dataTable a transformar</param>
        ''' <param name="theRegistry">Instancia de DBTable a tranformar el dataTable</param>
        ''' <returns>El ArrayList con las instancas. Es un ArrayList de Objetos</returns>
        ''' <remarks></remarks>
        Public Function TransformDataTable(ByVal table As Data.DataTable, ByVal theRegistry As Orbelink.DBHandler.DBTable) As ArrayList
            'Dim excID As String = "Orbelink.DBHandler.ObjectBuilder Error: TransformDataTable: "
            Dim objetos As New ArrayList
            Dim columCounter As Integer
            Dim fieldCounter As Integer

            'Crea tantas instancias como rows devolvio la consulta
            For rowCounter As Integer = 0 To table.Rows.Count - 1
                objetos.Add(theRegistry.CreateInstance)
            Next

            'Recorre todoas las columnas
            For columCounter = 0 To table.Columns.Count - 1

                'Y para cada columna agrega a los registros 
                For fieldCounter = 0 To theRegistry.Fields.Count - 1
                    Dim nombreField As String = VerifyFieldName(theRegistry.Fields(fieldCounter), True, False)
                    Dim nombreColumna As String = table.Columns(columCounter).ColumnName

                    'Compara nombres de columnas
                    If nombreField.Equals(nombreColumna) Then
                        For rowCounter As Integer = 0 To table.Rows.Count - 1
                            If Not table.Rows(rowCounter).Item(columCounter).GetType.Name = "DBNull" Then
                                objetos(rowCounter).Fields(fieldCounter).SetValue = table.Rows(rowCounter).Item(columCounter)
                            Else
                                objetos(rowCounter).Fields(fieldCounter).Clear()
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next

            Return objetos
        End Function

        ''' <summary>
        ''' Transforma un dataTable en un ArrayList de instancias de DBTable
        ''' </summary>
        ''' <param name="table">El dataTable a transformar</param>
        ''' <param name="theRegistry">Instancia de DBTable a tranformar el dataTable</param>
        ''' <returns>El ArrayList con las instancas. Es un ArrayList de Objetos</returns>
        ''' <remarks></remarks>
        Public Function TransformDataTable_ToList(ByRef table As Data.DataTable, ByRef theRegistry As DBTable) As List(Of DBTable)
            'Dim excID As String = "Orbelink.DBHandler.ObjectBuilder Error: TransformDataTable: "
            Dim objetos As New List(Of DBTable)
            Dim columCounter As Integer
            Dim fieldCounter As Integer

            'Crea tantas instancias como rows devolvio la consulta
            For rowCounter As Integer = 0 To table.Rows.Count - 1
                objetos.Add(theRegistry.CreateInstance)
            Next

            'Recorre todoas las columnas
            For columCounter = 0 To table.Columns.Count - 1

                'Y para cada columna agrega a los registros 
                For fieldCounter = 0 To theRegistry.Fields.Count - 1
                    Dim nombreField As String = VerifyFieldName(theRegistry.Fields(fieldCounter), True, False)
                    Dim nombreColumna As String = table.Columns(columCounter).ColumnName

                    'Compara nombres de columnas
                    If nombreField.Equals(nombreColumna) Then
                        For rowCounter As Integer = 0 To table.Rows.Count - 1
                            Dim elDBTable As DBTable = objetos(rowCounter)
                            If Not table.Rows(rowCounter).Item(columCounter).GetType.Name = "DBNull" Then
                                elDBTable.Fields(fieldCounter).SetValue = table.Rows(rowCounter).Item(columCounter)
                            Else
                                elDBTable.Fields(fieldCounter).Clear()
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next

            Return objetos
        End Function

        ''' <summary>
        ''' Transforma un dataTable en una lista de instancias DBTable
        ''' </summary>
        ''' <param name="table">El dataTable a transformar</param>
        ''' <param name="theRegistry">Instancia de DBTable a tranformar el dataTable</param>
        ''' <returns>La lista de instancias</returns>
        ''' <remarks></remarks>
        Public Function TransformDataTable_ToList_RealNames(ByVal table As Data.DataTable, ByVal theRegistry As Orbelink.DBHandler.DBTable) As System.Collections.Generic.List(Of DBTable)
            Dim objetos As New System.Collections.Generic.List(Of DBTable)

            'Crea tantas instancias como rows devolvio la consulta
            For rowCounter As Integer = 0 To table.Rows.Count - 1
                objetos.Add(theRegistry.CreateInstance)
            Next

            'Recorre todoas las columnas
            For columCounter As Integer = 0 To table.Columns.Count - 1

                'Y para cada columna agrega a los registros 
                For fieldCounter As Integer = 0 To theRegistry.Fields.Count - 1
                    Dim nombreField As String = VerifyFieldName(theRegistry.Fields(fieldCounter), True, True)
                    Dim nombreColumna As String = table.Columns(columCounter).ColumnName

                    'Compara nombres de columnas
                    If nombreField.Equals(nombreColumna) Then
                        For rowCounter As Integer = 0 To table.Rows.Count - 1
                            If Not table.Rows(rowCounter).Item(columCounter).GetType.Name = "DBNull" Then
                                objetos(rowCounter).Fields(fieldCounter).SetValue = table.Rows(rowCounter).Item(columCounter)
                            Else
                                objetos(rowCounter).Fields(fieldCounter).Clear()
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next

            Return objetos
        End Function

        ''' <summary>
        ''' Encuentra un valor en un data set
        ''' </summary>
        ''' <param name="valueToFind">El valor por encontrar</param>
        ''' <param name="theDataTable">En cual tabla</param>
        ''' <param name="columnIndex">Indice de columna a buscar</param>
        ''' <returns>Indice de la fila donde se encontro</returns>
        ''' <remarks></remarks>
        Public Function findValueInDataTable(ByVal valueToFind As String, ByVal theDataTable As Data.DataTable, ByVal columnIndex As Integer) As Integer
            Dim count As Integer = theDataTable.Rows.Count()
            Dim realIndex As Integer = -1
            Dim theDataRow As System.Data.DataRow
            Dim currentValue As Integer
            Dim toContinue As Boolean = True
            Dim begin, final, medio As Integer

            begin = 0
            final = count - 1

            While begin <= final And toContinue
                medio = (begin + final) / 2
                theDataRow = theDataTable.Rows(medio)
                currentValue = theDataRow.Item(columnIndex)
                If valueToFind = currentValue Then
                    realIndex = medio
                    toContinue = False
                ElseIf valueToFind > currentValue Then
                    begin = medio + 1
                Else
                    final = medio - 1
                End If
            End While

            Return realIndex
        End Function

        ''' <summary>
        ''' Verifica el nombre con que se consulto un DBField 
        ''' </summary>
        ''' <param name="theField">El field a verificar</param>
        ''' <param name="CheckForFieldAsName">Si desea checkear por un alias</param>
        ''' <param name="CheckForRealName">Si es por un nombre real</param>
        ''' <returns>Como se consulto el field</returns>
        ''' <remarks></remarks>
        Private Function VerifyFieldName(ByRef theField As Orbelink.DBHandler.Field, ByVal CheckForFieldAsName As Boolean, ByVal CheckForRealName As Boolean) As String
            Dim result As String = ""

            If theField.AsName.Length > 0 And CheckForFieldAsName Then
                result &= theField.AsName
            Else
                If CheckForRealName Then
                    result &= theField.RealName
                Else
                    result &= theField.Name
                End If

            End If

            Return result
        End Function
    End Module

End Namespace
