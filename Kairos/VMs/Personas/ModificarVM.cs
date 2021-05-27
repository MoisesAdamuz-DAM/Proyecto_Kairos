using Kairos.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.VMs.Personas {
    public class ModificarVM : BaseVM{

        //======================================================================================================================================
        // PROPIEDADES
        //======================================================================================================================================
        public ObservableCollection<PersonaM> Respuestas { get; set; } = new();
        public PersonaM PersonaM { get; }

        //======================================================================================================================================
        // VARIABLE
        //======================================================================================================================================
        private readonly int idUsuario;

        //======================================================================================================================================
        // CONSTRUCTOR
        //======================================================================================================================================
        public ModificarVM(PersonaM personaM, int idUsuario) {
            PersonaM = personaM;
            this.idUsuario = idUsuario;
        }

        //======================================================================================================================================
        // MÉTODOS
        //======================================================================================================================================
        /// <summary>
        /// Inicializa la carga perezosa.
        /// </summary>
        /// <returns>Devuelve un Task.</returns>
        public async Task Inicializador() {
            await CargarRespuestas();
        }

        /// <summary>
        /// Carga las respuestas del Sim.
        /// </summary>
        /// <returns>Devuelve un Task.</returns>
        private async Task CargarRespuestas() {
            //List<PersonaM> respuestas = await (PersonaM.id, idUsuario);
            //respuestas.ForEach(respuesta => Respuestas.Add(respuesta));
        }
    }
}
