using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Level
    {
        int level_serial_num;
        int guiding_serial_num;
        string content_level;

        public Level(int level_serial_num, int guiding_serial_num, string content_level)
        {
            Level_serial_num = level_serial_num;
            Guiding_serial_num = guiding_serial_num;
            Content_level = content_level;
        }

        public int Level_serial_num { get => level_serial_num; set => level_serial_num = value; }
        public int Guiding_serial_num { get => guiding_serial_num; set => guiding_serial_num = value; }
        public string Content_level { get => content_level; set => content_level = value; }
    }



}