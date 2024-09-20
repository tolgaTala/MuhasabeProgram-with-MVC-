using Entities.Dtos.Salers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Sales
{
    public class SaleAddDto
    {
        public Guid ProductId { get; set; }
        public Guid SalerId { get; set; }
        public DateTime SatisTarihi { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public IList<SalerDto> Salers { get; set; }
    }
}
