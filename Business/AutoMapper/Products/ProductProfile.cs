using AutoMapper;
using Entities.Dtos.Products;
using Entities.Dtos.Salers;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, ProductDto>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            //CreateMap<SalerUpdateDto, Saler>().ReverseMap();
            //CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            //CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
            //CreateMap<ArticleAddDto, Article>().ReverseMap();
        }
    }
}
