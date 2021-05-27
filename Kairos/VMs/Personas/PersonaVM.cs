using Kairos.Modelo;
using Kairos.Paginas.Persona;
using Kairos.PopUp;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kairos.VMs {
    public class PersonaVM : BaseVM {

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        public ICommand RefreshCommand { get; private set; }
        public ICommand ComandoAbrirPersona => comandoAbrirPersona ??= comandoAbrirPersona = new Command<PersonaM>(async (dto) => await AbrirPersona(dto));
        public ICommand TouchCommand => longTouch ??= longTouch = new Command(async () => await AbrirPopUp());



        private List<PersonaM> _postsList { get; set; }

        public List<PersonaM> PostsList {
            get {
                return _postsList;
            }
            set {
                if (value != _postsList) {
                    _postsList = value;
                    OnPropertyChanged();
                }
            }
        }



        private bool isRefreshing;
        public bool IsRefreshing {

            get => isRefreshing;
            set {

                isRefreshing = value;
                OnPropertyChanged();
            }
        }


        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private ICommand comandoAbrirPersona;
        private ICommand longTouch;
        private readonly PersonaM persona;


        //======================================================================================================================================
        // CONSTRUCTOR
        //======================================================================================================================================
        public PersonaVM(PersonaM persona) {

            this.persona = persona;
            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
        }



        //======================================================================================================================================
        // MÉTODOS
        //=====================================================================================================================================

        /// <summary>
        /// Obtiene todos los datos referente a Personas
        /// </summary>
        private async void GetDataAsync() {
            IsLoading = true;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://webapi-kairos.conveyor.cloud/api/persona");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            PostsList = new List<PersonaM>(posts);
            IsLoading = false;

        }

        /// <summary>
        /// Recarga Personas
        /// </summary>
        /// <returns></returns>
        private async Task LoadPublications() {
            if (IsRefreshing == true) {
                //IsRefreshing = true;
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://webapi-kairos.conveyor.cloud/api/persona");
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
                PostsList.Clear();
                PostsList = new List<PersonaM>(posts);
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Abre ModificarPersona
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task AbrirPersona(PersonaM dto) {
            await Shell.Current.Navigation.PushAsync(new ModificarPersona(dto, persona.id));
        }

        public async Task AbrirPopUp() {
            
        }

    }
}
