using Entities.Entities;

namespace Entities.Dtos.Sales
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SalerId { get; set; }
        public Saler Saler { get; set; }
        public DateTime SatisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
        public bool IsDeleted { get; set; }
    }
}
