using finalproject.Models.DAL;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
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
        string language;
       
        public Volunteer() { }
        public Volunteer(string volunteer_email, string first_name, string last_name, string date_of_birth, string volunteer_password, string volunteer_type, string gender, int phone_number, string start_date, string language)
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
            Language = language;
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
        public string Language { get => language; set => language = value; }

        private static string genereateRandomPassword()
        {
            string allowedCharecters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!*$&";
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

        public bool InsertEmail()
        {
            DataServices ds = new DataServices();
            this.Volunteer_password = genereateRandomPassword();
            bool Emailexist = ds.InsertEmail(this);
            if(Emailexist == false)
            {
                Emailexist = sendEmail(this.Volunteer_email, this.Volunteer_password);
            }
            return Emailexist;
          
        }

        // check if the user exist in DB
        public bool ReadEmail_V(string email)
        {
            DataServices ds = new DataServices();
            return ds.ReadEmail_V(email);

        }

        // check if the user's password is correct
        public bool ReadPassword_V(string email, string password)
        {
            DataServices ds = new DataServices();
            return ds.ReadPassword_V(email,password);

        }

        // check if there is a need to update the user's details
        public bool ReadDetails_V(string email)
        {
            DataServices ds = new DataServices();
            return ds.ReadDetails_V(email);

        }
        // update the user details (mandatory fields)
        public void UpdateVolunteerDetails(Volunteer volunteer)
        {
            DataServices ds = new DataServices();
            ds.UpdateVolunteerDetails(volunteer);
        }

     
    }
}