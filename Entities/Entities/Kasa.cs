using MuhasebeProgramı.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Entities
{
    public class Kasa : EntityBase
    {
        public int? BozukKasa { get; set; }
        public int? KagitKasa { get; set; }
        public int? UstKasa { get; set; }
        public int? AltKasa { get; set; }
        public int? KrediKart { get; set; }
        public int? Banka { get; set; }
        public int? BozukPara { get; set; }
        public int? ButunKasa { get; set; }
        public int ToplamKasa { get; set; }
        public int? MacSayisi { get;set; }
        public int? NakitGider { get;set; }
        public int? KrediGider { get;set; }
        public int? NakitAlim { get;set; }
        public int? KrediAlim { get;set; }
        public int? HariciHarcama { get; set; }
        public int Ciro { get; set; }
    }
}
