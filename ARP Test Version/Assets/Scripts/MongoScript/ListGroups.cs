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
using RegExpress = System.Text.RegularExpressions;

public class ListGroups : MonoBehaviour
{
    public GameObject UsernameText;
    //Panel Main; Muestra los grupos, funciona como vista "Home" de MenuGrupos.
    public GameObject PanelGroupMain;
    //Panel Detail; muestra informacion detalle del grupo seleccionado.
    public GameObject PanelGroupDetail;
    public GameObject PanelMembers;
    public GameObject PanelStatistics;
    public GameObject PanelActivities;
    public GameObject BtnActivities;
    public GameObject BtnMembers;
    public GameObject BtnCreateActivities;
    public GameObject BtnBackMain;
    public GameObject BtnBackDetail;
    public GameObject BtnBackToMember;
    public GameObject BtnBackHome;
    List<Group> groupBelongs;
    List<User> userMember;
    Group ListMembersBelongs;
    private string[] ListAssignedActivities;
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
    public string GameActivity { get; set; }
    private Group group;
    private User user;
    private GamesPlayed record;
    private TaskUser.Task activity;
    public GraphScript graphScript;

    public Text RewardActivity;
    public Text NameActivity;
    public Text StartDateActivity;
    public Text EndDateActivity;


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
        //graphScript = GetComponent<GraphScript>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entrando a ListGroups");
        group = new Group();
        record = new GamesPlayed();
        user = new User();
        activity = new TaskUser.Task();

        //UsernameText.GetComponent<Text>().text = PlayerPrefs.GetString("Username");
        groupBelongs = group.GetGroup(user);
        Debug.Log("ListGroups - Start: groupBelongs " + groupBelongs.Count);
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

        Debug.Log("ListGroups - Start: " + activitySprites.Length);
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
            //SceneManager.LoadScene(23);
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
        ListAssignedActivities = ListMembersBelongs.AssignedActivities;
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
            //LoadListMembers(indexData);
            BtnBackDetail.SetActive(true);
            PanelMembers.SetActive(true);
            PanelGroupMain.SetActive(false);
            BtnBackMain.SetActive(false);
            BtnActivities.SetActive(true);
            BtnBackHome.SetActive(false);
            BtnMembers.SetActive(true);
            //PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
            ChangePanelToActivities();
            PlayerPrefs.SetInt("IndexData", indexData);
            LoadDataPanel(groupBelongs[indexData]);
            //ChangeScene(23);
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
            PlayerPrefs.SetString("StatisticsFromAMember", userMember[indexData]._id.ToString());
            //LoadRecord(userMember[indexData]._id, ListMembersBelongs._id);
            Debug.Log("Cambiando de escena");
            ChangeScene(22);
        }
        catch (ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }
    }

 /*   public void ChangeMemberToStatistics(GameObject activityObj)
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
            //Text dataMember = PanelStatistics.GetComponent("MemberText") as Text;
            //dataMember.text = userMember[indexData].UserName+"\n"+ userMember[indexData].FirstName+" "+ userMember[indexData].LastName;
            //Debug.Log("ChangeMemberToStatistics: " + userMember[indexData].UserName + "\n" + userMember[indexData].FirstName + " " + userMember[indexData].LastName);
        }
        catch (ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }

    }*/

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
        BtnBackMain.SetActive(false);
    }

    public void ChangePanelToActivities()
    {
        BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelMembers.SetActive(false);
        PanelStatistics.SetActive(false);

        //If the user is the Group's Admin, can create a activity.
        if (group.GetGroup(PlayerPrefs.GetString("IDGroup")).Admin == user.GetSessionDataUser())
        {
            BtnCreateActivities.SetActive(true);
        }
        BtnActivities.SetActive(false);
        BtnBackMain.SetActive(false);
        BtnMembers.SetActive(true);
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
        BtnCreateActivities.SetActive(true);
        PanelActivities.SetActive(false);
        PanelGroupDetail.SetActive(true);
        PanelStatistics.SetActive(false);
        PanelMembers.SetActive(false);
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
        //Implementar busqueda de los GameObject - InputField
        Debug.Log("ListGroups -> CreateActivity: "+ RewardActivity);
        RewardAssigned = int.Parse(RewardActivity.text);
        PlayerPrefs.SetInt("RewardActivity",RewardAssigned);
        NameAssigned = NameActivity.text;
        StartDateAssigned = StartDateActivity.text;
        EndDateAssigned = StartDateActivity.text;
        Debug.Log("ListGroup - CreateActivity: "+ RegExpress.Regex.IsMatch(StartDateAssigned, @"^\d{2}/\d{2}/\d{4}$"));
        if((RegExpress.Regex.IsMatch(StartDateAssigned,@"^\d{2}/\d{2}/\d{4}$"))&&(RegExpress.Regex.IsMatch(EndDateAssigned, @"^\d{2}/\d{2}/\d{4}$")))
        {
            TaskAssigned.Task newtask = new TaskAssigned.Task(0, GameAssigned, RewardAssigned, NameAssigned, "", 0, 0f, StartDateAssigned, EndDateAssigned, "Sprites/checkbox_unchecked");
            activity.SaveTask(newtask);
            ListMembersBelongs = group.GetGroup(PlayerPrefs.GetString("IDGroup"));
            ListAssignedActivities = ListMembersBelongs.AssignedActivities;
            Debug.Log(" CreateActivity - ListAssignedActivities: " + ListAssignedActivities.Length);
            List<string> aux = new List<string>(ListAssignedActivities);
            aux.Add(newtask._id.ToString());
            group.UpdateGroupListAssignedActivities(PlayerPrefs.GetString("IDGroup"), aux);
            //Actualizar lista de Actividades Asignadas
            ChangePanelCreateActivitiesToActivities();
        }

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
        ChangePanelToActivities();
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
        Debug.Log("ListGroup - LoadDataPanel: " + groupItem.AssignedActivities.Length);
        for(int c = 0; (c < groupItem.AssignedActivities.Length) && flag; c++){
            TaskUser.Task nextDate = activity.GetTask(ObjectId.Parse(groupItem.AssignedActivities[c]));
            GameActivity = nextDate.GameType;
            DateTime dateEndConvert = DateTime.ParseExact(nextDate.EndDate, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateStartConvert = DateTime.ParseExact(nextDate.StartDate, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            int resultStartDates = DateTime.Compare(dateStartConvert, DateTime.Now);
            int resultEndDates = DateTime.Compare(dateEndConvert, DateTime.Now);
            Debug.Log("ListGroup - LoadDataPanel: resultStartDates " + resultStartDates);
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

    //Plays the activity.
    public void PlayActivity()
    {
        Debug.Log("ListGroup - PlayActivity:"+ GameActivity);
        int loadGame = 0;
        switch (GameActivity)
        {
            case "Cuenta Personas":
                loadGame = 2;
                break;
            case "Cuantas Personas Quedan":
                loadGame = 3;
                break;
            case "División":
                loadGame = 6;
                break;
            case "Multiplicación":
                loadGame = 5;
                break;
            case "Cuenta Personas - Especial":
                loadGame = 4;
                break;
            default:
                break;
        }
        if (loadGame != 0)
            ChangeScene(loadGame);
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
            //TODO: Pendiente hacer el pase de informacion a GrapScript
            //graphScript.ShowGraph(resultScores);
        }
    }
}