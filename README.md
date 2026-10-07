# AppMail

## 1. Descripción
**AppMail** es una biblioteca de clases ligera y sencilla desarrollada en Visual Basic .NET. Su propósito principal es facilitar el envío de correos electrónicos a través de un servidor SMTP. Proporciona una forma sencilla de encapsular la lógica de configuración de credenciales, puertos y conexión, exponiendo un único método para el envío mediante la clase `AppMail` y la estructura de configuración `dtSMTPMailServer`.

## 2. Pila Tecnológica (Tech Stack)
* **Lenguaje:** Visual Basic .NET (VB.NET)
* **Framework:** .NET Framework 4.5.2
* **Entorno de Desarrollo:** Visual Studio (formato de solución `.sln`)
* **Dependencias y librerías principales:**
  * `System.Net` (Para credenciales de red)
  * `System.Net.Mail` (Para la creación y envío de correos, a través de `SmtpClient` y `MailMessage`)

## 3. Instrucciones de Instalación y Configuración

Al ser una biblioteca de clases (Class Library), su uso principal es ser integrada y referenciada dentro de otras aplicaciones .NET (como aplicaciones de consola, WinForms, WebForms, etc.).

**Pasos para el entorno local:**

1. **Clonar el repositorio:**
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd AppMail
   ```

2. **Abrir la solución:**
   * Abre el archivo `AppMail.sln` usando Microsoft Visual Studio (versión 2015 o superior es recomendada).

3. **Construir (Build) el proyecto:**
   * En Visual Studio, ve a **Compilar > Compilar solución** (o presiona `Ctrl + Shift + B`).
   * Esto compilará el código y generará el archivo `AppMail.dll` en la ruta `AppMail/bin/Debug/` (o `Release/` si cambias la configuración).

4. **Variables y Configuración del Entorno:**
   No se requieren variables de entorno a nivel de sistema. La configuración del servidor SMTP se realiza de forma programática utilizando la clase `dtSMTPMailServer` antes de intentar enviar el correo. Las credenciales (`Usuario` y `Password`) y parámetros como el `Host` y el `Puerto` son administrados dinámicamente en tiempo de ejecución.

5. **Integración en otro proyecto:**
   * Haz clic derecho en **Referencias** de tu proyecto principal en Visual Studio.
   * Selecciona **Agregar referencia...** > **Examinar** y busca el archivo `AppMail.dll` generado.
   * (Alternativa) También puedes copiar y agregar directamente el archivo `AppMail.vb` a tu proyecto para evitar dependencias de DLL externas.

## 4. Estructura de Carpetas

* `/` (Raíz del Repositorio)
  * `AppMail.sln`: Archivo principal de la solución de Visual Studio.
  * `.gitignore` / `.gitattributes`: Archivos de configuración para el control de versiones con Git.
* `AppMail/` (Carpeta del Proyecto)
  * `AppMail.vb`: **Archivo principal del código fuente.** Contiene las clases `AppMail` y `dtSMTPMailServer`, donde reside toda la lógica de negocio para conectarse al SMTP y enviar el mensaje.
  * `AppMail.vbproj`: Archivo del proyecto de Visual Studio (define el Target Framework y configuraciones de compilación).
  * `My Project/`: Carpeta auto-generada por Visual Studio que contiene recursos, configuraciones globales de la aplicación e información de ensamblado (`AssemblyInfo.vb`).

## 5. Guía Básica de Uso (Ejemplo)

Para utilizar esta biblioteca, debes instanciar y poblar un objeto de configuración del servidor (`dtSMTPMailServer`), crear un objeto de correo nativo de .NET (`System.Net.Mail.MailMessage`), y luego usar el método `SmtpMail_send`.

A continuación, un ejemplo de código básico integrando la biblioteca:

```vbnet
Imports System.Net.Mail
' Asegúrate de importar el namespace del proyecto
' Imports AppMail

Module Program
    Sub Main()
        ' 1. Configurar los detalles del servidor SMTP
        Dim servidorSmtp As New dtSMTPMailServer()
        servidorSmtp.smtp_Host = "smtp.tudominio.com"
        servidorSmtp.smtp_Port = "587" ' Usualmente 587 o 25 dependiendo del proveedor
        servidorSmtp.smtp_User = "tu_correo@tudominio.com"
        servidorSmtp.smtp_Password = "tu_password_seguro"

        ' 2. Crear el mensaje
        Dim mensaje As New MailMessage()
        mensaje.From = New MailAddress("tu_correo@tudominio.com")
        mensaje.To.Add("destinatario@ejemplo.com")
        mensaje.Subject = "Prueba de la biblioteca AppMail"
        mensaje.Body = "Este es un correo automático generado con la librería de pruebas."
        ' mensaje.IsBodyHtml = True ' Opcional: si el cuerpo es HTML

        ' 3. Instanciar y enviar
        Dim enviador As New AppMail()
        Dim fueExitoso As Boolean = enviador.SmtpMail_send(servidorSmtp, mensaje)

        ' 4. Verificar resultado
        If fueExitoso Then
            Console.WriteLine("El correo se envió correctamente.")
        Else
            Console.WriteLine("Ocurrió un error al enviar el correo. Verifica las credenciales o el host.")
        End If

        Console.ReadLine()
    End Sub
End Module
```
