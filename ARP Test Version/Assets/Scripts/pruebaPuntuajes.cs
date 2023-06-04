﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using ARProject.Task;
using System;
using ARProject.User;
using ARProject.Group;

public class pruebaPuntuajes : MonoBehaviour
{
    


    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public string[] activityNames;      // Array de nombres para las actividades
    public string[] activityNames2;      // Array de nombres para las actividades

    // Variables para el scroll bar
    public ScrollRect scrollRect;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRect;  // Referencia al Rect Transform del contenido del Scroll Rect
    public float spacing = 10f;         // Espaciado entre las actividades

    // Variables para la posición y el tamaño de los objetos generados
    private Vector2 activitySize;
    private Vector2 activityPosition;
  
    // Start is called before the first frame update
    void Start()
    {
        //string iduser=PlayerPrefs.GetString("IDUser");
       
        
         
        User dataUser = new User();
        var userRewards = dataUser.GetUserRewards();



            
 


        activitySprites = new Sprite[userRewards.Count];
        activityNames = new string[userRewards.Count];
        activityNames2 = new string[userRewards.Count];
        Debug.Log(activitySprites.Length);
       // Obtener el tamaño del objeto de la actividad
       activitySize = activityPrefab.GetComponent<RectTransform>().sizeDelta;

        // Calcular la posición inicial del primer objeto de la actividad
        activityPosition = new Vector2(spacing + activitySize.x / 2f, -spacing - activitySize.y / 2f);

        // Generar los objetos de la actividad
        for (int i = 0; i < activitySprites.Length; i++)
        {
            
           
            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefab, contentRect);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];
            activitySprites[i] = Resources.Load<Sprite>("Sprites/btn_small_green");

            //dataTask = newTask.GetTaskById(assignedActivitiesList[i]);

            // Encontrar el componente de texto de la descripción de la actividad
            Text descriptionText = activityObject.transform.Find("Text").GetComponent<Text>();
            Text rewardNumber = activityObject.transform.Find("rewardText").GetComponent<Text>();
            // Cargar el sprite de la imagen
           
            // ...
            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];
            // ...
            // Asignar el sprite de la imagen del checkbox
          

            activityNames[i] = userRewards[i].UserName;
            activityNames2[i] = userRewards[i].Reward.ToString();
            Debug.Log(userRewards[i].UserName);

            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            //string pointTaskAsString = dataTask.PointTask.ToString();
            //descriptionText.text = dataTask.Description;
           rewardNumber.text = activityNames2[i];
          
            // Actualizar la posición para el siguiente objeto de la actividad
            //  activityPosition += new Vector2(activitySize.x + spacing, 0f);
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
