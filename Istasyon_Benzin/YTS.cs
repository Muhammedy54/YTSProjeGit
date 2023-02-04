using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Istasyon_Benzin
{
    public partial class YTS : Form
    {
        public YTS()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MUHAMMEDYILMAZ\SQLEXPRESS;Initial Catalog=TestBenzin;Integrated Security=True");

        void kasa()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLKASA", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LblKasaTutar.Text = dr[0].ToString();
            }
            baglanti.Close();
        }


        void listele()
        {
            //KURŞUNSUZ 95
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLBENZİN WHERE PETROLTUR='Kurşunsuz95'", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LblKursunsuz95Fiyat.Text = dr[3].ToString();
                PrgKursunsuz95.Value = int.Parse(dr[4].ToString());
                LblKursunsuz95Kalan.Text = dr[4].ToString();
            }
            baglanti.Close();

            //KURŞUNSUZ 97
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM TBLBENZİN WHERE PETROLTUR='Kurşunsuz97'", baglanti);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                LblKursunsuz97Fiyat.Text = dr2[3].ToString();
                PrgKursunsuz97.Value = int.Parse(dr2[4].ToString());
                LblKursunsuz97Kalan.Text = dr2[4].ToString();
            }
            baglanti.Close();

            //Euro Dizel10
            baglanti.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM TBLBENZİN WHERE PETROLTUR='EuroDizel10'", baglanti);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                LblEuroDizel10Fiyat.Text = dr3[3].ToString();
                PrgEuroDizel10.Value = int.Parse(dr3[4].ToString());
                LblEuroDizel10Kalan.Text = dr3[4].ToString();
            }
            baglanti.Close();

            //Yeni Pro Dizel
            baglanti.Open();
            SqlCommand cmd4 = new SqlCommand("SELECT * FROM TBLBENZİN WHERE PETROLTUR='YeniProDizel'", baglanti);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                LblYeniProDizelFiyat.Text = dr4[3].ToString();
                PrgYeniProDizel.Value = int.Parse(dr4[4].ToString());
                LblYeniProDizelKalan.Text = dr4[4].ToString();
            }
            baglanti.Close();

            //Gaz
            baglanti.Open();
            SqlCommand cmd5 = new SqlCommand("SELECT * FROM TBLBENZİN WHERE PETROLTUR='Gaz'", baglanti);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                LblGazFiyat.Text = dr5[3].ToString();
                PrgGaz.Value = int.Parse(dr5[4].ToString());
                LblGazKalan.Text = dr5[4].ToString();
            }
            baglanti.Close();
        }    

        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            kasa();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(LblKursunsuz95Fiyat.Text);
            litre = Convert.ToDouble(NudKursunsuz95LitreSatis.Value);
            tutar = kursunsuz95 * litre;
            TxtKursunsuz95Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(LblKursunsuz97Fiyat.Text);
            litre = Convert.ToDouble(NudKursunsuz97LitreSatis.Value);
            tutar = kursunsuz97 * litre;
            TxtKursunsuz97Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double eurodizel10, litre, tutar;
            eurodizel10 = Convert.ToDouble(LblEuroDizel10Fiyat.Text);
            litre = Convert.ToDouble(NudEuroDizel10LitreSatis.Value);
            tutar = eurodizel10 * litre;
            TxtEuroDizel10Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double yeniprodizel, litre, tutar;
            yeniprodizel = Convert.ToDouble(LblYeniProDizelFiyat.Text);
            litre = Convert.ToDouble(NudYeniProDizelLitreSatis.Value);
            tutar = yeniprodizel * litre;
            TxtYeniProDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(LblGazFiyat.Text);
            litre = Convert.ToDouble(NudGazLitreSatis.Value);
            tutar = gaz * litre;
            TxtGazFiyat.Text = tutar.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (NudKursunsuz95LitreSatis.Value != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                cmd.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@P2", "Kurşunsuz 95");
                cmd.Parameters.AddWithValue("@P3", NudKursunsuz95LitreSatis.Value);
                cmd.Parameters.AddWithValue("@P4",decimal.Parse( TxtKursunsuz95Fiyat.Text));
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                cmd2.Parameters.AddWithValue("@P1",decimal.Parse( TxtKursunsuz95Fiyat.Text));
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                

                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand("update TBLBENZİN SET STOK=STOK-@P1 WHERE PETROLTUR='Kurşunsuz95'", baglanti);
                cmd3.Parameters.AddWithValue("@P1", NudKursunsuz95LitreSatis.Value);
                cmd3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
                baglanti.Close();
            }
            else if (NudKursunsuz97LitreSatis.Value != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                cmd.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@P2", "Kurşunsuz 97");
                cmd.Parameters.AddWithValue("@P3", NudKursunsuz97LitreSatis.Value);
                cmd.Parameters.AddWithValue("@P4", decimal.Parse(TxtKursunsuz97Fiyat.Text));
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                cmd2.Parameters.AddWithValue("@P1", decimal.Parse(TxtKursunsuz97Fiyat.Text));
                cmd2.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand("update TBLBENZİN SET STOK=STOK-@P1 WHERE PETROLTUR='Kurşunsuz97'", baglanti);
                cmd3.Parameters.AddWithValue("@P1", NudKursunsuz97LitreSatis.Value);
                cmd3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
                baglanti.Close();
            }
            else if (NudEuroDizel10LitreSatis.Value != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                cmd.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@P2", "Euro Dizel10");
                cmd.Parameters.AddWithValue("@P3", NudEuroDizel10LitreSatis.Value);
                cmd.Parameters.AddWithValue("@P4", decimal.Parse(TxtEuroDizel10Fiyat.Text));
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                cmd2.Parameters.AddWithValue("@P1", decimal.Parse(TxtEuroDizel10Fiyat.Text));
                cmd2.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand("update TBLBENZİN SET STOK=STOK-@P1 WHERE PETROLTUR='EuroDizel10'", baglanti);
                cmd3.Parameters.AddWithValue("@P1", NudEuroDizel10LitreSatis.Value);
                cmd3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
                baglanti.Close();
            }
            else if (NudYeniProDizelLitreSatis.Value != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                cmd.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@P2", "Yeni Pro Dizel");
                cmd.Parameters.AddWithValue("@P3", NudYeniProDizelLitreSatis.Value);
                cmd.Parameters.AddWithValue("@P4", decimal.Parse(TxtYeniProDizelFiyat.Text));
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                cmd2.Parameters.AddWithValue("@P1", decimal.Parse(TxtYeniProDizelFiyat.Text));
                cmd2.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand("update TBLBENZİN SET STOK=STOK-@P1 WHERE PETROLTUR='YeniProDizel'", baglanti);
                cmd3.Parameters.AddWithValue("@P1", NudYeniProDizelLitreSatis.Value);
                cmd3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
                baglanti.Close();
            }
            else if (NudGazLitreSatis.Value != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                cmd.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@P2", "Gaz");
                cmd.Parameters.AddWithValue("@P3", NudGazLitreSatis.Value);
                cmd.Parameters.AddWithValue("@P4", decimal.Parse(TxtGazFiyat.Text));
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                cmd2.Parameters.AddWithValue("@P1", decimal.Parse(TxtGazFiyat.Text));
                cmd2.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand("update TBLBENZİN SET STOK=STOK-@P1 WHERE PETROLTUR='Gaz'", baglanti);
                cmd3.Parameters.AddWithValue("@P1", NudGazLitreSatis.Value);
                cmd3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NudKursunsuz95LitreAlim.Value != 0)
            {
                double tutar, litre;
                litre=Convert.ToDouble(NudKursunsuz95LitreAlim.Value);
                tutar = litre * 5.94;
                TxtKursunsuz95Maliyet.Text = tutar.ToString();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", tutar);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLBENZiN set STOK=STOK+@p1 where PETROLTUR='Kurşunsuz95'", baglanti);
                cmd2.Parameters.AddWithValue("@p1", NudKursunsuz95LitreAlim.Value);
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alım Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
            }
            else if (NudKursunsuz97LitreAlim.Value != 0)
            {
                double tutar, litre;
                litre = Convert.ToDouble(NudKursunsuz97LitreAlim.Value);
                tutar = litre * 5.98;
                TxtKursunsuz97Maliyet.Text = tutar.ToString();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", tutar);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLBENZiN set STOK=STOK+@p1 PETROLTUR='Kurşunsuz97'", baglanti);
                cmd2.Parameters.AddWithValue("@p1", NudKursunsuz97LitreAlim.Value);
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alım Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
            }
            else if (NudEuroDizel10LitreAlim.Value != 0)
            {
                double tutar, litre;
                litre = Convert.ToDouble(NudEuroDizel10LitreAlim.Value);
                tutar = litre * 4.49;
                TxtEuroDizel10Maliyet.Text = tutar.ToString();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", tutar);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLBENZiN set STOK=STOK+@p1 where PETROLTUR='EuroDizel10'", baglanti);
                cmd2.Parameters.AddWithValue("@p1", NudEuroDizel10LitreAlim.Value);
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alım Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
            }
            else if (NudYeniProDizelLitreAlim.Value != 0)
            {
                double tutar, litre;
                litre = Convert.ToDouble(NudYeniProDizelLitreAlim.Value);
                tutar = litre * 5.51;
                TxtYeniProDizelMaliyet.Text = tutar.ToString();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", tutar);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLBENZiN set STOK=STOK+@p1 where PETROLTUR='YeniProDizel'", baglanti);
                cmd2.Parameters.AddWithValue("@p1", NudYeniProDizelLitreAlim.Value);
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alım Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
            }
            else if (NudGazLitreAlim.Value != 0)
            {
                double tutar, litre;
                litre = Convert.ToDouble(NudGazLitreAlim.Value);
                tutar = litre * 3.28;
                TxtGazMaliyet.Text = tutar.ToString();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                cmd.Parameters.AddWithValue("@p1", tutar);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand("update TBLBENZiN set STOK=STOK+@p1 where PETROLTUR='Gaz'", baglanti);
                cmd2.Parameters.AddWithValue("@p1", NudGazLitreAlim.Value);
                cmd2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alım Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                kasa();
            }
        }
        
    }
}

