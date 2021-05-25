using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kairos.VMs {
    public abstract class BaseVM : INotifyPropertyChanged {

        private bool _isLoading { get; set; }
        public bool IsLoading {

            get { return _isLoading; }
            set {
                if (value != _isLoading) {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        //======================================================================================================================================
        // RESPUESTAS DE EVENTOS
        //======================================================================================================================================
        public void OnPropertyChanged([CallerMemberName] string nombrePropiedad = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }

        public virtual void OnAppearing(object navigationContext) { }

        public virtual void OnDisappearing() { }

    }

}