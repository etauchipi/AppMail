# 📧 AppMail

## 1. Descripción
**AppMail** es una pequeña pero eficiente biblioteca de clases (Class Library) escrita en VB.NET que simplifica el proceso de envío de correos electrónicos a través del protocolo SMTP. Su objetivo es proporcionar una interfaz sencilla que abstrae la configuración compleja de `System.Net.Mail.SmtpClient`. Al utilizar la clase `dtSMTPMailServer` para definir la configuración del servidor, los desarrolladores pueden enviar mensajes encapsulados en objetos `MailMessage` de forma segura y rápida con tan solo llamar a un método.

## 2. Pila Tecnológica (Tech Stack)
* **Lenguaje de Programación:** Visual Basic .NET (VB.NET)
* **Framework:** .NET Framework 4.5.2
* **Espacios de nombres utilizados:**
  * `System.Net` (Para el manejo de `NetworkCredential`)
  * `System.Net.Mail` (Para la creación de instancias de `SmtpClient` y `MailMessage`)
* **Herramientas de construcción (Build Tools):** MSBuild / Visual Studio

## 3. Instalación y Configuración del Entorno

Sigue estos pasos para compilar e integrar la librería en tu entorno de desarrollo local.

### Prerrequisitos
- Tener instalado Microsoft Visual Studio (versión 2015 o superior es recomendada) o las Build Tools de MSBuild.
- Tener instalado el **.NET Framework 4.5.2 Developer Pack**.

### Paso a paso
1. **Clonar el proyecto:**
   Abre una terminal o consola y ejecuta:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd AppMail
   ```

2. **Compilación mediante línea de comandos (MSBuild):**
   Si posees MSBuild en tus variables de entorno, puedes compilar la solución ejecutando en la raíz del proyecto:
   ```bash
   msbuild AppMail.sln /p:Configuration=Release
   ```
   *Nota: Si utilizas `dotnet build`, ten en cuenta que puede fallar si el Developer Pack de .NET 4.5.2 no está instalado en el entorno de CLI de .NET.*

3. **Compilación a través de Visual Studio:**
   - Haz doble clic en el archivo `AppMail.sln` para abrir el proyecto.
   - En el menú principal, selecciona `Compilar` -> `Compilar solución` (o presiona `Ctrl + Shift + B`).

4. **Variables y dependencias:**
   El proyecto no requiere configurar variables de entorno en el sistema operativo para ejecutarse. Las dependencias externas se limitan al framework base de .NET (`System.Net`, `System.Net.Mail`, etc.). Todas las variables necesarias (como Host, Usuario, Contraseña y Puerto SMTP) se pasan en tiempo de ejecución a través del código instanciando la clase `dtSMTPMailServer`.

### Salida (Output)
La compilación generará un archivo dinámico `AppMail.dll` dentro del directorio `AppMail/bin/Debug/` o `AppMail/bin/Release/` dependiendo de la configuración elegida. Este `.dll` es el que debe referenciarse en tus aplicaciones cliente (WinForms, WPF, ASP.NET, etc.).

## 4. Estructura de Carpetas

A continuación, se detalla la estructura principal del código fuente:

```text
/ (Raíz del Repositorio)
├── AppMail.sln             # Archivo de solución de Visual Studio que agrupa el proyecto.
├── .gitignore              # Reglas para ignorar archivos de compilación y temporales en Git.
├── .gitattributes          # Configuración de atributos y finales de línea para Git.
├── README.md               # Este archivo de documentación.
└── AppMail/                # Directorio principal del código de la biblioteca.
    ├── AppMail.vb          # Archivo con la lógica base: Contiene la clase AppMail (método SmtpMail_send) y la estructura dtSMTPMailServer.
    ├── AppMail.vbproj      # Archivo de definición de proyecto (referencias, framework target 4.5.2, etc.).
    └── My Project/         # Metadatos del proyecto auto-generados por Visual Studio (AssemblyInfo.vb, Resources.resx, Application.myapp, etc.).
```

## 5. Guía Básica de Uso

Para hacer uso de la biblioteca en cualquier otra aplicación .NET, primero debes **Agregar una referencia** (Add Reference) hacia el archivo `AppMail.dll` compilado.

Luego, puedes implementar el siguiente código en tu proyecto. Aquí un ejemplo en VB.NET (aplicable también a C# traduciendo la sintaxis):

```vbnet
Imports System.Net.Mail
' Asegúrate de importar la librería si está en otro namespace
' Imports AppMail

Module Program
    Sub Main()
        ' 1. Configurar las credenciales y propiedades del servidor SMTP
        Dim servidor As New dtSMTPMailServer()
        servidor.smtp_Host = "smtp.tudominio.com"  ' Ejemplo: smtp.gmail.com
        servidor.smtp_Port = "587"                 ' El puerto, usualmente 587, 465 o 25
        servidor.smtp_User = "tu_correo@tudominio.com"
        servidor.smtp_Password = "tu_contraseña_segura"

        ' 2. Crear el objeto del mensaje
        Dim mensaje As New MailMessage()
        mensaje.From = New MailAddress("tu_correo@tudominio.com", "Tu Nombre")
        mensaje.To.Add(New MailAddress("destino@ejemplo.com", "Destinatario"))
        mensaje.Subject = "Prueba de envío desde AppMail"
        mensaje.Body = "Este es un correo de prueba enviado usando la biblioteca AppMail en .NET 4.5.2."
        ' mensaje.IsBodyHtml = True ' Descomentar si deseas enviar contenido HTML

        ' 3. Inicializar AppMail y realizar el envío
        Dim clienteCorreo As New AppMail()

        Try
            Dim resultado As Boolean = clienteCorreo.SmtpMail_send(servidor, mensaje)

            If resultado Then
                Console.WriteLine("¡Correo enviado exitosamente!")
            Else
                Console.WriteLine("Error: No se pudo enviar el correo.")
            End If
        Catch ex As Exception
            Console.WriteLine("Ocurrió una excepción: " & ex.Message)
        End Try

        Console.ReadLine()
    End Sub
End Module
```
