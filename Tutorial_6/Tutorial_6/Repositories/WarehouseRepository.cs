using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial_6.Models.DTOs;

namespace Tutorial_6.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;

    public WarehouseRepository (IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public int? InsertIntoProductWarehouse(WarehouseDTO warehouseDto)
    {
        using SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using SqlCommand com = new SqlCommand();
        con.Open();
       
        com.Connection = con;
        com.CommandText =
            "SELECT Product.Price FROM Product Where IdProduct = @IdProduct" +
            " AND EXISTS (SELECT IdWarehouse FROM Warehouse WHERE Warehouse.IdWarehouse = @WarehouseId)";
        com.Parameters.AddWithValue("@IdProduct",warehouseDto.IdProduct );
        com.Parameters.AddWithValue("@WarehouseId",warehouseDto.IdWarehouse );
        decimal? Price = null;
        if (warehouseDto.Amount>0)
        { 
            var dr = com.ExecuteReader();
            if (!dr.HasRows)
            {
                return null;
            }

            bool row = dr.Read();
            Price = (decimal)dr["Price"];
            dr.Close();
        }
        else
        {
            return null;
        }
       
        int? IdOrder = null;
        com.CommandText =
            "SELECT [Order].IdOrder FROM [Order] WHERE [Order].IdProduct = @ProductIDS " +
            "AND [Order].Amount = @Amount AND [Order].CreatedAt < @CreatedAt";
        com.Parameters.AddWithValue("@ProductIDS",warehouseDto.IdProduct );
        com.Parameters.AddWithValue("@Amount",warehouseDto.Amount);
        com.Parameters.AddWithValue("@CreatedAt",warehouseDto.CreatedAt);
        
        var reader = com.ExecuteReader();
        
        if (!reader.Read())
        {
            return null;
        }

        IdOrder = (int)reader["IdOrder"];
        reader.Close();
        
        
        com.CommandText =
            "SELECT IdProductWarehouse FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        com.Parameters.AddWithValue("@IdOrder",IdOrder );
        
     
        reader = com.ExecuteReader();
        
        
        if (reader.Read())
        {
            return null;
        }
        
        
        DateTime now  = DateTime.Now;
        com.CommandText = "UPDATE [Order] SET FulfilledAt = @NowTime WHERE [Order].IdOrder = @OrderId";
        com.Parameters.AddWithValue("@OrderId",IdOrder );
        com.Parameters.AddWithValue("@NowTime",now);
        reader.Close();
       var upd = com.ExecuteNonQuery();

       com.CommandText = "INSERT INTO Product_Warehouse( IdWarehouse ,IdProduct , IdOrder,Amount,Price ,CreatedAt)" +
                         "VALUES (@WhouseId,@IdProd,@IdOrd, @AmountAll,@PriceFor,@CreatedDate)" +
                         "SELECT CONVERT(INT, SCOPE_IDENTITY()) AS LastRowId;";
       com.Parameters.AddWithValue("@WhouseId",warehouseDto.IdWarehouse );
       com.Parameters.AddWithValue("@IdProd",warehouseDto.IdProduct );
       com.Parameters.AddWithValue("@IdOrd",IdOrder );
       com.Parameters.AddWithValue("@AmountAll",warehouseDto.Amount );
       com.Parameters.AddWithValue("@PriceFor",Price*warehouseDto.Amount );
       com.Parameters.AddWithValue("@CreatedDate",warehouseDto.CreatedAt);

       reader= com.ExecuteReader();
       int? IdOfLastRow = null;
       if (reader.Read())
       {
           IdOfLastRow = (int)reader["LastRowId"];
       }

       return IdOfLastRow;
       
    }

    public int? InsertIntoProductWarehouseWithProcedure(WarehouseDTO warehouseDto)
    {
        using var Con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using var Com = new SqlCommand("AddProductToWarehouse", Con)
        {
            CommandType = CommandType.StoredProcedure
            
        };
            Com.Parameters.AddWithValue("@IdProduct", warehouseDto.IdProduct);
            Com.Parameters.AddWithValue("@IdWarehouse", warehouseDto.IdProduct);
            Com.Parameters.AddWithValue("@Amount", warehouseDto.Amount);
            Com.Parameters.AddWithValue("@CreatedAt", warehouseDto.CreatedAt);
            Con.Open();
            var reader = Com.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            int Id =(int)(decimal) reader["NewId"];
            return Id;

    }
}