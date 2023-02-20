using System;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //https://github.com/ststeiger/PdfSharpNetStandard
            //https://gunnarpeipman.com/no-data-is-available-for-encoding/
            //https://chsamii.medium.com/no-data-is-available-for-encoding-1252-8bc14651d631
            //to resolve the error No data is available for encoding 1252
            //while calling the method doc.Save
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Image MyImage = Image.FromFile("test.tif");

            PdfDocument doc = new PdfDocument();
            var imgFrameCount = MyImage.GetFrameCount(FrameDimension.Page);
            for (int PageIndex = 0; PageIndex < imgFrameCount; PageIndex++)
            {
                MyImage.SelectActiveFrame(FrameDimension.Page, PageIndex);
                XImage image = XImage.FromStream(ToStream(MyImage, ImageFormat.Tiff));
                var page = new PdfPage();
                page.Width = XUnit.FromPoint(image.PointWidth);
                page.Height = XUnit.FromPoint(image.PointHeight);
                //if (image.PixelWidth > image.PixelHeight)
                if (image.PointWidth > image.PointHeight)
                {
                    page.Orientation = PageOrientation.Landscape;
                }
                else
                {
                    page.Orientation = PageOrientation.Portrait;
                }
                doc.Pages.Add(page);
                XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[PageIndex]);
                xgr.DrawImage(image, 0, 0);
                //string destinaton = Path.GetFullPath(d.Path).Replace(".tif", ".pdf");
                doc.Save("test.pdf");
                doc.Close();
                MyImage.Dispose();
            }
        }
        public static Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;

            return stream;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
      
    }
}