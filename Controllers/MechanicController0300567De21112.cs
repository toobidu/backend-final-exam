using Microsoft.AspNetCore.Mvc;
using ToTienDung0300567.Dtos.Mechanic;
using ToTienDung0300567.Exceptions;
using ToTienDung0300567.Services.Interfaces;

namespace ToTienDung0300567.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MechanicController0300567De21112 : ControllerBase
{
    private readonly IMechanicService0300567De21044 _mechanicService;

    public MechanicController0300567De21112(IMechanicService0300567De21044 mechanicService)
    {
        _mechanicService = mechanicService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMechanic([FromBody] CreateMechanicDTO0300567De21001 dto)
    {
        try
        {
            var id = await _mechanicService.CreateMechanicAsync(dto);
            return Ok(new { Id = id, Message = "Thêm thợ thành công" });
        }
        catch (UserFriendlyException0300567De20954 ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMechanic(int id, [FromBody] UpdateMechanicDTO0300567De21006 dto)
    {
        try
        {
            await _mechanicService.UpdateMechanicAsync(id, dto);
            return Ok(new { Message = "Cập nhật thợ thành công" });
        }
        catch (UserFriendlyException0300567De20954 ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMechanic(int id)
    {
        try
        {
            await _mechanicService.DeleteMechanicAsync(id);
            return Ok(new { Message = "Xóa thợ thành công" });
        }
        catch (UserFriendlyException0300567De20954 ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMechanics([FromQuery] MechanicFilterDTO0300567De21012 filter)
    {
        try
        {
            var result = await _mechanicService.GetMechanicsAsync(filter);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi khi lấy danh sách thợ" });
        }
    }

    [HttpGet("{mechanicId}/count-repair-vehicle")]
    public async Task<IActionResult> GetMostRepairedVehicles(int mechanicId)
    {
        try
        {
            var result = await _mechanicService.GetMostRepairedVehiclesAsync(mechanicId);
            return Ok(result);
        }
        catch (UserFriendlyException0300567De20954 ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}