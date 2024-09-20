using AutoMapper;
using Business.Extensions;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Entities.Dtos.Products;
using Entities.Dtos.Salers;
using Entities.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MuhasabeProgramı.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IValidator<Product> validator;

        public ProductController(IProductService productService, IMapper mapper, IValidator<Product> validator)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllNonDeletedProducts();
            return View(products);
        }
        public async Task<IActionResult> DeletedProducts()
        {
            var products = await productService.GetAllDeletedProducts();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetail(Guid productId)
        {
            var product = await productService.GetProductById(productId);
            return View(product);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            var map = mapper.Map<Product>(productAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await productService.CreateProductAsnyc(productAddDto);
                //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Product");
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid productId)
        {
            var product = await productService.GetProductById(productId);
            var map = mapper.Map<ProductUpdateDto>(product);
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var map = mapper.Map<Product>(productUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await productService.UpdateProductAsync(productUpdateDto);
                //toast.AddSuccessToastMessage(Messages.saler.Add(salerAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Product");
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
    }
}
