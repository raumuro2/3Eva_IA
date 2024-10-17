using _3Eva_IA.Acceso_a_datos;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Eva_IA.Models
{
    public class Configuracion
    {
        private static readonly object _lock = new object();
        public string Idioma { get; set; }
        public string TipoVoz { get; set; }

        public List<string> IdiomaList;
        public System.Collections.ObjectModel.ReadOnlyCollection<VoiceInfo> VoiceList;

        public Configuracion()
        {
            IdiomaList = getListIdiomasDisponibles();
            InitializeVoices();
            Idioma = "Español";
            TipoVoz = "";
        }


        //Singleton
        private static Configuracion _instance;
        private static Configuracion configuracion
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Configuracion();
                        }
                    }
                }
                return _instance;
            }
        }


        private List<string> getListIdiomasDisponibles()
        {
            return new List<string>
            {
                "Español", "English", "Deutchland"
            };
        }

        private async Task InitializeVoices()
        {
            VoiceList =  await AzureAPI.getVoices();
        }
    }
}
