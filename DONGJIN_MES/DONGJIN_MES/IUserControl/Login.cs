using MES_IO.Class;
using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using DONGJIN_MES.Class;
using PLCMonitoring.Class;
using DarkSoul.KeyEncryption;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace DONGJIN_MES.IUserControl
{
    public partial class Login : UserControl
    {
        private frmMain frmMain;
        public UserSetting mUserSetting;
        private DataLayer mDataLayer;
        private Rectangle lbRect;
        private Rectangle txt1Rect;
        private Rectangle txt2Rect;
        private Rectangle formRect;
        private Rectangle cbRect;
        private Rectangle linkRect;
        private Rectangle btnLogin1;
        private Rectangle btnSign1;
        private Rectangle cbbLanguage1;

        private float lbFontSize;
        private float txt1FontSize;
        private float txt2FontSize;
        private float cbFontSize;
        private float linkFontSize;
        private float btnLoginFontSize;
        private float btnSignFontSize;
        private float cbbLanguage2;
        private float fontScale = 1f;
        private string mUser = string.Empty;
        private bool isStart = false;
        public Login(frmMain frm)
        {
            InitializeComponent();
            frmMain = frm;
            try
            {
                cbbLanguage.SelectedIndex = 0;
                if (!File.Exists(StaticSetting.PathSetting))
                {
                    Application.Exit();
                }
                mUserSetting = new UserSetting();
                using (StreamReader rd = new StreamReader(StaticSetting.PathSetting))
                {
                    mUserSetting = JsonConvert.DeserializeObject<UserSetting>(rd.ReadToEnd());
                    StaticSetting.Language = mUserSetting.Language;
                    cbbLanguage.Text = mUserSetting.Language;
                    StaticSetting.Connection = DataEncryption.Decrypt(mUserSetting.Connection, DataEncryption.gMD5Hash);
                    mDataLayer = new DataLayer(StaticSetting.Connection);
                }
                if (mUserSetting.Remember)
                {
                    txtUser.Text = mUserSetting.User;
                    cbRemember.Checked = mUserSetting.Remember;
                }
                
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            //ResizeChildrenContrtol();
        }
        private void Ini()
        {
            formRect = new Rectangle(this.Location, this.Size);
            lbRect = new Rectangle(lbTitle.Location, lbTitle.Size);
            txt1Rect = new Rectangle(txtUser.Location, txtUser.Size);
            txt2Rect = new Rectangle(txtPass.Location, txtPass.Size);
            cbRect = new Rectangle(cbRemember.Location, cbRemember.Size);
            linkRect = new Rectangle(lbForgot.Location, lbForgot.Size);
            btnLogin1 = new Rectangle(btnLogin.Location, btnLogin.Size);
            btnSign1 = new Rectangle(btnSign.Location, btnSign.Size);
            cbbLanguage1 = new Rectangle(cbbLanguage.Location, cbbLanguage.Size);

            lbFontSize = lbTitle.Font.Size;
            txt1FontSize = txtUser.Font.Size;
            txt2FontSize = txtPass.Font.Size;
            cbFontSize = cbRemember.Font.Size;
            linkFontSize = lbForgot.Font.Size;
            btnLoginFontSize = btnLogin.Font.Size;
            btnSignFontSize = btnSign.Font.Size;
            cbbLanguage2 = cbbLanguage.Font.Size;
        }
        private float height = 0;
        //Screen.PrimaryScreen.Bounds.Width
        private void ResizeControl(Control control, Rectangle rec, float fontSize, FontStyle fontStyle)
        {
            //if(formRect.Width==0) formRect.Width = 1300;
            //if(formRect.Height==0) formRect.Height = 660;
            float xRatio = (float)this.ClientRectangle.Width / (float)formRect.Width;
            float yRatio = (float)this.ClientRectangle.Height / (float)formRect.Height;
            if (xRatio == 0 && yRatio == 0) return;
            float newX = rec.Location.X * xRatio;
            float newY = rec.Location.Y * yRatio;
            //if (control == cbRemember || control == btnLogin || control == cbbLanguage)
            //{
            //    newY = newY - 60;
            //}
            //if (control == txtPass)
            //{
            //    newY = height + 60;
            //}
            if (control == txtUser)
            {
                height = newY;
            }
            control.Location = new Point((int)newX, (int)newY);
            control.Width = (int)(rec.Width * xRatio);
            if (control == txtUser || control == txtPass || control == cbbLanguage)
            {
                control.Height = (int)(rec.Height);
            }
            else
            {
                control.Height = (int)(rec.Height * yRatio);
            }
            float ratio = xRatio;
            if (xRatio >= yRatio) ratio = yRatio;
            //float fontSizeText = fontSize == 0 ? 12 : fontSize;
            float newFontSize = fontSize * ratio * fontScale;
            if (control == cbbLanguage)
            {
                newFontSize = newFontSize / ratio;
                control.Width = (int)(control.Width / xRatio);
            }
            Font newFont = new Font(control.Font.FontFamily, newFontSize, fontStyle);
            control.Font = newFont;
        }
        private void ResizeChildrenContrtol()
        {
            ResizeControl(lbTitle, lbRect, lbFontSize, FontStyle.Bold);
            ResizeControl(txtUser, txt1Rect, txt1FontSize, FontStyle.Regular);
            ResizeControl(txtPass, txt2Rect, txt2FontSize, FontStyle.Regular);
            ResizeControl(cbbLanguage, cbbLanguage1, cbbLanguage.Font.Size, FontStyle.Regular);
            ResizeControl(cbRemember, cbRect, cbFontSize, FontStyle.Regular);
            ResizeControl(lbForgot, linkRect, linkFontSize, FontStyle.Regular);
            ResizeControl(btnLogin, btnLogin1, btnLoginFontSize, FontStyle.Bold);
            ResizeControl(btnSign, btnSign1, btnSignFontSize, FontStyle.Bold);
        }
        private string mPass = string.Empty;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginFunction();
        }

        private void LoginFunction()
        {
            try
            {
                mUser = txtUser.Text;
                mPass = txtPass.Text;
                if (string.IsNullOrEmpty(mUser))
                {
                    Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("101") ? Language.Languages["101"] : "User name is empty");
                    return;
                }
                if (string.IsNullOrEmpty(mPass))
                {
                    Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("102") ? Language.Languages["102"] : "Password is empty");
                    return;
                }


                var info = LoginCheck(mUser, SHA512_ComputeHash(mPass, StaticSetting.Key));
                if (info.Rows.Count > 0)
                {
                    foreach (DataRow row in info.Rows)
                    {
                        UserSettings.User = mUser;
                        UserSettings.FullName = row["full_name"].ToString();
                        UserSettings.Email = row["email"].ToString();
                        UserSettings.ICode = row["i_code"].ToString();
                        UserSettings.Permision = row["i_usr_log_i"].ToString();
                        UserSettings.FactoryCode = row["i_factory_code"].ToString();
                        UserSettings.IsLogin = true;
                        break;
                    }
                    mUserSetting.User = mUser;
                    mUserSetting.Language = cbbLanguage.Text;
                    if (cbRemember.Checked)
                    {
                        mUserSetting.Remember = true;
                        using (StreamWriter sw = new StreamWriter(StaticSetting.PathSetting))
                        {
                            sw.WriteLine(JsonConvert.SerializeObject(mUserSetting));
                            sw.Close();
                        }
                    }
                    frmMain.ToMain();
                }
                else
                {
                    UserSettings.IsLogin = false;
                    Utinity.Mgs(MgsLog.LoginFailed, Language.Languages.ContainsKey("100") ? Language.Languages["100"] : "Your user name or password is incorrect.");
                    return;
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                Ini();
                isStart = true;
                GetLanguage();
                ChangeLanguage();
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        public void GetLanguage()
        {
            try
            {
                if (!isStart) return;
                if (string.IsNullOrEmpty(StaticSetting.Language)) return;
                var getLanguage = mDataLayer.GetLanguage();
                Language.Languages.Clear();
                foreach (DataRow row in getLanguage.Rows)
                {
                    if (!Language.Languages.ContainsKey(row["i_code"].ToString()))
                    {
                        switch (StaticSetting.Language)
                        {
                            case "English":
                                if (!string.IsNullOrEmpty(row["i_english"].ToString()))
                                    Language.Languages.Add(row["i_code"].ToString(), row["i_english"].ToString());
                                break;
                            case "Viet Nam":
                                if (!string.IsNullOrEmpty(row["i_vietnam"].ToString()))
                                    Language.Languages.Add(row["i_code"].ToString(), row["i_vietnam"].ToString());
                                break;
                            case "Korea":
                                if (!string.IsNullOrEmpty(row["i_korea"].ToString()))
                                    Language.Languages.Add(row["i_code"].ToString(), row["i_korea"].ToString());
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private DataTable LoginCheck(string user, string pass)
        {
            try
            {
                return mDataLayer.CheckUser(user, pass);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string SHA512_ComputeHash(string text, string secretKey)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        private void lbForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This function is updating...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This function is updating...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //private void txtPass_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        LoginFunction();
        //    }
        //}

        private void txtPass_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginFunction();
            }
        }

        private void cbbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isStart) return;
            StaticSetting.Language = cbbLanguage.Text;
            GetLanguage();
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            if (Language.Languages.Count == 0)
            {
                Utinity.Mgs(MgsLog.Error, "Language does not found!");
                return;
            }
                if (Language.Languages.ContainsKey("001"))
            {
                lbTitle.Text = Language.Languages["001"];
            }
            if (Language.Languages.ContainsKey("002"))
                cbRemember.Text = Language.Languages["002"];
            if (Language.Languages.ContainsKey("003"))
                btnLogin.Text = Language.Languages["003"];
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginFunction();
            }
        }
    }
}
