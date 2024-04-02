using EmployeeTravelTask.DTOs.Response;

namespace EmployeeTravelTask.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationResponseDTO>> GetAllLocations();
    }
}
