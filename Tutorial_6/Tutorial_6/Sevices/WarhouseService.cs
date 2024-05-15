using Tutorial_6.Models.DTOs;
using Tutorial_6.Repositories;

namespace Tutorial_6.Sevices;

public class WarehouseService : IWarehouseService
{
    private IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }


    public int? InsertIntoProductWarehouse(WarehouseDTO warehouseDto)
    {
        return _warehouseRepository.InsertIntoProductWarehouse(warehouseDto);
    }

    public int? InsertIntoProductWarehouseWithProcedure(WarehouseDTO warehouseDto)
    {
        return _warehouseRepository.InsertIntoProductWarehouseWithProcedure(warehouseDto);
    }
}