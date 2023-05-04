using CrystalDecisions.CrystalReports.Engine;
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
    public partial class Thống_kê_ncc : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt; //có thể khai báo DataSet
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


      
 

       
        public Thống_kê_ncc()
        {
            InitializeComponent();
        }

        private void cbbMaLoai_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ketnoi();
            string ma = cbbMaLoai.SelectedValue.ToString();
            da = new SqlDataAdapter("Select * from QLNCC where MaLoaiSP='" + ma.Trim() + "'", con);
            dt = new DataTable();
            da.Fill(dt);
            dgvThongKe.DataSource = dt;
        }

        private void Thống_kê_ncc_Load(object sender, EventArgs e)
        {
            loadcbb();
        }

        private void btnXuat_Click_1(object sender, EventArgs e)
        {
            ketnoi();
            string ma = cbbMaLoai.SelectedValue.ToString();
            da = new SqlDataAdapter("Select * from QLNCC where MaLoaiSP='" + ma.Trim() + "'", con);
            dt = new DataTable();
            da.Fill(dt);
            //khởi tạo đối tượng report
            CrQLNCC rpt = new CrQLNCC();
            //gán mã loại vào trong report
            ((TextObject)rpt.ReportDefinition.ReportObjects["txtMaLoai"]).Text = ma;
            //thêm dữ liệu vào report
            rpt.SetDataSource(dt);
            //làm mới report-->để rpt rỗng
            rpt.Refresh();
            //khởi tạo đối tượng form chứa report
            In_ra frm = new In_ra();
            frm.crystalReportViewer1.ReportSource = rpt;//đổ dữ liệu từ dt
            frm.ShowDialog();
            dongkn();
        }
    }
}
