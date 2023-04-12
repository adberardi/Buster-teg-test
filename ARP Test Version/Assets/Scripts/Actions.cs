using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using MongoDB.Driver;

public class Actions : MonoBehaviour
{
    public InputField email;
    public InputField passw;
    public InputField userName;
    public InputField firstName;
    public InputField lastName;
    private static User user;
    public IMongoDatabase db;
    // Start is called before the first frame update
    void Start()
    {
        //auth = FirebaseAuth.DefaultInstance;
        //user = new User(FirebaseAuth.DefaultInstance, db);
        user = new User();
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
        //user.CreateUser(email.text, "userName.text", passw.text, "firstName.text", "lastName.text");
    }

    public void Login()
    {
        
        //user.Login(email.text, passw.text);
    }

    public void Logout()
    {
        user.Logout();
        //auth.SignOut();
        //ChangeScene(0);
    }


}
