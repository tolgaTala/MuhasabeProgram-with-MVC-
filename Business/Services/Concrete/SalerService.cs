using AutoMapper;
using Business.Services.Abstract;
using Entities.Dtos.Buys;
using Entities.Dtos.Salers;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MuhasebeProgramı.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class SalerService : ISalerService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public SalerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateSalerAsnyc(SalerAddDto salerAddDto)
        {
            var saler = mapper.Map<Saler>(salerAddDto);                
            await unitOfWork.GetRepository<Saler>().AddAsync(saler);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<SalerDto>> GetAllNonDeletedSaler()
        {
            var salers = await unitOfWork.GetRepository<Saler>().GetAllAsync(x=>!x.IsDeleted);

            var map = mapper.Map<List<SalerDto>>(salers);
            return map;
        }
        public async Task<List<SalerDto>> GetAllDeletedSaler()
        {
            var salers = await unitOfWork.GetRepository<Saler>().GetAllAsync(x=>x.IsDeleted);

            var map = mapper.Map<List<SalerDto>>(salers);
            return map;
        }


        public async Task<Saler> GetSalerById(Guid id)
        {
            var saler = await unitOfWork.GetRepository<Saler>().GetByGuidAsync(id);
            return saler;
        }

        public async Task<string> SafeDeleteSalerAsync(Guid salerId)
        {
            var saler = await unitOfWork.GetRepository<Saler>().GetByGuidAsync(salerId);

            saler.IsDeleted = true;
            saler.DeletedDate = DateTime.Now;

            await unitOfWork.GetRepository<Saler>().UpdateAsync(saler);
            await unitOfWork.SaveAsync();

            return saler.Name;
        }

        public async Task<List<SalerBuyInfoDto>> SalerDetail(Guid salerId)
        {
            var salerBuyInfoDetails = await unitOfWork.GetRepository<Buy>().GetAllAsync(x=>x.SalerId == salerId, a=>a.Product, b=>b.Saler);
            var map = mapper.Map<List<SalerBuyInfoDto>>(salerBuyInfoDetails);

            return map;

        }

        public async Task<string> UndoDeleteSalerAsync(Guid salerId)
        {
            var saler = await unitOfWork.GetRepository<Saler>().GetByGuidAsync(salerId);

            saler.IsDeleted = false;
            saler.DeletedDate = null;
            saler.DeletedBy = null;

            await unitOfWork.GetRepository<Saler>().UpdateAsync(saler);
            await unitOfWork.SaveAsync();

            return saler.Name;
        }

        public async Task<string> UpdateSalerAsync(SalerUpdateDto salerUpdateDto)
        {
            var salerr = mapper.Map<Saler>(salerUpdateDto);
            await unitOfWork.GetRepository<Saler>().UpdateAsync(salerr);

            await unitOfWork.SaveAsync();

            return salerr.Name;
        }
    }
}
