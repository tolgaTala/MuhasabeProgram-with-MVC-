using AutoMapper;
using Business.Extensions;
using Business.Services.Abstract;
using Entities.Dtos.Buys;
using Entities.Dtos.TopluBuys;
using Entities.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MuhasabeProgramı.Controllers
{
    public class TopluBuyController : Controller
    {
        private readonly IBuyService buyService;
        private readonly ISalerService salerService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IValidator<Buy> validator;

        public TopluBuyController(IBuyService buyService, ISalerService salerService, IProductService productService, IMapper mapper, IValidator<Buy> validator)
        {
            this.buyService = buyService;
            this.salerService = salerService;
            this.productService = productService;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllNonDeletedProducts();
            var salers = await salerService.GetAllNonDeletedSaler();

            TopluBuyAddDto topluBuyAddDto = new TopluBuyAddDto();
            topluBuyAddDto.Products = products;
            topluBuyAddDto.Salers = salers;
            //BuyAddDto buyAddDto = new BuyAddDto();
            //buyAddDto.EvrakNo = "0";

            //topluBuyAddDto.buyAddDtos.Add(buyAddDto);
            return View(topluBuyAddDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add(TopluBuyAddDto topluBuyAddDto)
        {
            foreach (var buyAddDto in topluBuyAddDto.buyAddDtos)
            {
                if (buyAddDto.Stok == 0 && buyAddDto.Fiyat == 0)
                {
                    continue;
                }
                buyAddDto.AlisTarihi = topluBuyAddDto.AlisTarihi;
                buyAddDto.EvrakNo = topluBuyAddDto.EvrakNo;
                buyAddDto.SalerId = topluBuyAddDto.SalerId;

                var map = mapper.Map<Buy>(buyAddDto);
                var result = await validator.ValidateAsync(map);

                if (result.IsValid)
                {
                    await buyService.CreateBuyAsnyc(buyAddDto);
                    //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                   
                }
                else
                {
                    result.AddToModelState(this.ModelState);
                   
                }
            }

            return RedirectToAction("SalerBuyInfoDetail", "Saler", new { salerId = topluBuyAddDto.SalerId });



        }
    }
}
