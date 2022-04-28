using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalproject.Models.DAL;

namespace finalproject.Models
{
    public class QandA
    {
        string question_serial_num;
        string question_content;
        string answers;
        string guiding_serial_num;
        public QandA() { }

        public QandA(string question_serial_num, string question_content, string answers)
        {
            Question_serial_num = question_serial_num;
            Question_content = question_content;
            Answers = answers;
            Guiding_serial_num = guiding_serial_num;
        }

        public string Question_serial_num { get => question_serial_num; set => question_serial_num = value; }
        public string Question_content { get => question_content; set => question_content = value; }
        public string Answers { get => answers; set => answers = value; }
        public string Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }

        public void InsertQandA()
        {
            DataServices ds = new DataServices();
            ds.InsertQandA(this);

        }


    }
}