using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Cửa_hàng_xe_máy
{
    public partial class Quản_Lý_Khách_Hàng : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";

        void LoadQLKH()
        {



        }

        public Quản_Lý_Khách_Hàng()
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
            cmd = new SqlCommand("Select * from QLKH", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvQLKH.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
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
            da = new SqlDataAdapter("Select * from QLKH", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvQLKH.DataSource = dt;
            // Thay đổi độ rộng
            dgvQLKH.Columns[0].Width = 55;
            dgvQLKH.Columns[1].Width = 55;
            dgvQLKH.Columns[2].Width = 135;
            dgvQLKH.Columns[3].Width = 90;
            dgvQLKH.Columns[4].Width = 80;
            dgvQLKH.Columns[5].Width = 50;
            dgvQLKH.Columns[6].Width = 135;
            dgvQLKH.Columns[7].Width = 135;
            //thay đổi tiêu đề
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
        private void Quản_Lý_Khách_Hàng_Load(object sender, EventArgs e)
        {
            loadcbb_phikn();
            loaddgv_phikn();
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


        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text;
            string tenKH = txtTenKH.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string diachi = txtDiaChi.Text;
            string sdt = txtSDT.Text;
            string ngaysinh = txtNgaySinh.Text;
            string gioitinh = txtGioiTinh.Text;
            string socccd = txtSoCCCD.Text;


            string sql = " Insert into QLKH values ('" + maKH + "', '" + maloai + "', N'" + tenKH + "', '" + sdt + "', '" + ngaysinh + "', N'" + gioitinh + "', N'" + diachi + "', '" + socccd + "')";
            if (kiemtramatrung(txtMaKH.Text) == 1)
            {
                MessageBox.Show("Mã sp đã tồn tại, bạn hãy nhập lại mã khác!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else
            {
                thucthisql(sql);
                loaddgv_phikn();
            }
        }

        private void dgvQLKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//lấy hàng đang chọn
            //chuyển dữ liệu lên các textbox tương ứng
            txtMaKH.Text = dgvQLKH[0, i].Value.ToString();//truy vấn đến ô dữ liệu
            cbbMaLoai.Text = dgvQLKH[1, i].Value.ToString();
            txtTenKH.Text = dgvQLKH[2, i].Value.ToString();
            txtSDT.Text = dgvQLKH[3, i].Value.ToString();
            txtNgaySinh.Text = dgvQLKH[4, i].Value.ToString();
            txtGioiTinh.Text = dgvQLKH[5, i].Value.ToString();
            txtDiaChi.Text = dgvQLKH[6, i].Value.ToString();
            txtSoCCCD.Text = dgvQLKH[7, i].Value.ToString();

            txtMaKH.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
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
            txtMaKH.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text;
            string tenKH = txtTenKH.Text;
            string maloai = cbbMaLoai.SelectedValue.ToString();
            string diachi = txtDiaChi.Text;
            string sdt = txtSDT.Text;
            string ngaysinh = txtNgaySinh.Text;
            string gioitinh = txtGioiTinh.Text;
            string socccd = txtSoCCCD.Text;

            string strSua = "update QLKH set MaKH='" + maKH + "', MaLoaiSP='" + maloai + "', TenKH=N'" + tenKH + "', SĐTKH='" + sdt + "', NgaySinh='" + ngaysinh + "', GioiTinh=N'" + gioitinh + "', DiaChi=N'" + diachi + "', CCCD='" + socccd + "' where MaKh='" + maKH + "' ";

            thucthisql(strSua);
            loaddgv_phikn();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            string xoa = "delete from QLKH where MaKH='" + txtMaKH.Text + "' ";
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

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSoCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
