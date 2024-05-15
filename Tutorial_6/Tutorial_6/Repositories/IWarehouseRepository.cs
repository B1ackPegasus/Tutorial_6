using Tutorial_6.Models.DTOs;

namespace Tutorial_6.Repositories;

public interface IWarehouseRepository
{
    public int? InsertIntoProductWarehouse(WarehouseDTO warehouseDto);
    public int? InsertIntoProductWarehouseWithProcedure(WarehouseDTO warehouseDto);
}