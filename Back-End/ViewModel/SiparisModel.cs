using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace İntProg.ViewModel
{
    public class SiparisModel
    {

        public int siparisId { get; set; }
        public int siparisKodu { get; set; }
        public int siparisUyeId { get; set; }
        public string siparisTarih { get; set; }
        public int siparisUrunId { get; set; }
        public string siparisAciklama { get; set; }
        public string siparisAdres { get; set; }
        public string siparisurunAdi { get; set; }

        public string siparisuyeAdi { get; set; }

        public decimal siparisurunFiyatSat { get; set; }

    }
}