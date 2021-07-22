Imports System.Net
Imports System.Net.Mail

Public Class AppMail

    Private SmtpServer As SmtpClient

    Public Function SmtpMail_send(servidor As dtSMTPMailServer, mensaje As MailMessage) As Boolean

        Dim retorno As Boolean
        Dim credentials As NetworkCredential

        retorno = True
        credentials = New NetworkCredential(servidor.smtp_User, servidor.smtp_Password)
        SmtpServer = New SmtpClient(servidor.smtp_Host)
        SmtpServer.Port = servidor.smtp_Port
        SmtpServer.Credentials = credentials

        Try
            SmtpServer.Send(mensaje)
        Catch ex As Exception
            retorno = False
        Finally
        End Try

        Return retorno

    End Function

End Class

Public Class dtSMTPMailServer
    Public Property smtp_Host As String
    Public Property smtp_User As String
    Public Property smtp_Password As String
    Public Property smtp_Port As String
End Class
