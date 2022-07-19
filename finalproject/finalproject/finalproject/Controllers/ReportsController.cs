using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalproject.Models;
using System.Threading.Tasks;

namespace finalproject.Controllers
{
    public class ReportsController : ApiController
    {

        // GET api/<controller>
        [HttpGet]
        [Route("api/Reports/getTweet")]
        public async Task<Report> getTweet(long tweetid, string email)
        {
            Report t = new Report();
            return await t.getTweet(tweetid, email);
        }

        [HttpPost]
        [Route("api/Reports/UpdateStatus")]
        public void UpdateStatus(List<Report> notRe_tweets)
        {
            Report t = new Report();
            t.UpdateStatus(notRe_tweets);
        }

        [HttpGet]
        [Route("api/Reports/getTweets")]
        public List<Report> getTweets()
        {
            Report t = new Report();
            return t.getTweets();
        }

        [HttpGet]
        [Route("api/Reports/getHashtag")]
        public Dictionary<string, int> getHashtag()
        {
            Report t = new Report();
            return t.getHashtag();
        }
        // PUT api/<controller>/5

        [HttpPost]
        [Route("api/Reports")]
        public bool Post([FromBody] Report report)
        {
            return report.InsertReport();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
 