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
        int guiding_serial_num;
        int score;

        public PerformsProgram(string volunteer_email, int guiding_serial_num, int score)
        {
            Volunteer_email = volunteer_email;
            Guiding_serial_num = guiding_serial_num;
            Score = score;
        }

        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }
        public int Score { get => score; set => score = value; }

        public PerformsProgram() { }
        public void InsertPerforms()
        {
            DataServices ds = new DataServices();
            ds.InsertPerforms(this);
        }

        public int Read_maxProPerforms(string email)
        {
            DataServices ds = new DataServices();
            return ds.Read_maxProPerforms(email);
        }

        public List<PerformsProgram> Read_minScore()
        {
            DataServices ds = new DataServices();
            return ds.Read_minScore();
        }

        public void deletePerforms(string email, int guiding_serial_num)
        {
            DataServices ds = new DataServices();
            ds.deletePerforms(email, guiding_serial_num);
        }


    }
}