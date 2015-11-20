Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Web.HttpContext

Namespace Orbelink.Helpers

    Public Module Cookies

        Public Sub SetCookie(ByRef Var_Sesion As String)
            Dim ExpiryDate As DateTime = Date.Now()
            Dim cookie As New HttpCookie(Var_Sesion)
            cookie.Value = Current.Session(Var_Sesion)
            cookie.Expires = ExpiryDate.AddDays(7)
            Current.Response.Cookies.Add(cookie)
        End Sub

        Public Function GetCookie(ByRef id As String) As String
            If Not Current.Response.Cookies(id) Is Nothing Then
                Return Current.Server.HtmlEncode(Current.Request.Cookies(id).Value)
            End If
            Return Nothing
        End Function

        Public Sub RemoveCookie(ByRef theID As String)
            If Not Current.Response.Cookies(theID) Is Nothing Then
                Current.Response.Cookies(theID).Expires = Date.Now.AddYears(-30)
            End If
        End Sub

        Public Sub RemoveSession(ByRef Var_Sesion As String)
            If Not Current.Session(Var_Sesion) Is Nothing Then
                Current.Session.Remove(Var_Sesion)
            End If
        End Sub

    End Module

End Namespace