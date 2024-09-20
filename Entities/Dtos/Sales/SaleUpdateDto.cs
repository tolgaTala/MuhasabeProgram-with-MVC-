using Entities.Dtos.Salers;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.Sales
{
    public class SaleUpdateDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SalerId { get; set; }
        public DateTime SatisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public IList<SalerDto> Salers { get; set; }
    }
}
