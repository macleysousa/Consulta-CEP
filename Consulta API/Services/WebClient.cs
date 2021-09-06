using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consulta_API.Services
{
    public class WebClient
    {
        private HttpClient Client { get; set; }   
        
        public WebClient()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br/"),
            };
        }

        public async Task<ResponseWebClient> GetAsyn(string path)
        {
            HttpResponseMessage message = await Client.GetAsync(path);
            return new ResponseWebClient
            {
                StatusCode = Convert.ToInt32(message.StatusCode),
                Content = await message.Content.ReadAsStringAsync()
            };

        }
    }


    public class ResponseWebClient
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }

        public T Deserialize<T>()
        {
            return JsonSerializer.Deserialize<T>(Content);
        }
    }

}
