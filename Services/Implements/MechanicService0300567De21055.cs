using Microsoft.EntityFrameworkCore;
using ToTienDung0300567.DbContexts;
using ToTienDung0300567.Dtos.Mechanic;
using ToTienDung0300567.Dtos.Vehicle;
using ToTienDung0300567.Entities;
using ToTienDung0300567.Exceptions;
using ToTienDung0300567.Services.Interfaces;

namespace ToTienDung0300567.Services.Implements;

public class MechanicService0300567De21055 : IMechanicService0300567De21044
{
    private readonly AppDbContexts0300567De21026 _dbContext;

    public MechanicService0300567De21055(AppDbContexts0300567De21026 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateMechanicAsync(CreateMechanicDTO0300567De21001 dto)
    {
        // Check for duplicates
        if (await _dbContext.Mechanics.AnyAsync(m => m.maTho == dto.maTho))
            throw new UserFriendlyException0300567De20954("Mã thợ đã tồn tại");
            
        if (await _dbContext.Mechanics.AnyAsync(m => m.tenTho == dto.tenTho))
            throw new UserFriendlyException0300567De20954("Tên thợ đã tồn tại");
            
        if (await _dbContext.Mechanics.AnyAsync(m => m.cCCD == dto.cCCD))
            throw new UserFriendlyException0300567De20954("CCCD đã tồn tại");

        var mechanic = new Mechanic0300567De20945
        {
            maTho = dto.maTho,
            tenTho = dto.tenTho,
            cCCD = dto.cCCD,
            ngayNhanViec = DateTime.Now
        };

        _dbContext.Mechanics.Add(mechanic);
        await _dbContext.SaveChangesAsync();
        
        return mechanic.id;
    }

    public async Task UpdateMechanicAsync(int id, UpdateMechanicDTO0300567De21006 dto)
    {
        var mechanic = await _dbContext.Mechanics.FindAsync(id) 
            ?? throw new UserFriendlyException0300567De20954("Không tìm thấy thợ");

        // Check for duplicates (excluding current mechanic)
        if (await _dbContext.Mechanics.AnyAsync(m => m.maTho == dto.maTho && m.id != id))
            throw new UserFriendlyException0300567De20954("Mã thợ đã tồn tại");
            
        if (await _dbContext.Mechanics.AnyAsync(m => m.tenTho == dto.tenTho && m.id != id))
            throw new UserFriendlyException0300567De20954("Tên thợ đã tồn tại");
            
        if (await _dbContext.Mechanics.AnyAsync(m => m.cCCD == dto.cCCD && m.id != id))
            throw new UserFriendlyException0300567De20954("CCCD đã tồn tại");

        mechanic.maTho = dto.maTho;
        mechanic.tenTho = dto.tenTho;
        mechanic.cCCD = dto.cCCD;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMechanicAsync(int id)
    {
        var mechanic = await _dbContext.Mechanics.FindAsync(id)
            ?? throw new UserFriendlyException0300567De20954("Không tìm thấy thợ");

        // Check if mechanic has repair records
        if (await _dbContext.RepairRecords.AnyAsync(r => r.mechanicId == id))
            throw new UserFriendlyException0300567De20954("Không thể xóa thợ đã có lịch sử sửa chữa");

        _dbContext.Mechanics.Remove(mechanic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MechanicPagedResultDTO0300567De21019> GetMechanicsAsync(MechanicFilterDTO0300567De21012 filter)
    {
        var query = _dbContext.Mechanics.AsQueryable();

        // Apply filtering
        if (!string.IsNullOrEmpty(filter.Keyword))
        {
            query = query.Where(m => 
                m.tenTho.Contains(filter.Keyword) || 
                m.cCCD.Contains(filter.Keyword));
        }

        // Get total count for pagination
        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);

        // Apply pagination
        var mechanics = await query
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(m => new MechanicDTO0300567De20956
            {
                id = m.id,
                maTho = m.maTho,
                tenTho = m.tenTho,
                cCCD = m.cCCD,
                ngayNhanViec = m.ngayNhanViec
            })
            .ToListAsync();

        return new MechanicPagedResultDTO0300567De21019
        {
            TotalPages = totalPages,
            Items = mechanics
        };
    }

    public async Task<List<VehicleDTO0300567De21020>> GetMostRepairedVehiclesAsync(int mechanicId)
    {
        // Check if mechanic exists
        if (!await _dbContext.Mechanics.AnyAsync(m => m.id == mechanicId))
            throw new UserFriendlyException0300567De20954("Không tìm thấy thợ");

        // Get repair records for the mechanic
        var repairRecords = await _dbContext.RepairRecords
            .Where(r => r.mechanicId == mechanicId)
            .ToListAsync();

        if (!repairRecords.Any())
            return new List<VehicleDTO0300567De21020>();

        // Find the maximum repair count
        var maxRepairCount = repairRecords.Max(r => r.soLanSua);

        // Get vehicles with the maximum repair count
        var vehicleIds = repairRecords
            .Where(r => r.soLanSua == maxRepairCount)
            .Select(r => r.vehicleId)
            .ToList();

        // Get vehicle details
        var result = await _dbContext.Vehicles
            .Where(v => vehicleIds.Contains(v.id))
            .Select(v => new VehicleDTO0300567De21020()
            {
                loaiXe = v.loaiXe,
                bienSoXe = v.bienSoXe,
                soLanSua = maxRepairCount
            })
            .OrderByDescending(v => v.bienSoXe)
            .ToListAsync();

        return result;
    }
}