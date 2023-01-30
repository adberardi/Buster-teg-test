using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Content
{
    class Content
    {
        private int IdContent { get; set; }
        private string Titulo { get; set; }

        private string Nivel { get; set; }
        private string Grado { get; set; }

        public Content()
        {

        }

        public Dictionary<string, string> GetContent()
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


