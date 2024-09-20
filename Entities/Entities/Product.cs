using MuhasebeProgramı.Core.Entities;

namespace Entities.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public int EndOfYearStock { get; set; }
        public decimal EndOfYearPrice { get; set; }
        public decimal EndOfYearTotal { get; set; }
        public decimal OrtalamaMaliyet { get; set; }
        public int KalanStock { get; set; }
        public decimal KarOran { get; set; }
        public decimal KarFiyat { get; set; }
        public ICollection<Buy> Buys { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }

}
