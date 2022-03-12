using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class AntisemiticContent
    {
        int antisemitic_content_serial_num;
        string link;
        string description;
        string language;
        string country;
        string social_name;
        string volunteer_email;

        public AntisemiticContent(int antisemitic_content_serial_num, string link, string description, string language, string country, string social_name, string volunteer_email)
        {
            Antisemitic_content_serial_num = antisemitic_content_serial_num;
            Link = link;
            Description = description;
            Language = language;
            Country = country;
            Social_name = social_name;
            Volunteer_email = volunteer_email;
        }

        public int Antisemitic_content_serial_num { get => antisemitic_content_serial_num; set => antisemitic_content_serial_num = value; }
        public string Link { get => link; set => link = value; }
        public string Description { get => description; set => description = value; }
        public string Language { get => language; set => language = value; }
        public string Country { get => country; set => country = value; }
        public string Social_name { get => social_name; set => social_name = value; }
        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }




    }
}