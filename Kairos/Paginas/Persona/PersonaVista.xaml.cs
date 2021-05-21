using Kairos.Modelo;
using Kairos.VMs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Kairos.Paginas.Persona;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaVista : ContentPage {
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<PersonaM> _post;
        public PersonaVista() {
            InitializeComponent();
            //BindingContext = new VMs.Ropa();
            GetRegistration();
        }

        public async void GetRegistration() {

            string content = await client.GetStringAsync(Url);
            List<PersonaM> posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            _post = new ObservableCollection<PersonaM>(posts);
            LV.ItemsSource = _post;
            base.OnAppearing();
        }

        public async void Alta (object sender, EventArgs e) {

            await Navigation.PushAsync(new AltaPersona());
        }


        /// <summary>
        /// Abrir el Modificar pulsando sobre item con TapGestureRecognizer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e) {
            await Shell.Current.Navigation.PushAsync(new ModificarPersona());
        }
    }
}