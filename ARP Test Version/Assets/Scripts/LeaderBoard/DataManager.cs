using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    string databaseUrl = "https://tesis-database-5fd22-default-rtdb.firebaseio.com/";

    DatabaseReference reference;


    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    private void WriteUser(string userID, int score)
    {
        LeaderBoardEntry leaderBoard = new LeaderBoardEntry(userID, score);
        string dataJson = JsonUtility.ToJson(leaderBoard);
        reference.Child("users").Child(userID).SetRawJsonValueAsync(dataJson);
    }

    public void WriteNewScore(string userID, int score)
    {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = reference.Child("scores").Push().Key;
        LeaderBoardEntry leaderBoardEntry = new LeaderBoardEntry(userID, score);
        Dictionary<string, object> entryValues = leaderBoardEntry.ListScore();
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/scores/" + key] = entryValues;
        childUpdates["/user-scores/" + userID + "/" + key] = entryValues;
        reference.UpdateChildrenAsync(childUpdates).ContinueWith(task => 
        {
            if (task.IsFaulted)
            {
                Debug.Log("Ha ocurrido un error: "+task.Exception.ToString());
            }

            if (task.IsCanceled)
            {
                Debug.Log("El proceso de actualizacion ha sido cancelado: "+task.Exception.ToString());
            }
        });
    }


    private void ReadScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("scores").GetValueAsync().ContinueWith(task => 
        {
            if (task.IsFaulted)
            {
                Debug.Log("Ha ocurrido un error: " + task.Exception.ToString());
            }
            else if (task.IsCanceled)
            {
                Debug.Log("El proceso de actualizacion ha sido cancelado: " + task.Exception.ToString());
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Reading from Scores!");
                foreach (var item in (Dictionary<string, object>) snapshot.Value)
                {
                    Dictionary<string, object> attributes = (Dictionary<string, object>)item.Value;
                    Debug.LogFormat(" -> Item {0}", item.Value);
                    foreach (var score in attributes)
                    {
                        Debug.LogFormat(" --> Item {0} - {1}", score.Key, score.Value);
                    }
                }
            }
        });
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            //Do something
        }
    }

    public void InvocarWriteNewScore()
    {
        WriteNewScore("usuario1", 20);
    }
}
