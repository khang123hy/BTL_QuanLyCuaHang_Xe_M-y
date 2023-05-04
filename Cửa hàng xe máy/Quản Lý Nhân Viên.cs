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
using System.Text.RegularExpressions;

namespace Cửa_hàng_xe_máy
{
    public partial class Quản_Lý_Nhân_Viên : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";

        public Quản_Lý_Nhân_Viên()
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
            cmd = new SqlCommand("Select * from QLNV", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvQLNV.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
            }
            dongkn();
        }
        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLNV", con);
            dt = new DataTable();
            da.Fill(dt);
          

            dgvQLNV.DataSource = dt;
            // Thay đổi độ rộng
            //dgvQLKH.Columns[0].Width = 55;
            //dgvQLKH.Columns[1].Width = 55;
            //dgvQLKH.Columns[2].Width = 135;
            //dgvQLKH.Columns[3].Width = 90;
            //dgvQLKH.Columns[4].Width = 80;
            //dgvQLKH.Columns[5].Width = 50;
            //dgvQLKH.Columns[6].Width = 135;
            //dgvQLKH.Columns[7].Width = 135;
            //thay đổi tiêu đề
            dgvQLNV.Columns[0].HeaderText = "Mã NV";
            dgvQLNV.Columns[1].HeaderText = "Tên NV";
            dgvQLNV.Columns[2].HeaderText = "SĐT";
            dgvQLNV.Columns[3].HeaderText = "Địa Chỉ";
            dgvQLNV.Columns[4].HeaderText = "CCCD";
            dgvQLNV.Columns[5].HeaderText = "Chuyên Môn";
            dgvQLNV.Columns[6].HeaderText = "Mức Lương";
            dgvQLNV.Columns[7].HeaderText = "Giới Tính";

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
            string sql = "Select count(*) from QLNV where MaNV='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }

        private void Quản_Lý_Nhân_Viên_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();

        }

        private void dgvQLNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//lấy hàng đang chọn
            //chuyển dữ liệu lên các textbox tương ứng
            txtMaNV.Text = dgvQLNV[0, i].Value.ToString();//truy vấn đến ô dữ liệu
            txtTenNV.Text = dgvQLNV[1, i].Value.ToString();
            txtsdt.Text = dgvQLNV[2, i].Value.ToString();
            txtSoCMND.Text = dgvQLNV[4, i].Value.ToString();
            txtDiaChi.Text = dgvQLNV[3, i].Value.ToString();
            txtChuyenMon.Text = dgvQLNV[5, i].Value.ToString();
            txtMucLuong.Text = dgvQLNV[6, i].Value.ToString();
            txtGioiTinh.Text = dgvQLNV[7, i].Value.ToString();
          
            txtMaNV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string tenNV = txtTenNV.Text;
            string cccd = txtSoCMND.Text;
            string diachi = txtDiaChi.Text;
            string chuyenmon = txtChuyenMon.Text;
            string mucluong = txtMucLuong.Text;
            string sdt = txtsdt.Text;
            string gioitinh = txtGioiTinh.Text;
            string strSua = "update QLNV set MaNV='" + maNV + "', TenNV=N'" + tenNV + "', SDT='" + sdt + "', DiaChi=N'" + diachi + "', SoCCCD='" + cccd + "', ChuyenMon=N'" + chuyenmon + "', MucLuong= '" + mucluong + "', GioiTinh= '" + gioitinh + "' where MaNV='" + maNV + "' ";

            thucthisql(strSua);
            loaddgv_phikn();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng khồng?,", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            return;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string manv = txtMaNV.Text;
            string tennv = txtTenNV.Text;
            string socmnd = txtSoCMND.Text;
            string chuyenmon = txtChuyenMon.Text;
            int mucluong = int.Parse(txtMucLuong.Text);
            string sdt = txtsdt.Text;
            string diachi = txtDiaChi.Text;
            string gioitinh = txtGioiTinh.Text;

            //int dg = int.Parse(txtDonGia.Text.Replace(",", ""));

            string sql = " Insert into QLNV values ('" + manv + "', N'" + tennv + "', '" + sdt + "', N'" + diachi + "', '" + socmnd + "', N'" + chuyenmon + "', '" + mucluong + "', '" + gioitinh + "')";
            if (kiemtramatrung(txtMaNV.Text) == 1)
            {
                MessageBox.Show("Mã sp đã tồn tại, bạn hãy nhập lại mã khác!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else
            {
                thucthisql(sql);
                loaddgv_phikn();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).Text = "";
                }
                if (ctrl is ComboBox)
                {
                    (ctrl as ComboBox).Text = "";
                }
                if (ctrl is MaskedTextBox)
                {
                    (ctrl as MaskedTextBox).Text = "";
                }

            }
            loaddgv_phikn();
            txtMaNV.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa = "delete from QLNV where MaNV='" + txtMaNV.Text + "' ";
            DialogResult result = MessageBox.Show("Bạn có muốn xoá không ? ,", "Xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                thucthisql(xoa);
                loaddgv_phikn();
            }
            return;
        }

        private void txtChuyenMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtMucLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSoCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMucLuong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
