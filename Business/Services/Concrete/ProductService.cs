using AutoMapper;
using Business.Services.Abstract;
using Entities.Dtos.Products;
using Entities.Entities;
using MuhasebeProgramı.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateProductAsnyc(ProductAddDto productAddDto)
        {
            var product = mapper.Map<Product>(productAddDto);
            product.EndOfYearTotal = product.EndOfYearStock * product.EndOfYearPrice;
            product.OrtalamaMaliyet = product.EndOfYearTotal / product.EndOfYearStock;
            product.KalanStock = product.EndOfYearStock;
            product.KarOran = 0;
            product.KarFiyat = 0;

            await unitOfWork.GetRepository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync();   

        }

        public async Task<List<ProductDto>> GetAllNonDeletedProducts()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(x=>!x.IsDeleted);
            var map = mapper.Map<List<ProductDto>>(products);
            return map;
        }
        public async Task<List<ProductDto>> GetAllDeletedProducts()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(x=>x.IsDeleted);
            var map = mapper.Map<List<ProductDto>>(products);
            return map;
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x=>x.Id == id ,a => a.Buys, b => b.Sales);
            foreach (var item in product.Buys)
            {
                var saler = await unitOfWork.GetRepository<Saler>().GetAsync(x => x.Id == item.SalerId);
                item.Saler = saler;
            }
            foreach (var item in product.Sales)
            {
                var saler = await unitOfWork.GetRepository<Saler>().GetAsync(x => x.Id == item.SalerId);
                item.Saler = saler;
            }
            var map = mapper.Map<ProductDto>(product);
            return map;
        }

        public Task<string> SafeDeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UndoDeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == productUpdateDto.Id);
            productUpdateDto.EndOfYearTotal = productUpdateDto.EndOfYearStock * productUpdateDto.EndOfYearPrice;
            product.KalanStock -= product.EndOfYearStock;
            var map = mapper.Map(productUpdateDto, product);
            map.KalanStock += productUpdateDto.EndOfYearStock;
            await unitOfWork.GetRepository<Product>().UpdateAsync(map);
            await unitOfWork.SaveAsync();
            return map.Name;
        }
    }
}
