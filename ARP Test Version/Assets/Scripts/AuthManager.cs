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

    public const string MONGO_URI = "mongodb+srv://zilus13:canuto13@cluster0.ds89fgp.mongodb.net/?retryWrites=true&w=majority";
    public const string DATABASE_NAME = "Mercurio";
    public MongoClient client;
    public IMongoDatabase db;




    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //auth = FirebaseAuth.DefaultInstance;
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        user = new User(db);
    }

    private void InitializeFirebase()
    {

    }

    //Function for the login button
    public void LoginButton()
    {
       
        Debug.Log("entreeeee "+ emailLoginField.text+" Password: "+ passwordLoginField.text);
        //Call the login coroutine passing the email and password


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
        //user.CreateUser(emailRegisterField.text, "usernameRegisterField", passwordRegisterField.text, "firstName.text", "lastName.text");
    }

    //private IEnumerator Register(string _email, string _password, string _username)
    //{
    //    if (_username == "")
    //    {
    //        //If the username field is blank show a warning
    //        warningRegisterText.text = "Missing Username";
    //    }
    //    else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
    //    {
    //        //If the password does not match show a warning
    //        warningRegisterText.text = "Password Does Not Match!";
    //    }
    //    else 
    //    {
    //        //Call the Firebase auth signin function passing the email and password
    //        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
    //        //Wait until the task completes
    //        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

    //        if (RegisterTask.Exception != null)
    //        {
    //            //If there are errors handle them
    //            Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
    //            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
    //            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

    //            string message = "Register Failed!";
    //            switch (errorCode)
    //            {
    //                case AuthError.MissingEmail:
    //                    message = "Missing Email";
    //                    break;
    //                case AuthError.MissingPassword:
    //                    message = "Missing Password";
    //                    break;
    //                case AuthError.WeakPassword:
    //                    message = "Weak Password";
    //                    break;
    //                case AuthError.EmailAlreadyInUse:
    //                    message = "Email Already In Use";
    //                    break;
    //            }
    //            warningRegisterText.text = message;
    //        }
    //        else
    //        {
    //            //User has now been created
    //            //Now get the result
    //            User = RegisterTask.Result;

    //            if (User != null)
    //            {
    //                //Create a user profile and set the username
    //                UserProfile profile = new UserProfile{DisplayName = _username};

    //                //Call the Firebase auth update user profile function passing the profile with the username
    //                var ProfileTask = User.UpdateUserProfileAsync(profile);
    //                //Wait until the task completes
    //                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

    //                if (ProfileTask.Exception != null)
    //                {
    //                    //If there are errors handle them
    //                    Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
    //                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
    //                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
    //                    warningRegisterText.text = "Username Set Failed!";
    //                }
    //                else
    //                {
    //                    //Username is now set
    //                    //Now return to login screen
    //                    UIManager.instance.LoginScreen();
    //                    warningRegisterText.text = "good";
    //                }
    //            }
    //        }
    //    }
    //}

    //private IEnumerator UpdateUsernameAuth(string _username)
    //{
    //    //Create a user profile and set the username
    //    UserProfile profile = new UserProfile { DisplayName = _username };

    //    //Call the Firebase auth update user profile function passing the profile with the username
    //    var ProfileTask = User.UpdateUserProfileAsync(profile);
    //    //Wait until the task completes
    //    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

    //    if (ProfileTask.Exception != null)
    //    {
    //        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
    //    }
    //    else
    //    {
    //        //Auth username is now updated
    //    }
    //}

    //private IEnumerator UpdateUsernameDatabase(string _username)
    //{
    //    int oldCount = 2;
    //    // Using Structs
    //    Counter counter = new Counter
    //    {
    //        Count = oldCount + 1,
    //        UpdatedBy = "Vikings"
    //    };
    //    Debug.Log("entreeeee en la funcion ");
    //    DocumentReference countRef = db.Collection("counters").Document("counter");
    //    countRef.SetAsync(counter).ContinueWithOnMainThread(task =>
    //    {
    //        Debug.Log("Updated Counter");
    //        // GetData();
    //    });

    //    yield return "";
    //}
}

