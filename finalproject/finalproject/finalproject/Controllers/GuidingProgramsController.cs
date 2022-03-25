using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class GuidingProgramsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/GuidingPrograms/Read_GP")]
        public HttpResponseMessage Read_GP(int level_num)
        {
            try
            {
                GuidingProgram gp = new GuidingProgram();
                return Request.CreateResponse(HttpStatusCode.OK, gp.Read_GP(level_num));
            }
            catch (Exception ex)
            {
                if (ex.Message == "failed to connect to the server")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }

        }

        // POST api/<controller>

        public HttpResponseMessage Post([FromBody] GuidingProgram GP)
        { 
            GP.InsertLevel();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        // PUT api/<controller>/5
        public void Put(GuidingProgram GP)
        {
            GP.UpdateLevelDetails(GP);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}