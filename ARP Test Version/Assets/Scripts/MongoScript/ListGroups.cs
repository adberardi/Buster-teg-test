using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using TaskUser = ARProject.Task;
using System;
using System.Linq;
using ARProject.User;
using ARProject.Group;
using ARProject.GamesPlayed;
using UnityEngine.SceneManagement;
using System.Globalization;
using Task = System.Threading.Tasks.Task;

public class ListGroups : MonoBehaviour
{
    //Panel Main; Muestra los grupos, funciona como vista "Home" de MenuGrupos.
    public GameObject PanelGroupMain;
    //Panel Detail; muestra informacion detalle del grupo seleccionado.
    public GameObject PanelGroupDetail;
    public GameObject PanelMembers;

    public GameObject BtnBackMain;
    public GameObject BtnBackDetail;
    List<Group> groupBelongs;
    public string[] userBelongs;
    public Text TextNameGroup;
    public Text TextSchool;
    public Text TextActivityActual;
    public Text TextTaskDescription;
    private Group group;
    private User user;
    private GamesPlayed record;
    private TaskUser.Task activity;


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

    public Sprite[] activitySpritesMember;    // Array de sprites para las actividades
    public string[] activityNamesMember;      // Array de nombres para las actividades

    // Variables to Scroll Bar for Member Group
    public ScrollRect scrollRectMemberGroup;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRectMemberGroup;  // Referencia al Rect Transform del contenido del Scroll Rect

    // Variables para la posición y el tamaño de los objetos generados
    private Vector2 activitySizeMember;
    private Vector2 activityPositionMember;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entrando a ListGroups");
        group = new Group();
        record = new GamesPlayed();
        user = new User();
        activity = new TaskUser.Task();


        groupBelongs = group.GetGroup(user);

        string itemName = "Item ";

        activitySprites = new Sprite[groupBelongs.Count];
        //activitySprites = new Sprite[1];
        //activitySprites = new Sprite[dataGroup.Count];
        activityNames = new string[groupBelongs.Count];
        //activityNames = new string[1];
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
            Debug.Log("En Loop");
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);
        
        if (PlayerPrefs.GetString("EnableGroupDetails") == "True")
        {
            ChangePanelToDetail(PlayerPrefs.GetInt("IndexData"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadListMembers()
    {
        Group ListMembersBelongs = group.GetGroup(PlayerPrefs.GetString("IDGroup"));
        userBelongs = ListMembersBelongs.ParticipantsGroup;
        List<User> userMember = user.GetStudents(userBelongs);
        string itemName = "Item ";

        activitySpritesMember = new Sprite[(int) userBelongs.LongCount()];
        activityNamesMember = new string[(int) userBelongs.LongCount()];
        Debug.Log("Longitud de las lista participantes: "+activitySprites.Length);
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
            activityNames[i] = userMember[i].FirstName +" "+ userMember[i].LastName;
            activityObject.GetComponentInChildren<Text>().text = activityNames[i];
            descriptionText.text = userMember[i].UserName;
            Debug.Log("En Loop");
        }
        activityPrefab.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRect.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRect.sizeDelta.y);

        if (PlayerPrefs.GetString("EnableGroupDetails") == "True")
        {
            ChangePanelToDetail(PlayerPrefs.GetInt("IndexData"));
        }
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangePanelToDetail(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        BtnBackDetail.SetActive(true);
        PanelMembers.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);
        PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
        PlayerPrefs.SetInt("IndexData",indexData);
        LoadDataPanel(groupBelongs[indexData]);
    }

    /*public void ChangePanelToDetail(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);
        PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
        PlayerPrefs.SetInt("IndexData", indexData);
        LoadDataPanel(groupBelongs[indexData]);
    }*/


    public void ChangePanelToDetail(int pos)
    {
        
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);
        PlayerPrefs.SetString("IDGroup", groupBelongs[pos]._id.ToString());
        LoadDataPanel(groupBelongs[pos]);
    }

    public void UpdatePosSCroll(Vector2 value)
    {
        Debug.Log("UpdatePosSCroll: "+value.y);
    }

    public void ChangePanelToMain()
    {
        PlayerPrefs.SetString("EnableGroupDetails", "False");
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
        List<GamesPlayed> container;
        container = record.ReadGamesPlayedGroup(ObjectId.Parse(user.GetSessionDataUser()), groupItem._id); 
        //container = await Task.Run(() => record.ReadGamesPlayedGroup(ObjectId.Parse("6411384514070dd6d438055b"), groupItem._id));
        bool flag = true;
        List<TaskUser.Task> listDates = new List<TaskUser.Task>();
        for(int c = 0; (c < groupItem.AssignedActivities.Length) && flag; c++){
            TaskUser.Task nextDate = activity.GetTask(ObjectId.Parse(groupItem.AssignedActivities[c]));
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
        Debug.Log("Valores recopilados en coleccion: " + container.Count);
    }

    public void LoadRecord(ObjectId idUser, ObjectId idGroup)
    {
        List<GamesPlayed> result = record.ReadGamesPlayedGroup(idUser, idGroup);
        Debug.Log("Valores recopilados en coleccion: " + result.Count);

    }
}