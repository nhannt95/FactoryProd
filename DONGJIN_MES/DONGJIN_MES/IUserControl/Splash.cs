using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using DONGJIN_MES;
using DONGJIN_MES.IUserControl;

namespace Office2013StyleSplashScreen
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            //tasks.Text = "Starting...";
            Thread.Sleep(3000);
            splashtime.Start();
        }

        public bool Isminimized = false;

      
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    
        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Isminimized = true;
        }
       
        private void close_MouseHover(object sender, EventArgs e)
        {
            close.ForeColor = Color.Silver;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.ForeColor = Color.White;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            minimize.ForeColor = Color.Silver;
        }

        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            minimize.ForeColor = Color.White;
        }
        public void frmNewFormThread()
        {
            var frmNewForm = new LoginV2();
            //if(Isminimized == true)
            //{
            //    frmNewForm.WindowState = FormWindowState.Minimized;
            //}
            //else
            //{
            //    frmNewForm.WindowState = FormWindowState.Maximized;
            //}
            Application.Run(frmNewForm);
        }

        private void splashtime_Tick(object sender, EventArgs e)
        {
            splashtime.Stop();
            
            var newThread = new System.Threading.Thread(frmNewFormThread);

            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
            this.Close();
            
        }


    }
}
