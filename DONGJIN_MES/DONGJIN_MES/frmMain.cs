using DarkSoul.KeyEncryption;
using DONGJIN_MES.Class;
using DONGJIN_MES.IUserControl;
using MES_IO.Class;
using Newtonsoft.Json;
using PLCMonitoring.Class;
using System;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using AutoUpdaterDotNET;

namespace DONGJIN_MES
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel2.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        }
        private void IniLayout()
        {
            panel2.Controls.Add(new Main(this) { Dock = DockStyle.Fill });
        }

        private void frmMain1_Load(object sender, EventArgs e)
        {
            try
            {
                //CreateXml("1.0.0.1", Application.StartupPath + "\\Update\\file.zip", " ");
                
                //AutoUpdater.Start(Application.StartupPath + "\\Update\\update.xml");
                //using (StreamReader rd = new StreamReader(StaticSetting.PathSetting))
                //{
                //    var setting = JsonConvert.DeserializeObject<UserSetting>(rd.ReadToEnd());
                //    StaticSetting.Connection = DataEncryption.Decrypt(setting.Connection, DataEncryption.gMD5Hash);
                //    DataLayer dataLayer = new DataLayer(StaticSetting.Connection);
                //    var getVersion = dataLayer.CheckVersion(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                //    if (getVersion.Rows.Count > 0)
                //    {
                //        if(getVersion.Rows[0]["i_version"].ToString() != Application.ProductVersion)
                //        {
                //            CreateXml(getVersion.Rows[0]["i_version"].ToString(), getVersion.Rows[0]["i_url"].ToString(),string.Empty);
                //            AutoUpdater.ShowSkipButton = false;
                //            AutoUpdater.ShowRemindLaterButton=false;
                //            AutoUpdater.UpdateMode=Mode.ForcedDownload;
                //            AutoUpdater.ClearAppDirectory=false;
                //            AutoUpdater.Start(Application.StartupPath + "\\Update\\update.xml");
                //        }
                //    }
                //}
                IniLayout();
                StaticSetting.MacAddress = GetMac();
            }catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void CreateXml(string ver,string url,string log)
        {
            if (!Directory.Exists(Application.StartupPath + "\\Update")) Directory.CreateDirectory(Application.StartupPath + "\\Update");
            if (!File.Exists(Application.StartupPath + "\\Update\\update.xml")) File.Create(Application.StartupPath + "\\Update\\update.xml");

            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement element1 = doc.CreateElement(string.Empty, "item", string.Empty);
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement(string.Empty, "version", string.Empty);
            XmlText text1 = doc.CreateTextNode(ver);
            element1.AppendChild(element2);
            element2.AppendChild(text1);

            XmlElement element3 = doc.CreateElement(string.Empty, "url", string.Empty);
            XmlText text2 = doc.CreateTextNode(url);
            element1.AppendChild(element3);
            element3.AppendChild(text2);

            XmlElement element4 = doc.CreateElement(string.Empty, "changelog", string.Empty);
            XmlText text3 = doc.CreateTextNode(log);
            element1.AppendChild(element4);
            element4.AppendChild(text3);

            XmlElement element5 = doc.CreateElement(string.Empty, "mandatory", string.Empty);
            XmlText text4 = doc.CreateTextNode("true");
            element1.AppendChild(element5);
            element5.AppendChild(text4);
            doc.Save(Application.StartupPath + "\\Update\\update.xml");
        }
        private string GetMac()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    string mac = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                    if(!string.IsNullOrEmpty(mac)) return mac.Replace('-', ':');
                }
            }
            return string.Empty;
        }
        
        public void ToMain()
        {
            foreach (Control control in panel2.Controls)
            {
                control.Invoke(new MethodInvoker(() => {  control.Dispose(); }));
            }
            panel2.Controls.Add(new Main(this) { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) });
            foreach (var item in SerialPortConnection.SpPort)
            {
                if (item.IsOpen) item.Close();
            }
            foreach (var item in SerialPortConnection.Timers)
            {
                if (item.Enabled) item.Stop();
            }
        }
        public void Logout()
        {
            //foreach (Control control in panel2.Controls)
            //{
            //    control.Dispose();
            //}
            //panel2.Controls.Add(new LoginV2() { Dock = DockStyle.Fill });
            //UserSettings.Reset();
            this.Hide();
            LoginV2 frm=new LoginV2();
            frm.Show();
        }
        public void ToProductionResult()
        {
            foreach (Control control in panel2.Controls)
            {
                control.Dispose();
            }
            panel2.Controls.Add(new ProductionResult(this) { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) });
        }
        //private void SearchScreen()
        //{
        //    foreach (var item in SerialPortConnection.SpPort)
        //    {
        //        if (item.IsOpen) item.Close();
        //    }
        //    foreach (Control control in panel2.Controls)
        //    {
        //        control.Dispose();
        //    }
        //    panel2.Controls.Add(new ProductionResult() { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) });
        //    SearchVisible(true);
        //    txtSearchScreen.Text = string.Empty;
        //}

        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(size.Width * size.Width), Convert.ToInt32(size.Height * size.Height));
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            return bm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMaximun_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Maximized)? FormWindowState.Normal: FormWindowState.Maximized;
        }

        private void btnMinimun_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //SearchScreen();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ToMain();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        public void HideSearch(bool hide)
        {
            //txtSearchScreen.Visible = btnSearch.Visible = hide;
        }

        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
