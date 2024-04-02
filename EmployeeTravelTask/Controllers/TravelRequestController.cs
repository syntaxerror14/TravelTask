using EmployeeTravelTask.DTOs.Request;
using EmployeeTravelTask.DTOs.Response;
using EmployeeTravelTask.Models;
using EmployeeTravelTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeTravelTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRequestController : ControllerBase
    {
        private readonly ILocationService locationService;
        private readonly ITravelRequestService travelRequestService;

        public TravelRequestController(ILocationService locationService, ITravelRequestService travelRequestService)
        {
            this.locationService = locationService;
            this.travelRequestService = travelRequestService;
        }

        // EndPoint-1 (this endpoint will provide employees with a list of locations which they can select while creating a new travel request)
        [HttpGet("locations")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var result = await locationService.GetAllLocations();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        // EndPoint - 2 (using this endpoint the employees can create a new travel request)
        [HttpPost("new")]
        public async Task<IActionResult> AddTravelRequest(TravelRequestDTO u)
        {
            try
            {
                var result = await travelRequestService.AddTravelRequest(u);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(title: "Error",detail:ex.Message, statusCode: 400);
            }
        }

        // EndPoint - 3 (A HR will be able to fetch a list of newly created travel request which needs to be approved by him/her)
        [HttpGet("{HRid}/pending")]
        public async Task<IActionResult> AllPendingRequestByHR(int HRid)
        {
            try
            {
                var result = await travelRequestService.AllPendingRequestByHR(HRid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
                
        }

        //EndPoint - 4 (The endpoint will be resposible for fetching and displaying the details of a travel request raise earlier. If request is approved it should also include the approval details)                      
        [HttpGet("{trid})")]
        public async Task<IActionResult> GetTravelRequestById(int trid)
        {
            try
            {
                var result = await travelRequestService.GetTravelRequestById(trid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        //EndPoint - 5 (A HR will use this endpoint to approve or reject the travel request raised by his employees)
        [HttpPut("{trid}/update")]
        public async Task<IActionResult> UpdateTravelRequestById(int trid,TravelRequestHRRequestDTO u)
        {
            try
            {
                await travelRequestService.UpdateTravelRequestById(trid, u);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }

        //EndPoint - 6 (This endpoint will be responsible for calculating and returning the applicale budget for the travel)
        [HttpPost("calculatebudget")]
        public async Task<IActionResult> CalculateBudget(int travelRequestid)
        {
            try
            {
                var result = await travelRequestService.CalculateBudget(travelRequestid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: 400);
            }
        }
    }
}
