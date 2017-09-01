using System;
using AutoMapper;
using Board.DataLayer;
using Board.Web.Models;

namespace Board.Web
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {
            CreateMap<Quote, QuoteVm>();
            CreateMap<QuoteVm, Quote>()
                .ForMember(m => m.Category, o => o.MapFrom(m => new Category
                {
                    Id = Guid.Parse(m.CategoryId),
                    Title = m.CategoryTitle
                }));
        }
    }
}
