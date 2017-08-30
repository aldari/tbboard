using System.Collections.Generic;
using Board.DataLayer;

namespace DataLayer
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}