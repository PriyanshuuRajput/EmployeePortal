using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        // public ICollection<Designation>? Designations { get; set; } = new List<Designation>();
        public List<Designation>? Designations { get; set; }

       // public List<Employee>? Employees { get; set; }

    }
}
