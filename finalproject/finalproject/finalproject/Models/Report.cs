using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
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
using System.Net.Http;

namespace finalproject.Models
{
    public class Report
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
        string status;


        public Report() { }
        public Report(long tweet_id, string lang, string contentText, DateTime created_at, string country, string linkUrl, string network, string hashtag, string author_name, string volunteer_email, string status)
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
            Status = status;

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
        public string Status { get => status; set => status = value; }

        const string API_key = "hcUmMrTLpUjBoT3x7RtSHzTwF";
        const string API_Key_Secret = "7A0XTPRHfKzRCWyohOaKut5kpukOLMrD9epvW8QwQlbNmU2kEJ";
        const string Access_Token = "1521117634267435009-a2CLrnlhS5ATNDDjufTwrkYSAFuToO";
        const string Access_Token_Secret = "RLBdasjycfyovwIYobJ6V8KbpbcUKdPIyIg3VHY3cAp1F";

        public async Task<Report> getTweet(long tweetid, string email)
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
            else if (user.Location != null)
            {
                geo = user.Location.ToString();
            }
            if (tweet.Entities.Hashtags != null)
            {
                for (int i = 0; i < tweet.Entities.Hashtags.Length; i++)
                {
                    hashtag += tweet.Entities.Hashtags[i].Tag.ToString() + ",";
                }
            }
            Report myTwitter = new Report(tweetid, tweet.Lang, tweet.Text, utc, geo, url, "Twitter", hashtag, authorName, email, "Not removed");
            return myTwitter;
        }


        public bool InsertReport()
        {
            DataServices ds = new DataServices();
            return ds.InsertReport(this);

        }

        public List<Report> getStatusPage(List<Report> notRe_tweets)
        {
            List<Report> t_List = new List<Report>();
            foreach (var t in notRe_tweets)
            {
                try
                {
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(t.LinkUrl);
                    // Sends the HttpWebRequest and waits for a response.
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        t.Status = "Removed";
                        t_List.Add(t);
                    }
                    // Releases the resources of the response.
                    myHttpWebResponse.Close();

                }
                catch (WebException e)
                {
                    //Console.WriteLine("\r\nWebException Raised. The following error occurred : {0}", e.Status);
                    if (e.Status.ToString() == "ProtocolError")
                    {
                        t.Status = "Removed";
                        t_List.Add(t);
                    }
                    continue;

                }
            }

            return t_List;
        }



        public List<Twitter> getTweets()
        {
            DataServices ds = new DataServices();
            return ds.getTweets();
        }

        public void UpdateStatus(List<Report> reports)

        {
            List<Report> reports_List = new List<Report>();
            reports_List = getStatusPage(reports);
            DataServices ds = new DataServices();
            ds.UpdateStatus(reports_List);
        }

        public Dictionary<string, int> sorthashtags(List<string> hashtags)
        {
            List<string> listOf_hashtags = new List<string>();
            foreach (var h in hashtags)
            {
                listOf_hashtags.AddRange(h.Split(',').ToList());
            }
            listOf_hashtags.RemoveAll(item => item == "");
            var dict = new Dictionary<string, int>();
            foreach (var value in listOf_hashtags)
            {
                // When the key is not found, "count" will be initialized to 0
                dict.TryGetValue(value, out int count);
                dict[value] = count + 1;
            }
            var dictSort = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Int32> author in dict.OrderBy(key => key.Key))
            {
                dictSort.Add(author.Key, author.Value);
            }

            return dictSort;
        }

        public Dictionary<string, int> getHashtag()
        {
            DataServices ds = new DataServices();
            return sorthashtags(ds.getHashtag());
        }


    }

}