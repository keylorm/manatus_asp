Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Control.Archivos

    Public Class DocumentHandler
        Implements IHandlerArchivo

        Private Shared _supportedExtension() As String = {"doc", "docx", "xls", "xlsx", "ppt", "pptx", "pdf", "txt", "html"}

        Public ReadOnly Property Identifier() As String Implements IHandlerArchivo.Identifier
            Get
                Return "Document"
            End Get
        End Property

        Public ReadOnly Property SupportedExtension() As String() Implements IHandlerArchivo.SupportedExtension
            Get
                Return _supportedExtension
            End Get
        End Property

        Enum DocumentType As Integer
            NO_VALID_TYPE = -1
            WORD_DOCUMENT = 0
            Word_Microsoft_Office_Open_XML_Format_Document = 1
            Acrobat_Portable_Document_Format = 6
        End Enum

        Public Function GetDocumentType(ByVal theExtension As String) As DocumentType
            Dim theType As DocumentType = DocumentType.NO_VALID_TYPE
            If theExtension IsNot Nothing Then
                If theExtension.Length > 0 Then
                    If theExtension.Chars(0) = "."c Then
                        theExtension = theExtension.Substring(1)
                    End If

                    For counter As Integer = 0 To _supportedExtension.Length - 1
                        If theExtension.ToLower = _supportedExtension(counter) Then
                            theType = counter
                            Exit For
                        End If
                    Next
                End If
            End If
            Return theType
        End Function

        Public Function ReadDocument(ByVal theDocumentPath As String) As String
            Dim extension As String = System.IO.Path.GetExtension(theDocumentPath)
            Dim docType As DocumentType = GetDocumentType(extension)
            Dim content As String = ""

            Select Case docType
                Case DocumentType.Acrobat_Portable_Document_Format
                    content = ReadPDF(theDocumentPath)
            End Select

            Return content
        End Function

        Private Function ReadPDF(ByVal theDocumentPath As String) As String
            'Dim content As String = ""
            'Dim folder As String = System.IO.Path.GetFullPath(theDocumentPath)
            'folder = folder.Substring(0, folder.LastIndexOf("\") + 1)
            'Dim outFile As String = folder & System.IO.Path.GetFileNameWithoutExtension(theDocumentPath) + ".txt"

            'Try
            '    Dim pdf As CPDF = New CPDF()
            '    ' You can either use events or declare a callback function.
            '    'pdf.SetOnErrorProc(AddressOf PDFError)
            '    pdf.CreateNewPDFA(Nothing) ' We do not create a PDF file in this example

            '    ' We import the contents only, without conversion of pages to templates
            '    pdf.SetImportFlags(CPDF.TImportFlags.ifContentOnly Or CPDF.TImportFlags.ifImportAsPage)
            '    If (pdf.OpenImportFileA(theDocumentPath, CPDF.TPwdType.ptOpen, Nothing) < 0) Then
            '        'Console.Write("Input file not found!" + Chr(10))
            '        'Console.Read()
            '        'Exit Function
            '    End If
            '    pdf.ImportPDFFile(1, 1.0, 1.0)
            '    pdf.CloseImportFile()

            '    Dim parser As New CPDFToText1(pdf)
            '    ' Open the output text file.
            '    parser.Open(outFile)

            '    Dim i As Integer, pageCount As Integer
            '    pageCount = pdf.GetPageCount()
            '    For i = 1 To pageCount
            '        pdf.EditPage(i) ' Open the page
            '        parser.WritePageIdentifier(String.Format("%----------------------- Page {0} -----------------------------" + Chr(13) + Chr(10), i))
            '        parser.ParsePage()
            '        pdf.EndPage() ' Close the page
            '    Next i
            '    parser.Close()
            '    'Console.WriteLine("Text successfully extracted to ""{0}""!", outFile)

            '    Dim reader As New System.IO.StreamReader(outFile)
            '    content = reader.ReadToEnd()
            '    reader.Close()

            'Catch e As Exception
            '    'Console.WriteLine(e.Message)
            'End Try
            ''Console.Read()

            'Return content
        End Function

        Public Function ApplyConfig(ByVal orignalFilePath As String, ByVal theArchivoConfiguration As Entity.Archivos.Archivo_Configuration, ByVal thumbFilePath As String) As Boolean Implements IHandlerArchivo.ApplyConfig
            Return True
        End Function
    End Class

End Namespace