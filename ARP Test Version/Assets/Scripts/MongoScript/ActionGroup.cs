using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.User;
using ARProject.Group;

public class ActionGroup : MonoBehaviour
{
    private Group group;
    private User user;
    public GameObject item;
    public Transform Logo;
    public Text textOne;
    public Text textTwo;
    public Button btnIngresar;
    Transform NewParent;


    // Variables para los objetos a generar
    public GameObject activityPrefab;   // Prefab del objeto de la actividad
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    private string[] activityNames;      // Array de nombres para las actividades

    // Variables para el scroll bar
    public ScrollRect scrollRect;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRect;  // Referencia al Rect Transform del contenido del Scroll Rect
    public float spacing = 10f;         // Espaciado entre las actividades
    private Vector2 activitySize;
    private Vector2 activityPosition;

    // Start is called before the first frame update
    void Start()
    {
        group = new Group();
        user = new User();

        List<string> dataGroup = user.GetAllGroupsFromUser();
        List<Group> aux = new List<Group>();

        // Obtener el tamaño del objeto de la actividad
        activitySize = activityPrefab.GetComponent<RectTransform>().sizeDelta;

        // Calcular la posición inicial del primer objeto de la actividad
        activityPosition = new Vector2(spacing + activitySize.x / 2f, -spacing - activitySize.y / 2f);

        foreach (var index in dataGroup)
        {
            int i = 0;
            aux.Add(group.GetGroup(index));

            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefab, contentRect);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySprites[1];
            //activitySprites[i] = Resources.Load<Sprite>("Sprites/btn_square_blue_pressed");


            // Encontrar el componente de texto de la descripción de la actividad
            Text titleText = activityObject.transform.Find("Title").GetComponent<Text>();
            activityObject.transform.Find("Title").GetComponent<Text>().text = "Nuevo Titulo";
            Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();
            activityObject.transform.Find("Description").GetComponent<Text>().text = "U.E. Instituto Humanitas";
            // Cargar el sprite de la imagen
            //Sprite checkboxSprite = Resources.Load<Sprite>("Sprites/checkbox_checked_yellow");
            // ...
            // Asignar la imagen y el nombre de la actividad
            //activityObject.GetComponent<Image>().sprite = activitySprites[i];
            // ...
            // Asignar el sprite de la imagen del checkbox
            //activityObject.transform.Find("Checkbox").GetComponent<Image>().sprite = checkboxSprite;


            activityNames[i] = "Actividad#" + i;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            titleText.text = "Nuevo Titulo";
            descriptionText.text = "U.E. Instituto Humanitas";

            // Actualizar la posición para el siguiente objeto de la actividad
            //  activityPosition += new Vector2(activitySize.x + spacing, 0f);
            i++;
        }

        Debug.Log("activitySprites.Length " + activitySprites.Length);



        // Generar los objetos de la actividad
        /*for (int i = 0; i <= activitySprites.Length; i++)
        {



        }*/

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

}

//643359be46c68d2f7fe1c9be
//6411373b14070dd6d4380557
//