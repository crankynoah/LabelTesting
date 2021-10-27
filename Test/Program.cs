
using QRCoder;
using SelectPdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrInfo = qrCodeGenerator.CreateQrCode("bing.com", QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(qrInfo);
            Bitmap qrMap = code.GetGraphic(60);
            byte[] mapArray;

            using (var stream = new MemoryStream())
            {
                qrMap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                mapArray = stream.ToArray();
            }
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(mapArray));



            
            //var HTMLStr = String.Format(" <img src='{0}'>", QrUri);
            //var HTMLStr =String.Format("<!DOCTYPE html><html lang=\"en\"> <head> <meta charset=\"UTF - 8\"/> <meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge\"/> <meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\"/> <title>Document</title> <style>.container{{display: grid; grid-template-rows: 3.5in 3.5in 3.5in; grid-template-columns: 4in 4in; grid-row-gap: 10px; grid-column-gap: 10px; grid-gap: 10px;}}.item{{padding: 15px; font-size: 18px; font-family: sans-serif; color: rgb(80, 65, 65); border: 1px solid salmon; writing-mode: vertical-rl; text-orientation: mixed;}}h3{{padding: 0px; margin: 0px;}}.external{{max-height: 1in; float: left;}}.internal{{max-height: 0.9in;}}</style> </head> <body> <div class=\"container\"> <div class=\"item item--1\"> <h3>Item 107840</h3> 2018 Polaris Sportsman XP 800 <br/> <img class=\"external\" src='{0}'/> <br/> <span> CrankyApe.com <br/>Scan this QRcode to view details on the website </span> <br/><br/> <hr/> <br/> <img class=\"internal\" src='{0}'/> Office Use Only <br/> Vin: 4XASXE952JB135476 </div><div class=\"item item--2\">2: Green</div><div class=\"item item--3\">3: Violet</div><div class=\"item item--4\">4: Pink</div><div class=\"item item--5\">5: Blue</div><div class=\"item item--6\">6: Brown</div></div></body></html>", QrUri);

            


            StringBuilder sb = new StringBuilder();
            //append head of html doc
            sb.Append("<head><title>Document</title><style>.container{display:grid;grid-template-rows:4.4in 4.4in 4.4in;grid-template-columns:5in 5in;grid-row-gap:0px;grid-column-gap:18px;}.item{padding:15px;font-size:22px;font-family:sans-serif;color:rgb(80, 65, 65);border: 1px solid black;writing-mode:vertical-rl;text-orientation:mixed;}h3{padding:0px;margin:0px;}.external{max-height:1.5in;float:left;}.internal{max-height:1.15in;}</style></head>");
            //open body and main div tag
            sb.Append("<body> <div class=\"container\">");
            sb.Append("<div class=\"item\">2: Green</div>");
            //open div with details
            sb.Append("<div class=\"item item--1\">");
            //heading with item #
            sb.Append("<h3>Item 107840</h3>");
            //item details
            sb.Append("2018 Polaris Sportsman XP 800 <br/>");
            //public qr code
            sb.Append(String.Format("<img class=\"external\" src='{0}'/> <br/>", QrUri));
            //where to see the unit copy and hr
            sb.Append("<span> BongoDog <br/>Scan this QRcode to view details on the website </span> <br/><br/> <hr/> <br/>");
            //internal qr code
            sb.Append(String.Format("<img class=\"internal\" src='{0}'/>", QrUri));
            //internal copy and vin
            sb.Append("Office Use Only <br/> Vin: 4XASXE952JB135476");
            //close spacing div
            sb.Append("</div>");
            //spacing divs
            
            sb.Append("<div class=\"item\">3: Violet</div>");
            sb.Append("<div class=\"item\">4: Pink</div>");
            sb.Append("<div class=\"item\">5: Blue</div>");
            sb.Append("<div class=\"item\">6: Brown</div>");
            //close remaining open elements
            sb.Append("</div></body></html>");

            //var Renderer = new IronPdf.HtmlToPdf();
            //Renderer.PrintOptions.EnableJavaScript = true;
            //Renderer.PrintOptions.RenderDelay = 500; //milliseconds
            //Renderer.PrintOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;
            //
            //Renderer.PrintOptions.MarginTop = 10;
            //Renderer.PrintOptions.MarginBottom = 0;
            //Renderer.PrintOptions.MarginLeft = -2;
            //Renderer.PrintOptions.MarginRight = 2;
            //var PDF = Renderer.RenderHtmlAsPdf(sb.ToString());
            //PDF.SaveAs($@"C:\Development\Goofin\Label Testing\HTML.pdf");

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.Letter;
            converter.Options.RenderingEngine = RenderingEngine.Blink;
            converter.Options.MarginTop = 22;
            converter.Options.MarginLeft = 18;


            PdfDocument pdf = converter.ConvertHtmlString(sb.ToString());
            

            pdf.Save($@"C:\Development\Goofin\Label Testing\HTML2.pdf");

            pdf.Close();

            Console.WriteLine($@"Output \HTML.pdf");
            
        }
    }
}
