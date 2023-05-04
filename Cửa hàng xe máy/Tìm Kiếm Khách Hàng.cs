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


namespace Cửa_hàng_xe_máy
{
    public partial class Tìm_Kiếm_Khách_Hàng : Form
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
            cmd = new SqlCommand("Select * from QLKH", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvQLKH.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
            }
            dongkn();
        }
        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLKH", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvQLKH.DataSource = dt;
            dgvQLKH.Columns[0].HeaderText = "Mã KH";
            dgvQLKH.Columns[1].HeaderText = "Mã Loại SP";
            dgvQLKH.Columns[2].HeaderText = "Tên KH";
            dgvQLKH.Columns[3].HeaderText = "SĐT";
            dgvQLKH.Columns[4].HeaderText = "Ngày Sinh";
            dgvQLKH.Columns[5].HeaderText = "Giới Tính";
            dgvQLKH.Columns[6].HeaderText = "Địa Chỉ";
            dgvQLKH.Columns[7].HeaderText = "Số CMND/CCCD";

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

        private void Tìm_Kiếm_Khách_Hàng_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
}
