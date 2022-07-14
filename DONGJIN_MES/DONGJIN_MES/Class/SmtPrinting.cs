using DONGJIN_MES.Class;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Samsung_System
{
    public class SmtPrintSetting
    {
        public static int FieldOffSet = 7;
        public static int PaddingLeft = 4;
        public static int PaddingTop = 8;
        internal static int DetailSize = 8;
        internal static int PaddingCenter = 8;
        internal static int HOffSet { get; set; }
        internal static int MasterSize = 19;
        internal static int FontSize = 24;
        internal static int X = 30;
        internal static int Y = 30;
    }


    public class SmtExportLabelSetting
    {
        public static int FieldOffSet = 7;
        public static int PaddingLeft = 0;
        public static int PaddingTop = 8;
        internal static int DetailSize = 8;
        internal static int HOffSet { get; set; }
        internal static int MasterSize = 19;
        internal static int X = 30;
        internal static int Y = 30;
    }
    public class ProductionPrinting
    {
        private PrintDocument mPrintDoc;
        private SetInfo setInfo;
        private PrintSetting printerSetting;
        private PrintDialog mPrintDlg;
        public ProductionPrinting(SetInfo _setInfo, string printerName, PrintSetting pst, PrintDocument printDocument = null)
        {
            setInfo = _setInfo;
            printerSetting = pst;
            mPrintDoc = mPrintDoc == null ? new PrintDocument() : printDocument;
            mPrintDlg = new PrintDialog();
            mPrintDoc.PrinterSettings = mPrintDlg.PrinterSettings;
            mPrintDoc.PrinterSettings.PrinterName = printerName;
            int paper_width = (int)(setInfo.LabelWidth * 3.7795275591);
            int paper_height = (int)(setInfo.LabelHeight * 3.7795275591);

            PaperSize paper = new PaperSize("dm", paper_width, paper_height);
            mPrintDoc.DefaultPageSettings.PaperSize = paper;
            mPrintDoc.PrinterSettings.Copies = 1;
            //mPrintDoc.PrinterSettings.LandscapeAngle = 90;
            mPrintDoc.PrintPage += ProdPrint;

        }
        private void ProdPrint(object sender, PrintPageEventArgs e)
        {
            Font text_font_15 = new Font("Arial Narrow", printerSetting.Font, FontStyle.Regular, GraphicsUnit.Pixel);
            Font text_font_15_B = new Font("Arial Narrow", printerSetting.Font-1, FontStyle.Bold, GraphicsUnit.Pixel);
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            try
            {
                RectangleF rect = new RectangleF(0, 10, e.MarginBounds.X, e.MarginBounds.Y);
                RectangleF marginBounds = e.MarginBounds;
                int width = printerSetting.MarginLeft, height = printerSetting.MarginTop;
                //float startX = ((sender as PrintDocument).DefaultPageSettings.PaperSize.Height;
                //width = (int)startX;
                int kc = printerSetting.DistanceQRText;
                int bet_line = -2;
                e.Graphics.DrawImage(setInfo.QR, width, height);
                width += setInfo.QR.Width + 5;
                height += printerSetting.MarginTop+3;
                e.Graphics.DrawString($"Model:", text_font_15, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Model:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"Line:", text_font_15, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Line:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"Date:", text_font_15, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Date:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"Seq:", text_font_15, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Seq:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"Lot No:", text_font_15, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Lot No:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"{setInfo.LabelBox}", text_font_15_B, new SolidBrush(Color.Black), width, height);
                height = printerSetting.MarginTop + 5;
                width += (int)MesureString("Model:", text_font_15).Width + kc;
                e.Graphics.DrawString($"{setInfo.ModelName}", text_font_15_B, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Model:", text_font_15).Height + bet_line;
                e.Graphics.DrawString($"{setInfo.LineName}", text_font_15_B, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Model:", text_font_15).Height + bet_line;
                e.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd") + "   " + DateTime.Now.ToString("HH:mm"), text_font_15_B, new SolidBrush(Color.Black), width, height);
                height += (int)MesureString("Model:", text_font_15).Height + bet_line;
                e.Graphics.DrawString(setInfo.LabelBox.Substring(setInfo.LabelBox.Length - 4), text_font_15_B, new SolidBrush(Color.Black), width, height);
                e.Graphics.DrawString($"No:", text_font_15, new SolidBrush(Color.Black), width + (int)MesureString(setInfo.LabelBox.Substring(setInfo.LabelBox.Length - 4), text_font_15_B).Width + 20, height);

                int widths = width + 40 + (int)MesureString("No:", text_font_15).Width;

                e.Graphics.DrawString(setInfo.PO, text_font_15_B, new SolidBrush(Color.Black), widths, height);
                height += (int)MesureString("Model:", text_font_15).Height + bet_line;

                e.Graphics.DrawString(setInfo.LabelBox.Length>21?setInfo.LabelBox.Substring(13, 7): setInfo.LabelBox, text_font_15_B, new SolidBrush(Color.Black), width, height);

                e.Graphics.DrawString($"Qty:", text_font_15, new SolidBrush(Color.Black), width + (int)MesureString(setInfo.LabelBox.Substring(setInfo.LabelBox.Length - 4), text_font_15_B).Width + 20, height);
                int qty = setInfo.ItemModes == ItemMode.PACKING_SIZE ? setInfo.ActualPS : setInfo.Actual;
                e.Graphics.DrawString($"{qty}", text_font_15, new SolidBrush(Color.Black), widths, height);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        internal void Print()
        {
            mPrintDoc.DefaultPageSettings.PrinterResolution = mPrintDoc.PrinterSettings.PrinterResolutions[0];
            mPrintDoc.Print();
        }
        private SizeF MesureString(string text, Font font)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    SizeF size = graphics.MeasureString(text, font);
                    return size;
                }
            }
        }
    }
}
