using System;
using System.Collections.Generic;
using Board.DataLayer;

namespace DataLayer
{
    public interface IQuoteRepository
    {
        Quote GetById(Guid id);
        List<Quote> GetQuotes(string author, string categ_id);
        Quote Create(Quote quote);
        Quote Update(Quote quote);
        bool Delete(Guid id);
    }
}