using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Model_User Sample
public class User
    {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { set; get; }
    public string Name { private set; get; }
       

        //Possible Methods ...
    }




