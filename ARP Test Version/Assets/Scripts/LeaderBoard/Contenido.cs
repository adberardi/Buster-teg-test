using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARProject;

namespace ARProject
{
    class Contenido
    {
        private int IdContenido { get; set; }
        private string Titulo { get; set; }
        // TODO: Por agregar clase Temario
        private Temario Tema { get; set; }

        private string Nivel { get; set; }
        private string Grado { get; set; }

        public Contenido()
        {

        }
    }
}


