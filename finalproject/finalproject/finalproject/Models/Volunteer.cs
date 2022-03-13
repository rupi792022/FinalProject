using finalproject.Models.DAL;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
//using MailKit.Net.Smtp;
//using MailKit;
using System.Net;


namespace finalproject.Models
{
    public class Volunteer
    {
        string volunteer_email;
        string first_name;
        string last_name;
        string date_of_birth;
        string volunteer_password;
        string volunteer_type;
        string gender;
        int phone_number;
        string start_date;
        string team_name;
       
        public Volunteer() { }
        public Volunteer(string volunteer_email, string first_name, string last_name, string date_of_birth, string volunteer_password, string volunteer_type, string gender, int phone_number, string start_date, string team_name)
        {
            Volunteer_email = volunteer_email;
            First_name = first_name;
            Last_name = last_name;
            Date_of_birth = date_of_birth;
            Volunteer_password = volunteer_password;
            Volunteer_type = volunteer_type;
            Gender = gender;
            Phone_number = phone_number;
            Start_date = start_date;
            Team_name = team_name;
        }
       

        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Date_of_birth { get => date_of_birth; set => date_of_birth = value; }
        public string Volunteer_password { get => volunteer_password; set => volunteer_password = value; }
        public string Volunteer_type { get => volunteer_type; set => volunteer_type = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Phone_number { get => phone_number; set => phone_number = value; }
        public string Start_date { get => start_date; set => start_date = value; }
        public string Team_name { get => team_name; set => team_name = value; }

        private static string genereateRandomPassword()
        {
            string allowedCharecters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%^&*";
            StringBuilder sb = new StringBuilder();
            int randomNumber;
            Random random = new Random();
            const int PASSWORD_LENGTH = 8;

            for (int i = 0; i < PASSWORD_LENGTH; i++)
            {
                randomNumber = random.Next(allowedCharecters.Length - 1);
                sb.Append(allowedCharecters[randomNumber]);
            }

            return sb.ToString();
        }

        //private static bool sendEmail(string sendTo, string passsword)
        //{
        //    MimeMessage message = new MimeMessage();

        //    message.From.Add(new MailboxAddress("FinalProject", sendTo));
        //    message.To.Add(MailboxAddress.Parse("sendTo"));
        //    message.Subject = "Registartion Password";
        //    string body = $"Your password is : {passsword}";
        //    message.Body = new TextPart("plain")
        //    {
        //        Text = body
        //    };

        //    SmtpClient client = new SmtpClient();

        //    try
        //    {
        //        client.Connect("smtp.gmail.com", 465, true);
        //        client.Authenticate(EmailInformation.Email, EmailInformation.Password);
        //        client.Send(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        client.Disconnect(true);
        //        client.Dispose();
        //    }
        //    return true;
        //}

        //public static class EmailInformation
        //{
        //    private static readonly string email = "rupi792022@gmail.com";
        //    public static readonly string password = "AdiAmit114";
        //    public static string Email { get => email; }
        //    public static string Password { get => password; }
        //}



        public int sendEmail(string email, string password) //send Email
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

            return -2;
        }

        public int InsertEmail()
        {
            DataServices ds = new DataServices();
            this.Volunteer_password = genereateRandomPassword();
            int tosendEmail = ds.InsertEmail(this);
            if(tosendEmail != -1)
            {
                tosendEmail = sendEmail(this.volunteer_email, this.Volunteer_password);
            }
            return tosendEmail;
            //ask Anat how can we send the email to the user if we do not know if we succsseded connect to the DB
        }
      
        // check if the user exist in the system and if the password is correct
        public int ReadEmailPassword(string email, string password)
        {
            DataServices ds = new DataServices();
            return ds.ReadEmailPassword(email,password);
        }

        // update the user details (mandatory fields)
        public void UpdateVolunteerDetails(Volunteer volunteer)
        {
            DataServices ds = new DataServices();
            ds.UpdateVolunteerDetails(volunteer);
        }

        // update the user password 
        public string Readpassword(string email)
        {
            DataServices ds = new DataServices();
            return ds.Readpassword( email);
        }
    }
}