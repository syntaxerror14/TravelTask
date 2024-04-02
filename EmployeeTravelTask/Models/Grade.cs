using System;
using System.Collections.Generic;

namespace EmployeeTravelTask.Models
{
    public partial class Grade
    {
        public Grade()
        {
            GradesHistories = new HashSet<GradesHistory>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GradesHistory> GradesHistories { get; set; }
    }
}
