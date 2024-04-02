using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.DAL.Classes
{
    public class TravelBudgetAllocationRepo : ITravelBudgetAllocationRepo
    {
        private readonly TravelPlannerContext context;

        public TravelBudgetAllocationRepo(TravelPlannerContext context)
        {
            this.context = context;
        }
        public async Task AddBudgetAllocation(TravelBudgetAllocation travelBudgetAllocation)
        {
            await context.TravelBudgetAllocations.AddAsync(travelBudgetAllocation);
            await context.SaveChangesAsync();
        }
    }
}
