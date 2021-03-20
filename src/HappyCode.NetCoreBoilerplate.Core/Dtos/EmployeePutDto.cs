using System.ComponentModel.DataAnnotations;

namespace Quixa.Core.Dtos
{
    public class EmployeePutDto
    {
        [Required]
        public string LastName { get; set; }
    }
}
