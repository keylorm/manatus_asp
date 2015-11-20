Imports Microsoft.VisualBasic
Imports System.IO

Namespace Orbelink.Calendar
    Public Class VAttendee

        Sub New(ByVal theName As String, ByVal theEmail As String, ByVal theParticipationStat As Participation)
            _name = theName
            _mail = theEmail
            _partstat = theParticipationStat
        End Sub

        Private _mail As String
        Public ReadOnly Property Email() As String
            Get
                Return _mail
            End Get
        End Property

        Private _name As String
        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Private _partstat As Participation
        Public ReadOnly Property ParticipationStatus() As Participation
            Get
                Return _partstat
            End Get
        End Property

        Public Enum Participation As Integer
            ACCEPTED
            NEEDS_ACTION
        End Enum

        Public Sub Render(ByRef output As StreamWriter)
            output.Write("ATTENDEE;")
            output.Write("CUTYPE=INDIVIDUAL;")
            output.Write("ROLE=REQ-PARTICIPANT;")
            Select Case _partstat
                Case Participation.ACCEPTED
                    output.Write("PARTSTAT=ACCEPTED;")
                Case Participation.NEEDS_ACTION
                    output.Write("PARTSTAT=NEEDS-ACTION;")
            End Select
            output.Write("RSVP=TRUE;")
            output.Write("CN=")
            output.Write(_name)
            output.Write(";")
            output.Write("X-NUM-GUESTS=0")
            output.Write(":")
            output.Write("mailto:")
            output.WriteLine(_mail)
        End Sub


    End Class
End Namespace