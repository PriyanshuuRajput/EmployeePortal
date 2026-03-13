using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string EmpCode { get; set; } = string.Empty;
      
        private string _title = "";
        [Required(ErrorMessage = "Title is required.")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;

                if (_title == "Mr" || _title == "Sir" || _title == "Dr")
                    Gender = "Male";
                else if (_title == "Miss" || _title == "Mrs")
                    Gender = "Female";
            }
        }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter a valid number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(10, ErrorMessage = "Mobile number cannot exceed 10 ")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter a valid number")]

        public string? AlternatePhoneNumber { get; set; }
        [Required(ErrorMessage = "Select gender.")]
        public string Gender { get; set; } = "";
        [Required(ErrorMessage = "Select date of birth.")]
        public DateTime? DateOfBirth { get; set; }

        public string? Image { get; set; }
        [Required(ErrorMessage = "Select date.")]
        public DateTime? HireDate { get; set; }
        [Required(ErrorMessage = "Designation is required.")]
        public int DesignationId { get; set; }

        public DesignationDto? Designation { get; set; }
         [Required(ErrorMessage="Department is required.")]
        public int DepartmentId { get; set;  }
        public DepartmentDto? Department { get; set; }
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
