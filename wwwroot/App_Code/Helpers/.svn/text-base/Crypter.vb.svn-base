Imports Microsoft.VisualBasic
'Imports System.Text
Imports System.IO
Imports System.Security.Cryptography

Namespace Orbelink.Crypter

    ''' <summary>
    ''' Esta clase hará uso de la clase escrita anteriormente creando los objetos que permitirán cifrar los flujos con la información. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Crypto
        Private stringKey As String
        Private stringIV As String
        Private algorithm As CryptoProvider

        ''' <summary>
        ''' Proveedores del Servicio de criptografía.
        ''' </summary>
        Public Enum CryptoProvider As Integer
            DES = 0
            TripleDES = 1
            RC2 = 2
            Rijndael = 3
        End Enum

        ''' <summary>
        ''' Encripción / Desencripción.
        ''' </summary>
        Public Enum CryptoAction As Integer
            Encrypt = 0
            Desencrypt = 1
        End Enum

        ''' <summary>
        ''' Constructor por defecto.
        ''' </summary>
        ''' <param name="alg">Establece el algoritmo de Encripción a utilizar.</param>
        Public Sub New(ByVal alg As CryptoProvider)
            algorithm = alg
        End Sub

        'Estableceremos dos propiedades, una que establezca o devuelva el valor de la llave de encripción, y otra para el valor del vector de inicialización.

        ''' <summary>
        ''' Propiedad que obtiene o establece el valor de la llave de encripción
        ''' </summary>
        Public Property Key() As String
            Get
                Return stringKey
            End Get
            Set(ByVal value As String)

                stringKey = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que obtiene o establece el valor del vector de inicialización.
        ''' </summary>
        Public Property IV() As String
            Get
                Return stringIV
            End Get
            Set(ByVal value As String)

                stringIV = value
            End Set
        End Property

        'Hasta aquí no hemos entrado aún en lo que ha encriptar se refiere, antes debemos aclarar un tanto los métodos definidos a continuación.

        'Como ya se mencionó, de lo que se trata es de crear un clase genérica que nos permita cifrar y descifrar tanto archivos como cadenas de caracteres. Bien esta clase por tanto, debe contemplar métodos que hagan cada una de esas operaciones. Los valores de la llave y el vector de inicialización serán proporcionado por medio de propiedades escritas anteriormente. No siendo así con el proveedor (algoritmo) seleccionado, que deberá pasarse como parámetro del constructor. Hay que comprender algo al momento de trabajar con estos algoritmos de cifrado simétrico, esto es, la longitud de su llave y vector de inicialización (VI de ahora en adelante); la longitud de la llave varía de un algoritmo a otro, quedando de la siguiente forma:

        'Algoritmo Longitud de la clave 
        'DES Igual a 64 bits (8 bytes) 
        'TripleDES Entre 128 y 192 bits (16 y 24 bytes) 
        'RC2 Entre 64 y 128 bits (8 y 16 bytes) 
        'Rijndael Entre 128 y 256 bits (16 y 32 bytes) 

        'Para el vector de inicialización el caso es el mismo, varía dependiendo del algoritmo del que se trate.

        'Algoritmo Longitud del VI 
        'DES Igual a 64 bits (8 bytes) 
        '        TripleDES()
        '        RC2()
        'Rijndael Igual a 128 bits (16 bytes) 

        'Como se mencionó anteriormente los valores que reciben los métodos que generan las secuencias de encripción, deben ser por fuerza, arreglos de bytes; de modo que se han escrito dos métodos que se encargan de convertir los valores de las clave y del VI, de cadenas tipo string en sus respectivos arreglo de bytes. A su vez comprueban la longitud de la dichos valores y, en caso de ser necesario, completan o truncan su longitud hasta alcanzar valores válidos para el algoritmo en que vayan a ser utilizados. Generan el arreglo de bytes usando los métodos del sistema de codificación UTF8.

        ''' <summary>
        ''' Convierte los valores de tipo string, de la llave de cifrado
        ''' en sus correspondiente byte array.
        ''' </summary>
        ''' <returns>Devuelve el arreglo de bytes correspondiente a la llave de cifrado.</returns>
        Private Function MakeKeyByteArray() As Byte()

            ' dependiendo del algoritmo utilizado.
            Select Case (Me.algorithm)

                ' // para los algoritmos
                Case CryptoProvider.DES
                Case CryptoProvider.RC2
                    ' verificamos que la longitud no sea menor que 8 bytes,
                    If (stringKey.Length < 8) Then
                        ' // de ser así, completamos la cadena hasta un valor válido
                        stringKey = stringKey.PadRight(8)
                    ElseIf (stringKey.Length > 8) Then ' si la cadena supera los 8 bytes,
                        ' truncamos la cadena dejándola en 8 bytes.
                        stringKey = stringKey.Substring(0, 8)

                        ' para los algoritmos
                    End If
                Case CryptoProvider.TripleDES
                Case CryptoProvider.Rijndael
                    ' verificamos que la longitud no sea menor a 16 bytes
                    If (stringKey.Length < 16) Then
                        ' de ser así, completamos la cadena hasta esos 16 bytes.
                        stringKey = stringKey.PadRight(16)
                    ElseIf (stringKey.Length > 16) Then 'longitud es mayor a 16 bytes,
                        ' truncamos la cadena dejándola en 16 bytes.
                        stringKey = stringKey.Substring(0, 16)

                    End If
            End Select

            ' utilizando los métodos del namespace System.Text, 
            ' convertimos la cadena de caracteres en un arreglo de bytes
            ' mediante el método GetBytes() del sistema de codificación UTF.
            Return Encoding.UTF8.GetBytes(stringKey)
        End Function

        ''' <summary>
        ''' Convierte los valores de tipo string, del vector de inicialización
        ''' en sus correspondiente byte array.
        ''' </summary>
        ''' <returns>Devuelve el arreglo de bytes correspondiente al VI.</returns>
        Private Function MakeIVByteArray() As Byte()

            ' // dependiendo del algoritmo utilizado.
            Select Case (algorithm)

                ' para los algoritmos 
                Case CryptoProvider.DES
                Case CryptoProvider.RC2
                Case CryptoProvider.TripleDES
                    ' // verificamos que la longitud no sea menor que 8 bytes, 
                    If (stringIV.Length < 8) Then
                        ' de ser así, completamos la cadena hasta un valor válido
                        stringIV = stringIV.PadRight(8)
                    ElseIf (stringIV.Length > 8) Then 'si la cadena supera los 8 bytes,
                        ' truncamos la cadena dejándola en 8 bytes.
                        stringIV = stringIV.Substring(0, 8)

                    End If
                Case CryptoProvider.Rijndael
                    ' verificamos que la longitud no sea menor que 16 bytes,
                    If (stringIV.Length < 16) Then
                        ' de ser así, completamos la cadena hasta un valor válido
                        stringIV = stringIV.PadRight(16)
                    ElseIf (stringIV.Length > 16) Then 'si la cadena supera los 16 bytes,
                        ' truncamos la cadena dejándola en 16 bytes.
                        stringIV = stringIV.Substring(0, 16)
                    End If

            End Select

            ' utilizando los métodos del namespace System.Text, 
            ' convertimos la cadena de caracteres en un arreglo de bytes
            ' mediante el método GetBytes() del sistema de codificación UTF.
            Return Encoding.UTF8.GetBytes(stringIV)
        End Function

        ''' <summary>
        ''' Cifra la cadena usando el proveedor especificado.
        ''' </summary>
        ''' <param name="CadenaOriginal">Cadena que será cifrada.</param>
        ''' <returns>Devuelve la cadena cifrada.</returns>
        Public Function CifrarCadena(ByVal CadenaOriginal As String) As String
            ' creamos el flujo tomando la memoria como respaldo.
            Dim memStream As MemoryStream = Nothing
            Try
                ' verificamos que la llave y el VI han sido proporcionados.
                If stringKey <> Nothing And stringIV <> Nothing Then


                    ' obtenemos el arreglo de bytes correspondiente a la llave
                    ' // y al vector de inicialización.
                    Dim key As Byte() = MakeKeyByteArray()
                    Dim IV As Byte() = MakeIVByteArray()
                    ' convertimos el mensaje original en sus correspondiente
                    ' arreglo de bytes.
                    Dim textoPlano As Byte() = Encoding.UTF8.GetBytes(CadenaOriginal)
                    ' // creamos el flujo 
                    memStream = New MemoryStream(CadenaOriginal.Length * 2)
                    ' obtenemos nuestro objeto cifrador, usando la clase 
                    ' CryptoServiceProvider codificada anteriormente.
                    Dim cryptoProvider As CryptoServiceProvider = New CryptoServiceProvider(algorithm, CryptoServiceProvider.CryptoAction.Encrypt)
                    Dim transform As ICryptoTransform = cryptoProvider.GetServiceProvider(key, IV)
                    ' creamos el flujo de cifrado, usando el objeto cifrador creado y almancenando
                    ' el resultado en el flujo MemoryStream.
                    Dim cs As CryptoStream = New CryptoStream(memStream, transform, CryptoStreamMode.Write)
                    ' ciframos el mensaje.
                    cs.Write(textoPlano, 0, textoPlano.Length)
                    ' cerramos el flujo de cifrado.
                    cs.Close()

                Else
                    ' si los valores de la llave y/o del VI no se han establecido
                    ' informamos mendiante una excepción.
                    Throw New Exception("Error al inicializar la clave y el vector")
                End If
            Catch ex As Exception
                'Throw
                Return Nothing
            End Try

            ' la conversión se ha realizado con éxito,
            ' convertimos el arreglo de bytes en cadena de caracteres, 
            ' usando la clase Convert. Debido al formato UTF8 utilizado nos valemos
            ' del método ToBase64String para tal fin.
            Return Convert.ToBase64String(memStream.ToArray())

        End Function

        ''' <summary>
        ''' Descifra la cadena usando al proveedor específicado.
        ''' </summary>
        ''' <param name="CadenaCifrada">Cadena con el mensaje cifrado.</param>
        ''' <returns>Devuelve una cadena con el mensaje original.</returns>
        Public Function DescifrarCadena(ByVal CadenaCifrada As String) As String



            ' creamos el flujo tomando la memoria como respaldo.
            Dim memStream As MemoryStream = Nothing
            Try


                ' // verificamos que la llave y el VI han sido proporcionados.
                If (stringKey <> Nothing And stringIV <> Nothing) Then

                    ' obtenemos el arreglo de bytes correspondiente a la llave
                    ' y al vector de inicialización.
                    Dim key As Byte() = MakeKeyByteArray()
                    Dim IV As Byte() = MakeIVByteArray()
                    ' obtenemos el arreglo de bytes de la cadena cifrada.
                    Dim textoCifrado As Byte() = Convert.FromBase64String(CadenaCifrada)

                    ' creamos el flujo
                    memStream = New MemoryStream(CadenaCifrada.Length - 1)
                    ' obtenemos nuestro objeto cifrador, usando la clase 
                    ' CryptoServiceProvider codificada anteriormente.
                    Dim CryptoProvider As CryptoServiceProvider = New CryptoServiceProvider(algorithm, CryptoServiceProvider.CryptoAction.Desencrypt)
                    Dim transform As ICryptoTransform = CryptoProvider.GetServiceProvider(key, IV)
                    ' creamos el flujo de cifrado, usando el objeto cifrador creado y almancenando
                    ' el resultado en el flujo MemoryStream.
                    Dim cs As CryptoStream = New CryptoStream(memStream, transform, CryptoStreamMode.Write)
                    cs.Write(textoCifrado, 0, textoCifrado.Length) ' ciframos
                    cs.Close() ' // cerramos el flujo.

                Else
                    ' si los valores dela llave y/o del VI no se han establecido
                    ' informamos mendiante una excepción.
                    Throw New Exception("Error al inicializar la clave y el vector.")
                End If
            Catch ex As Exception
                'Throw
                Return Nothing
            End Try


            ' utilizamos el mismo sistema de codificación (UTF8) para obtener 
            ' la cadena a partir del byte array.
            Return Encoding.UTF8.GetString(memStream.ToArray())
        End Function

    End Class

End Namespace