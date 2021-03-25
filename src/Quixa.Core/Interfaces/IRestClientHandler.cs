using RestSharp;

namespace Quixa.Core.Interfaces
{
    public interface IRestClientHandler
    {
        T Execute<T>(RestRequest request);
        T ExecuteGet<T>(string baseURl);
        T ExecutePost<T>(string baseURl, RestRequest request);
    }
}
