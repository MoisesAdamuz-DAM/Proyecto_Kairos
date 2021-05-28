using Acr.UserDialogs;
using Kairos.Modelo;
using Kairos.Paginas;
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
    public class Ropa : INotifyPropertyChanged {

        public Command<int> LongTapped { get; }
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();

        private List<PersonaM> _postsList { get; set; }
        private bool _isLoading { get; set; }
        public bool IsLoading {

            get { return _isLoading; }
            set {
                if (value != _isLoading) {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public ICommand RefreshCommand { get; private set; }

        private bool isRefreshing;
        public bool IsRefreshing {

            get => isRefreshing;
            set {

                isRefreshing = value;
                OnPropertyChanged();
            }
        }


        public Ropa() {

            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
            LongTapped = new Command<int>(async (id) => await AbrirPopUp(id));
        }

        private async Task AbrirPopUp(int id) {

            bool eliminar = await UserDialogs.Instance.ConfirmAsync("¿Deseas eliminar esta persona?", "Eliminar", "Aceptar", "Cancelar");

            if (eliminar) {

                string uri = ("https://webapi-kairos.conveyor.cloud/api/persona"+ "/" + id);
                HttpResponseMessage response = await client.DeleteAsync(uri);
                await UserDialogs.Instance.ConfirmAsync("Se ha realizado la baja correctamente", "Operación Correcta","Aceptar");
            } else {
                await UserDialogs.Instance.ConfirmAsync("No se ha podido realizar la baja", "Aceptar");
            }
        }

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        //======================================================================================================================================
        // RESPUESTAS DE EVENTOS
        //======================================================================================================================================
        public void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }

        public virtual void OnAppearing(object navigationContext) { }

        public virtual void OnDisappearing() { }


        private async void GetDataAsync() {
            IsLoading = true;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://webapi-kairos.conveyor.cloud/api/persona");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            PostsList = new List<PersonaM>(posts);
            IsLoading = false;

        }

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



    }
}