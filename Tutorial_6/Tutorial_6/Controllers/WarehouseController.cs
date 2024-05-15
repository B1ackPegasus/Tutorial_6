using Microsoft.AspNetCore.Mvc;
using Tutorial_6.Models.DTOs;
using Tutorial_6.Sevices;

namespace Tutorial_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
  private IWarehouseService _warehouseService;
  public WarehouseController(IWarehouseService warehouseService)
  {
    _warehouseService = warehouseService;
  }

  [HttpPost]
  public IActionResult InsertIntoProductWarehouse(WarehouseDTO warehouseDto)
  {
    int? Id = _warehouseService.InsertIntoProductWarehouse(warehouseDto);
    if (Id != null)
    {
      return Ok(Id);
    }
    return StatusCode(StatusCodes.Status400BadRequest);
  }

  [HttpPost("insertWithProcedure")]
  public IActionResult InsertIntoProductWarehouseWithProcedure(WarehouseDTO warehouseDto)
  {
    int? Id = _warehouseService.InsertIntoProductWarehouseWithProcedure(warehouseDto);
    if (Id != null)
    {
      return Ok(Id);
    }
    return StatusCode(StatusCodes.Status400BadRequest);
  
  }
}