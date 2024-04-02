using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.DAL.Interfaces
{
    public interface ITravelRequestRepo
    {
        Task<TravelRequest> AddTravelRequest(TravelRequest travelrequest);
        Task<IEnumerable<TravelRequest>> AllPendingRequestByHR(int hRid);
        Task<TravelRequest> GetTravelRequestById(int travelRequestid);
        Task<bool> UpdateTravelRequestById(TravelRequest travelRequest);
        Task<string> GetAllIds(TravelRequest travelRequest);
        Task<IEnumerable<object>> GetAllIds(string status);
        Task<int> GetAllApprovedData(TravelRequest travelRequest);
        Task<IEnumerable<object>> GetAllApprovedData(int id);
    }
}
