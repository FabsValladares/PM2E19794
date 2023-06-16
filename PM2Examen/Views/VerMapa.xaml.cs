using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Media;

namespace PM2Examen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerMapa : ContentPage
    {
        String maplatitud, maplongitud, mapdescripcion;
        public VerMapa(String latitud, String longitud, String descripcion)
        {
            InitializeComponent();
            maplatitud = latitud;
            maplongitud = longitud;
            mapdescripcion = descripcion;

            
        }

        private async void btnCompartir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Share.RequestAsync(
                   new ShareTextRequest
                   {
                       Title= "Te comparto la ubicacion",
                       Text= "Este es el lugar donde me tome la fotografia",
                       Uri = "https://maps.google.com/?q="+maplongitud+","+maplatitud
                   }
                    );
            }
            catch
            {

            }
        }

        private void toolmenu_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

                var localizacion = Geolocation.GetLocationAsync();
                if (localizacion == null)
                {
                    DisplayAlert("Advertencia", "Su GPS se encuentra desactivado", "Ok");
                }

                Pin ubicacion = new Pin();
                ubicacion.Label = mapdescripcion.ToString();
                ubicacion.Type = PinType.Place;
                ubicacion.Position = new Position(Double.Parse(maplongitud), Double.Parse(maplatitud));
                mapa.Pins.Add(ubicacion);
                mapa.IsShowingUser = true;//muestra la ubicacion del usuario en donde se encuentra
                mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Double.Parse(maplongitud),
                Double.Parse(maplatitud)), Distance.FromMeters(500.0)));


      


        }





    }
}