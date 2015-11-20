Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Helpers

    Public Class JavaScript

        Dim _sources As ArrayList
        Dim _scripts As ArrayList
        Dim _thePage As System.Web.UI.Page
        Dim open As Boolean
        Dim prefijoNivel As String

        Public Sub New(ByRef thePage As System.Web.UI.Page, ByVal level As Integer)
            Dim counter As Integer
            _thePage = thePage
            _sources = New ArrayList
            _scripts = New ArrayList
            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next
        End Sub

        Public Sub AddSource(ByVal sourceDir As String)
            _sources.Add("<script type=""text/javascript"" src=""" & prefijoNivel & sourceDir & """></script>")
        End Sub

        Public Sub AddScript(ByVal theScript As String)
            _scripts.Add(theScript)
        End Sub

        Public Function WriteScripts() As Boolean
            Dim counter As Integer
            For counter = 0 To _sources.Count - 1
                Dim script As New Literal
                script.ID = "script" & counter
                script.Text = _sources(counter)
                _thePage.Header.Controls.Add(script)
            Next

            Dim scriptGeneral As New Literal
            scriptGeneral.ID = "scriptGeneral"
            scriptGeneral.Text = OpenScript()
            For counter = 0 To _scripts.Count - 1
                scriptGeneral.Text &= vbCrLf
                scriptGeneral.Text &= _scripts(counter)
                scriptGeneral.Text &= vbCrLf
            Next
            scriptGeneral.Text &= CloseScript()
            _thePage.Header.Controls.Add(scriptGeneral)
        End Function

        Private Shared Function OpenScript() As String
            Return "<script type=""text/javascript"" >" & vbCrLf
        End Function

        Private Shared Function CloseScript() As String
            Return vbCrLf & "</script>" & vbCrLf & vbCrLf
        End Function

        Public Shared Sub WriteScriptToLiteral(ByVal theScript As String, ByRef theLiteral As Literal)
            theLiteral.Text = OpenScript() & System.Environment.NewLine
            theLiteral.Text &= theScript & System.Environment.NewLine
            theLiteral.Text &= CloseScript()
        End Sub

        Public Shared Function Script_SetIntervalCallback(ByVal scriptID As String, ByVal milisecondsUntilRefresh As Integer, ByVal times As Integer, ByVal theScript As String, ByRef theStopLink As HyperLink) As String
            Dim generatedScript As String = ""
            Dim functionName As String = "Script_SetIntervalCallback_" & scriptID & "()"
            Dim intervalID As String = "Script_SetIntervalID_" & scriptID
            Dim timesVar As String = "Script_SetIntervalTimes_" & scriptID

            'Variable de contador
            If times > 0 Then
                generatedScript &= "var " & timesVar & " = " & times & ";" & System.Environment.NewLine
            End If

            'Funcion a llamar con intervalo
            generatedScript &= "var " & intervalID & " = setInterval('" & functionName & "', " & milisecondsUntilRefresh & ");" & System.Environment.NewLine
            generatedScript &= "function " & functionName & "{" & System.Environment.NewLine

            If times > 0 Then
                generatedScript &= "if (" & timesVar & " > 0){" & System.Environment.NewLine
            End If

            generatedScript &= theScript & System.Environment.NewLine

            If times > 0 Then
                generatedScript &= timesVar & "--;" & System.Environment.NewLine
                generatedScript &= "} else {" & System.Environment.NewLine
                generatedScript &= "clearInterval(" & intervalID & ");" & System.Environment.NewLine
                generatedScript &= "}" & System.Environment.NewLine
            End If
            generatedScript &= "}" & System.Environment.NewLine

            theStopLink.NavigateUrl = "javascript:clearInterval(" & intervalID & ")"

            Return generatedScript

        End Function

    End Class

End Namespace
