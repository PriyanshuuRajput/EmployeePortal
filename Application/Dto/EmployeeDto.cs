using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public int EmpCode { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? AlternatePhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Image { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        public int DesignationId { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
    }
}
