﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Transaksi_PreOrder.Model.Entity;
using Transaksi_PreOrder.Controller;

namespace Transaksi_PreOrder
{
    public partial class FormUtama : Form
    {
        private List<Barang> listBarang = new List<Barang>();
        private List<DetailPesanan> detailPesanan = new List<DetailPesanan>();
        private List<Pesanan> listPesanan = new List<Pesanan>();

        //tampil kode admin yg login
        public string currentAdmin = Login.AdminInfo.CurrentLoggedInAdmin;

        private BarangController controller;
        private PesananController controller1;
        //private PesananController pesananController;

       // private DetailPesananController controllerdetail;

        public FormUtama()
        {
            InitializeComponent();

            controller = new BarangController();
            controller1 = new PesananController();
            //pesananController = new pesa
           // controllerdetail = new DetailPesananController();

            InisialisasiListView();
            loadPesanan();

    }

        

        private void InisialisasiListView()
        {
            lvwData.View = View.Details;
            lvwData.FullRowSelect = true;
            lvwData.GridLines = true;

            lvwData.Columns.Add("No", 50, HorizontalAlignment.Center);
            lvwData.Columns.Add("Kode Pesanan", 140, HorizontalAlignment.Center);
            lvwData.Columns.Add("Tanggal Pesanan", 196, HorizontalAlignment.Center);
            lvwData.Columns.Add("Cara Bayar", 200, HorizontalAlignment.Center);
            lvwData.Columns.Add("Jatuh Tempo", 200, HorizontalAlignment.Center);
            lvwData.Columns.Add("Status Pesanan", 200, HorizontalAlignment.Center);
        }


        private void loadPesanan()
        {
            lvwData.Items.Clear();


            listPesanan = controller1.ReadAllPesanan();

            foreach(var psn in listPesanan)
            {
                var noUrut = lvwData.Items.Count + 1;

                // // tampilkan data mhs yg baru ke list view
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(psn.KdPesanan);
               // item.SubItems.Add(psn.TglPesan);
               item.SubItems.Add(psn.CaraBayar);
                //item.SubItems.Add(psn.JatuhTempo);



                lvwData.Items.Add(item);
            }
        }


        private void PesananCreateEventHandler(Pesanan psn)
        {
            // tambahkan objek mhs yang baru ke dalam collection

            // ENABLE BUAT DAFTAR PESANAN

            listPesanan.Add(psn);

             int noUrut = lvwData.Items.Count + 1;

            // // tampilkan data mhs yg baru ke list view
             ListViewItem item = new ListViewItem(noUrut.ToString());
             item.SubItems.Add(psn.KdPesanan);
           //  item.SubItems.Add(psn.TglPesan);
           // item.SubItems.Add(psn.CaraBayar);
           // item.SubItems.Add(psn.JatuhTempo);
            


            lvwData.Items.Add(item);
        }

        private void PesananUpdateEventHandler(Pesanan psn)
        {
            int index = lvwData.SelectedIndices[0];

            ListViewItem itemRow = lvwData.Items[index];
            itemRow.SubItems[1].Text = psn.KdPesanan;
            itemRow.SubItems[2].Text = psn.CaraBayar;

            
        }

        private void btnBuatPesanan_Click(object sender, EventArgs e)
        {
            FormPesanan formPesanan = new FormPesanan("tambah pesanan", controller1);

            formPesanan.PesananCreate += PesananCreateEventHandler;

             //FormDetailPesanan formDetail = new FormDetailPesanan("tambah Barang", controllerdetail);
            //formDetail.DetailPesananCreate += onCreateEventHandlerDetail;
            formPesanan.ShowDialog();
        }

        private void onCreateEventHandler(Barang brg)
        {
           // tambahkan objek mhs yang baru ke dalam collection

           // ENABLE BUAT DAFTAR PESANAN

           //listBarang.Add(brg);

           //int noUrut = lvwData.Items.Count + 1;

           // // tampilkan data mhs yg baru ke list view
          // ListViewItem item = new ListViewItem(noUrut.ToString());
           //item.SubItems.Add(brg.KdBarang);
          // item.SubItems.Add(brg.Nama);
          // item.SubItems.Add(Convert.ToString(brg.Harga));

           //lvwData.Items.Add(item);
        }

        private void onCreateEventHandlerDetail(DetailPesanan p)
        {
            // tambahkan objek mhs yang baru ke dalam collection

            // ENABLE BUAT DAFTAR PESANAN

            detailPesanan.Add(p);

            int noUrut = lvwData.Items.Count + 1;

            // // tampilkan data mhs yg baru ke list view
            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(p.KdDetail);
            item.SubItems.Add(p.KdPesanan);
            //item.SubItems.Add(Convert.ToString(brg.Harga));

            lvwData.Items.Add(item);
        }

        private void btnEntryBarang_Click(object sender, EventArgs e)
        {

            EntryBarang entryBarang = new EntryBarang("tambah Barang", controller);

            entryBarang.onCreate += onCreateEventHandler;

            entryBarang.ShowDialog();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            //tampil kode admin yg login
            txtAdmin.Text = currentAdmin;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lvwData.SelectedItems.Count > 0)
            {
                // ambil objek mhs yang mau diedit dari collection
                Pesanan psn = listPesanan[lvwData.SelectedIndices[0]];

                // buat objek form entry data mahasiswa
                FormPesanan frmPesanan = new FormPesanan("Update", psn, controller1);

                // mendaftarkan method event handler untuk merespon event OnUpdate
                frmPesanan.PesananUpdate += PesananUpdateEventHandler;

                // tampilkan form entry mahasiswa
                frmPesanan.ShowDialog();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwData.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus?", "Konfirmasi",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil objek mhs yang mau dihapus dari collection
                    Pesanan pesanan = listPesanan[lvwData.SelectedIndices[0]];

                    // panggil operasi CRUD
                    var result = controller1.Delete(pesanan);
                    if (result > 0) loadPesanan();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}