﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Tweetinvi;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class TwittersController : ApiController
    {

        // GET api/<controller>
        [HttpGet]
        [Route("api/Twitters/getTweet")]
        public async Task<Twitter> getTweet(long tweetid, string email) 
        {
            Twitter t = new Twitter();
            return await t.getTweet(tweetid, email);
        }

        [HttpGet]
        [Route("api/Twitters/getStatusPage")]
        public IEnumerable<Twitter> getStatusPage([FromUri] List<Twitter> notRe_tweets) {
            Twitter t = new Twitter();
            return t.getStatusPage(notRe_tweets);
        }

        [HttpGet]
        [Route("api/Twitters/getTweets")]
        public List<Twitter> getTweets()
        {
            Twitter t = new Twitter();
            return t.getTweets();
        }
        // PUT api/<controller>/5

        [HttpPost]
        [Route("api/Twitters")]
        public void Post([FromBody] Twitter twitter)
        {
            twitter.InsertTweet();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}