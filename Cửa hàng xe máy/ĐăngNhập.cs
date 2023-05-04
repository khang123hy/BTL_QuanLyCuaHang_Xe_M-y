using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cửa_hàng_xe_máy
{
    public partial class Đăng_Nhập : Form
    {
  
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
      
        public Đăng_Nhập()
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
        private void dangnhap_Click(object sender, EventArgs e)
        {

            ketnoi();
            cmd = new SqlCommand();
            cmd.CommandText = "AuthoLogin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Username", txtTaiKhoan.Text);
            cmd.Parameters.AddWithValue("@Password", txtMatKhau.Text);
            int code = (int)cmd.ExecuteScalar();
            if (code == 1)
            {
                MessageBox.Show("Chào mừng Admin đăng nhập!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.code = 1;
                TrangChu mn = new TrangChu();
                this.Hide();
                mn.ShowDialog();
                this.Close();

            }
            else if (code == 0)
            {
                MessageBox.Show("Chào mừng User đăng nhập!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.code = 0;
                TrangChu mn = new TrangChu();
                this.Hide();
                mn.ShowDialog();
              

                this.Close();
            }
            else if (code == 2)
            {
                MessageBox.Show("Sai thông tin tài khoản!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dongkn();
        }

        private void thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng khồng?,", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            return;
        }



        private void hienthimk_CheckedChanged(object sender, EventArgs e)
        {
            string Name = txtMatKhau.Text;

            String thanhvien = hienthimk.Text;
            if (hienthimk.CheckState == CheckState.Checked)
                txtMatKhau.UseSystemPasswordChar = false;
            else
                txtMatKhau.UseSystemPasswordChar = true;
        }

        private void txtTaiKhoan_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaiKhoan.Text))
            {
                e.Cancel = true;
                txtTaiKhoan.Focus();
                errUser.SetError(txtTaiKhoan, "Bạn cần nhập vào Username");
            }
            else
            {
                e.Cancel = false;
                errUser.SetError(txtTaiKhoan, "");
            }
        }

        private void txtMatKhau_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                e.Cancel = true;
                txtMatKhau.Focus();
                errPass.SetError(txtMatKhau, "Bạn cần nhập password");
            }
            else
            {
                e.Cancel = false;
                errPass.SetError(txtMatKhau, "");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
