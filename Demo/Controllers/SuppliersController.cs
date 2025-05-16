using Demo.DTOS;
using Demo.DTOS.Common;
using Demo.Exceptions;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SupplierDTO>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            var result = await _supplierService.GetAllAsync(paginationParams);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDTO>> GetById(int id)
    {
        try
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            return Ok(supplier);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // [HttpGet("store/{storeId}/friendly")]
    // public async Task<ActionResult<List<SupplierFriendlyDTO>>> GetSuppliersByStoreId(int storeId)
    // {
    //     try
    //     {
    //         var suppliers = await _supplierService.GetSuppliersByStoreIdAsync(storeId);
    //         return Ok(suppliers);
    //     }
    //     catch (Exception ex)
    //     {
    //         return HandleException(ex);
    //     }
    // }

    [HttpPost]
    public async Task<ActionResult<SupplierDTO>> Create([FromBody] SupplierDTO supplierDto)
    {
        try
        {
            var createdSupplier = await _supplierService.CreateAsync(supplierDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSupplier.supplierId }, createdSupplier);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierDTO>> Update(int id, [FromBody] SupplierDTO supplierDto)
    {
        try
        {
            var updatedSupplier = await _supplierService.UpdateAsync(id, supplierDto);
            return Ok(updatedSupplier);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _supplierService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    private ActionResult HandleException(Exception ex)
    {
        if (ex is UserFriendlyException userFriendlyEx)
        {
            return StatusCode(userFriendlyEx.StatusCode, new { error = userFriendlyEx.Message });
        }

        // Log the exception here
        return StatusCode(500, new { error = "An unexpected error occurred." });
    }
}