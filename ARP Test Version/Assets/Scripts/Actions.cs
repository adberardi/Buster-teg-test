using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject;

public class Actions : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    public InputField email;
    public InputField passw;
    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void CreateUser()
    {

        auth.CreateUserWithEmailAndPasswordAsync(email.text, passw.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void Login()
    {
        Usuario user = new Usuario(auth);
        user.Login(email.text, passw.text);
    }

    public void Logout()
    {
        auth.SignOut();
        ChangeScene(0);
    }
}
