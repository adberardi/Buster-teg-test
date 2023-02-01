using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System;


namespace ARProject.User
{
    class User
    {
        FirebaseAuth auth;

        [SerializeField]
        FirebaseFirestore db;
        public InputField emailField;
        public InputField passwField;


        private string IdUser { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string Role { get; set; }

        public string Profile { get; set; }

        public User (FirebaseAuth auth, InputField emailField, InputField passwField)
        {
            this.auth = auth;
            this.emailField = emailField;
            this.passwField = passwField;
        }

        public User ()
        {

        }

        public User (FirebaseAuth auth, FirebaseFirestore db)
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
                IdUser = newUser.UserId;
                //Username = usernameField;
                Email = emailField;
                Username = "Pepito123";
                FirstName = "Jaimito";
                LastName = "Perez";
                Profile = "Ruta Imagen Perfil";
                SaveSessionDataUser();
                SaveUser();
                Score.Score aux = new Score.Score(db);
                aux.SaveScore();
            });
        }

        public void Login(string emailField, string passwField)
        {
            if (ValidateInputFieldsLogin(emailField, passwField))
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
                    IdUser = userSignedUp.UserId;
                    SaveSessionDataUser();
                    ChangeScene(1);
                    ReadUser();
                });
            }
            else
            {
                Debug.Log(" CAMPOS DE ENTRADA VACIO(S)");
            }
                
        }

        public void Logout()
        {
            auth.SignOut();
            ChangeScene(0);
        }


        public void SaveUser()
        {
            DocumentReference docRef = db.Collection("Users").Document(IdUser);
            Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "birthday", DateTime.Now },
                { "email", emailField },
                { "firstName", FirstName },
                { "lastName", LastName },
                { "userName", Username},
                { "profile", Profile }
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


        public void ReadUser()
        {
            Debug.Log("ENTRANDO EN READUSER");
            DocumentReference docRef = db.Collection("Users").Document(GetSessionDataUser());
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> documentDic = doc.ToDictionary();
                Debug.Log(string.Format("Birthday: {0} , Email: {1}, FirstName: {2}, LastName: {3}, Username: {4}", documentDic["birthday"], documentDic["email"], documentDic["firstName"], documentDic["lastName"], documentDic["userName"]));
                Email = documentDic["email"].ToString();
                Username = documentDic["userName"].ToString();
                Birthday = Convert.ToDateTime(documentDic["birthday"]);
                FirstName = documentDic["firstName"].ToString();
                LastName = documentDic["lastName"].ToString();
                Profile = documentDic["profile"].ToString();
                Debug.Log("Read all data from the users collection.");
                
            });
            new Score.Score(db).ReadScore();
        }


        public void SaveSessionDataUser()
        {
            PlayerPrefs.SetString("IDUser", IdUser);
        }

        public string GetSessionDataUser()
        {
            return PlayerPrefs.GetString("IDUser");
        }

        public void ClearSessionDataUser()
        {
            PlayerPrefs.DeleteKey("IDUser");
        }

        public bool ValidateInputFieldsLogin(string emailField, string passwField)
        {
            if (emailField != "" && passwField != "")
                return true;
            return false;
        }

        public bool ValidateInputFieldsRegister(string usernameField, string birthdayField, string emailField, string passwField, string passwVerifyField)
        {
            if (usernameField != "" && birthdayField != "" && emailField != "" && passwField != "" && passwVerifyField != "")
                return true;
            return false;
        }
    }

    //Herencia
    class Docente: User
    {
        public Group.Group Salon { get; set; }
    }

    //Herencia
    class Estudiante: User
    {
        public Score.Score Nota { get; set; }
    }

}





