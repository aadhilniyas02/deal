using dealSystem.Data;
using dealSystem.Model;
using System.Linq;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using dealSystem.services;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;


namespace dealSystem.services
{
    public class IDealService: InterfaceService
    {
        private readonly DealContext _context;
        private object d;

        public IDealService(DealContext context)
        {
            _context = context;
        }

         public async Task<List<Deal>> GetDealsAsync()
        {
            var deals = await _context.DealManagementSystem.Include(d => d.Hotels).ToListAsync();
            return  deals;
        }

        public async Task<Deal> FindDealByIdAsync(int id)
        {
            var deals = await _context.DealManagementSystem.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id == id); 
            return deals;
        }


        public async Task<Deal> AddDealAsync(DealDto dealToCreate)
        {
            var deal = new Deal
            {
                Name = dealToCreate.Name,
                Slug = dealToCreate.Slug,
                Title = dealToCreate.Title,
            };

            if(dealToCreate.Hotels != null && dealToCreate.Hotels.Any())
            {
                deal.Hotels = dealToCreate.Hotels;
            }

            _context.DealManagementSystem.Add(deal);
            await _context.SaveChangesAsync();

            return deal;
        }



        public async Task<Deal> UpdateDealAsync(int id, DealDto dealToUpdate)
        {
            var deal = await _context.DealManagementSystem.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id == id);
            if(deal == null)
                throw new KeyNotFoundException("Deal not found");
            
            deal.Name = dealToUpdate.Name;
            deal.Slug = dealToUpdate.Slug;
            deal.Title = dealToUpdate.Title;

            var hotels = dealToUpdate.Hotels;
            if(hotels != null)
            {
                deal.Hotels.Clear();
            }

            await _context.SaveChangesAsync();
            return deal; 
        }
        

    
        public async Task<bool> DeleteDealAsync(int id)
        {
            var deal = await _context.DealManagementSystem.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id ==id);
            if (deal == null) return false;

            _context.DealManagementSystem.Remove(deal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Hotel> AddHotelToDealAsync(int dealId, Hotel hotel)
        {
            var deal = await _context.DealManagementSystem.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id == dealId);
            if( deal == null)
                throw new KeyNotFoundException("Deal Not Found");

            
            hotel.DealId = dealId;
            _context.HotelsTable.Add(hotel);
            return hotel;
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _context.HotelsTable.FindAsync(hotelId);
            if(hotel == null) return false;

            _context.HotelsTable.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        private static DealViewModel ConvertDealModelToDealViewModel (Deal deal)
        {
            return new DealViewModel
            {
                Name = deal.Name,
                Slug = deal.Slug,
                Title = deal.Title
            };
        }

        private static Deal ConvertDealDtoToDealModel (DealDto dealDto)
        {
            return new Deal 
            {
                Name = dealDto.Name,
                Slug = dealDto.Slug,
                Title = dealDto.Title
            };
        }
    }

}