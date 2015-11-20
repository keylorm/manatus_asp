Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.Archivos
Imports Orbelink.Control.Security

Namespace Orbelink.FrontEnd6
    Public Class PageBaseClass
        Inherits System.Web.UI.Page

        'Instancias de clases
        Protected connection As New SQLServer(Configuration.Config_DefaultConnectionString)
        Protected queryBuilder As QueryBuilder = New QueryBuilder()
        Protected securityHandler As SecurityHandler = New SecurityHandler(Configuration.Config_DefaultConnectionString)
        'Protected rssPublisher As RSSPublisher = Config.rssPublisher
        'Protected dateHandler As DateHandler = Config.dateHandler
        'Protected commonTasks As Orbelink.Helpers.CommonTasks = Config.commonTasks
        Protected crypto As Orbelink.Crypter.Crypto = Configuration.crypto
        Protected MyMaster As MasterBaseClass

        Dim _id_Actual As Integer
        Protected Property id_Actual() As Integer
            Get
                If _id_Actual <= 0 Then
                    If ViewState("id_Actual") IsNot Nothing Then
                        _id_Actual = ViewState("id_Actual")
                    Else
                        ViewState.Add("id_Actual", 0)
                        _id_Actual = 0
                    End If
                End If
                Return _id_Actual
            End Get
            Set(ByVal value As Integer)
                _id_Actual = value
                ViewState("id_Actual") = _id_Actual
            End Set
        End Property

        Protected Sub AddScripts(ByVal level As Integer)
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            For counter = 0 To level - 1
                prefijoNivel &= "../"
            Next

            'Agrega Javascript al head
            Dim jsScript As New Literal
            jsScript.ID = "jsScript"
            jsScript.Text = System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/orbeEvents.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<script type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/interface.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<!--[if lt IE 7]>"
            jsScript.Text &= System.Environment.NewLine & "<script defer type=""text/javascript"" src=""" & prefijoNivel & "orbecatalog/Scripts/pngfix.js" & """>"
            jsScript.Text &= System.Environment.NewLine & "</script>"
            jsScript.Text &= System.Environment.NewLine & "<![endif]-->"

            Header.Controls.Add(jsScript)
            Dim refresh As New HtmlMeta
            refresh.HttpEquiv = "refresh"
            refresh.Content = ((Configuration.TimeOut_Minutes * 60) + 1)

            'Header.Controls.Add(refresh)
        End Sub
    End Class
End Namespace