using System;
using System.Collections.Generic;
using System.Text;
using Quixa.Core.Dtos;

namespace Quixa.Core.Interfaces
{
    public interface IApiService
    {
        SwaggerApiDto PrepareApiResponse(dynamic swaggerObject);
    }
}
