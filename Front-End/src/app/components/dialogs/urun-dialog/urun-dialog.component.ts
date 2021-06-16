import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Kategori } from 'src/app/models/Kategori';
import { Urun } from 'src/app/models/Urun';
import { ApiService } from 'src/app/services/api.service';
import { MyAlertService } from 'src/app/services/myAlert.service';

@Component({
  selector: 'app-urun-dialog',
  templateUrl: './urun-dialog.component.html',
  styleUrls: ['./urun-dialog.component.css']
})
export class UrunDialogComponent implements OnInit {
  dialogBaslik: string;
  yeniKayit: Urun;
  islem: string;
  frm: FormGroup;
  dataSource: any;
  kategoriler: Kategori[];
  constructor(
    public dialogRef: MatDialogRef<UrunDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder,
    public apiServis: ApiService,
    public matDialog: MatDialog,
    public alert: MyAlertService
  ) {
    this.islem = data.islem;
    this.yeniKayit = data.kayit;

    if (this.islem == 'ekle') {
      this.dialogBaslik = 'Ürün Ekle';
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = 'Ürün Düzenle';
    }

    this.frm=this.FormOlustur();
   }

  ngOnInit() {
    this.KategoriListe()
  }

  FormOlustur() {
    return this.frmBuild.group({

      urunKodu: [this.yeniKayit.urunKodu],
      urunAdi: [this.yeniKayit.urunAdi],
      urunStok: [this.yeniKayit.urunStok],
      urunFiyatSat: [this.yeniKayit.urunFiyatSat],
      urunFiyatAl: [this.yeniKayit.urunFiyatAl],
      urunAciklama: [this.yeniKayit.urunAciklama],
      urunGorsel: [this.yeniKayit.urunGorsel],
      urunKatId: [this.yeniKayit.urunKatId],
      urunKatAdi: [this.yeniKayit.urunKatAdi],
    });
  }

  KategoriListe() {
    this.apiServis.KategoriListe().subscribe((d: Kategori[]) => {
      this.kategoriler = d;
      this.dataSource = new MatTableDataSource(this.kategoriler);

    });
  }
}
