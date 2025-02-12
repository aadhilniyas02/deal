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

        // GET all deals - list ( returns View Model)
         public async Task<List<DealViewModel>> GetDealsAsync()
        {
             var deals = await _context.DealManagementSystem.ToListAsync();
            return deals.Select(DealViewModel.fromDeal).ToList();
        }


        // GET One Deal by id ( returns View Model )
        public async Task<DealViewModel?> FindDealByIdAsync(int id)
        {
            var deal = await _context.DealManagementSystem.FindAsync(id);
            return deal == null ? null : DealViewModel.fromDeal(deal);
        }



        // CREAT a new Deal ( stores DealDTO , returns DTO)
        public async Task<DealDto> AddDealAsync(DealDto dealToCreate)
        {
            var deal = new Deal
            {
                Name = dealToCreate.Name,
                Slug = dealToCreate.Slug,
                Title = dealToCreate.Title
            };

            _context.DealManagementSystem.Add(deal);
            await _context.SaveChangesAsync();

            return dealToCreate;
        }



        // Update a deal (returns updated deal)
        public async Task<DealDto?> UpdateDealAsync(int id, DealDto dealToUpdate)
        {
            var deal = await _context.DealManagementSystem.FindAsync(id);
            if(deal == null) return null;   /// checking if there is a deal, doesn't exist - return as null
            
            // if Deal is existing - Updating the deal
            deal.Name = dealToUpdate.Name;
            deal.Slug = dealToUpdate.Slug;
            deal.Title = dealToUpdate.Title;

            await _context.SaveChangesAsync();
            return dealToUpdate; // returns the updated deal 
        }
        

        // Delete a deal ( returns true if deleted, false if not found)
        public async Task<bool> DeleteDealAsync(int id)
        {
            var deal = await _context.DealManagementSystem.FindAsync(id);
            if (deal == null) return false; /// checking if there is existing deal, if not found returns false

            _context.DealManagementSystem.Remove(deal);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }

}