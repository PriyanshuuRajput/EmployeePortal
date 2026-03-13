using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public  class DesignationDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public DepartmentDto? Department { get; set; }
    }
}
