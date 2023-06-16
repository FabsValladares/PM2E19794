using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Media;
using System.IO;
using PM2Examen.Views;
using PM2Examen.Models;
using PM2Examen.Controllers;

namespace PM2Examen
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ObtenerLatitudyLongitud();
        }

        public async void ObtenerLatitudyLongitud()
        {
            try
            {
                var geolocarequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                var Cancelaciontoken = new System.Threading.CancellationTokenSource();

                var ubicacion = await Geolocation.GetLocationAsync(geolocarequest, Cancelaciontoken.Token);
                

                if (ubicacion != null)
                {
                    txtLatitud.Text = ubicacion.Latitude.ToString();
                    txtLongitud.Text = ubicacion.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Alerta", "Este dispositivo no soporta el proceso de Geolocalizacion"+ fnsEx, "Ok");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Alerta", "El GPS se encuentra desactivado, favor activar el GPS", "Ok");
                System.Diagnostics.Process.GetCurrentProcess().Kill(); //cerramos la aplicacion hasta que el usuario active el GPS

            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Alerta", "La Aplicación no cuenta con los permisos para acceder al GPS" + pEx, "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ocurrrio un error al acceder a su Ubicación" + ex, "Ok");
            }
        }

        byte[] GuardarImagen;
        private async void AgregarImagen(object sender, EventArgs e)
        {
            try
            {  

                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MYAPP",
                    Name = DateTime.Now.ToString() + "_foto.jpg",
                    SaveToAlbum = true
                });

                await DisplayAlert("Ubicacion de la foto: ", photo.Path, "Aceptar");

                if (photo != null)
                {
                    GuardarImagen = null;
                    MemoryStream memoryStream = new MemoryStream();

                    photo.GetStream().CopyTo(memoryStream);
                    GuardarImagen = memoryStream.ToArray();

                    Foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                }
                ObtenerLatitudyLongitud();
                txtdescripcion.Focus();//le pasamos el puntero a la descripcion
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error al Ingresar una Imagen "+ex, "Aceptar");
            }
        }

        private async void btnlistar_Clicked(object sender, EventArgs e)
        {
            //Sintaxis para dirigirnos a otra pantalla
            await Navigation.PushAsync(new ListadeSitios());
        }

        private void btnsalir_Clicked(object sender, EventArgs e)
        {
            //mandamos un sentesia para que mate el proseso 
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            if (GuardarImagen == null)
            {
                await DisplayAlert("Aviso","Capture una imagen del sitio", "OK");
            }else if (txtdescripcion.Text == null)
            {
                await DisplayAlert("Aviso", "Ingrese la descripcion del sitio", "OK");
            }
            else
            {
                var sitio = new Sitios { imagen = GuardarImagen, longitud = txtLongitud.Text, latitud = txtLatitud.Text, descripcion = txtdescripcion.Text };
                var resultado = await App.Instancia.GuardarSitio(sitio);

                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "¡Sitio ingresado con exito!", "OK");
                    txtdescripcion.Text = "";
                    Foto.Source = "imagen.jpg";
                    GuardarImagen = null;
                    
                }
                else
                {
                    await DisplayAlert("Aviso", "Ha Ocurrido un Error", "OK");
                }


                await Navigation.PopAsync();
            }
        }
    }
}
