using Tutorial_6.Models.DTOs;
using Tutorial_6.Repositories;

namespace Tutorial_6.Sevices;

public interface IWarehouseService
{
    public int? InsertIntoProductWarehouse(WarehouseDTO warehouseDto);
    public int? InsertIntoProductWarehouseWithProcedure(WarehouseDTO warehouseDto);
}