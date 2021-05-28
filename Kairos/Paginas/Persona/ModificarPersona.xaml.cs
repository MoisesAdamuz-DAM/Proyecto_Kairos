using Kairos.Modelo;
using Kairos.VMs.Personas;

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