using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class QandA
    {
        string question_serial_num;
        string question_content;
        string answers;
        public QandA() { }
        public QandA(string question_serial_num, string question_content, string answers)
        {
            Question_serial_num = question_serial_num;
            Question_content = question_content;
            Answers = answers;
        }

        public string Question_serial_num { get => question_serial_num; set => question_serial_num = value; }
        public string Question_content { get => question_content; set => question_content = value; }
        public string Answers { get => answers; set => answers = value; }





    }
}