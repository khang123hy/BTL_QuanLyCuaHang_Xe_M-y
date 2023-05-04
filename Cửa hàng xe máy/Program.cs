using System;
using System.Windows.Forms;

namespace Cửa_hàng_xe_máy
{
    internal static class Program
    {
        public static int code = 2;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Đăng_Nhập());
        }
    }
}
