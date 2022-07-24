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

        public string ReadEmail_RpasswordV(string email) // forget password
        {
            Volunteer v = new Volunteer();
            return v.ReadEmail_RpasswordV(email);
        }


        [HttpGet]
        [Route("api/Volunteers/ReadEmail_V")]
        public bool ReadEmail_V(string email)
        {
            Volunteer v = new Volunteer();
            return v.ReadEmail_V(email);
        }


        [HttpGet]
        [Route("api/Volunteers/ReadDetails_V")]
        public bool ReadDetails_V(string email)
        {
            Volunteer v = new Volunteer();
            return v.ReadDetails_V(email);
        }


        [HttpGet]
        [Route("api/Volunteers/ReadVolunteer_V")]
        public Volunteer ReadVolunteer_V(string email)
        {
                Volunteer v = new Volunteer();
                return v.ReadVolunteer_V(email);     
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

        [HttpGet]
        [Route("api/Volunteers/volunteerDash")]
        public List<Volunteer> volunteerDash()
        {
            Volunteer v = new Volunteer();
            return v.volunteerDash();
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}