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


namespace Cửa_hàng_xe_máy
{
    public partial class Quản_Lý_Xe : Form
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
        void loadcbb()//kiến trúc kết nối: connect, command, datareader
        {
            //bước 1: mở kết nối
            ketnoi();
            //bước 2: khởi tạo đối tượng cmd
            //cmd = new SqlCommand();
            //cmd.CommandText = "Select * from LoaiSP";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = con;
            //string sql = "Select * from LoaiSP";
            cmd = new SqlCommand("Select * from MaLoaiSP", con);
            //Bước 3: gọi pt excutereader, gán kq trả về cho dr
            dr = cmd.ExecuteReader();
            //bước 4: đọc dữ liệu trên dr
            while (dr.Read())
            {
                //thêm dữ liệu vào cbb
                cbbMaLoai.Items.Add(dr[1].ToString());
            }
            //đóng kết nối
            dongkn();
        }
        void loaddgv()
        {
            ketnoi();
            cmd = new SqlCommand("Select * from QLXe", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvQLX.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            }
            dongkn();
        }
        void loadcbb_phikn()
        {
            ketnoi();
            //khởi tạo đt adapter giống cmd
            da = new SqlDataAdapter("Select *from LoaiXe", con);
            //khởi tạo đối tượng datatable
            dt = new DataTable();
            //điền dữ liệu cho datatable -->tạo bản sao
            da.Fill(dt);
            //đổ dl ra cbb
            cbbMaLoai.DataSource = dt;
            //cần chỉ định thêm đổ dữ liệu ở cột nào
            cbbMaLoai.DisplayMember = "TenMaLoaiSP";
            cbbMaLoai.ValueMember = "MaLoaiSP";
        }
        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from QLXe", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvQLX.DataSource = dt;
            // Thay đổi độ rộng
            dgvQLX.Columns[0].Width = 35;
            dgvQLX.Columns[1].Width = 35;
            dgvQLX.Columns[2].Width = 70;
            dgvQLX.Columns[3].Width = 35;
            dgvQLX.Columns[4].Width = 50;
            dgvQLX.Columns[5].Width = 200;


            //thay đổi tiêu đề
            dgvQLX.Columns[0].HeaderText = "Mã SP";
            dgvQLX.Columns[1].HeaderText = "Mã Loại SP";
            dgvQLX.Columns[2].HeaderText = "Tên Xe";
            dgvQLX.Columns[3].HeaderText = "Số Lượng";
            dgvQLX.Columns[4].HeaderText = "Màu Sắc";
            dgvQLX.Columns[5].HeaderText = "Giá Thành";
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
        private void Quản_Lý_Xe_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();
            loadcbb_phikn();

        }
        public Quản_Lý_Xe()
        {
            InitializeComponent();
        }









        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }













        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            //if (txtDonGia.Text == "" || txtDonGia.Text == "0") return;
            //decimal number;
            //number = decimal.Parse(txtDonGia.Text, System.Globalization.NumberStyles.Currency);
            //txtDonGia.Text = number.ToString("#,#");
            //txtDonGia.SelectionStart = txtDonGia.Text.Length;
        }

        private void dgvQLX_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//lấy hàng đang chọn
            //chuyển dữ liệu lên các textbox tương ứng
            txtMaXe.Text = dgvQLX[0, i].Value.ToString();//truy vấn đến ô dữ liệu
            cbbMaLoai.Text = dgvQLX[1, i].Value.ToString();

            txtTenXe.Text = dgvQLX[2, i].Value.ToString();
            txtSoLuong.Text = dgvQLX[3, i].Value.ToString();
            txtMauSac.Text = dgvQLX[4, i].Value.ToString();
            txtDonGia.Text = dgvQLX[5, i].Value.ToString();

            txtMaXe.Enabled = false;
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {

            foreach (Control ctrl in gtbNhapTT.Controls)
            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).Text = "";
                }
                if (ctrl is ComboBox)
                {
                    (ctrl as ComboBox).Text = "";
                }
            }
            loaddgv_phikn();
            txtMaXe.Enabled = true;

        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {

            string maXe = txtMaXe.Text;
            string tenXe = txtTenXe.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string mausac = txtMauSac.Text;
            int soluong = int.Parse(txtSoLuong.Text);
            int dg = int.Parse(txtDonGia.Text);

            //int dg = int.Parse(txtDonGia.Text.Replace(",", ""));

            string sql = " Insert into QLXe values ('" + maXe + "', '" + maloai + "', N'" + tenXe + "', '" + soluong + "', N'" + mausac + "', '" + dg + "')";
            if (kiemtramatrung(txtMaXe.Text) == 1)
            {
                MessageBox.Show("Mã sp đã tồn tại, bạn hãy nhập lại mã khác!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else
            {
                thucthisql(sql);
                loaddgv_phikn();
            }

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {

            string maXe = txtMaXe.Text;
            string tenXe = txtTenXe.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string mausac = txtMauSac.Text;
            int soluong = int.Parse(txtSoLuong.Text);
            int dg = int.Parse(txtDonGia.Text);

            //int dg = int.Parse(txtDonGia.Text.Replace(",", ""));

            string strSua = "update QLXe set MaSP='" + maXe + "',MaLoaiSP = '" + maloai + "', TenSP = N'" + tenXe + "', SoLuong='" + soluong + "', MauSac=N'" + mausac + "', GiaThanh='" + dg + "' where MaSP='" + maXe + "' ";

            thucthisql(strSua);
            loaddgv_phikn();


        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {

            string xoa = "delete from QLXe where MaSP='" + txtMaXe.Text + "' ";
            DialogResult result = MessageBox.Show("Bạn có muốn xoá không ? ,", "Xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                thucthisql(xoa);
                loaddgv_phikn();
            }
            return;

        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng khồng?,", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            return;

        }
    }
}
