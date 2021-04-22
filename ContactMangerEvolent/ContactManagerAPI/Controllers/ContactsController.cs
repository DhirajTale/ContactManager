using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ContactManagerAPI.Utility;
using ContactManagerAPI.Repository;
using System.Net.Http;
using System.Net;
using System.Data.SqlClient;
using ContactManagerAPI.Model;
using System.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ContactManagerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Contact")]
    public class ContactsController : Controller
    {

        private readonly IConfiguration config;

        public ContactsController(IConfiguration configuration)
        {
            config = configuration;
        }

        private string connection()
        {
            return config.GetSection("Settings").GetSection("DbConn").Value;

        }

        [HttpGet]
        [Route("GetContacts")]
        public IActionResult GetContacts() {
           var data = DbClientFactory<ContactDBClient>.Instance.GetAllContacts(config.GetSection("Settings").GetSection("DbConn").Value);
           return Ok(data);
        }

        [HttpDelete("id")]
        [Route("Delete")]
        public IActionResult DeleteContact([FromBody]int id)
        {
            var data = DbClientFactory<ContactDBClient>.Instance.DeleteContact(connection(), id);
            return Ok(data);
        }

        [HttpPost]
        [Route("AddContact")]
        public IActionResult AddContact([FromBody] Object contact)
        {
            var contactModel= Newtonsoft.Json.JsonConvert.DeserializeObject<ContactModel>(contact.ToString());
            var data = DbClientFactory<ContactDBClient>.Instance.AddContact(connection(), contactModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("UpdateContact")]
        public IActionResult UpdateContact([FromBody] Object contact)
        {
            var contactModel = JsonConvert.DeserializeObject<ContactModel>(contact.ToString());
            var data = DbClientFactory<ContactDBClient>.Instance.UpdateContact(connection(), contactModel);
            return Ok(data);
        }
    }
}
