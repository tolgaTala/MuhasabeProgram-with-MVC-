using Entities.Dtos.Buys;
using Entities.Dtos.Salers;
using Entities.Dtos.Sales;

namespace Entities.Dtos.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EndOfYearStock { get; set; }
        public decimal EndOfYearPrice { get; set; }
        public decimal EndOfYearTotal { get; set; }
        public decimal OrtalamaMaliyet { get; set; }
        public int KalanStock { get; set; }
        public decimal KarOran { get; set; }
        public decimal KarFiyat { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public IList<BuyDto> Buys { get; set; }
        public IList<SaleDto> Sales { get; set; }
    }
}
