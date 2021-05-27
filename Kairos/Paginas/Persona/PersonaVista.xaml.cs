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

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaVista : ContentPage {
       
        public PersonaVista() {
            InitializeComponent();
            //BindingContext = new VMs.Ropa();
         
        }

        public async void Alta (object sender, EventArgs e) {

            await Navigation.PushAsync(new AltaPersona());
        }
    }
}