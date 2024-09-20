using Entities.Dtos.Buys;
using Entities.Dtos.Products;
using Entities.Dtos.Salers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.TopluBuys
{
    public class TopluBuyAddDto
    {
        public Guid SalerId { get; set; }
        public DateTime AlisTarihi { get; set; }
        public string EvrakNo { get; set; }
        public IList<SalerDto> Salers { get; set; }
        public IList<ProductDto> Products { get; set; }
        public IList<BuyAddDto> buyAddDtos { get; set; }
    }
}
