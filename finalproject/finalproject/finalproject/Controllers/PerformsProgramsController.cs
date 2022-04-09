using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class PerformsProgramsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        [HttpGet]
        [Route("api/Volunteers/Read_maxLevelsPerforms")]
        public int Read_maxLevelsPerforms(string email)
        {
           PerformsProgram p = new PerformsProgram();
            return p.Read_maxLevelsPerforms (email);
        }

        // POST api/<controller>
        public void Post([FromBody] PerformsProgram p)
        {
            p.InsertPerforms();
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}