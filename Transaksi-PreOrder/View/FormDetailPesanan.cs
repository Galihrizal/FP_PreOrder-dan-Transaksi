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
    public partial class FormDetailPesanan : Form
    {
        //deklarasi untuk event tambah data & update
        public delegate void CreateDetailPesananHandler(DetailPesanan detpsn);

        //event tambah data
        public event CreateDetailPesananHandler DetailPesananCreate;

        //event update data
        //public event CreateDetailPesananHandler PesananUpdate;

        //objek kontraoller
        private DetailPesananController controller1;
        //private AdminController controllerAdmin;

        // deklarasi field untuk menyimpan status entry data (input baru atau update)
        private bool isNewData = true;

        // deklarasi field untuk meyimpan objek mahasiswa
        private DetailPesanan detpsn;

        public FormDetailPesanan()
        {
            InitializeComponent();
        }

       

        // constructor untuk inisialisasi data ketika entri data baru
        public FormDetailPesanan(string title, DetailPesananController controller1)
            : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller1 = controller1;
        }

        // constructor untuk inisialisasi data ketika mengedit data
        public FormDetailPesanan(string title, DetailPesanan obj1, DetailPesananController controller1)
            : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller1 = controller1;

            isNewData = false; // set status edit data
            detpsn = obj1; // set objek mhs yang akan diedit

            // untuk edit data, tampilkan data lama

            txtKdDetail.Text = detpsn.KdDetail;
            txtKdPesanan.Text = detpsn.KdPesanan;
            //txtAdmin.Text = psn.KdAdmin;

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // jika data baru, inisialisasi objek mahasiswa
            if (isNewData) detpsn = new DetailPesanan();

            // set nilai property objek mahasiswa yg diambil dari TextBox

            detpsn.KdDetail = txtKdDetail.Text;
            detpsn.KdPesanan = txtKdPesanan.Text;
            //psn.KdAdmin = txtAdmin.Text;

            


            int result1 = 0;

            if (isNewData) // tambah data baru, panggil method Create
            {
                // panggil operasi CRUD
                result1 = controller1.Create(detpsn);

                if (result1 > 0) // tambah data berhasil
                {
                    DetailPesananCreate(detpsn); // panggil event OnCreate

                    // reset form input, utk persiapan input data berikutnya
                    //txtKdPesanan.Clear();

                }
            }

        }

        private void FormDetailPesanan_Load(object sender, EventArgs e)
        {
            txtKdPesanan.Text = FormPesanan.PesananInfo.KodePesanan;
        }
    }
}
