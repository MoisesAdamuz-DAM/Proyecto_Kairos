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
    public class Ropa : INotifyPropertyChanged{

       private List<PersonaM> _postsList { get; set; }
        private bool _isLoading { get; set; }
        public bool IsLoading {

            get { return _isLoading; }
            set { if(value != _isLoading) {
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

        public ICommand RefreshCommand { private get; set; }

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
            DeleteDataAsync();


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
            if (IsRefreshing) { 
            IsRefreshing = true;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://webapi-kairos.conveyor.cloud/api/persona");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            PostsList.Clear();
            PostsList = new List<PersonaM>(posts);
            IsRefreshing = false;
            }
        }

        private async void DeleteDataAsync() {
            var url = "https://webapi-kairos.conveyor.cloud/api/persona";
            IsLoading = true;
            HttpClient httpClient = new HttpClient();

            var id = 4;
            var uri = new Uri(string.Format(url, id));
            var response = await httpClient.DeleteAsync(uri);
          
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            PostsList = new List<PersonaM>(posts);

            IsLoading = false;

        }
    }
}
