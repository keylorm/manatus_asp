Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Web.HttpContext

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Clase manejador de lenguajes
    ''' </summary>
    ''' <version>5.0</version>
    ''' <date>07/16/2007</date>
    ''' <remarks></remarks>
    Public Module LanguageHandler

        Dim classID As String = "LanguageHandler "

        Public AllSuffix As String() = { _
            "_es", _
            "_en", _
            "_fr", _
            "_it", _
            "_de", _
            "_ru", _
            "_nl"}

        Public AllCulturalCodes As String() = { _
            "es-CR", _
            "en-US", _
            "fr-FR", _
            "it-IT", _
            "de-DE", _
            "ru-RU", _
            "nl-NL"}

        Public AllLanguagesNames As String() = { _
            "Español", _
            "Ingles", _
            "Frances", _
            "Italiano", _
            "Aleman", _
            "Ruso", _
            "Holandes"}

        Dim _usedLanguagesPrivate As System.Collections.ObjectModel.Collection(Of Language)

        Public Enum Language As Integer
            ESPANOL = 0
            INGLES = 1
            FRANCES = 2
            ITALIANO = 3
            ALEMAN = 4
            RUSO = 5
            HOLANDES = 6
        End Enum

        Public ReadOnly Property CurrentLanguageSufix() As String
            Get
                Return AllSuffix(CurrentLanguage)
            End Get
        End Property

        Public ReadOnly Property CurrentLanguageCultureCode() As String
            Get
                Return AllCulturalCodes(CurrentLanguage)
            End Get
        End Property

        Public ReadOnly Property CurrentLanguageName() As String
            Get
                Return AllLanguagesNames(CurrentLanguage)
            End Get
        End Property

        Private ReadOnly Property UsedLanguagesPrivate() As System.Collections.ObjectModel.Collection(Of Language)
            Get
                If _usedLanguagesPrivate Is Nothing Then
                    _usedLanguagesPrivate = New System.Collections.ObjectModel.Collection(Of Language)
                    _usedLanguagesPrivate.Add(Configuration.DefaultLang)
                End If
                Return _usedLanguagesPrivate
            End Get
        End Property

        Public ReadOnly Property UsedLanguages(ByVal indice As Integer) As Language
            Get
                Return UsedLanguagesPrivate(indice)
            End Get
        End Property

        Public Sub AddUsedLanguages(ByVal theLanguage As Language)
            Dim yaEsta As Boolean = False
            For counter As Integer = 0 To UsedLanguagesPrivate.Count - 1
                If UsedLanguagesPrivate(counter) = theLanguage Then
                    yaEsta = True
                End If
            Next
            If Not yaEsta Then
                UsedLanguagesPrivate.Add(theLanguage)
            End If
        End Sub

        Public Property CurrentLanguage() As Language
            Get
                If Current IsNot Nothing Then
                    If Current.Session("currentLanguage") IsNot Nothing Then
                        Return Current.Session("currentLanguage")
                    Else
                        Return Configuration.DefaultLang
                    End If
                Else
                    Return Configuration.DefaultLang
                End If
            End Get
            Set(ByVal value As Language)
                If Current.Session("currentLanguage") IsNot Nothing Then
                    Current.Session("currentLanguage") = value
                Else
                    Current.Session.Add("currentLanguage", value)
                End If

            End Set
        End Property

        Public Function UsedLanguagesSufix() As String()
            Dim theSufix(UsedLanguagesPrivate.Count - 1) As String
            For counter As Integer = 0 To UsedLanguagesPrivate.Count - 1
                theSufix(counter) = AllSuffix(UsedLanguagesPrivate(counter))
            Next
            Return theSufix
        End Function

        Public Sub ClearUsedLanguagesPrivate()
            _usedLanguagesPrivate.Clear()
        End Sub

        Public Sub LoadDDL(ByRef theDDL As DropDownList, ByVal addEventHandler As Boolean)
            LoadList(theDDL.Items)

            If addEventHandler Then
                AddHandler theDDL.SelectedIndexChanged, AddressOf theDDL_SelectedIndexChanged
            End If
        End Sub

        Public Sub LoadList(ByRef theList As ListItemCollection)
            For counter As Integer = 0 To UsedLanguagesPrivate.Count - 1
                theList.Add(AllLanguagesNames(UsedLanguagesPrivate(counter)))
                theList(counter).Value = UsedLanguagesPrivate(counter)
            Next
        End Sub

        Public Sub LoadList_All(ByRef theList As ListItemCollection)
            For counter As Integer = 0 To AllLanguagesNames.Length - 1
                theList.Add(AllLanguagesNames(counter))
                theList(counter).Value = counter
            Next
        End Sub

        Public Sub theDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim theDDL As DropDownList = sender
            CurrentLanguage = theDDL.SelectedValue
        End Sub

    End Module

End Namespace