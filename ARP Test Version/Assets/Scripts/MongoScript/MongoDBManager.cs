using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

public class MongoDBManager
{
    private static readonly MongoClient _client = new MongoClient("mongodb+srv://zilus13:canuto13@cluster0.ds89fgp.mongodb.net/?retryWrites=true&w=majority");

    private MongoDBManager() { }

    public static MongoClient GetClient()
    {
        Debug.Log("ENTRANDO mongo"+ _client);

        return _client;
    }
}
