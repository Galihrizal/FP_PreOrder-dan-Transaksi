﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Transaksi_PreOrder.Model.Entity;
using Transaksi_PreOrder.Model.Context;

namespace Transaksi_PreOrder.Model.Repository
{
    public class DetailPesananRepository
    {
        //objek connection 
        private MySqlConnection _conn;

        //constructor
        public DetailPesananRepository(DbContext context)
        {
            //inisialisasi objek conn
            _conn = context.Conn;
        }

        public int Create(DetailPesanan detailPesanan)
        {
            int result = 0;


            //detail pesanan
            //string sql2 = @"insert into detail_pesanan (kd_detail, kd_barang, Qty, Subtotal, kd_pesanan)
            //               values (@kd_detail, @kd_barang, @Qty, @Subtotal, @kd_pesanan)";

            //detail pesanan
            string sql = @"insert into detail_pesanan (kd_detail,kd_pesanan)
                           values (@kddetail,@kdpesanan)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@kddetail", detailPesanan.KdDetail);
                //cmd.Parameters.AddWithValue("@kd_barang", detail.KdBarang);
                //cmd.Parameters.AddWithValue("@Qty", detail.Qty);
                //cmd.Parameters.AddWithValue("@Subtotal", detail.Subtotal);
                cmd.Parameters.AddWithValue("@kdpesanan", detailPesanan.KdPesanan);  

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return result;
        }

    }
}
