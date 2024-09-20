using MuhasebeProgramı.Core.Entities;

namespace Entities.Entities
{
    public class Sale : EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SalerId { get; set; }
        public Saler Saler { get; set; }
        public DateTime SatisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
    }

}
