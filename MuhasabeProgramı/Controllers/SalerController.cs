using AutoMapper;
using Business.Extensions;
using Business.Services.Abstract;
using Entities.Dtos.Salers;
using Entities.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MuhasabeProgramı.Controllers
{
    public class SalerController : Controller
    {
        private readonly ISalerService salerService;
        private readonly IMapper mapper;
        private readonly IValidator<Saler> validator;

        public SalerController(ISalerService salerService, IMapper mapper, IValidator<Saler> validator)
        {
            this.salerService = salerService;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var saler = await salerService.GetAllNonDeletedSaler();
            return View(saler);
        }
        public async Task<IActionResult> DeletedSalers()
        {
            var saler = await salerService.GetAllDeletedSaler();
            return View(saler);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SalerAddDto salerAddDto)
        {
            var map = mapper.Map<Saler>(salerAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await salerService.CreateSalerAsnyc(salerAddDto);
                //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Saler");
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] SalerAddDto salerAddDto)
        {
            var map = mapper.Map<Saler>(salerAddDto);
            var result = await validator.ValidateAsync(map);
            if (result.IsValid)
            {
                await salerService.CreateSalerAsnyc(salerAddDto);
                //toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return Json("Yeni Satıcı Eklendi");
            }
            else
            {
                //toast.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "İşlem Başarısız" });
                return Json(result.Errors.First().ErrorMessage);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid salerId)
        {
            var saler = await salerService.GetSalerById(salerId);
            var map = mapper.Map<Saler, SalerUpdateDto>(saler);
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SalerUpdateDto salerUpdateDto)
        {
            var map = mapper.Map<Saler>(salerUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await salerService.UpdateSalerAsync(salerUpdateDto);
                //toast.AddSuccessToastMessage(Messages.saler.Update(name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Saler");
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
        public async Task<IActionResult> Delete(Guid salerId)
        {
            var name = await salerService.SafeDeleteSalerAsync(salerId);
            //toast.AddSuccessToastMessage(Messages.saler.Delete(name), new ToastrOptions { Title = "İşlem Başarılı" });

            return RedirectToAction("Index", "Saler");
        }
        public async Task<IActionResult> UndoDelete(Guid salerId)
        {
            var name = await salerService.UndoDeleteSalerAsync(salerId);
            //toast.AddSuccessToastMessage(Messages.saler.UndoDelete(name), new ToastrOptions { Title = "İşlem Başarılı" });

            return RedirectToAction("Index", "Saler");
        }
        public async Task<IActionResult> SalerBuyInfoDetail(Guid salerId)
        {
            var salerInfoDetails = await salerService.SalerDetail(salerId);
            return View(salerInfoDetails);
        }
    }
}
