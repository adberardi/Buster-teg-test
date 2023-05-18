﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using ARProject.User;
using ARProject.GamesPlayed;
using ARProject.Group;
using TaskUser = ARProject.Task;

public class ListRecord : MonoBehaviour
{

    private User user;
    private Group group;
    private GamesPlayed record;
    private TaskUser.Task activity;
    List<GamesPlayed> recordBelongs;

    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public string[] activityNames;      // Array de nombres para las actividades

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
        group = new Group();
        record = new GamesPlayed();
        user = new User();
        activity = new TaskUser.Task();

        string itemName = "Item ";
        recordBelongs = record.ReadGamesPlayedGroup(ObjectId.Parse("6411384514070dd6d438055b"), ObjectId.Parse("6411373b14070dd6d4380557"));

        activitySprites = new Sprite[recordBelongs.Count];
        //activitySprites = new Sprite[dataGroup.Count];
        activityNames = new string[recordBelongs.Count];
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
            activitySprites[i] = Resources.Load<Sprite>("Sprites/btn_square_blue");

            // Encontrar el componente de texto de la descripción de la actividad
            Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[i];


            itemName = i.ToString();
            activityObject.name = itemName;
            activityNames[i] = "ACtividad #"+i;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            //descriptionText.text = recordBelongs[i].School;

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
