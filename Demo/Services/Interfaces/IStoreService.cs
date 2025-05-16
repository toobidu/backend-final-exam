using Demo.DTOS;
using Demo.DTOS.Common;

namespace Demo.Services.Interfaces;

public interface IStoreService
{
    Task<StoreDTO> GetStoreByIdAsync(int storeId);
    Task<PaginatedResult<StoreDTO>> GetStoresAsync(PaginationParams paginationParams);
    Task<StoreDTO> CreateStoreAsync(CreateStoreDTO createStoreDTO);
    Task<StoreDTO> UpdateStoreAsync(int storeId, UpdateStoreDTO updateStoreDTO);
    Task DeleteStoreAsync(int storeId);
    
}