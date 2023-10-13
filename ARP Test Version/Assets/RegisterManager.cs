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

public class RegisterManager : MonoBehaviour
{
    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField firstNameField;
    public InputField lastNameField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    public InputField birthDateRegisterField;
    public Text warningRegisterText;
    public GameObject WarningBackground;
    private static User user;
    private School school;
    public Dropdown dropdown;
    private string GroupSchool { get; set; }
    private string LevelSchool { get; set; }
    public GameObject FormOne;
    public GameObject FormTwo;
    public MongoClient client;
    public IMongoDatabase db;



    void Awake()
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        user = new User();
        school = new School();
        dropdown.options.Clear();
        IMongoCollection<School> docRef = school.GetCollection();
        List<School> result = docRef.Find(Builders<School>.Filter.Empty).ToList();
        //Llena la lista desplegable.
        foreach (var i in result)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = i.SchoolName });
        }
    }

    //Obtiene la escuela seleccionada cuando se crea el grupo.
    public void DropdownitemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        GroupSchool = dropdown.options[index].text;
    }

    //Obtiene el grado seleccionado por el usuario.
    public void DropdownLeveltemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        LevelSchool = dropdown.options[index].text.Trim();
        Debug.Log("LevelSchool: " + LevelSchool);
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

    //Change from FormOne to FormTwo
    public void ChangeFormOneToFormTwo()
    {
        FormTwo.SetActive(true);
        FormOne.SetActive(false);
    }

    //Change from FormTwo to FormOne
    public void ChangeFormTwoToFormOne()
    {
        FormOne.SetActive(true);
        FormTwo.SetActive(false);
    }


    public void CreateUser()
    {
        string password = passwordRegisterField.text;

        if (ValidatePassword(password))
        {
            if (ValidateEmail(emailRegisterField.text))
            {
                if (ValidateBirthDate(birthDateRegisterField.text))
                {
                    User newUser = new User(usernameRegisterField.text, emailRegisterField.text, password, birthDateRegisterField.text, firstNameField.text, lastNameField.text, LevelSchool);
                    user.CreateUser(newUser);

                    SceneManager.LoadScene("Login");
                }
                else
                {
                    warningRegisterText.text = "La fecha de nacimiento no es válida. Asegúrese de que la fecha de nacimiento esté en el formato correcto (DD/MM/AAAA).";
                    WarningBackground.SetActive(true);
                }
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
        string email = emailRegisterField.text;

        // Check if the email exists in the database
        bool emailExists = CheckEmailExistsInDatabase(email);
       /* if (!emailExists)
        {
            warningLoginText.text = "El correo electrónico ingresado no está registrado.";
            return;
        }*/

        // Get the user from the database
        User user = GetUserByEmail(email);

        // Send the password to the user's email
        SendPasswordByEmail(user.Email, user.Password);

        // Display a confirmation message
        //confirmLoginText.text = "Se ha enviado un correo electrónico con la contraseña.";
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
