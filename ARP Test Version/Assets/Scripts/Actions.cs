using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.Usuario;
using Firebase.Auth;

public class Actions : MonoBehaviour
{
    FirebaseAuth auth;
    public InputField email;
    public InputField passw;
    private Usuario user;
    // Start is called before the first frame update
    void Start()
    {
        //auth = FirebaseAuth.DefaultInstance;
       user = new Usuario(FirebaseAuth.DefaultInstance);
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
        user.CreateUser(email.text, passw.text);
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
