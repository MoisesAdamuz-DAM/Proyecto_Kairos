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
    public class RopaVM : INotifyPropertyChanged{

        private PersonaM _selectedItem;
        private PersonaM id;
        public PersonaM personaM { get; }
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

        public ICommand RefreshCommand {  get; private set; }

        private bool isRefreshing;
        public bool IsRefreshing {

            get => isRefreshing;
            set {

                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public Command ComandoAbrirPersona = new Command<PersonaM>(async (dto) => await AbrirSim(dto));

        private async static Task AbrirSim(PersonaM dto) {

            await Shell.Current.Navigation.PushAsync(new RopaVista(dto));
        }

        public RopaVM(PersonaM item) {
            personaM = item;
            GetDataAsync();
            RefreshCommand = new Command(async () => await LoadPublications());
           
        }

        public async void OnItemSelected(PersonaM item) {
            if (item == null)
                return;
            await Shell.Current.Navigation.PushAsync(new RopaVista(item));
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

        public void OnAppearing() {
         
        }

       

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
