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
        [Route("api/GuidingPrograms/Read_Max_Program")]
        public int Read_Max_Program()
        {
            GuidingProgram gp = new GuidingProgram();
            return gp.Read_Max_Program();
        }

        //[HttpGet]
        //[Route("api/GuidingPrograms/Read_Level")]
        //public GuidingProgram Read_Level(int level_num)
        //{
        //    GuidingProgram gp = new GuidingProgram();
        //    return gp.Read_Level(level_num);
        //}


        [HttpGet]
        [Route("api/GuidingPrograms/Read_maxLevel")]
        public int Read_maxLevel()
        {
            GuidingProgram gp = new GuidingProgram();
            return gp.Read_maxLevel();
        }


        //[HttpGet]
        //[Route("api/GuidingPrograms/Read_GP")]
        //public IEnumerable<GuidingProgram> Read_GP()
        //{
        //    GuidingProgram gp = new GuidingProgram();
        //    return gp.Read_GP();
        //}


        // POST api/<controller>

        public HttpResponseMessage Post([FromBody] GuidingProgram GP)
        {
            GP.InsertProgram();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        //// PUT api/<controller>/5
        //public void Put(GuidingProgram GP)
        //{
        //    GP.UpdateLevelDetails(GP);
        //}

        //// DELETE api/<controller>/5
        //public void Delete()
        //{
        //    GuidingProgram gp = new GuidingProgram();
        //    gp.DeleteProgram();
        //}

    }
}