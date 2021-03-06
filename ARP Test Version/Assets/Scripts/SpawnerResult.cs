using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerResult : MonoBehaviour
{
    Transform newCharacterResult;
    public Transform prefab;
    public Transform newParent;
    public static SpawnerResult current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObjectResult(float newPos)
    {
        newCharacterResult = Instantiate(prefab, transform.position, transform.rotation);
        //Controller.controlCharacter.DecreasePeopleCounteRigth();
        newCharacterResult.SetParent(newParent);
        newCharacterResult.localPosition = new Vector3(newPos, 0, 0);
        newCharacterResult.GetComponent<Animation>().Play();
        //start = true;
        //Debug.Log("Objeto creado No:" + transform.childCount);
    }

    public void DestroyObjectResult()
    {
        //newCharacterStart.GetComponent<Animation>().Stop();
        Destroy(newCharacterResult.gameObject, 0.5f);
        Debug.Log("Objeto Destruido");
    }
}
