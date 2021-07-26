using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                //Se puede o no hacer con lo siguiente dependiendo lo especifico que quiera ser
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category.Name))
                .ForMember(p => p.TradeMarkName, x => x.MapFrom(a => a.TradeMark.Name));
        }
    }
}
