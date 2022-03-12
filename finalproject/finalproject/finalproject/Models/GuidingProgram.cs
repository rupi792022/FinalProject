using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class GuidingProgram
    {
       int guiding_serial_num;
       string date;
       string program_name;
       string manager_email;

        public GuidingProgram(int guiding_serial_num, string date, string program_name, string manager_email)
        {
            Guiding_serial_num = guiding_serial_num;
            Date = date;
            Program_name = program_name;
            Manager_email = manager_email;
        }

        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }
        public string Date { get => date; set => date = value; }
        public string Program_name { get => program_name; set => program_name = value; }
        public string Manager_email { get => manager_email; set => manager_email = value; }
    }
}