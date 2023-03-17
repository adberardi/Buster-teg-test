using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ARProject.User
{
    class User
    {

        [SerializeField]
        //protected FirebaseFirestore db;
        public InputField emailField;
        public InputField passwField;

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Role { get; set; }
        public string Profile { get; set; }
        public bool StatusOnline { get; set; }

        public IMongoDatabase db;

        /*public User (FirebaseAuth auth, InputField emailField, InputField passwField)
        {
            this.auth = auth;
            this.emailField = emailField;
            this.passwField = passwField;
        }*/

        public User (IMongoDatabase db)
        {
            this.db = db;
        }

        /*public User (FirebaseAuth auth, IMongoDatabase db)
        {
            this.auth = auth;
            this.db = db;
        }*/

        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        /*public void CreateUser(string emailField, string usernameField, string passwField, string firstnameField, string lastnameField)
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
            });
        }*/

        public void Login(string emailField, string passwField)
        {

            if (ValidateInputFieldsLogin(emailField, passwField))
            {
                try
                {
                    IMongoCollection<User> userCollection = db.GetCollection<User>("User");
                    List<User> userModelList = userCollection.Find(user => true).ToList();
                    User credential = userModelList[0];
                    if (userModelList.Count > 0 && credential.Email == emailField && credential.Password == passwField)
                    {
                        //IdUser = userModelList[0]._id;
                        SaveSessionDataUser(credential._id);
                        ChangeScene(1);
                    }
                }
                catch(MongoException)
                {
                    Debug.Log("USUARIO NO EXISTE O PASSWORD CREDENCIALES NO EXISTEN");
                }
            }
            else
            {
                Debug.Log(" CAMPOS DE ENTRADA VACIO(S)");
            }
                
        }

        public void Logout()
        {
            //auth.SignOut();
            StatusOnline = false;
            ChangeScene(0);
        }


        public void SaveUser()
        {
 /*           DocumentReference docRef = db.Collection("Users").Document(IdUser);
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
            */
        }

        public async System.Threading.Tasks.Task UpdateUserAsync()
        {
 /*           DocumentReference cityRef = db.Collection("Users").Document(GetSessionDataUser());
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "email", "Solo probando" }
            };
            await cityRef.UpdateAsync(updates);

            // You can also update a single field with: await cityRef.UpdateAsync("Capital", false);
            */
        }


        public void ReadUser()
        {
            Debug.Log("ENTRANDO EN READUSER");
/*            DocumentReference docRef = db.Collection("Users").Document(GetSessionDataUser());
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> documentDic = doc.ToDictionary();
                Debug.Log(string.Format("Birthday: {0} , Email: {1}, FirstName: {2}, LastName: {3}, Username: {4}", documentDic["birthday"], documentDic["email"], documentDic["firstName"], documentDic["lastName"], documentDic["userName"]));
                Email = documentDic["email"].ToString();
                Username = documentDic["userName"].ToString();
                Birthday = DateTime.Parse(documentDic["birthday"].ToString());
                FirstName = documentDic["firstName"].ToString();
                LastName = documentDic["lastName"].ToString();
                Profile = documentDic["profile"].ToString();
                Debug.Log("Read all data from the users collection.");
                
            });
            AchievementClass.Achievement ach = new AchievementClass.Achievement(db);
            ach.SaveAchievement("Nuevocontenido");
            ach.ReadAchievement("Nuevocontenido");
            */
        }


        public void SaveSessionDataUser(ObjectId IdUser)
        {
            PlayerPrefs.SetString("IDUser", IdUser.ToString());
            StatusOnline = true;
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

        /* GetAllClassRoom: Gets the role from the specific classrooms (Admin or Student). */
        public string GetRoleUser(string idClassRoom)
        {
            return "";
            /*DocumentReference collRef = db.Collection("Groups").Document(idClassRoom);
            collRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot doc = task.Result;
                Dictionary<string, object> p = doc.ToDictionary();
                if (p["admin"].ToString() == IdUser)
                {
                    Role = "Admin";
                }
                else
                {
                    Role = "Student";
                }
            });
            Debug.Log("GetRoleUser: " + Role);
            return Role;
            */
        }

        /* GetAllClassRoom: Gets all the classrooms where the user is the administrator. */
        public Dictionary<string, string> GetAllClassRoom()
        {
            Dictionary<string, string> aux = new Dictionary<string, string>();
            return null;
            /*CollectionReference collRef = db.Collection("Groups");
            collRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                QuerySnapshot query = task.Result;
                foreach(DocumentSnapshot doc in query.Documents)
                {
                    Dictionary<string, object> p = doc.ToDictionary();
                    if (p["admin"].ToString() == IdUser)
                    {
                        aux.Add(p["name"].ToString(), p["id"].ToString());
                    }
                }
            });
            Debug.Log(string.Format("GetAllClassRoom: Longitud List: {0}", aux.Count));
            return aux;
            */
        }


    }

}





