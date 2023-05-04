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
using System.Data.OleDb;

namespace Cửa_hàng_xe_máy
{
    public partial class Quản_Lý_Nhà_Cung_Cấp : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
        public Quản_Lý_Nhà_Cung_Cấp()
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
            cmd = new SqlCommand("Select * from QLNCC", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvQLNCC.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
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
            da = new SqlDataAdapter("Select * from QLNCC", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvQLNCC.DataSource = dt;
            dgvQLNCC.Columns[0].HeaderText = "Mã NCC";
            dgvQLNCC.Columns[1].HeaderText = "Mã Loại SP";
            dgvQLNCC.Columns[2].HeaderText = "Tên NCC";
            dgvQLNCC.Columns[3].HeaderText = "SĐT";
            dgvQLNCC.Columns[4].HeaderText = "Địa Chỉ";
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
        private void Quản_Lý_Nhà_Cung_Cấp_Load(object sender, EventArgs e)
        {
            loadcbb_phikn();
            loaddgv_phikn();
        }


        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }



        private void dgvQLNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//lấy hàng đang chọn
            //chuyển dữ liệu lên các textbox tương ứng
            txtMaNCC.Text = dgvQLNCC[0, i].Value.ToString();//truy vấn đến ô dữ liệu
            cbbMaLoai.Text = dgvQLNCC[1, i].Value.ToString();

            txtTenNCC.Text = dgvQLNCC[2, i].Value.ToString();
            txtDiaChiNCC.Text = dgvQLNCC[4, i].Value.ToString();
            txtSDTNCC.Text = dgvQLNCC[3, i].Value.ToString();

            txtMaNCC.Enabled = false;
        }








        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {

            //txtMaSP.Clear();
            //txtTenSP.Text = "";
            //txtMoTa.Text = "";
            //duyệt các phần tử trên grp, nếu nó là textbox -->rỗng
            foreach (Control ctrl in grbNhapTT.Controls)
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
            txtMaNCC.Enabled = true;


        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {

            string maNCC = txtMaNCC.Text;
            string tenNCC = txtTenNCC.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string diachi = txtDiaChiNCC.Text;
            string sdt = txtSDTNCC.Text;
            string sql = " Insert into QLNCC values ('" + maNCC + "', '" + maloai + "', N'" + tenNCC + "', '" + sdt + "', N'" + diachi + "')";
            if (kiemtramatrung(txtMaNCC.Text) == 1)
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


            string maNCC = txtMaNCC.Text;
            string tenNCC = txtTenNCC.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string diachi = txtDiaChiNCC.Text;
            string sdt = txtSDTNCC.Text;
            string strSua = "update QLNCC set MaNCC='" + maNCC + "',MaLoaiSP = '" + maloai + "', TenNCC = '" + tenNCC + "', SDTNCC='" + sdt + "', DiaChiNCC='" + diachi + "' where MaNCC='" + maNCC + "' ";

            thucthisql(strSua);
            loaddgv_phikn();

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {


            string xoa = "delete from QLNCC where MaNCC='" + txtMaNCC.Text + "' ";
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

        private void txtSDTNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void grbNhapTT_Enter(object sender, EventArgs e)
        {

        }
    }
}
