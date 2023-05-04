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
    public partial class Loại_Xe : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string chuoikn = @"Data Source=LAPTOP-B8V4R50K\SQLEXPRESS;Initial Catalog=QL_Ban_Xe;Integrated Security=True";
        public Loại_Xe()
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
            cmd = new SqlCommand("Select * from LoaiXe", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgvMaLoai.Rows.Add(dr[0], dr[1], dr[2]);
            }
            dongkn();
        }
        void loaddgv_phikn()
        {
            ketnoi();
            da = new SqlDataAdapter("Select * from LoaiXe", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvMaLoai.DataSource = dt;
            dgvMaLoai.Columns[0].HeaderText = "Ma Loại SP";
            dgvMaLoai.Columns[1].HeaderText = "Tên Loại SP";
            dgvMaLoai.Columns[2].HeaderText = "Ghi Chú";

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
            string sql = "Select count(*) from LoaiXe where MaLoaiSP='" + ma.Trim() + "'";
            cmd = new SqlCommand(sql, con);
            i = (int)(cmd.ExecuteScalar());
            dongkn();
            return i;
        }




        private void Loại_Xe_Load(object sender, EventArgs e)
        {
            loaddgv_phikn();

        }








        private void dgvMaLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;//lấy hàng đang chọn
            //chuyển dữ liệu lên các textbox tương ứng
            txtMaLoai.Text = dgvMaLoai[0, i].Value.ToString();//truy vấn đến ô dữ liệu
            txtTenLoaiXe.Text = dgvMaLoai[1, i].Value.ToString();

            txtGhiChu.Text = dgvMaLoai[2, i].Value.ToString();

            txtMaLoai.Enabled = false;
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
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
            }
            loaddgv_phikn();
            txtMaLoai.Enabled = true;

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string xoa = "delete from LoaiXe where MaLoaiSP='" + txtMaLoai.Text + "' ";
            DialogResult result = MessageBox.Show("Bạn có muốn xoá không ? ,", "Xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                thucthisql(xoa);
                loaddgv_phikn();
            }
            return;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string maloai = txtMaLoai.Text;
            string tenmaloai = txtTenLoaiXe.Text;
            string ghichu = txtGhiChu.Text;

            string sql = " Insert into LoaiXe values ('" + maloai + "', N'" + tenmaloai + "', N'" + ghichu + "')";
            if (kiemtramatrung(txtMaLoai.Text) == 1)
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
            string maloai = txtMaLoai.Text;
            string tenmaloai = txtTenLoaiXe.Text;
            string ghichu = txtGhiChu.Text;
            string strSua = "update LoaiXe set MaLoaiSP='" + maloai + "',TenMaLoaiSP = N'" + tenmaloai + "', GhiChu = N'" + ghichu + "' where MaLoaiSP='" + maloai + "' ";

            thucthisql(strSua);
            loaddgv_phikn();
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
