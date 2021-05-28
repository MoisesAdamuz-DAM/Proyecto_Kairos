
using Kairos.Modelo;
using Kairos.VMs.Personas;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas.Persona {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarPersona : ContentPage {
       
        public ModificarPersona(PersonaM item) {
            InitializeComponent();
            BindingContext = new ModificarVM(item);
        }
    }
}