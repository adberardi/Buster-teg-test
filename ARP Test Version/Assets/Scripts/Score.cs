//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase.Firestore;
//using Firebase.Extensions;


//namespace ARProject.Score
//{
//    public class Score
//    {
//        private int IdRecord { get; set; }
//        private string IdUser { get; set; }
//        private int Nota { get; set; }
//        FirebaseFirestore db;
//        // TODO: Por agregar clase Content
//        private Content.Content Programa { get; set; }

//        public Score()
//        {

//        }

//        public Score (FirebaseFirestore db)
//        {
//            this.db = db;
//        }

//        public void SaveScore()
//        {
//            Content.Content aux = new Content.Content();
//            //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser());
//            DocumentReference docRef = db.Collection("Scores").Document("prueba");
//            Dictionary<string, object> score = new Dictionary<string, object>
//        {
//                { "username", "user01" },
//        };
//            docRef.SetAsync(score).ContinueWithOnMainThread(task =>
//            {
//                Debug.Log("Added data in the scores collection.");
//                CreateSubColeccionPersonalGame();
//            });
//        }


//        public void CreateSubColeccionPersonalGame()
//        {
//            //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser()).Collection("PersonalGame").Document();
//            DocumentReference docRef = db.Collection("Scores").Document("prueba").Collection("test").Document();
//            Dictionary<string, object> subcoll = new Dictionary<string, object>
//            {
//                { "dayPlayed", DateTime.Now.ToString() },
//                { "finalScore", 14 },
//                { "finalTimer", "01:34:00" }
//            };
//            docRef.SetAsync(subcoll).ContinueWithOnMainThread(task =>
//            {
//                Debug.Log("Added data in the scores collection.");
//            });
//            ReadScore();
//        }



//        public void ReadScore()
//        {
//            //DocumentReference docRef = db.Collection("Scores").Document(new User.User().GetSessionDataUser());
//            DocumentReference docRef = db.Collection("Scores").Document("prueba");
//            Debug.Log("///VOY POR READ SCORE");
//            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//            {

//                DocumentSnapshot doc = task.Result;
//                Dictionary<string, object> documentDic = doc.ToDictionary();
//                Debug.Log(string.Format("user: {0}", documentDic["username"]));
//                Debug.Log("Read all data from the scores collection.");
//            });

//            CollectionReference subcollRef = docRef.Collection("test");
//            subcollRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//            {
//                QuerySnapshot subcoll = task.Result;
//                foreach (DocumentSnapshot sub in subcoll.Documents)
//                {
//                    Dictionary<string, object> data = sub.ToDictionary();
//                    Debug.Log(string.Format("     _> Subcolleccion: DAYPLAYED {0} ", data["dayPlayed"]));
//                }
//            });
//        }

//    }
//}

