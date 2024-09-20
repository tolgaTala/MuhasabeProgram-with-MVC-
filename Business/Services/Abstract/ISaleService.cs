using Entities.Dtos.Salers;
using Entities.Dtos.Sales;
using Entities.Entities;

namespace Business.Services.Abstract
{
    public interface ISaleService
    {
        Task CreateSaleAsnyc(SaleAddDto saleAddDto);
        Task<List<SaleDto>> GetAllSales();
        Task<SaleDto> GetSaleById(Guid id);
        Task<string> UpdateSaleAsync(SaleUpdateDto saleUpdateDto);
        Task<string> SafeDeleteSaleAsync(Guid saleId);
        Task<string> UndoDeleteSaleAsync(Guid saleId);
        Task<Sale> HardDeleteSaleAsync(Guid saleId);
    }
}
