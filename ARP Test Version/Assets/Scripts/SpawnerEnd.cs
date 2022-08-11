using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnd : MonoBehaviour
{
    public static SpawnerEnd current;
    Transform newCharacterEnd;
    public bool start = false;
    Transform prefab;
    //public Transform newParent;
    Transform newParent;
    Vector3 pos;
    Vector3 destination = new Vector3(-0.439f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        prefab = Controller.controlCharacter.GetPrefab();
        newParent = Controller.controlCharacter.GetNewParent();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.controlCharacter.onGoingGame)
        {
            if (start == true)
            {
                newCharacterEnd.Translate(new Vector3(0, 0, Controller.controlCharacter.Speed) * Time.deltaTime);
                pos = newCharacterEnd.localPosition;
                if (Vector3.Distance(pos, destination) < 0.003f)
                {
                    newCharacterEnd.GetComponent<Animation>().Stop();
                    DestroyObjectEnd();
                    if (Controller.controlCharacter.peopleCounterLeft == 0)
                    {
                        //Controller.controlCharacter.CreateObject("Rigth");
                        Controller.controlCharacter.onGoingGame = false;
                        Controller.controlCharacter.startLeft = true;
                        Controller.controlCharacter.ShowResult();
                    }
                    
                    else
                        Controller.controlCharacter.CreateObject("Left");
                    
                }
            }
        }
        
    }

    // Creates the Character it's going out from the house
    public void CreateObjectEnd()
    {
        Debug.Log("GetPeoplecounterLeft:" + Controller.controlCharacter.peopleCounterLeft);
        if (Controller.controlCharacter.peopleCounterLeft > 0)
        {
            newCharacterEnd = Instantiate(prefab, transform.position, transform.rotation);
            Controller.controlCharacter.DecreasePeopleCounterLeft();
            newCharacterEnd.SetParent(newParent);
            newCharacterEnd.localPosition = new Vector3(-0.09f, 0, 0);
            newCharacterEnd.GetComponent<Animation>().Play();
            start = true;
        }

    }

    // The character that leaves the house at the end of the route will be destroyed.
    public void DestroyObjectEnd()
    {
        Destroy(newCharacterEnd.gameObject, 0.5f);
    }


    // Return the current position from the Character
    public Vector3 GetCharacterPosition()
    {
        return newCharacterEnd.localPosition;
    }

    // Sets the current position to character
    public void SetCurrentPosition(Vector3 currentPosition)
    {
        newCharacterEnd.localPosition = currentPosition;
        start = true;
    }
}
