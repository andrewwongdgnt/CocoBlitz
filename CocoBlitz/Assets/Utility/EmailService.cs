using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class EmailService : MonoBehaviour {

    private static MailMessage mail = new MailMessage();

    public static void SendEmail(string message)
    {

        mail.From = new MailAddress("cocogoapp@gmail.com");
        mail.To.Add("andrew.wong.dgnt@gmail.com");
        mail.Subject = "Coco Go! Stats from: "+ SystemInfo.deviceModel+"-"+SystemInfo.deviceName;
        mail.Body = message;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("cocogoapp@gmail.com", "AndrewAndKelsey11162008") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
        Debug.Log("Email sent successfully");

    }
}
