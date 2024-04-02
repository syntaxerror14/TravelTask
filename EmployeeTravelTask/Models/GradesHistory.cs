using System;
using System.Collections.Generic;

namespace EmployeeTravelTask.Models
{
    public partial class GradesHistory
    {
        public int Id { get; set; }
        public DateTime? AssignedOn { get; set; }
        public int? EmployeeId { get; set; }
        public int? GradeId { get; set; }
        public virtual User? Employee { get; set; }
        public virtual Grade? Grades { get; set; }
    }
}
