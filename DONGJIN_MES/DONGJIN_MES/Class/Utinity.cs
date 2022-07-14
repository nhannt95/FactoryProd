using DONGJIN_MES.Class;
using DONGJIN_MES.IUserControl.SubSetting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_IO.Class
{
    public static class Utinity
    {
        public static void ShowMsg(string msg,string caption,bool status)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, status ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
        public static class LoginStatus
        {
            public static bool Login=false;
        }
        public static string GetIPLocal()
        {
            var host=Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
        public static void Mgs(MgsLog notice,string msglog)
        {
            MsgBox msg = new MsgBox(notice, msglog);
            msg.ShowDialog();
        }
        private static bool SelectRow(DataGridView dtgw)
        {
            foreach (DataGridViewRow row in dtgw.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    return true;
                }
            }
            return false;
        }
        public static SizeF MeasureString(string text,Font font)
        {
            SizeF size = new SizeF();
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            size = graphics.MeasureString(text, font);
            return size;
        }
    }
}
