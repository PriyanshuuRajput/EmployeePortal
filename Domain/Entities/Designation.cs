using Domain.Common;

namespace Domain.Entities
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        //public int DepartmentId { get; set; }

        //public Department? Department { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
