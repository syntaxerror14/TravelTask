using EmployeeTravelTask.Models;

namespace EmployeeTravelTask.DAL.Interfaces
{
    public interface ITPUserRepo
    {
       Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUserTableData();
    }
}
