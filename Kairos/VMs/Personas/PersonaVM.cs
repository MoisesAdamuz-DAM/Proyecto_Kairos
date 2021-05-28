using Acr.UserDialogs;
using Kairos.Modelo;
using Kairos.Paginas;
using Kairos.Paginas.Persona;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kairos.VMs {
    public class PersonaVM : BaseVM {

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        private PersonaM _selectedItem;
        public ICommand RefreshCommand { get; private set; }
        public Command<PersonaM> ItemTapped { get; }
        public Command<int> LongTapped { get; }



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
        private readonly PersonaM persona;
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();


        //======================================================================================================================================
        // CONSTRUCTOR
        //======================================================================================================================================
        public PersonaVM(PersonaM persona) {

            this.persona = persona;
            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
            ItemTapped = new Command<PersonaM>(OnItemSelected);
            LongTapped = new Command<int>(async (id) => await AbrirPopUp(id));
        }

        private async Task AbrirPopUp(int id) {

            bool eliminar = await UserDialogs.Instance.ConfirmAsync("Deseas eliminar esta persona?", "Aceptar", "Cancelar");

            if (eliminar) {

                string uri = (Url + "?id" + id);
                HttpResponseMessage response = await client.DeleteAsync(uri);
                await UserDialogs.Instance.ConfirmAsync("Se ha realizado la baja correctamente", "Aceptar");
            } else {
                await UserDialogs.Instance.ConfirmAsync("No se ha podido realizar la baja", "Aceptar");
            }
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

        public PersonaM SelectedItem {
            get => _selectedItem;
            set {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        /// <summary>
        /// Abre ModificarPersona
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        async void OnItemSelected(PersonaM item) {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.Navigation.PushAsync(new ModificarPersona(item));
        }
    }
}
