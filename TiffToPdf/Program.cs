using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System.Drawing.Imaging;
using System.Text;
using System.Drawing;


FileInfo? fiTiff = default;

var options = CommandLine.Parser.Default.ParseArguments<Options>(args);
var optValue = options.Value;

if (optValue == null)
{
    Console.WriteLine(optValue);
    Environment.Exit(0);
}
else
{
    fiTiff = new FileInfo(optValue.TiffFileName);
}

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var MyImage = Image.FromFile(fiTiff.Name);

PdfDocument doc = new PdfDocument();
var imgFrameCount = MyImage.GetFrameCount(FrameDimension.Page);
for (int PageIndex = 0; PageIndex < imgFrameCount; PageIndex++)
{
    try
    {
        MyImage.SelectActiveFrame(FrameDimension.Page, PageIndex);
        var stream = MyImage.ToStream(ImageFormat.Tiff);
        XImage image = XImage.FromStream(stream);
        var page = new PdfPage();
        page.Width = XUnit.FromPoint(image.PointWidth);
        page.Height = XUnit.FromPoint(image.PointHeight);
        ////if (image.PixelWidth > image.PixelHeight)
        //if (image.PointWidth > image.PointHeight)
        //{
        //    page.Orientation = PageOrientation.Landscape;
        //}
        //else
        //{
        page.Orientation = PageOrientation.Portrait;
        //}
        doc.Pages.Add(page);
        XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[PageIndex]);
        xgr.DrawImage(image, 0, 0);
    }
    catch (Exception ex)// this is because some tiff are having problems
    {
        Console.WriteLine(PageIndex);
        Console.WriteLine(ex.Message);
    }
}
//string destinaton = Path.GetFullPath(d.Path).Replace(".tif", ".pdf");
doc.Save($"{Path.GetFileNameWithoutExtension(fiTiff.Name)}.pdf");
doc.Close();
MyImage.Dispose();
