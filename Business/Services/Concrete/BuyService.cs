using AutoMapper;
using Business.Services.Abstract;
using Entities.Dtos.Buys;
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
    public class BuyService : IBuyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BuyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateBuyAsnyc(BuyAddDto buyAddDto)
        {
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x=>x.Id == buyAddDto.ProductId, a=>a.Buys);
            var buy = mapper.Map<Buy>(buyAddDto);
            buy.Tutar = buy.Stok * buy.Fiyat;

            await unitOfWork.GetRepository<Buy>().AddAsync(buy);
            await unitOfWork.SaveAsync();

            decimal toplamAlisTutar = 0;
            int toplamAlisStok = 0;
            foreach (var item in product.Buys)
            {
                toplamAlisTutar += item.Stok * item.Fiyat;
                toplamAlisStok += item.Stok;
            }
            product.OrtalamaMaliyet = (product.EndOfYearTotal + toplamAlisTutar) / (product.EndOfYearStock + toplamAlisStok);
            product.KalanStock += buy.Stok;
            
            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<BuyDto>> GetAllBuys()
        {
            var buys = await unitOfWork.GetRepository<Buy>().GetAllAsync(x => !x.IsDeleted, x => x.Saler);
            var map = mapper.Map<List<BuyDto>>(buys);
            return map;
        }

        public async Task<BuyDto> GetBuyDtoById(Guid id)
        {
            var buy = await unitOfWork.GetRepository<Buy>().GetAsync(x=>x.Id == id);
            var map = mapper.Map<BuyDto>(buy);
            return map;
        }
        public async Task<Buy> GetBuyById(Guid id)
        {
            var buy = await unitOfWork.GetRepository<Buy>().GetAsync(x => x.Id == id);
            return buy;
        }

        public async Task<Buy> HardDeleteBuyAsync(Guid buyId)
        {
            var buy = await unitOfWork.GetRepository<Buy>().GetAsync(x => x.Id == buyId);
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == buy.ProductId, a => a.Buys);

            await unitOfWork.GetRepository<Buy>().DeleteAsync(buy);
            await unitOfWork.SaveAsync();

            decimal toplamAlisTutar = 0;
            int toplamAlisStok = 0;
            foreach (var item in product.Buys)
            {
                toplamAlisTutar += item.Stok * item.Fiyat;
                toplamAlisStok += item.Stok;
            }
            
            product.OrtalamaMaliyet = (product.EndOfYearTotal + toplamAlisTutar) / (product.EndOfYearStock + toplamAlisStok);
            product.KalanStock -= buy.Stok;

            await unitOfWork.GetRepository<Product>().UpdateAsync(product);            
            await unitOfWork.SaveAsync();

            return buy;
        }

        public Task<string> SafeDeleteBuyAsync(Guid buyId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UndoDeleteBuyAsync(Guid buyId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateBuyAsync(BuyUpdateDto buyUpdateDto)
        {
            var buy = await unitOfWork.GetRepository<Buy>().GetAsync(x => x.Id == buyUpdateDto.Id);
            var oldStock = buy.Stok;

            var map = mapper.Map(buyUpdateDto, buy);
            map.Tutar = map.Stok * map.Fiyat;

            await unitOfWork.GetRepository<Buy>().UpdateAsync(map);

            await unitOfWork.SaveAsync();

            
            var product = await unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == buyUpdateDto.ProductId, a => a.Buys);

            product.KalanStock -= oldStock;

            decimal toplamAlisTutar = 0;
            int toplamAlisStok = 0;
            foreach (var item in product.Buys)
            {
                toplamAlisTutar += item.Stok * item.Fiyat;
                toplamAlisStok += item.Stok;
            }

            product.OrtalamaMaliyet = (product.EndOfYearTotal + toplamAlisTutar) / (product.EndOfYearStock + toplamAlisStok);

            product.KalanStock += map.Stok;

            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();

            return map.AlisTarihi.ToString();
        }

       
    }
}
