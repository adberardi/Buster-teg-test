using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ARProject.Group
{
    class Group
    {
        private int IdGroup { get; set; }
        private string NombreGroup { get; set; }
        private DateTime FechaCreacion { get; set; }
        private DateTime FechaUpdate { get; set; }
        private int IdAutor { get; set; }
        // TODO: Por agregar clases Estudiante y Tarea
        private string Participante { get; set; }
        private string Asignacion { get; set; }


        public Group()
        {

        }

    }
}


