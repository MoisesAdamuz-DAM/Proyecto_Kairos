using Acr.UserDialogs;
using Kairos.Modelo;
using Kairos.Paginas;
using Kairos.Paginas.Persona;
using Newtonsoft.Json;
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
    public class Ropa : BaseVM {

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        //Llama al modelo
        private PersonaM _selectedItem;

        
        public ICommand RefreshCommand { get; private set; }
        public Command<PersonaM> ItemTapped { get; }
        public Command<int> LongTapped { get; }
        private List<PersonaM> _postsList { get; set; }
        private bool _isLoading { get; set; }

 
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
        // CONSTRUCTOR
        //======================================================================================================================================
        public Ropa() {

            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
            LongTapped = new Command<int>(async (id) => await AbrirPopUp(id));
            ItemTapped = new Command<PersonaM>(OnItemSelected);
        }


        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private readonly PersonaM persona;
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        //======================================================================================================================================
        // RESPUESTAS DE EVENTOS
        //======================================================================================================================================
       /* public void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }

        public virtual void OnAppearing(object navigationContext) { }

        public virtual void OnDisappearing() { }*/
        //======================================================================================================================================
        // MÉTODOS
        //=====================================================================================================================================

        /// <summary>
        /// Muestra los datos
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
        /// Refresca y muestra los datos actualizados
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
        /// Abre dialogo para ejecutar el eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task AbrirPopUp(int id) {

            bool eliminar = await UserDialogs.Instance.ConfirmAsync("¿Deseas eliminar esta persona?", "Eliminar", "Aceptar", "Cancelar");

            if (eliminar) {

                string uri = ("https://webapi-kairos.conveyor.cloud/api/persona" + "/" + id);
                HttpResponseMessage response = await client.DeleteAsync(uri);
                await UserDialogs.Instance.ConfirmAsync("Se ha realizado la baja correctamente", "Operación Correcta", "Aceptar");
            } else {
                await UserDialogs.Instance.ConfirmAsync("No se ha podido realizar la baja", "Aceptar");
            }
        }

        public PersonaM SelectedItem {
            get => _selectedItem;
            set {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(PersonaM item) {

            if (item == null)
                return;
            await Shell.Current.Navigation.PushAsync(new ModificarPersona(item));
        }
    }
}