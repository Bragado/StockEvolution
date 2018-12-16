using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamLogo {
  public class LogoSkiaPage : ContentPage
    {
        /* Canvas */
        SKCanvasView skiaView;
        /* HTTP Fetch Information */
        
        private ArrayList points = null;
        private string m1 = "";
        private ArrayList points2 = null;
        private string m2 = "";
        /* Http request of the Stokes */
        string uri = "";
        string uri1 = "https://marketdata.websol.barchart.com/getHistory.json?apikey=89d0fea63ea086bb1e7186f1eb72ac8e&symbol=INTC&type=daily&startDate=20181113";
        string uri2 = "https://marketdata.websol.barchart.com/getHistory.json?apikey=89d0fea63ea086bb1e7186f1eb72ac8e&symbol=AAPL&type=daily&startDate=20181113";

        /* Info */
        string stock1 = "Apple";
        string stock2 = "Micro";
        int period = 7;         // last 7 days


        public LogoSkiaPage() {
          Title = "XamLogo";
           skiaView = new SKCanvasView() {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
          };
          skiaView.PaintSurface += OnPaintDrawing;
          var title = new Label() {
            HorizontalTextAlignment = TextAlignment.Center,
            Text = "Xamarin Logo"
          };
          var layout = new StackLayout() {
            Spacing = 0.0
          };
          layout.Children.Add(skiaView);
          layout.Children.Add(title);
          Content = layout;
            uri = uri1;
            Task.Factory.StartNew(GetStockInfoAsync);
           

        }

    private void OnPaintDrawing(object sender, SKPaintSurfaceEventArgs e) {


            e.Surface.Canvas.Clear();
            ArrayList xLabels = null, yLabels = null;

           
           xLabels = Utilis.GetXLabels(points, 4);
           yLabels = Utilis.GetYLabels(m1, period);
           
            

            if (points2 != null)
            {
                ArrayList total = new ArrayList();
                total.AddRange(points);
                total.AddRange(points2);
                xLabels = Utilis.GetXLabels(total, 4);

            }



            Plot2D plot = new Plot2D(e.Surface.Canvas,  e.Info.Height, e.Info.Width, yLabels, xLabels, points, points2);
            plot.Label1 = stock1;
            plot.Label2 = stock2;
            plot.Display();


     }
        

        public void DoSomething(String message)
        {
              if (points != null)
            {
                points2 = Utilis.ParseStockPoints(message, period);
                m2 = message;
            }
                    
            else
            {
                points = Utilis.ParseStockPoints(message, period);
                m1 = message;
            }

                
                skiaView.InvalidateSurface();

         

            
        }

        public async System.Threading.Tasks.Task GetStockInfoAsync()
        {
            string Result;
            using (HttpClient client = new HttpClient())
                try
                {

                    HttpResponseMessage message = await client.GetAsync(uri);
                    if (message.StatusCode == HttpStatusCode.OK)
                        DoSomething(await message.Content.ReadAsStringAsync());

                    if(uri.Equals(uri1) && !uri2.Equals(""))
                    {
                        uri = uri2;
                        Task.Factory.StartNew(GetStockInfoAsync);
                    }
                }
                catch (Exception ex)
                {
                    Result = ex.Message;
                }
        }



    }
      

  }

 

