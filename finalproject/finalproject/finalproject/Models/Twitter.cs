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
        long tweet_id;
        string lang;
        string contentText;
        DateTime created_at;
        string country;
        string linkUrl;
        string network;
        string hashtag;
        string author_name;
        string volunteer_email;



        public Twitter() { }
        public Twitter(long tweet_id, string lang, string contentText, DateTime created_at, string country, string linkUrl, string network, string hashtag, string author_name, string volunteer_email)
        {
            Tweet_id = tweet_id;
            Lang = lang;
            ContentText = contentText;
            Created_at = created_at;
            Country = country;
            LinkUrl = linkUrl;
            Network = network;
            Hashtag = hashtag;
            Author_name = author_name;
            Volunteer_email = volunteer_email;
        }

        public string Lang { get => lang; set => lang = value; }
        public string ContentText { get => contentText; set => contentText = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
        public string Country { get => country; set => country = value; }
        public string LinkUrl { get => linkUrl; set => linkUrl = value; }
        public string Network { get => network; set => network = value; }
        public string Hashtag { get => hashtag; set => hashtag = value; }
        public string Author_name { get => author_name; set => author_name = value; }
        public string Volunteer_email { get => volunteer_email; set => volunteer_email = value; }
        public long Tweet_id { get => tweet_id; set => tweet_id = value; }

        const string API_key = "hcUmMrTLpUjBoT3x7RtSHzTwF";
        const string API_Key_Secret = "7A0XTPRHfKzRCWyohOaKut5kpukOLMrD9epvW8QwQlbNmU2kEJ";
        const string Bearer_Token = "AAAAAAAAAAAAAAAAAAAAAMmKcAEAAAAAFgCjHFhUUW6KkHGopns%2BWbjT%2FKM%3DfMpD4dwEHLuB3CaMMWiwpf9DjGE4A9zJf2qrhVjeSAnk1T8RR6";
        const string Access_Token = "1521117634267435009-a2CLrnlhS5ATNDDjufTwrkYSAFuToO";
        const string Access_Token_Secret = "RLBdasjycfyovwIYobJ6V8KbpbcUKdPIyIg3VHY3cAp1F";

        public async Task<Twitter> getTweet(long tweetid, string email)
        {
            // we create a client with your user's credentials
            var userClient = new TwitterClient(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
            var tweetResponse = await userClient.TweetsV2.GetTweetAsync(tweetid);
            var tweet = tweetResponse.Tweet;
            string url = "https://twitter.com/" + tweet.AuthorId + "/status/" + tweet.Id;
            DateTime utc = tweet.CreatedAt.UtcDateTime;
            string geo = "not Published";
            string hashtag = "";
            var userResponse = await userClient.UsersV2.GetUserByIdAsync(tweet.AuthorId);
            var user = userResponse.User;
            string authorName = user.Username;
            if (tweet.Geo != null)
            {
                geo = tweet.Geo.ToString();
            }
            else if(user.Location != null)
            {
                geo = user.Location.ToString();
            }
            if (tweet.Entities.Hashtags != null) { 
                for (int i = 0; i < tweet.Entities.Hashtags.Length; i++)
                {
                    hashtag += tweet.Entities.Hashtags[i].Tag.ToString() + ",";
                }
            }
            Twitter myTwitter = new Twitter(tweetid, tweet.Lang, tweet.Text, utc, geo, url, "Twitter", hashtag, authorName,email);
            return myTwitter;
        }

        public void InsertTweet()
        {
            DataServices ds = new DataServices();
             ds.InsertTweet(this);
           
        }

    }

}