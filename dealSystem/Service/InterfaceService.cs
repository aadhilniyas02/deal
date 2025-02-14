using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.services
{

    public interface InterfaceService
    {
        Task<List<Deal>> GetDealsAsync(); 
        Task<Deal> FindDealByIdAsync(int id); 
        Task<Deal> AddDealAsync(DealDto dealToCreate); 
        Task<Deal> UpdateDealAsync(int id, DealDto dealToUpdate); 
        Task<bool> DeleteDealAsync(int id); 

        Task<List<Hotel>> GetAllHotelsAsycn();
        Task<Hotel> FindHotelById(int id);
        Task<Hotel> AddHotelAsync();
        Task<Hotel> UpdateHotelAsync(int id, );
        Task<bool> DeleteHotelAsync(int id);
        
    }


}