using AutoMapper;
using Business.Services.Abstract;
using Entities.Dtos.Buys;
using Entities.Dtos.Salers;
using Entities.Dtos.Sales;
using Entities.Entities;
using MuhasebeProgramı.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateSaleAsnyc(SaleAddDto saleAddDto)
        {
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == saleAddDto.ProductId, a=>a.Sales);
            var sale = mapper.Map<Sale>(saleAddDto);
            sale.Tutar = sale.Stok * sale.Fiyat;

            sale.EvrakNo = (Convert.ToInt32((product.Sales.Last().EvrakNo)) + 1).ToString();

            await unitOfWork.GetRepository<Sale>().AddAsync(sale);
            await unitOfWork.SaveAsync();

            decimal toplamSatisTutar = 0;
            int toplamSatisStok = 0;
            foreach (var item in product.Sales)
            {
                toplamSatisTutar += item.Stok * item.Fiyat;
                toplamSatisStok += item.Stok;
            }

            product.KarOran = (toplamSatisTutar / toplamSatisStok) / product.OrtalamaMaliyet ;
            product.KarFiyat = (toplamSatisTutar - (toplamSatisStok * product.OrtalamaMaliyet));
            product.KalanStock -= sale.Stok;

            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<SaleDto>> GetAllSales()
        {
            var sales = await unitOfWork.GetRepository<Sale>().GetAllAsync();
            var map = mapper.Map<List<SaleDto>>(sales);
            return map;
        }

        public async Task<SaleDto> GetSaleById(Guid id)
        {
            var sale = await unitOfWork.GetRepository<Sale>().GetAsync(x=>x.Id == id);
            var map = mapper.Map<SaleDto>(sale);
            return map;
        }

        public async Task<Sale> HardDeleteSaleAsync(Guid saleId)
        {
            var sale = await unitOfWork.GetRepository<Sale>().GetAsync(x => x.Id == saleId);
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == sale.ProductId, a=>a.Sales);

            await unitOfWork.GetRepository<Sale>().DeleteAsync(sale);
            await unitOfWork.SaveAsync();

            decimal toplamSatisTutar = 0;
            int toplamSatisStok = 0;
            foreach (var item in product.Sales)
            {
                toplamSatisTutar += item.Stok * item.Fiyat;
                toplamSatisStok += item.Stok;
            }

            
            if (product.Sales.Count > 1)
            {
                product.KarOran = (toplamSatisTutar / toplamSatisStok) / product.OrtalamaMaliyet;
                product.KarFiyat = (toplamSatisTutar - (toplamSatisStok * product.OrtalamaMaliyet));
            }                
            else
            {
                product.KarFiyat = 0;
                product.KarOran = 0;
            }
            product.KalanStock += sale.Stok; 
            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();

            return sale;
        }

        public Task<string> SafeDeleteSaleAsync(Guid saleId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UndoDeleteSaleAsync(Guid saleId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateSaleAsync(SaleUpdateDto saleUpdateDto)
        {
            var sale = await unitOfWork.GetRepository<Sale>().GetAsync(x => x.Id == saleUpdateDto.Id);
            var oldStock = sale.Stok;

            var map = mapper.Map(saleUpdateDto, sale);
            map.Tutar = map.Stok * map.Fiyat;

            await unitOfWork.GetRepository<Sale>().UpdateAsync(map);

            await unitOfWork.SaveAsync();

            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == saleUpdateDto.ProductId, a => a.Sales);

            product.KalanStock += oldStock;

            decimal toplamSatisTutar = 0;
            int toplamSatisStok = 0;
            foreach (var item in product.Sales)
            {
                toplamSatisTutar += item.Stok * item.Fiyat;
                toplamSatisStok += item.Stok;
            }

            product.KarOran = (toplamSatisTutar / toplamSatisStok) / product.OrtalamaMaliyet;
            product.KarFiyat = (toplamSatisTutar - (toplamSatisStok * product.OrtalamaMaliyet));
            product.KalanStock -= map.Stok;

            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();

            return sale.SatisTarihi.ToString();
        }
    }
}
