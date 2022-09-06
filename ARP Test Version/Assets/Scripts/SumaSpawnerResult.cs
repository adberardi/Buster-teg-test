using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumaSpawnerResult : MonoBehaviour
{
    Transform newCharacterResult;
    //public Transform prefab;
    Transform prefab;
    //public Transform newParent;
    Transform newParent;
    public AnimationClip idle;
    public AnimationClip animWalk;
    public static SumaSpawnerResult current;
    public float domain;
    public float range;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        prefab = SumaController.controlCharacter.GetPrefab();
        newParent = SumaController.controlCharacter.GetNewParent();
        domain = SumaController.controlCharacter.GetHouse().transform.localScale.x;
        //domain = 0.0450f;
        range = SumaController.controlCharacter.GetHouse().transform.localScale.z;
        //range = 0.0450f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Creates the character stays inside the house
    public void CreateObjectResult(float newPosX, float newPosZ)
    {
        newCharacterResult = Instantiate(prefab, transform.position, transform.rotation);
        //SumaController.controlCharacter.DecreasePeopleCounteRigth();
        newCharacterResult.SetParent(newParent);
        newCharacterResult.localPosition = new Vector3(newPosX, 0, newPosZ);
        newCharacterResult.GetComponent<Animation>().RemoveClip(animWalk);
        newCharacterResult.GetComponent<Animation>().AddClip(idle, "Idle");
        newCharacterResult.GetComponent<Animation>().Play("Idle");
    }

    // The character will be destroyed when tha game ends.
    public void DestroyObjectResult()
    {
        //newCharacterStart.GetComponent<Animation>().Stop();
        Destroy(newCharacterResult.gameObject, 0.5f);
        Debug.Log("Objeto Destruido");
    }
}
