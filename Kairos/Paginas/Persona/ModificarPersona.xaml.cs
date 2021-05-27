
using Kairos.Modelo;
using Kairos.VMs;
using Kairos.VMs.Personas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas.Persona {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarPersona : ContentPage {
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<PersonaM> _post;

        public ModificarPersona(PersonaM dto, int id) {
            InitializeComponent();
            BindingContext = new ModificarVM(dto,id);
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
            var response = await client.PutAsync(Url, contentJson);

            if (response.StatusCode == HttpStatusCode.OK) {

                await DisplayAlert("Datos", "Se modificó correctamente la informacion", "OK");
                txtNombre.Text = "";
                txtPais.Text = "";
                txtUbicacion.Text = "";
                txtNecesidad.Text = "";
                txtHistorial.Text = "";
            } else {

                await DisplayAlert("Datos", "Se modificó correctamente la informacion", "OK");
                txtNombre.Text = "";
                txtPais.Text = "";
                txtUbicacion.Text = "";
                txtNecesidad.Text = "";
                txtHistorial.Text = "";
            }
        }
    }
}