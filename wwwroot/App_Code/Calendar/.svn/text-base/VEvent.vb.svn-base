Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO

Namespace Orbelink.Calendar

    Public Class VEvent

        Public VAttendeeCollection As List(Of VAttendee)

        Sub New(ByVal theCreated As Date, ByVal theStartDate As Date, ByVal theEndDate As Date, ByVal theOrganizerName As String, ByVal theOrganizarEmail As String, _
                ByVal theLocation As String, ByVal theSummary As String, ByVal theDescription As String)
            _startDate = theStartDate
            _endDate = theEndDate
            _created = theCreated
            _location = theLocation
            _summary = theSummary
            _description = theDescription
            VAttendeeCollection = New List(Of VAttendee)
            VAttendeeCollection.Add(New VAttendee(theOrganizerName, theOrganizarEmail, VAttendee.Participation.ACCEPTED))
        End Sub

        Private _location As String
        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property

        Private _summary As String
        Public Property Summary() As String
            Get
                Return _summary
            End Get
            Set(ByVal value As String)
                _summary = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _created As Date
        Public ReadOnly Property Created() As String
            Get
                Return _created
            End Get
        End Property

        Private _startDate As Date
        Public ReadOnly Property StartDate() As String
            Get
                Return _startDate
            End Get
        End Property

        Private _endDate As Date
        Public ReadOnly Property EndDate() As String
            Get
                Return _endDate
            End Get
        End Property

        Public Sub Render(ByRef output As StreamWriter)
            output.WriteLine("BEGIN:VEVENT")
            output.WriteLine("DTSTART:" + _startDate.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"))
            output.WriteLine("DTEND:" + _endDate.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"))
            output.WriteLine("DTSTAMP:" + _created.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"))
            output.WriteLine("CREATED:" + _created.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"))
            output.WriteLine("LAST-MODIFIED:" + _created.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"))
            output.WriteLine("UID:" + _created.ToUniversalTime().ToString("yyyyMMddHHmmssfff") + "@" + Configuration.Config_ShortSiteName)
            output.WriteLine("CLASS:PRIVATE")
            output.WriteLine("SEQUENCE:0")
            output.WriteLine("TRANSP:OPAQUE")

            Dim organizer As VAttendee = VAttendeeCollection(0)
            output.Write("ORGANIZER;CN=")
            output.Write(organizer.Name)
            output.Write(":mailto:")
            output.WriteLine(organizer.Email)

            For Each att As VAttendee In VAttendeeCollection
                att.Render(output)
            Next

            output.WriteLine("LOCATION:" + _location)
            output.WriteLine("DESCRIPTION:" + _description)
            output.WriteLine("SUMMARY:" + _summary)
            output.WriteLine("STATUS:CONFIRMED")
            output.WriteLine("PRIORITY:3")
            output.WriteLine("END:VEVENT")
        End Sub

    End Class

End Namespace
