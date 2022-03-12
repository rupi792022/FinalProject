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
        public int Get(string email, string password)
        {
            Volunteer volunteer = new Volunteer();

            return volunteer.ReadEmailPassword(email, password);

        }


        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Volunteer volunteer)
        {
            int emailExist = volunteer.InsertEmail();
            return Request.CreateResponse(HttpStatusCode.Created, emailExist);
        }


        // PUT api/<controller>/5
        public void Put(Volunteer volunteer)
        {
            volunteer.UpdateVolunteerDetails(volunteer);
        }

        public int Put(string password, string email)
        {
            Volunteer volunteer = new Volunteer();
           return volunteer.UpdateVolunteerpassword(password, email);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}