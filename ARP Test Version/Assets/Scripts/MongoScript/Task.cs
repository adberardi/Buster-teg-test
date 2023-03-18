using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ARProject.Task
{
    class Task
    {
        private string GroupTask { get; set; }
        private string ContentTask { get; set; }
        private int PointsTask { get; set; }
        private string PercentageTask { get; set; }
        private DateTime EndDate { get; set; }
        private DateTime StartDate { get; set; }

        /*public Task(FirebaseFirestore db)
        {
            this.db = db;
        }*/

        /*public void SaveTask()
        {
            DocumentReference docRef = db.Collection("Task").Document("SegundaTarea");
            Dictionary<string, object> taskToAdd = new Dictionary<string, object>
        {
                { "contentTask", "Content/SumayRestaUnaUnidad" },
                { "endDate", DateTime.Now.AddDays(4) },
                { "groupTask", "Groups/DUyBw4Yt733EHeSyNGRh" },
                { "percentageTask", 15 },
                { "pointTask", 20},
                { "startDate", DateTime.Now }
        };
            docRef.SetAsync(taskToAdd).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data in the task collection.");
            });
        }*/

        /*public async System.Threading.Tasks.Task UpdateTaskAsync()
        {
            DocumentReference taskRef = db.Collection("Task").Document("PrimeraTarea");
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "pointTask", 15 }
            };
            await taskRef.UpdateAsync(updates);
        }*/

        /*public void ReadTask()
        {
            Debug.Log("ENTRANDO EN READTASK");
            DocumentReference docRef = db.Collection("Task").Document("PrimeraTarea");
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> documentDic = doc.ToDictionary();
                Debug.Log(string.Format("groupTask: {0} ", documentDic["groupTask"]));
                ContentTask = documentDic["contentTask"].ToString();
                EndDate = DateTime.Parse(documentDic["endDate"].ToString());
                StartDate = DateTime.Parse(documentDic["startDate"].ToString());
                GroupTask = documentDic["groupTask"].ToString();
                PercentageTask = documentDic["percentageTask"].ToString();
                PointsTask = int.Parse(documentDic["pointTask"].ToString());
                Debug.Log("Read all data from the Task collection.");

            });
        }*/
    }
}


