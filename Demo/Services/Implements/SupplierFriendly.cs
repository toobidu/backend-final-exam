using Demo.DbContexts;
using Demo.DTOS;
using Demo.Exceptions;
using Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services.Implements;

public class SupplierFriendly : ISupplierFriendly
{
    private readonly ApplicationDbContext _dbContext;

    public SupplierFriendly(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<SupplierFriendlyDTO>> GetSuppliersByStoreIdAsync(int storeId)
    {
        // Check if store exists
        if (!await _dbContext.Stores.AnyAsync(s => s.storeId == storeId))
        {
            throw new UserFriendlyException($"Store with ID {storeId} not found", 404);
        }

        // Get suppliers with highest friendliness level
        var suppliers = await _dbContext.StoreSuppliers
            .Where(ss => ss.StoreId == storeId)
            .OrderByDescending(ss => ss.friendlyLevel)
            .Select(ss => new SupplierFriendlyDTO
            {
                supplierName = ss.Supplier.supplierName,
                supplierPhone = ss.Supplier.supplierPhone,
                friendlyLevel = ss.friendlyLevel
            })
            .ToListAsync();

        return suppliers;
    }
}