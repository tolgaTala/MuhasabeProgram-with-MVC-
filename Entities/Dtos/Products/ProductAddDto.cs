using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Products
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public int EndOfYearStock { get; set; }
        public decimal EndOfYearPrice { get; set; }
    }
}
