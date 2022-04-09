using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalproject.Models.DAL;

namespace finalproject.Models
{
    public class PerformsProgram
    {
        string volunteer_email;
        string level_serial_num;
        int score;

        public PerformsProgram() { }
        public PerformsProgram(string volunteer_email, string level_serial_num, int score)
        {
            Volunteer_email = volunteer_email;
            Level_serial_num = level_serial_num;
            Score = score;
        }

        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
        public string Level_serial_num { get => level_serial_num; set => level_serial_num = value; }
        public int Score { get => score; set => score = value; }


        public void InsertPerforms()
        {
            DataServices ds = new DataServices();
            ds.InsertPerforms(this);
        }

        public int Read_maxLevelsPerforms (string email)
        {
            DataServices ds = new DataServices();
            return ds.Read_maxLevelsPerforms(email);
        }
    }
}