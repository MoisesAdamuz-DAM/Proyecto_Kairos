
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IniciarSesion : ContentPage {
        public IniciarSesion() {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e) {

            await Shell.Current.GoToAsync("//inicio");
        }
    }
}