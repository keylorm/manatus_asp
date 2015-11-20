Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Una llave foranea
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Class ForeingKey
        Inherits System.Collections.ObjectModel.Collection(Of ForeingKeyStructure)

        Public Const classIdentifier As String = "Orbelink.DBHandler.ScriptBuilder Error: "
        Dim _ReferencesToTable As DBTable
        Dim _deleteRule As DeleteRule

        Public Enum DeleteRule As Integer
            NO_ACTION
            CASCADE
            SET_DEFAULT
            SET_NULL
        End Enum

        Public Structure ForeingKeyStructure
            Dim Field As Field
            Dim ReferencesTo As Field
        End Structure

        Public Sub New(ByRef FKField As Field, ByRef ReferencesTo As Field, Optional ByVal theDeleteRule As DeleteRule = DeleteRule.NO_ACTION)
            MyBase.New()
            _ReferencesToTable = ReferencesTo.ParentTable
            Add_FK(FKField, ReferencesTo)
            _deleteRule = theDeleteRule
        End Sub

        Public Sub Add_FK(ByRef FKField As Field, ByRef ReferencesTo As Field)
            If _ReferencesToTable.TableName = ReferencesTo.ParentTable.TableName Then
                If FKField.Type <> ReferencesTo.Type Then
                    Throw New Exception("Error on " & classIdentifier & ": Column '" & FKField.Name & "' is not the same data type as referencing column.")
                End If
                Dim theFK As ForeingKeyStructure
                theFK.Field = FKField
                theFK.ReferencesTo = ReferencesTo

                MyBase.Add(theFK)
            Else
                Throw New Exception("Error on " & classIdentifier & ": The field parent table is different from the related table")
            End If
        End Sub

        Public ReadOnly Property ReferencesToTable() As DBTable
            Get
                Return _ReferencesToTable
            End Get
        End Property

        Public ReadOnly Property OnDeleteRule() As DeleteRule
            Get
                Return _deleteRule
            End Get
        End Property

    End Class

End Namespace
