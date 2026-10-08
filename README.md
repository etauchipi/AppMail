# AppMail

## 1. Descripción
**AppMail** es una biblioteca de clases (Class Library) sencilla y eficiente desarrollada en Visual Basic .NET. Su propósito principal es facilitar el envío de correos electrónicos mediante el protocolo SMTP. La biblioteca abstrae la complejidad de la configuración de credenciales y conexión, permitiendo a los desarrolladores enviar correos con un único método a través de la clase `AppMail` y su estructura de configuración `dtSMTPMailServer`.

## 2. Pila Tecnológica (Tech Stack)
Al analizar el código fuente, la pila tecnológica identificada es:
* **Lenguaje:** Visual Basic .NET (VB.NET)
* **Framework:** .NET Framework 4.5.2
* **Entorno:** Microsoft Visual Studio (archivos `.sln` y `.vbproj`)
* **Dependencias Principales:**
  * `System.Net` (Para la gestión de credenciales de red mediante `NetworkCredential`).
  * `System.Net.Mail` (Para la gestión del cliente y los mensajes mediante `SmtpClient` y `MailMessage`).

## 3. Instrucciones de Instalación y Configuración Local
Dado que el proyecto es una librería de clases, no se ejecuta por sí sola, sino que está pensada para ser consumida desde otras aplicaciones .NET (como WebForms, WinForms o aplicaciones de Consola).

### Paso a paso para instalación local:
1. **Clonar el repositorio:**
   Abre una terminal y ejecuta:
   ```bash
   git clone <url-del-repositorio>
   cd AppMail
   ```

2. **Abrir el proyecto:**
   Abre el archivo `AppMail.sln` en tu IDE (recomendado: Microsoft Visual Studio 2015 o superior).

3. **Compilar el código:**
   * En Visual Studio, ve al menú **Compilar** y selecciona **Compilar solución** (o presiona `Ctrl + Shift + B`).
   * Como alternativa en la línea de comandos, si tienes MSBuild configurado en el PATH o desde la consola de desarrollador de Visual Studio:
     ```bash
     msbuild AppMail.sln /p:Configuration=Release
     ```
   * Tras la compilación, se generará el archivo `AppMail.dll` dentro del directorio `AppMail/bin/Debug/` (o `AppMail/bin/Release/`).

4. **Configuración del entorno (Variables y Dependencias):**
   * **Variables de entorno:** La biblioteca no requiere variables de entorno del sistema (como un `.env`). Toda la configuración (host SMTP, usuario, contraseña, puerto) se inyecta programáticamente a través de las propiedades de la clase `dtSMTPMailServer` antes de enviar el correo.
   * **Dependencias adicionales:** No requiere paquetes de NuGet externos, solo depende de las bibliotecas base (BCL) incluidas en .NET Framework 4.5.2.

5. **Referenciar la librería en tu proyecto:**
   En tu proyecto destino, da clic derecho en las Referencias, selecciona **Agregar referencia**, busca el archivo `AppMail.dll` recién compilado y agrégalo. O bien, puedes incluir el proyecto `AppMail.vbproj` en tu misma solución.

## 4. Estructura Principal de Carpetas
El repositorio tiene la siguiente estructura básica de archivos:

```text
/
├── .gitattributes      # Reglas para Git (fines de línea, etc.)
├── .gitignore          # Archivos que Git debe ignorar (bin, obj, etc.)
├── AppMail.sln         # Solución principal de Visual Studio
├── README.md           # Este archivo de documentación
└── AppMail/            # Directorio principal del código fuente
    ├── AppMail.vbproj  # Definición del proyecto (configuraciones, frameworks)
    ├── AppMail.vb      # Archivo fuente con la lógica principal (clases AppMail y dtSMTPMailServer)
    └── My Project/     # Metadatos del ensamblado, configuraciones y recursos de Visual Studio
```

## 5. Guía Básica de Uso (Ejemplo Práctico)

A continuación, un ejemplo de cómo instanciar y usar las clases expuestas por la librería para enviar un correo electrónico.

```vbnet
Imports System.Net.Mail
' Asegúrate de importar también el namespace si es diferente al proyecto raíz
' Imports AppMail

Module Program
    Sub Main()
        ' 1. Configurar los parámetros del servidor SMTP
        Dim configSmtp As New dtSMTPMailServer()
        configSmtp.smtp_Host = "smtp.tudominio.com" ' Ejemplo: smtp.gmail.com
        configSmtp.smtp_Port = "587" ' El puerto que requiera el proveedor (25, 465, 587...)
        configSmtp.smtp_User = "tu_correo@tudominio.com"
        configSmtp.smtp_Password = "tu_password_seguro"

        ' 2. Armar el mensaje de correo
        Dim mensaje As New MailMessage()
        mensaje.From = New MailAddress("tu_correo@tudominio.com")
        mensaje.To.Add("destinatario@ejemplo.com")
        mensaje.Subject = "Prueba de envío usando AppMail"
        mensaje.Body = "Este es un correo de prueba enviado exitosamente usando la librería AppMail."

        ' Si deseas enviar el cuerpo en formato HTML descomenta la siguiente línea:
        ' mensaje.IsBodyHtml = True

        ' 3. Instanciar la clase principal y enviar
        Dim enviadorMail As New AppMail()
        Dim envioExitoso As Boolean = enviadorMail.SmtpMail_send(configSmtp, mensaje)

        ' 4. Validar el resultado
        If envioExitoso Then
            Console.WriteLine("El correo se envió correctamente.")
        Else
            Console.WriteLine("Hubo un fallo al enviar el correo. Verifica los datos de configuración.")
        End If

        Console.ReadLine()
    End Sub
End Module
```
