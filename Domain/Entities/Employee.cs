using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee :BaseEntity
    {
        
        public string EmpCode { get; set;  } = string.Empty;
        public string Title { get; set; } = string.Empty;  
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty ;
        public string? AlternatePhoneNumber { get; set; }
        public string Gender {  get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime? DateofBirth {  get; set; }
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate {  get; set; }
        public int DesignationId { get; set; }
        public Designation? Designation { get; set;}
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string Address { get; set; } = string.Empty;
        //public string City { get; set; } = string.Empty;
        //public string State {  get; set; } = string.Empty;



    }
}
