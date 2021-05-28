using Kairos.Modelo;
using Kairos.VMs.Personas;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas.Persona {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarPersona : ContentPage {
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        
        public ModificarPersona(PersonaM item) {
            InitializeComponent();
            BindingContext = new ModificarVM(item);
        }

        private async void btnDatos_Clicked(object sender, System.EventArgs e) {


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

                await DisplayAlert("Datos", "Se inserto correctamente la informacion", "OK");
             
            } else {

                await DisplayAlert("Datos", "Se inserto correctamente la informacion", "OK");
            
            }
        }
    }
}