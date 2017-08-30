using Board.DataLayer;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class QuoteRepository : IQuoteRepository
    {
        public QuoteRepository()
        {

        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuoteList; Integrated Security = True; MultipleActiveResultSets = True");
        }

        public List<Quote> GetQuotes()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var quotes = connection.Query<Quote, Category, Quote>(@"
                    SELECT q.Id, q.Author, q.Created, q.Text, c.Id, c.Title
                    FROM Quotes q
                    INNER JOIN Categories c ON c.Id = q.CategoryId
                    ", (quote, category) => {
                    quote.Category = category;
                    return quote;
                },
                    commandType: CommandType.Text,
                     splitOn: "Id"
                     ).ToList();

                return quotes;
            }
        }

        public Quote GetById(Guid id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Query<Quote, Category, Quote>(@"
                    SELECT q.Id, q.Author, q.Created, q.Text, c.Id, c.Title
                    FROM Quotes q
                    INNER JOIN Categories c ON c.Id = q.CategoryId
                    WHERE q.id=@id",
                    (quote, category) => {
                        quote.Category = category;
                        return quote;
                    },
                    commandType: CommandType.Text,
                    splitOn: "Id",
                    param: new { Id = id }
                ).SingleOrDefault();
            }
        }

        public Quote Create(Quote quote)
        {
            using (var connection = GetConnection())
            {
                quote.Id = Guid.NewGuid();  
                quote.Created = DateTime.UtcNow; // injecting time ?!

                connection.Open();
                string query = @"Insert into quotes values (@Id, @Author, @CategoryId, @Created, @Text)";
                connection.Execute(query, new
                {
                    Id = quote.Id,
                    Author = quote.Author,
                    CategoryId = quote.Category.Id,
                    Created = quote.Created,
                    Text = quote.Text
                });
                return quote;
            }
        }

        public Quote Update(Quote quote)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var item = connection.Get<Quote>(quote.Id);
                if (item == null)
                    throw new ArgumentException();

                var isSuccess = connection.Execute(@"update quotes set author = @author, categoryid=@categoryid, created=@created, text=@text where id=@id ",
                    new {
                        Id = quote.Id,
                        Author = quote.Author,
                        CategoryId = quote.Category.Id,
                        Created = DateTime.UtcNow,
                        Text = quote.Text
                    });
                return item;
            }
        }

        public bool Delete(Guid id)
        {
            using (var connection = GetConnection())
            {
                var affectedrows = connection.Execute("delete from quotes where id = @Id", new { Id = id });
                return affectedrows > 0;
            }
        }
    }
}
