﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectBj.Service.Providers;
using ProjectBj.ViewModels;

namespace ProjectBj.Web.Controllers
{
    public class MainController : ApiController
    {
        // GET: api/Main
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Main/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Main
        public void Post([FromBody]GameSettings settings)
        {
        }

        // PUT: api/Main/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Main/5
        public void Delete(int id)
        {
        }
    }
}