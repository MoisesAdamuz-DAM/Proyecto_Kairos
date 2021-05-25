using Kairos.VMs;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RopaVista : ContentPage {

       
        public RopaVista() {
            InitializeComponent();
            BindingContext = new Ropa();

        }

        public async void Alta(object sender, EventArgs e) {

            await Navigation.PushAsync(new AltaPersona());
        }
    }
}