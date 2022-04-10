using finalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace finalproject.Controllers
{
    public class ManagersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        public bool Get(string email, string password) // check if the manager's email password is correct
        {
            Manager manager = new Manager();
            return manager.ReadPassword_M(email, password);
        }


        [HttpGet]
        [Route("api/Managers/ReadEmail_M")]

        public bool ReadEmail_M(string email) // check if the manager's email exist in DB
        {
            Manager m = new Manager();
            return m.ReadEmail_M(email);
        }


        [HttpGet]
        [Route("api/Managers/ReadEmail_RpasswordM")]

        public string ReadEmail_RpasswordM(string email) // forget password
        {
            Manager m = new Manager();
            return m.ReadEmail_RpasswordM(email);
        }

        [HttpGet]
        [Route("api/Managers/ReadManager_M")]

        public Manager ReadManager_M(string email)
        {
            Manager m = new Manager();
            return m.ReadManager_M(email);
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}