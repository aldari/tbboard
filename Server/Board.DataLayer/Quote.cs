using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Board.DataLayer
{
    public class Quote
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public Category Category { get; set; }
    }
}
