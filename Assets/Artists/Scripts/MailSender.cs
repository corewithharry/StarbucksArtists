using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine.UI;


public class MailSender : MonoBehaviour
{
    public string senderAddress = "youraddress@gmail.com";
    public string recipientAddress = "youraddress@gmail.com";

    public Text customerName;
    public Text customerAddress;
    public Text artwork;
    public string template;
    public Text message;
    private StringBuilder mailContent = new StringBuilder();

    public Text inputError; 
    public Text addressError;


    public void CheckMailAddress()
    {
        if(customerAddress.text.Length != 0)
        {
            var isMailAddress = RegexUtils.IsMailAddress(customerAddress.text);
            if (isMailAddress)
            {

                CheckName();
            }
            else
            {
                inputError.gameObject.SetActive(false);
                addressError.gameObject.SetActive(true);
                return;
            }
        }
        else
        {
            addressError.gameObject.SetActive(false);
            inputError.gameObject.SetActive(true);
            return;
        }
    }

    private void CheckName()
    {
        if (customerName.text.Length != 0)
        {
            Send();
        }
        else
        {
            addressError.gameObject.SetActive(false);
            inputError.gameObject.SetActive(true);
            return;
        }
    }

    private void Send()
    {
        inputError.gameObject.SetActive(false);
        addressError.gameObject.SetActive(false);
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(senderAddress);
        mail.To.Add(recipientAddress);
        mail.Subject = "作品購入希望";

        mailContent.Clear();
        mailContent.Append(template + "\n"); // 購入の連絡がありました.
        mailContent.Append(customerName.text + "　様" + "\n");
        mailContent.Append("メールアドレス：　" + customerAddress.text + "\n");
        mailContent.Append(artwork.text + "\n");
        mailContent.Append("============" + "\n");
        mailContent.Append(message.text); // 本文.

        mail.Body = mailContent.ToString();

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential(senderAddress, "YourPassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);

        //Debug.Log(customerName.text);
        //Debug.Log(customerAddress.text);
        //Debug.Log(mail.Body);
    }
}


public static class RegexUtils
{
    /// <summary>
    /// 指定された文字列がメールアドレスかどうかを返す.
    /// </summary>
    public static bool IsMailAddress(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        return Regex.IsMatch(
            input,
            @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
            RegexOptions.IgnoreCase
        );
    }
}

public class Artwork
{
    public readonly static Artwork Instance = new Artwork();
    public string name;
}