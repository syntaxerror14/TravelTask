using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace EmployeeTravelTask.DAL.Classes
{
    public class TPUserRepo : ITPUserRepo
    {
        private readonly TravelPlannerContext context;

        public TPUserRepo(TravelPlannerContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<User>> GetAllUserTableData()
        {
            var users = context.Users.AsNoTracking().AsEnumerable();
            return await Task.FromResult(users);
        }
        public async Task<User?> GetUserById(int id)
        {
            
            var user = await context.Users.FirstOrDefaultAsync(u=>u.EmployeeId==id);
            return user;
            
        }
    }
}
