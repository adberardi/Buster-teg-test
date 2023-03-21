using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using ARProject.User;

public class MyDatabaseHandler
{
    private MongoClient _client;

    public MyDatabaseHandler()
    {
        _client = MongoDBManager.GetClient();
    }

    public IMongoCollection<MyDatabaseHandler> GetCollection()
    {
        var db = _client.GetDatabase("Mercurio");
        return db.GetCollection<MyDatabaseHandler>("User");
    }
}
