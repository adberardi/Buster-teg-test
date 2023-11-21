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
using TaskAssigned = ARProject.Task;

public class ListGroups : MonoBehaviour
{
    //Panel Main; Muestra los grupos, funciona como vista "Home" de MenuGrupos.
    public GameObject PanelGroupMain;
    //Panel Detail; muestra informacion detalle del grupo seleccionado.
    public GameObject PanelGroupDetail;
    public GameObject PanelMembers;
    public GameObject PanelStatistics;
    public GameObject PanelActivities;
    public GameObject BtnActivities;
    public GameObject BtnCreateActivities;
    public GameObject BtnBackMain;
    public GameObject BtnBackDetail;
    public GameObject BtnBackToMember;
    List<Group> groupBelongs;
    List<User> userMember;
    Group ListMembersBelongs;
    private string GameAssigned { get; set; }
    private int RewardAssigned { get; set; }
    private string NameAssigned { get; set; }
    private string StartDateAssigned { get; set; }
    private string EndDateAssigned { get; set; }
    public string[] userBelongs;
    public Text TextNameGroup;
    public Text TextSchool;
    public Text TextActivityActual;
    public Text TextTaskDescription;
    private Group group;
    private User user;
    private GamesPlayed record;
    private TaskUser.Task activity;
    public GraphScript graphScript;


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
    public GameObject activityPrefabMember;   // Prefab del objeto de la actividad
    public Sprite[] activitySpritesMember;    // Array de sprites para las actividades
    public string[] activityNamesMember;      // Array de nombres para las actividades

    // Variables to Scroll Bar for Member Group
    public ScrollRect scrollRectMemberGroup;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRectMember;  // Referencia al Rect Transform del contenido del Scroll Rect

    // Variables para la posición y el tamaño de los objetos generados
    private Vector2 activitySizeMember;
    private Vector2 activityPositionMember;

    private void Awake()
    {
        graphScript = GetComponent<GraphScript>();
    }


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

    public void LoadListMembers(int indexData)
    {
        PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
        ListMembersBelongs = group.GetGroup(PlayerPrefs.GetString("IDGroup"));
        userBelongs = ListMembersBelongs.ParticipantsGroup;
        userMember = user.GetStudents(userBelongs);
        string itemName = "Item ";

        activitySpritesMember = new Sprite[(int) userBelongs.LongCount()];
        activityNamesMember = new string[(int) userBelongs.LongCount()];
        Debug.Log("Longitud de las lista participantes: "+activitySprites.Length);
        // Obtener el tamaño del objeto de la actividad
        activitySize = activityPrefabMember.GetComponent<RectTransform>().sizeDelta;

        // Calcular la posición inicial del primer objeto de la actividad
        activityPosition = new Vector2(spacing + activitySize.x / 2f, -spacing - activitySize.y / 2f);
        // Generar los objetos de la actividad
        for (int i = 0; i < activitySpritesMember.Length; i++)
        {
            // Instanciar el prefab de la actividad
            GameObject activityObject = Instantiate(activityPrefabMember, contentRectMember);

            // Establecer la posición del objeto de la actividad
            activityObject.GetComponent<RectTransform>().anchoredPosition = activityPosition;

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySpritesMember[i];
            activitySpritesMember[i] = Resources.Load<Sprite>("Sprites/btn_square_blue");

            // Encontrar el componente de texto de la descripción de la actividad
            Text descriptionText = activityObject.transform.Find("Description").GetComponent<Text>();

            // Asignar la imagen y el nombre de la actividad
            activityObject.GetComponent<Image>().sprite = activitySpritesMember[i];


            itemName = i.ToString();
            activityObject.name = itemName;
            activityNamesMember[i] = userMember[i].FirstName +" "+ userMember[i].LastName;
            activityObject.GetComponentInChildren<Text>().text = activityNamesMember[i];
            descriptionText.text = userMember[i].UserName;
            Debug.Log("En Loop");
        }
        activityPrefabMember.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRectMember.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRectMember.sizeDelta.y);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangePanelToDetail(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        try
        {
            Debug.Log("IndexData: " + indexData.ToString());
            LoadListMembers(indexData);
            BtnBackDetail.SetActive(true);
            PanelMembers.SetActive(true);
            PanelGroupMain.SetActive(false);
            BtnBackMain.SetActive(false);
            BtnActivities.SetActive(true);
            //PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
            PlayerPrefs.SetInt("IndexData", indexData);
            LoadDataPanel(groupBelongs[indexData]);
        }
        catch (ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: "+ indexData);
        }

    }

    public void ChangeMemberToStatistics(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        try
        {
            LoadRecord(userMember[indexData]._id, ListMembersBelongs._id);
            BtnBackMain.SetActive(false);
            PanelGroupDetail.SetActive(false);
            PanelMembers.SetActive(false);
            PanelStatistics.SetActive(true);
            BtnBackMain.SetActive(false);
            BtnBackToMember.SetActive(true);
            /*Text dataMember = PanelStatistics.GetComponent("MemberText") as Text;
            dataMember.text = userMember[indexData].UserName+"\n"+ userMember[indexData].FirstName+" "+ userMember[indexData].LastName;
            Debug.Log("ChangeMemberToStatistics: " + userMember[indexData].UserName + "\n" + userMember[indexData].FirstName + " " + userMember[indexData].LastName);*/
        }
        catch (ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }

    }

    public void ChangeStatisticsToMember()
    {
        PanelStatistics.SetActive(false);
        BtnBackToMember.SetActive(false);
        PanelMembers.SetActive(true);
    }

    public void ChangePanelDetailToMenuGroups()
    {
        //BtnBackDetail.SetActive(true);
        PanelMembers.SetActive(false);
        PanelGroupMain.SetActive(true);
        BtnBackMain.SetActive(true);
    }

    public void ChangePanelToActivities()
    {
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelMembers.SetActive(false);
        PanelStatistics.SetActive(false);
        BtnCreateActivities.SetActive(true);
        BtnActivities.SetActive(false);
        BtnBackMain.SetActive(false);
    }

    //Change the panel from PanelGroupDetail to PanelActivities.
    public void ChangePanelActivitiesToCreateActivities()
    {
        BtnCreateActivities.SetActive(false);
        PanelActivities.SetActive(true);
        PanelGroupDetail.SetActive(false);
        PanelStatistics.SetActive(false);
    }

    //Change the panel from PanelActivities to PanelGroupDetail.
    public void ChangePanelCreateActivitiesToActivities()
    {
        BtnCreateActivities.SetActive(false);
        PanelActivities.SetActive(true);
        PanelGroupDetail.SetActive(false);
        PanelStatistics.SetActive(false);
    }

    //Obtiene la escuela seleccionada cuando se crea el grupo
    public void DropdownitemSelectd(Dropdown dropdown)
    {
        int index = dropdown.value;
        GameAssigned = dropdown.options[index].text;
        //PlayerPrefs.SetString("NameSchool", GroupSchool);

    }


    public void CreateActivity()
    {
        TaskAssigned.Task task = new TaskAssigned.Task(0, GameAssigned, RewardAssigned, NameAssigned, "", 0, 0f, StartDateAssigned, EndDateAssigned, "Sprites/checkbox_unchecked");
        task.SaveTask();
    }

    /*public void ChangePanelToDetail(GameObject activityObj)
    {
        int indexData = int.Parse(activityObj.name);
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true 
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
        List<int> resultScores = new List<int>();
        foreach (var score in result)
        {
            resultScores.Add(score.FinalScore);
        }

        if (resultScores.Count > 0)
        {
            Debug.Log("resultScores: " + resultScores[0].ToString());
            graphScript.ShowGraph(resultScores);
        }
    }
}