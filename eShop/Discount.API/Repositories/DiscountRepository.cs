using Dapper;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly string? _connectionString;

    public DiscountRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        const string sql = "SELECT * FROM Coupon WHERE ProductName = @ProductName";

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductName = productName });

        return coupon ?? new Coupon
        {
            ProductName = productName,
            Amount = 0,
            Description = "No discount available"
        };
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        if (coupon == null)
            throw new ArgumentNullException(nameof(coupon));

        if (string.IsNullOrWhiteSpace(coupon.ProductName))
            throw new ArgumentException("Product name is required", nameof(coupon.ProductName));

        if (coupon.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0", nameof(coupon.Amount));

        await using var connection = new NpgsqlConnection(_connectionString);

        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new { coupon.ProductName, coupon.Description, coupon.Amount });

        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var parameters = new DynamicParameters();
        parameters.Add("Id", coupon.Id);
        parameters.Add("ProductName", coupon.ProductName.Trim());
        parameters.Add("Description", coupon.Description?.Trim() ?? string.Empty);
        parameters.Add("Amount", coupon.Amount);
        parameters.Add("UpdatedDate", DateTime.UtcNow);

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount, UpdatedDate = @UpdatedDate WHERE Id = @Id",
            parameters);

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",new { ProductName = productName });

        return affected > 0;     
    }
}
