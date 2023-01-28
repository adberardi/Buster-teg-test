using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;
using System;


namespace ARProject.Usuario
{
    class Usuario
    {
        FirebaseAuth auth;
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

        public Usuario (FirebaseAuth auth)
        {
            this.auth = auth;
        }

        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void CreateUser(string emailField, string passwField)
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





