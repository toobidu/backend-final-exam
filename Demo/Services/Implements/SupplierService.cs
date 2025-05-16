using Demo.DbContexts;
using Demo.DTOS;
using Demo.DTOS.Common;
using Demo.Entities;
using Demo.Exceptions;
using Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services.Implements;

public class SupplierService : ISupplierService
{
    private readonly ApplicationDbContext _dbContext;

    public SupplierService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PaginatedResult<SupplierDTO>> GetAllAsync(PaginationParams paginationParams)
    {
        var query = _dbContext.Suppliers.AsQueryable();

        // Apply filtering if keyword is provided
        if (!string.IsNullOrWhiteSpace(paginationParams.Keyword))
        {
            var keyword = paginationParams.Keyword.Trim().ToLower();
            query = query.Where(s => 
                s.supplierName.ToLower().Contains(keyword) || 
                s.supplierAddress.ToLower().Contains(keyword) ||
                s.supplierPhone.ToLower().Contains(keyword));
        }

        // Get total count for pagination
        var totalCount = await query.CountAsync();
        
        // Apply pagination
        var suppliers = await query
            .Skip((paginationParams.PageIndex - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        // Map to DTOs
        var supplierDtos = suppliers.Select(MapToDto).ToList();

        // Return paginated result
        return new PaginatedResult<SupplierDTO>
        {
            Items = supplierDtos,
            TotalCount = totalCount,
            PageIndex = paginationParams.PageIndex,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<SupplierDTO> GetByIdAsync(int id)
    {
        var supplier = await _dbContext.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            throw new UserFriendlyException($"Supplier with ID {id} not found", 404);
        }

        return MapToDto(supplier);
    }

    public async Task<SupplierDTO> CreateAsync(SupplierDTO supplierDto)
    {
        // Check for duplicate name
        if (await _dbContext.Suppliers.AnyAsync(s => s.supplierName == supplierDto.supplierName.Trim()))
        {
            throw new UserFriendlyException($"A supplier with the name '{supplierDto.supplierName.Trim()}' already exists");
        }

        // Map DTO to entity
        var supplier = new Supplier
        {
            supplierName = supplierDto.supplierName.Trim(),
            supplierAddress = supplierDto.supplierAddress.Trim(),
            supplierPhone = supplierDto.supplierPhone.Trim()
        };

        // Add to database
        _dbContext.Suppliers.Add(supplier);
        await _dbContext.SaveChangesAsync();

        // Return updated DTO with ID
        return MapToDto(supplier);
    }

    public async Task<SupplierDTO> UpdateAsync(int id, SupplierDTO supplierDto)
    {
        var supplier = await _dbContext.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            throw new UserFriendlyException($"Supplier with ID {id} not found", 404);
        }

        // Check for duplicate name if name is being changed
        if (supplier.supplierName != supplierDto.supplierName.Trim() && 
            await _dbContext.Suppliers.AnyAsync(s => s.supplierName == supplierDto.supplierName.Trim()))
        {
            throw new UserFriendlyException($"A supplier with the name '{supplierDto.supplierName.Trim()}' already exists");
        }

        // Update properties
        supplier.supplierName = supplierDto.supplierName.Trim();
        supplier.supplierAddress = supplierDto.supplierAddress.Trim();
        supplier.supplierPhone = supplierDto.supplierPhone.Trim();

        // Save changes
        await _dbContext.SaveChangesAsync();

        // Return updated DTO
        return MapToDto(supplier);
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await _dbContext.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            throw new UserFriendlyException($"Supplier with ID {id} not found", 404);
        }

        // Check if supplier is associated with any stores
        var hasStoreAssociations = await _dbContext.StoreSuppliers
            .AnyAsync(ss => ss.SupplierId == id);
            
        if (hasStoreAssociations)
        {
            throw new UserFriendlyException(
                "Cannot delete supplier because it is associated with one or more stores. " +
                "Remove the supplier from all stores first.");
        }

        // Remove from database
        _dbContext.Suppliers.Remove(supplier);
        await _dbContext.SaveChangesAsync();
    }

    // Helper method to map Supplier entity to SupplierDTO
    private static SupplierDTO MapToDto(Supplier supplier)
    {
        return new SupplierDTO
        {
            supplierId = supplier.supplierId,
            supplierName = supplier.supplierName,
            supplierAddress = supplier.supplierAddress,
            supplierPhone = supplier.supplierPhone
        };
    }
}