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
        int guiding_serial_num;
        string content_level;
        public GuidingProgram() { }
        public string File_path { get => file_path; set => file_path = value; }
        public string Program_name { get => program_name; set => program_name = value; }
        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }
        public string Content_level { get => content_level; set => content_level = value; }

        public GuidingProgram(string file_path, string program_name, int guiding_serial_num, string content_level)
        {
            File_path = file_path;
            Program_name = program_name;
            Guiding_serial_num = guiding_serial_num;
            Content_level = content_level;
        }

        public int Read_Max_Program()
        {
            DataServices ds = new DataServices();
            return ds.Read_Max_Program();

        }

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