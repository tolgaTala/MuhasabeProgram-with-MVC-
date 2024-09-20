using Entities.Dtos.Buys;
using Entities.Dtos.Salers;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ISalerService
    {
        Task CreateSalerAsnyc(SalerAddDto salerAddDto);
        Task<List<SalerDto>> GetAllNonDeletedSaler();
        Task<List<SalerDto>> GetAllDeletedSaler();
        Task<List<SalerBuyInfoDto>> SalerDetail(Guid salerId);
        Task<Saler> GetSalerById(Guid id);
        Task<string> UpdateSalerAsync(SalerUpdateDto salerUpdateDto);
        Task<string> SafeDeleteSalerAsync(Guid salerId);
        Task<string> UndoDeleteSalerAsync(Guid salerId);
    }
}
