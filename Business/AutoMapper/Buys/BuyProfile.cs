using AutoMapper;
using Entities.Dtos.Buys;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper.Buys
{
    public class BuyProfile : Profile
    {
        public BuyProfile()
        {
            CreateMap<BuyDto, Buy>().ReverseMap();
            CreateMap<BuyAddDto, Buy>().ReverseMap();
            CreateMap<BuyUpdateDto, Buy>().ReverseMap();
            CreateMap<BuyUpdateDto, BuyDto>().ReverseMap();
            CreateMap<SalerBuyInfoDto, Buy>().ReverseMap();
            //CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            //CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
            //CreateMap<ArticleAddDto, Article>().ReverseMap();
        }
    }
}
