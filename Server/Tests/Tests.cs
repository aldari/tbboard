using Board.DataLayer;
using Dapper;
using Dapper.Contrib.Extensions;
using DataLayer;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private ConnectionConfig _conectionConfig;

        private SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuoteList;Integrated Security=True; MultipleActiveResultSets=True");
        }

        //[Test]
        //public void Test()
        //{
        //    var repo = new QuoteRepository();
        //    var quotes = repo.GetQuotes("", "");
        //    //quotes.ForEach(x => Console.WriteLine($"{x.Id} {x.Author} {x.Category.Title} {x.Created} {x.Text} "));
        //}

        //[Test]
        //public void Test2()
        //{
        //    var repo = new QuoteRepository();
        //    var x = repo.GetById(Guid.Parse("3E6DAE45-8A16-4A1A-9D7B-6AAADFB7562F"));
        //    Console.WriteLine($"{x.Id} {x.Author} {x.Category.Title} {x.Created} {x.Text} ");
        //}

        [Test]
        public void TestGetAllCategories()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var quotes = connection.GetAll<Category>().ToList();
                quotes.ForEach(x => Console.WriteLine($"{x.Id} {x.Title}"));
            }
        }

        [Test]
        public void TestInsert()
        {
            var quote = new Quote
            {
                Id = Guid.NewGuid(),
                Author = "New Author",
                Category = new Category { Id = Guid.Parse("AE70513F-2AB7-446E-B990-5CB3463A8A24"), Title = "Category2" },
                Created = DateTime.UtcNow,
                Text = "Text For Quote"
            };
            using (var connection = GetConnection())
            {
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
            }
        }

        [Test]
        public void TestUpdate()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var quote = connection.Get<Quote>("3E6DAE45-8A16-4A1A-9D7B-6AAADFB7562F");
                quote.Author = "New Author 2";

                var isSuccess = connection.Execute(@"update quotes set author = @author, categoryid=@categoryid, created=@created, text=@text where id=@id",
                    new { Id = quote.Id, Author = quote.Author, CategoryId = Guid.Parse("AE70513F-2AB7-446E-B990-5CB3463A8A24"), Created = DateTime.UtcNow, Text = "TextText2" });
            }
        }

        [Test]
        public void TestDelete()
        {
            using (var connection = GetConnection())
            {
                //connection.Open();
                var isSuccess = connection.Delete(new Quote { Id = Guid.Parse("5BCB663A-16ED-44EB-9733-8B4CC7C6B996") });
            }
        }
    }
}
