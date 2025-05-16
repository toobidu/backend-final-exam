using Demo.DbContexts;
using Demo.DTOS;
using Demo.DTOS.Common;
using Demo.Entities;
using Demo.Exceptions;
using Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services.Implements;

public class StoreService : IStoreService
{
    private readonly ApplicationDbContext _dbContext;

    public StoreService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StoreDTO> GetStoreByIdAsync(int id)
    {
        var store = await _dbContext.Stores.FindAsync(id);
        if (store == null)
        {
            throw new UserFriendlyException($"Store with ID {id} not found", 404);
        }

        return MapToDto(store);
    }

    public async Task<PaginatedResult<StoreDTO>> GetStoresAsync(PaginationParams paginationParams)
    {
        var query = _dbContext.Stores.AsQueryable();

        if (!string.IsNullOrWhiteSpace(paginationParams.Keyword))
        {
            var keyword = paginationParams.Keyword.Trim().ToLower();
            query = query.Where(s => 
                s.storeName.ToLower().Contains(keyword) || 
                s.storeAddress.ToLower().Contains(keyword));
        }

        var totalCount = await query.CountAsync();
        
        var stores = await query
            .Skip((paginationParams.PageIndex - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PaginatedResult<StoreDTO>
        {
            Items = stores.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            PageIndex = paginationParams.PageIndex,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<StoreDTO> CreateStoreAsync(CreateStoreDTO createStoreDto)
    {
        // Check for duplicate name
        if (await _dbContext.Stores.AnyAsync(s => s.storeName == createStoreDto.storeName.Trim()))
        {
            throw new UserFriendlyException($"A store with the name '{createStoreDto.storeName.Trim()}' already exists");
        }

        var store = new Store
        {
            storeName = createStoreDto.storeName.Trim(),
            storeAddress = createStoreDto.storeAddress.Trim(),
            storePhone = createStoreDto.storePhone.Trim(),
            openTime = createStoreDto.openTime,
            closeTime = createStoreDto.closeTime
        };

        _dbContext.Stores.Add(store);
        await _dbContext.SaveChangesAsync();

        return MapToDto(store);
    }

    public async Task<StoreDTO> UpdateStoreAsync(int id, UpdateStoreDTO updateStoreDto)
    {
        var store = await _dbContext.Stores.FindAsync(id);
        if (id == null)
        {
            throw new UserFriendlyException($"Store with ID {id} not found", 404);
        }

        // Check for duplicate name if name is being changed
        if (store.storeName != updateStoreDto.storeName.Trim() && 
            await _dbContext.Stores.AnyAsync(s => s.storeName == updateStoreDto.storeName.Trim()))
        {
            throw new UserFriendlyException($"A store with the name '{updateStoreDto.storeName.Trim()}' already exists");
        }

        store.storeName = updateStoreDto.storeName.Trim();
        store.storeAddress = updateStoreDto.storeAddress.Trim();
        store.storePhone = updateStoreDto.storePhone.Trim();
        store.openTime = updateStoreDto.openTime;
        store.closeTime = updateStoreDto.closeTime;

        await _dbContext.SaveChangesAsync();

        return MapToDto(store);
    }

    public async Task DeleteStoreAsync(int id)
    {
        var store = await _dbContext.Stores.FindAsync(id);
        if (store == null)
        {
            throw new UserFriendlyException($"Store with ID {id} not found", 404);
        }

        _dbContext.Stores.Remove(store);
        await _dbContext.SaveChangesAsync();
    }

    private static StoreDTO MapToDto(Store store)
    {
        return new StoreDTO()
        {
            storeId = store.storeId,
            storeName = store.storeName,
            storePhone = store.storePhone,
            storeAddress = store.storeAddress,
            openTime = store.openTime,
            closeTime = store.closeTime
        };
    }
}