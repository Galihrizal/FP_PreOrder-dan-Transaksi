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
    public class PesananRepository
    {
        //objek connection 
        private MySqlConnection _conn;

        //constructor
        public PesananRepository(DbContext context)
        {
            //inisialisasi objek conn
            _conn = context.Conn;
        }

        public int Create(Pesanan pesanan)
        {
            int result1 = 0;
        // deklarasi perintah SQL
        string sql = @"insert into pesanan (kd_pesanan, kd_admin, cara_bayar )
                           values (@kdpesanan, @kd_admin, @cara_bayar)";

         //string sql = @"insert into pesanan (kd_pesanan, tgl_pesanan, cara_bayar, jatuh_tempo, catatan, uang_muka, sisa_bayar,kd_pembeli, kd_admin)
         //             values (@kd_pesanan, @tgl_pesanan, @cara_bayar, @jatuh_tempo, @catatan,@uang_muka,@sisa_bayar,kd_pembeli,@kd_admin)";


            // membuat objek command menggunakan blok using
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@kdpesanan", pesanan.KdPesanan);
                //cmd.Parameters.AddWithValue("@tgl_pesanan", pesanan.TglPesan);
                cmd.Parameters.AddWithValue("@cara_bayar", pesanan.CaraBayar);
                //cmd.Parameters.AddWithValue("@jatuh_tempo", pesanan.JatuhTempo);
                //cmd.Parameters.AddWithValue("@catatan", pesanan.Catatan);
               // cmd.Parameters.AddWithValue("@uang_muka", pesanan.Dp);
               // cmd.Parameters.AddWithValue("@sisa_bayar", pesanan.SisaPembayaran);
               // cmd.Parameters.AddWithValue("@kd_pembeli", pesanan.KdPembeli);
                cmd.Parameters.AddWithValue("@kd_admin", pesanan.KdAdmin);

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result1 = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            
            return result1;
        }

        public int Update(Pesanan psn)
        {
            int result1 = 0;

            // deklarasi perintah SQL
         

            string sql = @"update pesanan set cara_bayar = @cara_bayar
                           where kd_pesanan = @kd_pesanan";

            // membuat objek command menggunakan blok using
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@kd_pesanan", psn.KdPesanan);
                cmd.Parameters.AddWithValue("@cara_bayar", psn.CaraBayar);
                

                try
                {
                    // jalankan perintah UPDATE dan tampung hasilnya ke dalam variabel result
                    result1 = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }

            return result1;
        }

        public List<Pesanan> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Pesanan> list = new List<Pesanan>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select kd_pesanan, cara_bayar
                               from pesanan order by kd_pesanan";
                //, tgl_pesanan, jatuh_tempo, sts_pesanan,cara_bayar

                // membuat objek command menggunakan blok using
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Pesanan psn = new Pesanan();
                            
                            psn.KdPesanan = dtr["kd_pesanan"].ToString();
                            // psn.TglPesan = dtr["tgl_pesanan"].ToString();
                            // psn.JatuhTempo = dtr["jatuh_tempo"].ToString();
                            //psn.StatusPesanan = dtr["sts_pesanan"].ToString();
                            psn.CaraBayar = dtr["cara_bayar"].ToString();

                            list.Add(psn);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }

        public int Delete(Pesanan psn)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"DELETE FROM detail_pesanan where kd_pesanan=@kd_pesanan;
                           DELETE FROM pesanan where kd_pesanan=@kd_pesanan";

            // membuat objek command menggunakan blok using
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@kd_pesanan", psn.KdPesanan);

                try
                {
                    // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }

    }
}
