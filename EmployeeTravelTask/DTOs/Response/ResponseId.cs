using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.DTOs.Response
{
    public class ResponseId
    {
        public ResponseId(object item)
        {

        }
        public ResponseId(TravelRequest t)
        {
            RaisedByEmployeeId = t.RaisedByEmployeeId;
        }
        public int? RaisedByEmployeeId { get; set; }
    }
}
