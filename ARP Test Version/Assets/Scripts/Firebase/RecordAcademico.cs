using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;


namespace ARProject.RecordAcademico
{
    public class RecordAcademico
    {
        private int IdRecord { get; set; }
        private string IdUser { get; set; }
        private int Nota { get; set; }
        FirebaseFirestore db;
        // TODO: Por agregar clase Contenido
        private Contenido.Contenido Programa { get; set; }

        public RecordAcademico()
        {

        }

        public RecordAcademico (FirebaseFirestore db)
        {
            this.db = db;
        }

        public void SaveScore()
        {
            Contenido.Contenido aux = new Contenido.Contenido();
            DocumentReference docRef = db.Collection("Scores").Document(new Usuario.Usuario().GetSessionDataUser());
            Dictionary<string, object> score = new Dictionary<string, object>
        {
                { "calification", 10 },
                { "game", aux.GetContenido() },
        };
            docRef.SetAsync(score).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data in the scores collection.");
            });
        }

    }
}

