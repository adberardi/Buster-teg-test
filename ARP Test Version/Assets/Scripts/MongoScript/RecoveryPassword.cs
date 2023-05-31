using UnityEngine;
using System.Net;
using System.Net.Mail;

public class RecoveryPassword : MonoBehaviour
{
    public string senderEmail = "domingoberar@hotmail.com";
    public string senderPassword = "stkdadiiridhqhqf";
    public string recipientEmail = "antoberar@gmail.com";
    public string subject = "Test Email";
    public string body = "Hello, this is a test email.";

    public void SendEmail()
    {
        MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, body);
        SmtpClient client = new SmtpClient("smtp.office365.com", 587);
        client.EnableSsl = true; // This is required for Hotmail/Outlook
        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
        client.Send(mail);
    }
}
