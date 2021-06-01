
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Kairos.Modelo;
using System.Net.Http;
using System.Text;
using System.Net;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IniciarSesion : ContentPage {
        private string Url = "https://webapi-kairos.conveyor.cloud/api/logins";
        public IniciarSesion() {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e) {

            LoginM log = new LoginM {

                nombreUsuario = txtNombreUsuario.Text,
                claveUsuario = txtClaveUsuario.Text
            };
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(log);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Url, contentJson);
            if(response.StatusCode == HttpStatusCode.OK) {
               
                await Shell.Current.GoToAsync("//inicio");

            } else {

                await DisplayAlert("Mensaje", "Datos inválidos", "OK");
            }
            
        }
    }
}