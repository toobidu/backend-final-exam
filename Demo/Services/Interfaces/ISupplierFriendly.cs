using Demo.DTOS;

namespace Demo.Services.Interfaces;

public interface ISupplierFriendly
{
    Task<List<SupplierFriendlyDTO>> GetSuppliersByStoreIdAsync(int storeId);
}