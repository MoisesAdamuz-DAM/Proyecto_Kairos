using Kairos.Modelo;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Kairos.VMs.Personas {
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class ModificarVM : BaseVM {
        private PersonaM item;

        private string itemId;
        private string nombre;
        private string pais;
        private string ubicacion;
        private string necesidad;
        private string historial;

        public int Id { get; set; }

        public string Nombre {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
        public string Pais {
            get => pais;
            set => SetProperty(ref pais, value);
        }
        public string Ubicacion {
            get => ubicacion;
            set => SetProperty(ref ubicacion, value);
        }
        public string Necesidad {
            get => necesidad;
            set => SetProperty(ref necesidad, value);
        }
        public string Historial {
            get => historial;
            set => SetProperty(ref historial, value);
        }
        public string ItemId {
            get { return itemId; }
            set {
                itemId = value;
                //LoadItemId(value);
            }
        }
        public ModificarVM(PersonaM item) {
            LoadItemId(item);
        }

        public void LoadItemId(PersonaM item) {
            try {
                Id = item.id;
                Nombre = item.nombrePersona;
                Pais = item.paisOrigen;
                Ubicacion = item.ubicacionPersona;
                Necesidad = item.necesidadPersona;
                Historial = item.historialPersona;

            } catch (Exception) {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
