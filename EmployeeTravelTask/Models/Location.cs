using System;
using System.Collections.Generic;

namespace EmployeeTravelTask.Models
{
    public partial class Location
    {
        public Location()
        {
            TravelRequests = new HashSet<TravelRequest>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TravelRequest> TravelRequests { get; set; }
    }
}
