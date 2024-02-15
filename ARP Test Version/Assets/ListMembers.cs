using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using ARProject.User;
using ARProject.Group;
using System.Linq;
using UnityEngine.SceneManagement;

public class ListMembers : MonoBehaviour
{
    private Group group;
    private User user;
    Group ListMembersBelongs;
    public string[] userBelongs;
    List<Group> groupBelongs;
    List<User> userMember;
    public Sprite[] activitySprites;    // Array de sprites para las actividades
    public RectTransform contentRectMember;  // Referencia al Rect Transform del contenido del Scroll Rect

    // Variables para la posición y    // Variables para el scroll bar
    public ScrollRect scrollRect;       // Referencia al Scroll Rect en la escena
    public RectTransform contentRect;  // Referencia al Rect Transform del contenido del Scroll Rect
    public float spacing = 10f;         // Espaciado entre las actividades tamaño de los objetos generados

    private Vector2 activitySize;
    private Vector2 activityPosition;
    public GameObject activityPrefabMember;   // Prefab del objeto de la actividad
    public Sprite[] activitySpritesMember;    // Array de sprites para las actividades
    public string[] activityNamesMember;      // Array de nombres para las actividades

    // Start is called before the first frame update
    void Start()
    {
        user = new User();
        group = new Group();
        groupBelongs = group.GetGroup(user);
        string itemName = "Item ";
        ChangePanelToDetail(PlayerPrefs.GetInt("IndexData"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePosSCroll(Vector2 value)
    {
        Debug.Log("UpdatePosSCroll: " + value.y);
    }

    public void ChangePanelDetailToMenuGroups()
    {
        //BtnBackDetail.SetActive(true);
        //PanelMembers.SetActive(false);
        //PanelGroupMain.SetActive(true);
        //BtnBackMain.SetActive(true);
        SceneManager.LoadScene(14);
    }

    public void ChangePanelToDetail(int pos)
    {

        /*BtnBackDetail.SetActive(true);
        PanelGroupDetail.SetActive(true);
        PanelGroupMain.SetActive(false);
        BtnBackMain.SetActive(false);*/
        Debug.Log("ListMembers -> ChangePanelToDetail: "+ groupBelongs[pos]._id.ToString());
        //LoadDataPanel(groupBelongs[pos]););
        PlayerPrefs.SetString("IDGroup", groupBelongs[pos]._id.ToString());
        LoadListMembers(pos);
        //LoadDataPanel(groupBelongs[pos]);
    }

    //ChangePanelToDetail(GameObject activityObj)
    public void ChangePanelToDetail()
    {
        //int indexData = int.Parse(activityObj.name);
        int indexData = PlayerPrefs.GetInt("IndexData");
        try
        {
            Debug.Log("IndexData: " + indexData);
            LoadListMembers(indexData);
            /*BtnBackDetail.SetActive(true);
            PanelMembers.SetActive(true);
            PanelGroupMain.SetActive(false);
            BtnBackMain.SetActive(false);
            BtnActivities.SetActive(true);*/
            //PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
            PlayerPrefs.SetInt("IndexData", indexData);
            //LoadDataPanel(groupBelongs[indexData]);
        }
        catch (System.ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }

    }

    public void LoadListMembers(int indexData)
    {
        PlayerPrefs.SetString("IDGroup", groupBelongs[indexData]._id.ToString());
        ListMembersBelongs = group.GetGroup(PlayerPrefs.GetString("IDGroup"));
        userBelongs = ListMembersBelongs.ParticipantsGroup;
        userMember = user.GetStudents(userBelongs);
        string itemName = "Item ";

        activitySpritesMember = new Sprite[(int)userBelongs.LongCount()];
        activityNamesMember = new string[(int)userBelongs.LongCount()];
        Debug.Log("Longitud de las lista participantes: " + activitySprites.Length);
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
            activityNamesMember[i] = userMember[i].FirstName + " " + userMember[i].LastName;
            activityObject.GetComponentInChildren<Text>().text = activityNamesMember[i];
            descriptionText.text = userMember[i].UserName;
            Debug.Log("En Loop");
        }
        activityPrefabMember.SetActive(false);
        // Actualizar el tamaño del contenido del Scroll Rect para mostrar todas las actividades
        contentRectMember.sizeDelta = new Vector2(activityPosition.x + activitySize.x / 2f + spacing, contentRectMember.sizeDelta.y);
    }


    public void ChangeMemberToStatistics(GameObject activityObj)
    {
        //int indexData = int.Parse(activityObj.name);
        //int indexData = 0;
        //int indexData = PlayerPrefs.GetInt("IndexData");
        int indexData = int.Parse(activityObj.name);
        Debug.Log("-> Listmembers: ChangeMemberToStatistics "+ indexData);
        try
        {
            PlayerPrefs.SetString("StatisticsFromAMember", userMember[indexData]._id.ToString());
            //LoadRecord(userMember[indexData]._id, ListMembersBelongs._id);
            Debug.Log("Cambiando de escena");
            SceneManager.LoadScene(22);
        }
        catch (System.ArgumentOutOfRangeException err)
        {
            Debug.Log("Se ha detectado un error del tipo ArgumentOutOfRangeException ListGroups - ChangePanelToDetail: " + err + ", el valor index: " + indexData);
        }
    }
}
