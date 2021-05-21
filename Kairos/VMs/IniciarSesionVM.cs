using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kairos.VMs {
    class IniciarSesionVM : TriggerAction<ImageButton>, INotifyPropertyChanged {

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        public string ShowIcon { get; set; }
        public string HideIcon { get; set; }
        public string Clave { get; set; }

        public ICommand ComandoInicialesAMayusculas => comandoInicialesAMayusculas ??= new ComandoDelegado(InicialesAMayusculas);

        public ICommand ComandoIniciarSesion => comandoIniciarSesion ??= new ComandoDelegado(IniciarSesion);

        public ICommand ComandoMostrarClave => comandoMostrarClave ??= new ComandoDelegado(MostrarClave);

        public bool EsClave {
            get => esClave;
            set {
                esClave = value;
                OnPropertyChanged();
            }
        }
        public string Iniciales {
            get => iniciales;
            set {
                iniciales = value;
                OnPropertyChanged();
            }
        }
        public bool MantenerSesion { get; set; }
       

        public bool HidePassword {
            set {
                if (_hidePassword != value) {
                    _hidePassword = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }
            get => _hidePassword;
        }


        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private ICommand comandoInicialesAMayusculas;
        private ICommand comandoIniciarSesion;
        private ICommand comandoMostrarClave;
        private bool esClave = true;
        bool _hidePassword = true;
        private string iniciales;

        //======================================================================================================================================
        // MÉTODOS
        //======================================================================================================================================
        /// <summary>
        /// Pasa las iniciales a mayúsculas.
        /// </summary>
        private void InicialesAMayusculas() {
            if (!string.IsNullOrEmpty(iniciales)) { Iniciales = iniciales.ToUpper(); }
        }
        /// <summary>
        /// Inicia la sesión.
        /// </summary>
        private async void IniciarSesion() {
            if (string.IsNullOrEmpty(iniciales)) {
                _ = UserDialogs.Instance.Toast("Introduzca las iniciales");

            } else if (string.IsNullOrEmpty(Clave)) {
                _ = UserDialogs.Instance.Toast("Introduzca la contraseña");

            } else {
                try {
                    UsuarioAutenticadoDTO autenticadoDTO = await WebAPI_Home.IniciarSesion(iniciales.ToUpper(), Clave,
                        $"{DeviceInfo.Manufacturer} {DeviceInfo.Model} - {DeviceInfo.Platform} {DeviceInfo.VersionString}",
                        $"Sofitec Home ({Version})", Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault().ToString());

                    _ = UserDialogs.Instance.Toast($"Bienvenido/a {autenticadoDTO.Nombre} {autenticadoDTO.Apellido1}");

                    Preferences.Set("Token", autenticadoDTO.Token);

                    ComprobarMantenerSesion();
                    CargarInicio(autenticadoDTO);

                } catch (Exception ex) {
                    _ = UserDialogs.Instance.Toast(ex.Message);
                }
            }
        }

        /// <summary>
        /// Comprueba si tiene que mantener la sesión.
        /// </summary>
        private void ComprobarMantenerSesion() {
            Preferences.Set("MantenerSesion", MantenerSesion);
        }

        /// <summary>
        /// Carga la página de inicio.
        /// </summary>
        /// <param name="usuario">Datos del usuario.</param>
        private void CargarInicio(UsuarioAutenticadoDTO usuario) {
            Application.Current.MainPage = new AppShell();

            Shell.Current.Items.Add(new FlyoutItem {
                FlyoutItemIsVisible = false,
                Items = { new ShellContent { Content = new Inicio(usuario) } },
                Title = nameof(Inicio)
            });

            Shell.Current.Items.Add(new FlyoutItem {
                Items = { new ShellContent { Content = new Paginas.SIMs.SIMs(usuario) } },
                Title = nameof(SIMs)
            });
        }

        protected override void Invoke(ImageButton sender) {
            sender.Source = HidePassword ? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
