using finalproject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public int ReadLogInManager(string email, string password)
        {
            DataServices ds = new DataServices();
            return ds.ReadLogInManager(email, password);
        }


    }
}