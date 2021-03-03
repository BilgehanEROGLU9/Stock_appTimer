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

namespace timer
{
    public partial class Form1 : Form
    {
        bool aç = true;
        int sayac = 0;
        public string conString = "Data Source=DESKTOP-BA5NEFA;Initial Catalog=stok1;Integrated Security=True";
        public string conString2 = "Data Source=DESKTOP-BA5NEFA;Initial Catalog=stok2;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            label1.Text = sayac.ToString();
            SqlConnection con = new SqlConnection(conString);
            SqlConnection con2 = new SqlConnection(conString2);
            SqlCommand cmd = new SqlCommand("DELETE FROM stokB", con2);

            con.Open();
            con2.Open();
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("SELECT * FROM stokA WHERE aktarildi='evet'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            SqlBulkCopy bulkData = new SqlBulkCopy(con2);
            bulkData.DestinationTableName = "stokB";
            bulkData.WriteToServer(reader);
            bulkData.Close();
            con.Close();
            con2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (aç)
            {
                button1.Text = "DURDUR";
                timer1.Start();
                aç = false;
            }
            else
            {
                button1.Text = "BAŞLAT";
                timer1.Stop();
                aç = true;
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "BAŞLAT";
            timer1.Interval = 3000;
        }
    }
}
