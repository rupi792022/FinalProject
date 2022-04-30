using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class QandAsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/QandAs/Read_QandA")]
        public IEnumerable<QandA> Read_QandA(int numProgram)
        {
            QandA q = new QandA();
            return q.Read_QandA(numProgram);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] List<QandA> qa)
        {
            QandA q = new QandA();
            q.InsertQandA(qa);
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPut]
        [Route("api/QandAs/UpdateQandADetails")]
        public void UpdateQandADetails(List<QandA> qa)
        {
            QandA q = new QandA();
            q.UpdateQandADetails(qa);
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

       
        // PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }


    }
}