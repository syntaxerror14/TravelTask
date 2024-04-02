using System;
using System.Collections.Generic;

namespace EmployeeTravelTask.Models
{
    public partial class TravelRequest    {
        public TravelRequest()
        {
            TravelBudgetAllocations = new HashSet<TravelBudgetAllocation>();
        }

        public int RequestId { get; set; }
        public int? RaisedByEmployeeId { get; set; }
        public int? ToBeApprovedByHrid { get; set; }
        public DateTime? RequestRaisedOn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? PurposeOfTravel { get; set; }
        public int? LocationId { get; set; }
        public string? RequestStatus { get; set; } = null!;
        public DateTime? RequestApprovedOn { get; set; }
        public string? Priority { get; set; } = null!;

        public virtual Location? Location { get; set; }
        public virtual ICollection<TravelBudgetAllocation> TravelBudgetAllocations { get; set; }
    }
}
