﻿using finalproject.Models;
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
        public int Get(string email, string password)
        {
            Manager manager = new Manager();

            return manager.ReadLogInManager(email, password);

        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5

        public int Put(string password, string email)
        {
            Manager manager = new Manager();
            return manager.UpdateManagerpassword(password, email);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}