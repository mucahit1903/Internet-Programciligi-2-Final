using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;
using İntProg.Models;
using İntProg.ViewModel;

namespace İntProg.Controllers
{

    

    public class ServisController : ApiController
    {
        DB00Entities1 db = new DB00Entities1();
        SonucModel sonuc = new SonucModel();

        #region Kategori

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {

                katId = x.katId,
                katAdi = x.katAdi,
                katUrunSay = x.Urun.Count()

            }
            ).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katId == katId).Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
                katUrunSay = x.Urun.Count()
            }).FirstOrDefault();
            return kayit;
        }
        [Authorize]
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır.";
                return sonuc;
            }

            Kategori yeni = new Kategori();
            yeni.katAdi = model.katAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Başarıyla Eklendi";
            return sonuc;
        }
        [Authorize]
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == model.katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Kategori Yok";
                return sonuc;
            }

            kayit.katAdi = model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Başarıyla Düzenlendi";
            return sonuc;
        }
        [Authorize]
        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Kategori Yok";
                return sonuc;
            }

            if (db.Urun.Count(s => s.urunKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Kategorinin İçerisinde Ürünler Var Kategori Silinemedi";
                return sonuc;
            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }

        #endregion


        #region Urun
        [HttpGet]
        [Route("api/urunliste")]
        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {

                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunKatId = x.urunKatId,
                urunFiyatSat = x.urunFiyatSat,
                urunFiyatAl = x.urunFiyatAl,
                urunKodu = x.urunKodu,
                urunStok = x.urunStok,
                urunAciklama = x.urunAciklama,
                urunGorsel = x.urunGorsel,
                urunKatAdi = x.Kategori.katAdi



            }
            ).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/urunlistebykatid/{katId}")]
        public List<UrunModel> UrunListeByKatId(int katId)
        {
            List<UrunModel> liste = db.Urun.Where(s=>s.urunKatId==katId).Select(x => new UrunModel()
            {

                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunKatId = x.urunKatId,
                urunFiyatSat = x.urunFiyatSat,
                urunFiyatAl = x.urunFiyatAl,
                urunKodu = x.urunKodu,
                urunStok = x.urunStok,
                urunAciklama = x.urunAciklama,
                urunGorsel = x.urunGorsel,
                urunKatAdi = x.Kategori.katAdi



            }
            ).ToList();

            return liste;
        }


        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunModel UrunById(int urunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.urunId == urunId).Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunKatId = x.urunKatId,
                urunFiyatSat = x.urunFiyatSat,
                urunFiyatAl = x.urunFiyatAl,
                urunKodu = x.urunKodu,
                urunStok = x.urunStok,
                urunAciklama = x.urunAciklama,
                urunGorsel = x.urunGorsel,
                urunKatAdi = x.Kategori.katAdi


            }).FirstOrDefault();
            return kayit;
        }

        [Authorize]
        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunModel model)
        {
            if (db.Urun.Count(s => s.urunKodu == model.urunKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ürün Kayıtlıdır.";
                return sonuc;
            }

            Urun yeni = new Urun();
            yeni.urunKodu = model.urunKodu;
            yeni.urunAdi = model.urunAdi;
            yeni.urunStok = model.urunStok;
            yeni.urunFiyatSat = model.urunFiyatSat;
            yeni.urunFiyatAl = model.urunFiyatAl;
            yeni.urunAciklama = model.urunAciklama;
            yeni.urunGorsel = "stokfoto.png";
            yeni.urunKatId = model.urunKatId;
           

            db.Urun.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Başarıyla Eklendi";
            return sonuc;
        }
        [Authorize]
        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == model.urunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }
            

            kayit.urunAdi = model.urunAdi;
            kayit.urunStok = model.urunStok;
            kayit.urunFiyatSat = model.urunFiyatSat;
            kayit.urunFiyatAl = model.urunFiyatAl;
            kayit.urunAciklama = model.urunAciklama;
            kayit.urunGorsel = model.urunGorsel;
            kayit.urunKatId = model.urunKatId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Başarıyla Düzenlendi";
            return sonuc;
        }

        [Authorize]
        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SonucModel UrunSil(int urunId)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == urunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }

            db.Urun.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi";
            return sonuc;
        }
        [Authorize]
        [HttpPost]
        [Route("api/urungorselguncelle")]
        public SonucModel UrunGorselGuncelle(UrunGorselModel model)
        {
            Urun urn = db.Urun.Where(s=>s.urunKodu==model.urunKodu).SingleOrDefault();
            if (urn==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }

            if (urn.urunGorsel != "stokfoto.png")
            {
                string yol=System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + urn.urunGorsel);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.gorselData;
            string base64 = data.Substring(data.IndexOf(',')+1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = urn.urunKodu + model.gorselUzanti.Replace("image/",".");

            using (var ms=new MemoryStream(imgbytes,0,imgbytes.Length))
            {

                Image img = Image.FromStream(ms,true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));
                
            }

            urn.urunGorsel = dosyaAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Görsel Güncellendi";
            
            
            return sonuc;
        }


        #endregion


        #region Uye
        [Authorize]
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uyeler.Select(x => new UyeModel()
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

            }
            ).ToList();

            return liste;
        }
        [Authorize]
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uyeler.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
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
            }).FirstOrDefault();
            return kayit;
        }

        
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uyeler.Count(s => s.uyeKullaniciAdi == model.uyeKullaniciAdi || s.uyeMail == model.uyeMail) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Mail  Kayıtlıdır.";
                return sonuc;
            }

            Uyeler yeni = new Uyeler();

            yeni.uyeAdi = model.uyeAdi;
            yeni.uyeSoyadi = model.uyeSoyadi;
            yeni.uyeKullaniciAdi = model.uyeKullaniciAdi;
            yeni.uyeSifre = model.uyeSifre;
            yeni.uyeTel = model.uyeTel;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeAdres = model.uyeAdres;
            

            db.Uyeler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Başarıyla Eklendi";
            return sonuc;
        }
        [Authorize]
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uyeler kayit = db.Uyeler.Where(s => s.uyeId == model.uyeId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Üye Yok";
                return sonuc;
            }


            //if (db.Uyeler.Count(s => s.uyeKullaniciAdi == model.uyeKullaniciAdi) > 0)
            //{
            //    sonuc.islem = false;
            //    sonuc.mesaj = "Girilen Kullanıcı Adı Kayıtlıdır.";
            //    return sonuc;
            //}
            //else
            //{
            //    kayit.uyeKullaniciAdi = model.uyeKullaniciAdi
            //}

            kayit.uyeAdi = model.uyeAdi;
            kayit.uyeSoyadi = model.uyeSoyadi;
            kayit.uyeKullaniciAdi = model.uyeKullaniciAdi;
            kayit.uyeSifre = model.uyeSifre;
            kayit.uyeTel = model.uyeTel;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeAdres = model.uyeAdres;
            kayit.uyeYetki = model.uyeYetki;
            




            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Başarıyla Düzenlendi";
            return sonuc;
        }

        [Authorize]
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uyeler kayit = db.Uyeler.Where(s => s.uyeId == uyeId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Üye Yok";
                return sonuc;
            }


            db.Uyeler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }

        #endregion


        #region Siparis

        [HttpGet]
        [Route("api/siparisliste")]
        public List<SiparisModel> SiparisListe()
        {
            List<SiparisModel> liste = db.Siparisler.Select(x => new SiparisModel()
            {

                siparisId = x.siparisId,
                siparisKodu = x.siparisKodu,
                siparisUyeId = x.siparisUyeId,
                siparisuyeAdi = x.Uyeler.uyeAdi,
                siparisAdres = x.Uyeler.uyeAdres,
                siparisTarih = x.siparisTarih,
                siparisUrunId = x.siparisUrunId,
                siparisurunAdi = x.Urun.urunAdi,
                siparisurunFiyatSat = x.Urun.urunFiyatSat,
                siparisAciklama = x.Urun.urunAciklama,


            }
            ).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/siparisbyid/{siparisId}")]
        public SiparisModel SiparisById(int siparisId)
        {
            SiparisModel kayit = db.Siparisler.Where(s => s.siparisId == siparisId).Select(x => new SiparisModel()
            {
                siparisId = x.siparisId,
                siparisKodu = x.siparisKodu,
                siparisUyeId = x.siparisUyeId,
                siparisuyeAdi = x.Uyeler.uyeAdi,
                siparisAdres = x.Uyeler.uyeAdres,
                siparisTarih = x.siparisTarih,
                siparisUrunId = x.siparisUrunId,
                siparisurunAdi = x.Urun.urunAdi,
                siparisurunFiyatSat = x.Urun.urunFiyatSat,
                siparisAciklama = x.Urun.urunAciklama,

            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/siparisEkle")]
        public SonucModel SiparisEkle(SiparisModel model)
        {
            if (db.Siparisler.Count(s => s.siparisKodu == model.siparisKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Sipariş Kayıtlıdır.";
                return sonuc;
            }

            Siparisler yeni = new Siparisler();


            yeni.siparisUyeId = model.siparisUyeId;
            yeni.siparisTarih = model.siparisTarih;
            yeni.siparisKodu = model.siparisKodu;
            yeni.siparisUrunId = model.siparisUrunId;




            db.Siparisler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Başarıyla Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/siparisduzenle")]
        public SonucModel SiparisDuzenle(SiparisModel model)
        {
            Siparisler kayit = db.Siparisler.Where(s => s.siparisId == model.siparisId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Sipariş Yok";
                return sonuc;
            }

            kayit.siparisUrunId = model.siparisUrunId;
            kayit.siparisUyeId = model.siparisUyeId;
            


            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Başarıyla Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/siparissil/{siparisId}")]
        public SonucModel SiparisSil(int siparisId)
        {
            Siparisler kayit = db.Siparisler.Where(s => s.siparisId == siparisId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Sipariş Yok";
                return sonuc;
            }


            db.Siparisler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Silindi";
            return sonuc;
        }

        #endregion


    }
}
