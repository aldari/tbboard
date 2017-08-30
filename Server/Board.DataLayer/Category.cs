using Dapper.Contrib.Extensions;
using System;

namespace Board.DataLayer
{
    [Table("Categories")]
    public class Category
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
