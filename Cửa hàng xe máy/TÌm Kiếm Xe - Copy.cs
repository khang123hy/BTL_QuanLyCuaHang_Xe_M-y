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
    public partial class TÌm_Kiếm_Xe : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;


        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
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
            cmd = new SqlCommand("Select * from QLXe", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvTkXe.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            }
            dongkn();
        }
  
        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLXe", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvTkXe.DataSource = dt;
            // Thay đổi độ rộng
            dgvTkXe.Columns[0].Width = 35;
            dgvTkXe.Columns[1].Width = 35;
            dgvTkXe.Columns[2].Width = 70;
            dgvTkXe.Columns[3].Width = 35;
            dgvTkXe.Columns[4].Width = 50;
            dgvTkXe.Columns[5].Width = 200;


            //thay đổi tiêu đề
            dgvTkXe.Columns[0].HeaderText = "Mã SP";
            dgvTkXe.Columns[1].HeaderText = "Mã Loại SP";
            dgvTkXe.Columns[2].HeaderText = "Tên Xe";
            dgvTkXe.Columns[3].HeaderText = "Số Lượng";
            dgvTkXe.Columns[4].HeaderText = "Màu Sắc";
            dgvTkXe.Columns[5].HeaderText = "Giá Thành";
            //dgvQLX.Columns[5].DefaultCellStyle.Format = "#,###";
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
            string sql = "Select count(*) from QLXe where MaSP='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }
       
        public TÌm_Kiếm_Xe()
        {
            InitializeComponent();
        }

        private void TÌm_Kiếm_Xe_Load(object sender, EventArgs e)
        {
            
            
            
            loaddgv_phikn();
           
        }

        private void TimKiemMa_Click(object sender, EventArgs e)
        {
            string timkiem = "SELECT *FROM QLXe WHERE MaSP like'%" + txtMaXe.Text + "%' ";
            SqlCommand cmd = new SqlCommand(timkiem, con);
            SqlDataReader da = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(da);
            dgvTkXe.DataSource = dt;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
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

        private void txtMaXe_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}
