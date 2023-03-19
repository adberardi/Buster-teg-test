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

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        //public string Role { get; set; }
        public string Profile { get; set; }
        public bool StatusOnline { get; set; }

        public User (string usernameField, string emailField, string passwField)
        {
            UserName = usernameField;
            Email = emailField;
            Password= passwField;
            Profile = "Default";
            StatusOnline = false;
            Birthday = DateTime.Now.ToString();
            FirstName = "Dora";
            LastName = "Rodriguez";
        }

        private MongoClient _client;

        public User()
        {
            _client = MongoDBManager.GetClient();
        }

        public IMongoCollection<User> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<User>("User");
        }

        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void CreateUser(User newUser)
        {
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.UserName, newUser._id);
            //_id = newUser._id;
            UserName = newUser.UserName;
            Email = newUser.Email;
            FirstName =newUser.FirstName;
            LastName = newUser.LastName;
            Profile = newUser.Profile;
            Birthday = newUser.Birthday;
            User registerUser = new User();
            registerUser.GetCollection().InsertOne(newUser);
        }

        public void Login(string emailField, string passwField)
        {

            if (ValidateInputFieldsLogin(emailField, passwField))
            {
                try
                {
                    IMongoCollection<User> userCollection = GetCollection();
                    List<User> userModelList = userCollection.Find(user => true).ToList();
                    User credential = userModelList[0];
                    if (userModelList.Count > 0 && credential.Email == emailField && credential.Password == passwField)
                    {
                        //IdUser = userModelList[0]._id;
                        SaveSessionDataUser(credential._id);
                        ChangeScene(1);
                    }
                    else
                    {
                        Debug.Log("USUARIO NO EXISTE O CREDENCIALES NO COINCIDEN");
                    }
                }
                catch(MongoExecutionTimeoutException)
                {
                    Debug.Log("TIEMPO AGOTADO DE ESPERA - ERROR DE CONEXION");
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

        //Almacena los cambios datos de un usuario en especifico
        public async void SaveUser(string IdUser, string newFirstName)
        {
            try
            {
                var filterData = Builders<User>.Filter.Eq(query => query._id, ObjectId.Parse(IdUser));
                var dataToUpdate = Builders<User>.Update.Set(query => query.FirstName, newFirstName);
                IMongoCollection<User> userCollection = GetCollection();
                var result = await userCollection.UpdateOneAsync(filterData, dataToUpdate);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    Debug.Log("La operacion resulto exitosa");
                }
                else
                {
                    Debug.Log("La operacion fallo");
                }
            }
            catch(MongoException)
            {
                Debug.Log("Un error ha ocurrido");
            }
        }


        public void ReadUser()
        {
            Debug.Log("ENTRANDO EN READUSER");
            IMongoCollection<User> docRef = GetCollection();
            //IMongoCollection<User> userCollection = GetCollection();
            User credential = docRef.Find(task => task._id.ToString() == GetSessionDataUser()).ToList()[0];
            Debug.Log(string.Format("Birthday: {0} , Email: {1}, FirstName: {2}, LastName: {3}, Username: {4}", credential.Birthday, credential.Email, credential.FirstName, credential.LastName, credential.UserName));
            Email = credential.Email;
            UserName = credential.UserName;
            Birthday = credential.Birthday;
            FirstName = credential.FirstName;
            LastName = credential.LastName;
            Profile = credential.Profile;
            Debug.Log("Read all data from the users collection.");
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





