using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using ARProject.Task;
using System;
using ARProject.User;
using ARProject.Group;
using UnityEngine.SceneManagement;

public class ListGroups : MonoBehaviour
{
    //Panel Main; Muestra los grupos, funciona como vista "Home" de MenuGrupos.
    public GameObject PanelGroupMain;
    //Panel Detail; muestra informacion detalle del grupo seleccionado.
    public GameObject PanelGroupDetail;

    public GameObject BtnBackMain;
    public GameObject BtnBackDetail;


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

       Group group = new Group();
        User user = new User();

        List<Group> groupBelongs = group.GetGroup(user);

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


            //activityNames[i] = "Actividad#" + i;
            activityNames[i] = groupBelongs[i].NameGroup;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            //descriptionText.text = "Nueva descripción";
            descriptionText.text = groupBelongs[i].School;

            // Actualizar la posición para el siguiente objeto de la actividad
            //  activityPosition += new Vector2(activitySize.x + spacing, 0f);
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
    }

    private void OnBackToScene()
    {
        
       // BtnSceneBack.gameObject.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangePanelToDetail()
    {
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);
    }

    public void ChangePanelToMain()
    {
        BtnBackDetail.SetActive(false);
        PanelGroupDetail.SetActive(false);
        PanelGroupMain.SetActive(true);
        BtnBackMain.SetActive(true);
    }
}