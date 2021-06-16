using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace İntProg.ViewModel
{
    public class UrunModel
    {
        public int urunId { get; set; }
        public string urunKodu { get; set; }
        public string urunAdi { get; set; }
        public int urunStok { get; set; }
        public decimal urunFiyatSat { get; set; }
        public decimal urunFiyatAl { get; set; }
        public string urunAciklama { get; set; }
        public string urunGorsel { get; set; }
        public int urunKatId { get; set; }
        public string urunKatAdi { get; set; }


    }
}