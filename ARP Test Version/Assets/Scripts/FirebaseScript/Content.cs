using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

namespace ARProject.Content
{
    class Content
    {
        private string IdContent { get; set; }
        private string TitleContent { get; set; }
        private string DescContent { get; set; }
        private string LevelContent { get; set; }
        private string GradeContent { get; set; }

        private FirebaseFirestore db;

        public Content()
        {

        }

        public Content(FirebaseFirestore db)
        {
            this.db = db;
        }

        public void SaveContent(string idContent)
        {
            DocumentReference docRef = db.Collection("Content").Document(idContent);
            Dictionary<string, object> content = new Dictionary<string, object>
            {
                { "descContent", "TituloUno" },
                { "level", "1"},
                { "titleContent", "Matematica"},
                {"gradeContent", "2" }
            };

            docRef.SetAsync(content).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa el Content");
            });
        }

        public void ReadContent(string idContent)
        {
            DocumentReference docRef = db.Collection("Content").Document(idContent);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> docContent = doc.ToDictionary();
                DescContent = docContent["descContent"].ToString();
                LevelContent = docContent["level"].ToString();
                TitleContent = docContent["titleContent"].ToString();
                GradeContent = docContent["gradeContent"].ToString();

                Debug.Log(string.Format("Description Content: {0}", DescContent));

            });

        }

        public Dictionary<string, string> GetContent()
        {
            return new Dictionary<string, string>
            {
                { "descContent", "TituloUno" },
                { "level", "1"},
                { "titleContent", "Matematica"},
            };
        }
    }
}


