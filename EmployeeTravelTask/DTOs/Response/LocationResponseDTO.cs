using EmployeeTravelTask.Models;
namespace EmployeeTravelTask.DTOs.Response
{
    public class LocationResponseDTO
    {
        public LocationResponseDTO(Location l)
        {
            Id = l.Id;
            Name = l.Name;
        }
        //We need to show these response to the user
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
