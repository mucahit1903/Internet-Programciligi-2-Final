using İntProg.Models;
using İntProg.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace İntProg.Auth
{
    public class uyeService
    {
        DB00Entities1 db = new DB00Entities1();
         public UyeModel UyeOturumAc(string kadi, string parola)
        {
            UyeModel uye = db.Uyeler.Where(s => s.uyeKullaniciAdi == kadi && s.uyeSifre == parola).Select(x => new UyeModel() 
            {
                uyeId = x.uyeId,
                uyeAdi = x.uyeAdi,
                uyeSoyadi = x.uyeSoyadi,
                uyeKullaniciAdi = x.uyeKullaniciAdi,
                uyeSifre = x.uyeSifre,
                uyeTel = x.uyeTel,
                uyeMail = x.uyeMail,
                uyeAdres = x.uyeAdres,
                uyeYetki = x.uyeYetki,

            }).SingleOrDefault();
            
            return uye;
        }

    }
}