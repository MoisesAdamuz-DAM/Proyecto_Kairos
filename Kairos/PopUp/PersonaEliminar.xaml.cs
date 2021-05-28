using Kairos.Modelo;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Platform.Android.Resource;

namespace Kairos.PopUp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaEliminar : PopupPage {
        private const string Url = "https://webapi-kairos.conveyor.cloud/api/persona";
        private readonly HttpClient client = new HttpClient();
        private PersonaM persona;

        public PersonaEliminar() {
            InitializeComponent();
        }

        public async void Button_Aceptar(object sender, EventArgs e) {

            string uri = (string.Format(Url, persona.id));
            string jsonData = JsonConvert.SerializeObject(persona);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.DeleteAsync(uri);

        }

    }
}