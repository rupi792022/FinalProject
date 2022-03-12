using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Answered
    {
        string volunteer_email;
        string level_serial_num;
        string question_serial_num;
        int guiding_serial_num;
        string answered_question;
        string date;

        public Answered(string volunteer_email, string level_serial_num, string question_serial_num, int guiding_serial_num, string answered_question, string date)
        {
            Volunteer_email = volunteer_email;
            Level_serial_num = level_serial_num;
            Question_serial_num = question_serial_num;
            Guiding_serial_num = guiding_serial_num;
            Answered_question = answered_question;
            Date = date;
        }

        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
        public string Level_serial_num { get => level_serial_num; set => level_serial_num = value; }
        public string Question_serial_num { get => question_serial_num; set => question_serial_num = value; }
        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }
        public string Answered_question { get => answered_question; set => answered_question = value; }
        public string Date { get => date; set => date = value; }
    }
}