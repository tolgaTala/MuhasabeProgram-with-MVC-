using AutoMapper;
using Business.Extensions;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Entities.Dtos.Buys;
using Entities.Dtos.Salers;
using Entities.Dtos.Sales;
using Entities.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MuhasabeProgramı.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService saleService;
        private readonly ISalerService salerService;
        private readonly IMapper mapper;
        private readonly IValidator<Sale> validator;

        public SaleController(ISaleService saleService, ISalerService salerService, IMapper mapper, IValidator<Sale> validator)
        {
            this.saleService = saleService;
            this.salerService = salerService;
            this.mapper = mapper;
            this.validator = validator;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add(Guid productId)
        {
            var salers = await salerService.GetAllNonDeletedSaler();
            return View(new SaleAddDto { ProductId = productId, Salers = salers });
        }
        [HttpPost]
        public async Task<IActionResult> Add(SaleAddDto saleAddDto)
        {
            var map = mapper.Map<Sale>(saleAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await saleService.CreateSaleAsnyc(saleAddDto);
                //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("ProductDetail", "Product", new { ProductId = saleAddDto.ProductId });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
        public async Task<IActionResult> Delete(Guid saleId)
        {
            var sale = await saleService.HardDeleteSaleAsync(saleId);
            //toast.AddSuccessToastMessage(Messages.saler.Delete(name), new ToastrOptions { Title = "İşlem Başarılı" });

            return RedirectToAction("ProductDetail", "Product", new { ProductId = sale.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid saleId)
        {
            var sale = await saleService.GetSaleById(saleId);
            var salers = await salerService.GetAllNonDeletedSaler();
            var map = mapper.Map<SaleUpdateDto>(sale);
            map.Salers = salers;
            return View(map);
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(SaleUpdateDto saleUpdateDto)
        {
            var map = mapper.Map<Sale>(saleUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await saleService.UpdateSaleAsync(saleUpdateDto);
                //toast.AddSuccessToastMessage(Messages.saler.Update(name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("ProductDetail", "Product", new { ProductId = map.ProductId });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
    }
}
