using Demo.DTOS;
using Demo.DTOS.Common;

namespace Demo.Services.Interfaces;

public interface ISupplierService
{
    Task<PaginatedResult<SupplierDTO>> GetAllAsync(PaginationParams paginationParams);
    Task<SupplierDTO> GetByIdAsync(int id);
    Task<SupplierDTO> CreateAsync(SupplierDTO supplierDto);
    Task<SupplierDTO> UpdateAsync(int id, SupplierDTO supplierDto);
    Task DeleteAsync(int id);
}

