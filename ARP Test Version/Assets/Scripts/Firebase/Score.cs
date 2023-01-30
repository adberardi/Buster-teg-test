using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;


namespace ARProject.Score
{
    public class Score
    {
        private int IdRecord { get; set; }
        private string IdUser { get; set; }
        private int Nota { get; set; }
        FirebaseFirestore db;
        // TODO: Por agregar clase Content
        private Content.Content Programa { get; set; }

        public Score()
        {

        }

        public Score (FirebaseFirestore db)
        {
            this.db = db;
        }

        public void SaveScore()
        {
            Content.Content aux = new Content.Content();
            DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser());
            Dictionary<string, object> score = new Dictionary<string, object>
        {
                { "calification", 10 },
                { "game", aux.GetContent() },
        };
            docRef.SetAsync(score).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data in the scores collection.");
            });
        }


        public void ReadScore()
        {
            CollectionReference docRef = db.Collection("Scores");
            Debug.Log("///VOY POR READ SCORE");
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                QuerySnapshot snapshot = task.Result;
                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    Dictionary<string, object> documentDic = doc.ToDictionary();
                    Debug.Log(string.Format("calification: {0}", documentDic["calification"]));
                    Debug.Log(String.Format("contents: {0}", documentDic["contents"]));
                    Debug.Log(String.Format("level: {0}", documentDic["level"]));
                }
                Debug.Log("Read all data from the scores collection.");
            });
        }

    }
}

