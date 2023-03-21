using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.AchievementClass
{
    class Achievement
    {
        public string DescAch { get; set; }
        public string GoalAch { get; set; }
        public string ImgAch { get; set; }


        /*public Achievement(FirebaseFirestore db)
        {
            this.db = db;
        }*/

        /*public void SaveAchievement(string idAchievement)
        {
            DocumentReference docRef = db.Collection("Achievement").Document(idAchievement);
            Dictionary<string, object> content = new Dictionary<string, object>
            {
                { "descAch", "TituloUno" },
                { "goalAch", "1"},
                { "imgAch", "Matematica/imagen"},
            };

            docRef.SetAsync(content).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Se registro de manera exitosa la logro");
            });
        }*/

        /*public void ReadAchievement(string idAchievement)
        {
            DocumentReference docRef = db.Collection("Achievement").Document(idAchievement);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> docContent = doc.ToDictionary();
                DescAch = docContent["descAch"].ToString();
                GoalAch = docContent["goalAch"].ToString();
                ImgAch = docContent["imgAch"].ToString();

                Debug.Log(string.Format("Description Achievement: {0}", DescAch));

            });

        }*/
    }
}
