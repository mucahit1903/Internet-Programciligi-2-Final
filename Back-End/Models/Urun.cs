//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace İntProg.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            this.Siparisler = new HashSet<Siparisler>();
        }
    
        public int urunId { get; set; }
        public string urunKodu { get; set; }
        public string urunAdi { get; set; }
        public int urunStok { get; set; }
        public decimal urunFiyatSat { get; set; }
        public decimal urunFiyatAl { get; set; }
        public string urunAciklama { get; set; }
        public string urunGorsel { get; set; }
        public int urunKatId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparisler> Siparisler { get; set; }
    }
}
