using Consulta_API.Models;
using Consulta_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consulta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CEPController : ControllerBase
    {
        protected WebClient web = new WebClient();
        
        // GET: api/<CEPController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CEPController>/5
        [HttpGet("{cep}")]
        public async Task<object> Get(string cep)
        {
            ResponseWebClient response = await web.GetAsyn($"ws/{cep.Replace("-","")}/json");
            if (response.StatusCode == 200)
            {
                return response.Deserialize<CEP>();
            }
            else
            {
                return StatusCode(response.StatusCode, new
                {
                    error = response.StatusCode,
                    message = response.Content,
                });
            }
        }


    }
}
