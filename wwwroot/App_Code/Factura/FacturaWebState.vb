Imports Microsoft.VisualBasic
Imports System.Web.HttpContext

Namespace Orbelink.Control.Facturas
    Public Class FacturaWebState

        Dim _id_Carrito As Integer

        Sub New()
            _id_Carrito = -1
        End Sub

        ''' <summary>
        ''' Regresa el numero de carrito asignado a un cliente de tener alguno
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MyCarrito() As Integer
            Get
                If _id_Carrito < 0 Then
                    If Not Current Is Nothing Then
                        If Not Current.Session Is Nothing Then
                            If Current.Session("id_Carrito") IsNot Nothing Then
                                _id_Carrito = Current.Session("id_Carrito")
                            Else
                                'If Orbelink.Helpers.Cookies.GetCookie("id_Carrito") IsNot Nothing Then
                                '    If Orbelink.Helpers.Cookies.GetCookie("id_Carrito").Length > 0 Then
                                '        _id_Carrito = Orbelink.Helpers.Cookies.GetCookie("id_Carrito")
                                '        Current.Session("id_Carrito") = _id_Carrito
                                '    Else
                                '        Orbelink.Helpers.Cookies.RemoveCookie("id_Carrito")
                                '    End If
                                'End If
                            End If
                        End If
                    End If
                End If

                Return _id_Carrito
            End Get
        End Property

        Friend Sub SetIdCarrito(ByVal id_CarritoTemp As Integer)
            _id_Carrito = id_CarritoTemp
            Current.Session("id_Carrito") = id_CarritoTemp
            'Orbelink.Helpers.Cookies.SetCookie("id_Carrito")
        End Sub

        Public Sub BorrarMyCarrito()
            Current.Session.Remove("id_Carrito")
            'Orbelink.Helpers.Cookies.RemoveCookie("id_Carrito")
        End Sub

    End Class

End Namespace