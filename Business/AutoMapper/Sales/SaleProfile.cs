using AutoMapper;
using Entities.Dtos.Sales;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper.Sales
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleDto, Sale>().ReverseMap();
            CreateMap<SaleAddDto, Sale>().ReverseMap();
            CreateMap<SaleUpdateDto, Sale>().ReverseMap();
            CreateMap<SaleUpdateDto, SaleDto>().ReverseMap();
            //CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            //CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
            //CreateMap<ArticleAddDto, Article>().ReverseMap();
        }
    }
}
