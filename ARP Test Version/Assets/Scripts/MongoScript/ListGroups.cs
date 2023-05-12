﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using ARProject.Task;
using System;
using System.Linq;
using ARProject.User;
using ARProject.Group;
using UnityEngine.SceneManagement;
using System.Globalization;

public class ListGroups : MonoBehaviour
{
    //Panel Main; Muestra los grupos, funciona como vista "Home" de MenuGrupos.
    public GameObject PanelGroupMain;
    //Panel Detail; muestra informacion detalle del grupo seleccionado.
    public GameObject PanelGroupDetail;

    public GameObject BtnBackMain;
    public GameObject BtnBackDetail;
    List<Group> groupBelongs;
    public Text TextNameGroup;
    public Text TextSchool;
    public Text TextActivityActual;
    public Text TextTaskDescription;
    private Group group;
    private Task activity;


    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public string[] activityNames;      // Array de nombres para las actividades
    private GameObject AuxObj { get; set; }

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
        User user = new User();
        activity = new Task();

        string itemName = "Item ";
        groupBelongs = group.GetGroup(user);

        activitySprites = new Sprite[groupBelongs.Count];
        //activitySprites = new Sprite[dataGroup.Count];
        activityNames = new string[groupBelongs.Count];
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
            activityNames[i] = groupBelongs[i].NameGroup;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            descriptionText.text = groupBelongs[i].School;

        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangePanelToDetail(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);
        LoadDataPanel(groupBelongs[indexData]);
    }

    public void UpdatePosSCroll(Vector2 value)
    {
        Debug.Log("UpdatePosSCroll: "+value.y);
    }

    public void ChangePanelToMain()
    {
        BtnBackDetail.SetActive(false);
        PanelGroupDetail.SetActive(false);
        PanelGroupMain.SetActive(true);
        BtnBackMain.SetActive(true);
    }

    private void LoadDataPanel(Group groupItem)
    {
        TextActivityActual.text = "";
        TextNameGroup.text = "";
        TextSchool.text = "";
        TextTaskDescription.text = "";
        TextNameGroup.text = groupItem.NameGroup;
        TextSchool.text = groupItem.School;
        bool flag = true;
        List<Task> listDates = new List<Task>();
        for(int c = 0; (c < groupItem.AssignedActivities.Length) && flag; c++){
            Task nextDate = activity.GetTask(ObjectId.Parse(groupItem.AssignedActivities[c]));
            DateTime dateEndConvert = DateTime.ParseExact(nextDate.EndDate, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateStartConvert = DateTime.ParseExact(nextDate.StartDate, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            int resultStartDates = DateTime.Compare(dateStartConvert, DateTime.Now);
            int resultEndDates = DateTime.Compare(dateEndConvert, DateTime.Now);
            if (((resultStartDates < 0) || (resultStartDates == 0)) && !(resultEndDates < 0))
            {
                if (resultEndDates == 0)
                {
                    //La fecha es igual, en este caso es el dia actual.
                    //next = dateConvert;
                    flag = false;
                    TextActivityActual.text = dateEndConvert.ToString();
                    TextTaskDescription.text = nextDate.Description;
                }
                else
                {
                    //La fecha es anterior a la comparada.
                    listDates.Add(nextDate);
                }
            }
        }
        if ((flag) && (listDates.Count > 0))
        {
            var listSorted = listDates.OrderBy(x => x.EndDate);
            var result = listSorted.ToList()[0];
            TextActivityActual.text = result.EndDate.ToString();
            TextTaskDescription.text = result.Description.ToString();
        }
        else
        {
            TextActivityActual.text = "No hay actividad actual";
            TextTaskDescription.text = "No hay actividad actual";
        }
    }
}