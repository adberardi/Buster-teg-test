using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Database;
using System;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    string databaseUrl = "https://tesis-database-5fd22-default-rtdb.firebaseio.com/";

    [SerializeField]
    private string pathCollectionUser = "users";

    [SerializeField]
    private string pathDocUser = "alovelace";

    DatabaseReference reference;
    FirebaseFirestore db;


    // Start is called before the first frame update
    void Start()
    {
        //reference = FirebaseDatabase.DefaultInstance.RootReference;
        db = FirebaseFirestore.DefaultInstance;
    }

    public void SaveData()
    {
        DocumentReference docRef = db.Collection(pathCollectionUser).Document(pathDocUser);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "First", "Ada" },
            { "Last", "Lovelace" },
            { "Born", 1815 },
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }

    public void ReadData()
    {
        CollectionReference docRef = db.Collection(pathCollectionUser);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach(DocumentSnapshot doc in snapshot.Documents)
            {
                Debug.Log(string.Format("User: {0}", doc.Id));
                Dictionary<string, object> documentDic = doc.ToDictionary();
                Debug.Log(string.Format("First: {0}", documentDic["First"]));
                if (documentDic.ContainsKey("Middle"))
                {
                    Debug.Log(string.Format("Middle: {0}", documentDic["Middle"]));
                }
                Debug.Log(String.Format("Last: {0}", documentDic["Last"]));
                Debug.Log(String.Format("Born: {0}", documentDic["Born"]));
            }
            Debug.Log("Read all data from the users collection.");
        });
    }
}
