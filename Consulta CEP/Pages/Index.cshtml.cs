using Consulta_CEP.Models;
using Consulta_CEP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consulta_CEP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WebClient web;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            web = new WebClient("http://localhost:62625/");
        }

        public async Task OnGet(string cep)
        {
            if (cep != null)
            {
                ViewData["cepDigitado"] = cep;

                ResponseWebClient response = await web.GetAsyn($"/api/cep/{cep}");
                if (response.StatusCode == 200)
                {
                    var data = response.Deserialize<CEP>();
                    if (data?.cep != null)
                    {
                        ViewData["cep"] = data;
                    }
                    else
                    {
                        ViewData["errorCEP"] = "CEP inválido";
                    }

                }
                else
                {
                    ViewData["errorCEP"] = "CEP inválido";
                }
            }
        }
    }
}
