using Entities.Dtos.Buys;
using Entities.Dtos.Products;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IBuyService
    {
        Task CreateBuyAsnyc(BuyAddDto buyAddDto);
        Task<List<BuyDto>> GetAllBuys();
        Task<BuyDto> GetBuyDtoById(Guid id);
        Task<Buy> GetBuyById(Guid id);
        Task<string> UpdateBuyAsync(BuyUpdateDto buyUpdateDto);
        Task<string> SafeDeleteBuyAsync(Guid buyId);
        Task<string> UndoDeleteBuyAsync(Guid buyId);
        Task<Buy> HardDeleteBuyAsync(Guid buyId);
    }
}
