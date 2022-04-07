using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalproject.Models.DAL;

namespace finalproject.Models
{
    public class GuidingProgram
    {
        string file_path;
        string program_name;
        string manager_email;
        int level_serial_num;
        string content_level;
        string question_content_1;
        string question_content_2;
        string question_content_3;
        string question_content_4;
        string answers_1;
        string answers_2;
        string answers_3;
        string answers_4;

        public GuidingProgram(string file_path, string program_name, string manager_email, int level_serial_num, string content_level, string question_content_1, string question_content_2, string question_content_3, string question_content_4, string answers_1, string answers_2, string answers_3, string answers_4)
        {
            File_path = file_path;
            Program_name = program_name;
            Manager_email = manager_email;
            Level_serial_num = level_serial_num;
            Content_level = content_level;
            Question_content_1 = question_content_1;
            Question_content_2 = question_content_2;
            Question_content_3 = question_content_3;
            Question_content_4 = question_content_4;
            Answers_1 = answers_1;
            Answers_2 = answers_2;
            Answers_3 = answers_3;
            Answers_4 = answers_4;
        }
        public GuidingProgram() { }
        public string File_path { get => file_path; set => file_path = value; }
        public string Program_name { get => program_name; set => program_name = value; }
        public string Manager_email { get => manager_email; set => manager_email = value; }
        public int Level_serial_num { get => level_serial_num; set => level_serial_num = value; }
        public string Content_level { get => content_level; set => content_level = value; }
        public string Question_content_1 { get => question_content_1; set => question_content_1 = value; }
        public string Question_content_2 { get => question_content_2; set => question_content_2 = value; }
        public string Question_content_3 { get => question_content_3; set => question_content_3 = value; }
        public string Question_content_4 { get => question_content_4; set => question_content_4 = value; }
        public string Answers_1 { get => answers_1; set => answers_1 = value; }
        public string Answers_2 { get => answers_2; set => answers_2 = value; }
        public string Answers_3 { get => answers_3; set => answers_3 = value; }
        public string Answers_4 { get => answers_4; set => answers_4 = value; }

        public void InsertLevel()
        {
            DataServices ds = new DataServices();
            ds.InsertLevel(this);

        }

        public GuidingProgram Read_Level(int level_num)
        {
            DataServices ds = new DataServices();
            return ds.Read_Level(level_num);

        }

        public int Read_maxLevel()
        {
            DataServices ds = new DataServices();
            return ds.Read_maxLevel();

        }

        public List <GuidingProgram> Read_GP()
        {
            DataServices ds = new DataServices();
            return ds.Read_GP();

        }


        public void UpdateLevelDetails(GuidingProgram gp)
        {
            DataServices ds = new DataServices();
            ds.UpdateLevelDetails(gp);
        }

        public void DeleteProgram ()
        {
            DataServices ds = new DataServices();
            ds.DeleteProgram();
        }
    }
}