using EmployeeTravelTask.Models;
using System.Collections;

namespace EmployeeTravelTask.DAL.Interfaces
{
    public interface ILocationRepo
    {
        Task<IEnumerable<Location>> GetAllocations();
    }
}
