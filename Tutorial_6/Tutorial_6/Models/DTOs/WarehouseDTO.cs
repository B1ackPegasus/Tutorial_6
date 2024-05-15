using System.ComponentModel.DataAnnotations;

namespace Tutorial_6.Models.DTOs;

public class WarehouseDTO
{
    [Required]
    public int IdProduct { set; get;}
    [Required]
    public int IdWarehouse { set; get;}
    [Required]
    public int Amount { set; get;}
    [Required]
    public DateTime CreatedAt { set; get;}
    
}