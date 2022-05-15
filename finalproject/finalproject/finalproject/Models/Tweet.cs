//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Tweetinvi;
//using System.Configuration;
//using Tweetinvi.Models;
//using System.IO;

//namespace finalproject.Models
//{
//    public class Tweet
//    {
//        const string API_key = "hcUmMrTLpUjBoT3x7RtSHzTwF";
//        const string API_Key_Secret = "7A0XTPRHfKzRCWyohOaKut5kpukOLMrD9epvW8QwQlbNmU2kEJ";
//        const string Bearer_Token = "AAAAAAAAAAAAAAAAAAAAAMmKcAEAAAAAFgCjHFhUUW6KkHGopns%2BWbjT%2FKM%3DfMpD4dwEHLuB3CaMMWiwpf9DjGE4A9zJf2qrhVjeSAnk1T8RR6";
//        const string Access_Token = "1521117634267435009-a2CLrnlhS5ATNDDjufTwrkYSAFuToO";
//        const string Access_Token_Secret = "RLBdasjycfyovwIYobJ6V8KbpbcUKdPIyIg3VHY3cAp1F";

//        static void Main(string[] args)
//        {
//            Authentication
//            Auth.SetUserCredentials(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
//            var userClient = new TwitterClient(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
//            tweepy.Client
//            curl "https://api.twitter.com/2/tweets/{id}?expansions=referenced_tweets.id" - H "Authorization: Bearer $BEARER_TOKEN"


//             Create the Stream
//            var stream = Stream.CreateFilteredStream();

//            Keywords to Track
//            stream.AddTrack("KEYWORD_TO_TRACK");

//            Limit to English
//            stream.AddTweetLanguageFilter(LanguageFilter.English);

//            Message so you know its running
//            Console.WriteLine("I am listening to Twitter");

//            Called when a tweet maching the tracker is produced
//           stream.MatchingTweetReceived += (sender, arguments) =>
//           {
//               Console.WriteLine(arguments.Tweet.Text);
//           };

//            stream.StartStreamMatchingAllConditions();
//        }





//        public static async Task User_GetFavorites()
//        {
//            Auth.SetUserCredentials(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
//            var tweetResponse = await client.TweetsV2.GetTweetAsync(1313034609437880320);
//            var tweet = tweetResponse.Tweet;
//        }

//        static async Task Main()
//        {
//            Tweets t = new Tweets();
//            var userClient = new TwitterClient(API_key, API_Key_Secret, Access_Token, Access_Token_Secret);
//            var user = await userClient.Users.GetAuthenticatedUserAsync();
//            Console.WriteLine(user);
//            //var tweetResponse = await userClient.GetTweetAsync(1313034609437880320);
//            //var tweet = tweetResponse.Tweet;
//            //var tweet = await client.Tweets.GetTweetAsync(publishedTweet.Id);


//        }
//    }
//}