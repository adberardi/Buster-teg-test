using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using MongoDB.Driver;
using ARProject.School;
using System.Linq;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Net;

public class AuthManager : MonoBehaviour
{

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    private static User user;

    private School school;

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

    public void Login(string emailField, string passwField)
    {

        if (user.ValidateInputFieldsLogin(emailField, passwField) && user.PassowrdRequirements(passwField))
        {
            try
            {

                IMongoCollection<User> userCollection = user.GetCollection();
                Debug.Log("USER.LOGIN");
                //List<User> userModelList = userCollection.Find(user => true).ToList();
                List<User> userModelList = userCollection.Find(user => user.Email == emailField).ToList();

                User credential = userModelList[0];
                if (userModelList.Count > 0 && credential.Email == emailField && credential.Password == passwField)
                {
                    //IdUser = userModelList[0]._id;
                    user.SaveSessionDataUser(credential._id, credential.UserName, credential.FirstName, credential.LastName, credential.Birthday, credential.Email, credential.Reward, credential.School, credential.LevelSchool);
                    user.ChangeScene(1);
                    // AddToGroup(GetSessionDataUser(),new ObjectId());
                }
                else
                {
                    Debug.Log("USUARIO NO EXISTE O CREDENCIALES NO COINCIDEN");
                    emailLoginField.text = "";
                    passwordLoginField.text = "";
                }
            }
            catch (MongoExecutionTimeoutException)
            {
                Debug.Log("TIEMPO AGOTADO DE ESPERA - ERROR DE CONEXION");
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("Usuario no encontrado");
            }
        }
        else
        {
            Debug.Log(" CAMPOS DE ENTRADA VACIO(S)");
        }

    }

    //Function for the login button
    public void LoginButton()
    {
       
        Debug.Log("entreeeee "+ emailLoginField.text+" Password: "+ passwordLoginField.text);
        //user.Login(emailLoginField.text, passwordLoginField.text);
        Login(emailLoginField.text, passwordLoginField.text);
    }

    //Function for the register button
    public void RegisterButton()
    {
        SceneManager.LoadScene("Registro");
    }


    /*public void Login(string _email, string _password)
    {
        Debug.Log("AuthManager Login");
        //user.Login(_email, _password);
        SceneManager.LoadScene("Home");

    } */

    bool ValidateBirthDate(string birthDateString)
    {
        DateTime birthDate;
        if (DateTime.TryParseExact(birthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
        {
            return true;
        }
        else
        {
            return false;
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


    // Function to validate user email, check if it exists, and send password via email
    public void ValidateEmailAndSendPassword()
    {
        string email = emailLoginField.text;

        // Check if the email is valid
        if (!ValidateEmail(email))
        {
            warningLoginText.text = "La dirección de correo electrónico no es válida.";
            return;
        }

        // Check if the email exists in the database
        bool emailExists = CheckEmailExistsInDatabase(email);
        if (!emailExists)
        {
            warningLoginText.text = "El correo electrónico ingresado no está registrado.";
            return;
        }

        // Get the user from the database
        User user = GetUserByEmail(email);

        // Send the password to the user's email
        SendPasswordByEmail(user.Email, user.Password);

        // Display a confirmation message
        confirmLoginText.text = "Se ha enviado un correo electrónico con la contraseña.";
    }

    // Function to check if the email exists in the database
    private bool CheckEmailExistsInDatabase(string email)
    {
        MongoClient client = MongoDBManager.GetClient();
        IMongoCollection<User> userCollection = client.GetDatabase("Mercurio").GetCollection<User>("User");
        User user = userCollection.Find(u => u.Email == email).FirstOrDefault();
        return user != null;
    }

    // Function to get the user from the database by email
    private User GetUserByEmail(string email)
    {
        MongoClient client = MongoDBManager.GetClient();
        IMongoCollection<User> userCollection = client.GetDatabase("Mercurio").GetCollection<User>("User");
        User user = userCollection.Find(u => u.Email == email).FirstOrDefault();
        return user;
    }

    private const string ElasticEmailApiUrl = "https://api.elasticemail.com/v2";
    private const string ElasticEmailApiKey = "E3AA2FEED5E74C762766D756CF1F5E046AE51141B33611EDC49E26920E88FFEC9DB04112DC172709EF1D39C72E9883B4"; // Reemplaza con tu clave de API de Elastic Email

    public void SendPasswordByEmail(string userEmail, string password)
    {
        string fromEmail = "luisgermenher@gmail.com"; // Reemplaza con tu dirección de correo electrónico

        // Create the email message
        MailMessage mailMessage = new MailMessage(fromEmail, userEmail, "Recuperación de contraseña", "Su contraseña es: " + password);
        mailMessage.IsBodyHtml = true;

        // Send the email using the Elastic Email API
        using (var httpClient = new WebClient())
        {
            var formData = new System.Collections.Specialized.NameValueCollection();
            formData["apikey"] = ElasticEmailApiKey;
            formData["from"] = fromEmail;
            formData["to"] = userEmail;
            formData["subject"] = "Recuperación de contraseña";
            formData["bodyHtml"] = "Su contraseña es: " + password;
            httpClient.UploadValues($"{ElasticEmailApiUrl}/email/send", "POST", formData);
        }
    }

}

