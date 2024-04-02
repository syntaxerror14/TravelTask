using System;
using System.Collections.Generic;

namespace EmployeeTravelTask.Models
{
    public partial class User
    {
        public User()
        {
            GradesHistories = new HashSet<GradesHistory>();
        }

        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Role { get; set; }
        public int? CurrentGradeId { get; set; }
        public virtual Grade? CurrentGrade { get; set; }
        public virtual ICollection<GradesHistory> GradesHistories { get; set; }
    }
}
