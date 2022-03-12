using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Team
    {
        string team_name;
        int num_of_volunteers;

        public Team(string team_name, int num_of_volunteers)
        {
            Team_name = team_name;
            Num_of_volunteers = num_of_volunteers;
        }

        public string Team_name { get => team_name; set => team_name = value; }
        public int Num_of_volunteers { get => num_of_volunteers; set => num_of_volunteers = value; }
    }
}