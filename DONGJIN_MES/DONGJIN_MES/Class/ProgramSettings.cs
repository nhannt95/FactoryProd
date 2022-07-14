using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace DONGJIN_MES.Class
{
    public class ProgramSettings
    {
        public string ScreenID { get; set; }
        public string ScreenName { get; set; }
        public string IpAddress { get; set; }
        public string ShortScreen { get; set; }
        public LineInfox LineInfos = new LineInfox();
        public List<SerialPortx> SerialPorts = new List<SerialPortx>();
        public List<PLCInfox> PLCInfos = new List<PLCInfox>();
        public List<IOInfox> IOInfos = new List<IOInfox>();
        public List<SoundInfox> SoundInfos = new List<SoundInfox>();
        public List<Printerx> Printers = new List<Printerx>();
        public List<UnitIDx> UnitIDs = new List<UnitIDx>();
        public object Clone()
        {
            return this;
        }
    }

    public class SerialPortx
    {
        public string Device { get; set; }
        public string Comport { get; set; }
        public int BaudRate { get; set; }
        public string Parity { get; set; }
        public int StopBit { get; set; }
        public int DataBis { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Remark { get; set; }

    }
    public static class SerialPortConnection
    {
        public static List<SerialPort> SpPort = new List<SerialPort>();
        public static List<System.Windows.Forms.Timer> Timers = new List<System.Windows.Forms.Timer>();
    }
    public class LineInfox
    {
        public string Factory { get; set; }
        public string FactoryCode { get; set; }
        public string LineName { get; set; }
        public string LineCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public string WSName { get; set; }
        public string WSCode { get; set; }
        public string AutoPO { get; set; }
        public bool ScanSerial { get; set; } = true;
        public string Input { get; set; }
        public string Final { get; set; }
        public string BarcodeYN { get; set; }
        public string Reflect { get; set; } = "N";
    }
    public class PLCInfox
    {
        public string LineType { get; set; }
        public string PLCEvent { get; set; }
        public string Series { get; set; }
    }
    public class IOInfox
    {
        public string Chanel { get; set; }
        public string Event { get; set; }
        public string ONOFF { get; set; }
        public string Maker { get; set; }
        public string Remark { get; set; }
        public int Delay { get; set; }
    }
    public class SoundInfox
    {
        public string Item { get; set; }
        public string Path { get; set; }
    }
    public class Printerx
    {
        public string LabelBox { get; set; }
        public string LabelDetail { get; set; }
        public string PrinterName { get; set; }
        public string Remark { get; set; }
    }
    public class UnitIDx
    {
        public string UnitIDs { get; set; }
        public string UnitName { get; set; }
    }
    public class UserSetting
    {
        public string User { get; set; }
        public bool Remember { get; set; }
        public string Connection { get; set; }
        public string Language { get; set; }
        public string Token { get; set; }
    }
    public static class StaticSetting
    {
        public static string PathSetting = Application.StartupPath + "\\Settings.json";
        public static string PathLog = Application.StartupPath + "\\Log";
        public static string PathConfig = Application.StartupPath + "\\Configuration.json";
        public static string PathConfigPrinter = Application.StartupPath + "\\PrintSettings.json";
        public static string Key = "DTH@us-L1m1ted-C0mp@ny-Vietnammm";
        public static string Connection = string.Empty;
        public static string IP { get; set; }
        public static string ScreenID { get; set; }
        public static string ScreenShortName { get; set; }
        public static string Line = string.Empty;
        public static string MacAddress = string.Empty;
        public static string AutoPO = string.Empty;
        public static string Language = string.Empty;
        public static string FactoryCode = string.Empty;
        public static string Token = string.Empty;
    }
    public static class UserSettings
    {
        public static string User { get; set; }
        public static string FullName { get; set; }
        public static string Email { get; set; }
        public static string ICode { get; set; }
        public static string Permision { get; set; }
        public static string FactoryCode { get; set; }
        public static bool IsLogin = false;
        public static void Reset()
        {
            User = FullName = Email = ICode = Permision = FactoryCode = string.Empty;
            IsLogin = false;
        }
    }
    public class SetInfo {
        public string UnitID { get; set; }
        public string LabelBox { get; set; }
        public string PO { get; set; }
        public string Serial { get; set; }
        public string Line { get; set; }
        public string LineName { get; set; }
        public string LotNo { get; set; }
        public string Factory { get; set; }
        public string WSCode { get; set; }
        public int ModelID { get; set; }
        public string Model { get; set; }
        public string GRNo { get; set; }
        public string ModelName { get; set; }
        public int Plan { get; set; }
        public int Actual { get; set; } = 0;
        public int ActualPS { get; set; } = 0;
        public int Bal { get; set; }
        public int TactTime { get; set; }
        public int PackingSize { get; set; } = 0;
        public int POSize { get; set; } = 0;
        public ItemMode ItemModes;
        public Image QR;
        public string PrinterName { get; set; }
        public int LabelWidth { get; set; }
        public int LabelHeight { get; set; }
        public string Remark { get; set; }
        public string LineSerial { get; set; }
        public string Reason { get; set; }
        public int Verison { get; set; }
        public string State { get; set; }
        public string ProcessCode { get; set; }
        public string Input { get; set; }
        public string Final { get; set; }
        public string POType { get; set; }
        public string Reflect { get; set; }
        public string ProdGRId { get; set; }
        public string PlanDate { get; set; }
    }
    public enum ItemMode
    {
        PACKING_SIZE,
        PO_SIZE
    }
    public enum AutoPO
    {
        Y,
        N
    }
    public class LineQty
    {
        public string FactoryCode { get; set; }
        public string PlanDate { get; set; }
        public string LineCode { get; set; }
        public string ModelCode { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string PO { get; set; }
        public string GRNo { get; set; }
        public int TactTime { get; set; }
        public int Plan { get; set; }
        public int Actual { get; set; }
        public int Bal { get; set; }
        public int Version { get; set; }
        public string State { get; set; }
        public string ProcessCode { get; set; }
        public string POType { get; set; }
        public string LotNo { get; set; }
        public string ProdGRId { get; set; }

    }
    public class PrintSetting
    {
        public int PaperWidth { get; set; }
        public int PaperHeight { get; set; }
        public int MarginLeft { get; set; }
        public int MarginTop { get; set; }
        public int DistanceQRText { get; set; }
        public int DistanceLine { get; set; }
        public int Font { get; set; }
        public int QRSize { get; set; }
        public decimal DtgwSize { get; set; }
        public decimal LogSize { get; set; }
    }
    public static class Language
    {
        public static Dictionary<string, string> Languages = new Dictionary<string, string>();
    }
    public enum MgsLog
    {
        Confirm,
        Success,
        Information,
        Error,
        LoginFailed,
        Warning,
        Notice
    }
}
