using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

namespace ARProject.RewardClass
{
    class Reward
    {
        public string DescReward { get; set; }
        FirebaseFirestore db;

        public Reward(FirebaseFirestore db)
        {
            this.db = db;
        }

        public void SaveContent(string idReward)
        {
            DocumentReference docRef = db.Collection("Reward").Document(idReward);
            Dictionary<string, object> content = new Dictionary<string, object>
            {
                { "descReward", "TituloUnoReward" },
            };

            docRef.SetAsync(content).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa el Content");
            });
        }

        public void ReadReward(string idReward)
        {
            DocumentReference docRef = db.Collection("Reward").Document(idReward);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> docContent = doc.ToDictionary();
                DescReward = docContent["descReward"].ToString();

                Debug.Log(string.Format("Description Content: {0}", DescReward));

            });
        }
    }
}
