using EmployeeTravelTask.DTOs.Request;
using EmployeeTravelTask.DTOs.Response;
using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.Services.Interfaces
{
    public interface ITravelRequestService
    {
        Task<TravelRequestResponseDTO> AddTravelRequest(TravelRequestDTO travelrequestdto);
        Task<User> GetUserByEmployeeId(int id);
        Task<IEnumerable<TravelRequestResponseDTO>> AllPendingRequestByHR(int HRid);
        Task<TravelRequestResponseDTO> GetTravelRequestById(int trid);
        Task<bool> UpdateTravelRequestById(int trid,TravelRequestHRRequestDTO travelrequesthrdto);
        Task<int> CalculateApprovedBudget(int id, string priority, int days);
        Task<int> CalculateBudget(int travelRequestId);
        Task<IEnumerable<TravelRequestResponseDTO>> GetAllApprovedData(int id);
        Task<IEnumerable<ResponseId>> GetAllIds(string status);
    }
}
