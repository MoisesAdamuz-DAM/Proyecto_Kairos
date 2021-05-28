using Kairos.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using Kairos.VMs;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RopaVista : ContentPage {

        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<PersonaM> _post;
        public RopaVista() {
            InitializeComponent();
            BindingContext = new Ropa();
            
        }

        public async void btnDatos_Clicked(object sender, EventArgs e) {

            PersonaM mem = new PersonaM {

                nombrePersona = txtNombre.Text,
                paisOrigen = txtPais.Text,
                ubicacionPersona = txtUbicacion.Text,
                necesidadPersona = txtNecesidad.Text,
                historialPersona = txtHistorial.Text
                
            };
       
            var json = JsonConvert.SerializeObject(mem);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Url, contentJson);

            if (response.StatusCode == HttpStatusCode.OK) {

                await DisplayAlert("Datos", "Se inserto correctamente la informacion", "OK");
                txtNombre.Text = "";
                txtPais.Text = "";
                txtUbicacion.Text = "";
                txtNecesidad.Text = "";
                txtHistorial.Text = "";
            } else {

                await DisplayAlert("Datos", "Se inserto correctamente la informacion", "OK");
                txtNombre.Text = "";
                txtPais.Text = "";
                txtUbicacion.Text = "";
                txtNecesidad.Text = "";
                txtHistorial.Text = "";
            }
        }
    }
}