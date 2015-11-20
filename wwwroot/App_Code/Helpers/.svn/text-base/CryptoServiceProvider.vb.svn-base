Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Cryptography

'Proveedor de Cifrado.

'Antes de comenzar con la clase trataremos de conocer como trabajan los proveedores de cifrado que proporciona la plataforma punto NET. Existen varios proveedores presentes para ser utilizados, proveedores que trabajan basándose en algoritmos de encripción simétricos: conocido como cifrado de bloques, utilizan una misma clave privada para el cifrado y el descifrado de los datos; o asimétricos: utilizan un par de claves, pública y privada, para realizar el cifrado y descifrado; aquí sólo trabajaremos con algunos de ellos. Más específicamente, con aquellos que implementan algoritmos de encripción simétricos:

'DES (DESCryptoServiceProvider),

'TripleDES (TripleDESCryptoServiceProvider),

'RC2 (RC2CryptoServiceProvider) y

'Rijndael (RijndaelManaged).

'Como es de suponer funcionan basándose en algoritmos de encripción del que toman sus nombres. Así, DESServiceProvider implementa el algoritmo de encripción DES, TripleDESServiceProvider implementa al algoritmo TripleDES y así sucesivamente. 

'Acerca de los algoritmos hay que mencionar:

'DES: Es el algoritmo de encripción más extendido mundialmente. Utiliza bloques de 64 bits, los cuales codifica usando claves de 56 bits y aplicando permutaciones a nivel de bit, mediante tablas de permutaciones y operaciones XOR.

'TripleDES: Consiste en aplicar varias veces el algoritmo DES con diferentes claves al mensaje original.

'RC2: Permite definir el tamaño del bloque a encriptar, el tamaño de la clave utilizada y el número de fases de encriptación.

'Rijndael: Posee una estructura en capas formadas por funciones polinómicas reversibles (tienen inversa) y no lineales.


'Bien, ahora nos toca implementar el encapsulado de los proveedores de cifrado. De lo que se trata es de crear dos clases genéricas, una de ellas, la que se implementa aquí, contendrá un método que devolverá al proveedor apropiado para ser utilizado en la clase que codificaremos más adelante.


Namespace Orbelink.Crypter
    Friend Class CryptoServiceProvider

        Private algorithm As CryptoProvider
        Private cAction As CryptoAction

        ''' <summary>
        ''' Lista con las posibles acciones a realizar dentro de la clase.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Enum CryptoAction As Integer
            Encrypt = 0
            Desencrypt = 1
        End Enum

        ''' <summary>
        ''' Lista con los proveedores de cifrado que proporciona la clase.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Enum CryptoProvider As Integer
            DES = 0
            TripleDES = 1
            RC2 = 2
            Rijndael = 3
        End Enum

        '''<summary>
        '''Contructor por defecto.
        ''' </summary>
        '''<param name="alg">Proveedor (algoritmo) seleccionado.</param>
        '''<param name="action">Acción a realizar.</param>
        Friend Sub New(ByVal alg As CryptoProvider, ByVal action As CryptoAction)
            Me.algorithm = alg
            Me.cAction = action
        End Sub

        'Antes de comenzar con la codificación del único método presente en la clase, debemos comprender que para poder realizar el cifrado de los datos, las clases de criptografía simétrica utilizan un flujo especial llamado: CrytoStream, el cual cifra los datos leídos de una secuencia proporcionada. 
        'Esta secuencia puede ser cualquiera que derive de la clase Stream: FileStream, MemoryStream y NetworkStream. Por otro lado, CryptoStream se inicializa, además de la secuencia mencionada, con una clase que implemente la interfaz ICryptoTransform y una enumeración del tipo CryptoStreamMode, que describe el acceso permitido a CryptoStream.  

        'Para nuestro caso, el método implementado en la clase CryptoServiceProvider, se encargará de devolvernos un objeto ya inicializado de ICryptoTransform. La inicialización se realiza proporcionando, además del tipo de proveedor seleccionado, pasando como parámetros del método, los valores de 
        'la clave privada: la clave secreta del algoritmo simétrico; y del vector de inicialización: Vector de inicialización del algoritmo simétrico. Cuando definimos un proveedor de encripción, utilizamos a dicho proveedor para crear un objeto cifrador o descifrador, de la clase del proveedor seleccionado. 

        'Todos los algoritmo simétricos necesitan de una clave secreta y de un vector de inicialización para realizar el cifrado, explicar que propósito tiene cada uno tomaría mucho tiempo, lo único que debo mencionar al respecto es que al momento de crear los objetos cifradores o descifradores, debemos proporcionar 
        'dicha clave y vector de inicialización en forma de arreglo de bytes (byte array) y no como cadenas de caracteres. Se puede omitir alguna de ellas o ambas, entonces dichos valores son generados aleatoriamente.  

        ''' <summary>
        ''' Define un objeto para la operaciones básicas de transformaciones
        ''' criptográficas.
        ''' </summary>
        ''' <param name="Key">Clave de encripción.</param>
        ''' <param name="IV">Vector de inicialización.</param>
        ''' <returns>Devuelve el objeto que implementa la interfaz ICryptoTransform.
        ''' </returns>
        Friend Function GetServiceProvider(ByVal Key As Byte(), ByVal IV As Byte()) As ICryptoTransform
            '// creamos la variable que contendrá al objeto ICryptoTransform.
            Dim transform As ICryptoTransform = Nothing

            ' // dependiendo del algoritmo seleccionado, se devuelve el objeto adecuado.
            Select Case (Me.algorithm)

                '// Algoritmo DES.
                Case CryptoProvider.DES
                    Dim des As DESCryptoServiceProvider = New DESCryptoServiceProvider()
                    '// dependiendo de la acción a realizar.
                    '// creamos el objeto adecuado.
                    Select Case (cAction)

                        Case CryptoAction.Encrypt '// si estamos cifrando,
                            ' // creamos el objeto cifrador. 
                            transform = des.CreateEncryptor(Key, IV)
                            ' break;
                        Case CryptoAction.Desencrypt '// sí estamos descifrando,
                            '// creamos el objeto descifrador.
                            transform = des.CreateDecryptor(Key, IV)
                            'break;
                    End Select
                    Return transform  '// devolvemos el objeto transform correspondiente.
                    '// algoritmo TripleDES.
                Case CryptoProvider.TripleDES
                    Dim des3 As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
                    Select Case (cAction)

                        Case CryptoAction.Encrypt
                            transform = des3.CreateEncryptor(Key, IV)
                            'break;
                        Case CryptoAction.Desencrypt
                            transform = des3.CreateDecryptor(Key, IV)
                            'break;
                    End Select
                    Return transform
                    '// algoritmo RC2.
                Case CryptoProvider.RC2
                    Dim rc2 As RC2CryptoServiceProvider = New RC2CryptoServiceProvider()
                    Select Case (cAction)

                        Case CryptoAction.Encrypt
                            transform = rc2.CreateEncryptor(Key, IV)
                            ' break;
                        Case CryptoAction.Desencrypt
                            transform = rc2.CreateDecryptor(Key, IV)
                            'break;
                    End Select
                    Return transform
                    '// algoritmo Rijndael.
                Case CryptoProvider.Rijndael
                    Dim rijndael As Rijndael = New RijndaelManaged()
                    Select Case (cAction)

                        Case CryptoAction.Encrypt
                            transform = rijndael.CreateEncryptor(Key, IV)
                            'break;
                        Case CryptoAction.Desencrypt
                            transform = rijndael.CreateDecryptor(Key, IV)
                            'break;
                    End Select
                    Return transform
                Case Else
                    '// en caso que no exista el proveedor seleccionado, generamos
                    '// una excepción para informarlo.
                    Throw New CryptographicException("Error al inicializar al proveedor de cifrado")
            End Select

        End Function
    End Class

End Namespace