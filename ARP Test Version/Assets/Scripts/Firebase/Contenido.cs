using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARProject.Temario;

namespace ARProject.Contenido
{
    class Contenido
    {
        private int IdContenido { get; set; }
        private string Titulo { get; set; }
        // TODO: Por agregar clase Temario
        private Temario.Temario Tema { get; set; }

        private string Nivel { get; set; }
        private string Grado { get; set; }

        public Contenido()
        {

        }

        public Dictionary<string, string> GetContenido()
        {
            return new Dictionary<string, string>
            {
                { "Titulo", "TituloUno" },
                { "Temario", "Matematica"},
                { "Nivel", "1"},
            };
        }
    }
}


