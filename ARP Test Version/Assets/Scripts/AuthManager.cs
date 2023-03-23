using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using MongoDB.Driver;
using System.Collections.Generic;

public class AuthManager : MonoBehaviour
{
   
    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    public Text warningRegisterText;

    private static User user;

    public MongoClient client;
    public IMongoDatabase db;




    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        user = new User();
    }

    //Function for the login button
    public void LoginButton()
    {
       
        Debug.Log("entreeeee "+ emailLoginField.text+" Password: "+ passwordLoginField.text);
        user.Login(emailLoginField.text, passwordLoginField.text);
    }

    //Function for the register button
    public void RegisterButton()
    {
        SceneManager.LoadScene("Registro");
        //yield return "";
        ////Call the register coroutine passing the email, password, and username
        //StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    


    //Function for the save button
    public void SaveDataButton()
    {
       

        
    }
    public void Login(string _email, string _password)
    {
        Debug.Log("AuthManager Login");
        //user.Login(_email, _password);
        SceneManager.LoadScene("Home");

    }


    public void CreateUser()
    {
        //if (passwordRegisterField.text == passwordRegisterVerifyField.text)
        if (passwordRegisterField.text == "123456789")
        {
            User newUser = new User(usernameRegisterField.text, emailRegisterField.text, passwordRegisterField.text);
            user.CreateUser(newUser);
        }
        else
        {
            Debug.Log("Las contrasenas no coinciden");
        }
        SceneManager.LoadScene("Login");
    }

}

