using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnerStart : MonoBehaviour
{
    //public Transform prefab;
    Transform prefab;
    //public Transform newParent;
    Transform newParent;
    Transform newCharacterStart;
    Transform newCharacterEnd;
    Vector3 initialPosition;
    public bool start;
    public static SpawnerStart current;
    //public Transform limit;
    Transform limit;
    float valorPrueba = 0.296f;

    private void Awake()
    {
        current = this;
        prefab = Controller.controlCharacter.GetPrefab();
        newParent = Controller.controlCharacter.GetNewParent();
        limit = Controller.controlCharacter.GetLimitInsideHouse();
    }
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        initialPosition = new Vector3(0.379f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.controlCharacter.GetOnGoingGame())
        {
            if (start == true)
            {
                //Instantiate(prefab, transform.position, transform.rotation);
                newCharacterStart.Translate(new Vector3(0, 0, Controller.controlCharacter.Speed) * Time.deltaTime);
                Vector3 pos = newCharacterStart.localPosition;
                Vector3 posLimit = limit.localPosition;
                if (Vector3.Distance(pos, posLimit) < 0.003f)
                {
                    //newCharacterStart.localPosition = initialPosition;
                    DestroyObjectStart();
                    start = false;
                    if (Controller.controlCharacter.GetPeoplecounterRigth() == 0)
                    {
                        Controller.controlCharacter.startRigth = false;
                        Controller.controlCharacter.startLeft = true;
                        Controller.controlCharacter.CreateObject("Left");
                    }
                    else
                    {
                        Controller.controlCharacter.CreateObject("Rigth");
                    }
                }
            }
        }
        
    }

    // Creates the character that will enter the house
    public void CreateObjectStart()
    {
        if (Controller.controlCharacter.GetPeoplecounterRigth() > 0)
        {
            newCharacterStart = Instantiate(prefab, transform.position, transform.rotation);
            Controller.controlCharacter.DecreasePeopleCounteRigth();
            newCharacterStart.SetParent(newParent);
            newCharacterStart.localPosition = new Vector3(0.478f, 0, 0);
            newCharacterStart.GetComponent<Animation>().Play();
            start = true;
            //Debug.Log("Objeto creado No:" + transform.childCount);
        }

    }

    // The character that enter in the house  at the end of the route (once stay inside the house) will be destroyed.
    public void DestroyObjectStart()
    {
        //newCharacterStart.GetComponent<Animation>().Stop();
        Destroy(newCharacterStart.gameObject, 0.5f);
        Debug.Log("Objeto Destruido");
    }

    // Return the current position from the Character
    public Vector3 GetCharacterPosition()
    {
        return newCharacterStart.localPosition;
    }

    // Sets the current position to character
    public void SetCurrentPosition(Vector3 currentPosition)
    {
        newCharacterStart.localPosition = currentPosition;
        start = true;
    }
}
