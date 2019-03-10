using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text;

using UnityEngine;
using UnityEngine.UI;


public class MailSender : MonoBehaviour
{
    public string senderAddress = "youraddress@gmail.com"; // アプリ内で送信用に使用するアドレス（お客様のアドレスではない）.
    public string recipientAddress = "youraddress@gmail.com"; // お客様からのメールが届くアドレス.
    public string smtpHost = "smtp.gmail.com";
    public int smtpPort = 587;
    [SerializeField] protected string password = "";

    private StringBuilder mailContent = new StringBuilder();
    private MailMessage mail;
    public Text customerName;
    public Text customerAddress;
    public Text artwork;
    public string template;
    public Text message;

    public Text inputError; 
    public Text addressError;
    public SceneLoader sceneLoader;


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

        mail = new MailMessage();
        CreateMailContent();

        SmtpClient smtpServer = new SmtpClient(smtpHost);
        smtpServer.Port = smtpPort;
        smtpServer.Credentials = new System.Net.NetworkCredential(senderAddress, password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);

        sceneLoader.LoadSentScene();
    }

    private void CreateMailContent()
    {
        mail.From = new MailAddress(senderAddress);
        mail.To.Add(recipientAddress);
        mail.Subject = "作品購入希望";

        mailContent.Clear();
        mailContent.Append(template + "\n"); // 「購入の連絡がありました」.
        mailContent.Append(customerName.text + "　様" + "\n");
        mailContent.Append("メールアドレス：　" + customerAddress.text + "\n");
        mailContent.Append(artwork.text + "\n");
        mailContent.Append("============" + "\n");
        mailContent.Append(message.text); // 本文.

        mail.Body = mailContent.ToString();
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