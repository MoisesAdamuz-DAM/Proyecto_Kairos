using Kairos.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kairos.VMs {
    public class PersonaVM : BaseVM{

        //======================================================================================================================================
        // CONSTANTES
        //======================================================================================================================================
        private const int REGISTROS = 20;

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        public ICommand ComandoRecargarPersona => comandoRecargarPersona ??= comandoRecargarPersona = new ComandoDelegado(async () => await RecargarPersona());
        public ICommand CargarMas => cargarMas ??= cargarMas = new ComandoDelegado(async () => await CargarMasPeronas());
        public bool EstaRefrescando {
            get => estaRefrescando;
            set {
                estaRefrescando = value;
                OnPropertyChanged();
            }
        }
        public bool MostrarCargarMas {
            get => mostrarCargarMas;
            set {
                mostrarCargarMas = value;
                OnPropertyChanged();
            }
        }


        //======================================================================================================================================
        // VARIABLES
        //======================================================================================================================================
        private ICommand comandoRecargarPersona;
        private ICommand cargarMas;
        private bool estaRefrescando;
        private bool mostrarCargarMas;
        private int pagina = 1;


        //======================================================================================================================================
        // CONSTRUCTOR
        //======================================================================================================================================
        public PersonaVM() {

       
        }

        //======================================================================================================================================
        // MÉTODOS
        //======================================================================================================================================
        /// <summary>
        /// Inicializador de la carga de datos perezosa.
        /// </summary>
        public async void Inicializador() {
            await CargarPersona();
        }

        /// <summary>
        /// Carga las Personas.
        /// </summary>
        /// <returns>Devuelve un Task.</returns>
        private async Task CargarPersona() {
            using (UserDialogs.Instance.Loading("Cargando Persona...")) {
                List<PersonaM> sims = await WebAPI_Home.ObtenerSims(usuario.Iniciales, pagina, REGISTROS);
                sims.ForEach(sim => Sims.Add(sim));
                if (sims.Any()) { MostrarCargarMas = true; }
            }
        }

        /// <summary>
        /// Recarga los Sims.
        /// </summary>
        /// <returns>Devuelve un Task.</returns>
        private async Task RecargarPersona() {
            //MostrarCargarMas = false;
            //pagina = 1;
            //Sims.Clear();
            EstaRefrescando = false;
            await CargarPersona();
        }

        private async Task CargarMasPersona() {
            pagina += 1;
            await CargarPersona();
        }

    }
}
