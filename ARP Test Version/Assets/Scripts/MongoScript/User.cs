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
        public List<string> MemberGroup { get; set; }

        public User (string usernameField, string emailField, string passwField)
        {
            UserName = usernameField;
            Email = emailField;
            Password= passwField;
            Profile = "Default";
            StatusOnline = false;
            Birthday = DateTime.Now.ToString();
            MemberGroup = new List<string>();
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
            UserName = newUser.UserName;
            Email = newUser.Email;
            FirstName =newUser.FirstName;
            LastName = newUser.LastName;
            Profile = newUser.Profile;
            Birthday = newUser.Birthday;
            MemberGroup = newUser.MemberGroup;
            User registerUser = new User();
            registerUser.GetCollection().InsertOne(newUser);
        }

        public async void AddGroupToUser(ObjectId idGroup)
        {
            IMongoCollection<User> userCollection = GetCollection();
            User userModelList = userCollection.Find(user => user._id == ObjectId.Parse(GetSessionDataUser())).ToList()[0];
            userModelList.MemberGroup.Add(idGroup.ToString());
            var filterData = Builders<User>.Filter.Eq(query => query._id, ObjectId.Parse(GetSessionDataUser()));
            var dataToUpdate = Builders<User>.Update.Set(query => query.MemberGroup, userModelList.MemberGroup);
            var result = await userCollection.UpdateOneAsync(filterData, dataToUpdate);
            IsSuccessfullyOperation(result);
        }

        public async void DeleteGroupUser(string idGroup)
        {
            try
            {
                IMongoCollection<User> docRef = GetCollection();
                User userModelList = docRef.Find(user => user._id == ObjectId.Parse(GetSessionDataUser())).ToList()[0];
                userModelList.MemberGroup.Remove(idGroup);
                var filterData = Builders<User>.Filter.Eq(query => query._id, ObjectId.Parse(GetSessionDataUser()));
                var dataToUpdate = Builders<User>.Update.Set(query => query.MemberGroup, userModelList.MemberGroup);
                var result = await docRef.UpdateOneAsync(filterData, dataToUpdate);
                
            
                if (result.IsAcknowledged & result.ModifiedCount > 0)
                    Debug.Log("Grupo borrado exitosamente");
                else
                    Debug.Log("Error al borrar el grupo");
            }
            catch (MongoException)
            {
                Debug.Log("Error al borrar el grupo");
            }

        }

        public List<string> GetAllGroupsFromUser()
        {
            IMongoCollection<User> docRef = GetCollection();
            User userModelList = docRef.Find(user => user._id == ObjectId.Parse(GetSessionDataUser())).ToList()[0];
            return userModelList.MemberGroup;
        }

        public bool PassowrdRequirements(string passw)
        {
            if (passw.Length >= 5)
                return true;
            return false;
        }

        public void Login(string emailField, string passwField)
        {

            if (ValidateInputFieldsLogin(emailField, passwField) && PassowrdRequirements(passwField))
            {
                try
                {
                    
                    IMongoCollection<User> userCollection = GetCollection();
                    Debug.Log("USER.LOGIN");
                    //List<User> userModelList = userCollection.Find(user => true).ToList();
                    List<User> userModelList = userCollection.Find(user => user.Email == emailField).ToList();
                    
                    User credential = userModelList[0];
                    if (userModelList.Count > 0 && credential.Email == emailField && credential.Password == passwField)
                    {
                        //IdUser = userModelList[0]._id;
                        SaveSessionDataUser(credential._id);
                        ChangeScene(1);
                       // AddToGroup(GetSessionDataUser(),new ObjectId());
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

        public bool IsSuccessfullyOperation(UpdateResult result)
        {
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                Debug.Log("La operacion resulto exitosa");
                return true;
            }
            else
            {
                Debug.Log("La operacion fallo");
                return false;
            }
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
                IsSuccessfullyOperation(result);
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
            User credential = docRef.Find(task => task._id == ObjectId.Parse(GetSessionDataUser())).ToList()[0];
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

        public void EmailExist(string emailToValidate)
        {
            IMongoCollection<User> userCollection = GetCollection();
            User credential = userCollection.Find(user => user.Email.Equals(emailToValidate)).ToList()[0];
            //var filterData = Builders<Group>.Filter.Eq(query => query._id, emailToValidate);
            Debug.Log(credential.Email);
        }


    }

}





