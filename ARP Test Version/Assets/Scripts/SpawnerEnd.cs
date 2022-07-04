﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnd : MonoBehaviour
{
    public static SpawnerEnd current;
    Transform newCharacterEnd;
    public bool start = false;
    public Transform prefab;
    public Transform newParent;
    Vector3 pos;
    Vector3 destination = new Vector3(-0.439f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            newCharacterEnd.Translate(new Vector3(0, 0, 0.5f) * Time.deltaTime);
            pos = newCharacterEnd.localPosition;
            if(Vector3.Distance(pos, destination) < 0.003f)
            {
                newCharacterEnd.GetComponent<Animation>().Stop();
                DestroyObjectEnd();
                Controller.controlCharacter.CreateObject("Rigth");
            }

            /*if(Controller.controlCharacter.startLeft)
            {
                Invoke("CreateObjectEnd", 0.5f);
            }*/
        }
    }

    public void CreateObjectEnd()
    {
        newCharacterEnd = Instantiate(prefab, transform.position, transform.rotation);
        newCharacterEnd.SetParent(newParent);
        newCharacterEnd.localPosition = new Vector3(-0.09f, 0, 0);
        newCharacterEnd.GetComponent<Animation>().Play();
        start = true;
    }


    public void DestroyObjectEnd()
    {
        Destroy(newCharacterEnd.gameObject, 0.5f);
    }

}
