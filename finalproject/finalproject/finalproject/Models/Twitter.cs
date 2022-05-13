using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Parameters;
using Tweetinvi.Parameters.Enum;
using Tweetinvi.Parameters.V2;
using Tweetinvi.Models;
using System.Security.Policy;
using Microsoft.Ajax.Utilities;
using Tweetinvi.Exceptions;
using Tweetinvi.Core.Models;
using System.Data;
using System.Runtime.InteropServices;
using finalproject.Models.DAL;


namespace finalproject.Models
{
    public class Twitter
    {
        string lang;
        string contentText;
        string created_at;
        string country;
        string linkUrl;
        string network;
        private static object client;

        public Twitter()
        {
            Network = "Twitter";
        }

        public Twitter(string lang, string contentText, string created_at, string country, string linkUrl, string network)
        {
            Lang = lang;
            ContentText = contentText;
            Created_at = created_at;
            Country = country;
            LinkUrl = linkUrl;
            Network = network;
        }

        public string Lang { get => lang; set => lang = value; }
        public string ContentText { get => contentText; set => contentText = value; }
        public string Created_at { get => created_at; set => created_at = value; }
        public string Country { get => country; set => country = value; }
        public string LinkUrl { get => linkUrl; set => linkUrl = value; }
        public string Network { get => network; set => network = value; }

    }

}