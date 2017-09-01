using System;

namespace Board.Web.Models
{
    public class QuoteVm
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
