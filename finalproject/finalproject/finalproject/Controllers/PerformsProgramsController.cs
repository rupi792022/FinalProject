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
        [Route("api/PerformsPrograms/Read_maxProPerforms")]
        public int Read_maxProPerforms(string email)
        {
           PerformsProgram p = new PerformsProgram();
            return p.Read_maxProPerforms(email);
        }

        [HttpGet]
        [Route("api/PerformsPrograms/Read_minScore")]
        public List<PerformsProgram> Read_minScore()
        {
            PerformsProgram p = new PerformsProgram();
            return p.Read_minScore();
        }

        // POST api/<controller>
        public void Post([FromBody] PerformsProgram p)
        {
            p.InsertPerforms();
            
        }

        public void Delete(string email, int guiding_serial_num)
        {
            PerformsProgram p = new PerformsProgram();
            p.deletePerforms(email, guiding_serial_num);
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}