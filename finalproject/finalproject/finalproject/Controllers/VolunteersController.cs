using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class VolunteersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        public bool Get(string email, string password) // check if the volunteer's password is correct
        {
            Volunteer volunteer = new Volunteer();
            return volunteer.ReadPassword_V(email, password);
        }

        [HttpGet]
        [Route("api/Volunteers/ReadEmail_RpasswordV")]

        public HttpResponseMessage ReadEmail_RpasswordV(string email) // forget password
        {
            try
            {
                Volunteer v = new Volunteer();
                return Request.CreateResponse(HttpStatusCode.OK, v.ReadEmail_RpasswordV(email));
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

        [HttpGet]
        [Route("api/Volunteers/ReadEmail_V")]
        public HttpResponseMessage ReadEmail_V(string email)
        {
            try
            {
                Volunteer v = new Volunteer();
                return Request.CreateResponse(HttpStatusCode.OK, v.ReadEmail_V(email));
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
        ///{email}

        [HttpGet]
        [Route("api/Volunteers/ReadDetails_V")]
        public HttpResponseMessage ReadDetails_V(string email)
        {
            try
            {
                Volunteer v = new Volunteer();
                return Request.CreateResponse(HttpStatusCode.OK, v.ReadDetails_V(email));
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
        public HttpResponseMessage Post([FromBody] Volunteer volunteer)
        {
            bool emailExist = volunteer.InsertEmail();
            return Request.CreateResponse(HttpStatusCode.Created, emailExist);
        }


        // PUT api/<controller>/5
        public void Put(Volunteer volunteer)
        {
            volunteer.UpdateVolunteerDetails(volunteer);
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}