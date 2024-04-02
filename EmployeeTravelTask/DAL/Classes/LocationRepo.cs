using System.Collections.Generic;
using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTravelTask.DAL.Classes
{
    public class LocationRepo : ILocationRepo
    {
        private readonly TravelPlannerContext context;
        public LocationRepo(TravelPlannerContext context) 
        {
            this.context = context;
        }
        public async Task<IEnumerable<Location>> GetAllocations()
        {
            var result = context.Locations.AsNoTracking().AsEnumerable();
            return await Task.FromResult(result);
        }
    }
}
