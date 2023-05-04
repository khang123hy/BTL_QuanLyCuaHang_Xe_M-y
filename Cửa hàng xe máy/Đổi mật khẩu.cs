using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cửa_hàng_xe_máy
{
    public partial class Đổi_mật_khẩu : Form
    {
        public Đổi_mật_khẩu()
        {
            InitializeComponent();
        }

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
        int kiemtramatrung(string ma)
        {
            int i;
            ketnoi();
            string sql = "Select count(*) from Users where Pass='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }
        int kiemtramkm()
        {
            if (txtMKM.Text == txtXNMK.Text)
            {
                return 1;
            }
            else return 0;
        }
        void thucthisql(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongkn();
        }

        private void btnDoi_Click(object sender, EventArgs e)
        {

            string sql = "update Users set Pass='" + txtMKM.Text + "' where UserID = '" + txtTaiKhoan.Text + "' ";
            DialogResult dr = MessageBox.Show("Bạn có muốn đổi mật khẩu ko ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (kiemtramatrung(txtMKC.Text) == 0)
                {
                    MessageBox.Show("Mật khẩu cũ ko đúng, vui lòng nhập lại !!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    txtMKC.Clear();
                }
                else if (kiemtramkm() == 0)
                {
                    MessageBox.Show("Mật không trùng với mật khẩu mới, vui lòng nhập lại !!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    txtXNMK.Clear();
                }
                else
                {
                    MessageBox.Show("Chúc mừng bạn đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    thucthisql(sql);
                    txtMKC.Clear();
                    txtMKM.Clear();
                    txtTaiKhoan.Clear();
                    txtXNMK.Clear();

                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
