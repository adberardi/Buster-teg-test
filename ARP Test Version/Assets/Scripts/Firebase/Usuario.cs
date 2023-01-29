using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System;


namespace ARProject.Usuario
{
    class Usuario
    {
        FirebaseAuth auth;

        [SerializeField]
        FirebaseFirestore db;
        public InputField emailField;
        public InputField passwField;


        private string IdUsuario { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string Role { get; set; }

        public Usuario (FirebaseAuth auth, InputField emailField, InputField passwField)
        {
            this.auth = auth;
            this.emailField = emailField;
            this.passwField = passwField;
        }

        public Usuario ()
        {

        }

        public Usuario (FirebaseAuth auth, FirebaseFirestore db)
        {
            this.auth = auth;
            this.db = db;
        }

        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void CreateUser(string emailField, string usernameField, string passwField, string firstnameField, string lastnameField)
        {

            auth.CreateUserWithEmailAndPasswordAsync(emailField, passwField).ContinueWith(task => {
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
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                IdUsuario = newUser.UserId;
                //Username = usernameField;
                Email = emailField;
                Username = "Pepito123";
                FirstName = "Jaimito";
                LastName = "Perez";
                SaveSessionDataUser();
                SaveUser();
                RecordAcademico.RecordAcademico aux = new RecordAcademico.RecordAcademico(db);
                aux.SaveScore();
            });
        }

        public void Login(string emailField, string passwField)
        {
            auth.SignInWithEmailAndPasswordAsync(emailField, passwField).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser userSignedUp = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    userSignedUp.DisplayName, userSignedUp.UserId);
                IdUsuario = userSignedUp.UserId;
                ChangeScene(1);
            });
        }

        public void Logout()
        {
            auth.SignOut();
            ChangeScene(0);
        }


        public void SaveUser()
        {
            DocumentReference docRef = db.Collection("Users").Document(IdUsuario);
            Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "birthday", DateTime.Now },
                { "email", emailField },
                { "firstName", FirstName },
                { "lastName", LastName },
                { "userName", Username}
        };
            docRef.SetAsync(user).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data to the alovelace document in the users collection.");
            });
        }

        public async System.Threading.Tasks.Task UpdateUserAsync()
        {
            DocumentReference cityRef = db.Collection("Users").Document(GetSessionDataUser());
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "email", "Solo probando" }
            };
            await cityRef.UpdateAsync(updates);

            // You can also update a single field with: await cityRef.UpdateAsync("Capital", false);
        }


        public void SaveSessionDataUser()
        {
            PlayerPrefs.SetString("IDUser", IdUsuario);
        }

        public string GetSessionDataUser()
        {
            return PlayerPrefs.GetString("IDUser");
        }

        public void ClearSessionDataUser()
        {
            PlayerPrefs.DeleteKey("IDUser");
        }
    }

    //Herencia
    class Docente: Usuario
    {
        public Grupo.Grupo Salon { get; set; }
    }

    //Herencia
    class Estudiante: Usuario
    {
        public RecordAcademico.RecordAcademico Nota { get; set; }
    }

}





