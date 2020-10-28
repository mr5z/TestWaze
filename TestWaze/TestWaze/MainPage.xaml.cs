using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestWaze
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            entryDestination.TextChanged += Entry_TextChanged;
            entryOrigin.TextChanged += Entry_TextChanged;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var origin = Parse(entryOrigin.Text);
            var destination = Parse(entryDestination.Text);
            if (origin != null && destination != null)
            {
                UpdateLocation(origin, destination);
            }
        }

        private LatLng Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
                
            var geolocation = text.Split(',');
            if (geolocation.Length < 2)
                return null;

            if (double.TryParse(geolocation[0], out var latitude) && 
                double.TryParse(geolocation[1], out var longitude))
            {
                return new LatLng
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            }
            
            return null;
        }

        private void UpdateLocation(LatLng origin, LatLng destination)
        {
            var source = $"https://www.waze.com/en/livemap/directions?&to=ll.{destination.Latitude}%2C{destination.Longitude}&from=ll.{origin.Latitude}%2C{destination.Longitude}";
            webView.Source = source;
        }

        class LatLng
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
