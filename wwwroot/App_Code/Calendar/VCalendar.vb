Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO

Namespace Orbelink.Calendar

    Public Class VCalendar

        Public VEventCollection As List(Of VEvent)

        Sub New()
            VEventCollection = New List(Of VEvent)
        End Sub

        Public Sub Render(ByRef output As StreamWriter)
            output.WriteLine("BEGIN:VCALENDAR")
            output.WriteLine("PRODID:-//Orbelink//OrbeCalendar 1.0//EN")
            output.WriteLine("VERSION:2.0")
            output.WriteLine("CALSCALE:GREGORIAN")
            output.WriteLine("METHOD:REQUEST")

            For Each att As VEvent In VEventCollection
                att.Render(output)
            Next

            output.WriteLine("END:VCALENDAR")
        End Sub

    End Class

End Namespace
