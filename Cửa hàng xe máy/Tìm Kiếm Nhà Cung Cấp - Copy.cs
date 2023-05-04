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

namespace Cửa_hàng_xe_máy
{
    public partial class Tìm_Kiếm_Nhà_Cung_Cấp : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
        public Tìm_Kiếm_Nhà_Cung_Cấp()
        {
            InitializeComponent();
        }
        public void ketnoi()
        {
            con = new SqlConnection(chuoikn);
            //con.ConnectionString = chuoikn;
            if (con.State == ConnectionState.Closed)
                con.Open();
            //MessageBox.Show("Mở kết nối thành công");
        }

        public void dongkn()
        {
            con = new SqlConnection(chuoikn);
            if (con.State == ConnectionState.Open)
                con.Close();
            //MessageBox.Show("ngắt kết nối thành công");
        }
  
        void loaddgv()
        {
            ketnoi();
            cmd = new SqlCommand("Select * from QLNCC", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvTKNCC.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
            }
            dongkn();
        }

        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLNCC", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvTKNCC.DataSource = dt;
            // Thay đổi độ rộng
            dgvTKNCC.Columns[0].Width = 30;
            dgvTKNCC.Columns[1].Width = 30;
            dgvTKNCC.Columns[2].Width = 100;
            dgvTKNCC.Columns[3].Width = 40;
            dgvTKNCC.Columns[4].Width = 300;
           
            //thay đổi tiêu đề
            dgvTKNCC.Columns[0].HeaderText = "Mã NCC";
            dgvTKNCC.Columns[1].HeaderText = "Mã Loại SP";
            dgvTKNCC.Columns[2].HeaderText = "Tên NCC";
            dgvTKNCC.Columns[3].HeaderText = "SĐT";
            dgvTKNCC.Columns[4].HeaderText = "Địa Chỉ";
        }
        void thucthisql(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongkn();
        }
        int kiemtramatrung(string ma)
        {
            int i;
            ketnoi();
            string sql = "Select count(*) from QLNCC where MaNCC='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }




        //void LoadQLNCC()
        //{
        //    cmd= con.CreateCommand();
        //    cmd.CommandText= "select * from QLNCC";
        //    adapter.SelectCommand = cmd;
        //    tb.Clear();
        //    adapter.Fill(tb);
        //    dgvQLNCC.DataSource = tb;
        //}
      
        private void Tìm_Kiếm_Nhà_Cung_Cấp_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();
        }
     

        private void TimKiemMa_Click_1(object sender, EventArgs e)
        {
            ketnoi();
            string timkiem = "SELECT *FROM QLNCC WHERE MaNCC like'%" + txtMaNCC.Text + "%' ";

            SqlCommand cmd = new SqlCommand(timkiem, con);
            SqlDataReader da = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(da);
            dgvTKNCC.DataSource = dt;
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            foreach (Control ctrl in TTTK1.Controls)

            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).Text = "";
                }
               

            }


          
            loaddgv_phikn();
        }
    }
}
