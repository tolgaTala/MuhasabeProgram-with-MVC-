using AutoMapper;
using Entities.Dtos.Salers;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper.Salers
{
    public class SalerProfile : Profile
    {
        public SalerProfile()
        {
            CreateMap<SalerAddDto, Saler>().ReverseMap();
            CreateMap<SalerDto, Saler>().ReverseMap();
            CreateMap<SalerUpdateDto, Saler>().ReverseMap();
            //CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            //CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
            //CreateMap<ArticleAddDto, Article>().ReverseMap();
        }
    }
}
