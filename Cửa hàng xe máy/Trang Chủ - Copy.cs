using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cửa_hàng_xe_máy
{
    public partial class TrangChu : Form
    {
        //public static int code == 0;
        public TrangChu()
        {
            InitializeComponent();
        }

      

        private void thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) { Close(); }
            return;
        }

        private void qlDonBan_Click(object sender, EventArgs e)
        {
      
        }

        private void qlXe_Click(object sender, EventArgs e)
        {
            Quản_Lý_Xe quanlyxe = new Quản_Lý_Xe();
            this.Hide();
            quanlyxe.ShowDialog();
            this.Show();
        }

        private void qlDonNhap_Click(object sender, EventArgs e)
        {

            Quản_Lý_Nhân_Viên qlnhap= new Quản_Lý_Nhân_Viên();
            this.Hide();
            qlnhap.ShowDialog();
            this.Show();

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quản_Lý_Khách_Hàng kh = new Quản_Lý_Khách_Hàng();
            this.Hide();
            kh.ShowDialog();
            this.Show(); ;
        }

        private void qlNhaCungCap_Click(object sender, EventArgs e)
        {
            Quản_Lý_Nhà_Cung_Cấp qlcc = new Quản_Lý_Nhà_Cung_Cấp();
            this.Hide();
            qlcc.ShowDialog();
            this.Show();
        }

        private void tkKhachHang_Click(object sender, EventArgs e)
        {
            Tìm_Kiếm_KH tk = new Tìm_Kiếm_KH();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }

        private void tkNhaCungCap_Click(object sender, EventArgs e)
        {
            Tìm_Kiếm_Nhà_Cung_Cấp tkcc = new Tìm_Kiếm_Nhà_Cung_Cấp();
            this.Hide();
            tkcc.ShowDialog();
            this.Show();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (Program.code == 0)
            {
                QuanLy.Visible = false;

            }
            if (Program.code == 1)
            {
                QuanLy.Visible = true;

            }

        }

       

        private void quảnLýLoạiXe_Click(object sender, EventArgs e)
        {
            Loại_Xe ld = new Loại_Xe();
            this.Hide();
            ld.ShowDialog();
            this.Show();
        }

        private void tkXe_Click(object sender, EventArgs e)
        {
            TÌm_Kiếm_Xe xe = new TÌm_Kiếm_Xe();
            this.Hide();
            xe.ShowDialog();
            this.Show();
        }

        private void bcKhachHang_Click(object sender, EventArgs e)
        {
            Thống_kê_QLSP tk = new Thống_kê_QLSP();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Quản_Lý_Nhân_Viên tk = new Quản_Lý_Nhân_Viên();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?,", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Đăng_Nhập dn = new Đăng_Nhập();
                this.Hide();
                dn.ShowDialog();
                this.Close();
               
            }
            return;
        }

        private void bcNhapXe_Click(object sender, EventArgs e)
        {
            Thống_kê_ncc tk = new Thống_kê_ncc();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }

        private void xeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thống_Kê_Xe tk = new Thống_Kê_Xe();
            tk.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {

            Đổi_mật_khẩu dmk = new Đổi_mật_khẩu();
            dmk.ShowDialog();
           
        }

        private void thôngTinỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thông_tin_ứng_dụng dmk = new Thông_tin_ứng_dụng();
            dmk.ShowDialog();
        }

        private void tổngĐàiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tổng_đài td = new Tổng_đài();
            td.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?,", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
               Close();

            }
            return;
        }

      
    }
}
