
using Kairos.Modelo;
using Kairos.Paginas;
using Kairos.Paginas.Persona;
using Kairos.PopUp;
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
    public class PersonaVM :BaseVM{

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        private PersonaM _selectedItem;
        public ICommand RefreshCommand { get; private set; }
        public Command<PersonaM> ItemTapped { get; }
        public Command LongTouchCommand { get; }
        
      

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


        //======================================================================================================================================
        // CONSTRUCTOR
        //======================================================================================================================================
        public PersonaVM(PersonaM persona) {

            this.persona = persona;
            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
            ItemTapped = new Command<PersonaM>(OnItemSelected);
            LongTouchCommand = new Command(async () => await AbrirPopUp());
        }

        private async  Task AbrirPopUp() {

            await PopupNavigation.Instance.PushAsync(new PersonaEliminar());
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