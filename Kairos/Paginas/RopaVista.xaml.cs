using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kairos.Paginas {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RopaVista : ContentPage {
        public RopaVista() {
            InitializeComponent();
            BindingContext = new VMs.Ropa();
        }
    }
}