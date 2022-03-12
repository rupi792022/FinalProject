using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class ActivityTime
    {
        int activity_time_serial_num;
        string date;
        string beginning_time;
        string end_time;
        string manager_email;
        string volunteer_email;

        public ActivityTime(int activity_time_serial_num, string date, string beginning_time, string end_time, string manager_email, string volunteer_email)
        {
            Activity_time_serial_num = activity_time_serial_num;
            Date = date;
            Beginning_time = beginning_time;
            End_time = end_time;
            Manager_email = manager_email;
            Volunteer_email = volunteer_email;
        }

        public int Activity_time_serial_num { get => activity_time_serial_num; set => activity_time_serial_num = value; }
        public string Date { get => date; set => date = value; }
        public string Beginning_time { get => beginning_time; set => beginning_time = value; }
        public string End_time { get => end_time; set => end_time = value; }
        public string Manager_email { get => manager_email; set => manager_email = value; }
        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
    }



}