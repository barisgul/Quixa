using System.Collections.Generic;

namespace Quixa.Core.Dtos
{
    public class SwaggerApiDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<dynamic> Api { get; set; } 
    }
}
