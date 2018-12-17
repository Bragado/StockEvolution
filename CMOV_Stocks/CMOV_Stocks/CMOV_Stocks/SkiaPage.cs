using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMOV_Stocks
{
	public class SkiaPage : ContentPage
	{
        /* Canvas */
        SKCanvasView skiaView;
        /* HTTP Fetch Information */

        private ArrayList points = null;
        private string m1 = "";
        private ArrayList points2 = null;
        private string m2 = "";
        /* Http request of the Stokes */
        private string api_key = "89d0fea63ea086bb1e7186f1eb72";
        private string api_key2 = "a7dab2dcc57c272e767a59cdf3b919e1";

        string uri = "";
        string uri1 = "";
        string uri2 = "";
        
        /* Info */
        string stock1 = "Apple";
        string stock2 = "Micro";
        int period = 7;         // last 7 days

        public Company Company1 { get; set; }
        public Company Company2 { get; set; }

        public SkiaPage() {
            InitiatePage();
        }

        public SkiaPage(Company company1, Company company2) {
            Company1 = company1;

            if (company2 != null)
                Company2 = company2;

            InitiatePage();
        }

        private void InitiatePage() {
            //set uris - change api_keys if necessary
            uri1 = "https://marketdata.websol.barchart.com/getHistory.json?apikey=" + api_key2 + "&symbol={CODE}&type=daily&startDate=20181113";
            uri2 = "https://marketdata.websol.barchart.com/getHistory.json?apikey=" + api_key2 + "&symbol={CODE}&type=daily&startDate=20181113";

            if (Company1 != null) {
                uri1 = uri1.Replace("{CODE}", Company1.Code);
            }

            if (Company2 != null) {
                uri2 = uri2.Replace("{CODE}", Company2.Code);
            }

            Title = "My Stocks";
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

            xLabels = Utils.GetXLabels(points, 4);
            yLabels = Utils.GetYLabels(m1, period);

            if (points2 != null) {
                ArrayList total = new ArrayList();
                total.AddRange(points);
                total.AddRange(points2);
                xLabels = Utils.GetXLabels(total, 4);
            }

            Plot2D plot = new Plot2D(e.Surface.Canvas, e.Info.Height, e.Info.Width, yLabels, xLabels, points, points2);
            plot.Label1 = Company1.Code;

            if (Company2 != null) {
                plot.Label2 = Company2.Code;
            } else {
                plot.Label2 = "";
            }
            
            plot.Display();
        }

        public void DoSomething(String message) {
            if (points != null) {
                points2 = Utils.ParseStockPoints(message, period);
                m2 = message;
            } else {
                points = Utils.ParseStockPoints(message, period);
                m1 = message;
            }

            skiaView.InvalidateSurface();
        }

        public async System.Threading.Tasks.Task GetStockInfoAsync() {
            string Result;
            using (HttpClient client = new HttpClient())
                try {
                    HttpResponseMessage message = await client.GetAsync(uri);
                    Console.WriteLine(uri);
                    if (message.StatusCode == HttpStatusCode.OK)
                        DoSomething(await message.Content.ReadAsStringAsync());

                    if (Company2 != null && uri.Equals(uri1) && !uri2.Equals("")) {
                        uri = uri2;
                        Task.Factory.StartNew(GetStockInfoAsync);
                    }
                } catch (Exception ex) {
                    Result = ex.Message;
                }
        }
    }
}