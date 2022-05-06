using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalproject.Models.DAL;

namespace finalproject.Models
{
    public class QandA
    {
        int question_serial_num;
        string question_content;
        string answers;
        int guiding_serial_num;
        public QandA() { }

        public QandA(int question_serial_num, string question_content, string answers, int guiding_serial_num)
        {
            Question_serial_num = question_serial_num;
            Question_content = question_content;
            Answers = answers;
            Guiding_serial_num = guiding_serial_num;
        }

        public int Question_serial_num { get => question_serial_num; set => question_serial_num = value; }
        public string Question_content { get => question_content; set => question_content = value; }
        public string Answers { get => answers; set => answers = value; }
        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }

        public void InsertQandA(List<QandA> qa)
        {
            DataServices ds = new DataServices();
            ds.InsertQandA(qa);

        }
        public List<QandA> Read_QandA(int numProgram)
        {
            DataServices ds = new DataServices();
            return ds.Read_QandA(numProgram);

        }
        public void UpdateQandADetails(List<QandA> qa)
        {
            DataServices ds = new DataServices();
            ds.UpdateQandADetails(qa);
        }

    }
}