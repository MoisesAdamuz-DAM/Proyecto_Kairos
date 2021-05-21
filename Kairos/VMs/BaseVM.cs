using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kairos.VMs {
    public abstract class BaseVM : INotifyPropertyChanged {

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        //======================================================================================================================================
        // RESPUESTAS DE EVENTOS
        //======================================================================================================================================
        public void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }

        public virtual void OnAppearing(object navigationContext) { }

        public virtual void OnDisappearing() { }

    }

}