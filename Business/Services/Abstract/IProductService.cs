using Entities.Dtos.Products;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IProductService
    {
        Task CreateProductAsnyc(ProductAddDto productAddDto);
        Task<List<ProductDto>> GetAllNonDeletedProducts();
        Task<List<ProductDto>> GetAllDeletedProducts();
        Task<ProductDto> GetProductById(Guid id);
        Task<string> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<string> SafeDeleteProductAsync(Guid productId);
        Task<string> UndoDeleteProductAsync(Guid productId);
    }
}
