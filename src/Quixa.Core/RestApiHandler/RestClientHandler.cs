using System;
using Quixa.Core.Interfaces;
using RestSharp;

namespace Quixa.Core.RestApiHandler
{
    public class RestClientHandler : IRestClientHandler
    {
        public T Execute<T>(RestRequest request)
        { 
            throw new NotImplementedException();
        }

        public T ExecuteGet<T>(string baseURl)
        {
            var client = new RestClient(baseURl);
            var request = new RestRequest("v2/swagger.json", Method.GET);
            var queryResult = client.Execute<T>(request).Data;

            return queryResult;
        }

        public T ExecutePost<T>(string baseURl, RestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
