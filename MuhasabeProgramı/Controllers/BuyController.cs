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
    public class BuyController : Controller
    {
        private readonly IBuyService buyService;
        private readonly ISalerService salerService;
        private readonly IMapper mapper;
        private readonly IValidator<Buy> validator;

        public BuyController(IBuyService buyService,ISalerService salerService, IMapper mapper, IValidator<Buy> validator)
        {
            this.buyService = buyService;
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
            return View(new BuyAddDto { ProductId = productId ,Salers = salers });
        }
        [HttpPost]
        public async Task<IActionResult> Add(BuyAddDto buyAddDto)
        {
            var map = mapper.Map<Buy>(buyAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await buyService.CreateBuyAsnyc(buyAddDto);
                //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("ProductDetail" , "Product", new { ProductId = buyAddDto.ProductId });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
        public async Task<IActionResult> Delete(Guid buyId)
        {
            var buy = await buyService.HardDeleteBuyAsync(buyId);
            //toast.AddSuccessToastMessage(Messages.saler.Delete(name), new ToastrOptions { Title = "İşlem Başarılı" });

            return RedirectToAction("ProductDetail", "Product", new { ProductId = buy.ProductId });
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid buyId)
        {
            var buy = await buyService.GetBuyById(buyId);
            var salers = await salerService.GetAllNonDeletedSaler();
            var map = mapper.Map<BuyUpdateDto>(buy);
            map.Salers = salers;
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BuyUpdateDto buyUpdateDto)
        {
            var map = mapper.Map<Buy>(buyUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await buyService.UpdateBuyAsync(buyUpdateDto);
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
