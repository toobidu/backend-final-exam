using ToTienDung0300567.Dtos.Mechanic;
using ToTienDung0300567.Dtos.Vehicle;

namespace ToTienDung0300567.Services.Interfaces;

public interface IMechanicService0300567De21044
{
    Task<int> CreateMechanicAsync(CreateMechanicDTO0300567De21001 dto);
    Task UpdateMechanicAsync(int id, UpdateMechanicDTO0300567De21006 dto);
    Task DeleteMechanicAsync(int id);
    Task<MechanicPagedResultDTO0300567De21019> GetMechanicsAsync(MechanicFilterDTO0300567De21012 filter);
    Task<List<VehicleDTO0300567De21020>> GetMostRepairedVehiclesAsync(int mechanicId);
}