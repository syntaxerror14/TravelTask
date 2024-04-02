using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.DTOs.Response;
using EmployeeTravelTask.Services.Interfaces;

namespace EmployeeTravelTask.Services.Classes
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo locationRepo;

        public LocationService(ILocationRepo locationRepo)
        {
            this.locationRepo = locationRepo;
        }
        public async Task<IEnumerable<LocationResponseDTO>> GetAllLocations()
        {
            var result = await locationRepo.GetAllocations();
            
            List<LocationResponseDTO> lrd = new(); //List that will contain DTO type object

            foreach(var location in result)
            {
                LocationResponseDTO lr = new(location);
                lrd.Add(lr);
            }
            return lrd;
        }
    }
}
