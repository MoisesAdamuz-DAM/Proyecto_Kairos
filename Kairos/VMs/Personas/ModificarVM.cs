using Acr.UserDialogs;
using Kairos.Modelo;
using Kairos.Paginas.Persona;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kairos.VMs.Personas {
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
   public class ModificarVM : BaseVM {
      
       // public ModificarPersona modificar;
        private string itemId;
        private string nombre;
        private string pais;
        private string ubicacion;
        private string necesidad;
        private string historial;
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();

        public int Id { get; set; }

        public string Nombre {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
        public string Pais {
            get => pais;
            set => SetProperty(ref pais, value);
        }
        public string Ubicacion {
            get => ubicacion;
            set => SetProperty(ref ubicacion, value);
        }
        public string Necesidad {
            get => necesidad;
            set => SetProperty(ref necesidad, value);
        }
        public string Historial {
            get => historial;
            set => SetProperty(ref historial, value);
        }
        public string ItemId {
            get { return itemId; }
            set {
                itemId = value;
            }
        }

        public ICommand UpdateCommand { get; private set; }


        public Command modificarPersona { get; }

        public ModificarVM(PersonaM item) {
            LoadItemId(item);
            //modificarPersona = new Command(async () => await PersonaModificar());
            Nombre = item.nombrePersona;
            Pais = item.paisOrigen;
            Ubicacion = item.ubicacionPersona;
            Necesidad = item.necesidadPersona;
            Historial = item.historialPersona;
            UpdateCommand = new Command(async () => await UpdateMethod());
        }

        private async Task UpdateMethod() {

            var person = new PersonaM {
                id = Id,
                nombrePersona = Nombre,
                paisOrigen = Pais,
                ubicacionPersona = Ubicacion,
                necesidadPersona = Necesidad,
                historialPersona = Historial
                
            };

            string uri = "https://webapi-kairos.conveyor.cloud/api/persona"+ "/" + Id;
            var json = JsonConvert.SerializeObject(person);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(uri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK) {

                await UserDialogs.Instance.ConfirmAsync("", "¿Datos modificados correctamente?", "Aceptar");

            } else {

                await UserDialogs.Instance.ConfirmAsync("", "¿Error al modificar?", "Aceptar");

            }
        }

  
        public void LoadItemId(PersonaM item) {
            try {
                Id = item.id;
                Nombre = item.nombrePersona;
                Pais = item.paisOrigen;
                Ubicacion = item.ubicacionPersona;
                Necesidad = item.necesidadPersona;
                Historial = item.historialPersona;

            } catch (Exception) {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

