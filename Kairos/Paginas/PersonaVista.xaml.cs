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

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaVista : ContentPage {
        public PersonaVista() {
            InitializeComponent();
            BindingContext = new VMs.PersonaVM();
        }


        //Realizar Get
       private async void Button_Clicked(object sender, EventArgs e) {

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://webapi-kairos.conveyor.cloud/api/persona");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accpet", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if(response.StatusCode == HttpStatusCode.OK) {

                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<PersonaM>>(content);

                ListDemo.ItemsSource = resultado;
            }
        }
    }
}