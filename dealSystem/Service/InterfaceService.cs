using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.services
{
    // Defines all CRUD MethodS // DTO using for input and output
    public interface InterfaceService
    {
        Task<List<DealViewModel>> GetDealsAsync(); // Get all deals - list of deals - using View Model - getting from database
        Task<DealViewModel?> FindDealByIdAsync(int id); // Get one Deal by Id - find - using View Model - getting from base
        Task<DealDto> AddDealAsync(DealDto dealToCreate); // Add deal - using DTO - sending to database
        Task<DealDto?> UpdateDealAsync(int id, DealDto dealToUpdate); // Updating an item by ID - using DTo - sending to database
        Task<bool> DeleteDealAsync(int id); // Delete an item by giving existing ID
    }


}