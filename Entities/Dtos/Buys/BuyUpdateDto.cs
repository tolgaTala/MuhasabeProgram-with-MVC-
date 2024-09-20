using Entities.Dtos.Salers;

namespace Entities.Dtos.Buys
{
    public class BuyUpdateDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SalerId { get; set; }
        public DateTime AlisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public IList<SalerDto> Salers { get; set; }
    }
}
