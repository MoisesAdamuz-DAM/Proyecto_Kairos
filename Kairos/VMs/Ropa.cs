using Kairos.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kairos.VMs {
    public class Ropa {

        public ObservableCollection<PersonaM> persona { get; set; }

        public Ropa() {

            persona = new ObservableCollection<PersonaM> {

                new PersonaM{nombrePersona = "Luis", paisOrigen = "Rumanía", ubicacionPersona = "Ato", necesidadPersona="Agua",historialPersona="Familia"},
                new PersonaM{nombrePersona = "Luis", paisOrigen = "Rumanía", ubicacionPersona = "Ato", necesidadPersona="Agua",historialPersona="Familia"},
                new PersonaM{nombrePersona = "Luis", paisOrigen = "Rumanía", ubicacionPersona = "Ato", necesidadPersona="Agua",historialPersona="Familia"}
            };
        }
    }
}
