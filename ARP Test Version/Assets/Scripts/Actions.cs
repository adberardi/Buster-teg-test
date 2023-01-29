using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.Usuario;
using Firebase.Auth;
using Firebase.Firestore;

public class Actions : MonoBehaviour
{
    FirebaseAuth auth;
    public InputField email;
    public InputField passw;
    public InputField userName;
    public InputField firstName;
    public InputField lastName;
    private static Usuario user;
    // Start is called before the first frame update
    void Start()
    {
        //auth = FirebaseAuth.DefaultInstance;
       user = new Usuario(FirebaseAuth.DefaultInstance, FirebaseFirestore.DefaultInstance);
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
        user.CreateUser(email.text, "userName.text", passw.text, "firstName.text", "lastName.text");
    }

    public void Login()
    {
        
        user.Login(email.text, passw.text);
    }

    public void Logout()
    {
        user.Logout();
        //auth.SignOut();
        //ChangeScene(0);
    }
}
