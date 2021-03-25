using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quixa.Core.Dtos;
using Quixa.Core.Interfaces;
using Quixa.Core.RestApiHandler;
using Quixa.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quixa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestCollectorController : ControllerBase
    {
        IConfiguration configuration;
        IRestClientHandler restClientHandler;
        IApiService apiService;
        public RestCollectorController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.restClientHandler = new RestClientHandler();
            this.apiService = new RegisteredApiService();
        }
        // GET: api/<RestCollectorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get registered api addresses
        /// </summary>
        /// <returns></returns>
        [HttpGet("RegisteredApis")]
        public List<string> GetRegisteredApplications()
        {
            var values = configuration.GetSection("ApiRegistration:SwaggerAddress").Get<List<string>>();

            return values;
        }

        [HttpGet("ApiDocs")]
        public List<SwaggerApiDto> ApiDocs()
        {
            List<SwaggerApiDto> swaggerApiDtos = new List<SwaggerApiDto>();
            var values = configuration.GetSection("ApiRegistration:SwaggerAddress").Get<List<string>>();
            foreach (var item in values)
            {
                dynamic swaggerDoc = restClientHandler.ExecuteGet<dynamic>(item);
                SwaggerApiDto swaggerApi = apiService.PrepareApiResponse(swaggerDoc);
                swaggerApiDtos.Add(swaggerApi);
            }

            return swaggerApiDtos;
        }


        // GET api/<RestCollectorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RestCollectorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RestCollectorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestCollectorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
