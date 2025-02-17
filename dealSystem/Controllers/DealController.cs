using dealSystem.Model;
using dealSystem.services;
using dealSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dealSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DealController : ControllerBase
    {
        private readonly IDealService _dealservice;

        public DealController(IDealService dealService)
        {
            _dealservice = dealService;
        }

        // GET all Deals ( returns View Model )
        [HttpGet]
        public async Task<ActionResult<List<DealViewModel>>> GetDeals()
        {
            var deals = await _dealservice.GetDealsAsync();
            return Ok(deals);
        }


        // GET a Deal by ID ( returns View Model )
        [HttpGet("{id}")]
        public async Task<ActionResult<DealViewModel>> GetDealById(int id)
        {
            var deal = await _dealservice.FindDealByIdAsync(id);
            return Ok(deal);
        }



        // CREATE a new Deal
        [HttpPost]
        public async Task<ActionResult<DealDto>> CreateDeal(DealDto dealToCreate)
        {
            var deal = await _dealservice.AddDealAsync(dealToCreate);
            return Ok(deal);
        }


        // PUT - Update an existing deal ( retunrs updated deal )
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeal(int id, DealDto dealToUpdate)
        {
            var updatedDeal = await _dealservice.UpdateDealAsync(id, dealToUpdate);
            return Ok(updatedDeal);
        }


        // DELETE - Remove a deal ( return successfull message)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal (int id)
        {
            var result = await _dealservice.DeleteDealAsync(id);

            return Ok("Deal Deleted Successfully");
        }

        [HttpPost("{dealId}/add-hotel")]
        public async Task<ActionResult<Hotel>> AddHotelToDeal(int dealId, Hotel hotel)
        {
            var addedHotel = await _dealservice.AddHotelToDealAsync(dealId, hotel);
            return Ok(addedHotel);
        }

        [HttpDelete("delete-hotel/{hotelId}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var result = await _dealservice.DeleteHotelAsync(hotelId);
            return Ok("Hotel Deleted Successfully");
        }

    }
}