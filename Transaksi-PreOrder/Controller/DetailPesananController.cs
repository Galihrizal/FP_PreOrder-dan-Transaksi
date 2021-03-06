﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Transaksi_PreOrder.Model.Entity;
using Transaksi_PreOrder.Model.Repository;
using Transaksi_PreOrder.Model.Context;

namespace Transaksi_PreOrder.Controller
{
    public class DetailPesananController
    {
        //objek CRUD
        private DetailPesananRepository _repository;


        public int Create(DetailPesanan psn)
        {
            int result1 = 0;

            // cek npm yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(psn.KdDetail))
            {
                MessageBox.Show("Kode barang harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new DetailPesananRepository(context);

                // panggil method Create class repository untuk menambahkan data
                result1 = _repository.Create(psn);
            }

            if (result1 > 0)
            {
                MessageBox.Show("Data mahasiswa berhasil disimpan !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data mahasiswa gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result1;
        }
    }
}
