using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using dealSystem.services;


namespace dealSystem.services
{
    public class IDealService: InterfaceService
    {
        private readonly DealContext _context;

        public IDealService(DealContext context)
        {
            _context = context;
        }

         public async Task<List<Deal>> GetDealsAsync()
        {
            var deals = await _context.DealManagementSystem.ToListAsync();
            return deals;
        }

        public async Task<Deal> FindDealByIdAsync(int id)
        {
            var deals = await _context.DealManagementSystem.FindAsync(id);
            return deals;
        }


        public async Task<Deal> AddDealAsync(DealDto dealToCreate)
        {
            var deal = new Deal
            {
                Name = dealToCreate.Name,
                Slug = dealToCreate.Slug,
                Title = dealToCreate.Title
            };

            _context.DealManagementSystem.Add(deal);
            await _context.SaveChangesAsync();

            return deal;
        }



        public async Task<Deal> UpdateDealAsync(int id, DealDto dealToUpdate)
        {
            var deal = await _context.DealManagementSystem.FindAsync(id);
            if(deal == null) return null;   
            
            deal.Name = dealToUpdate.Name;
            deal.Slug = dealToUpdate.Slug;
            deal.Title = dealToUpdate.Title;

            await _context.SaveChangesAsync();
            return deal; 
        }
        

    
        public async Task<bool> DeleteDealAsync(int id)
        {
            var deal = await _context.DealManagementSystem.FindAsync(id);
            if (deal == null) return false; 

            _context.DealManagementSystem.Remove(deal);
            await _context.SaveChangesAsync();
            return true;
        }

        

        // private static DealViewModel ConvertDealModelToDealViewModel (Deal deal)
        // {
        //     return new DealViewModel
        //     {
        //         Name = deal.Name,
        //         Slug = deal.Slug,
        //         Title = deal.Title
        //     };
        // }

        // private static Deal ConvertDealDtoToDealModel (DealDto dealDto)
        // {
        //     return new Deal 
        //     {
        //         Name = dealDto.Name,
        //         Slug = dealDto.Slug,
        //         Title = dealDto.Title
        //     };
        // }
    }

}