using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace OkulProje
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        string c = "";
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1L6L6RD\SQLEXPRESS;Initial Catalog=OkulProje;Integrated Security=True");
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKULUPLER ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "KULUPAD";
            comboBox1.ValueMember = "KULUPID";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            
            if(radioButton1.Checked == true)
            {
                c = "Kız";
            }
            if(radioButton2.Checked == true)
            {
                c = "Erkek";
            }
            ds.OgrenciEkle(TxtAd.Text,TxtSoyad.Text,byte.Parse(comboBox1.SelectedValue.ToString()),c);
            MessageBox.Show("Öğrenci Ekleme İşlemi Yapıldı.");
           
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtID.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[4].Value.Equals("KIZ"))
                radioButton1.Checked = true;
            else
                radioButton1.Checked = false;

            if (dataGridView1.CurrentRow.Cells[4].Value.Equals("ERKEK"))
                radioButton2.Checked = true;
            else
                radioButton2.Checked = false;
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource=  ds.OgrenciGetir(TxtAra.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtID.Text = comboBox1.SelectedValue.ToString();
        }
    }
}
