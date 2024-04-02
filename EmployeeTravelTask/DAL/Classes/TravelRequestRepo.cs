using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTravelTask.DAL.Classes
{
    public class TravelRequestRepo : ITravelRequestRepo
    {
        private readonly TravelPlannerContext context;
        public TravelRequestRepo(TravelPlannerContext context)
        {
            this.context = context;
        }

        public async Task<TravelRequest> AddTravelRequest(TravelRequest travelrequest)
        {
            try
            {
                var result = await context.TravelRequests.AddAsync(travelrequest);
                await context.SaveChangesAsync();
                return result.Entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<IEnumerable<TravelRequest>> AllPendingRequestByHR(int HRid)
        {

            var result = await (from x in context.TravelRequests where x.ToBeApprovedByHrid == HRid && x.RequestStatus == "New" select x).ToListAsync();
            return await Task.FromResult(result);
        }
        public async Task<TravelRequest> GetTravelRequestById(int trid)
        {
            var result = await context.TravelRequests.FindAsync(trid);
            Console.Write(result);
            return result;
        }
        
        public async Task<bool> UpdateTravelRequestById(TravelRequest travelRequest)
        {
            context.Update(travelRequest);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetAllApprovedData(int id)
        {
            var result = context.TravelRequests.AsNoTracking().Where(u => u.RaisedByEmployeeId == id && u.RequestStatus == "Approved");
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<TravelRequest>> GetAllIds(string status)
        {
            var result = context.TravelRequests.AsNoTracking().Where(u => u.RequestStatus == status).AsEnumerable();
            return result;
        }

        Task<string> ITravelRequestRepo.GetAllIds(TravelRequest travelRequest)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<object>> ITravelRequestRepo.GetAllIds(string status)
        {
            throw new NotImplementedException();
        }

        Task<int> ITravelRequestRepo.GetAllApprovedData(TravelRequest travelRequest)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<object>> ITravelRequestRepo.GetAllApprovedData(int id)
        {
            throw new NotImplementedException();
        }
    }
}
