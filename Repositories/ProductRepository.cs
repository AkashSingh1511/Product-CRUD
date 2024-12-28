using ProductCRUD.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace ProductCRUD.Repositories
{
        public class ProductRepository : IProductRepository
        {
            private readonly IDbConnection _dbConnection;

            public ProductRepository(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public async Task<IEnumerable<Product>> GetAllProductsAsync()
            {
                var query = "SELECT * FROM Products";
                return await _dbConnection.QueryAsync<Product>(query);
            }

            public async Task<Product> GetProductByIdAsync(int id)
            {
                var query = "SELECT * FROM Products WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            }

            public async Task AddProductAsync(Product product)
            {
                var query = "INSERT INTO Products (Name, Description, Created) VALUES (@Name, @Description, GETDATE())";
                await _dbConnection.ExecuteAsync(query, product);
            }

            public async Task UpdateProductAsync(Product product)
            {
                var query = "UPDATE Products SET Name = @Name, Description = @Description WHERE Id = @Id";
                await _dbConnection.ExecuteAsync(query, new { product.Name, product.Description, product.Id });
            }


            public async Task DeleteProductAsync(int id)
            {
                var query = "DELETE FROM Products WHERE Id = @Id";
                await _dbConnection.ExecuteAsync(query, new { Id = id });


            }

        }
    }
