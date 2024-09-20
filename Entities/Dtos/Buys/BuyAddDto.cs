using Entities.Dtos.Salers;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Buys
{
    public class BuyAddDto
    {
        public Guid ProductId { get; set; }
        public Guid SalerId { get; set; }
        public DateTime AlisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public int Stok { get; set; }
        public decimal Fiyat { get; set; }
        public IList<SalerDto> Salers { get; set; }
    }
}
