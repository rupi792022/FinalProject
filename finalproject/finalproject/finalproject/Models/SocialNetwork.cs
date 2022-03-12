using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class SocialNetwork
    {
       string social_name;

        public SocialNetwork(string social_name)
        {
            Social_name = social_name;
        }

        public string Social_name { get => social_name; set => social_name = value; }

    }
}