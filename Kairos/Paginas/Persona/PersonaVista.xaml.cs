using Kairos.Modelo;
using Kairos.VMs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaVista : ContentPage {
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<PersonaM> _post;

        public PersonaVista() {
            InitializeComponent();
            BindingContext = new PersonaVM();
            GetRegistration();

        }


        public async void GetRegistration() {

            string content = await client.GetStringAsync(Url);
            List<PersonaM> posts = JsonConvert.DeserializeObject<List<PersonaM>>(content);
            _post = new ObservableCollection<PersonaM>(posts);
            LV.ItemsSource = _post;
            base.OnAppearing();
        }

        public async void Alta(object sender, EventArgs e) {

            await Navigation.PushAsync(new AltaPersona());
        }
    }
}