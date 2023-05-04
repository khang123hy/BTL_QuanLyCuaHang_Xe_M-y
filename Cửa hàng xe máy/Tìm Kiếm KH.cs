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
using System.Data.OleDb;


namespace Cửa_hàng_xe_máy
{
    public partial class Tìm_Kiếm_KH : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
     

        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
        public Tìm_Kiếm_KH()
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
            cmd = new SqlCommand("Select * from QLKH", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvTKKH.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
            }
            dongkn();
        }

        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLKH", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvTKKH.DataSource = dt;
            // Thay đổi độ rộng
            dgvTKKH.Columns[0].Width = 55;
            dgvTKKH.Columns[1].Width = 55;
            dgvTKKH.Columns[2].Width = 135;
            dgvTKKH.Columns[3].Width = 90;
            dgvTKKH.Columns[4].Width = 80;
            dgvTKKH.Columns[5].Width = 50;
            dgvTKKH.Columns[6].Width = 135;
            dgvTKKH.Columns[7].Width = 135;
            //thay đổi tiêu đề

            dgvTKKH.Columns[0].HeaderText = "Mã KH";
            dgvTKKH.Columns[1].HeaderText = "Mã Loại SP";
            dgvTKKH.Columns[2].HeaderText = "Tên KH";
            dgvTKKH.Columns[3].HeaderText = "SĐT";
            dgvTKKH.Columns[4].HeaderText = "Ngày Sinh";
            dgvTKKH.Columns[5].HeaderText = "Giới Tính";
            dgvTKKH.Columns[6].HeaderText = "Địa Chỉ";
            dgvTKKH.Columns[7].HeaderText = "Số CMND/CCCD";

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
            string sql = "Select count(*) from QLKH where MaKH='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }
        private void Tìm_Kiếm_KH_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();

        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in TTTK1.Controls )

            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).Text = "";
                }
               

            }
         
    
            txtMaKH.Enabled = true;
            loaddgv_phikn();
        }
        private void TimKiemMa_Click(object sender, EventArgs e)
        {

            string timkiem = "SELECT *FROM QLKH WHERE MaKH like'%" + txtMaKH.Text + "%' ";
            SqlCommand cmd = new SqlCommand(timkiem, con);
            SqlDataReader da = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(da);
            dgvTKKH.DataSource = dt;

        }

        private void dgvTKKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TTTK1_Enter(object sender, EventArgs e)
        {

        }
    }
}
