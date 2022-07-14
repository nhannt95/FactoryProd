using DONGJIN_MES.Class;
using PLCMonitoring.Class;
using System;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl
{
    public partial class Database : Form
    {
        LoginV2 mLogin;
        private Databases dtb;
        public Database(LoginV2 login)
        {
            InitializeComponent();
            mLogin = login;
            UIActive(false);
            try
            {
                if (string.IsNullOrEmpty(StaticSetting.Connection)) return;
                string[] con= StaticSetting.Connection.Split(';');
                dtb = new Databases();
                foreach(string s in con)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s.StartsWith("Server")) dtb.Server = s.Substring(7);
                        else if (s.StartsWith("Port")) dtb.Port = s.Substring(5);
                        else if (s.StartsWith("Database")) dtb.Database = s.Substring(9);
                        else if (s.StartsWith("Uid")) dtb.Uid = s.Substring(4);
                        else if (s.StartsWith("Pwd")) dtb.Pwd = s.Substring(4);
                    }
                }
                txtHost.Text = dtb.Server;
                txtDatabase.Text = dtb.Database;
                txtPort.Text = dtb.Port;
                txtUsername.Text = dtb.Uid;
                txtPass.Text = dtb.Pwd;
            }catch (Exception ex)
            {

            }
        }
        public class Databases
        {
            public string Server { get; set; }
            public string Port { get; set; }
            public string Database { get; set; }
            public string Uid { get; set; }
            public string Pwd { get; set; }
        }
        private void UIActive(bool active)
        {
            txtHost.Enabled=txtDatabase.Enabled=txtPort.Enabled=txtUsername.Enabled=txtPass.Enabled=btnTest.Enabled=btnSave.Enabled= active;
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            if(txtUserAdmin.Text=="Admin" && txtPassWordAdmin.Text == "Dongjin@2022")
            {
                UIActive(true);
                txtPassWordAdmin.Text=txtUserAdmin.Text=string.Empty;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtHost.Text)|| string.IsNullOrEmpty(txtDatabase.Text) || string.IsNullOrEmpty(txtPort.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Please fill all data!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                StaticSetting.Connection = $"Server={txtHost.Text};Port={txtPort.Text};Database={txtDatabase.Text};Uid={txtUsername.Text};Pwd={txtPass.Text};";
                string con = $"Server={txtHost.Text};Port={txtPort.Text};Database={txtDatabase.Text};Uid={txtUsername.Text};Pwd={txtPass.Text};";
                DataLayer dtl = new DataLayer(con);
                dtl.m_cnn.Open();
                MessageBox.Show("Connect successfully!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                dtl.m_cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHost.Text) || string.IsNullOrEmpty(txtDatabase.Text) || string.IsNullOrEmpty(txtPort.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Please fill all data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string con = $"Server={txtHost.Text};Port={txtPort.Text};Database={txtDatabase.Text};Uid={txtUsername.Text};Pwd={txtPass.Text};";
                StaticSetting.Connection = con;
                if (mLogin.SaveDatabaseChange())
                {
                    MessageBox.Show("Save success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Save failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassWordAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtUserAdmin.Text == "Admin" && txtPassWordAdmin.Text == "Dongjin@2022")
                {
                    UIActive(true);
                    txtPassWordAdmin.Text = txtUserAdmin.Text = string.Empty;
                }
            }
        }
    }
}
