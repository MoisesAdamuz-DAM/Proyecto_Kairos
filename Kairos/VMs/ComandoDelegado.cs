using System;
using System.Windows.Input;

namespace Kairos.VMs {
    class ComandoDelegado : ICommand {
        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private readonly Action _ejecutar;
        private readonly Func<bool> _puedeEjecutar;

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        /// <summary>
        /// Notifica a los controles XAML usando el comando para reevaluar el CanExecute del mismo.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        //======================================================================================================================================
        // CONSTRUCTORES
        //======================================================================================================================================
        public ComandoDelegado(Action ejecutar) : this(ejecutar, null) { }

        public ComandoDelegado(Action ejecutar, Func<bool> puedeEjecutar) {
            _ejecutar = ejecutar;
            _puedeEjecutar = puedeEjecutar;
        }

        //======================================================================================================================================
        // MÉTODOS
        //======================================================================================================================================
        /// <summary>
        /// Se llama desde el XAML para evaluar si el comando se puede ejecutar.
        /// </summary>
        public bool CanExecute(object parameter) {
            if (_puedeEjecutar != null) return _puedeEjecutar(); else { return true; }
        }

        /// <summary>
        /// Se llama desde el XAML para ejecutar el comando.
        /// </summary>
        public void Execute(object parameter) {
            _ejecutar();
        }

        /// <summary>
        /// Permite forzar la ejecución del método 'CanExecute' para reevaluar la ejecución.
        /// </summary>
        public void OnCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

    }

    /// <summary>
    /// Permite delegar la ejecución de comandos a VistaModelos (ViewModels) usando un tipo T como parámetro.
    /// </summary>
    public class ComandoDelegado<T> : ICommand {

        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private readonly Action<T> _ejecutar;
        private readonly Func<T, bool> _puedeEjecutar;

        //======================================================================================================================================
        // EVENTOS
        //======================================================================================================================================
        /// <summary>
        /// Notifica a los controles XAML usando el comando para reevaluar el CanExecute del mismo.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        //======================================================================================================================================
        // CONSTRUCTORES
        //======================================================================================================================================
        public ComandoDelegado(Action<T> ejecutar) : this(ejecutar, null) { }

        public ComandoDelegado(Action<T> ejecutar, Func<T, bool> puedeEjecutar) {
            _ejecutar = ejecutar;
            _puedeEjecutar = puedeEjecutar;
        }

        //======================================================================================================================================
        // MÉTODOS
        //======================================================================================================================================
        /// <summary>
        /// Se llama desde el XAML para evaluar si el comando se puede ejecutar.
        /// </summary>
        public bool CanExecute(object parameter) {
            if (_puedeEjecutar != null) return _puedeEjecutar((T)parameter); else { return true; }
        }

        /// <summary>
        /// Se llama desde el XAML para ejecutar el comando.
        /// </summary>
        public void Execute(object parameter) {
            _ejecutar((T)parameter);
        }

        /// <summary>
        /// Permite forzar la ejecución del método 'CanExecute' para reevaluar la ejecución.
        /// </summary>
        public void OnCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

    }

}

