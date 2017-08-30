using Board.DataLayer;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository()
        {

        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuoteList; Integrated Security = True; MultipleActiveResultSets = True");
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
