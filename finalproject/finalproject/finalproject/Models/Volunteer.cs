using finalproject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
//using MailKit.Net.Smtp;
//using MailKit;
//using MimeKit;

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
        //    string body = $"You password is : {passsword}";
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

        public int InsertEmail()
        {
            DataServices ds = new DataServices();
             this.Volunteer_password = genereateRandomPassword();
            return ds.InsertEmail(this);
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
        public int UpdateVolunteerpassword(string password, string email)
        {
            DataServices ds = new DataServices();
            return ds.UpdateVolunteerpassword(password, email);
        }
    }
}