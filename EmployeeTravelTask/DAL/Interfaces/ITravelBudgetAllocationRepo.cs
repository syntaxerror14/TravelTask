using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.DAL.Interfaces
{
    public interface ITravelBudgetAllocationRepo
    {
        Task AddBudgetAllocation(TravelBudgetAllocation travelBudgetAllocation);
    }
}
