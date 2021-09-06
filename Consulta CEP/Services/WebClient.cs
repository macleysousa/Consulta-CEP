using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consulta_CEP.Services
{
    public class WebClient
    {
        private HttpClient Client { get; set; }   
        
        public WebClient(string BaseAddress)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public async Task<ResponseWebClient> GetAsyn(string path, IEnumerable<HeadersWebClient> headers = null)
        {
            headers?.ToList().ForEach(x => { Client.DefaultRequestHeaders.Add(x.Name, x.Name); });
            HttpResponseMessage message = await Client.GetAsync(path);
            return new ResponseWebClient
            {
                StatusCode = Convert.ToInt32(message.StatusCode),
                Content = await message.Content.ReadAsStringAsync(),
                RequestMessage = message.RequestMessage
            };

        }
    }

    public class ResponseWebClient
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public T Deserialize<T>()
        {
            try { return JsonSerializer.Deserialize<T>(Content); } catch { return default; }
        }
             
    }

    public class HeadersWebClient 
    {
       public string Name { get; set; }
       public string Value { get; set; }
    }

}
