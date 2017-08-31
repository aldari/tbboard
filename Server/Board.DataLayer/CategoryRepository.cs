using Board.DataLayer;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        public ConnectionConfig ConnectionConfig { get; }

        public CategoryRepository(IOptions<ConnectionConfig> connectionConfig)
        {
            ConnectionConfig = connectionConfig.Value;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionConfig.DefaultConnection);
        }

        public List<Category> GetCategories()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var categories = connection.GetAll<Category>().ToList();
                return categories;
            }
        }
    }
}
