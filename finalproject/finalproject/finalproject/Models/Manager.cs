using finalproject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MimeKit;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace finalproject.Models
{
    public class Manager
    {
        string manager_email;
        string first_name;
        string last_name;
        string manager_password;
        string manager_type;

        public Manager() { }
        public Manager(string manager_email, string first_name, string last_name, string manager_password, string manager_type)
        {
            Manager_email = manager_email;
            First_name = first_name;
            Last_name = last_name;
            Manager_password = manager_password;
            Manager_type = manager_type;
        }

        public string Manager_email { get => manager_email; set => manager_email = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Manager_password { get => manager_password; set => manager_password = value; }
        public string Manager_type { get => manager_type; set => manager_type = value; }

        public bool ReadEmail_M(string email)
        {
            DataServices ds = new DataServices();
            return ds.ReadEmail_M(email);
        }
        public bool ReadPassword_M(string email, string password)
        {
            DataServices ds = new DataServices();
            return ds.ReadPassword_M(email, password);
        }

        public bool sendEmail(string email, string password) //send Email
        {
            //    DBservices dbs = new DBservices();
            //    string password = dbs.forgotPassword(email);
            //if (password == null)
            //    return "The email address is not registered. \n Please try again.";
            // Gmail Address from where you send the mail 
            var fromAddress = "rupi792022@gmail.com";
            // any address where the email will be sending       
            var toAddress = email;
            //Password of your gmail address 
            const string fromPassword = "AdiAmit114";
            // Passing the values and make a email formate to display 
            string subject = "Password";
            string body = "From: FOA System" + "\n";
            body += "From Email: " + fromAddress + "\n";
            body += "Subject: " + subject + "\n";
            body += "Your password is: " + password + "\n";
            // smtp settings 
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }

            smtp.Send(fromAddress, toAddress, subject, body);

            return false;
        }

        public string ReadEmail_RpasswordM(string email)
        {
            DataServices ds = new DataServices();
            string DBpassword = ds.ReadEmail_RpasswordM(email);
            bool sendEmailt = sendEmail(email, DBpassword);
            return DBpassword;

        }

    }
}