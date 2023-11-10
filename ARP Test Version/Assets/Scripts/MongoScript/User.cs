using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

namespace ARProject.User
{
    public class User
    {

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LevelSchool { get; set; }
        public string Birthday { get; set; }
        public string Profile { get; set; }
        public bool StatusOnline { get; set; }
        public int Reward { get; set; }
        public List<string> MemberGroup { get; set; }
        public string School { get; set; }

        public User (string usernameField, string emailField, string passwField, string birthday, string firstName, string lastName, string levelSchool, string school)
        {
            UserName = usernameField;
            Email = emailField;
            Password= passwField;
            Profile = "Default";
            StatusOnline = false;
            Birthday = birthday;
            //Birthday = DateTime.Now.ToString();
            MemberGroup = new List<string>();
            FirstName = firstName;
            LastName = lastName;
            LevelSchool = levelSchool;
            School = school;
            Reward = 0;
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
            //IsSuccessfullyOperation(result);
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

        public void DeleteUser()
        {
            try
            {
                IMongoCollection<User> docRef = GetCollection();
                docRef.DeleteOne(user => user._id == ObjectId.Parse(GetSessionDataUser()));
                Debug.Log("DeleteUser - Usuario Borrado");
            }
            catch (MongoException)
            {
                Debug.Log("DeleteUser - Error al borrar Usuario");
            }

        }

        public List<string> GetAllGroupsFromUser()
        {
            IMongoCollection<User> docRef = GetCollection();
            //Debug.Log("Adentro de GetAllGroupsFromUser");
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
                        SaveSessionDataUser(credential._id, credential.UserName, credential.FirstName, credential.LastName, credential.Birthday, credential.Email, credential.Reward, credential.School, credential.LevelSchool);
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
                catch(ArgumentOutOfRangeException)
                {
                    Debug.Log("Usuario no encontrado");
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
                ChangeScene(1);
                return true;
            }
            else
            {
                Debug.Log("La operacion fallo");
                return false;
            }
        }

        //Se actualiza los datos del usuario, excluyendo la contrasena.
        private async void UpdateUser(FilterDefinition<User> filterData, string newFirstName, string newLastName, string newEmail, string newBirthday, string newProfile, string newGroupSchool, string newLevelSchool)
        {
            try
            {
                IMongoCollection<User> userCollection = GetCollection();
                var dataToUpdate = Builders<User>.Update.Set(query => query.FirstName, newFirstName)
                    .Set(query => query.LastName, newLastName)
                    .Set(query => query.Email, newEmail)
                    .Set(query => query.Birthday, newBirthday)
                    .Set(query => query.Profile, newProfile)
                    .Set(query => query.LevelSchool, newLevelSchool)
                    .Set(query => query.School, newGroupSchool);
                var result = await userCollection.UpdateOneAsync(filterData, dataToUpdate);
                IsSuccessfullyOperation(result);
                //Falta implementar el borrado de un estudiante en un colegio y agregarlo en el nuevo.
            }
            catch(MongoException)
            {
                Debug.Log("Se ha generado un error al actualizar en la Base de Datos");
            }

        }

        //Se actualiza los datos del usuario, incluyendo la contrasena.
        private async void UpdateUser(FilterDefinition<User> filterData, string newFirstName, string newLastName, string newPassword, string newEmail, string newBirthday, string newProfile, string groupSchool, string levelSchool)
        {
            IMongoCollection<User> userCollection = GetCollection();
            var dataToUpdate = Builders<User>.Update.Set(query => query.FirstName, newFirstName)
                .Set(query => query.LastName, newLastName)
                .Set(query => query.Email, newEmail)
                .Set(query => query.Birthday, newBirthday)
                .Set(query => query.Password, newPassword)
                .Set(query => query.Profile, newProfile)
                .Set(query => query.School,groupSchool)
                .Set(query => query.LevelSchool ,levelSchool);
            var result = await userCollection.UpdateOneAsync(filterData, dataToUpdate);
            IsSuccessfullyOperation(result);
        }

        //Almacena los cambios datos de un usuario en especifico
        public void SaveUser(string newFirstName, string newLastName, string newPassword, string newEmail, string newBirthday, string newProfile, string groupSchool, string levelSchool)
        {
            try
            {
                IMongoCollection<User> userCollection = GetCollection();
                var filterData = Builders<User>.Filter.Eq(query => query._id, ObjectId.Parse(GetSessionDataUser()));
                if (newEmail == GetDataUser()["Email"])
                {
                    //Si el correo es el mismo que el actual, no se procede a validar si existe algun otro correo igual.
                    if (newPassword != "")
                    {
                        UpdateUser(filterData, newFirstName, newLastName, newPassword, newEmail, newBirthday, newProfile, groupSchool, levelSchool);
                    }
                    else
                    {
                        UpdateUser(filterData, newFirstName, newLastName, newEmail, newBirthday, newProfile, groupSchool, levelSchool);
                    }
                }
                else
                {
                    if (EmailExist(newEmail) == false)
                    {
                        // Si el correo ingresado es distinto al actual, se procede a validar que no exista algun correo igual, es decir, que sea igual a false
                        if (newPassword != "")
                        {
                            UpdateUser(filterData, newFirstName, newLastName, newPassword, newEmail, newBirthday, newProfile, groupSchool, levelSchool);
                        }
                        else
                        {
                            UpdateUser(filterData, newFirstName, newLastName, newEmail, newBirthday, newProfile, groupSchool, levelSchool);
                        }
                    }
                }
                ChangeScene(1);
            }
            catch(MongoException error)
            {
                Debug.Log("Un error ha ocurrido "+ error);
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
            Reward = credential.Reward;
            LevelSchool = credential.LevelSchool;
            School = credential.School;
            Debug.Log("Read all data from the users collection.");
        }


        public void SaveSessionDataUser(ObjectId IdUser, string userName, string firstName, string lastName, string birthday, string email, int reward, string school, string levelSchool)
        {
            PlayerPrefs.SetString("IDUser", IdUser.ToString());
            PlayerPrefs.SetString("Username", userName);
            PlayerPrefs.SetString("Firstname", firstName);
            PlayerPrefs.SetString("Lastname",lastName);
            PlayerPrefs.SetString("Birthday", birthday);
            PlayerPrefs.SetString("Email", email);
            PlayerPrefs.SetString("UserReward", reward.ToString());
            PlayerPrefs.SetString("School", school);
            PlayerPrefs.SetString("LevelSchool", levelSchool);

            StatusOnline = true;
        }

        public string GetSessionDataUser()
        {
            return PlayerPrefs.GetString("IDUser");
            //return "6411384514070dd6d438055b";
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

        public bool EmailExist(string emailToValidate)
        {
            IMongoCollection<User> userCollection = GetCollection();
            User credential = userCollection.Find(user => user.Email.Equals(emailToValidate)).ToList()[0];
            //var filterData = Builders<Group>.Filter.Eq(query => query._id, emailToValidate);
            Debug.Log(credential.Email);
            return false;
        }


        public Dictionary<string, string> GetDataUser()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "Username", PlayerPrefs.GetString("Username") },
                { "Firstname", PlayerPrefs.GetString("Firstname") },
                { "Lastname", PlayerPrefs.GetString("Lastname") },
                { "Birthday", PlayerPrefs.GetString("Birthday") },
                { "Email", PlayerPrefs.GetString("Email") }
            };

            return data;
        }



        public List<UserReward> GetUserRewards()
        {
            List<UserReward> userRewards = new List<UserReward>();

            MongoClient client = MongoDBManager.GetClient();
            IMongoCollection<User> userCollection = client.GetDatabase("Mercurio").GetCollection<User>("User");
            List<User> users = userCollection.Find(_ => true).ToList();

            foreach (User user in users)
            {
                UserReward userReward = new UserReward
                {
                    UserName = user.UserName,
                    Reward = user.Reward
                };

                userRewards.Add(userReward);
            }

            // Ordenar la lista por Reward de forma descendente
            userRewards = userRewards.OrderByDescending(u => u.Reward).ToList();

            return userRewards;
        }


        public List<User> GetStudents(List<ObjectId> listParam)
        {
            List<User> data = new List<User>();
            IMongoCollection<User> userCollection = GetCollection();
            Debug.Log("User - GetStudents | Valor de listParam: " + listParam[0]);
            for (int index = 0; index < listParam.Count; index++)
            {
                User credential = userCollection.Find(user => user._id == listParam[0]).ToList()[0];
                data.Add(credential);
            }
            //var filterData = Builders<Group>.Filter.Eq(query => query._id, emailToValidate);
            //Debug.Log(credential.Email);
            return data;
        }


        internal List<User> GetStudents(string[] listParam)
        {
            List<User> data = new List<User>();
            IMongoCollection<User> userCollection = GetCollection();
            foreach (var index in listParam)
            {
                User credential = userCollection.Find(user => user._id == ObjectId.Parse(index)).ToList()[0];
                data.Add(credential);
            }
            //var filterData = Builders<Group>.Filter.Eq(query => query._id, emailToValidate);
            //Debug.Log(credential.Email);
            return data;
        }
    }

}




public class UserReward
{
    public string UserName { get; set; }
    public int Reward { get; set; }
}



