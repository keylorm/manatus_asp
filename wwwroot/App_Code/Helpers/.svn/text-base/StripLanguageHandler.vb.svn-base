Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6
Imports System.Web.HttpContext

Public Class StripLanguageHandler

    Public Class Tags
        Public TagInicio As String
        Public TagFin As String
        Public Idioma As String

        Public Sub New()
            TagInicio = ""
            TagFin = ""
            Idioma = ""
        End Sub

        Public Sub New(ByVal TagInicio As String, ByVal TagFin As String, ByVal Idioma As String)
            Me.TagInicio = TagInicio
            Me.TagFin = TagFin
            Me.Idioma = Idioma
        End Sub
    End Class

    Public Enum StripType
        Idioma
        TextoSinIdioma
    End Enum

    Public Enum TipoControl
        ComboBox
        ListBox
        CheckBoxList
    End Enum

    Public Enum Tag
        Ingles = 1
        Espanol = 2
    End Enum

    Public ReadOnly Property Ingles() As Tags
        Get
            Dim Lang As New Tags("[en]", "[/en]", "English")
            Return Lang
        End Get
    End Property

    Public ReadOnly Property Espanol() As Tags
        Get
            Dim Lang As New Tags("[es]", "[/es]", "Español")
            Return Lang
        End Get
    End Property

    ''' <summary>
    ''' Retorna instancia con los tags del idioma del usuario logueado, basándose en el SecurityHandler
    ''' </summary>
    ''' <returns>Instancia de tags de idioma</returns>
    ''' <remarks></remarks>
    Public Shared Function Idioma() As Tags
        'Dim Lang As New StripLanguageHandler()
        Select Case CType(Current.Session("id_idioma"), Integer)
            Case 1
                Return Idioma(Global.StripLanguageHandler.Tag.Ingles)
            Case 2
                Return Idioma(Global.StripLanguageHandler.Tag.Espanol)
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Retorna instancia con los tags del idioma que se especifique como parámetro
    ''' </summary>
    ''' <param name="Language">Idioma del que se requieren los tags</param>
    ''' <returns>Instancia de tags de idioma</returns>
    ''' <remarks></remarks>
    Public Shared Function Idioma(ByVal Language As Tag) As Tags
        Dim Lang As New StripLanguageHandler()
        Select Case Language
            Case Tag.Ingles
                Return Lang.Ingles
            Case Tag.Espanol
                Return Lang.Espanol
            Case Else
                Return Nothing
        End Select
    End Function

    <Obsolete("Use version 5.0 or above of the Orbecatalog. ")> _
    Public Shared Function StripLanguage(ByVal Texto As String, ByVal Tipo As StripType) As String
        Dim SubString As String = ""
        Dim PosTagInicio As Integer = -1
        Dim PosTagFin As Integer = -1
        Dim Lang As Tags = Global.StripLanguageHandler.Idioma(Current.Session("id_idioma"))
        Dim StringMenosIdioma As String

        Try
            PosTagInicio = Texto.IndexOf(Lang.TagInicio)
            PosTagFin = Texto.IndexOf(Lang.TagFin)
        Catch ex As Exception
            Lang = Global.StripLanguageHandler.Idioma(Tag.Ingles)
            PosTagInicio = Texto.IndexOf(Lang.TagInicio)
            PosTagFin = Texto.IndexOf(Lang.TagFin)
        End Try

        If (PosTagInicio = -1 Or PosTagFin = -1) And Tipo = StripType.Idioma Then
            Return Nothing
        ElseIf (PosTagInicio = -1 Or PosTagFin = -1) And Tipo = StripType.TextoSinIdioma Then
            Dim items As Array = System.Enum.GetValues(GetType(Tag))
            Dim item As String
            For Each item In items
                If item <> Current.Session("id_idioma") Then
                    Lang = Global.StripLanguageHandler.Idioma(item)
                    PosTagInicio = Texto.IndexOf(Lang.TagInicio)
                    PosTagFin = Texto.IndexOf(Lang.TagFin)
                    Try
                        SubString += Lang.TagInicio + Texto.Substring(PosTagInicio + Lang.TagInicio.Length, (PosTagFin - Lang.TagFin.Length - PosTagInicio + 1)).Replace(Lang.TagFin, "") + Lang.TagFin
                    Catch ex As Exception

                    End Try

                End If
            Next
            Return SubString
        Else

            SubString = Texto.Substring(PosTagInicio + Lang.TagInicio.Length, (PosTagFin - Lang.TagFin.Length - PosTagInicio + 1)).Replace(Lang.TagFin, "")

            Select Case Tipo
                Case StripType.Idioma
                    Return SubString
                Case StripType.TextoSinIdioma
                    StringMenosIdioma = Texto.Replace(Lang.TagInicio & SubString & Lang.TagFin, "")
                    Return StringMenosIdioma
                Case Else
                    Return Nothing
            End Select

        End If
    End Function

    Public Shared Function StripLanguage(ByVal Texto As String, ByVal Idioma As Tag, ByVal Tipo As StripType) As String
        Dim SubString As String = ""
        Dim PosTagInicio As Integer = -1
        Dim PosTagFin As Integer = -1
        Dim Lang As Tags = Global.StripLanguageHandler.Idioma(Idioma)
        Dim StringMenosIdioma As String

        PosTagInicio = Texto.IndexOf(Lang.TagInicio)
        PosTagFin = Texto.IndexOf(Lang.TagFin)

        If (PosTagInicio = -1 Or PosTagFin = -1) And Tipo = StripType.Idioma Then
            Return Nothing
        ElseIf (PosTagInicio = -1 Or PosTagFin = -1) And Tipo = StripType.TextoSinIdioma Then
            Dim items As Array = System.Enum.GetValues(GetType(Tag))
            Dim item As String
            For Each item In items
                If item <> Idioma Then
                    Lang = Global.StripLanguageHandler.Idioma(item)
                    PosTagInicio = Texto.IndexOf(Lang.TagInicio)
                    PosTagFin = Texto.IndexOf(Lang.TagFin)
                    Try
                        SubString += Lang.TagInicio + Texto.Substring(PosTagInicio + Lang.TagInicio.Length, (PosTagFin - Lang.TagFin.Length - PosTagInicio + 1)).Replace(Lang.TagFin, "") + Lang.TagFin
                    Catch ex As Exception

                    End Try

                End If
            Next
            Return SubString
        Else

            SubString = Texto.Substring(PosTagInicio + Lang.TagInicio.Length, (PosTagFin - Lang.TagFin.Length - PosTagInicio + 1)).Replace(Lang.TagFin, "")

            Select Case Tipo
                Case StripType.Idioma
                    Return SubString
                Case StripType.TextoSinIdioma
                    StringMenosIdioma = Texto.Replace(Lang.TagInicio & SubString & Lang.TagFin, "")
                    Return StringMenosIdioma
                Case Else
                    Return Nothing
            End Select

        End If
    End Function

    Public Shared Function GetAttributes(ByVal Control As WebControl) As String
        Return Control.Attributes("texto")
    End Function

    Public Shared Function GetAttributes(ByVal Control As WebControl, ByVal Key As String) As String
        Return Control.Attributes(Key)
    End Function

    Public Shared Sub SetAttributes(ByVal Control As WebControl, ByVal Value As String)
        Control.Attributes.Add("texto", Value)
    End Sub

    Public Shared Sub SetAttributes(ByVal Control As WebControl, ByVal Key As String, ByVal Value As String)
        Control.Attributes.Add(Key, Value)
    End Sub

    Public Shared Function AddTags(ByVal Texto As String, ByVal Idioma As Tag) As String
        Dim Lang As Tags = Global.StripLanguageHandler.Idioma(Idioma)
        Dim NuevoTexto As String = Lang.TagInicio + Texto + Lang.TagFin
        Return NuevoTexto
    End Function

    Public Shared Function AddTags(ByVal Texto As String) As String
        Dim Lang As Tags = Global.StripLanguageHandler.Idioma(Current.Session("id_idioma"))
        Dim NuevoTexto As String = Lang.TagInicio + Texto + Lang.TagFin
        Return NuevoTexto
    End Function

    Public Shared Function AddAllTags(ByVal Texto As String) As String
        Dim Items As Array = GetLanguages()
        Dim Item As String
        Dim Valor As String = ""
        For Each Item In Items
            Valor += Global.StripLanguageHandler.AddTags(Texto, Item)
        Next
        Return Valor
    End Function

    Public Shared Function GetLanguages() As Array
        Dim Items As Array = System.Enum.GetValues(GetType(Tag))
        Return Items
    End Function

    Public Shared Sub LoadControlInfo(ByVal Control As WebControl, ByVal Tipo As TipoControl)
        Dim chk As CheckBoxList = Nothing
        Dim cmb As DropDownList = Nothing
        Dim lst As ListBox = Nothing

        Dim Temp As ListItemCollection = Nothing
        Dim Items As New ListItemCollection()
        Dim Item As New ListItem()
        Dim Item2 As New ListItem()
        Dim Texto As String = ""
        Dim Arreglo As New ArrayList()


        Select Case Tipo
            Case TipoControl.CheckBoxList
                chk = DirectCast(Control, CheckBoxList)
                Temp = chk.Items
            Case TipoControl.ComboBox
                cmb = DirectCast(Control, DropDownList)
                Temp = cmb.Items
            Case TipoControl.ListBox
                lst = DirectCast(Control, ListBox)
                Temp = lst.Items
        End Select

        Dim i As Integer = Temp.Count - 1

        For j As Integer = 0 To i
            Arreglo.Add(Temp.Item(j))
        Next

        Arreglo.Reverse()

        For j As Integer = 0 To i
            Items.Add(Arreglo(j))
        Next

        Do While i >= 0
            Texto = StripLanguage(Items.Item(i).Text, Current.Session("id_idioma"), StripType.Idioma)
            Item = New ListItem(Texto, Items.Item(i).Value)
            Item2 = Items.Item(i)
            'Items.Remove(Items.Item(i))

            Select Case Tipo
                Case TipoControl.CheckBoxList
                    chk.Items.Remove(Item2)
                    chk.Items.Add(Item)
                Case TipoControl.ComboBox
                    cmb.Items.Remove(Item2)
                    cmb.Items.Add(Item)
                Case TipoControl.ListBox
                    lst.Items.Remove(Item2)
                    lst.Items.Add(Item)
            End Select
            i -= 1
        Loop
    End Sub

    'METODOS OBSOLETOS

    <Obsolete("Use version 5.0 or above of the Orbecatalog. ")> _
    Public Shared Sub LoadCombo(ByVal cmb As DropDownList)

        Dim Temp As ListItemCollection = cmb.Items
        Dim Items As New ListItemCollection()
        Dim Item As New ListItem()
        Dim Item2 As New ListItem()
        Dim Texto As String = ""
        Dim Arreglo As New ArrayList()
        Dim i As Integer = Temp.Count - 1

        For j As Integer = 0 To i
            Arreglo.Add(Temp.Item(j))
        Next

        Arreglo.Reverse()

        For j As Integer = 0 To i
            Items.Add(Arreglo(j))
        Next

        Do While i >= 0
            Texto = StripLanguage(Items.Item(i).Text, Current.Session("id_idioma"), StripType.Idioma)
            Item = New ListItem(Texto, Items.Item(i).Value)
            Item2 = Items.Item(i)
            'Items.Remove(Items.Item(i))
            cmb.Items.Remove(Item2)
            cmb.Items.Add(Item)
            i -= 1
        Loop
    End Sub

    <Obsolete("Use version 5.0 or above of the Orbecatalog. ")> _
    Public Shared Sub LoadCheckBoxList(ByVal cbl As CheckBoxList)
        Dim Temp As ListItemCollection = cbl.Items
        Dim Items As New ListItemCollection()
        Dim Item As New ListItem()
        Dim Item2 As New ListItem()
        Dim Texto As String = ""
        Dim Arreglo As New ArrayList()
        Dim i As Integer = Temp.Count - 1

        For j As Integer = 0 To i
            Arreglo.Add(Temp.Item(j))
        Next

        Arreglo.Reverse()

        For j As Integer = 0 To i
            Items.Add(Arreglo(j))
        Next

        Do While i >= 0
            Texto = StripLanguage(Items.Item(i).Text, Current.Session("id_idioma"), StripType.Idioma)
            Item = New ListItem(Texto, Items.Item(i).Value)
            Item2 = Items.Item(i)
            'Items.Remove(Items.Item(i))
            cbl.Items.Remove(Item2)
            cbl.Items.Add(Item)
            i -= 1
        Loop
    End Sub
End Class
