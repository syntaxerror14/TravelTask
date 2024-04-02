using EmployeeTravelTask.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeTravelTask.DTOs.Response
{
    public class TravelRequestResponseDTO
    {
        private object item;

        //public class TravelRequestResponseDTO()
        //{
        //
        //}
        public TravelRequestResponseDTO(TravelRequest tr)
        {
            RequestId = (int)tr.RequestId;
            RaisedByEmployeeId = tr.RaisedByEmployeeId;
            ToBeApprovedByHrid = tr.ToBeApprovedByHrid;
            RequestRaisedOn = tr.RequestRaisedOn;
            FromDate = tr.FromDate;
            ToDate = tr.ToDate;
            PurposeOfTravel = tr.PurposeOfTravel;
            LocationId = tr.LocationId;
            RequstStatus = tr.RequestStatus;
            RequestApprovedOn = tr.RequestApprovedOn;
            Priority = tr.Priority;
        }

        public TravelRequestResponseDTO(object item)
        {
            this.item = item;
        }

        public int RequestId { get; set; }
        public int? RaisedByEmployeeId { get; set; }
        public int? ToBeApprovedByHrid { get; set; }
        public DateTime? RequestRaisedOn { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? PurposeOfTravel { get; set; }
        public int? LocationId { get; set; }
        public string? RequstStatus { get; set; }
        public DateTime? RequestApprovedOn { get; set; }
        public string? Priority { get; set; }

    }

}
