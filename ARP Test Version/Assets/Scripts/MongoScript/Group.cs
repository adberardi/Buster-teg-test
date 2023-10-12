using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARProject.User;
using UnityEngine.UI;
using System.Net.Mail;
using System.Net;

namespace ARProject.Group
{
    class Group
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public ObjectId _id { get; set; }
        public string NameGroup { get; set; }
        public string DateCreated { get; set; }
        public string Admin { get; set; }
        public string School { get; set; }
        public string LevelSchool { get; set; }
        public string[] ParticipantsGroup { get; set; }
        public string[] AssignedActivities { get; set; }
        public List<string> ListNewMembers = new List<string>();

        private MongoClient _client;

        public Group()
        {
            _client = MongoDBManager.GetClient();
        }

        public Group(ObjectId idGroup, string nameGroup, DateTime dateCreated, string admin, string school, string levelSchool)
        {
            _id = idGroup;
            NameGroup = nameGroup;
            DateCreated = dateCreated.ToString();
            Admin = admin;
            School = school;
            LevelSchool = levelSchool;
            ParticipantsGroup = new string[] { };
            AssignedActivities = new string[] { };
        }

        public IMongoCollection<Group> GetCollection()
        {
            var db = _client.GetDatabase("Mercurio");
            return db.GetCollection<Group>("Groups");
        }

        public ObjectId CreateGroup(Group newGroup)
        {
            Group registerGroup = new Group();
            registerGroup.GetCollection().InsertOne(newGroup);

            return newGroup._id;
        }

        // Valida si ya existe un grupo con; el mismo nombre (name), colegio (groupSchool), y grado escolar (levelSchool) del curso
        public bool ExistsSameGroup(string name, string groupSchool, string levelSchool)
        {
            IMongoCollection<Group> docRef = GetCollection();
            List<Group> result = docRef.Find(group => (group.NameGroup == name) && (group.School == groupSchool) && (group.LevelSchool == levelSchool)).ToList();
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void ReadGroup(string IdGroup)
        {
            IMongoCollection<Group> docRef = GetCollection();
            Group credential = docRef.Find(group => group._id == ObjectId.Parse(IdGroup)).ToList()[0];         
            NameGroup = credential.NameGroup;
            Admin = credential.Admin;
            School = credential.School;
            LevelSchool = credential.LevelSchool;
            DateCreated = credential.DateCreated;
            AssignedActivities = credential.AssignedActivities;
            ParticipantsGroup = credential.ParticipantsGroup;
            PlayerPrefs.SetString("NameGroup", NameGroup);
            PlayerPrefs.SetString("NameSchool",School);
            PlayerPrefs.SetString("LevelSchool",LevelSchool);
            Debug.Log(" Total Actividades asignadas: " + AssignedActivities.Length);
            //Debug.Log(string.Format("=> Longitud AssignedActivities: {0}", ParticipantsGroup.Count));
            //Debug.Log(string.Format("> Leyendo Grupo: Admin {0} | Name {1} | Participantes {2} | DateCreated {3} | AssignedActivities {4}", Admin, NameGroup, ParticipantsGroup.Count, DateCreated, AssignedActivities.Count));
            }


        public List<Group> GetGroup(User.User user)
        {
            List<string> groupList = user.GetAllGroupsFromUser();
            IMongoCollection<Group> docRef = GetCollection();

            List<Group> result = new List<Group>();
            Debug.Log("Total groupList: " + groupList.Count.ToString());
            int cont = 0;
            foreach (var index in groupList)
            {
                try
                {
                    Debug.Log("Dentro del Loop - Index:" + cont.ToString() + " | id: " + index.ToString());

                    Group credential = docRef.Find(group => group._id == ObjectId.Parse(index)).ToList()[0];
                    result.Add(credential);
                    cont++;
                }
                catch(ArgumentOutOfRangeException)
                {
                    Debug.Log("Index Error into GetGroup: " + index.ToString());
                }
                catch(PlatformNotSupportedException)
                {
                    Debug.Log("PlatformNotSupportedException Error into GetGroup: " + index.ToString());
                }
            }
            Debug.Log("Saliendo GetAllGroupsFromUser");

            return result;
        }

        public Group GetGroup(string IdGroup)
        {
            IMongoCollection<Group> docRef = GetCollection();
            Group credential = docRef.Find(group => group._id == ObjectId.Parse(IdGroup)).ToList()[0];
            return credential;
        }

        public async void UpdateGroup (string IdGroup, string newNameGroup, string newSchool)
        {
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var filterData = Builders<Group>.Filter.Eq(query => query._id, ObjectId.Parse(IdGroup));
                var dataToUpdate = Builders<Group>.Update.Set(query => query.NameGroup, newNameGroup)
                    .Set(query => query.School, newSchool);
                IMongoCollection<Group> groupRef = GetCollection();
                var result = await groupRef.UpdateOneAsync(filterData, dataToUpdate);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    Debug.Log("Operacion completada");
                }
                else
                {
                    Debug.Log("Operacion Fallida");
                }
            }
            catch(MongoException)
            {
                Debug.Log("Un error ha ocurido");
            }
        }

        public async void DeleteGroup(string IdGroup)
        {
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var deleteFilter = Builders<Group>.Filter.Eq("_id", ObjectId.Parse(IdGroup));
                DeleteResult result = await docRef.DeleteOneAsync(deleteFilter);

                if (result.IsAcknowledged & result.DeletedCount > 0)
                    Debug.Log("Grupo borrado exitosamente");
                else
                    Debug.Log("Error al borrar el grupo");

            }
            catch(MongoException)
            {
                Debug.Log("Error al borrar el grupo");
            }
        }

        // Valida los usuarios que que tengan el Toggle con estatus 'Checked'
        public void ProcessRequest(List<GameObject> activityObject, List<string> listDataMember, List<string> listDataMembersId)
        {
            for (int index = 0; index < activityObject.Count; index++)
            {
                if (activityObject[index].transform.Find("ToggleGreen").GetComponent<Toggle>().isOn)
                {
                    Debug.Log("ProcessRequest: Hay una persona con casilla marcada");
                    //SendNotificationByEmail(listDataMember[index], PlayerPrefs.GetString("NameGroup"),PlayerPrefs.GetString("NameSchool"),PlayerPrefs.GetString("LevelSchool"));
                    SendNotificationByEmail("antoberar@gmail.com", PlayerPrefs.GetString("NameGroup"), PlayerPrefs.GetString("NameSchool"), PlayerPrefs.GetString("LevelSchool"));

                    //Registra las personas que fueron seleccionadas que van a ser agregadas en el grupo.
                    //ListNewMembers.Add(listDataMember[index]);
                    Debug.Log("Validando cantidad de miembros a ingresar " + listDataMembersId[index]);
                    ListNewMembers.Add(listDataMembersId[index]);
                }
                else
                {
                    Debug.Log("ProcessRequest: NO hay una persona con casilla marcada");
                }
            }
            //Si por lo menos hay una persona que fue seleccionada, se procedera a registrarlo en la Base de Datos. En caso contrario, se hara caso omiso.
            if (ListNewMembers.Count > 0)
                AddMembersToGroup();
        }
        //Se registrara los miembros del grupo en la Base de Datos.
        public async void AddMembersToGroup()
        {
            string IdGroup = PlayerPrefs.GetString("IdGroupCreated");
            try
            {
                IMongoCollection<Group> docRef = GetCollection();
                var filterData = Builders<Group>.Filter.Eq(query => query._id, ObjectId.Parse(IdGroup));
                var dataToUpdate = Builders<Group>.Update.Set(query => query.ParticipantsGroup, ListNewMembers.ToArray());
                IMongoCollection<Group> groupRef = GetCollection();
                var result = await groupRef.UpdateOneAsync(filterData, dataToUpdate);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    Debug.Log("Operacion completada");
                }
                else
                {
                    Debug.Log("Operacion Fallida");
                }
            }
            catch (MongoException)
            {
                Debug.Log("Un error ha ocurido");
            }
        }

        private const string ElasticEmailApiUrl = "https://api.elasticemail.com/v2";
        private const string ElasticEmailApiKey = "E3AA2FEED5E74C762766D756CF1F5E046AE51141B33611EDC49E26920E88FFEC9DB04112DC172709EF1D39C72E9883B4"; // Reemplaza con tu clave de API de Elastic Email

        public void SendNotificationByEmail(string userEmail, string nameGroup, string nameSchool, string levelSchool)
        {
            string fromEmail = "antoberar@gmail.com"; // Reemplaza con tu dirección de correo electrónico

            // Create the email message
            MailMessage mailMessage = new MailMessage(fromEmail, userEmail, "Recuperación de contraseña", "Su contraseña es: " + nameGroup);
            mailMessage.IsBodyHtml = true;

            // Send the email using the Elastic Email API
            using (var httpClient = new WebClient())
            {
                var formData = new System.Collections.Specialized.NameValueCollection();
                formData["apikey"] = ElasticEmailApiKey;
                formData["from"] = fromEmail;
                formData["to"] = userEmail;
                formData["subject"] = "Te han agregado al grupo: "+nameGroup;
                formData["bodyHtml"] = "Ahora formas parte del grupo "+nameGroup+" del colegio "+nameSchool+" para el grado "+LevelSchool+"\n";
                httpClient.UploadValues($"{ElasticEmailApiUrl}/email/send", "POST", formData);
            }
        }
    }

    }

