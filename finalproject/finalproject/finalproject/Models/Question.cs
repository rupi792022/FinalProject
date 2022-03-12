using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Question
    {
        int question_serial_num;
        string question_content;
        string answer_1;
        string answer_2;
        string answer_3;
        string answer_4;

        public Question(int question_serial_num, string question_content, string answer_1, string answer_2, string answer_3, string answer_4)
        {
            Question_serial_num = question_serial_num;
            Question_content = question_content;
            Answer_1 = answer_1;
            Answer_2 = answer_2;
            Answer_3 = answer_3;
            Answer_4 = answer_4;
        }

        public int Question_serial_num { get => question_serial_num; set => question_serial_num = value; }
        public string Question_content { get => question_content; set => question_content = value; }
        public string Answer_1 { get => answer_1; set => answer_1 = value; }
        public string Answer_2 { get => answer_2; set => answer_2 = value; }
        public string Answer_3 { get => answer_3; set => answer_3 = value; }
        public string Answer_4 { get => answer_4; set => answer_4 = value; }



    }
}