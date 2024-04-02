using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.DTOs.Request;
using EmployeeTravelTask.DTOs.Response;
using EmployeeTravelTask.Models;
using EmployeeTravelTask.Services;
using EmployeeTravelTask.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace EmployeeTravelTask.Services.Classes
{
    public class TravelRequestService : ITravelRequestService
    {
        private readonly ITravelRequestRepo travelRequestRepo;
        private readonly ITPUserRepo userTableRepo;
        private readonly ITravelBudgetAllocationRepo travelBudgetAllocationRepo;
       
        public TravelRequestService(ITravelRequestRepo travelRequestRepo, ITPUserRepo userTableRepo, ITravelBudgetAllocationRepo travelBudgetAllocationRepo)
        {
            this.travelRequestRepo = travelRequestRepo;
            this.userTableRepo = userTableRepo;
            this.travelBudgetAllocationRepo = travelBudgetAllocationRepo;
        }

        //<---------------------------ENDPOINT 2 (ADD NEW TRAVEL REQUEST)------------------->
        public async Task<TravelRequestResponseDTO> AddTravelRequest(TravelRequestDTO travelrequestdto)
        {
            if (await GetUserByEmployeeId(travelrequestdto.RaiseByEmployeeId.GetValueOrDefault()) == null)
                throw new Exception("This EmployeeId doesn't exist");

            User? user = await GetUserByEmployeeId(travelrequestdto.ToBeApprovedByHrid.GetValueOrDefault());
            if (user.Role != "HR")
                throw new Exception("No HR exist's wiht this Id");
            
            TravelRequest travelrequest = new()
            {
                RaisedByEmployeeId = travelrequestdto.RaiseByEmployeeId,
                ToBeApprovedByHrid = travelrequestdto.ToBeApprovedByHrid,
                FromDate = travelrequestdto.FromDate,
                ToDate = travelrequestdto.ToDate,
                PurposeOfTravel = travelrequestdto.PurposeOfTravel,
                LocationId = travelrequestdto.LocationId,
                RequestStatus = "New",

                Priority = travelrequestdto.Priority
            };
            var result = await travelRequestRepo.AddTravelRequest(travelrequest);

                TravelRequestResponseDTO trdto = new(result);
                return trdto;
 
        }
        // in reference to get/call employeeId
        public async Task<User?> GetUserByEmployeeId(int id)
        {
            var result = await userTableRepo.GetUserById(id);
            return result;
        }

        //<---------------------------ENDPOINT 3 (GET PENDING REQUEST)------------------->
        public async Task<IEnumerable<TravelRequestResponseDTO>> AllPendingRequestByHR(int HRid)
        {

            User user = await GetUserByEmployeeId(HRid);
            if (user == null)
                throw new Exception("This Employee Id doesn't Exist");
            if (user.Role != "HR")
                throw new Exception("No HR exist's with this Id");
            var result = await travelRequestRepo.AllPendingRequestByHR(HRid);

            if (result.Count() == 0)
                throw new Exception("No Pending Request for this HR");

            List<TravelRequestResponseDTO> trrd = new();
            foreach (TravelRequest tr in result)
            {
                TravelRequestResponseDTO t = new(tr);
                trrd.Add(t);
            }

            return trrd;

    }
        //<-----------------------------EndPoint -4--------------------------------->
        public async Task<TravelRequestResponseDTO> GetTravelRequestById(int trid)
        {
            var result = await travelRequestRepo.GetTravelRequestById(trid);
            if (result == null)
                throw new Exception("No Travel Request with this Travel Request Id");

            TravelRequestResponseDTO travelRequestResponseDTO = new(result);
            return travelRequestResponseDTO;

        }
        //<---------------------------EndPoint -5 -------------------------------->
        public async Task<bool> UpdateTravelRequestById(int trid, TravelRequestHRRequestDTO travelrequesthrdto)
        {
            TravelRequest travelRequest = await travelRequestRepo.GetTravelRequestById(trid);

            if (travelRequest == null)
                throw new Exception("Travel Id not exist");

            if (travelRequest.RequestStatus != "New")
                throw new Exception("This travel request is already processed");

            travelRequest.RaisedByEmployeeId = travelrequesthrdto.RaiseByEmployeeId;

            User? user1 = await GetUserByEmployeeId((int)travelrequesthrdto.RaiseByEmployeeId);

            if (user1 == null)
                throw new Exception("This EmployeeId doesn't exist");
            
            travelRequest.ToBeApprovedByHrid = travelrequesthrdto.ToBeApprovedByHrid;

            User? user = await GetUserByEmployeeId((int)travelrequesthrdto.ToBeApprovedByHrid);
            if (user == null || user.Role != "HR")
                throw new Exception("No HR exist's with this Id");

            travelRequest.RequestRaisedOn = travelrequesthrdto.RequestRaisedOn;
            travelRequest.FromDate = (DateTime)travelrequesthrdto.FromDate;
            travelRequest.ToDate = (DateTime)travelrequesthrdto.ToDate;
            travelRequest.PurposeOfTravel = travelrequesthrdto.PurposeOfTravel;
            travelRequest.LocationId = travelrequesthrdto.LocationId;
            travelRequest.RequestStatus = travelrequesthrdto.RequestStatus;
            travelRequest.Priority = travelrequesthrdto.Priority;
            travelRequest.RequestApprovedOn = DateTime.Now;

            if (travelrequesthrdto.RequestStatus == "Approved")
            {

                int days = (int)(travelRequest.ToDate - travelRequest.FromDate).TotalDays;

                Console.WriteLine(3);

                int budget = await CalculateApprovedBudget((int)travelrequesthrdto.RaiseByEmployeeId, travelrequesthrdto.Priority, (travelrequesthrdto.ToDate.GetValueOrDefault() - travelrequesthrdto.FromDate.GetValueOrDefault()).Days);
                string modeOfTravel = await CalculateModeOfTravel();
                var hotelrating = await CalculateHotel((int)travelrequesthrdto.RaiseByEmployeeId);

                
                var travelBudgetAlloction = new TravelBudgetAllocation
                {
                    TravelRequestId = trid,
                    ApprovedBudget = budget,
                    ApprovedModeOfTravel = modeOfTravel,
                    ApprovedHotelStarRating = hotelrating
                };
                await travelBudgetAllocationRepo.AddBudgetAllocation(travelBudgetAlloction);
            }
            var result = await travelRequestRepo.UpdateTravelRequestById(travelRequest);
            return result;
        }

        public async Task<int> CalculateBudget(int travelRequestId)
        {
            TravelRequest travelRequest = await travelRequestRepo.GetTravelRequestById(travelRequestId);

            if (travelRequest == null)
                throw new Exception("Travel Request ID not exist");

            if (travelRequest.RequestStatus != "Approved")
                throw new Exception("Travel Request is not approved so can not calculate the budget for it");
            
            int budget = await CalculateApprovedBudget(travelRequest.RaisedByEmployeeId.GetValueOrDefault(), travelRequest.Priority, (travelRequest.ToDate - travelRequest.FromDate).Days);
            return budget;  
        }

        public async Task<string> CalculateHotel(int requestRaisedById)
        {
            User user = await GetUserByEmployeeId(requestRaisedById);
            string[] HrHotel = { "7-star", "5-star" };
            string[] OthersHotel = { "5-star", "3-star" };
            Random rand = new();
            string hotel = "";
            if (user.Role == "HR")
                hotel = HrHotel[rand.Next(HrHotel.Length)];
            else
                hotel = OthersHotel[rand.Next(OthersHotel.Length)];
            return hotel;
        }

        public async Task<string> CalculateModeOfTravel()
        {
            Random rand = new();
            string[] ModeOfTravel = { "Air", "Train", "Bus" };
            string mode = ModeOfTravel[rand.Next(ModeOfTravel.Length)];
            return await Task.FromResult(mode);

        }
        public async Task<int> CalculateApprovedBudget(int id, string priority, int days)
        {
            User user = await GetUserByEmployeeId(id);
            int userGradeId = user.CurrentGradeId.GetValueOrDefault();
            int finalBudget;
            int maxBudgetByGrade = 0;
            int maxDaysByPriority = 0;

            if (userGradeId == 1)
            {
                maxBudgetByGrade = 15000;
            }
            else if (userGradeId == 2)
            {
                maxBudgetByGrade = 12500;
            }
            else if (userGradeId == 3)
            {
                maxBudgetByGrade = 10000;
            }
            if (priority == "1")
            {
                maxDaysByPriority = 30;
            }
            else if (priority == "2")
            {
                maxDaysByPriority = 20;
            }
            else if (priority == "3")
            {
                maxDaysByPriority = 10;
            }
            else
            {
                throw new Exception("Invalid priority");
            }
            if (days > maxDaysByPriority)
                throw new Exception("Can't go for this long");

            finalBudget = days * maxBudgetByGrade;
            return finalBudget;

        }
        public async Task<IEnumerable<TravelRequestResponseDTO>> GetAllApprovedData(int id)
        {
            var result = await travelRequestRepo.GetAllApprovedData(id);
            List<TravelRequestResponseDTO> appList = new();
            foreach (var item in result)
            {
                TravelRequestResponseDTO t = new(item);
                appList.Add(t);
            }
            return appList;
        }
        public async Task<IEnumerable<ResponseId>> GetAllIds(string status)
        {
            var result = await travelRequestRepo.GetAllIds(status);
            List<ResponseId> appList = new();
            foreach (var item in result)
            {
                ResponseId t = new(item);
                appList.Add(t);
            }
            return appList;
        }
    }
}
