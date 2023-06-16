using PM2Examen.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2Examen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class ListadeSitios : ContentPage
    {
        private Sitios sitio;

        public ListadeSitios()
        {
            InitializeComponent();
        }

        private void toolmenu_Clicked(object sender, EventArgs e)
        {

        }

        private void ListadeSitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sitio = (Sitios)e.Item;

        }




        private async void btnvermapa_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new VerMapa(sitio.latitud, sitio.longitud, sitio.descripcion));
            }
            catch
            {
                await DisplayAlert("Aviso", "Por Favor,Seleccione el Sitio que desea ver", "Ok");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ListadeSitio.ItemsSource = await App.Instancia.ObtenerlistadeSitios();
        }


        private async void btneliminacasa_Clicked(object sender, EventArgs e)
        {
            try
            {
                var EliminarSitio = await App.Instancia.EliminarSitio(sitio);


                if (EliminarSitio != 0)
                {
                    await DisplayAlert("Aviso", "Sitio eliminado Correctamente !", "Ok");
                    ListadeSitio.ItemsSource = await App.Instancia.ObtenerlistadeSitios();

                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error !", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Aviso", "Favor seleccione que sitio desea eliminar", "Ok");
            }



        }
    }
}
