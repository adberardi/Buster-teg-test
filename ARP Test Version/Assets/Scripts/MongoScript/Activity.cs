using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ARProject.Activity
{
    class Activity
    {
        public string name;
        public Sprite sprite;

        public Activity(string name, Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }

    }

}





