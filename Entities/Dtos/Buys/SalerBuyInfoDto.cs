using Entities.Entities;

namespace Entities.Dtos.Buys
{
    public class SalerBuyInfoDto
    {
        public Guid Id { get; set; }
        public DateTime AlisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SalerId { get; set; }
        public Saler Saler { get; set; }

    }
}
