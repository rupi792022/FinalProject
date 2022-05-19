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
        DateTime created_at;
        string country;
        string linkUrl;
        string network;
        string hashtag;



        public Twitter() { }
        public Twitter(string lang, string contentText, DateTime created_at, string country, string linkUrl, string network, string hashtag)
        {
            Lang = lang;
            ContentText = contentText;
            Created_at = created_at;
            Country = country;
            LinkUrl = linkUrl;
            Network = network;
            Hashtag = hashtag;
        }

        public string Lang { get => lang; set => lang = value; }
        public string ContentText { get => contentText; set => contentText = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }

        public string Country { get => country; set => country = value; }
        public string LinkUrl { get => linkUrl; set => linkUrl = value; }
        public string Network { get => network; set => network = value; }
        public string Hashtag { get => hashtag; set => hashtag = value; }

        const string API_key = "hcUmMrTLpUjBoT3x7RtSHzTwF";
        const string API_Key_Secret = "7A0XTPRHfKzRCWyohOaKut5kpukOLMrD9epvW8QwQlbNmU2kEJ";
        const string Bearer_Token = "AAAAAAAAAAAAAAAAAAAAAMmKcAEAAAAAFgCjHFhUUW6KkHGopns%2BWbjT%2FKM%3DfMpD4dwEHLuB3CaMMWiwpf9DjGE4A9zJf2qrhVjeSAnk1T8RR6";
        const string Access_Token = "1521117634267435009-a2CLrnlhS5ATNDDjufTwrkYSAFuToO";
        const string Access_Token_Secret = "RLBdasjycfyovwIYobJ6V8KbpbcUKdPIyIg3VHY3cAp1F";

        public async Task<Twitter> getTweet(long tweetid)
        {
            // we create a client with your user's credentials
            var userClient = new TwitterClient(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
            var tweetResponse = await userClient.TweetsV2.GetTweetAsync(tweetid);
            var tweet = tweetResponse.Tweet;
            //string url = "https://twitter.com/" + tweet.AuthorId + "/status/" + tweet.Id;
            DateTime utc = tweet.CreatedAt.UtcDateTime;
            string geo = "";
            string hashtag = "";
            //if (tweet.Geo != null)
            //{
            //  geo = tweet.Geo.ToString();
            //}
            //if (tweet.Geo != null)
            //{
            //    hashtag = tweet.Entities.Hashtags.ToString();
            //}
            Twitter myTwitter = new Twitter(tweet.Lang, tweet.Text, utc, geo, "", "Twitter", hashtag);
            return myTwitter;
        }

    }

}