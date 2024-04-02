namespace EmployeeTravelTask.DTOs.Request
{
    public class TravelRequestDTO
    {
        public int? RaiseByEmployeeId { get; set; }
        public int? ToBeApprovedByHrid { get; set;}
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set;}
        public string? PurposeOfTravel {  get; set; }
        public int? LocationId { get; set; }
        public string? Priority { get; set; }
       
    }
}
