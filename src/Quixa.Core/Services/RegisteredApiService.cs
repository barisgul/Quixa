using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quixa.Core.Dtos;
using Quixa.Core.Interfaces;

namespace Quixa.Core.Services
{
    public class RegisteredApiService : IApiService
    {
        //todo parse edilerek swagger title ve name bilgileri gönderilecek. Bunlar kategorilendirmeler için kullanılacak
        public SwaggerApiDto PrepareApiResponse(dynamic swaggerObject)
        {
            var data = (JObject)JsonConvert.DeserializeObject(swaggerObject);
            ParseJobject(data);
            SwaggerApiDto swaggerApi = new SwaggerApiDto
            {
                Name = Title,
                Address = Address
            };

            return swaggerApi;
        }

        private void ParseJobject(JObject jObject)
        {
            string title = jObject["Atlantic/Canary"].Value<string>();
            string address = jObject["Atlantic/Canary"].Value<string>();
        }

        private string Title { get; set; }
        private string Address { get; set; }
    }
}
