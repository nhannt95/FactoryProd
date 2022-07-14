using DarkSoul.KeyEncryption;
using DONGJIN_MES.Class;
using MES_IO.Class;
using Newtonsoft.Json;
using PLCMonitoring.Class;
using RestSharp;
using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DONGJIN_MES.IUserControl
{
    public partial class LoginV2 : Form
    {
        public UserSetting mUserSetting;
        public DataLayer mDataLayer;
        public LoginV2()
        {
            InitializeComponent();
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
                    StaticSetting.Token = mUserSetting.Token;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginFunction();
            //ShowErrorTip("aaaaaaaaaaa");
        }
        private string mUser = string.Empty;
        private string mPass = string.Empty;
        private bool isStart = false;
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
                    StaticSetting.Token = "Bearer " + APILogin(mUser, SHA512_ComputeHash(mPass, StaticSetting.Key)).Token;
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
                    frmMain frm=new frmMain();
                    frm.Show();
                    this.Hide();
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
                Utinity.Mgs(MgsLog.Error,"Connection Failed! Please contact DThaus team!");
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

        private void LoginV2_Load(object sender, EventArgs e)
        {
            isStart = true;
            GetLanguage();
            ChangeLanguage();
            btnUpdate.TipsText = $"Current version {Application.ProductVersion}";
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
        private void ChangeLanguage()
        {
            if (Language.Languages.Count == 0)
            {
                Utinity.Mgs(MgsLog.Error, "Language does not found!");
                return;
            }
            if (Language.Languages.ContainsKey("002"))
                cbRemember.Text = Language.Languages["002"];
            if (Language.Languages.ContainsKey("003"))
                btnLogin.Text = Language.Languages["003"];
        }

        private void cbbLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cbbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isStart) return;
            StaticSetting.Language = cbbLanguage.Text;
            GetLanguage();
            ChangeLanguage();
        }

        private void LoginV2_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoginFunction();
        }
        private class UserLogin {
            public string email { get; set; }
            public string password { get; set; }
        }
        private class APIToken {
            public bool Status { get; set; }
            public string Token { get; set; }
        }
        private class Token {
            public string token { get; set; }
        }
        private APIToken APILogin(string user,string pass) {
            try {
                APIToken apiToken = new APIToken();
                var client = new RestClient("http://dthaus-api-317058744.ap-southeast-1.elb.amazonaws.com/api/rest/authentication/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("Accept-Language", "en,vi;q=0.9,en-US;q=0.8");
                request.AddHeader("Authorization", "Bearer null");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Type", "application/json;charset=UTF-8");
                request.AddHeader("FeatureCode", "user.create");
                request.AddHeader("Origin", "http://dthaus.net");
                request.AddHeader("Pragma", "no-cache");
                request.AddHeader("Referer", "http://dthaus.net/");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
                var body = JsonConvert.SerializeObject(new UserLogin { email = user, password = pass });
                request.AddParameter("application/json;charset=UTF-8", body, ParameterType.RequestBody);
                
                var result= client.Execute(request);
                apiToken.Status = result.IsSuccessful;
                apiToken.Token = JsonConvert.DeserializeObject<Token>(result.Content).token;
                return apiToken;
            } catch(Exception ex) {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
                return null;
            }
        }

        private void lbDatabase_Click(object sender, EventArgs e)
        {
            Database frm=new Database(this);
            frm.Show();
        }
        public bool SaveDatabaseChange()
        {
            try
            {
                mDataLayer = new DataLayer(StaticSetting.Connection);
                mUserSetting.Connection = DataEncryption.Encrypt(StaticSetting.Connection, DataEncryption.gMD5Hash);
                using(StreamWriter wrt=new StreamWriter(StaticSetting.PathSetting))
                {
                    wrt.WriteLine(JsonConvert.SerializeObject(mUserSetting));
                    wrt.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var getVersion = new DataLayer(StaticSetting.Connection).UpdateVersion(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                if(getVersion.Rows.Count == 0)
                {
                    Utinity.Mgs(MgsLog.Error, "Application is not found update version!");
                    return;
                }
                else
                {
                    if(getVersion.Rows[0]["i_version"].ToString() == Application.ProductVersion)
                    {
                        Utinity.Mgs(MgsLog.Information, "This is last version!");
                        return;
                    }
                    else
                    {
                        CreateXml(getVersion.Rows[0]["i_version"].ToString(), getVersion.Rows[0]["i_url"].ToString(), getVersion.Rows[0]["i_modify"].ToString());
                        AutoUpdaterDotNET.AutoUpdater.Start(Application.StartupPath+ "\\Update\\update.xml");
                        AutoUpdaterDotNET.AutoUpdater.ShowSkipButton = false;
                        AutoUpdaterDotNET.AutoUpdater.UpdateMode = AutoUpdaterDotNET.Mode.Forced;

                    }
                }
            }catch(Exception ex)
            {

            }
        }
        private void CreateXml(string ver, string url, string log)
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
            XmlAttribute attribute = doc.CreateAttribute("mode");
            attribute.Value = "2";
            element5.Attributes.Append(attribute);
            XmlText text4 = doc.CreateTextNode("true");
            element1.AppendChild(element5);
            element5.AppendChild(text4);
            doc.Save(Application.StartupPath + "\\Update\\update.xml");
        }
    }
}
