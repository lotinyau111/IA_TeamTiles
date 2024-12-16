using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UnityEngine;

public class Mailing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //        if (Input.GetKey(KeyCode.E)) SendEmail("lotinyau111@gmail.com", "Tin Yau", "Create Account", "Thank you for creating account. ");


    }

    public void SendEmail(string receipient, string receipientName, string subject, string body)
    {
        email(receipient, receipientName, subject, body);
    }

    private void email(string receipient, string receipientName, string subject, string body)
    {
        MailAddress from = new MailAddress("teamtilesproject@gmail.com", "TeamTiles");
        MailAddress to = new MailAddress(receipient, receipientName);

        string host = "smtp.gmail.com";
        NetworkCredential credentials = new NetworkCredential(from.Address, "txbimiydzutjwcpw"); // email address and password for sending email.

        using (MailMessage message = new MailMessage(from, to))
        {
            message.Subject = subject;
            message.Body = body;
            message.CC.Add(from);
            message.IsBodyHtml = true;

            using (SmtpClient mailClient = new SmtpClient(host, 587))
            {
                mailClient.Credentials = credentials;
                mailClient.EnableSsl = true;
                

                try { mailClient.Send(message); print("sent"); }
                catch (Exception e) { print(e.StackTrace); }
            }
        }

       
    }
}