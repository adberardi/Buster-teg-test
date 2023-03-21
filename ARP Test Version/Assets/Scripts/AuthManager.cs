using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

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
    public GameObject WarningBackground;
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
    }




    //Function for the save button
    public void CloseWarning()
    {
        WarningBackground.SetActive(false);


    }
    public void Login(string _email, string _password)
    {
        Debug.Log("AuthManager Login");
        //user.Login(_email, _password);
        SceneManager.LoadScene("Home");

    }


    public void CreateUser()
    {
        string password = passwordRegisterField.text;

        if (ValidatePassword(password))
        {
            if (ValidateEmail(emailRegisterField.text))
            {
                User newUser = new User(usernameRegisterField.text, emailRegisterField.text, password);
                user.CreateUser(newUser);

                SceneManager.LoadScene("Login");
            }
            else
            {
                warningRegisterText.text = "La dirección de correo electrónico no es válida. Asegúrese de que la dirección de correo electrónico esté en el formato correcto (ejemplo@ejemplo.com).";
                WarningBackground.SetActive(true);
            }
        }
        else
        {
            warningRegisterText.text = "La contraseña no cumple con los requisitos mínimos. Asegúrese de que la contraseña tenga al menos 8 caracteres, contenga al menos un número, una letra mayúscula y una letra minúscula.";
            WarningBackground.SetActive(true);
        }

    }

    public bool ValidateEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    public bool ValidatePassword(string password)
    {
        // Requerir una longitud mínima de 8 caracteres
        if (password.Length < 8)
        {
            return false;
        }

        // Requerir al menos un número
        if (!password.Any(char.IsDigit))
        {
            return false;
        }

        // Requerir al menos una letra mayúscula
        if (!password.Any(char.IsUpper))
        {
            return false;
        }

        // Requerir al menos una letra minúscula
        if (!password.Any(char.IsLower))
        {
            return false;
        }

        return true;
    }


}

