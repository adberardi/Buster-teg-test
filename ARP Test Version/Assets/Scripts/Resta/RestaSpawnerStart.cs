using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaSpawnerStart : MonoBehaviour
{
    //public Transform prefab;
    Transform prefab;
    //public Transform newParent;
    Transform newParent;
    Transform newCharacterEnd;
    Vector3 initialPosition;
    public bool start;
    public static RestaSpawnerStart current;
    //public Transform limit;
    Transform limit;

    private void Awake()
    {
        current = this;
        prefab = RestaController.controlCharacter.GetPrefab();
        newParent = RestaController.controlCharacter.GetNewParent();
        limit = RestaController.controlCharacter.GetLimitInsideHouse();
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
        if (RestaController.controlCharacter.onGoingGame)
        {
            if (start == true)
            {
                //Instantiate(prefab, transform.position, transform.rotation);
                newCharacterEnd.Translate(new Vector3(0, 0, RestaController.controlCharacter.Speed) * Time.deltaTime);
                Vector3 pos = newCharacterEnd.localPosition;
                Vector3 posLimit = limit.localPosition;
                if (Vector3.Distance(pos, posLimit) < 0.003f)
                {
                    RestaController.controlCharacter.endRoute = true;
                    //newCharacterEnd.localPosition = initialPosition;
                    DestroyObjectStart();
                    start = false;
                    if (RestaController.controlCharacter.peopleCounterLeft == 0)
                    {
                        RestaController.controlCharacter.onGoingGame = false;
                        RestaController.controlCharacter.ShowResult(RestaSpawnerResult.current.domain, RestaSpawnerResult.current.range);
                    }
                    else
                    {
                        if (RestaController.controlCharacter.endRoute)
                            //RestaController.controlCharacter.CreateObject();
                            CreateObjectStart();
                    }
                }
            }
        }

    }

    // Creates the character that will enter the house
    public void CreateObjectStart()
    {
        Debug.Log("CreateObjectStart");
        if (RestaController.controlCharacter.peopleCounterLeft > 0)
        {
            Debug.Log("Dentro del CreateObjectStart");
            newCharacterEnd = Instantiate(RestaController.controlCharacter.GetPrefab(), transform.position, transform.rotation);
            RestaController.controlCharacter.DecreasePeopleCounteLeft();
            newCharacterEnd.SetParent(newParent);
            newCharacterEnd.localPosition = new Vector3(0.478f, 0, 0);
            newCharacterEnd.GetComponent<Animation>().Play();
            start = true;
            RestaController.controlCharacter.onGoingGame = true;
            Debug.Log("Objeto creado ");
        }

    }

    // The character that enter in the house  at the end of the route (once stay inside the house) will be destroyed.
    public void DestroyObjectStart()
    {
        //newCharacterEnd.GetComponent<Animation>().Stop();
        Destroy(newCharacterEnd.gameObject, 0.5f);
        Debug.Log("Objeto Destruido");
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
